using System;
using System.IO;
using System.Numerics;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.Utility;
using ImGuiNET;
using ImGuiScene;
using Lumina.Data.Files;
using static System.Net.Mime.MediaTypeNames;

namespace RPToolkit.Windows;

/*
public class NoRainWindow : Window, IDisposable
{
    private TextureWrap stowUmbrellaImage;
    private Plugin Plugin;

    float windowWidth = 250;
    float windowHeight = 160;

    public unsafe NoRainWindow(Plugin plugin) : base(
        "No Rain Prompt", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoResize)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(windowWidth, windowHeight),
            MaximumSize = new Vector2(windowWidth, windowHeight)
        };
        this.Plugin = plugin;

        CalcPosition();
        LoadTextures();
    }

    private void CalcPosition()
    {
        this.Position = new Vector2((Plugin.gameDimensions.Width / 2) - (windowWidth / 2), (Plugin.gameDimensions.Height / 2) - (windowHeight / 2) + (int)Math.Round(Plugin.gameDimensions.Height / 5.8));
    }

    public override void OnOpen()
    {
        CalcPosition();
    }

    public void Dispose()
    {
        this.stowUmbrellaImage.Dispose();
    }

    public override void Draw()
    {
        string line = "The rain seems to have stopped.";
        float textWidth = ImGui.CalcTextSize(line).X;

        ImGui.SetCursorPosX((windowWidth - textWidth) * 0.5f);
        ImGui.Text(line);

        line = "Would you like to put away your umbrella?";
        textWidth = ImGui.CalcTextSize(line).X;
        ImGui.SetCursorPosX((windowWidth - textWidth) * 0.5f);
        ImGui.Text(line);


        ImGui.Spacing();
        ImGui.SetCursorPosY(80);
        int imageSize = 64;
        ImGui.SetCursorPosX(windowWidth * 0.5f - imageSize / 2 - ImGui.GetStyle().CellPadding.X);
        if (ImGui.ImageButton(stowUmbrellaImage.ImGuiHandle, new Vector2(imageSize)))
        {
            ChatHelper.SendChatMessage($"/fashion");
            Plugin.WindowSystem.GetWindow("No Rain Prompt").IsOpen = false;
        }

        Vector2 buttonSize = ImGui.CalcTextSize("No");
        ImGui.SetCursorPosY(80 + imageSize - ImGui.GetStyle().CellPadding.Y / 2 - buttonSize.Y);
        ImGui.SetCursorPosX(windowWidth - (buttonSize.X + ImGui.GetStyle().CellPadding.Y) - 16);
        if (ImGui.Button("No"))
        {
            Plugin.WindowSystem.GetWindow("No Rain Prompt").IsOpen = false;
        }
    }

    public void ReloadTextures()
    {
        Dispose();
        LoadTextures();
    }

    public void LoadTextures()
    {
        // TODO: Find "put away" icon instead of this one
        var path = $"ui/icon/{86 / 1000 * 1000:000000}/{86:000000}_hr1.tex";
        TexFile? iconFile = Plugin.Data.GetFile<TexFile>(path);
        if (iconFile != null)
        {
            stowUmbrellaImage = Plugin.UiBuilder.LoadImageRaw(iconFile.GetRgbaImageData(), iconFile.Header.Width, iconFile.Header.Height, 4);
        }
    }
}
*/
