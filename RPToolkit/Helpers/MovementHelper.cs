using Dalamud.Game;
using Dalamud.Logging;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Helpers
{
    internal unsafe class MovementHelper
    {
        public static void Initialize(DalamudPluginInterface pluginInterface)
        => pluginInterface.Create<MovementHelper>();
        
        private static nint* walkingPtr;
        public static bool isWalking { get { return walkingPtr == null ? false : *walkingPtr == 1 ? true : false; } private set { } }

        public MovementHelper()
        {
            walkingPtr = (IntPtr*)Plugin.SigScanner.GetStaticAddressFromSig("40 38 35 ?? ?? ?? ?? 75 2D");
            PluginLog.Information("Movement Helper Initialized");
            //PluginLog.Information($"Walking: {walkingPtr->ToString()}");
        }

        public static void SetWalking(bool walking)
        {
            if (walkingPtr != null)
                *walkingPtr = walking ? 1 : 0;
        }
    }
}
