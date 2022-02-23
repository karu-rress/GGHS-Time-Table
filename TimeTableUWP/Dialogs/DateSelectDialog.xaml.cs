namespace TimeTableUWP;

public sealed partial class DateSelectDialog : ContentDialog
{
    public DateTime SelectedDate { get; set; }

    public DateSelectDialog()
    {
        InitializeComponent();
    }

    private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        if (datePicker.SelectedDate is null)
        {
            args.Cancel = true;
            textBlock.Visibility = Visibility.Visible;
        }
        else
        {
            DateTime date = datePicker.SelectedDate.Value.DateTime;
            SelectedDate = new(date.Year, date.Month, date.Day);
        }
    }
}
