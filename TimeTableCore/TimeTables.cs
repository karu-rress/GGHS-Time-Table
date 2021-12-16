using System;
using System.Collections.Generic;
using static System.DayOfWeek;

namespace TimeTableCore
{
    public class TimeTable
    {
        private Subject[] _timeTable;
        public Subject[] Data { get => _timeTable; set => _timeTable = value; }
        public TimeTable(int @class, Subject[] timeTable)
        {
            _timeTable = timeTable;
            // 창체, 동아리 시간 잘라버리기
            // 반별 공통과목 제한
            ResetByClass(@class);
        }

        private void ResetByClass(int @class)
        {
            throw new NotImplementedException();
        }
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
    }
}