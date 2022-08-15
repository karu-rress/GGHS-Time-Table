using System.Runtime.Serialization;

namespace TimeTableCore
{
    [DataContract(Name = "Subject")]
    public class Subject
    {
        public Subject(string name) => FullName = name;
        public Subject(in Subject subject) : this(subject.FullName, subject.ShortName) { }
        public Subject(string name, string? shortName)
        {
            FullName = name;
            ShortName = shortName;
        }
        
        [DataMember] public virtual string FullName { get; set; } = string.Empty;
        [DataMember] public virtual string? ShortName { get; set; } = null;
        public virtual string Name => ShortName ?? FullName;

        public override string ToString() => Name;
        public bool IsSameWith(string name) => name == ShortName || name == FullName;

        private Subject AddPostFix(char ch) => ShortName is null 
            ? new(FullName + ch) : new(FullName + ch, ShortName + ch);
        public Subject A => AddPostFix('A');
        public Subject B => AddPostFix('B');
    }

    public interface ISelectiveSubject
    {
        void SetAs(string subject);
    }


    [DataContract(Name = "Social")]
    public class Social : Subject, ISelectiveSubject
    {
        public static Subject Eastern => new("동아시아사", "동사");
        public static Subject KoreanGeo => new("한국지리", "한지");
        public static Subject Culture => new("사회·문화", "사문");
        public static Subject Default => new("사회탐구", "사탐");
        [DataMember] public static Subject Selected { get; set; } = Default;
        public Social() : base(Default) { }
        public Social(in Subject social) : base(social) { }
        public void SetAs(string subject)
        {
            if (Eastern.IsSameWith(subject))
                Selected = Eastern;
            else if (KoreanGeo.IsSameWith(subject))
                Selected = KoreanGeo;
            else if (Culture.IsSameWith(subject))
                Selected= Culture;
        }
        [DataMember] public override string FullName => Selected.FullName;
        [DataMember] public override string? ShortName => Selected.ShortName;
        public override string Name => ShortName ?? FullName;
    }

    [DataContract(Name = "Language")]
    public class Language : Subject, ISelectiveSubject
    {
        public static Subject Spanish => new("스페인어권 문화", "스문");
        public static Subject Japanese => new("일본문화", "일문");
        public static Subject Chinese => new("중국문화", "중문");
        public static Subject Default => new("제2외국어", "외국어");
        [DataMember] public static Subject Selected { get; set; } = Default;
        public Language() : base(Default) { }
        public Language(in Subject language) : base(language) { }
        public void SetAs(string subject)
        {
            if (Spanish.IsSameWith(subject))
                Selected = Spanish;
            else if (Japanese.IsSameWith(subject))
                Selected = Japanese;
            else if (Chinese.IsSameWith(subject))
                Selected = Chinese;
        }
        [DataMember] public override string FullName => Selected.FullName;
        [DataMember] public override string? ShortName => Selected.ShortName;
        public override string Name => ShortName ?? FullName;
    }

    [DataContract(Name = "Global1")]
    public class Global1 : Subject, ISelectiveSubject
    {
        public static Subject SocialResearch => new("사회 탐구 방법", "사탐방");
        public static Subject KoreanSociety => new("한국 사회의 이해", "한사이");
        public static Subject Default => new("국제1");
        [DataMember] public static Subject Selected { get; set; } = Default;
        public Global1() : base(Default) { }
        public Global1(in Subject global1) : base(global1) { }
        public void SetAs(string subject)
        {
            if (SocialResearch.IsSameWith(subject))
                Selected = SocialResearch;
            else if (KoreanSociety.IsSameWith(subject))
                Selected = KoreanSociety;
        }
        [DataMember] public override string FullName => Selected.FullName;
        [DataMember] public override string? ShortName => Selected.ShortName;
        public override string Name => ShortName ?? FullName;
    }

    [DataContract(Name = "Global2")]
    public class Global2 : Subject, ISelectiveSubject
    {
        public static Subject FutureSociety => new("세계 문제와 미래 사회", "세문미");
        public static Subject Ethics => new("윤리학 연습", "윤연");
        public static Subject Default => new("국제2");
        [DataMember] public static Subject Selected { get; set; } = Default;
        public Global2() : base(Default) { }
        public Global2(in Subject global2) : base(global2) { }
        public void SetAs(string subject)
        {
            if (FutureSociety.IsSameWith(subject))
                Selected = FutureSociety;
            else if (Ethics.IsSameWith(subject))
                Selected = Ethics;
        }
        [DataMember] public override string FullName => Selected.FullName;
        [DataMember] public override string? ShortName => Selected.ShortName;
        public override string Name => ShortName ?? FullName;
    }

    namespace Grade3.Semester2
    {
        public static class Subjects
        {
            public static Social Social { get; set; } = new();
            public static Language Language { get; set; } = new();
            public static Global1 Global1 { get; set; } = new();
            public static Global2 Global2 { get; set; } = new();

            public static Subject LogicalWriting => new("논리적 글쓰기", "논글");
            public static Subject Reading => new("독서와 의사소통", "독의");
            public static Subject TraditionalArt => new("전통 예술과 사상", "전예");
            public static Subject AdvancedEnglish => new("심화 영어 독해Ⅱ", "심영독");
            public static Subject GlobalStatistics => new("통계로 바라보는 국제 문제", "통국");
            public static Subject Sports => new("체육");
            public static Subject Others => new("창의적 체험활동", "창체");
            public static void ResetSelectiveSubjects()
            {
                Social.Selected = Social.Default;
                Language.Selected = Language.Default;
                Global1.Selected = Global1.Default;
                Global2.Selected = Global2.Default;
            }
        }
    }
}