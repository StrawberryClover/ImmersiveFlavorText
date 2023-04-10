using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Dalamud.Game.Text;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;
using Newtonsoft.Json.Linq;

namespace RPToolkit.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;
    private List<uint> jobEnums = new List<uint>();

    public ConfigWindow(Plugin plugin) : base(
        $"{plugin.Name} Config Window",
        ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.Size = new Vector2(450, 248);
        this.SizeCondition = ImGuiCond.FirstUseEver;

        this.Configuration = Plugin.Configuration;


        var baseJobEnums = (uint[])Enum.GetValues(typeof(ClimateOutfitData.Jobs));
        for (uint e = 0; e < baseJobEnums.Length; e++)
        {
            if (e == 7) e = 18;
            jobEnums.Add(baseJobEnums[e]);
        }
        for (uint e = 7; e < 18; e++)
        {
            if (baseJobEnums[e] != null) jobEnums.Add(baseJobEnums[e]);
        }
    }

    public void Dispose() { }

    public override void Draw()
    {
        if (ImGui.BeginTabBar("mainTabBar"))
        {
            if (ImGui.BeginTabItem("Climate"))
            {
                DrawClimateSettings();
                ImGui.EndTabItem();
            }

            ImGui.PushID("Misc");
            if (ImGui.BeginTabItem("Misc & Extras"))
            {
                DrawMiscSettings();
                ImGui.EndTabItem();
            }
            ImGui.PopID();

            ImGui.EndTabBar();
        }
    }

    private void DrawClimateSettings()
    {
        if (ImGui.BeginTabBar("climateTabBar"))
        {
            if (ImGui.BeginTabItem("General"))
            {
                DrawGeneralClimateSettings();
                ImGui.EndTabItem();
            }

            if (!Plugin.Singleton.glamourerAvailable)
            {
                ImGui.BeginDisabled();
            }
            if (ImGui.BeginTabItem("Climate Outfits"))
            {
                DrawClimateOutfitSettings();
                ImGui.EndTabItem();
            }
            ImGui.EndDisabled();
            if (!Plugin.Singleton.glamourerAvailable && ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
            {
                //ImGui.SetNextWindowBgAlpha(1);
                ImGui.BeginTooltip();
                ImGui.TextColored(new Vector4(1, 0, 0, 1), "Glamourer plugin is not loaded.");
                ImGui.Text("This is a required dependency if you want \r\nto change your outfits based on temperature.");
                ImGui.EndTooltip();
            }

            ImGui.EndTabBar();
        }
    }

    private void DrawGeneralClimateSettings()
    {
        var enableTemperatureMessages = this.Configuration.enableTemperatureMessages;
        if (ImGui.Checkbox("Temperature update flavor text", ref enableTemperatureMessages))
        {
            this.Configuration.enableTemperatureMessages = enableTemperatureMessages;

            this.Configuration.Save();
        }
        var selectedChatType = this.Configuration.temperatureChatType;
        if (ImGui.BeginCombo("Chat Type", selectedChatType.ToString())) // The second parameter is the label previewed before opening the combo.
        {
            foreach (XivChatType chatType in XivChatType.GetValues(typeof(XivChatType)))
            {
                bool is_selected = (chatType == selectedChatType); // You can store your selection however you want, outside or inside your objects
                if (ImGui.Selectable(chatType.ToString(), is_selected))
                {
                    this.Configuration.temperatureChatType = chatType;

                    this.Configuration.Save();
                }
                if (is_selected)
                    ImGui.SetItemDefaultFocus();   // You may set the initial focus when opening the combo (scrolling + for keyboard navigation support)
            }
            ImGui.EndCombo();
        }
        var enableShade = this.Configuration.enableShade;
        if (ImGui.Checkbox("(EXPERIMENTAL) Temperature is cooler in shadows", ref enableShade))
        {
            this.Configuration.enableShade = enableShade;

            this.Configuration.Save();
        }
        ImGui.Spacing();
        ImGui.Spacing();
        var showTemperatureSuggestionPopup = this.Configuration.showTemperatureSuggestionPopup;
        if (ImGui.Checkbox("Help contribute temperature data for zones", ref showTemperatureSuggestionPopup))
        {
            this.Configuration.showTemperatureSuggestionPopup = showTemperatureSuggestionPopup;
            this.Configuration.Save();
        }
        if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
        {
            ImGui.BeginTooltip();
            ImGui.Text("Specifies whether to show suggestion popup");
            ImGui.Text("window automatically, or if it should instead");
            ImGui.Text("notify user via chat that no data is available.");
            ImGui.EndTooltip();
        }
    }

    private void DrawClimateOutfitSettings()
    {
        for (int i = 0; i < this.Configuration.climateOutfitData.Count; i++)
        {
            ImGui.PushItemWidth(90);
            ImGui.PushID("jobCombo" + i);
            string jobLabelText = Enum.GetName(typeof(ClimateOutfitData.Jobs), this.Configuration.climateOutfitData[i].jobID);
            if (ImGui.BeginCombo("", jobLabelText != null ? jobLabelText : "Select Job"))
            {
                foreach (uint value in jobEnums)
                {
                    string valueName = Enum.GetName(typeof(ClimateOutfitData.Jobs), value);
                    ImGui.PushID(valueName + i);
                    bool isSelected = (value == this.Configuration.climateOutfitData[i].jobID);
                    if (ImGui.Selectable(valueName.Replace("_", " "), isSelected))
                    {
                        this.Configuration.climateOutfitData[i].jobID = value;
                        this.Configuration.Save();
                    }
                    if (isSelected)
                        ImGui.SetItemDefaultFocus();
                    ImGui.PopID();
                }
                ImGui.EndCombo();
            }
            ImGui.PopID();
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.PushItemWidth(175);
            ImGui.PushID("tempConditionCombo" + i);
            string climateConditionsLabelText = "";
            foreach (int value in Enum.GetValues(typeof(ClimateOutfitData.ClimateConditions)))
            {
                if ((this.Configuration.climateOutfitData[i].climateConditions & value) == value)
                    climateConditionsLabelText += $"{Enum.GetName(typeof(ClimateOutfitData.ClimateConditions), value)}, ";
            }
            climateConditionsLabelText = climateConditionsLabelText.TrimEnd(',', ' ').Replace("_", " ");
            if (ImGui.BeginCombo("", climateConditionsLabelText != "" ? climateConditionsLabelText : "Select Climate Conditions"))
            {
                foreach (int value in Enum.GetValues(typeof(ClimateOutfitData.ClimateConditions)))
                {
                    string valueName = Enum.GetName(typeof(ClimateOutfitData.ClimateConditions), value);
                    ImGui.PushID(valueName + i);
                    bool isSelected = (this.Configuration.climateOutfitData[i].climateConditions & value) == value;
                    if (ImGui.Selectable(valueName.Replace("_", " "), isSelected, ImGuiSelectableFlags.DontClosePopups))
                    {
                        this.Configuration.climateOutfitData[i].climateConditions ^= value;
                        this.Configuration.Save();
                    }
                    ImGui.PopID();
                }
                ImGui.EndCombo();
            }
            ImGui.PopID();
            ImGui.PopItemWidth();

            ImGui.SameLine();
            ImGui.PushID("updateAppearance" + i);
            if (ImGui.Button("Update Appearance"))
            {
                this.Configuration.climateOutfitData[i].customizationString = Plugin.Singleton.GetGlamourerCurrentEquipment();
                this.Configuration.Save();
            }
            if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
            {
                //ImGui.SetNextWindowBgAlpha(1);
                ImGui.BeginTooltip();
                ImGui.Text("Saves your currently worn glamourer equipment.");
                ImGui.EndTooltip();
            }
            ImGui.PopID();

            ImGui.SameLine();
            ImGui.PushID("deleteOutfitData" + i);
            if (ImGui.Button("X"))
            {
                this.Configuration.climateOutfitData.RemoveAt(i);
                this.Configuration.Save();
            }
        }

        if (ImGui.Button("+"))
        {
            this.Configuration.climateOutfitData.Add(new ClimateOutfitData(Plugin.Singleton.clientState.LocalPlayer != null ? Plugin.Singleton.clientState.LocalPlayer.ClassJob.Id : 0, 0, Plugin.Singleton.GetGlamourerCurrentEquipment()));
            this.Configuration.Save();
        }
    }

    private void DrawMiscSettings()
    {
        ImGui.PushItemWidth(120);
        int minPickpocketAmt = this.Configuration.minPickpocketAmt;
        if (ImGui.InputInt("Min Pickpocket Amt", ref minPickpocketAmt))
        {
            this.Configuration.minPickpocketAmt = minPickpocketAmt;
            this.Configuration.Save();
        }

        int maxPickpocketAmt = this.Configuration.maxPickpocketAmt;
        if (ImGui.InputInt("Max Pickpocket Amt", ref maxPickpocketAmt))
        {
            this.Configuration.maxPickpocketAmt = maxPickpocketAmt;
            this.Configuration.Save();
        }
        ImGui.PopItemWidth();
    }
}
