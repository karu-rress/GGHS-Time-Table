#nullable enable

using Windows.Storage;

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
        public const string Developer = "RRESS-KARU527";
        public const string Coral = "CORAL-3GGHS10";
        public const string Bisque = "BSQUE-LX5MBH3";
        public const string Azure = "AZURE-A8VP36N";
    } 

    public static class SubTitles
    {
        public const string Developer = "Welcome to the Ultimate GTT5, Karu";
        public const string Azure = "Let's try our hardest, our last journey.";
        public const string Bisque = "We're the ones who've made it this far";
        public const string Coral = "Now we are the K-高3";
    }

    public static class Datas
    {
        public const string Version = "5.0.beta5.1";
    }

    public static class Messages
    {
        public const string GGHSTimeTableWithVer = "GGHS Time Table 5";

        public const string Welcome = @"환영합니다, Rolling Ress의 카루입니다.
GGHS Time Table을 설치해주셔서 감사합니다. 

*** 임시시간표가 나오지 않아 시간표 기능은 동작하지 않습니다.
*** GGHS Anonymous (익명챗) 기능을 우선 시험해보세요 :)

자신의 선택과목을 선택하고, 시간표를 누르면 해당 시간의 
줌 링크와 클래스룸 링크가 띄워집니다.
상단바에서 To do (GTD)를 선택할 경우 
각종 수행평가를 기록하고, 관리할 수 있습니다.
상단바 오른쪽 끝 톱니바퀴 모양을 통해 
설정 메뉴에 들어가실 수 있습니다.

줌 링크가 누락된 경우, 설정 메뉴에서 'Feedback'을 통해
줌 링크/ID/비밀번호를 전달해주시면 바로 추가하겠습니다.";
        public const string WhatsNew = @"- 일본어 ZOOM ID/PW 및 클래스룸 추가";
        public static string Updated => @$"GGHS Time Table이 V{Info.Version}{Info.Version.GetLastNumber() switch
        {
            '0' or '3' or '6' => "으로",
            _ => "로",
        }} 업데이트 되었습니다.

V{Info.Version}부터 추가된 기능
{WhatsNew}

GTT4 부터 To-do 기능이 추가되었습니다. 상단바에서 'To do'를 선택하면 " +
"수행평가 일정 목록을 관리할 수 있습니다. 기존 설정 메뉴는 상단바 " +
"맨 우측으로 옮겨갔으니 참고하시기 바랍니다.";

        public static string About => @$"GGHS Time Table V{Info.Version}

환영합니다, Rolling Ress의 카루입니다.

GGHS Time Table을 설치해주셔서 감사합니다.

자신의 선택과목을 선택하고, 시간표를 누르면 해당 시간의
줌 링크와 클래스룸 링크가 띄워집니다.

기능에 문제가 있거나, 줌 링크가 누락이 된 반 혹은 과목이 있다면
설정 창의 'Send Feedback' 버튼을 통해 제보해주시면 감사하겠습니다.

카루 블로그 링크:
";

        public static string Troubleshoot => "GGHS Time Table 5 사용중 문제가 발생했나요?\n\n" +
"오류가 난 경우 대부분 개발자에게 자동으로 보고되며, 별다른 조치를 취하실 필요가 없습니다. " +
"만약 오류 창이 뜨거나 경고 메시지가 뜬 경우 해당 화면을 캡처해서 제게 보내주시기 바랍니다. " +
"혹은, 'Send Feedback' 버튼을 통해 오류를 제보해주세요.\n\n" +
"선택과목 및 ZOOM 링크에 오류가 있는 경우에도 제보 부탁드립니다.\n" +
$"저장된 데이터가 유실된 경우 {ApplicationData.Current.LocalFolder.Path}폴더를 확인하세요.";
    }
}