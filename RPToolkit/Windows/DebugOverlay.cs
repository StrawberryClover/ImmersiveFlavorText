using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.Graphics.Kernel;
using ImGuiNET;
using RPToolkit.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Windows
{
    internal class DebugOverlay : Window, IDisposable
    {
        public static Window window;
        private Plugin Plugin;
        public DebugOverlay(Plugin plugin) : base(
        "Debug Overlay", ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.NoNav | ImGuiWindowFlags.NoTitleBar
                        | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.AlwaysUseWindowPadding)
        {
            window = this;
            unsafe
            {
                float windowWidth = Device.Instance()->Width;
                float windowHeight = Device.Instance()->Height;
                this.SizeConstraints = new WindowSizeConstraints
                {
                    MinimumSize = new Vector2(windowWidth, windowHeight),
                    MaximumSize = new Vector2(windowWidth, windowHeight)
                };
                this.SizeCondition = ImGuiCond.Always;
            }

            this.Plugin = plugin;
            this.Position = new Vector2(0, 0);
            this.PositionCondition = ImGuiCond.Always;
        }

        public override void Draw()
        {
            //FFXIVClientStructs.FFXIV.Client.Graphics.Scene.Camera.WorldToScreenPoint
            if (TemperatureHandler.lastShadedHit.HasValue && Plugin.clientState.LocalPlayer != null)
            {
                ImGui.GetWindowDrawList().PathLineTo(FFXIVClientStructs.FFXIV.Client.Graphics.Scene.Camera.WorldToScreenPoint((Vector3)Plugin.clientState.LocalPlayer?.Position));
                ImGui.GetWindowDrawList().PathLineTo(FFXIVClientStructs.FFXIV.Client.Graphics.Scene.Camera.WorldToScreenPoint(TemperatureHandler.lastShadedHit.Value.Point));
                ImGui.GetWindowDrawList().PathStroke(0xff0000ff, ImDrawFlags.None, 2f);
            }
            /*if (Plugin.targetPosition != null)
            {
                ImGui.GetWindowDrawList().PathLineTo(FFXIVClientStructs.FFXIV.Client.Graphics.Scene.Camera.WorldToScreenPoint((Vector3)Plugin.clientState.LocalPlayer?.Position));
                ImGui.GetWindowDrawList().PathLineTo(FFXIVClientStructs.FFXIV.Client.Graphics.Scene.Camera.WorldToScreenPoint((Vector3)Plugin.targetPosition));
                ImGui.GetWindowDrawList().PathStroke(0x0000ffff, ImDrawFlags.None, 2f);
            }*/
            /*foreach (var gameObject in Plugin.Singleton._objectTable)
            {
                ImGui.GetWindowDrawList().PathLineTo(FFXIVClientStructs.FFXIV.Client.Graphics.Scene.Camera.WorldToScreenPoint((Vector3)Plugin.clientState.LocalPlayer?.Position));
                ImGui.GetWindowDrawList().PathLineTo(FFXIVClientStructs.FFXIV.Client.Graphics.Scene.Camera.WorldToScreenPoint(gameObject.Position));
                ImGui.GetWindowDrawList().PathStroke(0xffff0000, ImDrawFlags.None, 2f);
            }*/
        }

        public void Dispose()
        {

        }
    }
}
