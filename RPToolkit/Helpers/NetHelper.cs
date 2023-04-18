using Dalamud.Logging;
using Newtonsoft.Json;
using RPToolkit.Handlers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit
{
    internal class NetHelper
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task SubmitDataAsync(ushort zoneID, string? zoneName, uint areaID, string? areaName, uint subAreaID, string? subAreaName, string? playerName, string? highTempSuggestion, string? lowTempSuggestion, string? weatherAdjustment = null)
        {
            string url = "https://events.hookdeck.com/e/src_fESIsyHefnCM";
            var values = new Dictionary<string, string>
            {
                {"zoneID", zoneID.ToString() },
                {"location", zoneName },
                {"areaID", areaID.ToString() },
                {"areaName", areaName },
                {"subAreaID", subAreaID.ToString() },
                {"subAreaName", subAreaName },
                {"name", playerName },
                {"highTemp", highTempSuggestion + " " + TemperatureHandler.GetTemperatureUnit() },
                {"lowTemp", lowTempSuggestion + " " + TemperatureHandler.GetTemperatureUnit() }
            };
            if (weatherAdjustment != null) values.Add("weatherAdjustment", weatherAdjustment);
            var content = new FormUrlEncodedContent(values);
            await client.PostAsync(url, content);
        }
    }
}
