using System;
using System.Collections.Generic;
namespace GGHS
{
    namespace Grade2
    {
        public static class Class8
        {
            // 분리할까?
            public record ZoomInfo(string Link, string Id, string Pw);
            public static readonly Dictionary<string, ZoomInfo> ZoomLink = new()
            {
                [Subjects.Literature] = new("https://zoom.us/j/97317948690?pwd=WWovQUdBQS8rVVMzMUNyZmNPSDY3Zz09", "973 1794 8690", "626239"),
                [Subjects.RawName.Chemistry] = new("https://zoom.us/j/93595351190?pwd=eHVIMXVGSnFTaGhYWVprNm4xTEh0Zz09", "935 9535 1190", "2021gghs"),
                [Subjects.RawName.Economy] = new("https://zoom.us/j/2521095403?pwd=MVBmOURvRGU1azRwY0lnejVwa2tjUT09", "252 109 5403", "2021"),
                [Subjects.RawName.Environment] = new("https://zoom.us/j/94849418747", "948 4941 8747", "geogeo"),
                [Subjects.RawName.Chinese] = new("https://zoom.us/j/99535123743?pwd=d0dPemVjNXIxcks5RCt0OFc1aGg0Zz09", "995 3512 3743", "1eMXJM"),
                [Subjects.CreativeSolve] = new("https://meet.google.com/lookup/bgt6c65ccm", "None", "None"),
                [Subjects.CriticalEnglish + "A"] = new("https://zoom.us/j/5031101343?pwd=QTRmbnRLSHFPamh4U3d2ZS9JTXdrUT09", "503 110 1343", "1111"),
                [Subjects.CriticalEnglish + "B"] = new("https://zoom.us/j/98351310802?pwd=ODlENFVJeGlqWG0wYmgyR2RNUURHUT09", "983 5131 0802", "8at5CP"),
                [Subjects.CriticalEnglish + "C"] = new("https://zoom.us/j/5365083101?pwd=VDV4VHA5MVUrcDV4cDV1RitZeHovZz09", "536 508 3101", "2021"),
            };
        }
        public static class Classes
        {
            public static readonly string[,] Class1 = new string[5, 7]
            {
            { Subjects.Social2Tmp, Subjects.LanguageTmp, Subjects.Literature, Subjects.CriticalEnglish + "B", Subjects.CriticalEnglish + "C", Subjects.Others, Subjects.Others },
            { Subjects.Social1Tmp, Subjects.CreativeSolve, Subjects.ScienceTmp, Subjects.Mathematics, Subjects.Literature, Subjects.CriticalEnglish + "A", Subjects.Social2Tmp },
            { Subjects.CreativeSolve, Subjects.Mathematics, Subjects.MathResearch, Subjects.Social2Tmp, Subjects.Literature, Subjects.Sport, Subjects.ScienceTmp },
            { Subjects.LanguageTmp, Subjects.Sport, Subjects.Social1Tmp, Subjects.CriticalEnglish + "A", Subjects.Literature, Subjects.Mathematics, Subjects.CriticalEnglish + "D"},
            { Subjects.LanguageTmp, Subjects.Social1Tmp, Subjects.Mathematics, Subjects.ScienceTmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
            };
            public static readonly string[,] Class2 = new string[5, 7]
            {
            { Subjects.LanguageTmp, Subjects.CriticalEnglish + "A", Subjects.Mathematics, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others },
            { Subjects.Literature, Subjects.Social1Tmp, Subjects.Sport, Subjects.CreativeSolve, Subjects.ScienceTmp, Subjects.Mathematics, Subjects.CriticalEnglish + "D" },
            { Subjects.ScienceTmp, Subjects.Sport, Subjects.Literature, Subjects.CriticalEnglish + "B", Subjects.MathResearch, Subjects.LanguageTmp, Subjects.Social1Tmp },
            { Subjects.Literature, Subjects.CreativeSolve, Subjects.CriticalEnglish + "C", Subjects.Mathematics, Subjects.Social1Tmp, Subjects.LanguageTmp, Subjects.Social2Tmp},
            { Subjects.ScienceTmp, Subjects.Mathematics, Subjects.Social2Tmp, Subjects.CriticalEnglish + "A", Subjects.Others, Subjects.Others, Subjects.HomeComing }
            };
            public static readonly string[,] Class3 = new string[5, 7]
            {
            { Subjects.Mathematics, Subjects.ScienceTmp, Subjects.CreativeSolve, Subjects.Sport,  Subjects.Social1Tmp,  Subjects.Others,  Subjects.Others },
            { Subjects.CriticalEnglish + "D",  Subjects.Literature,  Subjects.Mathematics,  Subjects.Social2Tmp,  Subjects.CriticalEnglish + "B",  Subjects.Social1Tmp, Subjects.LanguageTmp},
            { Subjects.Sport, Subjects.MathResearch, Subjects.CriticalEnglish+"A", Subjects.LanguageTmp, Subjects.ScienceTmp, Subjects.Literature, Subjects.Mathematics },
            { Subjects.Mathematics, Subjects.Social1Tmp, Subjects.Literature, Subjects.Social2Tmp, Subjects.ScienceTmp, Subjects.CriticalEnglish + "A", Subjects.LanguageTmp},
            { Subjects.CriticalEnglish + "C", Subjects.CreativeSolve, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
            };
            public static readonly string[,] Class4 = new string[5, 7]
            {
            { Subjects.CreativeSolve, Subjects.Social1Tmp, Subjects.ScienceTmp, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others },
            { Subjects.LanguageTmp, Subjects.Mathematics, Subjects.CriticalEnglish + "B", Subjects.Literature, Subjects.Sport, Subjects.CriticalEnglish + "C", Subjects.Social1Tmp },
            { Subjects.CriticalEnglish +"A", Subjects.CreativeSolve, Subjects.MathResearch, Subjects.Mathematics, Subjects.Literature, Subjects.Social1Tmp, Subjects.Sport },
            { Subjects.CriticalEnglish+"A", Subjects.CriticalEnglish+"D", Subjects.Mathematics, Subjects.Literature, Subjects.LanguageTmp, Subjects.ScienceTmp, Subjects.Social2Tmp},
            { Subjects.LanguageTmp, Subjects.ScienceTmp, Subjects.Social2Tmp, Subjects.Mathematics, Subjects.Others, Subjects.Others, Subjects.HomeComing }
            };
            public static readonly string[,] Class5 = new string[5, 7]
    {
            { Subjects.CriticalEnglish + "C", Subjects.ScienceTmp, Subjects.Social1Tmp, Subjects.LanguageTmp, Subjects.Mathematics, Subjects.Others, Subjects.Others },
            { Subjects.Mathematics, Subjects.Literature, Subjects.LanguageTmp, Subjects.Social2Tmp, Subjects.CriticalEnglish + "D", Subjects.CriticalEnglish + "B", Subjects.CreativeSolve},
            { Subjects.Literature, Subjects.LanguageTmp, Subjects.MathResearch, Subjects.CriticalEnglish + "A", Subjects.ScienceTmp, Subjects.Sport, Subjects.Mathematics },
            { Subjects.CriticalEnglish + "A", Subjects.Sport, Subjects.Literature, Subjects.Social2Tmp, Subjects.ScienceTmp, Subjects.CreativeSolve, Subjects.Social1Tmp},
            { Subjects.Social1Tmp, Subjects.Mathematics, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
    };
            public static readonly string[,] Class6 = new string[5, 7]
            {
            { Subjects.Mathematics, Subjects.CriticalEnglish + "A", Subjects.Literature, Subjects.CreativeSolve,  Subjects.Social2Tmp,  Subjects.Others,  Subjects.Others },
            { Subjects.CriticalEnglish + "C",  Subjects.Social1Tmp,  Subjects.Sport,  Subjects.LanguageTmp,  Subjects.Mathematics,  Subjects.Literature, Subjects.ScienceTmp},
            { Subjects.MathResearch, Subjects.Sport, Subjects.Mathematics, Subjects.ScienceTmp, Subjects.Social1Tmp, Subjects.Literature, Subjects.CriticalEnglish + "D" },
            { Subjects.Literature, Subjects.LanguageTmp, Subjects.ScienceTmp, Subjects.CreativeSolve, Subjects.CriticalEnglish + "B", Subjects.Mathematics, Subjects.Social2Tmp},
            { Subjects.CriticalEnglish + "A", Subjects.LanguageTmp, Subjects.Social2Tmp, Subjects.Social1Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
            };
            public static readonly string[,] Class7 = new string[5, 7]
            {
            { Subjects.LanguageTmp, Subjects.Mathematics, Subjects.Social1Tmp, Subjects.Sport, Subjects.ScienceTmp, Subjects.Others, Subjects.Others },
            { Subjects.CreativeSolve, Subjects.CriticalEnglish+"D", Subjects.CriticalEnglish + "C", Subjects.Social2Tmp, Subjects.CriticalEnglish + "A", Subjects.MathResearch, Subjects.Literature },
            { Subjects.Sport, Subjects.ScienceTmp, Subjects.CriticalEnglish + "B", Subjects.Literature, Subjects.Literature, Subjects.LanguageTmp, Subjects.CreativeSolve },
            { Subjects.ScienceTmp, Subjects.CriticalEnglish + "A", Subjects.Mathematics, Subjects.Social2Tmp, Subjects.Literature, Subjects.LanguageTmp, Subjects.Social1Tmp},
            { Subjects.Social1Tmp, Subjects.Mathematics, Subjects.Literature, Subjects.Social2Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing }
            };
            public static readonly string[,] Class8 = new string[5, 7] {
            { Subjects.Literature, Subjects.CriticalEnglish + "D", Subjects.CriticalEnglish + "A", Subjects.Mathematics, Subjects.ScienceTmp, Subjects.Others, Subjects.Others },
            { Subjects.CriticalEnglish + "A", Subjects.Mathematics, Subjects.Literature, Subjects.Social2Tmp, Subjects.Sport, Subjects.Social1Tmp, Subjects.LanguageTmp },
            { Subjects.Literature, Subjects.ScienceTmp, Subjects.CriticalEnglish + "C", Subjects.LanguageTmp, Subjects.CreativeSolve, Subjects.MathResearch, Subjects.Sport},
            { Subjects.ScienceTmp, Subjects.Social1Tmp, Subjects.Literature, Subjects.Social2Tmp, Subjects.Mathematics, Subjects.CriticalEnglish + "B", Subjects.LanguageTmp},
            { Subjects.Mathematics, Subjects.Social1Tmp, Subjects.CreativeSolve, Subjects.Social2Tmp, Subjects.Others, Subjects.Others, Subjects.HomeComing}
        };
        }
        public static class Subjects
        {

            public static class RawName
            {
                public const string Literature = "문학";
                public const string Mathematics = "수학Ⅰ";
                public const string CriticalEnglish = "비판적 영어 글쓰기와 말하기";
                public const string Sport = "운동과 건강";
                public const string CreativeSolve = "창의적 문제 해결 기법";
                public const string MathResearch = "수학과제탐구";
                public const string Others = "창의적 체험활동";
                public const string HomeComing = "홈커밍";

                public const string Physics = "물리학Ⅰ";
                public const string Chemistry = "화학Ⅰ";
                public const string Biology = "생명과학Ⅰ";

                public const string Ethics = "실천 윤리학의 이해";
                public const string Environment = "인간과 환경";

                public const string History = "세계사";
                public const string Geography = "세계지리";
                public const string Politics = "정치와 법";
                public const string Economy = "경제";

                public const string Japanese = "일본어Ⅰ";
                public const string Spanish = "스페인어Ⅰ";
                public const string Chinese = "중국어Ⅰ";
            }
            public const string Literature = RawName.Literature;
            public const string Mathematics = RawName.Mathematics;
            public const string CriticalEnglish = "비영";
            public const string Sport = "운동";
            public const string CreativeSolve = "창문해";
            public const string MathResearch = "수과탐";
            public const string Others = "창체";
            public const string HomeComing = RawName.HomeComing;

            public const string ScienceTmp = "SCIENCE";
            public const string Social1Tmp = "SOCIAL1";
            public const string Social2Tmp = "SOCIAL2";
            public const string LanguageTmp = "LANGUAGE";

            public enum Science
            {
                Physics,
                Chemistry,
                Biology
            }
            private static Science scienceSubject = Science.Biology;
            public static string GetScienceSubject()
            => scienceSubject switch
            {
                Science.Physics => RawName.Physics,
                Science.Chemistry => RawName.Chemistry,
                Science.Biology => RawName.Biology,
                _ => throw new System.Exception(),
            };

            public static void SetScienceSubject(Science subject)
                => scienceSubject = subject;

            public enum Social1
            {
                Ethics,
                Environment
            }
            private static Social1 social1Subject = Social1.Ethics;
            public static string GetSocial1Subject()
            => social1Subject switch
            {
                Social1.Ethics => "실윤이",
                Social1.Environment => "인환",
                _ => throw new Exception(),
            };

            public static void SetSocial1Subject(Social1 subject)
            => social1Subject = subject;

            public enum Social2
            {
                History,
                Geography,
                Politics,
                Economy
            }
            private static Social2 social2Subject = Social2.Politics;
            public static string GetSocial2Subject()
              => social2Subject switch
              {
                  Social2.History => RawName.History,
                  Social2.Geography => RawName.Geography,
                  Social2.Politics => RawName.Politics,
                  Social2.Economy => RawName.Economy,
                  _ => throw new System.Exception(),
              };
            public static void SetSocial2Subject(Social2 subject)
            => social2Subject = subject;

            public enum Language
            {
                Japanese,
                Spanish,
                Chinese
            }
            private static Language language = Language.Spanish;
            public static string GetLanguageSubject()
            => language switch
            {
                Language.Japanese => RawName.Japanese,
                Language.Spanish => RawName.Spanish,
                Language.Chinese => RawName.Chinese,
                _ => throw new System.Exception(),
            };
            public static void SetLanguageSubject(Language subject)
                => language = subject;
        }
    }
}
