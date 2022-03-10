namespace TimeTableUWP;

public sealed partial class SubjectSelectDialog : ContentDialog
{
    public string SelectedSubject { get; set; }
    public SubjectSelectDialog()
    {
        InitializeComponent();
        SubjectComboBox.ItemsSource = new List<string>()
        {
            Korean.Selected.FullName,
            ttc::Math.Selected.FullName,
            Social.Selected.FullName,
            ttc::Language.Selected.FullName,
            Global1.Selected.FullName,
            Global2.Selected.FullName,
            "영미 문학 읽기",
            "심화영어Ⅱ",
            "독서와 의사소통",
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