using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Dalamud;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using RPToolkit;
using static CharacterPanelRefined.Tooltips;

namespace CharacterPanelRefined;

public class Tooltips : IDisposable {
    private const ushort TitleColor = 8;
    private const ushort HighlightColor = 33;
    private const ushort GreenColor = 43;
    private const ushort OrangeColor = 31;
    private const ushort RedColor = 0x1f4;

    private readonly Dictionary<Entry, IntPtr> allocations = new();

    private readonly Dictionary<Entry, SeString> tooltips = new();

    private readonly Dictionary<(Entry, string), int> keywordIndices = new();

    public enum Entry {
        Crit,
        DirectHit,
        Determination,
        Speed,
        ExpectedDamage,
        ExpectedHeal,
        Tenacity,
        Piety,
        Defense,
        MagicDefense,
        Vitality,
        MainStat,
        Temperature,
        Hunger,
        Thirst
    }

    public Tooltips() {
        foreach (var entry in Enum.GetValues<Entry>()) {
            allocations.Add(entry, Marshal.AllocHGlobal(4096));
        }
        var str = new SeString();
        tooltips[Entry.Hunger] = str;
        var sb = new StringBuilder();
        sb.Append("T e S t");
        str.Append(sb.ToString());
    }

    private void LoadLocString(Entry entry, params string[] localization)
    {
        var str = new SeString();
        tooltips[entry] = str;
        var sb = new StringBuilder();
        var keywords = new HashSet<string>();

        void AddKeyword(string keyword)
        {
            while (!keywords.Add(keyword))
                keyword += '_';
            keywordIndices[(entry, keyword)] = str.Payloads.Count;
        }

        foreach (var s in localization)
        {
            int start;
            var end = -1;
            while ((start = s.IndexOf('{', end + 1)) >= 0)
            {
                if (start - end > 1)
                {
                    sb.Append(s, end + 1, start - end - 1);
                    str.Append(sb.ToString());
                    sb.Clear();
                }

                end = s.IndexOf('}', start);
                var len = end - start - 1;
                var keyword = s.Substring(start + 1, len);
                if (keyword[0] == '@')
                {
                    ushort col = 0;
                    switch (keyword)
                    {
                        case "@Title":
                            col = TitleColor;
                            break;
                        case "@Highlight":
                            col = HighlightColor;
                            break;
                        case "@Red":
                            col = RedColor;
                            break;
                        case "@Wasting":
                            AddKeyword(keyword);
                            break;
                    }

                    str.Append(new UIForegroundPayload(col));
                }
                else
                {
                    AddKeyword(keyword);
                    str.Append(new TextPayload(""));
                }
            }

            if (end < s.Length - 1)
                sb.Append(s, end + 1, s.Length - end - 1);
        }

        if (sb.Length > 0)
            str.Append(sb.ToString());
    }

    public void ReloadTemperatureTooltip(int zoneTemperature, int weatherTemperature)
    {
        LoadLocString(Entry.Temperature, $"Temperature from zone: {zoneTemperature}\r\nTemperature affected by weather: {weatherTemperature}");
        WriteString(Entry.Temperature);
    }

    public IntPtr this[Entry entry] => allocations[entry];

    public void UpdateExpectedOutput(Entry entry, double normalOutput, double critOutput)
    {
        var tooltip = tooltips[entry];
        ((TextPayload)tooltip.Payloads[keywordIndices[(entry, "NormalValue")]]).Text = normalOutput.ToString("N0");
        ((TextPayload)tooltip.Payloads[keywordIndices[(entry, "CritValue")]]).Text = critOutput.ToString("N0");
        WriteString(entry);
    }

    private void WriteString(Entry entry)
    {
        var target = allocations[entry];
        var encoded = tooltips[entry].Encode();

        Marshal.Copy(encoded, 0, target, encoded.Length);
        Marshal.WriteByte(target, encoded.Length, 0);
    }

    private void ReleaseUnmanagedResources()
    {
        foreach (var (_, alloc) in allocations)
        {
            Marshal.FreeHGlobal(alloc);
        }

        allocations.Clear();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Tooltips() {
        ReleaseUnmanagedResources();
    }
}
