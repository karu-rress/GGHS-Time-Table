using System.Collections.Generic;
using System.Linq;
using TimeTableUWP.Pages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP
{
    public sealed partial class SubjectSelectDialog : ContentDialog
    {
        public string SelectedSubject { get; set; }
        public SubjectSelectDialog()
        {
            InitializeComponent();
            SubjectComboBox.ItemsSource = new List<string>() {             "언어와 매체",
            "화법과 작문",
            "확률과 통계",
            "미적분",
            "영미 문학 읽기",
            "동아시아사",
            "한국지리",
            "사회/문화",
            "체육",
            "스페인어권 문화",
            "일본문화",
            "중국문화",
            "심화영어Ⅱ",
            "독서와 의사소통",
            "사회 탐구 방법",
            "한국 사회의 이해",
            "세계 문제와 미래사회",
            "윤리학 연습",
            "기타"};
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (SubjectComboBox.SelectedIndex is -1)
            {
                args.Cancel = true;
                TextBlock.Visibility = Visibility.Visible;
            }
            if (SubjectComboBox.SelectedItem is string s)
            {
                SelectedSubject = s;
            }
        }

    }
}
