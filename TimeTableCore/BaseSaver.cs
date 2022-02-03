using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableCore
{
    public class BaseSaver
    {
        public Subject? Korean { get; set; }
        public Subject? Math { get; set; }
        public Subject? Social { get; set; }
        public Subject? Language { get; set; }
        public Subject? Global1 { get; set; }
        public Subject? Global2 { get; set; }

        public User? UserData { get; set; }
        public void SetSubjects(Subject korean, Subject math, Subject social, Subject lang, Subject global1, Subject global2)
        {
            Korean = korean;
            Math = math;
            Social = social;
            Language = lang;
            Global1 = global1;
            Global2 = global2;
        }
    }
}
