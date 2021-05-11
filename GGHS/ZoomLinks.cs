using RollingRess.GGHS.Grade2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Enables using record types as tuple-like types.
namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class IsExternalInit { }
}

namespace GGHS
{
    public static class ZoomLinks
    {
        public record ZoomInfo(string Link, string Id, string Pw);
        private static class Common
        {
            // Literature
            // public static readonly ZoomInfo Literature1to3 = new("");
            public static readonly ZoomInfo Literature4to6 = new("https://zoom.us/j/85644781112?pwd=SC8xWDBZbVRpNW1TSXF6QjRNQk8zQT09", "856 4478 1112", "0204");
            public static readonly ZoomInfo Literature7to8 = new("https://zoom.us/j/97317948690?pwd=WWovQUdBQS8rVVMzMUNyZmNPSDY3Zz09", "973 1794 8690", "626239");

            // Math
            public static readonly ZoomInfo Math4to6 = new("https://zoom.us/j/7014267742?pwd=eFAvUWQweVdSbmc4Q2JLNGlQWTdsZz09", "701 426 7742", "1111");

            //English
            public static readonly ZoomInfo CriticalEnglishA1to4 = new("https://zoom.us/j/5472878985?pwd=RlIzSmJnWHBRaWhkRTNkOEJ6UUF5UT09", "547 287 8985", "0512");
            public static readonly ZoomInfo CriticalEnglishA5to8 = new("https://zoom.us/j/5031101343?pwd=QTRmbnRLSHFPamh4U3d2ZS9JTXdrUT09", "503 110 1343", "1111");
            // [Changes] public static readonly ZoomInfo CriticalEnglishB = new("https://zoom.us/j/98351310802?pwd=ODlENFVJeGlqWG0wYmgyR2RNUURHUT09", "983 5131 0802", "8at5CP");
            public static readonly ZoomInfo CriticalEnglishC = new("https://zoom.us/j/5365083101?pwd=VDV4VHA5MVUrcDV4cDV1RitZeHovZz09", "536 508 3101", "2021");
            public static readonly ZoomInfo CriticalEnglishD = new("https://zoom.us/j/7936438089?pwd=UkZNaWhFUTE5R2xIYkRxWTRSTC90QT09", "793 643 8089", "1234");

            // Social2
            public static readonly ZoomInfo Politics = new("https://zoom.us/j/4613355190?pwd=WEZzaFhtTVpDZVk4L09XK1VlQ3Z5UT09", "461 335 5190", "1111");
            public static readonly ZoomInfo Economy = new("https://zoom.us/j/2521095403?pwd=MVBmOURvRGU1azRwY0lnejVwa2tjUT09", "252 109 5403", "2021");
            // public static readonly ZoomInfo History = new("");
            // public static readonly ZoomInfo Geography = new("");


            // Science
            // public static readonly ZoomInfo Physics = new("");
            public static readonly ZoomInfo Chemistry = new("https://zoom.us/j/93595351190?pwd=eHVIMXVGSnFTaGhYWVprNm4xTEh0Zz09", "935 9535 1190", "2021gghs");
            public static readonly ZoomInfo Biology = new("https://zoom.us/j/4153909733?pwd=M2h3eVp5WnNPUTdJdlBwMkd1VGs1dz09", "415 390 9733", "1111");

            // Social1 [Completed]
            public static readonly ZoomInfo Ethics = new("https://zoom.us/j/9401959597?pwd=TE5BSW5jSUFpaE1xKytzZ2I4Q2FWUT09", "940 195 9597", "255226");
            public static readonly ZoomInfo Environment = new("https://zoom.us/j/94849418747", "948 4941 8747", "geogeo");

            // Language
            // public static readonly ZoomInfo Spanish = new("");
            public static readonly ZoomInfo Chinese = new("https://zoom.us/j/99535123743?pwd=d0dPemVjNXIxcks5RCt0OFc1aGg0Zz09", "995 3512 3743", "1eMXJM");
            // public static readonly ZoomInfo Japanese = new("");

            // Others
            public static readonly ZoomInfo CreativeSolve = new("https://meet.google.com/lookup/bgt6c65ccm", "None", "None");
            public static readonly ZoomInfo Sport1to4 = new("https://zoom.us/j/3373011774?pwd=QWYybW1SMlljVlVFRzRTY0RvenVrUT09", "337 301 1774", "123456");
        }

        public static readonly Dictionary<string, ZoomInfo> Class4 = new()
        {
            [Subjects.CellName.Literature] = Common.Literature4to6,
            [Subjects.CellName.Mathematics] = Common.Math4to6,
            [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA1to4,
            [Subjects.CellName.CriticalEnglish + "C"] = Common.CriticalEnglishC,
            [Subjects.CellName.CriticalEnglish + "D"] = Common.CriticalEnglishD,

            [Subjects.CellName.Politics] = Common.Politics,
            [Subjects.CellName.Economy] = Common.Economy,

            [Subjects.CellName.Biology] = Common.Biology,
            [Subjects.CellName.Ethics] = Common.Ethics,
            [Subjects.CellName.Chinese] = Common.Chinese,
            [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            [Subjects.CellName.Sport] = Common.Sport1to4
        };

        public static readonly Dictionary<string, ZoomInfo> Class5 = new()
        {
            [Subjects.CellName.Literature] = Common.Literature4to6,
            [Subjects.CellName.Mathematics] = Common.Math4to6,

            [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA5to8,
            [Subjects.CellName.CriticalEnglish + "C"] = Common.CriticalEnglishC,
            [Subjects.CellName.CriticalEnglish + "D"] = Common.CriticalEnglishD,

            [Subjects.CellName.Politics] = Common.Politics,
            [Subjects.CellName.Economy] = Common.Economy,

            [Subjects.CellName.Biology] = Common.Biology,
            // TODO: 화학 물리
            [Subjects.CellName.Ethics] = Common.Ethics,
            [Subjects.CellName.Environment] = Common.Environment,
            [Subjects.CellName.Chinese] = Common.Chinese,
            // Spanish
            [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
        };

        public static readonly Dictionary<string, ZoomInfo> Class8 = new()
        {
            [Subjects.CellName.Literature] = Common.Literature7to8,

            [Subjects.CellName.Chemistry] = Common.Chemistry,
            [Subjects.CellName.Economy] = Common.Economy,
            [Subjects.CellName.Environment] = Common.Environment,

            [Subjects.Languages.Chinese] = Common.Chinese,
            [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA5to8,
            [Subjects.CellName.CriticalEnglish + "C"] = Common.CriticalEnglishC,
        };
    }
}
