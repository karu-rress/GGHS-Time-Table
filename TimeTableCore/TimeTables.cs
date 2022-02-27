﻿using System;
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

        CommonSubject = @class switch
        {
            1 => Common.None,
            2 => Common.None,
            3 => Common.None,
            4 => Common.None,
            5 => Common.None,
            6 => Common.None,
            7 => Common.None,
            8 => Common.None,
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
        Subjects.Korean, Subjects.Math, Subjects.Social, Subjects.Language, Subjects.Global1, Subjects.Global2, Subjects.AdvancedEnglish.B(),
        Subjects.Reading, Subjects.Korean, Subjects.AdvancedEnglish.C(), Subjects.Social, Subjects.Language, Subjects.Global1, Subjects.Global2,
        Subjects.Global1, Subjects.Global2, Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Math, Subjects.Sports, Subjects.Language,
        Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Math, Subjects.Social, Subjects.AdvancedEnglish.A(), Subjects.Global1, Subjects.EnglishLiterature,
        Subjects.Global1, Subjects.EnglishLiterature, Subjects.AdvancedEnglish.A(), Subjects.Korean, Subjects.Math, Subjects.Social, Subjects.HomeComing,
    });
    public TimeTable Class2 { get; } = new(2, new Subject[] 
    {
        Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Math, Subjects.Social, Subjects.Language, Subjects.Global1, Subjects.Global2,
    });
    public TimeTable Class3 { get; } = new(3, new Subject[] 
    {
        Subjects.Global2, Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Math, Subjects.Social, Subjects.Language, Subjects.Global1,
    });
    public TimeTable Class4 { get; } = new(4, new Subject[] 
    {
        Subjects.Global1, Subjects.Global2, Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Math, Subjects.Social, Subjects.Language,
    });
    public TimeTable Class5 { get; } = new(5, new Subject[] 
    {
        Subjects.Language, Subjects.Global1, Subjects.Global2, Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Math, Subjects.Social,
    });
    public TimeTable Class6 { get; } = new(6, new Subject[] 
    {
        Subjects.Social, Subjects.Language, Subjects.Global1, Subjects.Global2, Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Math,
    });
    public TimeTable Class7 { get; } = new(7, new Subject[] 
    {
        Subjects.Math, Subjects.Social, Subjects.Language, Subjects.Global1, Subjects.Global2, Subjects.AdvancedEnglish.B(), Subjects.Korean,
    });
    public TimeTable Class8 { get; } = new(8, new Subject[] 
    {
        Subjects.AdvancedEnglish.B(), Subjects.Korean, Subjects.Math, Subjects.Social, Subjects.Language, Subjects.Global1, Subjects.Global2,
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

        // TODO: 그리고 여기 선택과목 바꿔치기하는 코드
        // ForEach (국어 => 언매) 이런식
    }
}