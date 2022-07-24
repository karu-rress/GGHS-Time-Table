namespace TimeTableUWP;

public sealed partial class SubjectSelectDialog : ContentDialog
{
    public string SelectedSubject { get; set; }
    public SubjectSelectDialog()
    {
        InitializeComponent();
        SubjectComboBox.ItemsSource = new List<string>()
        {
            Social.Selected.FullName,
            ttc::Language.Selected.FullName,
            Global1.Selected.FullName,
            Global2.Selected.FullName,
            "논리적 글쓰기",
            "독서와 의사소통",
            "전통 예술과 사상",
            "심화 영어 독해Ⅱ",
            "통계로 바라보는 국제 문제",
            "체육",
            "기타"
        };
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