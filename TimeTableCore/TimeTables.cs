using System;
using System.Collections.Generic;
using TimeTableCore.Grade3.Semester1;

namespace TimeTableCore;
public class TimeTable
{
    public Subject[] Data { get; set; }
    public int Class { get; }
    // Usage: = Common.Korean | Common.Math;
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


        // 반별 공통과목 제한
        // 예를 들어, 1반이 모두 언매, 확통이라면
        // CommonSubject = Common.Korean | Common.Math

        // Subjects.Korean = Korean.언매 <- 근데 이건 애초에 TimeTable을 저렇게 대입하면 필요 없음.
        // Subjects.Math = Math.Probabiility..

        // TODO 1: 반별 공통과목 제한하기
        //throw new NotImplementedException();
#if !NOT_YET

        // Korean, Math, Social, Language, Global1(사탐), Global2(윤연)
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

#endif
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
    // TODO: 실제 시간표 대입
    public TimeTable Class1 { get; } = new(1, new Subject[]
    {
        Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Sports, Subjects.Math, Subjects.Global1, Subjects.AdvancedEnglish.A(), Subjects.Others,
        Subjects.EnglishLiterature, Subjects.Korean, Subjects.Reading, Subjects.Global2, Subjects.Language, Subjects.AdvancedEnglish.A(), Subjects.Global1,
        Subjects.Sports, Subjects.Language, Subjects.Math, Subjects.Social, Subjects.Global2, Subjects.AdvancedEnglish.A(), Subjects.Empty,
        Subjects.Social, Subjects.Language, Subjects.AdvancedEnglish.B(), Subjects.Math, Subjects.Reading, Subjects.Global2, Subjects.Global1,
        Subjects.EnglishLiterature, Subjects.Global1, Subjects.Math, Subjects.Korean, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class2 { get; } = new(2, new Subject[]
    {
        Subjects.EnglishLiterature, Subjects.Korean, Subjects.Reading, Subjects.Math, Subjects.Global1, Subjects.AdvancedEnglish.B(), Subjects.Others,
        Subjects.Sports, Subjects.Korean, Subjects.Global1, Subjects.Global2, Subjects.Language, Subjects.Reading, Subjects.Global1,
        Subjects.AdvancedEnglish.A(), Subjects.Language, Subjects.Math, Subjects.Social, Subjects.Global2, Subjects.AdvancedEnglish.B(), Subjects.Empty,
        Subjects.Social, Subjects.Language, Subjects.AdvancedEnglish.A(), Subjects.Math, Subjects.Global1, Subjects.Global2, Subjects.Sports,
        Subjects.AdvancedEnglish.A(), Subjects.EnglishLiterature, Subjects.Math, Subjects.Korean, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class3 { get; } = new(3, new Subject[]
    {
        Subjects.Global2, Subjects.Korean, Subjects.Global1, Subjects.AdvancedEnglish.B(), Subjects.Sports, Subjects.Math, Subjects.Others,
        Subjects.Global2, Subjects.Korean, Subjects.Math, Subjects.EnglishLiterature, Subjects.Language, Subjects.Math, Subjects.AdvancedEnglish.A(),
        Subjects.Math, Subjects.Language, Subjects.Global2, Subjects.Social, Subjects.Reading, Subjects.Global1, Subjects.Empty,
        Subjects.Social, Subjects.Language, Subjects.Reading, Subjects.EnglishLiterature, Subjects.AdvancedEnglish.A(), Subjects.Global1, Subjects.AdvancedEnglish.B(),
        Subjects.Global1, Subjects.AdvancedEnglish.A(), Subjects.Sports, Subjects.Korean, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class4 { get; } = new(4, new Subject[]
    {
        Subjects.Global2, Subjects.EnglishLiterature, Subjects.Global1, Subjects.Reading, Subjects.Language, Subjects.Korean, Subjects.Others,
        Subjects.Global2, Subjects.Language, Subjects.AdvancedEnglish.A(), Subjects.Math, Subjects.Social, Subjects.AdvancedEnglish.B(), Subjects.Sports,
        Subjects.Korean, Subjects.Sports, Subjects.Global2, Subjects.AdvancedEnglish.A(), Subjects.Social, Subjects.Global1, Subjects.Empty,
        Subjects.Math, Subjects.EnglishLiterature, Subjects.Korean, Subjects.Math, Subjects.AdvancedEnglish.B(), Subjects.Global1, Subjects.AdvancedEnglish.A(),
        Subjects.Global1, Subjects.Math, Subjects.Language, Subjects.Reading, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class5 { get; } = new(5, new Subject[]
    {
        Subjects.Math, Subjects.Global2, Subjects.Sports, Subjects.Global1, Subjects.Language, Subjects.EnglishLiterature, Subjects.Others,
        Subjects.AdvancedEnglish.A(), Subjects.Language, Subjects.Math, Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Global1, Subjects.Global2,
        Subjects.Sports, Subjects.Korean, Subjects.Global1, Subjects.AdvancedEnglish.B(), Subjects.Math, Subjects.Reading, Subjects.Empty,
        Subjects.Math, Subjects.Social, Subjects.AdvancedEnglish.A(), Subjects.Global2, Subjects.Korean, Subjects.Reading, Subjects.EnglishLiterature,
        Subjects.Social, Subjects.AdvancedEnglish.A(), Subjects.Language, Subjects.Global1, Subjects.Others, Subjects.Others,
    });
    public TimeTable Class6 { get; } = new(6, new Subject[]
    {
        Subjects.Korean, Subjects.Global2, Subjects.AdvancedEnglish.A(), Subjects.Global1, Subjects.Social, Subjects.Language, Subjects.Others,
        Subjects.Sports, Subjects.Math, Subjects.Korean, Subjects.EnglishLiterature, Subjects.Reading, Subjects.Global1, Subjects.Global2,
        Subjects.Reading, Subjects.AdvancedEnglish.B(), Subjects.Global1, Subjects.Korean, Subjects.AdvancedEnglish.A(), Subjects.Language, Subjects.Empty,
        Subjects.Math, Subjects.AdvancedEnglish.B(), Subjects.Math, Subjects.Global2, Subjects.AdvancedEnglish.A(), Subjects.Social, Subjects.Sports,
        Subjects.EnglishLiterature, Subjects.Math, Subjects.Language, Subjects.Global1, Subjects.Others, Subjects.Others, 
    });
    public TimeTable Class7 { get; } = new(7, new Subject[]
    {
        Subjects.Math, Subjects.Global1, Subjects.Social, Subjects.Korean, Subjects.Sports, Subjects.Global2, Subjects.Others,
        Subjects.Math, Subjects.Social, Subjects.Language, Subjects.AdvancedEnglish.A(), Subjects.Korean, Subjects.Global1, Subjects.EnglishLiterature,
        Subjects.Language, Subjects.Global2, Subjects.AdvancedEnglish.A(), Subjects.Math, Subjects.Reading, Subjects.AdvancedEnglish.B(), Subjects.Empty,
        Subjects.Global2, Subjects.Korean, Subjects.EnglishLiterature, Subjects.Math, Subjects.Language, Subjects.Global1, Subjects.AdvancedEnglish.A(),
        Subjects.Reading, Subjects.Global1, Subjects.Sports, Subjects.AdvancedEnglish.B(), Subjects.Others, Subjects.Others, 
    });
    public TimeTable Class8 { get; } = new(8, new Subject[]
    {
        Subjects.AdvancedEnglish.A(), Subjects.Social, Subjects.Math, Subjects.AdvancedEnglish.B(), Subjects.Global2, Subjects.Korean, Subjects.Others,
        Subjects.Language, Subjects.EnglishLiterature, Subjects.Korean, Subjects.Math, Subjects.Global1, Subjects.Social, Subjects.Sports,
        Subjects.Reading, Subjects.Sports, Subjects.AdvancedEnglish.B(), Subjects.Language, Subjects.Global1, Subjects.Global2, Subjects.Empty,
        Subjects.Math, Subjects.AdvancedEnglish.A(), Subjects.Global2, Subjects.Global1, Subjects.EnglishLiterature, Subjects.Korean, Subjects.Math,
        Subjects.Language, Subjects.Reading, Subjects.Global1, Subjects.AdvancedEnglish.A(), Subjects.Others, Subjects.Others, 
    });

    private static TimeTable? table = null;
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
            _ => throw new ArgumentException("@class is not in 1-8.")
        };
    }
}