using System;
using System.Collections.Generic;
using static System.DayOfWeek;

namespace TimeTableCore
{
    public class TimeTable
    {
        private Subject[] _timeTable;
        public int Class { get; }
        public Subject[] Data { get => _timeTable; set => _timeTable = value; }
        public Common CommonSubject { get; } = Common.None; // Common.Korean | Common.Math;
        public TimeTable(int @class, Subject[] timeTable)
        {
            _timeTable = timeTable;
            Class = @class;

            SetByClass(@class);
        }

        private void SetByClass(int @class)
        {
            // TODO: CommonSubject = switch @class {  Common.None , ..... 
            // 창체, 동아리 시간, 홈커밍은 여기서 잘라버리기


            // 반별 공통과목 제한
            // 예를 들어, 1반이 모두 언매, 확통이라면
            // CommonSubject = Common.Korean | Common.Math
            // Subjects.Korean = Korean.언매
            // Subjects.Math = Math.Probabiility..
            throw new NotImplementedException();
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
        public TimeTable Class1 { get; } = new(1, new Subject[] { });
        public TimeTable Class2 { get; } = new(2, new Subject[] { });
        public TimeTable Class3 { get; } = new(3, new Subject[] { });
        public TimeTable Class4 { get; } = new(4, new Subject[] { });
        public TimeTable Class5 { get; } = new(5, new Subject[] { });
        public TimeTable Class6 { get; } = new(6, new Subject[] { });
        public TimeTable Class7 { get; } = new(7, new Subject[] { });
        public TimeTable Class8 { get; } = new(8, new Subject[] { });

        private TimeTable? table = null;
        public TimeTable Table { get => table; set => table = value; }

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

            // 그리고 여기 선택과목 바꿔치기하는 코드
        }
    }
}