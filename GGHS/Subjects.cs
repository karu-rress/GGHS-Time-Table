using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RollingRess;

namespace GGHS.Grade2
{
    public static class Subjects
    {
        //using static Subjects.CellName?
        /// <summary>
        /// RawName: Used in ComboBox Text
        /// </summary>
        public static class RawName
        {
            // 이걸 상위 하위 변환이 가능한가?
            public const string Literature = "문학";
            public const string Mathematics = "수학Ⅰ";
            public const string CriticalEnglish = "비판적 영어 글쓰기와 말하기";
            public const string Sport = "운동과 건강";
            public const string CreativeSolve = "창의적 문제 해결 기법";
            public const string MathResearch = "수학과제탐구";
            public const string Others = "창의적 체험활동";
            public const string HomeComing = "홈커밍";

            public const string Physics = "물리학Ⅰ";
            public const string Chemistry = "화학Ⅰ";
            public const string Biology = "생명과학Ⅰ";

            public const string Ethics = "실천 윤리학의 이해";
            public const string Environment = "인간과 환경";

            public const string History = "세계사";
            public const string Geography = "세계지리";
            public const string Politics = "정치와 법";
            public const string Economy = "경제";

            public const string Japanese = "일본어Ⅰ";
            public const string Spanish = "스페인어Ⅰ";
            public const string Chinese = "중국어Ⅰ";
        }

        /// <summary>
        /// CellName: Used in TimeTable Text or Pop-up Dialogs
        /// </summary>
        public static class CellName
        {
            public const string Literature = RawName.Literature;
            public const string Mathematics = RawName.Mathematics;
            public const string CriticalEnglish = "비영";
            public const string Sport = "운동";
            public const string CreativeSolve = "창문해";
            public const string MathResearch = "수과탐";
            public const string Others = "창체";
            public const string HomeComing = RawName.HomeComing;

            public const string Physics = RawName.Physics;
            public const string Chemistry = RawName.Chemistry;
            public const string Biology = RawName.Biology;

            public const string Ethics = "실윤이";
            public const string Environment = "인환";

            public const string History = RawName.History;
            public const string Geography = RawName.Geography;
            public const string Politics = RawName.Politics;
            public const string Economy = RawName.Economy;

            public const string Japanese = RawName.Japanese;
            public const string Spanish = RawName.Spanish;
            public const string Chinese = RawName.Chinese;
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
            public const string Physics = CellName.Physics;
            public const string Chemistry = CellName.Chemistry;
            public const string Biology = CellName.Biology;
            public const string None = "과탐";
            static string selected = None;
            public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Physics, Chemistry, Biology); }
        }

        
        public static class Specials
        {
            public const string Ethics = CellName.Ethics;
            public const string Environment = CellName.Environment;
            public const string None = "전문";
            static string selected = None;
            public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Ethics, Environment); }
        }

        
        public static class Socials
        {
            public const string History = CellName.History;
            public const string Geography = CellName.Geography;
            public const string Politics = CellName.Politics;
            public const string Economy = CellName.Economy;
            public const string None = "사탐";
            static string selected = None;
            public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, History, Geography, Politics, Economy); }
        }

        
        public class Languages
        {
            public const string Japanese = CellName.Japanese;
            public const string Spanish = CellName.Spanish;
            public const string Chinese = CellName.Chinese;
            public const string None = "외국어";
            static string selected = None;
            public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Japanese, Spanish, Chinese); }
        }
    }
}
