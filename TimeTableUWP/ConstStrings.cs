#nullable enable

namespace TimeTableUWP
{
    public static class Sensitive
    {
        public const string GTTMail = "gghstimetable@gmail.com";
        public const string KaruMail = "nsun527@naver.com";
        public const string MailPassword = "rflnapjbcqznllqu";
    }

    public static class ActivateKeys
    {
        public const string Developer = "RRESS-X93FSBU";
        public const string Grade2 = "GGGHS-B38XDQP";
        public const string Insider = "THANK-LX5MBH3";
        public const string ShareTech = "SHARE-A8VP36N";
    }

    public static class SaveFiles
    {
        public const string DataFile = "gttdatxml.sav";
        public const string KeyFile = "gttactxml.key";
        public const string SettingsFile = "gttsetxml.sav";
        public const string VersionFile = "gttverxml.sav";
    }

    public static class SubTitles
    {
        public const string Developer = "Welcome to the Ultimate GTT4, Karu";
        public const string ShareTech = "ShareTech, let's try our hardest";
        public const string Insider = "Insiders, we're the ones who've made it this far";
        public const string Grade2 = "Back to online, with the task manager";
    }

    public static class Messages
    {
        public const string GGHSTimeTableWithVer = "GGHS Time Table 4";

        public const string Welcome = @"환영합니다, Rolling Ress의 카루입니다.
GGHS Time Table을 설치해주셔서 감사합니다. 

자신의 선택과목을 선택하고, 시간표를 누르면 해당 시간의 
줌 링크와 클래스룸 링크가 띄워집니다.
상단바에서 To do (GTD)를 선택할 경우 
각종 수행평가를 기록하고, 관리할 수 있습니다.
상단바 오른쪽 끝 톱니바퀴 모양을 통해 
설정 메뉴에 들어가실 수 있습니다.

줌 링크가 누락된 경우, 설정 메뉴에서 'Feedback'을 통해
줌 링크/ID/비밀번호를 전달해주시면 바로 추가하겠습니다.";
        public const string WhatsNew = @"- 일본어 ZOOM ID/PW 및 클래스룸 추가";
        public static string Updated => @$"GGHS Time Table이 V{MainPage.Version}{MainPage.Version[MainPage.Version.Length - 1] switch
        {
            '0' or '3' or '6' => "으로",
            _ => "로",
        }} 업데이트 되었습니다.

V{MainPage.Version}부터 추가된 기능
{WhatsNew}

GTT4 부터 To-do 기능이 추가되었습니다. 상단바에서 'To do'를 선택하면 " +
"수행평가 일정 목록을 관리할 수 있습니다. 기존 설정 메뉴는 상단바 " +
"맨 우측으로 옮겨갔으니 참고하시기 바랍니다.";
    }
}