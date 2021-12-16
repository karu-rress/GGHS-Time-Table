#nullable disable

using System;
using System.Collections.Generic;
using static System.DayOfWeek;

namespace TimeTableUWP
{
    [Obsolete("=> TimeTableCore.DateType")]
    public enum DateType
    {
        YYYYMMDD,
        MMDDYYYY,
        YYYYMMDD2
    }
}
internal static class StringOfExtension
{
    public static ref string Of(this string[,] @class, DayOfWeek day, int time) => ref @class[(int)day - 1, time - 1];
}

namespace GGHS
{
    public interface ITimeTable
    {
        string[,] Class1 { get; }
        string[,] Class2 { get; }
        string[,] Class3 { get; }
        string[,] Class4 { get; }
        string[,] Class5 { get; }
        string[,] Class6 { get; }
        string[,] Class7 { get; }
        string[,] Class8 { get; }

        /// <summary>
        /// Initialize default subjects -- Others, Homecoming. This methods should be called in ctor.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Assign subjects if all students of class learn the same subject.
        /// </summary>
        /// <param name="class">Class number</param>
        void ResetByClass(int @class);

        IEnumerable<string[,]> Classes { get; }
    }

    namespace Grade2.Semester2
    {
        public class TimeTables : ITimeTable
        {
            public TimeTables()
            {
                Initialize();
            }

            public void Initialize()
            {
                foreach (var item in Classes)
                {
                    item.Of(Monday, 6) = item.Of(Monday, 7) = item.Of(Friday, 5) = item.Of(Friday, 6) = Subjects.CellName.Others;
                    item.Of(Friday, 7) = Subjects.CellName.HomeComing;
                }
            }

            public IEnumerable<string[,]> Classes
            {
                get
                {
                    yield return Class1;
                    yield return Class2;
                    yield return Class3;
                    yield return Class4;
                    yield return Class5;
                    yield return Class6;
                    yield return Class7;
                    yield return Class8;
                }
            }

            // 각 반에서 공통으로 듣는 선택과목이 있을 경우 
            public void ResetByClass(int @class) // ITimeTable.ResetByClass(int)
            {
                switch (@class)
                {
                    case 1:
                        Class1.Of(Monday, 2) = Class1.Of(Wednesday, 7) = Class1.Of(Friday, 3) = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                        Class1.Of(Wednesday, 3) = Class1.Of(Thursday, 1) = Class1.Of(Friday, 4) = Subjects.Specials1.Selected;
                        Class1.Of(Monday, 4) = Class1.Of(Tuesday, 1) = Class1.Of(Wednesday, 2) = Subjects.Specials2.Selected;
                        Class1.Of(Monday, 1) = Class1.Of(Thursday, 2) = Subjects.Sciences.Selected = Subjects.Sciences.LifeAndScience;
                        break;
                    case 2:
                        Class2.Of(Monday, 5) = Class2.Of(Tuesday, 3) = Class2.Of(Thursday, 6) = Subjects.Languages.Selected;
                        Class2.Of(Wednesday, 3) = Class2.Of(Thursday, 1) = Class2.Of(Friday, 4) = Subjects.Specials1.Selected;
                        Class2.Of(Monday, 4) = Class2.Of(Tuesday, 1) = Class2.Of(Wednesday, 2) = Subjects.Specials2.Selected;
                        Class2.Of(Wednesday, 7) = Class2.Of(Thursday, 5) = Subjects.Sciences.Selected;
                        break;
                    case 3:
                        Class3.Of(Monday, 3) = Class3.Of(Tuesday, 7) = Class3.Of(Thursday, 3) = Subjects.Languages.Selected;
                        Class3.Of(Wednesday, 3) = Class3.Of(Thursday, 1) = Class3.Of(Friday, 4) = Subjects.Specials1.Selected;
                        Class3.Of(Monday, 4) = Class3.Of(Tuesday, 1) = Class3.Of(Wednesday, 2) = Subjects.Specials2.Selected;
                        Class3.Of(Wednesday, 7) = Class3.Of(Thursday, 5) = Subjects.Sciences.Selected;
                        break;
                    case 4:
                        Class4.Of(Monday, 1) = Class4.Of(Tuesday, 4) = Class4.Of(Friday, 1) = Subjects.Languages.Selected = Subjects.Languages.Chinese;
                        Class4.Of(Wednesday, 3) = Class4.Of(Thursday, 1) = Class4.Of(Friday, 4) = Subjects.Specials1.Selected;
                        Class4.Of(Monday, 4) = Class4.Of(Tuesday, 1) = Class4.Of(Wednesday, 2) = Subjects.Specials2.Selected;
                        Class4.Of(Thursday, 3) = Class4.Of(Friday, 2) = Subjects.Sciences.Selected;
                        break;
                    case 5:
                        Class5.Of(Wednesday, 1) = Class5.Of(Wednesday, 6) = Class5.Of(Friday, 1) = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                        Class5.Of(Wednesday, 3) = Class5.Of(Thursday, 1) = Class5.Of(Friday, 4) = Subjects.Specials1.Selected;
                        Class5.Of(Monday, 4) = Class5.Of(Tuesday, 1) = Class5.Of(Wednesday, 2) = Subjects.Specials2.Selected;
                        Class5.Of(Thursday, 3) = Class5.Of(Friday, 2) = Subjects.Sciences.Selected;
                        break;
                    case 6:
                        Class6.Of(Tuesday, 4) = Class6.Of(Wednesday, 4) = Class6.Of(Thursday, 2) = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                        Class6.Of(Wednesday, 3) = Class6.Of(Thursday, 1) = Class6.Of(Friday, 4) = Subjects.Specials1.Selected;
                        Class6.Of(Monday, 4) = Class6.Of(Tuesday, 1) = Class6.Of(Wednesday, 2) = Subjects.Specials2.Selected;
                        Class6.Of(Monday, 3) = Class6.Of(Friday, 1) = Subjects.Sciences.Selected = Subjects.Sciences.LifeAndScience;
                        break;
                    case 7:
                        Class7.Of(Monday, 5) = Class7.Of(Tuesday, 3) = Class7.Of(Thursday, 6) = Subjects.Languages.Selected;
                        Class7.Of(Wednesday, 3) = Class7.Of(Thursday, 1) = Class7.Of(Friday, 4) = Subjects.Specials1.Selected;
                        Class7.Of(Monday, 4) = Class7.Of(Tuesday, 1) = Class7.Of(Wednesday, 2) = Subjects.Specials2.Selected;
                        Class7.Of(Tuesday, 5) = Class7.Of(Wednesday, 4) = Subjects.Sciences.Selected;
                        break;
                    case 8:
                        Class8.Of(Monday, 3) = Class8.Of(Tuesday, 7) = Class8.Of(Thursday, 3) = Subjects.Languages.Selected;
                        Class8.Of(Wednesday, 3) = Class8.Of(Thursday, 1) = Class8.Of(Friday, 4) = Subjects.Specials1.Selected;
                        Class8.Of(Monday, 4) = Class8.Of(Tuesday, 1) = Class8.Of(Wednesday, 2) = Subjects.Specials2.Selected;
                        Class8.Of(Tuesday, 5) = Class8.Of(Wednesday, 4) = Subjects.Sciences.Selected;
                        break;
                }
            }

            public string[,] Class1 => new string[5, 7]
            {
                { Subjects.Sciences.Selected, Subjects.Languages.Selected, Subjects.CellName.Reading, Subjects.Specials2.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Others, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Sport, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.CreativeSolve, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "B", Subjects.CellName.Mathematics },
                { Subjects.CellName.Reading, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.Mathematics, Subjects.CellName.MathResearch, Subjects.CellName.AdvancedEnglish + "A", Subjects.Languages.Selected },
                { Subjects.Specials1.Selected, Subjects.Sciences.Selected, Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Sport, Subjects.CellName.Sport },
                { Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.AdvancedEnglish + "C", Subjects.Languages.Selected, Subjects.Specials1.Selected, Subjects.CellName.Others, Subjects.CellName.Others, Subjects.CellName.HomeComing},
            };

            public string[,] Class2 => new string[5, 7]
            {
                { Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.Specials2.Selected, Subjects.Languages.Selected, Subjects.CellName.Others, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.AdvancedEnglish + "B", Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Sport, Subjects.CellName.Sport },
                { Subjects.CellName.MathResearch, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.Reading, Subjects.CellName.Sport, Subjects.CellName.CreativeSolve, Subjects.Sciences.Selected },
                { Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish + "C", Subjects.CellName.Mathematics, Subjects.CellName.Reading, Subjects.Sciences.Selected, Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "A" },
                { Subjects.CellName.Mathematics, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "A", Subjects.Specials1.Selected, Subjects.CellName.Others, Subjects.CellName.Others, Subjects.CellName.HomeComing },
            };

            public string[,] Class3 => new string[5, 7]
            {
                { Subjects.CellName.Sport, Subjects.CellName.Sport, Subjects.Languages.Selected, Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.Others, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Mathematics, Subjects.Languages.Selected },
                { Subjects.CellName.AdvancedEnglish + "C", Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.MathResearch, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Mathematics, Subjects.Sciences.Selected },
                { Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "B", Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Reading },
                { Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.CellName.Sport, Subjects.Specials1.Selected, Subjects.CellName.Others, Subjects.CellName.Others, Subjects.CellName.HomeComing },
            };

            public string[,] Class4 => new string[5, 7]
            {
                { Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.AdvancedEnglish + "B", Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.Others, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "C", Subjects.CellName.MathResearch, Subjects.CellName.AdvancedEnglish + "A"  },
                { Subjects.CellName.Reading, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Sport, Subjects.CellName.Sport },
                { Subjects.Specials1.Selected, Subjects.CellName.Sport, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading, Subjects.CellName.CreativeSolve },
                { Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.Specials1.Selected, Subjects.CellName.Others, Subjects.CellName.Others, Subjects.CellName.HomeComing },
            };

            public string[,] Class5 => new string[5, 7]
            {
                { Subjects.CellName.AdvancedEnglish + "C", Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading, Subjects.Specials2.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Others, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Sport, Subjects.CellName.MathResearch, Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.CellName.CreativeSolve, Subjects.CellName.AdvancedEnglish + "A" },
                { Subjects.Languages.Selected, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish + "B", Subjects.CellName.AdvancedEnglish + "A", Subjects.Languages.Selected, Subjects.CellName.Mathematics },
                { Subjects.Specials1.Selected, Subjects.CellName.Reading, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Sport, Subjects.CellName.Sport },
                { Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.Reading, Subjects.Specials1.Selected, Subjects.CellName.Others, Subjects.CellName.Others, Subjects.CellName.HomeComing },
            };

            public string[,] Class6 => new string[5, 7]
            {
                { Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish  + "D", Subjects.Sciences.Selected, Subjects.Specials2.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Others, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "A", Subjects.Languages.Selected, Subjects.CellName.Reading, Subjects.CellName.Sport, Subjects.CellName.Sport },
                { Subjects.CellName.Mathematics, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.Languages.Selected, Subjects.CellName.Sport, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "C" },
                { Subjects.Specials1.Selected, Subjects.Languages.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.MathResearch, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading },
                { Subjects.Sciences.Selected, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "B", Subjects.Specials1.Selected, Subjects.CellName.Others, Subjects.CellName.Others, Subjects.CellName.HomeComing },
            };

            public string[,] Class7 => new string[5, 7]
            {
                { Subjects.CellName.Sport, Subjects.CellName.Sport, Subjects.CellName.Mathematics, Subjects.Specials2.Selected, Subjects.Languages.Selected, Subjects.CellName.Others, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.AdvancedEnglish + "D", Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading },
                { Subjects.CellName.AdvancedEnglish + "A", Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.Sciences.Selected, Subjects.CellName.MathResearch, Subjects.CellName.AdvancedEnglish  + "B", Subjects.CellName.Reading },
                { Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Reading, Subjects.CellName.CreativeSolve, Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "C"},
                { Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.CellName.Sport, Subjects.Specials1.Selected, Subjects.CellName.Others, Subjects.CellName.Others, Subjects.CellName.HomeComing },
            };

            public string[,] Class8 => new string[5, 7]
            {
                { Subjects.CellName.CreativeSolve, Subjects.CellName.Mathematics, Subjects.Languages.Selected, Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.Others, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "B", Subjects.CellName.AdvancedEnglish + "A", Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.Languages.Selected },
                { Subjects.CellName.MathResearch, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.Sciences.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Sport, Subjects.CellName.Sport },
                { Subjects.Specials1.Selected, Subjects.CellName.Sport, Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "C", Subjects.CellName.Mathematics },
                { Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.Specials1.Selected, Subjects.CellName.Others, Subjects.CellName.Others, Subjects.CellName.HomeComing },
            };
        }
    }
}