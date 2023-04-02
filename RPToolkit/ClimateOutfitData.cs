using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPToolkit
{
    [Serializable]
    public class ClimateOutfitData
    {
        public uint jobID;
        public int climateConditions;
        public string customizationString;

        public ClimateOutfitData(uint jobID = 0, int climateConditions = 0, string customizationString = "")
        {
            this.jobID = jobID;
            this.climateConditions = climateConditions;
            this.customizationString = customizationString;
        }

        public enum ClimateConditions : int
        {
            Only_When_Raining = 1,
            Heatwave = 1 << 1,
            Very_Hot = 1 << 2,
            Hot = 1 << 3,
            Room_Temperature = 1 << 4,
            Mild = 1 << 5,
            Lukewarm = 1 << 6,
            Chilled = 1 << 7,
            Cold = 1 << 8,
            Very_Cold = 1 << 9,
            Frigid = 1 << 10,
        }

        public enum Jobs : uint
        {
            GLA = 1,
            PGL = 2,
            MRD = 3,
            LNC = 4,
            ARC = 5,
            CNJ = 6,
            THM = 7,
            PLD = 19,
            MNK = 20,
            WAR = 21,
            DRG = 22,
            BRD = 23,
            WHM = 24,
            BLM = 25,
            ACN = 26,
            SMN = 27,
            SCH = 28,
            ROG = 29,
            NIN = 30,
            MCH = 31,
            DRK = 32,
            AST = 33,
            SAM = 34,
            RDM = 35,
            BLU = 36,
            GNB = 37,
            DNC = 38,
            RPR = 39,
            SGE = 40,

            CRP = 8,
            BSM = 9,
            ARM = 10,
            GSM = 11,
            LTW = 12,
            WVR = 13,
            ALC = 14,
            CUL = 15,
            MIN = 16,
            BTN = 17,
            FSH = 18,
        }
    }
}
