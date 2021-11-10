#nullable enable

using RollingRess;

namespace GGHS
{

    public interface ISubjects
    {
        int Grade { get; }
        int Semester { get; }
    }

    namespace Grade2
    {
        namespace Semester2
        {
            public static class SubjectsFullNames
            {
                public const string Reading = "독서";
                public const string Mathematics = "수학Ⅱ";
                public const string AdvancedEnglish = "심화영어Ⅰ";
                public const string Sport = "운동과 건강";
                public const string CreativeSolve = "창의적 문제 해결 기법";
                public const string MathResearch = "수학과제탐구";
                public const string Others = "창의적 체험활동";
                public const string HomeComing = "홈커밍";

                public const string ScienceHistory = "과학사";
                public const string LifeAndScience = "생활과 과학";

                public const string GlobalEconomics = "국제경제";
                public const string GlobalPolitics = "국제정치";
                public const string CompareCulture = "비교문화";
                public const string EasternHistory = "동양근대사";
                public const string HistoryAndCulture = "세계 역사와 문화";
                public const string PoliticsPhilosophy = "현대정치철학의 이해";
                public const string RegionResearch = "세계 지역 연구";
                public const string GISAnalyze = "공간 정보와 공간 분석";

                public const string Japanese = "일본어Ⅰ";
                public const string Spanish = "스페인어Ⅰ";
                public const string Chinese = "중국어Ⅰ";
            }

            public class Subjects : ISubjects
            {
                int ISubjects.Grade => 2;
                int ISubjects.Semester => 2;

                //using static Subjects.CellName?
                /// <summary>
                /// RawName: Used in ComboBox Text
                /// </summary>
                public class RawName
                {
                    public static string Reading => SubjectsFullNames.Reading;
                    public static string Mathematics => SubjectsFullNames.Mathematics;
                    public static string AdvancedEnglish => SubjectsFullNames.AdvancedEnglish;
                    public static string Sport => SubjectsFullNames.Sport;
                    public static string CreativeSolve => SubjectsFullNames.CreativeSolve;
                    public static string MathResearch => SubjectsFullNames.MathResearch;
                    public static string Others => SubjectsFullNames.Others;
                    public static string HomeComing => SubjectsFullNames.HomeComing;

                    public static string ScienceHistory => SubjectsFullNames.ScienceHistory;
                    public static string LifeAndScience => SubjectsFullNames.LifeAndScience;

                    public static string GlobalEconomics => SubjectsFullNames.GlobalEconomics;
                    public static string GlobalPolitics => SubjectsFullNames.GlobalPolitics;
                    public static string CompareCulture => SubjectsFullNames.CompareCulture;
                    public static string EasternHistory => SubjectsFullNames.EasternHistory;
                    public static string HistoryAndCulture => SubjectsFullNames.HistoryAndCulture;
                    public static string PoliticsPhilosophy => SubjectsFullNames.PoliticsPhilosophy;
                    public static string RegionResearch => SubjectsFullNames.RegionResearch;
                    public static string GISAnalyze => SubjectsFullNames.GISAnalyze;

                    public static string Japanese => SubjectsFullNames.Japanese;
                    public static string Spanish => SubjectsFullNames.Spanish;
                    public static string Chinese => SubjectsFullNames.Chinese;
                }

                /// <summary>
                /// CellName: Used in TimeTable Text or Pop-up Dialogs
                /// </summary>
                public class CellName : RawName
                {
                    // public static string Reading => RawName.Reading;
                    // public static string Mathematics => RawName.Mathematics;
                    public static new string AdvancedEnglish => "심영Ⅰ";
                    public static new string Sport => "운동";
                    public static new string CreativeSolve => "창문해";
                    public static new string MathResearch => "수과탐";
                    public static new string Others => "창체";
                    // public static string HomeComing => RawName.HomeComing;

                    // public static string ScienceHistory => RawName.ScienceHistory;
                    // public static new string LifeAndScience => "생활과학";

                    // public static string GlobalEconomics => RawName.GlobalEconomics;
                    // public static string GlobalPolitics => RawName.GlobalPolitics;
                    // public static string CompareCulture => RawName.CompareCulture;
                    // public static string EasternHistory => RawName.EasternHistory;
                    public static new string HistoryAndCulture => "세역문";
                    public static new string PoliticsPhilosophy => "현정철";
                    public static new string RegionResearch => "세지연";
                    public static new string GISAnalyze => "GIS";

                    // public static string Japanese => RawName.Japanese;
                    // public static string Spanish => RawName.Spanish;
                    // public static string Chinese => RawName.Chinese;
                }

                /// <summary>
                /// Reset All the subjects as None
                /// </summary>
                public static void Clear()
                {
                    Sciences.Selected = Sciences.None;
                    Specials1.Selected = Specials1.None;
                    Specials2.Selected = Specials2.None;
                    Languages.Selected = Languages.None;
                }

                public static class Sciences // sealed
                {
                    public static string ScienceHistory => CellName.ScienceHistory;
                    public static string LifeAndScience => CellName.LifeAndScience;
                    public static string None => "과학선택";

                    private static string selected = None;
                    public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, ScienceHistory, LifeAndScience); }
                }

                public static class Specials1
                {
                    public static string GlobalEconomics => CellName.GlobalEconomics;
                    public static string GlobalPolitics => CellName.GlobalPolitics;
                    public static string CompareCulture => CellName.CompareCulture;
                    public static string EasternHistory => CellName.EasternHistory;
                    public static string HistoryAndCulture => CellName.HistoryAndCulture;
                    public static string PoliticsPhilosophy => CellName.PoliticsPhilosophy;
                    public static string RegionResearch => CellName.RegionResearch;
                    public static string GISAnalyze => CellName.GISAnalyze;
                    public static string None => "전문A";
                    private static string selected = None;
                    public static string Selected
                    {
                        get => selected; set => selected = value.ReturnIfHasInOrElse(None,
GlobalEconomics, GlobalPolitics, CompareCulture, EasternHistory, HistoryAndCulture, PoliticsPhilosophy,
RegionResearch, GISAnalyze);
                    }
                }

                public static class Specials2
                {
                    public static string GlobalEconomics => CellName.GlobalEconomics;
                    public static string GlobalPolitics => CellName.GlobalPolitics;
                    public static string CompareCulture => CellName.CompareCulture;
                    public static string EasternHistory => CellName.EasternHistory;
                    public static string HistoryAndCulture => CellName.HistoryAndCulture;
                    public static string PoliticsPhilosophy => CellName.PoliticsPhilosophy;
                    public static string RegionResearch => CellName.RegionResearch;
                    public static string GISAnalyze => CellName.GISAnalyze;
                    public static string None => "전문B";
                    private static string selected = None;
                    public static string Selected
                    {
                        get => selected; set => selected = value.ReturnIfHasInOrElse(None,
                            GlobalEconomics, GlobalPolitics, CompareCulture, EasternHistory, HistoryAndCulture, PoliticsPhilosophy,
                        RegionResearch, GISAnalyze);
                    }
                }

                public static class Languages
                {
                    public static string Japanese => CellName.Japanese;
                    public static string Spanish => CellName.Spanish;
                    public static string Chinese => CellName.Chinese;
                    public static string None => "외국어";
                    static string selected = None;
                    public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Japanese, Spanish, Chinese); }
                }
            }
        } // Semester 2

        namespace Semester1
        {
            public class Subjects : ISubjects
            {
                int ISubjects.Grade { get; } = 2;
                int ISubjects.Semester { get; } = 1;


                //using static Subjects.CellName?
                /// <summary>
                /// RawName: Used in ComboBox Text
                /// </summary>
                public class RawName
                {
                    public static string Literature { get; } = "문학";
                    public static string Mathematics { get; } = "수학Ⅰ";
                    public static string CriticalEnglish { get; } = "비판적 영어 글쓰기와 말하기";
                    public static string Sport { get; } = "운동과 건강";
                    public static string CreativeSolve { get; } = "창의적 문제 해결 기법";
                    public static string MathResearch { get; } = "수학과제탐구";
                    public static string Others { get; } = "창의적 체험활동";
                    public static string HomeComing { get; } = "홈커밍";

                    public static string Physics { get; } = "물리학Ⅰ";
                    public static string Chemistry { get; } = "화학Ⅰ";
                    public static string Biology { get; } = "생명과학Ⅰ";

                    public static string Ethics { get; } = "실천 윤리학의 이해";
                    public static string Environment { get; } = "인간과 환경";

                    public static string History { get; } = "세계사";
                    public static string Geography { get; } = "세계지리";
                    public static string Politics { get; } = "정치와 법";
                    public static string Economy { get; } = "경제";

                    public static string Japanese { get; } = "일본어Ⅰ";
                    public static string Spanish { get; } = "스페인어Ⅰ";
                    public static string Chinese { get; } = "중국어Ⅰ";
                }

                /// <summary>
                /// CellName: Used in TimeTable Text or Pop-up Dialogs
                /// </summary>
                public class CellName : RawName
                {
                    // public static string Literature { get; } = RawName.Literature;
                    // public static string Mathematics { get; } = RawName.Mathematics;
                    public static new string CriticalEnglish { get; } = "비영";
                    public static new string Sport { get; } = "운동";
                    public static new string CreativeSolve { get; } = "창문해";
                    public static new string MathResearch { get; } = "수과탐";
                    public static new string Others { get; } = "창체";
                    // public static string HomeComing { get; } = RawName.HomeComing;

                    // public static string Physics { get; } = RawName.Physics;
                    // public static string Chemistry { get; } = RawName.Chemistry;
                    // public static string Biology { get; } = RawName.Biology;

                    public static new string Ethics { get; } = "실윤이";
                    public static new string Environment { get; } = "인환";

                    // public static string History { get; } = RawName.History;
                    // public static string Geography { get; } = RawName.Geography;
                    // public static string Politics { get; } = RawName.Politics;
                    // public static string Economy { get; } = RawName.Economy;

                    // public static string Japanese { get; } = RawName.Japanese;
                    // public static string Spanish { get; } = RawName.Spanish;
                    // public static string Chinese { get; } = RawName.Chinese;
                }

                /// <summary>
                /// Reset All the subjects as None
                /// </summary>
                public static void Clear()
                {
                    Sciences.Selected = Sciences.None;
                    Specials.Selected = Specials.None;
                    Socials.Selected = Socials.None;
                    Languages.Selected = Languages.None;
                }

                public static class Sciences // sealed
                {
                    public static string Physics { get; } = CellName.Physics;
                    public static string Chemistry { get; } = CellName.Chemistry;
                    public static string Biology { get; } = CellName.Biology;
                    public static string None { get; } = "과탐";
                    static string selected = None;
                    public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Physics, Chemistry, Biology); }
                }


                public static class Specials
                {
                    public static string Ethics { get; } = CellName.Ethics;
                    public static string Environment { get; } = CellName.Environment;
                    public static string None { get; } = "전문";
                    static string selected = None;
                    public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Ethics, Environment); }
                }


                public static class Socials
                {
                    public static string History { get; } = CellName.History;
                    public static string Geography { get; } = CellName.Geography;
                    public static string Politics { get; } = CellName.Politics;
                    public static string Economy { get; } = CellName.Economy;
                    public static string None { get; } = "사탐";
                    static string selected = None;
                    public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, History, Geography, Politics, Economy); }
                }


                public static class Languages
                {
                    public static string Japanese { get; } = CellName.Japanese;
                    public static string Spanish { get; } = CellName.Spanish;
                    public static string Chinese { get; } = CellName.Chinese;
                    public static string None { get; } = "외국어";
                    static string selected = None;
                    public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Japanese, Spanish, Chinese); }
                }
            }
        }
    }
}