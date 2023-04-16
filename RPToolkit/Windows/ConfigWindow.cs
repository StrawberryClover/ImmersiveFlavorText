using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Game.ClientState;
using Dalamud.Game.Text;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;
using Newtonsoft.Json.Linq;
using RPToolkit.Handlers;
using RPToolkit.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RPToolkit.Windows;

public class ConfigWindow : Window, IDisposable
{
    public static Window window;
    private Configuration Configuration;
    private List<uint> jobEnums = new List<uint>();

    public ConfigWindow(Plugin plugin) : base(
        $"{plugin.Name} Config Window",
        ImGuiWindowFlags.NoScrollWithMouse)
    {
        window = this;
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

            if (Plugin.Singleton.PluginInterface.IsDev)
            {
                ImGui.PushID("Debug");
                if (ImGui.BeginTabItem("Debug"))
                {
                    DrawDebugInfo();
                    ImGui.EndTabItem();
                }
                ImGui.PopID();
            }

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

            if (!Plugin.Singleton.glamourerAvailable) ImGui.BeginDisabled();
            if (ImGui.BeginTabItem("Climate Outfits"))
            {
                DrawClimateOutfitSettings();
                ImGui.EndTabItem();
            }
            if (!Plugin.Singleton.glamourerAvailable) ImGui.EndDisabled();
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
        bool useCelsius = Configuration.useCelsius.HasValue ? Configuration.useCelsius.Value : false;
        ImGui.Text("Temperature Unit");
        ImGui.SameLine();
        if (useCelsius) ImGui.BeginDisabled();
        ImGui.PushID("celsiusButton");
        if (ImGui.SmallButton("Celsius"))
        {
            Configuration.useCelsius = true;
            Configuration.Save();
        }
        ImGui.PopID();
        if (useCelsius) ImGui.EndDisabled();
        ImGui.SameLine();
        ImGui.SetCursorPosX(ImGui.GetCursorPosX() - 8);
        if (!useCelsius) ImGui.BeginDisabled();
        ImGui.PushID("fahrenheitButton");
        if (ImGui.SmallButton("Fahrenheit"))
        {
            Configuration.useCelsius = false;
            Configuration.Save();
        }
        ImGui.PopID();
        if (!useCelsius) ImGui.EndDisabled();
        ImGui.Separator();
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
            ImGui.PopID();
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

    private void DrawDebugInfo()
    {
        if (Plugin.Singleton.clientState.LocalPlayer != null)
        {
            unsafe {
                PushAreaDetails("Zone Info", Plugin.Singleton.clientState.TerritoryType.ToString(), Plugin.Data.GetExcelSheet<TerritoryType>()?.GetRow(Plugin.Singleton.clientState.TerritoryType)!.PlaceName.Value!.Name.RawString);
                PushAreaDetails("Area Info", Plugin.AreaInfo->AreaPlaceNameID.ToString(), Plugin.Data.GetExcelSheet<PlaceName>()?.GetRow(Plugin.AreaInfo->AreaPlaceNameID).NameNoArticle);
                PushAreaDetails("SubArea Info", Plugin.AreaInfo->SubAreaPlaceNameID.ToString(), Plugin.Data.GetExcelSheet<PlaceName>()?.GetRow(Plugin.AreaInfo->AreaPlaceNameID).NameNoArticle);
                if (TemperatureHandler.debugInfo != null)
                {
                    ImGui.Separator();
                    ImGui.Text(TemperatureHandler.debugInfo);
                }
                ImGui.Separator();
                ImGui.Text($"Weather: ({*WeatherHandler.currentWeather}) {Plugin.Data.GetExcelSheet<Weather>()?.GetRow(*WeatherHandler.currentWeather).Name}");
                /*PluginLog.Information($"\r\n({clientState.TerritoryType}) \"{Data.GetExcelSheet<TerritoryType>()?.GetRow(clientState.TerritoryType)!.PlaceName.Value!.Name.RawString}\" " +
                $"\r\n> ({AreaInfo->AreaPlaceNameID}) \"{Data.GetExcelSheet<PlaceName>()?.GetRow(AreaInfo->AreaPlaceNameID).NameNoArticle}\" " +
                $"\r\n> ({AreaInfo->SubAreaPlaceNameID}) \"{Data.GetExcelSheet<PlaceName>()?.GetRow(AreaInfo->SubAreaPlaceNameID).NameNoArticle}\" " +
                    $"\r\n Weather: ({*WeatherHandler.currentWeather}) {Data.GetExcelSheet<Weather>()?.GetRow(*WeatherHandler.currentWeather).Name}");*/
            }
        }
    }

    private float CalcTextSize(string text)
    {
        return ImGui.CalcTextSize(text).X + ImGui.GetStyle().CellPadding.X * 2;
    }

    private void PushAreaDetails(string label, string areaID, string areaName)
    {
        ImGui.Text(label);
        ImGui.SameLine();
        ImGui.Text("ID:");
        ImGui.SameLine();
        ImGui.PushID(label + "areaID" + areaID);
        ImGui.PushItemWidth(CalcTextSize("00000"));
        ImGui.InputText("", ref areaID, (uint)areaID.Length, ImGuiInputTextFlags.ReadOnly);
        ImGui.PopID();
        ImGui.SameLine();
        ImGui.Text(" Name:");
        ImGui.SameLine();
        ImGui.PushID(label + "areaName" + areaName);
        ImGui.PushItemWidth(CalcTextSize(areaName));
        ImGui.InputText("", ref areaName, (uint)areaName!.Length, ImGuiInputTextFlags.ReadOnly);
        ImGui.PopID();
    }
}
