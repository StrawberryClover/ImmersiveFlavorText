using Dalamud.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RPToolkit.Localizations
{
    internal class Localization
    {
        //private static Dictionary<string, List<string>> loadedLocalization;
        public static LocalizationBase loadedLocalization;

        public static void LoadLocalization()
        {
            switch (Plugin.Singleton.clientState.ClientLanguage)
            {
                case Dalamud.ClientLanguage.English:
                default:
                    //loadedLocalization = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(Assembly.GetExecutingAssembly().GetManifestResourceStream("RPToolkit.Localizations.Localization_EN.json"));
                    loadedLocalization = new Localization_EN();
                    break;
            }
        }
    }
}
