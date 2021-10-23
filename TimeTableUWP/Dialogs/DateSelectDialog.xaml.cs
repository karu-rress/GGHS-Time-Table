using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TimeTableUWP.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP.Dialogs
{
    public sealed partial class DateSelectDialog : ContentDialog
    {
        public DateTime SelectedDate { get; set; }

        public DateSelectDialog()
        {
            InitializeComponent();
            RequestedTheme = MainPage.Theme;
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
                var date = datePicker.SelectedDate.Value.DateTime;
                SelectedDate = new(date.Year, date.Month, date.Day);
            }
        }
    }
}