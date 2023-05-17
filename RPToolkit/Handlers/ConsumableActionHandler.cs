using Dalamud.Hooking;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.Game;
using RPToolkit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalamud.Logging;
using Lumina.Excel.GeneratedSheets;
using RPToolkit.Data;
using System.Diagnostics;

namespace RPToolkit.Handlers
{
    internal unsafe class ConsumableActionHandler
    {
        public static void Initialize(DalamudPluginInterface pluginInterface)
        => pluginInterface.Create<ConsumableActionHandler>();

        private delegate bool UseActionDelegate(IntPtr manager, ActionType actionType, uint actionId, GameObjectID targetId, uint a4, uint a5,
        uint a6, IntPtr a7);
        private static Hook<UseActionDelegate>? useActionHook;

        private Stopwatch sw;
        private float messageCooldown = 30f;
        private string lastMessage;

        public ConsumableActionHandler()
        {
            var hookPtr = (IntPtr)ActionManager.MemberFunctionPointers.UseAction;
            useActionHook = Hook<UseActionDelegate>.FromAddress(hookPtr, OnUseAction);
            useActionHook.Enable();

            sw = new Stopwatch();
            sw.Start();
        }

        public static void Dispose()
        {
            useActionHook.Dispose();
        }

        private bool OnUseAction(IntPtr manager, ActionType actionType, uint actionId, GameObjectID targetId, uint a4, uint a5, uint a6, IntPtr a7)
        {
            PluginLog.Information($"{actionType}, {actionId}, {targetId.ObjectID}, {a4}, {a5}, {a6}, {a7}");
            if (actionType == ActionType.Item)
            {
                var itemId = actionId;
                bool itemHQ = false;
                if (itemId >= 1000000)
                {
                    itemHQ = true;
                    itemId -= 1000000;
                }
                PluginLog.Information($"{itemId}, {itemHQ}");
                var item = Plugin.Data.GetExcelSheet<Item>().GetRow(itemId);
                PluginLog.Information($"{(itemHQ ? "(HQ) " : "")}{item.Name.RawString}");

                if (Consumables.consumables.ContainsKey(itemId))
                {
                    var consumable = Consumables.consumables[itemId];
                    if (Plugin.Configuration.showFoodMessages)
                    {
                        float secondsElapsed = sw.ElapsedMilliseconds / 1000f;
                        if (secondsElapsed >= messageCooldown || consumable.flavorText != lastMessage)
                        {
                            string name = "Food";
                            var flavorText = consumable.flavorText.Replace("<food>", item.Name.RawString.ToLower()).Replace("<temp>", consumable.temp.ToString().ToLower()).Replace("<type>", consumable.type.ToString().ToLower());
                            //if (itemHQ) flavorText = "Wow, what amazing quality! " + flavorText;
                            if (consumable.type == Consumables.FoodType.LightDrink || consumable.type == Consumables.FoodType.RefreshingDrink)
                                name = "Drink";

                            ChatHelper.Echo(flavorText, Plugin.Configuration.flavorTextChatType, name);
                            lastMessage = consumable.flavorText;
                            sw.Restart();
                        }
                    }

                    if (Plugin.Configuration.foodAffectsTemperature)
                    {
                        // Hot Temperature
                        float temperatureAdjustment = 6f;
                        if (itemHQ) temperatureAdjustment = 12f;

                        switch (consumable.temp)
                        {
                            // Warm, not hot, warm you by only 75%
                            case Consumables.FoodTemp.Warm:
                                temperatureAdjustment *= 0.75f;
                                break;
                            // Cold, invert temperature adjustment instead
                            case Consumables.FoodTemp.Cold:
                                temperatureAdjustment = -temperatureAdjustment;
                                break;
                            default:
                                break;
                        }
                        TemperatureHandler.consumableAdjustment += temperatureAdjustment;
                        if (TemperatureHandler.consumableAdjustment > TemperatureHandler.maxConsumableAdjustment) TemperatureHandler.consumableAdjustment = TemperatureHandler.maxConsumableAdjustment;
                        if (TemperatureHandler.consumableAdjustment < -TemperatureHandler.maxConsumableAdjustment) TemperatureHandler.consumableAdjustment = -TemperatureHandler.maxConsumableAdjustment;
                    }
                }
            }

            return useActionHook!.Original(manager, actionType, actionId, targetId, a4, a5, a6, a7);
        }
    }
}
