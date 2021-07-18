#nullable enable

using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Documents;
using GGHS;
using GGHS.Grade2;
using TimeTableUWP.ComboboxItem;
using RollingRess;
using static RollingRess.Librarys;
using Microsoft.Toolkit.Uwp.Notifications;

namespace TimeTableUWP
{

    public sealed partial class MainPage : Page
    {
        private async Task LoopTimeAsync()
        {
            await Task.Run(() => LoopTime());
        }

        private void LoopTime()
        {
            (int day, int time) pos;
            while (true)
            {
                Thread.Sleep(200);
                now = DateTime.Now;
                _ = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    clock.Text = now.ToString(SettingsPage.Use24Hour ? "HH:mm" : "hh:mm");
                    amorpmBox.Text = SettingsPage.Use24Hour ? string.Empty : now.ToString("tt", CultureInfo.InvariantCulture);
                    dateBlock.Text = now.ToString(SettingsPage.DateFormat switch
                    {
                        DateType.MMDDYYYY => "MM/dd/yyyy",
                        DateType.YYYYMMDD => "yyyy/MM/dd",
                        DateType.YYYYMMDD2 => "yyyy-MM-dd",
                        _ => throw new NotImplementedException()
                    });
                    dayBlock.Text = now.ToString("ddd", CultureInfo.CreateSpecificCulture("en-US"));
                });

                if (now.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday ||
                now.Hour is >= 17 or < 9 or 13)
                {
                    continue;
                }

                pos.day = (int)now.DayOfWeek;
                pos.time = now.Hour switch
                {
                    9 or 10 or 11 or 12 => now.Hour - 8,
                    14 or 15 or 16 => now.Hour - 9,
                    _ => throw new DataAccessException($"Hour is not in 9, 10, 11, 12, 14, 15, 16. given {now.Hour}.")
                };

                DispatchedHandler ChangeColor = delegate()
                {
                    foreach (var item in Buttons)
                    {
                        (item.Background, item.Foreground) = (new SolidColorBrush(Colors.Black), new SolidColorBrush(Colors.White));
                    }
                    var brush = new SolidColorBrush(SaveData.ColorType);
                    Buttons.ElementAt((7 * (pos.day - 1)) + (pos.time - 1)).Background = brush;
                    if (pos.time is <= 6)
                    {
                        Buttons.ElementAt((7 * (pos.day - 1)) + pos.time).Foreground = brush;
                    }
                };
                _ = CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, ChangeColor);

                // 토스트 Notification도 고려해볼 것.
            }
        }
    }
}