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
                public static ZoomInfo Reading => new(null, null, null, "https://classroom.google.com/u/0/c/Mzc4NjE5OTMzNjQw", "");
                public static ZoomInfo Mathematics => new(null, null, null, "https://classroom.google.com/u/0/c/MjgzNDM1NjIyODM0", "");


                public static ZoomInfo GlobalEconomics => new("https://zoom.us/j/2521095403?pwd=MVBmOURvRGU1azRwY0lnejVwa2tjUT09", "252 109 5403", "2021", null, "김용지");
                public static class Class1to3
                {
                    public static ZoomInfo Reading => Common.Reading with { Link = "https://zoom.us/j/92759524061?pwd=dHUwZllmSEp2NWpRYlVTbmdjODR3dz09", Id = "927 5952 4061", Pw = "0203", Teacher = "백지혜" };
                    public static ZoomInfo Mathematics => Common.Mathematics with { Teacher = "김수진" };

                }
                public static class Class4to6
                {
                    public static ZoomInfo Reading => Common.Reading with { Link = "https://zoom.us/j/85644781112?pwd=SC8xWDBZbVRpNW1TSXF6QjRNQk8zQT09", Id = "856 4478 1112", Pw = "0204", Teacher = "윤채영" };
                    public static ZoomInfo Mathematics => Common.Mathematics with { Teacher = "박지영" };
                }
                public static class Class7to8
                {
                    public static ZoomInfo Reading => Common.Reading with { Link = "https://zoom.us/j/97317948690?pwd=WWovQUdBQS8rVVMzMUNyZmNPSDY3Zz09", Id = "973 1794 8690", Pw = "626239", Teacher = "임제완" };
                    public static ZoomInfo Mathematics => Common.Mathematics with { Teacher = "공현진" };

                }


                // Advanced English
                // (string? Link, string? Id, string? Pw, string? ClassRoom, string Teacher);
                public static ZoomInfo AdvancedEnglishA1to4 => new("https://zoom.us/j/5472878985?pwd=RlIzSmJnWHBRaWhkRTNkOEJ6UUF5UT09", "547 287 8985", "0512", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "김찬미");
                public static ZoomInfo AdvancedEnglishA5to8 => new("https://zoom.us/j/5031101343?pwd=QTRmbnRLSHFPamh4U3d2ZS9JTXdrUT09", "503 110 1343", "1111", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "김한나");
                public static ZoomInfo AdvancedEnglishB => new(null, null, null, "https://classroom.google.com/u/0/c/Mzc4Mzc4MDg4NzY4", "Lubi");
                public static ZoomInfo AdvancedEnglishC => new("https://zoom.us/j/5365083101?pwd=VDV4VHA5MVUrcDV4cDV1RitZeHovZz09", "536 508 3101", "2021", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "허진");
                public static ZoomInfo AdvancedEnglishD1to2 => new("https://zoom.us/j/5759526245?pwd=akMzYU5kUVpMc202aVkrcWZNRGZSUT09", "575 952 6245", "01020304", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "문선현");
                public static ZoomInfo AdvancedEnglishD3to4 => new("https://zoom.us/j/2277812654?pwd=aytrUzdSRTJSQnNHcTlaUEdJQzFBZz09", "227 781 2654", "11111", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "오지연");
                public static ZoomInfo AdvancedEnglishD5to6 => new("https://zoom.us/my/gghsjennifer?pwd=bkphMW9QeGpXellzczY4d0FCUEFqQT09", "274 535 2030", "20212", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "이경진");
                public static ZoomInfo AdvancedEnglishD7to8 => new("https://zoom.us/j/7936438089?pwd=UkZNaWhFUTE5R2xIYkRxWTRSTC90QT09", "793 643 8089", "1234", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "장종윤");


                public static ZoomInfo LifeAndScience => new("https://zoom.us/j/4153909733?pwd=M2h3eVp5WnNPUTdJdlBwMkd1VGs1dz09", "415 390 9733", "1111", "https://classroom.google.com/u/0/c/Mzc4MzMzMzI1MTA1", "이지원");

                public static ZoomInfo GlobalPolitics2to4 => new("https://zoom.us/j/93720213007?pwd=NVhibHlFdk5QK21uenZFdUhDb3VwQT09", "937 2021 3007", "204", "https://classroom.google.com/u/0/c/MzA1OTAyOTM5OTM2", "엄혜용");
                public static ZoomInfo GlobalPolitics1and8 => new("https://zoom.us/j/4613355190?pwd=WEZzaFhtTVpDZVk4L09XK1VlQ3Z5UT09", "461 335 5190", "1111", "https://classroom.google.com/u/0/c/MzA1OTAyOTM5OTM2", "정슬기");
                public static ZoomInfo CompareCultureA => new("https://zoom.us/j/6941791038?pwd=TUJLRnZEVEhMNGVhRGxtaVBXb3BNZz09", "694 179 1038", "2021", "https://classroom.google.com/u/0/c/Mzc4NzczNjQ1MDU0", "홍정민");
                public static ZoomInfo CompareCultureB => new("https://zoom.us/j/5467290895?pwd=dUFHMDlEdjk3VzV6dnl0OERCVzlqQT09", "546 729 0895", "2030", "https://classroom.google.com/u/0/c/Mzc4NzczNjQ1MDU0", "정혜영");
                /*
public static string Mathematics => "수학Ⅱ";
public static string Sport => "운동과 건강";
public static string CreativeSolve => "창의적 문제 해결 기법";
public static string MathResearch => "수학과제탐구";
public static string Others => "창의적 체험활동";
public static string HomeComing => "홈커밍";

public static string ScienceHistory => "과학사";
public static string LifeAndScience => "생활과 과학";

public static string GlobalEconomics => "국제경제";
public static string GlobalPolitics => "국제정치";
public static string CompareCulture => "비교문화";
public static string EasternHistory => "동양근대사";
public static string HistoryAndCulture => "세계 역사와 문화";
public static string PoliticsPhilosophy => "현대정치철학의 이해";
public static string RegionResearch => "세계 지역 연구";
public static string GISAnalyze => "공간 정보와 공간 분석";
*/




                // Language
                // public static ZoomInfo Spanish => new("https://us02web.zoom.us/j/7411091130?pwd=M0tZTXJwaFRXb3RmZm1jODNhdUtqUT09");
                public static ZoomInfo Chinese => new("https://zoom.us/j/99535123743?pwd=d0dPemVjNXIxcks5RCt0OFc1aGg0Zz09", "995 3512 3743", "1eMXJM", "https://classroom.google.com/u/0/c/MjgzOTg5NzIyMjQ5", "김나연");
                public static ZoomInfo Japanese => new("https://zoom.us/j/2298991005?pwd=ZUpFSWdlTHdleTZzSEVSeEZSTDlzQT09", null, null, null, "하지원");

                // Others
                public static ZoomInfo CreativeSolve => new("https://meet.google.com/lookup/bgt6c65ccm", null, null, "https://classroom.google.com/u/0/c/Mjc3MDAwNjYxOTcx", "조광진");
                public static ZoomInfo Sport1to4 => new("https://zoom.us/j/3373011774?pwd=QWYybW1SMlljVlVFRzRTY0RvenVrUT09", "337 301 1774", "123456", null, "허진용");
                public static ZoomInfo Sport5to8 => new("https://zoom.us/j/3225620828?pwd=NHBqdnZrZnZCMTNpRmlCakhVUVJPZz09", "322 562 0828", "1234", null, "윤보경");
            }

            public ZoomDictionary Class1 => new()
            {
                [Subjects.CellName.Reading] = Common.Class1to3.Reading,

                [Subjects.CellName.Mathematics] = Common.Class1to3.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA1to4,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD1to2,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,

                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics1and8,
                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,
            };

            public ZoomDictionary Class2 => new()
            {
                [Subjects.CellName.Reading] = Common.Class1to3.Reading,

                [Subjects.CellName.Mathematics] = Common.Class1to3.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA1to4,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD1to2,

                [Subjects.CellName.Chinese] = Common.Chinese,
                [Subjects.CellName.Japanese] = Common.Japanese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,

                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics2to4,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,
            };

            public ZoomDictionary Class3 => new()
            {
                [Subjects.CellName.Reading] = Common.Class1to3.Reading,

                [Subjects.CellName.Mathematics] = Common.Class1to3.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA1to4,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD3to4,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,


                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics2to4,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,
            };

            public ZoomDictionary Class4 => new()
            {
                [Subjects.CellName.Reading] = Common.Class4to6.Reading,

                [Subjects.CellName.Mathematics] = Common.Class4to6.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA1to4,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD3to4,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,


                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics2to4,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,
            };

            public ZoomDictionary Class5 => new()
            {
                [Subjects.CellName.Reading] = Common.Class4to6.Reading,

                [Subjects.CellName.Mathematics] = Common.Class4to6.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA5to8,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD5to6,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,


                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                // [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,
            };

            public ZoomDictionary Class6 => new()
            {
                [Subjects.CellName.Reading] = Common.Class4to6.Reading,

                [Subjects.CellName.Mathematics] = Common.Class4to6.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA5to8,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD5to6,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,


                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                // [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,
            };

            public ZoomDictionary Class7 => new()
            {
                [Subjects.CellName.Reading] = Common.Class7to8.Reading,

                [Subjects.CellName.Mathematics] = Common.Class7to8.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA5to8,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD7to8,

                [Subjects.CellName.Chinese] = Common.Chinese,
                [Subjects.CellName.Japanese] = Common.Japanese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,


                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                // [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,
            };

            public ZoomDictionary Class8 => new()
            {
                [Subjects.CellName.Reading] = Common.Class7to8.Reading,

                [Subjects.CellName.Mathematics] = Common.Class7to8.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA5to8,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD7to8,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,


                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                // [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,
            };

        }

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
