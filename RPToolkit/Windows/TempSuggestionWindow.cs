using Dalamud.Interface.Windowing;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Dalamud.Interface.Windowing.Window;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RPToolkit.Windows
{
    public class TempSuggestionWindow : Window, IDisposable
    {
        private Plugin Plugin;

        float windowWidth = 370;
        float windowHeight = 180;
        string highTemperature = "";
        string lowTemperature = "";
        string weatherAdjustment = "";
        string errorText = "";
        Vector4 errorColor = new Vector4(1, 0, 0, 1);

        public TempSuggestionWindow(Plugin plugin) : base(
        "Temperature Suggestion Window", ImGuiWindowFlags.NoScrollWithMouse)
        {
            this.SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new Vector2(windowWidth, windowHeight),
                MaximumSize = new Vector2(windowWidth, windowHeight)
            };
            this.SizeCondition = ImGuiCond.FirstUseEver;

            this.Plugin = plugin;
            this.PositionCondition = ImGuiCond.Appearing;
            CalcPosition();
        }

        private void CalcPosition()
        {
            this.Position = new Vector2((Plugin.gameDimensions.Width / 2) - (windowWidth / 2), (Plugin.gameDimensions.Height / 2) - (windowHeight / 2) + (int)Math.Round(Plugin.gameDimensions.Height * 0.1f));
        }

        public override void OnOpen()
        {
            //CalcPosition();
        }

        public override void PreOpenCheck()
        {
            CalcPosition();
        }

        public override void Draw()
        {
            ImGui.Text("This zone doesn't seem to have any temperature data yet!");
            ImGui.Text("Enter high and low temperatures to log:");
            ImGui.PushID("highTempInput");
            ImGui.PushItemWidth(50);
            if (ImGui.InputText("High Temperature", ref highTemperature, 9, ImGuiInputTextFlags.AllowTabInput))
            {

            }
            ImGui.PopID();
            ImGui.PushID("lowTempInput");
            if (ImGui.InputText("Low Temperature", ref lowTemperature, 9, ImGuiInputTextFlags.AllowTabInput))
            {

            }
            ImGui.PopID();
            /*ImGui.PushID("weatherAdjustmentInput");
            if (ImGui.InputText("Weather Adjustment", ref weatherAdjustment, 3, ImGuiInputTextFlags.AllowTabInput))
            {

            }
            ImGui.PopID();*/
            ImGui.PopItemWidth();
            ImGui.TextColored(errorColor, errorText);
            ImGui.SameLine();
            Vector2 buttonSize = ImGui.CalcTextSize("Submit");
            ImGui.SetCursorPosX(windowWidth - (buttonSize.X + ImGui.GetStyle().CellPadding.Y) - 16);
            if (ImGui.Button("Submit"))
            {
                if (highTemperature != "" && lowTemperature != "")
                {
                    NetHelper.SubmitDataAsync(Plugin.clientState.TerritoryType, Plugin.clientState.LocalPlayer?.Name.ToString(), Plugin.Data.GetExcelSheet<TerritoryType>()?.GetRow(Plugin.clientState.TerritoryType).PlaceName.Value.Name.RawString, highTemperature, lowTemperature);
                    highTemperature = "";
                    lowTemperature = "";
                    weatherAdjustment = "";
                    errorText = "";
                    Plugin.WindowSystem.GetWindow("Temperature Suggestion Window").IsOpen = false;
                }
                else
                {
                    errorText = "Temps not entered!";
                }
            }
            ImGui.Text("(this can be disabled in settings)");
        }

        public void Dispose()
        {

        }
    }
}
