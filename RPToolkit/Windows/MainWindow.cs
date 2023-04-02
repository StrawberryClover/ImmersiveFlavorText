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
public class RainWindow : Window, IDisposable
{
    private TextureWrap parasolImage;
    private int lastParasolCheckID;
    private Plugin Plugin;

    float windowWidth = 250;
    float windowHeight = 160;

    public RainWindow(Plugin plugin) : base(
        "Rain Prompt", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoResize)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(windowWidth, windowHeight),
            MaximumSize = new Vector2(windowWidth, windowHeight)
        };

        this.Plugin = plugin;

        lastParasolCheckID = plugin.Configuration.SelectedParasolID;
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
        this.parasolImage.Dispose();
    }

    public override void Draw()
    {
        if (lastParasolCheckID != Plugin.Configuration.SelectedParasolID)
        {
            ReloadTextures();
            lastParasolCheckID = Plugin.Configuration.SelectedParasolID;
        }
        string line = "You feel rain begin to fall upon your skin.";
        float textWidth = ImGui.CalcTextSize(line).X;

        ImGui.SetCursorPosX((windowWidth - textWidth) * 0.5f);
        ImGui.Text(line);

        line = "Would you like to bring out your umbrella?";
        textWidth = ImGui.CalcTextSize(line).X;
        ImGui.SetCursorPosX((windowWidth - textWidth) * 0.5f);
        ImGui.Text(line);


        ImGui.Spacing();
        ImGui.SetCursorPosY(80);
        int imageSize = 64;
        ImGui.SetCursorPosX(windowWidth * 0.5f - imageSize / 2 - ImGui.GetStyle().CellPadding.X);
        if (ImGui.ImageButton(parasolImage.ImGuiHandle, new Vector2(imageSize)))
        {
            ChatHelper.SendChatMessage($"/fashion \"{Plugin.parasols[Plugin.Configuration.SelectedParasolID]}\"");
            Plugin.WindowSystem.GetWindow("Rain Prompt").IsOpen = false;
        }

        Vector2 buttonSize = ImGui.CalcTextSize("No");
        ImGui.SetCursorPosY(80 + imageSize - ImGui.GetStyle().CellPadding.Y / 2 - buttonSize.Y);
        ImGui.SetCursorPosX(windowWidth - (buttonSize.X + ImGui.GetStyle().CellPadding.Y) - 16);
        if (ImGui.Button("No"))
        {
            Plugin.WindowSystem.GetWindow("Rain Prompt").IsOpen = false;
        }
    }

    public void ReloadTextures()
    {
        Dispose();
        LoadTextures();
    }

    public void LoadTextures()
    {
        // you might normally want to embed resources and load them from the manifest stream
        //var imagePath = Path.Combine(Plugin.PluginInterface.AssemblyLocation.Directory?.FullName!, "goat.png");
        //this.GoatImage = Plugin.PluginInterface.UiBuilder.LoadImage(imagePath);

        var path = $"ui/icon/{Plugin.Configuration.SelectedParasolID / 1000 * 1000:000000}/{Plugin.Configuration.SelectedParasolID:000000}_hr1.tex";
        TexFile? iconFile = Plugin.Data.GetFile<TexFile>(path);
        if (iconFile != null)
        {
            parasolImage = Plugin.UiBuilder.LoadImageRaw(iconFile.GetRgbaImageData(), iconFile.Header.Width, iconFile.Header.Height, 4);
        }
    }
}
*/
