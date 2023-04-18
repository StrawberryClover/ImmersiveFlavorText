using CharacterPanelRefined;
using Dalamud.Game;
using Dalamud.Game.ClientState;
using Dalamud.Hooking;
using Dalamud.Logging;
using Dalamud.Memory;
using Dalamud.Plugin;
using Dalamud.Utility.Signatures;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.Graphics;
using FFXIVClientStructs.FFXIV.Component.GUI;
using FFXIVClientStructs.Interop;
using FFXIVClientStructs.STD;
using RPToolkit.Data;
using RPToolkit.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Lumina.Data.Parsing.Uld.NodeData;

namespace RPToolkit.Handlers
{
    // All of this is very experimental...
    public class CharacterWindowUIHandler
    {
        private static bool updateCharacterPanel = false;

        public static void Initialize(DalamudPluginInterface pluginInterface)
        => pluginInterface.Create<CharacterWindowUIHandler>();

        public CharacterWindowUIHandler()
        {
            SignatureHelper.Initialise(this);
            if (updateCharacterPanel)
            {
                tooltips = new Tooltips();
                characterStatusOnSetup.Enable();
            }
            PluginLog.Information($"{nameof(CharacterWindowUIHandler)} Initialized.");
        }

        private static Tooltips tooltips;

        [Signature("4C 8B DC 55 53 41 56 49 8D 6B A1 48 81 EC F0 00 00 00 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 07", DetourName = nameof(CharacterStatusOnSetupDetour))]
        private static Hook<AddonOnSetup> characterStatusOnSetup = null!;

        private unsafe delegate void* AddonOnSetup(AtkUnitBase* atkUnitBase, int a2, void* a3);

        private unsafe void* CharacterStatusOnSetupDetour(AtkUnitBase* atkUnitBase, int a2, void* a3)
        {
            var val = characterStatusOnSetup.Original(atkUnitBase, a2, a3);
            try
            {
                CharacterStatusOnSetup(atkUnitBase);
            }
            catch (Exception e)
            {
                PluginLog.Log(e, "Failed to setup Character Panel");
            }
            return val;
        }

        internal unsafe void CharacterStatusOnSetup(AtkUnitBase* atkUnitBase)
        {
            PluginLog.Information("In Character Status On Setup");
            var uiState = UIState.Instance();

            var gearProp = atkUnitBase->UldManager.SearchNodeById(80);
            var avgItemLvlNode = gearProp->ChildNode;

            var gearLabel = gearProp->ChildNode->PrevSiblingNode->ChildNode->PrevSiblingNode->GetAsAtkTextNode();
            PluginLog.Information(gearLabel->NodeText.ToString());
            gearLabel->SetText("hewwo");
            var gearIcon = gearProp->ChildNode->PrevSiblingNode->ChildNode->PrevSiblingNode->PrevSiblingNode->GetAsAtkImageNode();
            //gearIcon->LoadTexture("ui/uld/Character.tex");
            gearIcon->PartId = 14;


            // Attempt to make new category (so far attempts have been unsuccessful)
            ExpandBaseNodeList(atkUnitBase, 2);
            var newGearNode = CloneNode(gearProp);
            newGearNode->ParentNode = atkUnitBase->RootNode;
            newGearNode->PrevSiblingNode = atkUnitBase->UldManager.SearchNodeById(86);
            newGearNode->Y = 400;
            newGearNode->NodeID = atkUnitBase->UldManager.NodeListCount + (uint)1;
            atkUnitBase->UldManager.NodeList[atkUnitBase->UldManager.NodeListCount++] = newGearNode;
            PluginLog.Information(newGearNode->NodeID.ToString());

            var newGearComponentNode = CloneNode((AtkComponentNode*)avgItemLvlNode);
            newGearComponentNode->AtkResNode.ParentNode = newGearNode;
            newGearComponentNode->AtkResNode.Y = 400;
            newGearComponentNode->AtkResNode.NodeID = atkUnitBase->UldManager.NodeListCount + (uint)1;
            atkUnitBase->UldManager.NodeList[atkUnitBase->UldManager.NodeListCount++] = (AtkResNode*)newGearComponentNode;
            PluginLog.Information(newGearComponentNode->AtkResNode.NodeID.ToString());



            ExpandBaseNodeList(atkUnitBase, 2);
            var parentNode = (AtkComponentNode*)avgItemLvlNode;
            var collisionNode = parentNode->Component->UldManager.RootNode;
            parentNode->AtkResNode.Height += 20;
            collisionNode->Height += 20;

            var numberNode = (AtkTextNode*)collisionNode->PrevSiblingNode;
            var labelNode = (AtkTextNode*)numberNode->AtkResNode.PrevSiblingNode;
            var newNumberNode = CloneNode(numberNode);
            var prevSiblingNode = labelNode->AtkResNode.PrevSiblingNode;
            //labelNode->AtkResNode.PrevSiblingNode = (AtkResNode*)newNumberNode;
            newNumberNode->AtkResNode.ParentNode = (AtkResNode*)atkUnitBase->RootNode;
            //newNumberNode->AtkResNode.NextSiblingNode = (AtkResNode*)labelNode;
            newNumberNode->AtkResNode.Y = parentNode->AtkResNode.Height - 24;
            newNumberNode->TextColor = new ByteColor { A = 0xFF, R = 0xA0, G = 0xA0, B = 0xA0 };
            newNumberNode->NodeText.StringPtr = (byte*)MemoryHelper.GameAllocateUi((ulong)newNumberNode->NodeText.BufSize);
            parentNode->Component->UldManager.NodeList[parentNode->Component->UldManager.NodeListCount++] = (AtkResNode*)newNumberNode;
            var newLabelNode = CloneNode(labelNode);
            newNumberNode->AtkResNode.PrevSiblingNode = (AtkResNode*)newLabelNode;
            newLabelNode->AtkResNode.ParentNode = (AtkResNode*)atkUnitBase->RootNode;
            //newLabelNode->AtkResNode.PrevSiblingNode = prevSiblingNode;
            //newLabelNode->AtkResNode.NextSiblingNode = (AtkResNode*)newNumberNode;
            newLabelNode->AtkResNode.Y = parentNode->AtkResNode.Height - 24;
            newLabelNode->TextColor = new ByteColor { A = 0xFF, R = 0xA0, G = 0xA0, B = 0xA0 };
            newLabelNode->NodeText.StringPtr = (byte*)MemoryHelper.GameAllocateUi((ulong)newLabelNode->NodeText.BufSize);
            newLabelNode->SetText("Test");
            parentNode->Component->UldManager.NodeList[parentNode->Component->UldManager.NodeListCount++] = (AtkResNode*)newLabelNode;

            /*ExpandNodeList(newGearNode->GetAsAtkComponentNode(), 1);
            var newComponentNode = (AtkComponentNode*)CloneNode(avgItemLvlNode);
            newComponentNode->AtkResNode.ParentNode = newGearNode;
            newComponentNode->AtkResNode.NodeID = newGearNode->NodeID + (uint)1;
            atkUnitBase->UldManager.NodeList[atkUnitBase->UldManager.NodeListCount++] = (AtkResNode*)newComponentNode;*/
            //avgItemLvlNode->PrevSiblingNode->ToggleVisibility(false); // header

            /*var _node = gearProp;
            PluginLog.Information(_node.ChildCount.ToString());
            var node = _node.ChildNode->PrevSiblingNode->ChildNode;
            if (node != null)
            {
                var textNode = node->GetAsAtkTextNode();
                PluginLog.Information($"{(textNode != null ? textNode->NodeText.ToString() : "")} - {node->GetType()} [{node->ChildCount.ToString()}]");
            }
            else
            {
                PluginLog.Information("null");
            }*/
            //PluginLog.Information($"{avgItemLvlNode->PrevSiblingNode->ParentNode[0].ChildNode->ToString()}, {(textNode != null ? textNode->NodeText : "null")}");
            /*expectedDamagePtr = AddStatRow((AtkComponentNode*)avgItemLvlNode, Localization.Panel_Damage_per_100_Potency, true);
            SetTooltip(expectedDamagePtr, Tooltips.Entry.ExpectedDamage);*/

            /*for (var i = 0; i < atkUnitBase->UldManager.NodeListCount; i++)
            {
                if (atkUnitBase->UldManager.NodeList[i] == null) continue;
                var node = atkUnitBase->UldManager.NodeList[i];
                var textNode = node->GetAsAtkTextNode();
                PluginLog.Information($"{i}, {(textNode != null ? textNode->NodeText.ToString() : "")} - {node->GetType()} [{node->ChildCount.ToString()}]");
                for (var b = 0; b < node->ChildCount; b++)
                {
                    var subNode = node[i];
                    var subTextNode = subNode.GetAsAtkTextNode();
                    PluginLog.Information($"    {b}, {(subTextNode != null ? subTextNode->NodeText.ToString() : "")} [{subNode.ChildCount.ToString()}]");
                }
            }*/

            TemperatureHandler.UpdateTemps();
            int hours = DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Hour;
            int minutes = DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Minute;
            int seconds = DateTimeOffset.FromUnixTimeSeconds(*TimeHelper.TrueTime).Second;
            float calcHours = hours + ((float)minutes / 60) + ((float)seconds / 60 / 60);
            tooltips.ReloadTemperatureTooltip(Climates.GetTemperature(Plugin.Singleton.clientState.TerritoryType, Plugin.AreaInfo->AreaPlaceNameID, Plugin.AreaInfo->SubAreaPlaceNameID, calcHours) + TemperatureHandler.temperatureDivergence, Climates.weatherTemperatures.ContainsKey(*WeatherHandler.currentWeather) ? Climates.weatherTemperatures[*WeatherHandler.currentWeather] : Climates.weatherTemperatures[0]);

            var testRow = AddStatRow((AtkComponentNode*)avgItemLvlNode, "Temperature", false);
            var test2Row = AddStatRow((AtkComponentNode*)avgItemLvlNode, "Temperature2");
            testRow->SetText(TemperatureHandler.currentTemp.ToString() + "℉");
            test2Row->SetText(TemperatureHandler.currentTemp.ToString() + "C");
            SetTooltip((AtkComponentNode*)avgItemLvlNode, Tooltips.Entry.Hunger);
            SetTooltip(testRow, Tooltips.Entry.Temperature);
            SetTooltip(test2Row, Tooltips.Entry.Temperature);


            //var testRow3 = AddStatRow((AtkComponentNode*)newNode->ChildNode, "Temperature", false);

            var characterStatusPtr = (IntPtr)atkUnitBase;

            //UpdateCharacterPanelForJob(job, lvl);
        }

        private unsafe void SetTooltip(AtkComponentNode* parentNode, Tooltips.Entry entry)
        {
            if (parentNode == null)
                return;
            var collisionNode = parentNode->Component->UldManager.RootNode;
            if (collisionNode == null)
                return;

            var ttMgr = AtkStage.GetSingleton()->TooltipManager;
            var ttMsg = Find(ttMgr.TooltipMap.Head->Parent, collisionNode).Value;
            ttMsg->AtkTooltipArgs.Text = (byte*)tooltips[entry];
        }

        private unsafe void SetTooltip(AtkTextNode* node, Tooltips.Entry entry)
        {
            SetTooltip((AtkComponentNode*)node->AtkResNode.ParentNode, entry);
        }

        private unsafe AtkTextNode* AddStatRow(AtkComponentNode* parentNode, string label, bool hideOriginal = false)
        {
            ExpandNodeList(parentNode, 2);
            var collisionNode = parentNode->Component->UldManager.RootNode;
            if (!hideOriginal)
            {
                parentNode->AtkResNode.Height += 20;
                collisionNode->Height += 20;
            }

            var numberNode = (AtkTextNode*)collisionNode->PrevSiblingNode;
            var labelNode = (AtkTextNode*)numberNode->AtkResNode.PrevSiblingNode;
            var newNumberNode = CloneNode(numberNode);
            var prevSiblingNode = labelNode->AtkResNode.PrevSiblingNode;
            labelNode->AtkResNode.PrevSiblingNode = (AtkResNode*)newNumberNode;
            newNumberNode->AtkResNode.ParentNode = (AtkResNode*)parentNode;
            newNumberNode->AtkResNode.NextSiblingNode = (AtkResNode*)labelNode;
            newNumberNode->AtkResNode.Y = parentNode->AtkResNode.Height - 24;
            newNumberNode->TextColor = new ByteColor { A = 0xFF, R = 0xA0, G = 0xA0, B = 0xA0 };
            newNumberNode->NodeText.StringPtr = (byte*)MemoryHelper.GameAllocateUi((ulong)newNumberNode->NodeText.BufSize);
            parentNode->Component->UldManager.NodeList[parentNode->Component->UldManager.NodeListCount++] = (AtkResNode*)newNumberNode;
            var newLabelNode = CloneNode(labelNode);
            newNumberNode->AtkResNode.PrevSiblingNode = (AtkResNode*)newLabelNode;
            newLabelNode->AtkResNode.ParentNode = (AtkResNode*)parentNode;
            newLabelNode->AtkResNode.PrevSiblingNode = prevSiblingNode;
            newLabelNode->AtkResNode.NextSiblingNode = (AtkResNode*)newNumberNode;
            newLabelNode->AtkResNode.Y = parentNode->AtkResNode.Height - 24;
            newLabelNode->TextColor = new ByteColor { A = 0xFF, R = 0xA0, G = 0xA0, B = 0xA0 };
            newLabelNode->NodeText.StringPtr = (byte*)MemoryHelper.GameAllocateUi((ulong)newLabelNode->NodeText.BufSize);
            newLabelNode->SetText(label);
            parentNode->Component->UldManager.NodeList[parentNode->Component->UldManager.NodeListCount++] = (AtkResNode*)newLabelNode;
            if (hideOriginal)
            {
                labelNode->AtkResNode.ToggleVisibility(false);
                numberNode->TextColor.A = 0; // toggle visibility doesn't work since it's constantly updated by the game
            }

            return newNumberNode;
        }

        private unsafe void ExpandNodeList(AtkComponentNode* componentNode, ushort addSize)
        {
            var originalList = componentNode->Component->UldManager.NodeList;
            var originalSize = componentNode->Component->UldManager.NodeListCount;
            var newSize = (ushort)(componentNode->Component->UldManager.NodeListCount + addSize);
            var oldListPtr = new IntPtr(originalList);
            var newListPtr = MemoryHelper.GameAllocateUi((ulong)((newSize + 1) * 8));
            var clone = new IntPtr[originalSize];
            Marshal.Copy(oldListPtr, clone, 0, originalSize);
            Marshal.Copy(clone, 0, newListPtr, originalSize);
            componentNode->Component->UldManager.NodeList = (AtkResNode**)newListPtr;
        }

        private unsafe void ExpandBaseNodeList(AtkUnitBase* atkUnitBase, ushort addSize)
        {
            var originalList = atkUnitBase->UldManager.NodeList;
            var originalSize = atkUnitBase->UldManager.NodeListCount;
            var newSize = (ushort)(atkUnitBase->UldManager.NodeListCount + addSize);
            var oldListPtr = new IntPtr(originalList);
            var newListPtr = MemoryHelper.GameAllocateUi((ulong)((newSize + 1) * 8));
            var clone = new IntPtr[originalSize];
            Marshal.Copy(oldListPtr, clone, 0, originalSize);
            Marshal.Copy(clone, 0, newListPtr, originalSize);
            atkUnitBase->UldManager.NodeList = (AtkResNode**)newListPtr;
        }

        private static unsafe AtkTextNode* CloneNode(AtkTextNode* original)
        {
            var size = sizeof(AtkTextNode);
            var allocation = MemoryHelper.GameAllocateUi((ulong)size);
            var bytes = new byte[size];
            Marshal.Copy(new IntPtr(original), bytes, 0, bytes.Length);
            Marshal.Copy(bytes, 0, allocation, bytes.Length);

            var newNode = (AtkResNode*)allocation;
            newNode->ParentNode = null;
            newNode->ChildNode = null;
            newNode->ChildCount = 0;
            newNode->PrevSiblingNode = null;
            newNode->NextSiblingNode = null;
            return (AtkTextNode*)newNode;
        }
        private static unsafe AtkResNode* CloneNode(AtkResNode* original)
        {
            var size = sizeof(AtkResNode);
            var allocation = MemoryHelper.GameAllocateUi((ulong)size);
            var bytes = new byte[size];
            Marshal.Copy(new IntPtr(original), bytes, 0, bytes.Length);
            Marshal.Copy(bytes, 0, allocation, bytes.Length);

            var newNode = (AtkResNode*)allocation;
            newNode->ParentNode = null;
            newNode->ChildNode = null;
            newNode->ChildCount = 0;
            newNode->PrevSiblingNode = null;
            newNode->NextSiblingNode = null;

            return newNode;
        }
        private static unsafe AtkComponentNode* CloneNode(AtkComponentNode* original)
        {
            var size = sizeof(AtkComponentNode);
            var allocation = MemoryHelper.GameAllocateUi((ulong)size);
            var bytes = new byte[size];
            Marshal.Copy(new IntPtr(original), bytes, 0, bytes.Length);
            Marshal.Copy(bytes, 0, allocation, bytes.Length);

            var newNode = (AtkResNode*)allocation;
            newNode->ParentNode = null;
            newNode->ChildNode = null;
            newNode->ChildCount = 0;
            newNode->PrevSiblingNode = null;
            newNode->NextSiblingNode = null;
            return (AtkComponentNode*)newNode;
        }

        private unsafe TVal Find<TKey, TVal>(StdMap<Pointer<TKey>, TVal>.Node* node, TKey* item) where TKey : unmanaged where TVal : unmanaged
        {
            while (!node->IsNil)
            {
                if (node->KeyValuePair.Item1.Value < item)
                {
                    node = node->Right;
                    continue;
                }

                if (node->KeyValuePair.Item1.Value > item)
                {
                    node = node->Left;
                    continue;
                }

                return node->KeyValuePair.Item2;
            }

            return default;
        }

        public static void Dispose()
        {
            if (updateCharacterPanel)
            {
                if (characterStatusOnSetup != null)
                    characterStatusOnSetup.Dispose();
                if (tooltips != null)
                    tooltips.Dispose();
            }
        }
    }
}
