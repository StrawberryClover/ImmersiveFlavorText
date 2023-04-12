using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit.Localizations
{
    public abstract class LocalizationBase
    {
        public abstract TemperatureStages temperatureStages { get; }
        public abstract rstring rainStarts { get; }
        public abstract rstring rainEnds { get; }
    }
    public struct TemperatureStages
    {
        public TemperatureDescription heatwave, veryHot, hot, roomTemp, mild, lukewarm, chilled, cold, veryCold, frigid;

        public TemperatureStages (
            TemperatureDescription heatwave,
            TemperatureDescription veryHot,
            TemperatureDescription hot,
            TemperatureDescription roomTemp,
            TemperatureDescription mild,
            TemperatureDescription lukewarm,
            TemperatureDescription chilled,
            TemperatureDescription cold,
            TemperatureDescription veryCold,
            TemperatureDescription frigid)
        {
            this.heatwave = heatwave;
            this.veryHot = veryHot;
            this.hot = hot;
            this.roomTemp = roomTemp;
            this.mild = mild;
            this.lukewarm = lukewarm;
            this.chilled = chilled;
            this.cold = cold;
            this.veryCold = veryCold;
            this.frigid = frigid;
        }
    }
    public struct TemperatureDescription
    {
        public rstring increaseDesc;
        public rstring decreaseDesc;

        public TemperatureDescription(rstring increaseDesc, rstring decreaseDesc)
        {
            this.increaseDesc = increaseDesc;
            this.decreaseDesc = decreaseDesc;
        }
    }

    ///<summary>Represents an array of strings, returning a random string as a value.</summary>
    public class rstring
    {
        public string? value { get { return GetRandomValue(values); } }
        public string[] values;

        public rstring(string[] values)
        {
            this.values = values;
        }

        private string? GetRandomValue(string[] values)
        {
            if (values.Length > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, values.Length);
                return values[index];
            }
            else return null;
        }

        public static implicit operator rstring(string[] values)
        {
            if (values == null) return null;
            else return new rstring(values);
        }

        public static implicit operator rstring(string value)
        {
            if (value == null) return null;
            else return new rstring(new string[] { value });
        }

        public static implicit operator string(rstring rs)
        {
            return rs.value;
        }
        public override string ToString() => value;
    }
}
