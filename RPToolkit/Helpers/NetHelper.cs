using Dalamud.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit
{
    internal class NetHelper
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task SubmitDataAsync(ushort zoneID, string? zoneName, uint areaID, string? areaName, uint subAreaID, string? subAreaName, string? playerName, string? highTempSuggestion, string? lowTempSuggestion, string? weatherAdjustment = null)
        {
            string url = $"http://grinboo.ru/data/rptoolkit.php?zoneID={zoneID}&location={zoneName}&areaID={areaID}&areaName={areaName}&subAreaID={subAreaID}&subAreaName={subAreaName}&name={playerName}&highTemp={highTempSuggestion}&lowTemp={lowTempSuggestion}";
            if (weatherAdjustment != null) url += $"&weatherAdjustment={weatherAdjustment}";
            await client.GetStringAsync(url);
        }
    }
}
