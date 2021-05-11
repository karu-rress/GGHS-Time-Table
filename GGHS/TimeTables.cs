using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.DayOfWeek;

namespace TimeTableUWP
{
    public enum DateType
    {
        YYYYMMDD,
        MMDDYYYY,
        YYYYMMDD2
    }
}

namespace GGHS
{
    namespace Grade2
    {
        public static class TimeTables
        {
            static TimeTables()
            {
                foreach (var item in new[] { Class1, Class2, Class3, Class4, Class5, Class6, Class7, Class8 })
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
                if (day is Saturday or Sunday)
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
                        Class1[1, 0] = Class1[3, 2] = Class1[4, 1] = Subjects.Specials.Selected = Subjects.Specials.Ethics;
                        Class1[0, 0] = Class1[1, 6] = Class1[2, 3] = Subjects.Socials.Selected = Subjects.Socials.Politics;
                        Class1[0, 1] = Class1[3, 0] = Class1[4, 0] = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                        Class1[1, 2] = Class1[2, 6] = Class1[4, 3] = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                        break;
                    case 2:
                        Class2[1, 1] = Class2[2, 6] = Class2[3, 4] = Subjects.Specials.Selected = Subjects.Specials.Ethics;
                        Class2[0, 4] = Class2[3, 6] = Class2[4, 2] = Subjects.Socials.Selected;
                        Class2[0, 0] = Class2[2, 5] = Class2[3, 5] = Subjects.Languages.Selected;
                        Class2[1, 4] = Class2[2, 0] = Class2[4, 0] = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                        break;
                    case 3:
                        Class3[0, 4] = Class3[1, 5] = Class3[3, 1] = Subjects.Specials.Selected = Subjects.Specials.Ethics;
                        Class3[1, 3] = Class3[3, 3] = Class3[4, 3] = Subjects.Socials.Selected;
                        Class3[1, 6] = Class3[2, 3] = Class3[3, 6] = Subjects.Languages.Selected;
                        Class3[0, 1] = Class3[2, 4] = Class3[3, 4] = Subjects.Sciences.Selected;
                        break;
                    case 4:
                        Class4[0, 1] = Class4[1, 6] = Class4[2, 5] = Subjects.Specials.Selected = Subjects.Specials.Ethics;
                        Class4[0, 4] = Class4[3, 6] = Class4[4, 2] = Subjects.Socials.Selected;
                        Class4[1, 0] = Class4[3, 4] = Class4[4, 0] = Subjects.Languages.Selected = Subjects.Languages.Chinese;
                        Class4[0, 2] = Class4[3, 5] = Class4[4, 1] = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                        break;
                    case 5:
                        Class5[0, 2] = Class5[3, 6] = Class5[4, 0] = Subjects.Specials.Selected;
                        Class5[1, 3] = Class5[3, 3] = Class5[4, 3] = Subjects.Socials.Selected;
                        Class5[0, 3] = Class5[1, 2] = Class5[2, 1] = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                        Class5[0, 1] = Class5[2, 4] = Class5[3, 4] = Subjects.Sciences.Selected;
                        break;
                    case 6:
                        Class6[1, 1] = Class6[2, 4] = Class6[4, 3] = Subjects.Specials.Selected = Subjects.Specials.Environment;
                        Class6[0, 4] = Class6[3, 6] = Class6[4, 2] = Subjects.Socials.Selected;
                        Class6[1, 3] = Class6[3, 1] = Class6[4, 1] = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                        Class6[1, 6] = Class6[2, 3] = Class6[3, 2] = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                        break;
                    case 7:
                        Class7[0, 2] = Class7[3, 6] = Class7[4, 0] = Subjects.Specials.Selected;
                        Class7[1, 3] = Class7[3, 3] = Class7[4, 3] = Subjects.Socials.Selected;
                        Class7[0, 0] = Class7[2, 5] = Class7[3, 5] = Subjects.Languages.Selected;
                        Class7[0, 4] = Class7[2, 1] = Class7[3, 0] = Subjects.Sciences.Selected;
                        break;
                    case 8:
                        Class8[1, 5] = Class8[3, 1] = Class8[4, 1] = Subjects.Specials.Selected = Subjects.Specials.Environment;
                        Class8[1, 3] = Class8[3, 3] = Class8[4, 3] = Subjects.Socials.Selected;
                        Class8[1, 6] = Class8[2, 3] = Class8[3, 6] = Subjects.Languages.Selected;
                        Class8.Of(Monday, 5) = Class8.Of(Wednesday, 2) = Class8.Of(Thursday, 1) = Subjects.Sciences.Selected;
                        break;
                }
            }
            public static readonly string[,] Class1 = new string[5, 7]
            {
            { Subjects.Socials.Selected, Subjects.Languages.Selected, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.CriticalEnglish + "C", null, null},
            { Subjects.Specials.Selected, Subjects.CellName.CreativeSolve, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "A", Subjects.Socials.Selected },
            { Subjects.CellName.CreativeSolve, Subjects.CellName.Mathematics, Subjects.CellName.MathResearch, Subjects.Socials.Selected, Subjects.CellName.Literature, Subjects.CellName.Sport, Subjects.Sciences.Selected },
            { Subjects.Languages.Selected, Subjects.CellName.Sport, Subjects.Specials.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Literature, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "D"},
            { Subjects.Languages.Selected, Subjects.Specials.Selected, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, null, null, null }
            };
            public static readonly string[,] Class2 = new string[5, 7]
            {
            { Subjects.Languages.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null },
            { Subjects.CellName.Literature, Subjects.Specials.Selected, Subjects.CellName.Sport, Subjects.CellName.CreativeSolve, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "D" },
            { Subjects.Sciences.Selected, Subjects.CellName.Sport, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.MathResearch, Subjects.Languages.Selected, Subjects.Specials.Selected },
            { Subjects.CellName.Literature, Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish + "C", Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.Languages.Selected, Subjects.Socials.Selected},
            { Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.Socials.Selected, Subjects.CellName.CriticalEnglish + "A", null, null, null }
            };
            public static readonly string[,] Class3 = new string[5, 7]
            {
            { Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.Sport,  Subjects.Specials.Selected, null, null},
            { Subjects.CellName.CriticalEnglish + "D",  Subjects.CellName.Literature,  Subjects.CellName.Mathematics,  Subjects.Socials.Selected,  Subjects.CellName.CriticalEnglish + "B",  Subjects.Specials.Selected, Subjects.Languages.Selected},
            { Subjects.CellName.Sport, Subjects.CellName.MathResearch, Subjects.CellName.CriticalEnglish+"A", Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.Literature, Subjects.CellName.Mathematics },
            { Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.Languages.Selected},
            { Subjects.CellName.CriticalEnglish + "C", Subjects.CellName.CreativeSolve, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null, null }
            };
            public static readonly string[,] Class4 = new string[5, 7]
            {
            { Subjects.CellName.CreativeSolve, Subjects.Specials.Selected, Subjects.Sciences.Selected, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null },
            { Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Literature, Subjects.CellName.Sport, Subjects.CellName.CriticalEnglish + "C", Subjects.Specials.Selected },
            { Subjects.CellName.CriticalEnglish +"A", Subjects.CellName.CreativeSolve, Subjects.CellName.MathResearch, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Specials.Selected, Subjects.CellName.Sport },
            { Subjects.CellName.CriticalEnglish+"A", Subjects.CellName.CriticalEnglish+"D", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.Socials.Selected},
            { Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.Socials.Selected, Subjects.CellName.Mathematics, null, null, null }
            };
            public static readonly string[,] Class5 = new string[5, 7]
    {
            { Subjects.CellName.CriticalEnglish + "C", Subjects.Sciences.Selected, Subjects.Specials.Selected, Subjects.Languages.Selected, Subjects.CellName.Mathematics, null, null },
            { Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Socials.Selected, Subjects.CellName.CriticalEnglish + "D", Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.CreativeSolve},
            { Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.CellName.MathResearch, Subjects.CellName.CriticalEnglish + "A", Subjects.Sciences.Selected, Subjects.CellName.Sport, Subjects.CellName.Mathematics },
            { Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Sport, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.Specials.Selected},
            { Subjects.Specials.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null, null }
    };
            public static readonly string[,] Class6 = new string[5, 7]
            {
            { Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Literature, Subjects.CellName.CreativeSolve,  Subjects.Socials.Selected,  Subjects.CellName.Others,  Subjects.CellName.Others },
            { Subjects.CellName.CriticalEnglish + "C",  Subjects.Specials.Selected,  Subjects.CellName.Sport,  Subjects.Languages.Selected,  Subjects.CellName.Mathematics,  Subjects.CellName.Literature, Subjects.Sciences.Selected},
            { Subjects.CellName.MathResearch, Subjects.CellName.Sport, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.Specials.Selected, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "D" },
            { Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Mathematics, Subjects.Socials.Selected},
            { Subjects.CellName.CriticalEnglish + "A", Subjects.Languages.Selected, Subjects.Socials.Selected, Subjects.Specials.Selected, null, null, null }
            };
            public static readonly string[,] Class7 = new string[5, 7]
            {
            { Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.CellName.Sport, Subjects.Sciences.Selected, null, null },
            { Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish+"D", Subjects.CellName.CriticalEnglish + "C", Subjects.Socials.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.MathResearch, Subjects.CellName.Literature },
            { Subjects.CellName.Sport, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Literature, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.CellName.CreativeSolve },
            { Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.Socials.Selected, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Specials.Selected},
            { Subjects.Specials.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null, null }
            };
            public static readonly string[,] Class8 = new string[5, 7] {
            { Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "D", Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.Sciences.Selected, null, null },
            { Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.CellName.Sport, Subjects.Specials.Selected, Subjects.Languages.Selected },
            { Subjects.CellName.Literature, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "C", Subjects.Languages.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.MathResearch, Subjects.CellName.Sport},
            { Subjects.Sciences.Selected, Subjects.Specials.Selected, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "B", Subjects.Languages.Selected},
            { Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.CellName.CreativeSolve, Subjects.Socials.Selected, null, null, null}
        };
        }
    }
}
