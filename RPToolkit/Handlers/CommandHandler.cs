using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Logging;
using Dalamud.Plugin;
using Dalamud.Utility;
using FFXIVClientStructs.FFXIV.Client.Graphics.Scene;
using Lumina.Excel.GeneratedSheets;
using RPToolkit.Extensions;
using RPToolkit.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static RPToolkit.Plugin;

namespace RPToolkit.Handlers
{
    internal class CommandHandler
    {
        public static void Initialize(DalamudPluginInterface pluginInterface)
        => pluginInterface.Create<CommandHandler>();

        public static CommandManager commandManager;

        private static Command[] commands = {
            new Command("/immersiveconfig", "Opens up the config window."),
            new Command("/checkweather", "Checks the weather, for testing purposes."),
            new Command("/pickpocket", "Pickpockets your target for a random amount of gil."),
            new Command("/suggestion", "Opens up the temperature suggestion window."),
            new Command("/setpose", "Specify a number to set your pose to a specific pose."),
            new Command("/spose", "Specify a number to set your pose to a specific pose.")
        };

        public CommandHandler(CommandManager _commandManager)
        {
            commandManager = _commandManager;

            foreach (Command cmd in commands)
            {
                commandManager.AddHandler(cmd.command, new CommandInfo(OnCommand)
                {
                    HelpMessage = cmd.description
                });
            }
        }

        private async void OnCommand(string command, string args)
        {
            PluginLog.Information(command);
            switch (command)
            {
                case "/immersiveconfig":
                    ConfigWindow.window.IsOpen = true;
                    break;
                case "/checkweather":
                    unsafe
                    {
                        PluginLog.Information($"Weather is currently: {*WeatherHandler.currentWeather}");
                        PluginLog.Information($"Named weather shows: {WeatherHandler.weathers[*WeatherHandler.currentWeather]}");
                    }
                    WeatherHandler.CheckWeather();
                    break;
                case "/umbrella":
                    ChatHelper.SendChatMessage("/fashion \"Prim Dot Parasol\"");
                    break;
                case "/pickpocket":
                    Random rnd = new Random();
                    ChatHelper.SendChatMessage($"/emote pickpockets <t> for {String.Format("{0:n0}", rnd.Next(Plugin.Configuration.minPickpocketAmt, Plugin.Configuration.maxPickpocketAmt))}.");
                    break;
                case "/suggestion":
                    TempSuggestionWindow.window.IsOpen = true;
                    break;
                case "/setpose":
                case "/spose":
                    int poseNum = -1;
                    PluginLog.Information(args);
                    if (int.TryParse(args, out poseNum))
                    {
                        if (poseNum >= 0)
                        {
                            _ = ChangePose(poseNum);
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        public async Task ChangePose(int poseGiven)
        {
            int steps = 0;
            unsafe
            {
                if (Plugin.Singleton.clientState.LocalPlayer != null)
                {
                    if (Plugin.Singleton.clientState.LocalPlayer is Character managedChara)
                    {
                        var chara = managedChara.AsNative();
                        if (chara != null)
                        {
                            var actionTimelineSheet = Plugin.Data.Excel.GetSheet<ActionTimeline>();
                            var animationId = chara->ActionTimelineManager.Driver.TimelineIds[0];
                            var animationEntry = actionTimelineSheet?.GetRow(animationId);
                            string animationKey = animationEntry?.Key ?? "unknown";
                            PluginLog.Information($"Animation: {animationKey} ({animationId})");
                            Regex rx = new Regex(@"(.*)(\d\d)");
                            string trimmedKey = animationKey.Replace("emote/", "").Replace("_loop", "").Replace("_start", "");
                            PluginLog.Information(trimmedKey);
                            Match m = rx.Match(trimmedKey);
                            string poseType;
                            int poseID = -1;
                            PluginLog.Information($"m: {m.Groups[2]}");
                            if (m != null && m.Groups[2] != null && m.Groups[2].ToString() != "" && int.TryParse(m.Groups[2].ToString(), out poseID))
                            {
                                poseType = m.Groups[1].ToString();
                            }
                            else
                            {
                                poseType = trimmedKey;
                                poseID = 0;
                            }

                            int maxPose = 0;
                            switch (poseType)
                            {
                                case "j_pose": //sitting on ground
                                case "jmn":
                                    maxPose = 3;
                                    break;
                                case "s_pose": //sitting on chair
                                case "sit":
                                    maxPose = 4;
                                    break;
                                case "l_pose": //laying
                                case "bed_liedown":
                                    maxPose = 2;
                                    break;
                                case "pose": //standing
                                case "normal/idle":
                                    maxPose = 6;
                                    break;

                                default: //Do nothing, not a valid pose
                                    break;
                            }
                            PluginLog.Information($"Capture: {poseType} - {poseID}");

                            if (poseGiven > maxPose) poseGiven = maxPose;
                            if (maxPose > 0 && poseID >= 0)
                            {
                                if (poseGiven < poseID) steps = (maxPose + 1 - poseID) + poseGiven;
                                else steps = poseGiven - poseID;

                                if (poseType == "l_pose" || poseType == "bed_liedown") //bed cycles through order in reverse... why???
                                {
                                    steps = (maxPose + 1) - steps;
                                    if (steps == maxPose + 1) steps = 0;
                                }
                            }

                            if (steps > 0)
                                PluginLog.Information($"Steps: {steps}");
                        }
                    }
                }
            }

            if (steps != 0 && steps < 10) // hard cap at 10 steps, just in case something breaks, plus 10 steps can easily fit in a macro.
            {
                for (int i = 0; i < steps; i++)
                {
                    ChatHelper.SendChatMessage("/cpose");
                    Random rnd = new Random();
                    int rndWait = 50 + rnd.Next(-25, 25);
                    PluginLog.Information(rndWait.ToString());
                    await Task.Delay(rndWait);
                }
            }
        }

        public static void Dispose()
        {
            if (commandManager != null)
            {
                foreach (Command cmd in commands)
                {
                    commandManager.RemoveHandler(cmd.command);
                }
            }
        }
    }
}
