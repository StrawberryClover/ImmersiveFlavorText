using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;

namespace RPToolkit.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    int currentParasol;

    public ConfigWindow(Plugin plugin) : base(
        $"{plugin.Name} Config Window",
        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
        ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.Size = new Vector2(324, 120);
        this.SizeCondition = ImGuiCond.Always;

        this.Configuration = plugin.Configuration;
        this.currentParasol = this.Configuration.SelectedParasolID;
    }

    public void Dispose() { }

    public override void Draw()
    {
        var enableTemperatureMessages = this.Configuration.enableTemperatureMessages;
        if (ImGui.Checkbox("Temperature update flavor text", ref enableTemperatureMessages))
        {
            this.Configuration.enableTemperatureMessages = enableTemperatureMessages;

            this.Configuration.Save();
        }

        ImGui.Separator();


        // can't ref a property, so use a local copy
        var enableRainPopup = this.Configuration.enableRainPopup;
        if (ImGui.Checkbox("Enable rain popup window", ref enableRainPopup))
        {
            this.Configuration.enableRainPopup = enableRainPopup;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }

        if (ImGui.BeginCombo("Selected Parasol", Plugin.parasols[currentParasol])) // The second parameter is the label previewed before opening the combo.
        {
            foreach (KeyValuePair<int, string> parasolData in Plugin.parasols)
            {
                bool is_selected = (currentParasol == parasolData.Key); // You can store your selection however you want, outside or inside your objects
                if (ImGui.Selectable(parasolData.Value, is_selected))
                {
                    currentParasol = parasolData.Key;
                    this.Configuration.SelectedParasolID = parasolData.Key;

                    this.Configuration.Save();
                }
                if (is_selected)
                    ImGui.SetItemDefaultFocus();   // You may set the initial focus when opening the combo (scrolling + for keyboard navigation support)
            }
            ImGui.EndCombo();
        }
    }
}
