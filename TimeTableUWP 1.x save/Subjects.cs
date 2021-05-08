using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectDll
{
    namespace Grade2
    {
        public static class Subjects
        {
            public static class RawName
            {
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
            public const string Literature = RawName.Literature;
            public const string Mathematics = RawName.Mathematics;
            public const string CriticalEnglish = "비영";
            public const string Sport = "운동";
            public const string CreativeSolve = "창문해";
            public const string MathResearch = "수과탐";
            public const string Others = "창체";
            public const string HomeComing = RawName.HomeComing;

            public const string ScienceTmp = "SCIENCE";
            public const string Social1Tmp = "SOCIAL1";
            public const string Social2Tmp = "SOCIAL2";
            public const string LanguageTmp = "LANGUAGE";

            public enum Science
            {
                Physics,
                Chemistry,
                Biology
            }
            private static Science scienceSubject = Science.Biology;
            public static string GetScienceSubject()
            => scienceSubject switch
            {
                Science.Physics => RawName.Physics,
                Science.Chemistry => RawName.Chemistry,
                Science.Biology => RawName.Biology,
                _ => throw new System.Exception(),
            };

            public static void SetScienceSubject(Science subject)
                => scienceSubject = subject;

            public enum Social1
            {
                Ethics,
                Environment
            }
            private static Social1 social1Subject = Social1.Ethics;
            public static string GetSocial1Subject()
            => social1Subject switch
            {
                Social1.Ethics => "실윤이",
                Social1.Environment => "인환",
                _ => throw new Exception(),
            };

            public static void SetSocial1Subject(Social1 subject)
            => social1Subject = subject;

            public enum Social2
            {
                History,
                Geography,
                Politics,
                Economy
            }
            private static Social2 social2Subject = Social2.Politics;
            public static string GetSocial2Subject()
              => social2Subject switch
              {
                  Social2.History => RawName.History,
                  Social2.Geography => RawName.Geography,
                  Social2.Politics => RawName.Politics,
                  Social2.Economy => RawName.Economy,
                  _ => throw new System.Exception(),
              };
            public static void SetSocial2Subject(Social2 subject)
            => social2Subject = subject;

            public enum Language
            {
                Japanese,
                Spanish,
                Chinese
            }
            private static Language language = Language.Spanish;
            public static string GetLanguageSubject()
            => language switch
            {
                Language.Japanese => RawName.Japanese,
                Language.Spanish => RawName.Spanish,
                Language.Chinese => RawName.Chinese,
                _ => throw new System.Exception(),
            };
            public static void SetLanguageSubject(Language subject)
                => language = subject;
        }
    }
}
