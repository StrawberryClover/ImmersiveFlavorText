using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Component.GUI;
using RPToolkit.Handlers;
using RPToolkit.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit
{
    // ==========================================
    // Class code has been repurposed from TPie.
    // All credit goes to Tischel.
    // ==========================================
    internal unsafe class ChatHelper : IDisposable
    {
        #region Singleton

        public static ChatHelper Instance { get; private set; } = null!;
        public static void Initialize() {
            Instance = new ChatHelper();
            PluginLog.Information("ChatHelper Initialized");
        }

        ~ChatHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            Instance = null!;
        }
        #endregion

        private IntPtr _chatModulePtr;
        private IntPtr _inputTextActive = IntPtr.Zero;

        private static DalamudLinkPayload chatLinkPayload;
        private ChatHelper()
        {
            _chatModulePtr = Plugin.SigScanner.ScanText("48 89 5C 24 ?? 57 48 83 EC 20 48 8B FA 48 8B D9 45 84 C9");
            _inputTextActive = *(IntPtr*)((IntPtr)AtkStage.GetSingleton() + 0x28) + 0x188E;

            chatLinkPayload = Plugin.Singleton.PluginInterface.AddChatLinkHandler(0, OnChatLinkClick);
        }

        public bool IsInputTextActive()
        {
            return _inputTextActive != IntPtr.Zero && *(bool*)_inputTextActive;
        }
        private void OnChatLinkClick(uint cmdId, SeString msg)
        {
            if (msg.ToString().Contains("suggest some"))
                TempSuggestionWindow.window.IsOpen = true;
        }

        public static void SendSuggestionMessage()
        {
            var chatMsg = new SeString(new TextPayload("This zone doesn't seem to have any temperature data yet! Would you like to help "), new UIForegroundPayload(708), chatLinkPayload, new TextPayload("[suggest some]"),
                        new UIForegroundPayload(0), new TextPayload("?"), RawPayload.LinkTerminator);
            Plugin.chat.PrintChat(new XivChatEntry { Message = chatMsg, Type = Plugin.Configuration.flavorTextChatType, Name = "Temperature" });
        }

        public static void Echo(string message, XivChatType chatType = XivChatType.Echo, string name = "")
        {
            XivChatType[] chatsWithNoName =
            {
                XivChatType.CustomEmote,
                XivChatType.StandardEmote,
                XivChatType.Echo,
                XivChatType.Debug,
                XivChatType.Urgent,
                XivChatType.Notice,
                XivChatType.SystemMessage,
                XivChatType.SystemError,
                XivChatType.GatheringSystemMessage,
                XivChatType.ErrorMessage,
                XivChatType.RetainerSale
            };

            var chatMessage = new XivChatEntry();
            chatMessage.Message = message;
            if (!chatsWithNoName.Contains(chatType))
            {
                chatMessage.Name = name;
            }
            //else if (name != "") chatMessage.Message = $"{name}: {chatMessage.Message}";

            chatMessage.Type = chatType;
            Plugin.chat.PrintChat(chatMessage);
        }

        public static void SendChatMessage(string message)
        {
            if (Instance == null)
            {
                return;
            }

            Instance.SendMessage(message);
        }

        private void SendMessage(string message)
        {
            if (message == null || message.Length == 0)
            {
                return;
            }

            // let dalamud process the command first
            if (CommandHandler.commandManager.ProcessCommand(message))
            {
                return;
            }

            if (_chatModulePtr == IntPtr.Zero)
            {
                return;
            }

            // encode message
            var (text, length) = EncodeMessage(message);
            var payload = MessagePayload(text, length);

            ChatDelegate chatDelegate = Marshal.GetDelegateForFunctionPointer<ChatDelegate>(_chatModulePtr);
            chatDelegate.Invoke(Plugin.GameGui.GetUIModule(), payload, IntPtr.Zero, (byte)0);

            Marshal.FreeHGlobal(payload);
            Marshal.FreeHGlobal(text);
        }

        private static (IntPtr, long) EncodeMessage(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            var mem = Marshal.AllocHGlobal(bytes.Length + 30);
            Marshal.Copy(bytes, 0, mem, bytes.Length);
            Marshal.WriteByte(mem + bytes.Length, 0);
            return (mem, bytes.Length + 1);
        }

        private static IntPtr MessagePayload(IntPtr message, long length)
        {
            var mem = Marshal.AllocHGlobal(400);
            Marshal.WriteInt64(mem, message.ToInt64());
            Marshal.WriteInt64(mem + 0x8, 64);
            Marshal.WriteInt64(mem + 0x10, length);
            Marshal.WriteInt64(mem + 0x18, 0);
            return mem;
        }
    }

    public delegate IntPtr UiModuleDelegate(IntPtr baseUiPtr);
    public delegate IntPtr ChatDelegate(IntPtr uiModulePtr, IntPtr message, IntPtr unknown1, byte unknown2);
}
