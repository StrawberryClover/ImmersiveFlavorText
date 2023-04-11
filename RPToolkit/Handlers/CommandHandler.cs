using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Logging;
using Dalamud.Plugin;
using RPToolkit.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            new Command("/suggestion", "Opens up the temperature suggestion window.")
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

        private void OnCommand(string command, string args)
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
                default:
                    break;
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
