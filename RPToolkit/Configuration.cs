using Dalamud.Configuration;
using Dalamud.Game.Text;
using Dalamud.Logging;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;

namespace RPToolkit
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 1;

        //public int SelectedParasolID { get; set; } = 58001;
        public bool enableTemperatureMessages { get; set; } = true;
        public XivChatType temperatureChatType = XivChatType.Echo;
        public bool enableShade = true;
        public bool showTemperatureSuggestionPopup = false;

        public int minPickpocketAmt = 1;
        public int maxPickpocketAmt = 20000;

        public List<ClimateOutfitData> climateOutfitData = new List<ClimateOutfitData>();

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
