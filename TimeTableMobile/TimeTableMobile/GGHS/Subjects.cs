#nullable enable

using System;
using TimeTableMobile.GGHS.Grade2.Semester2;

namespace TimeTableMobile.GGHS
{
    internal static class extensions
    {
        public static string ReturnIfHasInOrElse(this string var, string @else, params string[] array)
    => Array.IndexOf(array, var) > -1 ? var : @else;

        public static string RawNameToCellName(this string subject)
        {
            return subject switch
            {
                SubjectsFullNames.Reading => Subjects.CellName.Reading,
                SubjectsFullNames.Mathematics => Subjects.CellName.Mathematics,
                SubjectsFullNames.AdvancedEnglish => Subjects.CellName.AdvancedEnglish,
                SubjectsFullNames.Sport => Subjects.CellName.Sport,
                SubjectsFullNames.CreativeSolve => Subjects.CellName.CreativeSolve,
                SubjectsFullNames.MathResearch => Subjects.CellName.MathResearch,
                SubjectsFullNames.Others => Subjects.CellName.Others,
                SubjectsFullNames.HomeComing => Subjects.CellName.HomeComing,

                SubjectsFullNames.ScienceHistory => Subjects.CellName.ScienceHistory,
                SubjectsFullNames.LifeAndScience => Subjects.CellName.LifeAndScience,

                SubjectsFullNames.GlobalEconomics => Subjects.CellName.GlobalEconomics,
                SubjectsFullNames.GlobalPolitics => Subjects.CellName.GlobalPolitics,
                SubjectsFullNames.CompareCulture => Subjects.CellName.CompareCulture,
                SubjectsFullNames.EasternHistory => Subjects.CellName.EasternHistory,
                SubjectsFullNames.HistoryAndCulture => Subjects.CellName.HistoryAndCulture,
                SubjectsFullNames.PoliticsPhilosophy => Subjects.CellName.PoliticsPhilosophy,
                SubjectsFullNames.RegionResearch => Subjects.CellName.RegionResearch,
                SubjectsFullNames.GISAnalyze => Subjects.CellName.GISAnalyze,

                SubjectsFullNames.Japanese => Subjects.CellName.Japanese,
                SubjectsFullNames.Spanish => Subjects.CellName.Spanish,
                SubjectsFullNames.Chinese => Subjects.CellName.Chinese
            };
        }
    }

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
                    public static new string LifeAndScience => "생과";

                    public static new string GlobalEconomics => "국경";
                    public static new string GlobalPolitics => "국정";
                    public static new string CompareCulture => "비문";
                    public static new string EasternHistory => "동근사";
                    public static new string HistoryAndCulture => "세역문";
                    public static new string PoliticsPhilosophy => "현정철";
                    public static new string RegionResearch => "세지연";
                    public static new string GISAnalyze => "GIS";

                    public static new string Japanese => "일어Ⅰ";
                    public static new string Spanish => "스어Ⅰ";
                    public static new string Chinese => "중어Ⅰ";
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
        }
    }
}