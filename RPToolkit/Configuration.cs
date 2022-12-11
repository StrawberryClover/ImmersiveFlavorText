using Dalamud.Configuration;
using Dalamud.Logging;
using Dalamud.Plugin;
using System;

namespace RPToolkit
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 1;

        public bool enableRainPopup { get; set; } = true;
        public bool enableTemperatureMessages { get; set; } = true;
        public int SelectedParasolID { get; set; } = 58001;

        // the below exist just to make saving less cumbersome
        [NonSerialized]
        private DalamudPluginInterface? PluginInterface;

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.PluginInterface = pluginInterface;
        }

        public void Save()
        {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
