using Dalamud.Game;
using Dalamud.Plugin;
using RPToolkit.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Helpers
{
    public unsafe class TimeHelper
    {
        public static void Initialize(DalamudPluginInterface pluginInterface)
        => pluginInterface.Create<TimeHelper>();

        internal static IntPtr TimeAsmPtr;
        internal static long* TrueTime;
        private uint* Time;

        public TimeHelper()
        {
            TrueTime = (long*)(Plugin.Framework.Address.BaseAddress + 0x1770);
            TimeAsmPtr = Plugin.SigScanner.ScanText("48 89 5C 24 ?? 57 48 83 EC 30 4C 8B 15") + 0x19;
            Time = (uint*)(TimeAsmPtr + 0x3);
        }
    }
}
