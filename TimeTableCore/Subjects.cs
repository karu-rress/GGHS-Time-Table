using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TimeTableCore
{
    public class Subject
    {
        public Subject(string name) => FullName = name;
        public Subject(string name, string shortName)
        {
            FullName = name;
            ShortName = shortName;
        }
        public Subject(in Subject subject)
        {
            FullName = subject.FullName;
            ShortName = subject.ShortName;
        }

        public static implicit operator string(Subject subject) => subject.Name;
        public string FullName { get; } = string.Empty;
        public string? ShortName { get; } = null;
        public string Name => ShortName ?? FullName;
    }

    public interface ISelectiveSubject
    {
        Subject Selected { get; set; }
    }

    public class Korean : Subject, ISelectiveSubject
    {
        public static Subject LangMedia => new("언어와 매체", "언매");
        public static Subject SpeechWriting => new("화법과 작문", "화작");
        public static Subject Default => new("국어");
        public Subject Selected { get; set; } = Default;
        public Korean(in Subject korean) : base(korean) { }
    }

    public class Math : Subject, ISelectiveSubject
    {
        public static Subject Probability => new("확률과 통계", "확통");
        public static Subject Daic => new("미적분");
        public static Subject Default => new("수학");
        public Subject Selected { get; set; } = Default;
        public Math(in Subject math) : base(math) { }
    }

    public class Social : Subject, ISelectiveSubject
    {
        public static Subject Eastern => new("동아시아사", "동사");
        public static Subject KoreanGeo => new("한국지리");
        public static Subject Culture => new("사회·문화");
        public static Subject Default => new("사회");
        public Subject Selected { get; set; } = Default;
        public Social(in Subject social) : base(social) { }
    }

    public class Language : Subject, ISelectiveSubject
    {
        public static Subject Spanish => new("스페인어권 문화", "스문");
        public static Subject Japanese => new("일본문화");
        public static Subject Chinese => new("중국문화");
        public static Subject Default => new("외국어");
        public Subject Selected { get; set; } = Default;
        public Language(in Subject language) : base(language) { }
    }

    public class Global1 : Subject, ISelectiveSubject
    {
        public static Subject SocialResearch => new("사회 탐구 방법", "사탐방");
        public static Subject KoreanSociety => new("한국 사회의 이해", "한사이");
        public static Subject Default => new("국제1");
        public Subject Selected { get; set; } = Default;
        public Global1(in Subject global1) : base(global1) { }
    }

    public class Global2 : Subject, ISelectiveSubject
    {
        public static Subject FutureSociety => new("세계 문제와 미래 사회", "세문미");
        public static Subject Ethics => new("윤리학 연습", "윤연");
        public static Subject Default => new("국제2");
        public Subject Selected { get; set; } = Default;
        public Global2(in Subject global2) : base(global2) { }
    }

    namespace Grade3.Semester1
    {
        public static class Subjects
        {
            public static Korean Korean { get; set; }
            public static Math Math { get; set; }
            public static Social Social { get; set; }
            public static Language Language { get; set; }
            public static Global1 Global1 { get; set; }
            public static Global2 Global2 { get; set; }

            public static Subject EnglishLiterature => new("영미 문학 읽기", "영문");
            public static Subject Sports => new("체육");
            public static Subject Reading => new("독서와 의사소통", "독의");
            public static Subject AdvancedEnglish => new("심화영어Ⅱ", "심영Ⅱ");

        }
    }
}