using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static System.DayOfWeek;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class IsExternalInit { }
}


namespace GGHS
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class RefersToCellNameAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class RefersToComboBoxNameAttribute : Attribute
    {
    }

    namespace Grade2
    {
        [RefersToCellName]
        public static class ZoomLinks
        {
            public record ZoomInfo(string Link, string Id, string Pw);
            private static class Common
            {
                // Literature
                public static readonly ZoomInfo Literature4to6 = new("https://zoom.us/j/85644781112?pwd=SC8xWDBZbVRpNW1TSXF6QjRNQk8zQT09", "856 4478 1112", "0204");
                public static readonly ZoomInfo Literature7to8 = new("https://zoom.us/j/97317948690?pwd=WWovQUdBQS8rVVMzMUNyZmNPSDY3Zz09", "973 1794 8690", "626239");

                // Math
                public static readonly ZoomInfo Math4to6 = new("https://zoom.us/j/7014267742?pwd=eFAvUWQweVdSbmc4Q2JLNGlQWTdsZz09", "701 426 7742", "1111");

                //English
                public static readonly ZoomInfo CriticalEnglishA1to4 = new("https://zoom.us/j/5472878985?pwd=RlIzSmJnWHBRaWhkRTNkOEJ6UUF5UT09", "547 287 8985", "0512");
                public static readonly ZoomInfo CriticalEnglishA5to8 = new("https://zoom.us/j/5031101343?pwd=QTRmbnRLSHFPamh4U3d2ZS9JTXdrUT09", "503 110 1343", "1111");

                // public static readonly ZoomInfo CriticalEnglishB = new("https://zoom.us/j/98351310802?pwd=ODlENFVJeGlqWG0wYmgyR2RNUURHUT09", "983 5131 0802", "8at5CP");
                public static readonly ZoomInfo CriticalEnglishC = new("https://zoom.us/j/5365083101?pwd=VDV4VHA5MVUrcDV4cDV1RitZeHovZz09", "536 508 3101", "2021");
                public static readonly ZoomInfo CriticalEnglishD = new("https://zoom.us/j/7936438089?pwd=UkZNaWhFUTE5R2xIYkRxWTRSTC90QT09", "793 643 8089", "1234");

                // Social2
                public static readonly ZoomInfo Politics = new("https://zoom.us/j/4613355190?pwd=WEZzaFhtTVpDZVk4L09XK1VlQ3Z5UT09", "461 335 5190", "1111");
                public static readonly ZoomInfo Economy = new("https://zoom.us/j/2521095403?pwd=MVBmOURvRGU1azRwY0lnejVwa2tjUT09", "252 109 5403", "2021");

                // Science
                public static readonly ZoomInfo Chemistry = new("https://zoom.us/j/93595351190?pwd=eHVIMXVGSnFTaGhYWVprNm4xTEh0Zz09", "935 9535 1190", "2021gghs");
                public static readonly ZoomInfo Biology = new("https://zoom.us/j/4153909733?pwd=M2h3eVp5WnNPUTdJdlBwMkd1VGs1dz09", "415 390 9733", "1111");

                // Social1
                public static readonly ZoomInfo Ethics = new("https://zoom.us/j/9401959597?pwd=TE5BSW5jSUFpaE1xKytzZ2I4Q2FWUT09", "940 195 9597", "255226");
                public static readonly ZoomInfo Environment = new("https://zoom.us/j/94849418747", "948 4941 8747", "geogeo");

                // Language
                public static readonly ZoomInfo Chinese = new("https://zoom.us/j/99535123743?pwd=d0dPemVjNXIxcks5RCt0OFc1aGg0Zz09", "995 3512 3743", "1eMXJM");

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

        [RefersToCellName]
        public static class TimeTables
        {
            static TimeTables()
            {
                foreach (var item in new[] { Class1, Class2, Class3, Class4, Class5, Class6, Class7, Class8})
                {
                    item[0, 5] = item[0, 6] = item[4, 4] = item[4, 5] = Subjects.CellName.Others;
                    item[4, 6] = Subjects.CellName.HomeComing;
                }
                ResetByClass(8);
            }

            /// <summary>
            /// Enables you to access the Class* array human-friendly
            /// </summary>
            /// <param name="class">one of the class string</param>
            /// <param name="day">the day you want to access (except Saturday or Sunday)</param>
            /// <param name="time">the time you want to access (range: 1 to 7)</param>
            /// <returns>the reference of @class</returns>
            public static ref string Of(this string[,] @class, DayOfWeek day, int time)
            {
                if (@class != Class1 && @class != Class2 && @class != Class3 && @class != Class4 && @class != Class5 && @class != Class6 && @class != Class7 && @class != Class8)
                    throw new ArgumentException("Only accepts TimeTables' member array");
                if (day is DayOfWeek.Saturday or DayOfWeek.Sunday)
                    throw new ArgumentException("No class in weekend!");
                if (time is <= 0 or > 7)
                    throw new ArgumentException("Please give 1~7 value");
                return ref @class[(int)day - 1, time - 1];
            }

            public static void ResetByClass(int @class)
            {
                // TODO: Change all to SubjectOf() for future?
                switch (@class)
                {
                    case 1:
                        Class1[1, 0] = Class1[3, 2] = Class1[4, 1] = Subjects.Social1.Selected = Subjects.Social1.Ethics;
                        Class1[0, 0] = Class1[1, 6] = Class1[2, 3] = Subjects.Social2.Selected = Subjects.Social2.Politics;
                        Class1[0, 1] = Class1[3, 0] = Class1[4, 0] = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                        Class1[1, 2] = Class1[2, 6] = Class1[4, 3] = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                        break;
                    case 2:
                        Class2[1, 1] = Class2[2, 6] = Class2[3, 4] = Subjects.Social1.Selected = Subjects.Social1.Ethics;
                        Class2[0, 4] = Class2[3, 6] = Class2[4, 2] = Subjects.Social2.Selected;
                        Class2[0, 0] = Class2[2, 5] = Class2[3, 5] = Subjects.Languages.Selected;
                        Class2[1, 4] = Class2[2, 0] = Class2[4, 0] = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                        break;
                    case 3:
                        Class3[0, 4] = Class3[1, 5] = Class3[3, 1] = Subjects.Social1.Selected = Subjects.Social1.Ethics;
                        Class3[1, 3] = Class3[3, 3] = Class3[4, 3] = Subjects.Social2.Selected;
                        Class3[1, 6] = Class3[2, 3] = Class3[3, 6] = Subjects.Languages.Selected;
                        Class3[0, 1] = Class3[2, 4] = Class3[3, 4] = Subjects.Sciences.Selected;
                        break;
                    case 4:
                        Class4[0, 1] = Class4[1, 6] = Class4[2, 5] = Subjects.Social1.Selected = Subjects.Social1.Ethics;
                        Class4[0, 4] = Class4[3, 6] = Class4[4, 2] = Subjects.Social2.Selected;
                        Class4[1, 0] = Class4[3, 4] = Class4[4, 0] = Subjects.Languages.Selected = Subjects.Languages.Chinese;
                        Class4[0, 2] = Class4[3, 5] = Class4[4, 1] = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                        break;
                    case 5:
                        Class5[0, 2] = Class5[3, 6] = Class5[4, 0] = Subjects.Social1.Selected;
                        Class5[1, 3] = Class5[3, 3] = Class5[4, 3] = Subjects.Social2.Selected;
                        Class5[0, 3] = Class5[1, 2] = Class5[2, 1] = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                        Class5[0, 1] = Class5[2, 4] = Class5[3, 4] = Subjects.Sciences.Selected;
                        break;
                    case 6:
                        Class6[1, 1] = Class6[2, 4] = Class6[4, 3] = Subjects.Social1.Selected = Subjects.Social1.Environment;
                        Class6[0, 4] = Class6[3, 6] = Class6[4, 2] = Subjects.Social2.Selected;
                        Class6[1, 3] = Class6[3, 1] = Class6[4, 1] = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                        Class6[1, 6] = Class6[2, 3] = Class6[3, 2] = Subjects.Sciences.Selected = Subjects.Sciences.Biology; 
                        break;
                    case 7:
                        Class7[0, 2] = Class7[3, 6] = Class7[4, 0] = Subjects.Social1.Selected;
                        Class7[1, 3] = Class7[3, 3] = Class7[4, 3] = Subjects.Social2.Selected;
                        Class7[0, 0] = Class7[2, 5] = Class7[3, 5] = Subjects.Languages.Selected;
                        Class7[0, 4] = Class7[2, 1] = Class7[3, 0] = Subjects.Sciences.Selected;
                        break;
                    case 8:
                        Class8[1, 5] = Class8[3, 1] = Class8[4, 1] = Subjects.Social1.Selected = Subjects.Social1.Environment;
                        Class8[1, 3] = Class8[3, 3] = Class8[4, 3] = Subjects.Social2.Selected;
                        Class8[1, 6] = Class8[2, 3] = Class8[3, 6] = Subjects.Languages.Selected;
                        Class8.Of(Monday, 5) = Class8.Of(Wednesday, 2) = Class8.Of(Thursday, 1) = Subjects.Sciences.Selected;
                        break;
                }
            }
            public static readonly string[,] Class1 = new string[5, 7]
            {
            { Subjects.Social2.Selected, Subjects.Languages.Selected, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.CriticalEnglish + "C", null, null},
            { Subjects.Social1.Selected, Subjects.CellName.CreativeSolve, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "A", Subjects.Social2.Selected },
            { Subjects.CellName.CreativeSolve, Subjects.CellName.Mathematics, Subjects.CellName.MathResearch, Subjects.Social2.Selected, Subjects.CellName.Literature, Subjects.CellName.Sport, Subjects.Sciences.Selected },
            { Subjects.Languages.Selected, Subjects.CellName.Sport, Subjects.Social1.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Literature, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "D"},
            { Subjects.Languages.Selected, Subjects.Social1.Selected, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, null, null, null }
            };
            public static readonly string[,] Class2 = new string[5, 7]
            {
            { Subjects.Languages.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Social2.Selected, null, null },
            { Subjects.CellName.Literature, Subjects.Social1.Selected, Subjects.CellName.Sport, Subjects.CellName.CreativeSolve, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "D" },
            { Subjects.Sciences.Selected, Subjects.CellName.Sport, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.MathResearch, Subjects.Languages.Selected, Subjects.Social1.Selected },
            { Subjects.CellName.Literature, Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish + "C", Subjects.CellName.Mathematics, Subjects.Social1.Selected, Subjects.Languages.Selected, Subjects.Social2.Selected},
            { Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.Social2.Selected, Subjects.CellName.CriticalEnglish + "A", null, null, null }
            };
            public static readonly string[,] Class3 = new string[5, 7]
            {
            { Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.Sport,  Subjects.Social1.Selected, null, null},
            { Subjects.CellName.CriticalEnglish + "D",  Subjects.CellName.Literature,  Subjects.CellName.Mathematics,  Subjects.Social2.Selected,  Subjects.CellName.CriticalEnglish + "B",  Subjects.Social1.Selected, Subjects.Languages.Selected},
            { Subjects.CellName.Sport, Subjects.CellName.MathResearch, Subjects.CellName.CriticalEnglish+"A", Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.Literature, Subjects.CellName.Mathematics },
            { Subjects.CellName.Mathematics, Subjects.Social1.Selected, Subjects.CellName.Literature, Subjects.Social2.Selected, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.Languages.Selected},
            { Subjects.CellName.CriticalEnglish + "C", Subjects.CellName.CreativeSolve, Subjects.CellName.Literature, Subjects.Social2.Selected, null, null, null }
            };
            public static readonly string[,] Class4 = new string[5, 7]
            {
            { Subjects.CellName.CreativeSolve, Subjects.Social1.Selected, Subjects.Sciences.Selected, Subjects.CellName.Literature, Subjects.Social2.Selected, null, null },
            { Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Literature, Subjects.CellName.Sport, Subjects.CellName.CriticalEnglish + "C", Subjects.Social1.Selected },
            { Subjects.CellName.CriticalEnglish +"A", Subjects.CellName.CreativeSolve, Subjects.CellName.MathResearch, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Social1.Selected, Subjects.CellName.Sport },
            { Subjects.CellName.CriticalEnglish+"A", Subjects.CellName.CriticalEnglish+"D", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.Social2.Selected},
            { Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.Social2.Selected, Subjects.CellName.Mathematics, null, null, null }
            };
            public static readonly string[,] Class5 = new string[5, 7]
    {
            { Subjects.CellName.CriticalEnglish + "C", Subjects.Sciences.Selected, Subjects.Social1.Selected, Subjects.Languages.Selected, Subjects.CellName.Mathematics, null, null },
            { Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Social2.Selected, Subjects.CellName.CriticalEnglish + "D", Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.CreativeSolve},
            { Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.CellName.MathResearch, Subjects.CellName.CriticalEnglish + "A", Subjects.Sciences.Selected, Subjects.CellName.Sport, Subjects.CellName.Mathematics },
            { Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Sport, Subjects.CellName.Literature, Subjects.Social2.Selected, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.Social1.Selected},
            { Subjects.Social1.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Social2.Selected, null, null, null }
    };
            public static readonly string[,] Class6 = new string[5, 7]
            {
            { Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Literature, Subjects.CellName.CreativeSolve,  Subjects.Social2.Selected,  Subjects.CellName.Others,  Subjects.CellName.Others },
            { Subjects.CellName.CriticalEnglish + "C",  Subjects.Social1.Selected,  Subjects.CellName.Sport,  Subjects.Languages.Selected,  Subjects.CellName.Mathematics,  Subjects.CellName.Literature, Subjects.Sciences.Selected},
            { Subjects.CellName.MathResearch, Subjects.CellName.Sport, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.Social1.Selected, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "D" },
            { Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Mathematics, Subjects.Social2.Selected},
            { Subjects.CellName.CriticalEnglish + "A", Subjects.Languages.Selected, Subjects.Social2.Selected, Subjects.Social1.Selected, null, null, null }
            };
            public static readonly string[,] Class7 = new string[5, 7]
            {
            { Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.Social1.Selected, Subjects.CellName.Sport, Subjects.Sciences.Selected, null, null },
            { Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish+"D", Subjects.CellName.CriticalEnglish + "C", Subjects.Social2.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.MathResearch, Subjects.CellName.Literature },
            { Subjects.CellName.Sport, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Literature, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.CellName.CreativeSolve },
            { Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.Social2.Selected, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Social1.Selected},
            { Subjects.Social1.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Social2.Selected, null, null, null }
            };
            public static readonly string[,] Class8 = new string[5, 7] {
            { Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "D", Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.Sciences.Selected, null, null },
            { Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Social2.Selected, Subjects.CellName.Sport, Subjects.Social1.Selected, Subjects.Languages.Selected },
            { Subjects.CellName.Literature, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "C", Subjects.Languages.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.MathResearch, Subjects.CellName.Sport},
            { Subjects.Sciences.Selected, Subjects.Social1.Selected, Subjects.CellName.Literature, Subjects.Social2.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "B", Subjects.Languages.Selected},
            { Subjects.CellName.Mathematics, Subjects.Social1.Selected, Subjects.CellName.CreativeSolve, Subjects.Social2.Selected, null, null, null}
        };
        }

        public static class Subjects
        {
            //using static Subjects.CellName?

            /// <summary>
            /// ComboBoxName: Used in ComboBox Text
            /// </summary>
            public class ComboBoxName
            {
                // 이걸 상위 하위 변환이 가능한가?
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
            /// <summary>
            /// CellName: Used in TimeTable Text or Pop-up Dialogs
            /// </summary>
            public class CellName : ComboBoxName
            {
                new public const string CriticalEnglish = "비영";
                new public const string Sport = "운동";
                new public const string CreativeSolve = "창문해";
                new public const string MathResearch = "수과탐";
                new public const string Others = "창체";
                new public const string Ethics = "실윤이";
                new public const string Environment = "인환";
            }

            //
            //  enum에서 모두 property {get;}으로 만들것
            //  상속을 이용한다
            //  implicit operator을 이용해서 자동으로 string으로 변환되게끔
            //  public static implicit operator String(LogCategory category) { return Value; } // 정한 값
            //  각각 class마다 default 값을 추가하고
            //  멤버 변수를 하나 두자. 뭐가 설정되었는지.
            //

            /// <summary>
            /// (Extended) Return the string value if it is in the string[] list.
            /// </summary>
            /// <param name="var">this string value</param>
            /// <param name="else">Returned if var is not in array</param>
            /// <param name="array">Containers to check if var is in</param>
            /// <returns>original variable if in the list, else return @else</returns>
            static string ReturnIfHasInOrElse(this string var, string @else, params string[] array) => Array.IndexOf(array, var) > -1? var : @else;
            /// <summary>
            /// Reset All the subjects as None
            /// </summary>
            public static void Clear()
            {
                Sciences.Selected = Sciences.None;
                Social1.Selected = Social1.None;
                Social2.Selected = Social2.None;
                Languages.Selected = Languages.None;
            }

            [RefersToCellName]
            public static class Sciences // sealed
            {
                // 여기는 어쩔 수 없이 switch 때문에 const가 되어야 함...
                public const string Physics = CellName.Physics;
                public const string Chemistry = CellName.Chemistry;
                public const string Biology = CellName.Biology;
                public const string None = "과탐";
                static string selected = None;
                public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Physics,Chemistry,Biology); }
            }

            [RefersToCellName]
            public static class Social1
            {
                public const string Ethics = CellName.Ethics;
                public const string Environment = CellName.Environment;
                public const string None = "전문";
                static string selected = None;

                // 이거 읽기 전용으로 만들고 get set 다가진 selected를 추가하자 그리고 변환 메서드도 만들고..
                public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Ethics, Environment); }
            }

            [RefersToCellName]
            public static class Social2
            {
                public const string History = CellName.History;
                public const string Geography = CellName.Geography;
                public const string Politics = CellName.Politics;
                public const string Economy = CellName.Economy;
                public const string None = "사탐";
                static string selected = None;
                public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, History, Geography, Politics, Economy); }
            }

            [RefersToCellName]
            public class Languages
            {
                public const string Japanese = CellName.Japanese;
                public const string Spanish = CellName.Spanish;
                public const string Chinese = CellName.Chinese;
                public const string None = "외국어";
                static string selected = None;
                public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Japanese, Spanish, Chinese); }
            }
        }
    }
}
