#nullable enable

using GGHS.Grade2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using ZoomDictionary = System.Collections.Generic.Dictionary<string, GGHS.ZoomInfo?>;

// Enables using record types as tuple-like types.
namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class IsExternalInit { }
}

namespace GGHS
{
    public record ZoomInfo(string? Link, string? Id, string? Pw, string? ClassRoom, string Teacher);

    public interface IZoomLinks
    {
        ZoomDictionary Class1 { get; }
        ZoomDictionary Class2 { get; }
        ZoomDictionary Class3 { get; }
        ZoomDictionary Class4 { get; }
        ZoomDictionary Class5 { get; }
        ZoomDictionary Class6 { get; }
        ZoomDictionary Class7 { get; }
        ZoomDictionary Class8 { get; }
    }

    namespace Grade2.Semester2
    {
        public record ZoomLinks : IZoomLinks
        {
            private record Common
            {
                // Reading
                public static ZoomInfo Reading1to3 { get; } = new("https://zoom.us/j/92759524061?pwd=dHUwZllmSEp2NWpRYlVTbmdjODR3dz09", "927 5952 4061", "0203", "https://classroom.google.com/c/MjgyNTY3ODQ3MTQ3", "백지혜");
                public static ZoomInfo Reading4to6 { get; } = new("https://zoom.us/j/85644781112?pwd=SC8xWDBZbVRpNW1TSXF6QjRNQk8zQT09", "856 4478 1112", "0204", "https://classroom.google.com/c/MjgyNTY3ODQ3MTQ3", "윤채영");
                public static ZoomInfo Reading7to8 { get; } = new("https://zoom.us/j/97317948690?pwd=WWovQUdBQS8rVVMzMUNyZmNPSDY3Zz09", "973 1794 8690", "626239", "https://classroom.google.com/c/MjgyNTY3ODQ3MTQ3", "임제완");

                // Language
                // public static ZoomInfo Spanish { get; } = new("https://us02web.zoom.us/j/7411091130?pwd=M0tZTXJwaFRXb3RmZm1jODNhdUtqUT09");
                public static ZoomInfo Chinese { get; } = new("https://zoom.us/j/99535123743?pwd=d0dPemVjNXIxcks5RCt0OFc1aGg0Zz09", "995 3512 3743", "1eMXJM", null, "김나연");
                // public static ZoomInfo Japanese { get; } = new("");

                // Others
                public static ZoomInfo CreativeSolve { get; } = new("https://meet.google.com/lookup/bgt6c65ccm", null, null, "https://classroom.google.com/u/0/c/Mjc3MDAwNjYxOTcx", "조광진");
                public static ZoomInfo Sport1to4 { get; } = new("https://zoom.us/j/3373011774?pwd=QWYybW1SMlljVlVFRzRTY0RvenVrUT09", "337 301 1774", "123456", null, "허진용");
                public static ZoomInfo Sport5to8 { get; } = new("https://zoom.us/j/3225620828?pwd=NHBqdnZrZnZCMTNpRmlCakhVUVJPZz09", "322 562 0828", "1234", null, "윤보경");
            }

            public ZoomDictionary Class1 { get; } = new()
            {
                [Subjects.CellName.Reading] = Common.Reading1to3,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            };

            public ZoomDictionary Class2 { get; } = new()
            {
                [Subjects.CellName.Reading] = Common.Reading1to3,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            };

            public ZoomDictionary Class3 { get; } = new()
            {
                [Subjects.CellName.Reading] = Common.Reading1to3,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            };

            public ZoomDictionary Class4 { get; } = new()
            {
                [Subjects.CellName.Reading] = Common.Reading4to6,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            };

            public ZoomDictionary Class5 { get; } = new()
            {
                [Subjects.CellName.Reading] = Common.Reading4to6,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            };

            public ZoomDictionary Class6 { get; } = new()
            {
                [Subjects.CellName.Reading] = Common.Reading4to6,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            };

            public ZoomDictionary Class7 { get; } = new()
            {
                [Subjects.CellName.Reading] = Common.Reading7to8,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            };

            public ZoomDictionary Class8 { get; } = new()
            {
                [Subjects.CellName.Reading] = Common.Reading7to8,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
            };

        }
        /*
        public static string Reading { get; } = "독서";
        public static string Mathematics { get; } = "수학Ⅱ";
        public static string AdvancedEnglish { get; } = "심화영어Ⅰ";
        public static string Sport { get; } = "운동과 건강";
        public static string CreativeSolve { get; } = "창의적 문제 해결 기법";
        public static string MathResearch { get; } = "수학과제탐구";
        public static string Others { get; } = "창의적 체험활동";
        public static string HomeComing { get; } = "홈커밍";

        public static string ScienceHistory { get; } = "과학사";
        public static string LifeAndScience { get; } = "생활과 과학";

        public static string GlobalEconomics { get; } = "국제경제";
        public static string GlobalPolitics { get; } = "국제정치";
        public static string CompareCulture { get; } = "비교문화";
        public static string EasternHistory { get; } = "동양근대사";
        public static string HistoryAndCulture { get; } = "세계 역사와 문화";
        public static string PoliticsPhilosophy { get; } = "현대정치철학의 이해";
        public static string RegionResearch { get; } = "세계 지역 연구";
        public static string GISAnalyze { get; } = "공간 정보와 공간 분석";

        public static string Japanese { get; } = "일본어Ⅰ";
        public static string Spanish { get; } = "스페인어Ⅰ";
        public static string Chinese { get; } = "중국어Ⅰ";
        */
    }
    namespace Grade2.Semester1
    {
        public class ZoomLinks : IZoomLinks
        {
            // TODO: 선생님 성함도?
            private record Common
            {
                // Literature
                public static ZoomInfo Literature1to3 { get; } = new("https://zoom.us/j/92759524061?pwd=dHUwZllmSEp2NWpRYlVTbmdjODR3dz09", "927 5952 4061", "0203", "https://classroom.google.com/c/MjgyNTY3ODQ3MTQ3", "백지혜");
                public static ZoomInfo Literature4to6 { get; } = new("https://zoom.us/j/85644781112?pwd=SC8xWDBZbVRpNW1TSXF6QjRNQk8zQT09", "856 4478 1112", "0204", "https://classroom.google.com/c/MjgyNTY3ODQ3MTQ3", "윤채영");
                public static ZoomInfo Literature7to8 { get; } = new("https://zoom.us/j/97317948690?pwd=WWovQUdBQS8rVVMzMUNyZmNPSDY3Zz09", "973 1794 8690", "626239", "https://classroom.google.com/c/MjgyNTY3ODQ3MTQ3", "임제완");

                // Math
                public static ZoomInfo Math1to3 { get; } = new(null, null, null, "https://classroom.google.com/c/MjgzNDM1NjIyODM0", "이혜란");
                public static ZoomInfo Math4to6 { get; } = new("https://zoom.us/j/7014267742?pwd=eFAvUWQweVdSbmc4Q2JLNGlQWTdsZz09", "701 426 7742", "1111", "https://classroom.google.com/c/MjgzNDM1NjIyODM0", "박지영");
                public static ZoomInfo Math7to8 { get; } = new(null, null, null, "https://classroom.google.com/c/MjgzNDM1NjIyODM0", "공현진");

                //English
                public static ZoomInfo CriticalEnglishA1to4 { get; } = new("https://zoom.us/j/5472878985?pwd=RlIzSmJnWHBRaWhkRTNkOEJ6UUF5UT09", "547 287 8985", "0512", "https://classroom.google.com/c/Mjc3NTczMTU4MzE3", "김찬미");
                public static ZoomInfo CriticalEnglishA5to8 { get; } = new("https://zoom.us/j/5031101343?pwd=QTRmbnRLSHFPamh4U3d2ZS9JTXdrUT09", "503 110 1343", "1111", "https://classroom.google.com/c/Mjc3NTczMTU4MzE3", "김한나");
                public static ZoomInfo CriticalEnglishB { get; } = new(null, null, null, "https://classroom.google.com/c/Mjc3NzExMjYyMTg2", "Lubi");
                public static ZoomInfo CriticalEnglishC { get; } = new("https://zoom.us/j/5365083101?pwd=VDV4VHA5MVUrcDV4cDV1RitZeHovZz09", "536 508 3101", "2021", "https://classroom.google.com/c/Mjc3NTczMTU4MzE3", "허진");
                public static ZoomInfo CriticalEnglishD { get; } = new("https://zoom.us/j/7936438089?pwd=UkZNaWhFUTE5R2xIYkRxWTRSTC90QT09", "793 643 8089", "1234", "https://classroom.google.com/c/Mjc3NTczMTU4MzE3", "장종윤");

                // Socials
                public static ZoomInfo Politics { get; } = new("https://zoom.us/j/4613355190?pwd=WEZzaFhtTVpDZVk4L09XK1VlQ3Z5UT09", "461 335 5190", "1111", null, "정슬기");
                public static ZoomInfo Economy { get; } = new("https://zoom.us/j/2521095403?pwd=MVBmOURvRGU1azRwY0lnejVwa2tjUT09", "252 109 5403", "2021", null, "김용지");
                public static ZoomInfo History { get; } = new("https://zoom.us/j/91236721004?pwd=MkhMN3FLdXZsbW1LbExqWWNkaGhHdz09", "912 3672 1004", "duhanworld", null, "이두한");
                // public static ZoomInfo Geography { get; } = new("");

                // Science
                // public static ZoomInfo Physics { get; } = new("");
                public static ZoomInfo Chemistry { get; } = new("https://zoom.us/j/93595351190?pwd=eHVIMXVGSnFTaGhYWVprNm4xTEh0Zz09", "935 9535 1190", "2021gghs", null, "이지은");
                public static ZoomInfo Biology { get; } = new("https://zoom.us/j/4153909733?pwd=M2h3eVp5WnNPUTdJdlBwMkd1VGs1dz09", "415 390 9733", "1111", null, "이지원");

                // Specials [Completed]
                public static ZoomInfo Ethics { get; } = new("https://zoom.us/j/9401959597?pwd=TE5BSW5jSUFpaE1xKytzZ2I4Q2FWUT09", "940 195 9597", "255226", null, "류제광");
                public static ZoomInfo Environment { get; } = new("https://zoom.us/j/94849418747", "948 4941 8747", "geogeo", "https://classroom.google.com/u/0/c/MjgyOTMwNTQ2NTU5", "조한솔");

                // Language
                // public static ZoomInfo Spanish { get; } = new("https://us02web.zoom.us/j/7411091130?pwd=M0tZTXJwaFRXb3RmZm1jODNhdUtqUT09");
                public static ZoomInfo Chinese { get; } = new("https://zoom.us/j/99535123743?pwd=d0dPemVjNXIxcks5RCt0OFc1aGg0Zz09", "995 3512 3743", "1eMXJM", null, "김나연");
                // public static ZoomInfo Japanese { get; } = new("");

                // Others
                public static ZoomInfo CreativeSolve { get; } = new("https://meet.google.com/lookup/bgt6c65ccm", null, null, "https://classroom.google.com/u/0/c/Mjc3MDAwNjYxOTcx", "조광진");
                public static ZoomInfo Sport1to4 { get; } = new("https://zoom.us/j/3373011774?pwd=QWYybW1SMlljVlVFRzRTY0RvenVrUT09", "337 301 1774", "123456", null, "허진용");
                public static ZoomInfo Sport5to8 { get; } = new("https://zoom.us/j/3225620828?pwd=NHBqdnZrZnZCMTNpRmlCakhVUVJPZz09", "322 562 0828", "1234", null, "윤보경");
                //
            }

            public ZoomDictionary Class3 { get; } = new()
            {
                [Subjects.CellName.Literature] = Common.Literature1to3,
                [Subjects.CellName.Mathematics] = Common.Math1to3,

                // English
                [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA1to4,
                [Subjects.CellName.CriticalEnglish + "B"] = Common.CriticalEnglishB,
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
            public ZoomDictionary Class4 { get; } = new()
            {
                [Subjects.CellName.Literature] = Common.Literature4to6,
                [Subjects.CellName.Mathematics] = Common.Math4to6,

                // English
                [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA1to4,
                [Subjects.CellName.CriticalEnglish + "B"] = Common.CriticalEnglishB,
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

            public ZoomDictionary Class5 { get; } = new()
            {
                [Subjects.CellName.Literature] = Common.Literature4to6,
                [Subjects.CellName.Mathematics] = Common.Math4to6,

                // English
                [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA5to8,
                [Subjects.CellName.CriticalEnglish + "B"] = Common.CriticalEnglishB,
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

            public ZoomDictionary Class6 { get; } = new()
            {
                [Subjects.CellName.Literature] = Common.Literature4to6,
                [Subjects.CellName.Mathematics] = Common.Math4to6,

                // English
                [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA5to8,
                [Subjects.CellName.CriticalEnglish + "B"] = Common.CriticalEnglishB,
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

            public ZoomDictionary Class8 { get; } = new()
            {
                [Subjects.CellName.Literature] = Common.Literature7to8,
                [Subjects.CellName.Mathematics] = Common.Math7to8,

                // English
                [Subjects.CellName.CriticalEnglish + "A"] = Common.CriticalEnglishA5to8,
                [Subjects.CellName.CriticalEnglish + "B"] = Common.CriticalEnglishB,
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

            public ZoomDictionary Class1 => throw new NotImplementedException();

            public ZoomDictionary Class2 => throw new NotImplementedException();

            public ZoomDictionary Class7 => throw new NotImplementedException();
        }
    }
}
