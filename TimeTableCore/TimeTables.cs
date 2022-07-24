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
        Subjects.AdvancedEnglish.B, Subjects.Korean, Subjects.Sports, Subjects.Math, Subjects.AdvancedEnglish.A, Subjects.Others, Subjects.Others,
        Subjects.EnglishLiterature, Subjects.Korean, Subjects.Reading, Subjects.Global2, Subjects.Language, Subjects.AdvancedEnglish.A, Global1.SocialResearch,
        Subjects.Sports, Subjects.Language, Subjects.Math, Subjects.Social, Subjects.Global2, Subjects.AdvancedEnglish.A, Global1.SocialResearch,
        Subjects.Social, Subjects.Language, Subjects.AdvancedEnglish.B, Subjects.Math, Subjects.Reading, Subjects.Global2, Global1.SocialResearch,
        Subjects.EnglishLiterature, Global1.SocialResearch, Subjects.Math, Subjects.Korean, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class2 { get; } = new(2, new Subject[]
    {
        Subjects.EnglishLiterature, Subjects.Korean, Subjects.Reading, Subjects.Math, Subjects.AdvancedEnglish.B, Subjects.Others, Subjects.Others,
        Subjects.Sports, Subjects.Korean, Global1.KoreanSociety, Subjects.Global2, Subjects.Language, Subjects.Reading, Global1.KoreanSociety,
        Subjects.AdvancedEnglish.A, Subjects.Language, Subjects.Math, Subjects.Social, Subjects.Global2, Subjects.AdvancedEnglish.B, Global1.KoreanSociety,
        Subjects.Social, Subjects.Language, Subjects.AdvancedEnglish.A, Subjects.Math, Global1.KoreanSociety, Subjects.Global2, Subjects.Sports,
        Subjects.AdvancedEnglish.A, Subjects.EnglishLiterature, Subjects.Math, Subjects.Korean, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class3 { get; } = new(3, new Subject[]
    {
        Subjects.Global2, Subjects.Korean, Subjects.Global1, Subjects.AdvancedEnglish.B, Subjects.Sports, Subjects.Others, Subjects.Others,
        Subjects.Global2, Subjects.Korean, Math.Probability.B, Subjects.EnglishLiterature, Subjects.Language, Math.Probability.A, Subjects.AdvancedEnglish.A,
        Math.Probability.A, Subjects.Language, Subjects.Global2, Subjects.Social, Subjects.Reading, Subjects.Global1, Subjects.AdvancedEnglish.B,
        Subjects.Social, Subjects.Language, Subjects.Reading, Subjects.EnglishLiterature, Subjects.AdvancedEnglish.A, Subjects.Global1, Math.Probability.A,
        Subjects.Global1, Subjects.AdvancedEnglish.A, Subjects.Sports, Subjects.Korean, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class4 { get; } = new(4, new Subject[]
    {
        Subjects.Global2, Subjects.EnglishLiterature, Subjects.Global1, Subjects.Reading, Social.Culture, Subjects.Others, Subjects.Others,
        Subjects.Global2, Subjects.Language, Subjects.AdvancedEnglish.A, Math.Probability.A, Social.Culture, Subjects.AdvancedEnglish.B, Subjects.Sports,
        Korean.LangMedia, Subjects.Sports, Subjects.Global2, Subjects.AdvancedEnglish.A, Subjects.Language, Subjects.Global1, Math.Probability.A,
        Math.Probability.A, Subjects.EnglishLiterature, Korean.LangMedia, Math.Probability.B, Subjects.AdvancedEnglish.B, Subjects.Global1, Subjects.AdvancedEnglish.A,
        Subjects.Global1, Korean.LangMedia, Subjects.Language, Subjects.Reading, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class5 { get; } = new(5, new Subject[]
    {
        Subjects.Reading, Subjects.Global1, Subjects.Sports, Subjects.Global2, Math.Probability.A, Subjects.Others, Subjects.Others,
        Subjects.AdvancedEnglish.A, Subjects.Language, Math.Probability.A, Subjects.AdvancedEnglish.B, Korean.LangMedia, Subjects.Global1, Subjects.Global2,
        Subjects.Sports, Korean.LangMedia, Subjects.Global1, Subjects.AdvancedEnglish.B, Subjects.Language, Math.Probability.A, Subjects.EnglishLiterature,
        Subjects.AdvancedEnglish.A, Social.Culture, Math.Probability.B, Subjects.Global2, Korean.LangMedia, Subjects.Reading, Subjects.EnglishLiterature,
        Social.Culture, Subjects.AdvancedEnglish.A, Subjects.Language, Subjects.Global1, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class6 { get; } = new(6, new Subject[]
    {
        Korean.LangMedia, Subjects.Global1, Subjects.AdvancedEnglish.A, Subjects.Global2, Language.Spanish, Subjects.Others, Subjects.Others,
        Subjects.Sports, Math.Probability.A, Korean.LangMedia, Subjects.EnglishLiterature, Subjects.Reading, Subjects.Global1, Subjects.Global2,
        Subjects.Reading, Subjects.AdvancedEnglish.B, Subjects.Global1, Korean.LangMedia, Subjects.AdvancedEnglish.A, Social.Culture, Language.Spanish,
        Math.Probability.B, Subjects.AdvancedEnglish.B, Math.Probability.A, Subjects.Global2, Subjects.AdvancedEnglish.A, Social.Culture, Subjects.Sports,
        Subjects.EnglishLiterature, Math.Probability.A, Language.Spanish, Subjects.Global1, Subjects.Others, Subjects.Others, 
    });
    public TimeTable Class7 { get; } = new(7, new Subject[]
    {
        Math.Probability.A, Subjects.Global1, Subjects.Global2, Korean.LangMedia, Subjects.Sports, Subjects.Others, Subjects.Others,
        Math.Probability.A, Social.Culture, Language.Spanish, Subjects.AdvancedEnglish.A, Korean.LangMedia, Subjects.Global1, Subjects.EnglishLiterature,
        Language.Spanish, Subjects.Global2, Subjects.AdvancedEnglish.A, Math.Probability.A, Subjects.Reading, Subjects.AdvancedEnglish.B, Social.Culture,
        Subjects.Global2, Korean.LangMedia, Subjects.EnglishLiterature, Math.Probability.B, Language.Spanish, Subjects.Global1, Subjects.AdvancedEnglish.A,
        Subjects.Reading, Subjects.Global1, Subjects.Sports, Subjects.AdvancedEnglish.B, Subjects.Others, Subjects.Others, 
    });
    public TimeTable Class8 { get; } = new(8, new Subject[]
    {
        Subjects.AdvancedEnglish.A, Social.Culture, Math.Probability.A, Subjects.AdvancedEnglish.B, Subjects.Global2, Subjects.Others, Subjects.Others,
        Language.Spanish, Subjects.EnglishLiterature, Korean.LangMedia, Math.Probability.A, Subjects.Global1, Social.Culture, Subjects.Sports,
        Subjects.Reading, Subjects.Sports, Subjects.AdvancedEnglish.B, Language.Spanish, Subjects.Global1, Subjects.Global2, Korean.LangMedia,
        Math.Probability.A, Subjects.AdvancedEnglish.A, Subjects.Global2, Subjects.Global1, Subjects.EnglishLiterature, Korean.LangMedia, Math.Probability.B,
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
                Math.Selected = Math.Probability;
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
                Korean.Selected = Korean.LangMedia;
                Math.Selected = Math.Probability;
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