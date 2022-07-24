using System;
using System.Collections.Generic;
using TimeTableCore.Grade3.Semester2;

namespace TimeTableCore;
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
        // TODO: CommonSubject = switch @class {  Common.None , ..... 
        // 창체, 동아리 시간, 홈커밍은 여기서 잘라버리기

        CommonSubject = @class switch
        {
            1 => Common.Global1,
            2 => Common.Global1,
            3 => Common.Math,
            4 => Common.Korean | Common.Math | Common.Social, // 언매, 확통, 사문
            5 => Common.Korean | Common.Math | Common.Social, // 언매, 확통, 사문
            6 => Common.Korean | Common.Math | Common.Social | Common.Language, // 언매, 확통, 사문, 스문
            7 => Common.Korean | Common.Math | Common.Social | Common.Language 
            | Common.Global1 | Common.Global2, // 언매, 확통, 사문, 스문, 사탐방, 세문미
            8 => Common.Korean | Common.Math | Common.Social | Common.Language
            | Common.Global1 | Common.Global2, // 언매, 확통, 사문, 스문, 사탐방, 윤리
            _ => throw new IndexOutOfRangeException(@"SetByClass: @class is {@class}")
        };
    }
}

[Flags]
public enum Common
{
    None = 0,
    Korean = 1,
    Math = 2,
    Social = 4,
    Language = 8,
    Global1 = 16,
    Global2 = 32,
}

public class TimeTables
{
    public TimeTable Class1 { get; } = new(1, new Subject[]
    {
        Subjects.AdvancedEnglish.B, Subjects.TraditionalArt, Subjects.Sports, Subjects.GlobalStatistics, Subjects.AdvancedEnglish.A, Subjects.Others, Subjects.Others,
        Subjects.LogicalWriting, Subjects.TraditionalArt, Subjects.Reading, Subjects.Global2, Subjects.Language, Subjects.AdvancedEnglish.A, Global1.SocialResearch,
        Subjects.Sports, Subjects.Language, Subjects.GlobalStatistics, Subjects.Social, Subjects.Global2, Subjects.AdvancedEnglish.A, Global1.SocialResearch,
        Subjects.Social, Subjects.Language, Subjects.AdvancedEnglish.B, Subjects.GlobalStatistics, Subjects.Reading, Subjects.Global2, Global1.SocialResearch,
        Subjects.LogicalWriting, Global1.SocialResearch, Subjects.GlobalStatistics, Subjects.TraditionalArt, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class2 { get; } = new(2, new Subject[]
    {
        Subjects.LogicalWriting, Subjects.TraditionalArt, Subjects.Reading, Subjects.GlobalStatistics, Subjects.AdvancedEnglish.B, Subjects.Others, Subjects.Others,
        Subjects.Sports, Subjects.TraditionalArt, Global1.KoreanSociety, Subjects.Global2, Subjects.Language, Subjects.Reading, Global1.KoreanSociety,
        Subjects.AdvancedEnglish.A, Subjects.Language, Subjects.GlobalStatistics, Subjects.Social, Subjects.Global2, Subjects.AdvancedEnglish.B, Global1.KoreanSociety,
        Subjects.Social, Subjects.Language, Subjects.AdvancedEnglish.A, Subjects.GlobalStatistics, Global1.KoreanSociety, Subjects.Global2, Subjects.Sports,
        Subjects.AdvancedEnglish.A, Subjects.LogicalWriting, Subjects.GlobalStatistics, Subjects.TraditionalArt, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class3 { get; } = new(3, new Subject[]
    {
        Subjects.Global2, Subjects.TraditionalArt, Subjects.Global1, Subjects.AdvancedEnglish.B, Subjects.Sports, Subjects.Others, Subjects.Others,
        Subjects.Global2, Subjects.TraditionalArt, Subjects.GlobalStatistics, Subjects.LogicalWriting, Subjects.Language, Subjects.GlobalStatistics, Subjects.AdvancedEnglish.A,
        Subjects.GlobalStatistics, Subjects.Language, Subjects.Global2, Subjects.Social, Subjects.Reading, Subjects.Global1, Subjects.AdvancedEnglish.B,
        Subjects.Social, Subjects.Language, Subjects.Reading, Subjects.LogicalWriting, Subjects.AdvancedEnglish.A, Subjects.Global1, Subjects.GlobalStatistics,
        Subjects.Global1, Subjects.AdvancedEnglish.A, Subjects.Sports, Subjects.TraditionalArt, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class4 { get; } = new(4, new Subject[]
    {
        Subjects.Global2, Subjects.LogicalWriting, Subjects.Global1, Subjects.Reading, Social.Culture, Subjects.Others, Subjects.Others,
        Subjects.Global2, Subjects.Language, Subjects.AdvancedEnglish.A, Subjects.GlobalStatistics, Social.Culture, Subjects.AdvancedEnglish.B, Subjects.Sports,
        Subjects.TraditionalArt, Subjects.Sports, Subjects.Global2, Subjects.AdvancedEnglish.A, Subjects.Language, Subjects.Global1, Subjects.GlobalStatistics,
        Subjects.GlobalStatistics, Subjects.LogicalWriting, Subjects.TraditionalArt, Subjects.GlobalStatistics, Subjects.AdvancedEnglish.B, Subjects.Global1, Subjects.AdvancedEnglish.A,
        Subjects.Global1, Subjects.TraditionalArt, Subjects.Language, Subjects.Reading, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class5 { get; } = new(5, new Subject[]
    {
        Subjects.Reading, Subjects.Global1, Subjects.Sports, Subjects.Global2, Subjects.GlobalStatistics, Subjects.Others, Subjects.Others,
        Subjects.AdvancedEnglish.A, Subjects.Language, Subjects.GlobalStatistics, Subjects.AdvancedEnglish.B, Subjects.TraditionalArt, Subjects.Global1, Subjects.Global2,
        Subjects.Sports, Subjects.TraditionalArt, Subjects.Global1, Subjects.AdvancedEnglish.B, Subjects.Language, Subjects.GlobalStatistics, Subjects.LogicalWriting,
        Subjects.AdvancedEnglish.A, Social.Culture, Subjects.GlobalStatistics, Subjects.Global2, Subjects.TraditionalArt, Subjects.Reading, Subjects.LogicalWriting,
        Social.Culture, Subjects.AdvancedEnglish.A, Subjects.Language, Subjects.Global1, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class6 { get; } = new(6, new Subject[]
    {
        Subjects.TraditionalArt, Subjects.Global1, Subjects.AdvancedEnglish.A, Subjects.Global2, Language.Spanish, Subjects.Others, Subjects.Others,
        Subjects.Sports, Subjects.GlobalStatistics, Subjects.TraditionalArt, Subjects.LogicalWriting, Subjects.Reading, Subjects.Global1, Subjects.Global2,
        Subjects.Reading, Subjects.AdvancedEnglish.B, Subjects.Global1, Subjects.TraditionalArt, Subjects.AdvancedEnglish.A, Social.Culture, Language.Spanish,
        Subjects.GlobalStatistics, Subjects.AdvancedEnglish.B, Subjects.GlobalStatistics, Subjects.Global2, Subjects.AdvancedEnglish.A, Social.Culture, Subjects.Sports,
        Subjects.LogicalWriting, Subjects.GlobalStatistics, Language.Spanish, Subjects.Global1, Subjects.Others, Subjects.Others, 
    });
    public TimeTable Class7 { get; } = new(7, new Subject[]
    {
        Subjects.GlobalStatistics, Subjects.Global1, Subjects.Global2, Subjects.TraditionalArt, Subjects.Sports, Subjects.Others, Subjects.Others,
        Subjects.GlobalStatistics, Social.Culture, Language.Spanish, Subjects.AdvancedEnglish.A, Subjects.TraditionalArt, Subjects.Global1, Subjects.LogicalWriting,
        Language.Spanish, Subjects.Global2, Subjects.AdvancedEnglish.A, Subjects.GlobalStatistics, Subjects.Reading, Subjects.AdvancedEnglish.B, Social.Culture,
        Subjects.Global2, Subjects.TraditionalArt, Subjects.LogicalWriting, Subjects.GlobalStatistics, Language.Spanish, Subjects.Global1, Subjects.AdvancedEnglish.A,
        Subjects.Reading, Subjects.Global1, Subjects.Sports, Subjects.AdvancedEnglish.B, Subjects.Others, Subjects.Others, 
    });
    public TimeTable Class8 { get; } = new(8, new Subject[]
    {
        Subjects.AdvancedEnglish.A, Social.Culture, Subjects.GlobalStatistics, Subjects.AdvancedEnglish.B, Subjects.Global2, Subjects.Others, Subjects.Others,
        Language.Spanish, Subjects.LogicalWriting, Subjects.TraditionalArt, Subjects.GlobalStatistics, Subjects.Global1, Social.Culture, Subjects.Sports,
        Subjects.Reading, Subjects.Sports, Subjects.AdvancedEnglish.B, Language.Spanish, Subjects.Global1, Subjects.Global2, Subjects.TraditionalArt,
        Subjects.GlobalStatistics, Subjects.AdvancedEnglish.A, Subjects.Global2, Subjects.Global1, Subjects.LogicalWriting, Subjects.TraditionalArt, Subjects.GlobalStatistics,
        Language.Spanish, Subjects.Reading, Subjects.Global1, Subjects.AdvancedEnglish.A, Subjects.Others, Subjects.Others, 
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
            case 3:
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
            case 5:  // (+) 언매 확통 사문
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