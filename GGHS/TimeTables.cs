#nullable disable

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

            /*
            public static ref string Of(this string[,] @class, DayOfWeek day, int time)
            {
                switch (time)
                {
                    case var _ when @class != Class1 && @class != Class2 && @class != Class3 && @class != Class4 && @class != Class5 && @class != Class6 && @class != Class7 && @class != Class8:
                        throw new ArgumentException("Only accepts TimeTables' member array");
                    case var _ when day is Saturday or Sunday:
                        throw new ArgumentException("No class in weekend!");
                    case var _ when time is <= 0 or > 7:
                        throw new ArgumentException("Please give 1~7 value");
                    default:
                        return ref @class[(int)day - 1, time - 1];
                }
            }*/


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
                        Class4.Of(Monday, 1) = Class4.Of(Tuesday, 2) = Class4.Of(Friday, 1) = Subjects.Languages.Selected = Subjects.Languages.Chinese;
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
                        Class6.Of(Tuesday, 4) = Class6.Of(Wednesday, 4) = Class6.Of(Thursday, 4) = Subjects.Languages.Selected = Subjects.Languages.Spanish;
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

            public string[,] Class1 { get; } = new string[5, 7]
            {
                { Subjects.Sciences.Selected, Subjects.Languages.Selected, Subjects.CellName.Reading, Subjects.Specials2.Selected, Subjects.CellName.Mathematics, Subjects.CellName.MathResearch, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Sport, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.CreativeSolve, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "B", Subjects.CellName.Mathematics },
                { Subjects.CellName.Reading, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Others },
                { Subjects.Specials1.Selected, Subjects.Sciences.Selected, Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Sport, Subjects.CellName.Sport },
                { Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.AdvancedEnglish + "C", Subjects.Languages.Selected, Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish, Subjects.Languages.Selected, Subjects.CellName.HomeComing },
            };

            public string[,] Class2 { get; } = new string[5, 7]
            {
                { Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.Specials2.Selected, Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.AdvancedEnglish + "B", Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Sport, Subjects.CellName.Sport },
                { Subjects.CellName.MathResearch, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish, Subjects.CellName.CreativeSolve, Subjects.CellName.Others },
                { Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish + "C", Subjects.CellName.Mathematics, Subjects.CellName.Reading, Subjects.CellName.Others, Subjects.Sciences.Selected, Subjects.CellName.AdvancedEnglish + "A" },
                { Subjects.CellName.Mathematics, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "A", Subjects.Specials1.Selected, Subjects.CellName.Sport, Subjects.Sciences.Selected, Subjects.CellName.HomeComing },
            };

            public string[,] Class3 { get; } = new string[5, 7]
            {
                { Subjects.CellName.Sport, Subjects.CellName.Sport, Subjects.Languages.Selected, Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.AdvancedEnglish + "D", Subjects.Languages.Selected, Subjects.CellName.Mathematics },
                { Subjects.CellName.AdvancedEnglish + "C", Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.MathResearch, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Others },
                { Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "B", Subjects.CellName.Others, Subjects.Sciences.Selected, Subjects.CellName.Reading },
                { Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.CellName.Sport, Subjects.Specials1.Selected, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.CellName.HomeComing },
            };

            public string[,] Class4 { get; } = new string[5, 7]
            {
                { Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.AdvancedEnglish + "B", Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "C", Subjects.CellName.MathResearch, Subjects.CellName.AdvancedEnglish + "A"  },
                { Subjects.CellName.Reading, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Sport, Subjects.CellName.Others },
                { Subjects.Specials1.Selected, Subjects.CellName.Sport, Subjects.Sciences.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Others, Subjects.CellName.Reading, Subjects.CellName.CreativeSolve },
                { Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.Specials1.Selected, Subjects.CellName.MathResearch, Subjects.CellName.Sport, Subjects.CellName.HomeComing },
            };

            public string[,] Class5 { get; } = new string[5, 7]
            {
                { Subjects.CellName.AdvancedEnglish + "C", Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading, Subjects.Specials2.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Sport, Subjects.CellName.MathResearch, Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.CellName.CreativeSolve, Subjects.CellName.AdvancedEnglish + "A" },
                { Subjects.Languages.Selected, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish + "B", Subjects.CellName.AdvancedEnglish + "A", Subjects.Languages.Selected, Subjects.CellName.Mathematics },
                { Subjects.Specials1.Selected, Subjects.CellName.Reading, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.Sport, Subjects.CellName.Sport },
                { Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.Reading, Subjects.Specials1.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.Mathematics, Subjects.CellName.HomeComing },
            };

            public string[,] Class6 { get; } = new string[5, 7]
            {
                { Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish  + "D", Subjects.Sciences.Selected, Subjects.Specials2.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Sport, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Mathematics, Subjects.CellName.AdvancedEnglish + "A", Subjects.Languages.Selected, Subjects.CellName.Others, Subjects.CellName.Sport, Subjects.CellName.AdvancedEnglish },
                { Subjects.CellName.Mathematics, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.Languages.Selected, Subjects.CellName.Reading, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "C" },
                { Subjects.Specials1.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CreativeSolve, Subjects.Languages.Selected, Subjects.CellName.MathResearch, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading },
                { Subjects.Sciences.Selected, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish, Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish + "B", Subjects.Specials1.Selected, Subjects.CellName.HomeComing },
            };

            public string[,] Class7 { get; } = new string[5, 7]
            {
                { Subjects.CellName.Sport, Subjects.CellName.Sport, Subjects.CellName.Mathematics, Subjects.Specials2.Selected, Subjects.Languages.Selected, Subjects.CellName.MathResearch, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.AdvancedEnglish + "D", Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading },
                { Subjects.CellName.AdvancedEnglish + "A", Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.Sciences.Selected, Subjects.Sciences.Selected, Subjects.CellName.AdvancedEnglish  + "B", Subjects.CellName.Reading },
                { Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Reading, Subjects.CellName.Others, Subjects.CellName.CreativeSolve, Subjects.CellName.AdvancedEnglish + "C"},
                { Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.CellName.Sport, Subjects.Specials1.Selected, Subjects.CellName.AdvancedEnglish, Subjects.CellName.Reading, Subjects.CellName.HomeComing },
            };

            public string[,] Class8 { get; } = new string[5, 7]
            {
                { Subjects.CellName.CreativeSolve, Subjects.CellName.Mathematics, Subjects.Languages.Selected, Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.Reading, Subjects.CellName.Others },
                { Subjects.Specials2.Selected, Subjects.CellName.Reading, Subjects.CellName.AdvancedEnglish + "B", Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Others, Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish },
                { Subjects.CellName.MathResearch, Subjects.Specials2.Selected, Subjects.Specials1.Selected, Subjects.Sciences.Selected, Subjects.Sciences.Selected, Subjects.CellName.Sport, Subjects.CellName.Others },
                { Subjects.Specials1.Selected, Subjects.CellName.Sport, Subjects.Languages.Selected, Subjects.CellName.AdvancedEnglish + "D", Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.AdvancedEnglish + "C", Subjects.CellName.Mathematics },
                { Subjects.CellName.AdvancedEnglish + "A", Subjects.CellName.Reading, Subjects.CellName.Mathematics, Subjects.Specials1.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Sport, Subjects.CellName.HomeComing },
            };

            /*
        public static string[,] Class1 { get; } = new string[5, 7]
        {
        { Subjects.Socials.Selected, Subjects.Languages.Selected, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.CriticalEnglish + "C", null, null},
        { Subjects.Specials.Selected, Subjects.CellName.CreativeSolve, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "A", Subjects.Socials.Selected },
        { Subjects.CellName.CreativeSolve, Subjects.CellName.Mathematics, Subjects.CellName.MathResearch, Subjects.Socials.Selected, Subjects.CellName.Literature, Subjects.CellName.Sport, Subjects.Sciences.Selected },
        { Subjects.Languages.Selected, Subjects.CellName.Sport, Subjects.Specials.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Literature, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "D"},
        { Subjects.Languages.Selected, Subjects.Specials.Selected, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, null, null, null }
        };
        public string[,] Class2 { get; } = new string[5, 7]
        {
        { Subjects.Languages.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null },
        { Subjects.CellName.Literature, Subjects.Specials.Selected, Subjects.CellName.Sport, Subjects.CellName.CreativeSolve, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "D" },
        { Subjects.Sciences.Selected, Subjects.CellName.Sport, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.MathResearch, Subjects.Languages.Selected, Subjects.Specials.Selected },
        { Subjects.CellName.Literature, Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish + "C", Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.Languages.Selected, Subjects.Socials.Selected},
        { Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.Socials.Selected, Subjects.CellName.CriticalEnglish + "A", null, null, null }
        };
        public string[,] Class3 { get; } = new string[5, 7]
        {
        { Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.Sport,  Subjects.Specials.Selected, null, null},
        { Subjects.CellName.CriticalEnglish + "D",  Subjects.CellName.Literature,  Subjects.CellName.Mathematics,  Subjects.Socials.Selected,  Subjects.CellName.CriticalEnglish + "B",  Subjects.Specials.Selected, Subjects.Languages.Selected},
        { Subjects.CellName.Sport, Subjects.CellName.MathResearch, Subjects.CellName.CriticalEnglish+"A", Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.Literature, Subjects.CellName.Mathematics },
        { Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.Languages.Selected},
        { Subjects.CellName.CriticalEnglish + "C", Subjects.CellName.CreativeSolve, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null, null }
        };
        public string[,] Class4 { get; } = new string[5, 7]
        {
        { Subjects.CellName.CreativeSolve, Subjects.Specials.Selected, Subjects.Sciences.Selected, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null },
        { Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Literature, Subjects.CellName.Sport, Subjects.CellName.CriticalEnglish + "C", Subjects.Specials.Selected },
        { Subjects.CellName.CriticalEnglish +"A", Subjects.CellName.CreativeSolve, Subjects.CellName.MathResearch, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Specials.Selected, Subjects.CellName.Sport },
        { Subjects.CellName.CriticalEnglish+"A", Subjects.CellName.CriticalEnglish+"D", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.Socials.Selected},
        { Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.Socials.Selected, Subjects.CellName.Mathematics, null, null, null }
        };
        public string[,] Class5 { get; } = new string[5, 7]
{
        { Subjects.CellName.CriticalEnglish + "C", Subjects.Sciences.Selected, Subjects.Specials.Selected, Subjects.Languages.Selected, Subjects.CellName.Mathematics, null, null },
        { Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Socials.Selected, Subjects.CellName.CriticalEnglish + "D", Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.CreativeSolve},
        { Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.CellName.MathResearch, Subjects.CellName.CriticalEnglish + "A", Subjects.Sciences.Selected, Subjects.CellName.Sport, Subjects.CellName.Mathematics },
        { Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Sport, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.Specials.Selected},
        { Subjects.Specials.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null, null }
};
        public string[,] Class6 { get; } = new string[5, 7]
        {
        { Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Literature, Subjects.CellName.CreativeSolve,  Subjects.Socials.Selected,  Subjects.CellName.Others,  Subjects.CellName.Others },
        { Subjects.CellName.CriticalEnglish + "C",  Subjects.Specials.Selected,  Subjects.CellName.Sport,  Subjects.Languages.Selected,  Subjects.CellName.Mathematics,  Subjects.CellName.Literature, Subjects.Sciences.Selected},
        { Subjects.CellName.MathResearch, Subjects.CellName.Sport, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.Specials.Selected, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "D" },
        { Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Mathematics, Subjects.Socials.Selected},
        { Subjects.CellName.CriticalEnglish + "A", Subjects.Languages.Selected, Subjects.Socials.Selected, Subjects.Specials.Selected, null, null, null }
        };
        public string[,] Class7 { get; } = new string[5, 7]
        {
        { Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.CellName.Sport, Subjects.Sciences.Selected, null, null },
        { Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish+"D", Subjects.CellName.CriticalEnglish + "C", Subjects.Socials.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.MathResearch, Subjects.CellName.Literature },
        { Subjects.CellName.Sport, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Literature, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.CellName.CreativeSolve },
        { Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.Socials.Selected, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Specials.Selected},
        { Subjects.Specials.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null, null }
        };
        public string[,] Class8 { get; } = new string[5, 7] {
        { Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "D", Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.Sciences.Selected, null, null },
        { Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.CellName.Sport, Subjects.Specials.Selected, Subjects.Languages.Selected },
        { Subjects.CellName.Literature, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "C", Subjects.Languages.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.MathResearch, Subjects.CellName.Sport},
        { Subjects.Sciences.Selected, Subjects.Specials.Selected, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "B", Subjects.Languages.Selected},
        { Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.CellName.CreativeSolve, Subjects.Socials.Selected, null, null, null}
        }; 
            */
        }
    }
namespace Grade2.Semester1
{
    public static class TimeTables
    {
        static TimeTables()
        {
            foreach (var item in Classes)
            {
                item.Of(Monday, 6) = item.Of(Monday, 7) = item.Of(Friday, 5) = item.Of(Friday, 6) = Subjects.CellName.Others;
                item.Of(Friday, 7) = Subjects.CellName.HomeComing;
            }
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
                // if (@class != Class1 && @class != Class2 && @class != Class3 && @class != Class4 && @class != Class5 && @class != Class6 && @class != Class7 && @class != Class8)
                throw new ArgumentException("Only accepts TimeTables' member array");
            if (day is Saturday or Sunday)
                throw new ArgumentException("No class in weekend!");
            if (time is <= 0 or > 7)
                throw new ArgumentException("Please give 1~7 value");
            return ref @class[(int)day - 1, time - 1];
        }

        // 모든 선택과목 업데이트
        public static void ResetByClass(int @class)
        {
            switch (@class)
            {
                case 1:
                    Class1.Of(Tuesday, 1) = Class1.Of(Thursday, 3) = Class1.Of(Friday, 2) = Subjects.Specials.Selected = Subjects.Specials.Ethics;
                    Class1.Of(Monday, 1) = Class1.Of(Tuesday, 7) = Class1.Of(Wednesday, 4) = Subjects.Socials.Selected = Subjects.Socials.Politics;
                    Class1.Of(Monday, 2) = Class1.Of(Thursday, 1) = Class1.Of(Friday, 1) = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                    Class1.Of(Tuesday, 3) = Class1.Of(Wednesday, 7) = Class1.Of(Friday, 4) = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                    break;
                case 2:
                    Class2.Of(Tuesday, 2) = Class2.Of(Wednesday, 7) = Class2.Of(Thursday, 5) = Subjects.Specials.Selected = Subjects.Specials.Ethics;
                    Class2.Of(Monday, 5) = Class2.Of(Thursday, 7) = Class2.Of(Friday, 3) = Subjects.Socials.Selected;
                    Class2.Of(Monday, 1) = Class2.Of(Wednesday, 6) = Class2.Of(Thursday, 6) = Subjects.Languages.Selected;
                    Class2.Of(Tuesday, 5) = Class2.Of(Wednesday, 1) = Class2.Of(Friday, 1) = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                    break;
                case 3:
                    Class3.Of(Monday, 5) = Class3.Of(Tuesday, 6) = Class3.Of(Thursday, 2) = Subjects.Specials.Selected = Subjects.Specials.Ethics;
                    Class3.Of(Tuesday, 4) = Class3.Of(Thursday, 4) = Class3.Of(Friday, 4) = Subjects.Socials.Selected;
                    Class3.Of(Tuesday, 7) = Class3.Of(Wednesday, 4) = Class3.Of(Thursday, 7) = Subjects.Languages.Selected;
                    Class3.Of(Monday, 2) = Class3.Of(Wednesday, 5) = Class3.Of(Thursday, 5) = Subjects.Sciences.Selected;
                    break;
                case 4:
                    Class4.Of(Monday, 2) = Class4.Of(Tuesday, 7) = Class4.Of(Wednesday, 6) = Subjects.Specials.Selected = Subjects.Specials.Ethics;
                    Class4.Of(Monday, 5) = Class4.Of(Thursday, 7) = Class4.Of(Friday, 3) = Subjects.Socials.Selected;
                    Class4.Of(Tuesday, 1) = Class4.Of(Thursday, 5) = Class4.Of(Friday, 1) = Subjects.Languages.Selected = Subjects.Languages.Chinese;
                    Class4.Of(Monday, 3) = Class4.Of(Thursday, 6) = Class4.Of(Friday, 2) = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                    break;
                case 5:
                    Class5.Of(Monday, 3) = Class5.Of(Thursday, 7) = Class5.Of(Friday, 1) = Subjects.Specials.Selected;
                    Class5.Of(Tuesday, 4) = Class5.Of(Thursday, 4) = Class5.Of(Friday, 4) = Subjects.Socials.Selected;
                    Class5.Of(Monday, 4) = Class5.Of(Tuesday, 3) = Class5.Of(Wednesday, 2) = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                    Class5.Of(Monday, 2) = Class5.Of(Wednesday, 5) = Class5.Of(Thursday, 5) = Subjects.Sciences.Selected;
                    break;
                case 6:
                    Class6.Of(Tuesday, 2) = Class6.Of(Wednesday, 5) = Class6.Of(Friday, 4) = Subjects.Specials.Selected = Subjects.Specials.Environment;
                    Class6.Of(Monday, 5) = Class6.Of(Thursday, 7) = Class6.Of(Friday, 3) = Subjects.Socials.Selected;
                    Class6.Of(Tuesday, 4) = Class6.Of(Thursday, 2) = Class6.Of(Friday, 2) = Subjects.Languages.Selected = Subjects.Languages.Spanish;
                    Class6.Of(Tuesday, 7) = Class6.Of(Wednesday, 4) = Class6.Of(Thursday, 3) = Subjects.Sciences.Selected = Subjects.Sciences.Biology;
                    break;
                case 7:
                    Class7.Of(Monday, 3) = Class7.Of(Thursday, 7) = Class7.Of(Friday, 1) = Subjects.Specials.Selected;
                    Class7.Of(Tuesday, 4) = Class7.Of(Thursday, 4) = Class7.Of(Friday, 4) = Subjects.Socials.Selected;
                    Class7.Of(Monday, 1) = Class7.Of(Wednesday, 6) = Class7.Of(Thursday, 6) = Subjects.Languages.Selected;
                    Class7.Of(Monday, 5) = Class7.Of(Wednesday, 2) = Class7.Of(Thursday, 1) = Subjects.Sciences.Selected;
                    break;
                case 8:
                    Class8.Of(Tuesday, 6) = Class8.Of(Thursday, 2) = Class8.Of(Friday, 2) = Subjects.Specials.Selected = Subjects.Specials.Environment;
                    Class8.Of(Tuesday, 4) = Class8.Of(Thursday, 4) = Class8.Of(Friday, 4) = Subjects.Socials.Selected;
                    Class8.Of(Tuesday, 7) = Class8.Of(Wednesday, 4) = Class8.Of(Thursday, 7) = Subjects.Languages.Selected;
                    Class8.Of(Monday, 5) = Class8.Of(Wednesday, 2) = Class8.Of(Thursday, 1) = Subjects.Sciences.Selected;
                    break;
            }
        }

        public static IEnumerable<string[,]> Classes
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

        public static string[,] Class1 { get; } = new string[5, 7]
        {
        { Subjects.Socials.Selected, Subjects.Languages.Selected, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.CriticalEnglish + "C", null, null},
        { Subjects.Specials.Selected, Subjects.CellName.CreativeSolve, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "A", Subjects.Socials.Selected },
        { Subjects.CellName.CreativeSolve, Subjects.CellName.Mathematics, Subjects.CellName.MathResearch, Subjects.Socials.Selected, Subjects.CellName.Literature, Subjects.CellName.Sport, Subjects.Sciences.Selected },
        { Subjects.Languages.Selected, Subjects.CellName.Sport, Subjects.Specials.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Literature, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "D"},
        { Subjects.Languages.Selected, Subjects.Specials.Selected, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, null, null, null }
        };
        public static string[,] Class2 { get; } = new string[5, 7]
        {
        { Subjects.Languages.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null },
        { Subjects.CellName.Literature, Subjects.Specials.Selected, Subjects.CellName.Sport, Subjects.CellName.CreativeSolve, Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "D" },
        { Subjects.Sciences.Selected, Subjects.CellName.Sport, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.MathResearch, Subjects.Languages.Selected, Subjects.Specials.Selected },
        { Subjects.CellName.Literature, Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish + "C", Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.Languages.Selected, Subjects.Socials.Selected},
        { Subjects.Sciences.Selected, Subjects.CellName.Mathematics, Subjects.Socials.Selected, Subjects.CellName.CriticalEnglish + "A", null, null, null }
        };
        public static string[,] Class3 { get; } = new string[5, 7]
        {
        { Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.Sport,  Subjects.Specials.Selected, null, null},
        { Subjects.CellName.CriticalEnglish + "D",  Subjects.CellName.Literature,  Subjects.CellName.Mathematics,  Subjects.Socials.Selected,  Subjects.CellName.CriticalEnglish + "B",  Subjects.Specials.Selected, Subjects.Languages.Selected},
        { Subjects.CellName.Sport, Subjects.CellName.MathResearch, Subjects.CellName.CriticalEnglish+"A", Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.Literature, Subjects.CellName.Mathematics },
        { Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.Languages.Selected},
        { Subjects.CellName.CriticalEnglish + "C", Subjects.CellName.CreativeSolve, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null, null }
        };
        public static string[,] Class4 { get; } = new string[5, 7]
        {
        { Subjects.CellName.CreativeSolve, Subjects.Specials.Selected, Subjects.Sciences.Selected, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null },
        { Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Literature, Subjects.CellName.Sport, Subjects.CellName.CriticalEnglish + "C", Subjects.Specials.Selected },
        { Subjects.CellName.CriticalEnglish +"A", Subjects.CellName.CreativeSolve, Subjects.CellName.MathResearch, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Specials.Selected, Subjects.CellName.Sport },
        { Subjects.CellName.CriticalEnglish+"A", Subjects.CellName.CriticalEnglish+"D", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.Socials.Selected},
        { Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.Socials.Selected, Subjects.CellName.Mathematics, null, null, null }
        };
        public static string[,] Class5 { get; } = new string[5, 7]
{
        { Subjects.CellName.CriticalEnglish + "C", Subjects.Sciences.Selected, Subjects.Specials.Selected, Subjects.Languages.Selected, Subjects.CellName.Mathematics, null, null },
        { Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Socials.Selected, Subjects.CellName.CriticalEnglish + "D", Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.CreativeSolve},
        { Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.CellName.MathResearch, Subjects.CellName.CriticalEnglish + "A", Subjects.Sciences.Selected, Subjects.CellName.Sport, Subjects.CellName.Mathematics },
        { Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Sport, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.Specials.Selected},
        { Subjects.Specials.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null, null }
};
        public static string[,] Class6 { get; } = new string[5, 7]
        {
        { Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Literature, Subjects.CellName.CreativeSolve,  Subjects.Socials.Selected,  Subjects.CellName.Others,  Subjects.CellName.Others },
        { Subjects.CellName.CriticalEnglish + "C",  Subjects.Specials.Selected,  Subjects.CellName.Sport,  Subjects.Languages.Selected,  Subjects.CellName.Mathematics,  Subjects.CellName.Literature, Subjects.Sciences.Selected},
        { Subjects.CellName.MathResearch, Subjects.CellName.Sport, Subjects.CellName.Mathematics, Subjects.Sciences.Selected, Subjects.Specials.Selected, Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "D" },
        { Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Sciences.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Mathematics, Subjects.Socials.Selected},
        { Subjects.CellName.CriticalEnglish + "A", Subjects.Languages.Selected, Subjects.Socials.Selected, Subjects.Specials.Selected, null, null, null }
        };
        public static string[,] Class7 { get; } = new string[5, 7]
        {
        { Subjects.Languages.Selected, Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.CellName.Sport, Subjects.Sciences.Selected, null, null },
        { Subjects.CellName.CreativeSolve, Subjects.CellName.CriticalEnglish+"D", Subjects.CellName.CriticalEnglish + "C", Subjects.Socials.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.MathResearch, Subjects.CellName.Literature },
        { Subjects.CellName.Sport, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "B", Subjects.CellName.Literature, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.CellName.CreativeSolve },
        { Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.Socials.Selected, Subjects.CellName.Literature, Subjects.Languages.Selected, Subjects.Specials.Selected},
        { Subjects.Specials.Selected, Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, null, null, null }
        };
        public static string[,] Class8 { get; } = new string[5, 7] {
        { Subjects.CellName.Literature, Subjects.CellName.CriticalEnglish + "D", Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.Sciences.Selected, null, null },
        { Subjects.CellName.CriticalEnglish + "A", Subjects.CellName.Mathematics, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.CellName.Sport, Subjects.Specials.Selected, Subjects.Languages.Selected },
        { Subjects.CellName.Literature, Subjects.Sciences.Selected, Subjects.CellName.CriticalEnglish + "C", Subjects.Languages.Selected, Subjects.CellName.CreativeSolve, Subjects.CellName.MathResearch, Subjects.CellName.Sport},
        { Subjects.Sciences.Selected, Subjects.Specials.Selected, Subjects.CellName.Literature, Subjects.Socials.Selected, Subjects.CellName.Mathematics, Subjects.CellName.CriticalEnglish + "B", Subjects.Languages.Selected},
        { Subjects.CellName.Mathematics, Subjects.Specials.Selected, Subjects.CellName.CreativeSolve, Subjects.Socials.Selected, null, null, null}
    };
    }
}
}
