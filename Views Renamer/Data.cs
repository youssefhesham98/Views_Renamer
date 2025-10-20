using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Architecture;

namespace Views_Renamer
{
    
    public class Data
    {
        public static Dictionary<string,View> elevations { get; set; }
        public static Dictionary<string, View> sections { get; set; }
        public static Dictionary<string, View> threed { get; set; }
        public static Dictionary<string, List<View>> elevdic { get; set; }
        public static Dictionary<string, List<View>> secdic { get; set; }
        public static Dictionary<string, List<View>> threeddic { get; set; }
        public static readonly List<string> ViewCategories = new List<string>
        {
            "00_Work In Progress",
            "01_Blockwork Plans",
            "02_Flooring",
            "03_Ceiling",
            "04_Furniture",
            "05_Elevations",
            "06_Interior Design",
            "07_Sttairs Details",
            "08_Doors and Windows",
            "09_General Details",
            "10_Landscapr",
            "11_Life Safety"
        };
        public static readonly List<string> STViewCategories = new List<string>
        {
            "00_Work In Progress",
            "01_General",
            "02_Column & Axis",
            "03_Foundation",
            "04_Slabs",
            "05_Parapet",
            "06_Steel Structure",
            "07_Piles",
            "08_Concrete Dimension",
            "09_Reinforcement",
            "10_Analytical",
        };
        public static readonly List<string> Levels = new List<string>
        {
            "RD",
            "PV",
            "FO",
            "PC",
            "RC",
            "BS",
            "GR",
            "L1",
            "L4",
            "L3",
            "L4",
            "MZ",
            "RF",
            "UR",
            "PR"
        };
        public static readonly List<string> Categories = new List<string>
        {
            "WIP",
            "BW",
            "FL",
            "RC",
            "FN",
            "ED",
            "ID",
            "SD",
            "DW",
            "GD",
            "LS",
            "LF"
        };
        public static readonly Dictionary<string, string> prefixMap = new Dictionary<string, string>
        {
            { "00_Work In Progress", "WIP" },
            { "01_Blockwork Plans", "BW" },
            { "02_Flooring", "FL" },
            { "03_Ceiling" , "RC" },
            { "04_Furniture", "FN" },
            { "05_Elevation" , "ED" },
            { "06_Interior Design", "ID" },
            { "07_Sttairs Details", "SD" },
            { "08_Doors and Windows", "DW" },
            { "09_General Details", "GD" },
            { "10_Landscapr", "LS" },
            { "11_Life Safety", "LF" }
        };
        public static readonly Dictionary<string, string> STprefixMap = new Dictionary<string, string>
        {
            { "00_Work In Progress", "WIP" },
            { "01_General", "XX" },
            { "02_Column & Axis", "CA" },
            { "03_Foundation", "FO" },
            { "04_Slabs", "SB" },
            { "05_Parapet", "PR" },
            { "06_Steel Structure", "SS" },
            { "07_Piles", "PI" },
            { "08_Concrete Dimension", "CO" },
            { "09_Reinforcement", "RF" },
            { "10_Analytical", "AN" }
        };

        public static void Intialize()
        {
            elevations = new Dictionary<string, View>();
            sections = new Dictionary<string, View>();
            threed = new Dictionary<string, View>();
            elevdic = new Dictionary<string, List<View>>();
            secdic = new Dictionary<string, List<View>>();
            threeddic = new Dictionary<string, List<View>>();
        }
    }
}
