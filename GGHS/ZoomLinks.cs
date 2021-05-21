using GGHS.Grade2;
using System.Collections.Generic;
using System.ComponentModel;

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
         // TODO: 선생님 성함도?
        public record ZoomInfo(string Link, string Id, string Pw, string Teacher);
        private static class Common
        {
            // Literature
            public static readonly ZoomInfo Literature1to3 = new("https://zoom.us/j/92759524061?pwd=dHUwZllmSEp2NWpRYlVTbmdjODR3dz09", "927 5952 4061", "0203", "백지혜");
            public static readonly ZoomInfo Literature4to6 = new("https://zoom.us/j/85644781112?pwd=SC8xWDBZbVRpNW1TSXF6QjRNQk8zQT09", "856 4478 1112", "0204", "윤채영");
            public static readonly ZoomInfo Literature7to8 = new("https://zoom.us/j/97317948690?pwd=WWovQUdBQS8rVVMzMUNyZmNPSDY3Zz09", "973 1794 8690", "626239", "임제완");

            // Math
            public static readonly ZoomInfo Math4to6 = new("https://zoom.us/j/7014267742?pwd=eFAvUWQweVdSbmc4Q2JLNGlQWTdsZz09", "701 426 7742", "1111", "박지영");

            //English
            public static readonly ZoomInfo CriticalEnglishA1to4 = new("https://zoom.us/j/5472878985?pwd=RlIzSmJnWHBRaWhkRTNkOEJ6UUF5UT09", "547 287 8985", "0512", "김찬미");
            public static readonly ZoomInfo CriticalEnglishA5to8 = new("https://zoom.us/j/5031101343?pwd=QTRmbnRLSHFPamh4U3d2ZS9JTXdrUT09", "503 110 1343", "1111", "김한나");
            // [Changes] public static readonly ZoomInfo CriticalEnglishB = new("https://zoom.us/j/98351310802?pwd=ODlENFVJeGlqWG0wYmgyR2RNUURHUT09", "983 5131 0802", "8at5CP");
            public static readonly ZoomInfo CriticalEnglishC = new("https://zoom.us/j/5365083101?pwd=VDV4VHA5MVUrcDV4cDV1RitZeHovZz09", "536 508 3101", "2021", "허진");
            public static readonly ZoomInfo CriticalEnglishD = new("https://zoom.us/j/7936438089?pwd=UkZNaWhFUTE5R2xIYkRxWTRSTC90QT09", "793 643 8089", "1234", "장종윤");

            // Socials
            public static readonly ZoomInfo Politics = new("https://zoom.us/j/4613355190?pwd=WEZzaFhtTVpDZVk4L09XK1VlQ3Z5UT09", "461 335 5190", "1111", "정슬기");
            public static readonly ZoomInfo Economy = new("https://zoom.us/j/2521095403?pwd=MVBmOURvRGU1azRwY0lnejVwa2tjUT09", "252 109 5403", "2021", "김용지");
            public static readonly ZoomInfo History = new("https://zoom.us/j/91236721004?pwd=MkhMN3FLdXZsbW1LbExqWWNkaGhHdz09", "912 3672 1004", "duhanworld", "이두한");
            // public static readonly ZoomInfo Geography = new("");

            // Science
            // public static readonly ZoomInfo Physics = new("");
            public static readonly ZoomInfo Chemistry = new("https://zoom.us/j/93595351190?pwd=eHVIMXVGSnFTaGhYWVprNm4xTEh0Zz09", "935 9535 1190", "2021gghs", "이지은");
            public static readonly ZoomInfo Biology = new("https://zoom.us/j/4153909733?pwd=M2h3eVp5WnNPUTdJdlBwMkd1VGs1dz09", "415 390 9733", "1111", "이지원");

            // Specials [Completed]
            public static readonly ZoomInfo Ethics = new("https://zoom.us/j/9401959597?pwd=TE5BSW5jSUFpaE1xKytzZ2I4Q2FWUT09", "940 195 9597", "255226", "류제광");
            public static readonly ZoomInfo Environment = new("https://zoom.us/j/94849418747", "948 4941 8747", "geogeo", "조한솔");

            // Language
            // public static readonly ZoomInfo Spanish = new("");
            public static readonly ZoomInfo Chinese = new("https://zoom.us/j/99535123743?pwd=d0dPemVjNXIxcks5RCt0OFc1aGg0Zz09", "995 3512 3743", "1eMXJM", "김나연");
            // public static readonly ZoomInfo Japanese = new("");

            // Others
            public static readonly ZoomInfo CreativeSolve = new("https://meet.google.com/lookup/bgt6c65ccm", "None", "None", "조광진");
            public static readonly ZoomInfo Sport1to4 = new("https://zoom.us/j/3373011774?pwd=QWYybW1SMlljVlVFRzRTY0RvenVrUT09", "337 301 1774", "123456", "허진용");
            public static readonly ZoomInfo Sport5to8 = new("https://zoom.us/j/3225620828?pwd=NHBqdnZrZnZCMTNpRmlCakhVUVJPZz09", "322 562 0828", "1234", "윤보경");
            //
        }

        public static readonly Dictionary<string, ZoomInfo> Class3 = new()
        {
            [Subjects.CellName.Literature] = Common.Literature1to3,
            // [Subjects.CellName.Mathematics] = Common.Math1to3,

            // English
            [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA1to4,
            [Subjects.CellName.CriticalEnglish + "B"] = null, // RANDOM
            [Subjects.CellName.CriticalEnglish + "C"] = Common.CriticalEnglishC,
            [Subjects.CellName.CriticalEnglish + "D"] = Common.CriticalEnglishD,

            // Sciences
            [Subjects.CellName.Physics] = null,
            [Subjects.CellName.Chemistry] = Common.Chemistry,
            [Subjects.CellName.Biology] = Common.Biology,

            // Special: Environment = None
            [Subjects.CellName.Ethics] = Common.Ethics,

            // Social
            [Subjects.CellName.History] = Common.History,
            [Subjects.CellName.Geography] = null,
            [Subjects.CellName.Politics] = Common.Politics,
            [Subjects.CellName.Economy] = Common.Economy,

            // Language: Japanese = None
            [Subjects.CellName.Chinese] = Common.Chinese,
            [Subjects.CellName.Spanish] = null,

            [Subjects.CellName.Sport] = Common.Sport1to4,
            [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            [Subjects.CellName.MathResearch] = null, // RANDOM
        };

        // FINISHED
        public static readonly Dictionary<string, ZoomInfo> Class4 = new()
        {
            [Subjects.CellName.Literature] = Common.Literature4to6,
            [Subjects.CellName.Mathematics] = Common.Math4to6,

            // English
            [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA1to4,
            [Subjects.CellName.CriticalEnglish + "B"] = null, // RANDOM
            [Subjects.CellName.CriticalEnglish + "C"] = Common.CriticalEnglishC,
            [Subjects.CellName.CriticalEnglish + "D"] = Common.CriticalEnglishD,

            // Sciences: Physics, Chemistry = None
            [Subjects.CellName.Biology] = Common.Biology,

            // Special: Environment = None
            [Subjects.CellName.Ethics] = Common.Ethics,

            // Social
            [Subjects.CellName.History] = Common.History,
            [Subjects.CellName.Geography] = null,
            [Subjects.CellName.Politics] = Common.Politics,
            [Subjects.CellName.Economy] = Common.Economy,

            // Language: Japanese, Spanish = None
            [Subjects.CellName.Chinese] = Common.Chinese,

            [Subjects.CellName.Sport] = Common.Sport1to4,
            [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            [Subjects.CellName.MathResearch] = null, // RANDOM
        };

        public static readonly Dictionary<string, ZoomInfo> Class5 = new()
        {
            [Subjects.CellName.Literature] = Common.Literature4to6,
            [Subjects.CellName.Mathematics] = Common.Math4to6,

            // English
            [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA5to8,
            [Subjects.CellName.CriticalEnglish + "B"] = null, // RANDOM
            [Subjects.CellName.CriticalEnglish + "C"] = Common.CriticalEnglishC,
            [Subjects.CellName.CriticalEnglish + "D"] = Common.CriticalEnglishD,

            // Sciences
            [Subjects.CellName.Physics] = null,
            [Subjects.CellName.Chemistry] = Common.Chemistry,
            [Subjects.CellName.Biology] = Common.Biology,

            // Special
            [Subjects.CellName.Ethics] = Common.Ethics,
            [Subjects.CellName.Environment] = Common.Environment,

            // Social
            [Subjects.CellName.History] = Common.History,
            [Subjects.CellName.Geography] = null,
            [Subjects.CellName.Politics] = Common.Politics,
            [Subjects.CellName.Economy] = Common.Economy,

            // Language: Chinese, Japanese = None
            [Subjects.CellName.Spanish] = null,

            [Subjects.CellName.Sport] = Common.Sport5to8,
            [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            [Subjects.CellName.MathResearch] = null, // RANDOM
        };

        public static readonly Dictionary<string, ZoomInfo> Class6 = new()
        {
            [Subjects.CellName.Literature] = Common.Literature4to6,
            [Subjects.CellName.Mathematics] = Common.Math4to6,

            // English
            [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA5to8,
            [Subjects.CellName.CriticalEnglish + "B"] = null, // RANDOM
            [Subjects.CellName.CriticalEnglish + "C"] = Common.CriticalEnglishC,
            [Subjects.CellName.CriticalEnglish + "D"] = Common.CriticalEnglishD,

            // Sciences: ONLY BIO
            [Subjects.CellName.Biology] = Common.Biology,

            // Special: Ethics = None
            [Subjects.CellName.Environment] = Common.Environment,

            // Social
            [Subjects.CellName.History] = Common.History,
            [Subjects.CellName.Geography] = null,
            [Subjects.CellName.Politics] = Common.Politics,
            [Subjects.CellName.Economy] = Common.Economy,

            // Language: Ch, Japanese = None
            [Subjects.CellName.Spanish] = null,

            [Subjects.CellName.Sport] = Common.Sport5to8,
            [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            [Subjects.CellName.MathResearch] = null, // RANDOM
        };

        public static readonly Dictionary<string, ZoomInfo> Class8 = new()
        {
            [Subjects.CellName.Literature] = Common.Literature7to8,
            [Subjects.CellName.Mathematics] = null,

            // English
            [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA5to8,
            [Subjects.CellName.CriticalEnglish + "B"] = null, // RANDOM
            [Subjects.CellName.CriticalEnglish + "C"] = Common.CriticalEnglishC,
            [Subjects.CellName.CriticalEnglish + "D"] = Common.CriticalEnglishD,

            // Sciences
            [Subjects.CellName.Physics] = null,
            [Subjects.CellName.Chemistry] = Common.Chemistry,
            [Subjects.CellName.Biology] = Common.Biology,

            // Special
            // Ethics: None
            [Subjects.CellName.Environment] = Common.Environment,

            // Social
            [Subjects.CellName.History] = Common.History,
            [Subjects.CellName.Geography] = null,
            [Subjects.CellName.Politics] = Common.Politics,
            [Subjects.CellName.Economy] = Common.Economy,

            // Language
            // Japanese: None
            [Subjects.CellName.Chinese] = Common.Chinese,
            [Subjects.CellName.Spanish] = null,

            [Subjects.CellName.Sport] = Common.Sport5to8,
            [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            [Subjects.CellName.MathResearch] = null, // RANDOM
        };
    }
}
