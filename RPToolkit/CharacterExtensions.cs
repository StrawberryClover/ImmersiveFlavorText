using Dalamud.Game.ClientState.Objects.Types;
using StructsCharacter = FFXIVClientStructs.FFXIV.Client.Game.Character.Character;

namespace RPToolkit.Extensions;

public static class CharacterExtensions
{
    public unsafe static StructsCharacter* AsNative(this Character chara) => (StructsCharacter*)chara.Address;
}
