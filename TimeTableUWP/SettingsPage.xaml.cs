using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class SettingsPage : Page
    {
        public static bool Use24Hour { get; private set; } = false;
        public static DateType DateFormat { get; private set; } = DateType.MMDDYYYY;
        Dictionary<DateType, int> dateFormatDict = new()
        {
            [DateType.YYYYMMDD] = 0,
            [DateType.YYYYMMDD2] = 1,
            [DateType.MMDDYYYY] = 2,
        };
        public SettingsPage()
        {
            this.InitializeComponent();
            use24Toggle.IsOn = Use24Hour;
            dateFormatRadio.SelectedIndex = dateFormatDict[DateFormat];
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(MainPage));

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e) => Use24Hour = use24Toggle.IsOn;

        private void dateFormatRadio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is RadioButtons rb)
            {
                if (rb.SelectedItem is not string selected)
                    return;
                DateFormat = selected switch
                {
                    "2020/06/03" => DateType.YYYYMMDD,
                    "2020-06-03" => DateType.YYYYMMDD2,
                    "06/03/2020" => DateType.MMDDYYYY,
                    _ => throw new DataAccessException($"dateFormatRadio_SelectionChanged(): SelectedItem is '{selected}'.")
                };
            }
        }
    }
}
