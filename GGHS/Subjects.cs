using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGHS.Grade2
{
    public static class Subjects
    {
        //using static Subjects.CellName?

        /// <summary>
        /// ComboBoxName: Used in ComboBox Text
        /// </summary>
        public class ComboBoxName
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
        public class CellName : ComboBoxName
        {
            new public const string CriticalEnglish = "비영";
            new public const string Sport = "운동";
            new public const string CreativeSolve = "창문해";
            new public const string MathResearch = "수과탐";
            new public const string Others = "창체";
            new public const string Ethics = "실윤이";
            new public const string Environment = "인환";
        }

        //
        //  enum에서 모두 property {get;}으로 만들것
        //  상속을 이용한다
        //  implicit operator을 이용해서 자동으로 string으로 변환되게끔
        //  public static implicit operator String(LogCategory category) { return Value; } // 정한 값
        //  각각 class마다 default 값을 추가하고
        //  멤버 변수를 하나 두자. 뭐가 설정되었는지.
        //

        /// <summary>
        /// (Extended) Return the string value if it is in the string[] list.
        /// </summary>
        /// <param name="var">this string value</param>
        /// <param name="else">Returned if var is not in array</param>
        /// <param name="array">Containers to check if var is in</param>
        /// <returns>original variable if in the list, else return @else</returns>
        static string ReturnIfHasInOrElse(this string var, string @else, params string[] array) => Array.IndexOf(array, var) > -1 ? var : @else;
        /// <summary>
        /// Reset All the subjects as None
        /// </summary>
        public static void Clear()
        {
            Sciences.Selected = Sciences.None;
            Social1.Selected = Social1.None;
            Social2.Selected = Social2.None;
            Languages.Selected = Languages.None;
        }

        [RefersToCellName]
        public static class Sciences // sealed
        {
            // 여기는 어쩔 수 없이 switch 때문에 const가 되어야 함...
            public const string Physics = CellName.Physics;
            public const string Chemistry = CellName.Chemistry;
            public const string Biology = CellName.Biology;
            public const string None = "과탐";
            static string selected = None;
            public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Physics, Chemistry, Biology); }
        }

        [RefersToCellName]
        public static class Social1
        {
            public const string Ethics = CellName.Ethics;
            public const string Environment = CellName.Environment;
            public const string None = "전문";
            static string selected = None;

            // 이거 읽기 전용으로 만들고 get set 다가진 selected를 추가하자 그리고 변환 메서드도 만들고..
            public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, Ethics, Environment); }
        }

        [RefersToCellName]
        public static class Social2
        {
            public const string History = CellName.History;
            public const string Geography = CellName.Geography;
            public const string Politics = CellName.Politics;
            public const string Economy = CellName.Economy;
            public const string None = "사탐";
            static string selected = None;
            public static string Selected { get => selected; set => selected = value.ReturnIfHasInOrElse(None, History, Geography, Politics, Economy); }
        }

        [RefersToCellName]
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
