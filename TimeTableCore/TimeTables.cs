using System;
using System.Collections.Generic;
using TimeTableCore.Grade3.Semester2;

namespace TimeTableCore;

[Flags]
public enum Common
{
    None = 0,
    Social = 1,
    Language = 2,
    Global1 = 4,
    Global2 = 8,
}

public class TimeTable
{
    public Subject[] Data { get; set; }
    public int Class { get; }

    /// <summary>
    /// Use | operator to combine.
    /// </summary>
    public Common CommonSubject { get; private set; } = Common.None; 
    public TimeTable(int @class, Subject[] timeTable)
    {
        Data = timeTable;
        Class = @class;

        SetByClass(@class);
    }

    public TimeTable(int @class, List<Subject> timeTable)
    {
        Data = timeTable.ToArray();
        Class = @class;

        SetByClass(@class);
    }

    public ref readonly Subject AtPos(int day, int time) => ref Data[day * 7 + time];

    private void SetByClass(int @class)
    {
        CommonSubject = @class switch
        {
            1 or 2 => Common.Global1,
            3 => Common.None,
            4 or 5 => Common.Social,
            6 => Common.Social | Common.Language,
            7 or 8 => Common.Social | Common.Language | Common.Global1 | Common.Global2,
            _ => throw new IndexOutOfRangeException(@"SetByClass: @class is {@class}")
        };
    }
}

public class TimeTables
{
    public TimeTable Class1 => new(1, new Subject[]
{
        Subjects.Social, Subjects.AdvancedEnglish.A, Subjects.Global2.B, Subjects.TraditionalArt, Subjects.LogicalWriting, Subjects.Others, Subjects.Others,
        Subjects.GlobalStatistics.A, Subjects.Global2.A, Subjects.Reading, Subjects.AdvancedEnglish.A, Subjects.TraditionalArt, Subjects.LogicalWriting, Subjects.Global1,
        Subjects.Reading, Subjects.Global2.A, Subjects.Global2.B, Subjects.Language.A, Subjects.AdvancedEnglish.B, Subjects.Global1, Subjects.GlobalStatistics.A,
        Subjects.Language.B, Subjects.Global1, Subjects.LogicalWriting, Subjects.TraditionalArt, Subjects.AdvancedEnglish.A, Subjects.GlobalStatistics.B, Subjects.Sports,
        Subjects.AdvancedEnglish.B, Subjects.Global1, Subjects.Global2.A, Subjects.GlobalStatistics.A, Subjects.Others, Subjects.Others,
});
    public TimeTable Class2 => new(2, new Subject[]
    {
        Subjects.Social, Subjects.LogicalWriting, Subjects.Global2.B, Subjects.Sports, Subjects.AdvancedEnglish.A, Subjects.Others, Subjects.Others,
        Subjects.AdvancedEnglish.A, Subjects.Global2.A, Subjects.Global1.B, Subjects.AdvancedEnglish.B, Subjects.TraditionalArt, Subjects.Global1.A, Subjects.Reading,
        Subjects.TraditionalArt, Subjects.Global2.A, Subjects.Global2.B, Subjects.Language.A, Subjects.GlobalStatistics.A, Subjects.Reading, Subjects.LogicalWriting,
        Subjects.Language.B, Subjects.GlobalStatistics.B, Subjects.Global1.A, Subjects.GlobalStatistics.A, Subjects.TraditionalArt, Subjects.AdvancedEnglish.A, Subjects.AdvancedEnglish.B,
        Subjects.GlobalStatistics.A, Subjects.Global1.A, Subjects.Global2.A, Subjects.LogicalWriting, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class3 => new(3, new Subject[]
    {
        Subjects.Social, Subjects.GlobalStatistics.A, Subjects.Reading, Subjects.AdvancedEnglish.B, Subjects.Global2.A, Subjects.Others, Subjects.Others,
        Subjects.TraditionalArt, Subjects.LogicalWriting, Subjects.Global2.B, Subjects.Global1.A, Subjects.GlobalStatistics.A, Subjects.AdvancedEnglish.B, Subjects.AdvancedEnglish.A,
        Subjects.Global1.A, Subjects.GlobalStatistics.B, Subjects.TraditionalArt, Subjects.Language.A, Subjects.AdvancedEnglish.A, Subjects.Global2.B, Subjects.LogicalWriting,
        Subjects.Language.B, Subjects.Global1.A, Subjects.LogicalWriting, Subjects.TraditionalArt, Subjects.Sports, Subjects.GlobalStatistics.A, Subjects.Global2.A,
        Subjects.Global1.B, Subjects.Global2.A, Subjects.AdvancedEnglish.A, Subjects.Reading, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class4 => new(4, new Subject[]
    {
        Subjects.TraditionalArt, Subjects.Language.A, Subjects.LogicalWriting, Subjects.GlobalStatistics.A, Subjects.Global2.A, Subjects.Others, Subjects.Others,
        Subjects.GlobalStatistics.B, Subjects.AdvancedEnglish.B, Subjects.Global2.B, Subjects.Global1, Subjects.Social, Subjects.TraditionalArt, Subjects.GlobalStatistics.A,
        Subjects.Global1, Subjects.AdvancedEnglish.B, Subjects.AdvancedEnglish.A, Subjects.Reading, Subjects.TraditionalArt, Subjects.Global2.B, Subjects.GlobalStatistics.A,
        Subjects.AdvancedEnglish.A, Subjects.Global1, Subjects.Sports, Subjects.Language.B, Subjects.Reading, Subjects.LogicalWriting, Subjects.Global2.A,
        Subjects.Global1, Subjects.Global2.A, Subjects.LogicalWriting, Subjects.AdvancedEnglish.A, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class5 => new(5, new Subject[]
    {
        Subjects.LogicalWriting, Subjects.Language.A, Subjects.AdvancedEnglish.A, Subjects.Reading, Subjects.Global1, Subjects.Others, Subjects.Others,
        Subjects.AdvancedEnglish.B, Subjects.LogicalWriting, Subjects.Global2.B, Subjects.AdvancedEnglish.A, Subjects.Reading, Subjects.Global1, Subjects.Global2.A,
        Subjects.TraditionalArt, Subjects.GlobalStatistics.A, Subjects.Global1, Subjects.Social, Subjects.Global2.A, Subjects.GlobalStatistics.B, Subjects.LogicalWriting,
        Subjects.GlobalStatistics.A, Subjects.TraditionalArt, Subjects.AdvancedEnglish.A, Subjects.Language.B, Subjects.Global1, Subjects.Global2.A, Subjects.Sports,
        Subjects.Global2.B, Subjects.AdvancedEnglish.B, Subjects.GlobalStatistics.A, Subjects.TraditionalArt, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class6 => new(6, new Subject[]
    {
        Subjects.Reading, Subjects.Language.B, Subjects.GlobalStatistics.A, Subjects.Sports, Subjects.Global1.A, Subjects.Others, Subjects.Others,
        Subjects.Social, Subjects.TraditionalArt, Subjects.Global2.B, Subjects.GlobalStatistics.A, Subjects.LogicalWriting, Subjects.Global1.B, Subjects.Global2.A,
        Subjects.GlobalStatistics.B, Subjects.TraditionalArt, Subjects.Global1.A, Subjects.AdvancedEnglish.A, Subjects.Global2.A, Subjects.AdvancedEnglish.B, Subjects.LogicalWriting,
        Subjects.Reading, Subjects.AdvancedEnglish.A, Subjects.AdvancedEnglish.B, Subjects.Language.A, Subjects.Global1.A, Subjects.Global2.A, Subjects.LogicalWriting,
        Subjects.Global2.B, Subjects.TraditionalArt, Subjects.GlobalStatistics.A, Subjects.AdvancedEnglish.A, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class7 => new(7, new Subject[]
    {
        Subjects.GlobalStatistics.A, Subjects.TraditionalArt, Subjects.LogicalWriting, Subjects.AdvancedEnglish.B, Subjects.Global2.B, Subjects.Others, Subjects.Others,
        Subjects.Language.B, Subjects.TraditionalArt, Subjects.AdvancedEnglish.A, Subjects.GlobalStatistics.B, Subjects.Global2.A, Subjects.AdvancedEnglish.B, Subjects.Global1,
        Subjects.LogicalWriting, Subjects.Language.A, Subjects.Social, Subjects.Reading, Subjects.GlobalStatistics.A, Subjects.AdvancedEnglish.A, Subjects.Global1,
        Subjects.TraditionalArt, Subjects.Global1, Subjects.LogicalWriting, Subjects.Global2.A, Subjects.Sports, Subjects.Global2.B, Subjects.GlobalStatistics.A,
        Subjects.AdvancedEnglish.A, Subjects.Global2.A, Subjects.Global1, Subjects.Reading, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class8 => new(8, new Subject[]
    {
        Subjects.LogicalWriting, Subjects.Global1, Subjects.Global2.A, Subjects.GlobalStatistics.B, Subjects.AdvancedEnglish.A, Subjects.Others, Subjects.Others,
        Subjects.TraditionalArt, Subjects.GlobalStatistics.A, Subjects.Global1, Subjects.Global2.A, Subjects.Language.B, Subjects.Reading, Subjects.AdvancedEnglish.A,
        Subjects.AdvancedEnglish.A, Subjects.GlobalStatistics.A, Subjects.LogicalWriting, Subjects.AdvancedEnglish.B, Subjects.Global1, Subjects.Global2.A, Subjects.TraditionalArt,
        Subjects.LogicalWriting, Subjects.Reading, Subjects.Sports, Subjects.AdvancedEnglish.B, Subjects.GlobalStatistics.A, Subjects.Global1, Subjects.Global2.B,
        Subjects.Language.A, Subjects.TraditionalArt, Subjects.Global2.B, Subjects.Social, Subjects.Others, Subjects.Others,
    });

    private static TimeTable? table;
    public TimeTable? Table { get => table; set => table = value; }

    public TimeTables() { }
    public TimeTables(int @class)
    {
        ResetClass(@class);
    }

    public void ResetClass(int @class)
    {
        switch (@class)
        {
            case 1:
                Global1.Selected = Global1.SocialResearch;
                break;
            case 2:
                Global1.Selected = Global1.KoreanSociety;
                break;
            case 7: // 사탐방, 세문미
                Global1.Selected = Global1.SocialResearch;
                Global2.Selected = Global2.FutureSociety;
                goto case 6;
            case 8: // 윤연, 세문미
                Global1.Selected = Global1.SocialResearch;
                Global2.Selected = Global2.Ethics;
                goto case 6;
            case 6: // (+) 스문
                Language.Selected = Language.Spanish;
                goto case 4;
                
            case 4:
            case 5:  // (+) 사문
                Social.Selected = Social.Culture;
                break;
        }

        Table = @class switch
        {
            1 => Class1,
            2 => Class2,
            3 => Class3,
            4 => Class4,
            5 => Class5,
            6 => Class6,
            7 => Class7,
            8 => Class8,
            _ => throw new ArgumentException($"@class is not in 1-8. @class = {@class}")
        };
    }
}