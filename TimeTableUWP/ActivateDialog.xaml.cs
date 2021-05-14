using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RollingRess;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP
{
    // TODO: ActivateKeys랑 ActivateLvel...enum 구조 재정리? 아님 Dll에 또 넣어야 하나...
    static class ActivateKeys
    {
        public const string Test = "00001-00001-00001-00001-00001";
        public const string Developer = "RRESS-D83JF-KMX91-MXBF7-ZNX93";
        public const string Grade2 = "GGGHS-IRWFE-H9SOX-WF0JQ-ZJ39S";
    }

    public enum ActivateLevel
    {
        Test,
        Developer,
        Grade2,
        None
    }

    /*
     * 
     * 인증키 과정
     * 
     * 입력을 받는다
     */
    public sealed partial class ActivateDialog : ContentDialog
    {
        public ActivateDialog()
        {
            this.InitializeComponent();
        }

        // TextBox[] keyBoxArray = new TextBox[5];

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (Librarys.AreNullOrEmpty(keyBox1.Text, keyBox2.Text, keyBox3.Text, keyBox4.Text, keyBox5.Text))
            {
                MessageDialog messageDialog = new("Please enter the entire key", "Error");
                _ = messageDialog.ShowAsync();
                return;
            }

            string key = $"{keyBox1.Text}-{keyBox2.Text}-{keyBox3.Text}-{keyBox4.Text}-{keyBox5.Text}";
            string license;

            switch (key.ToUpper())
            {
                case ActivateKeys.Test:
                    SaveData.ActivateStatus = ActivateLevel.Test;
                    license = "TEST";
                    break;
                case ActivateKeys.Developer:
                    SaveData.ActivateStatus = ActivateLevel.Developer;
                    license = "developer"; // TODO: make this as an enum? class?
                    break;
                case ActivateKeys.Grade2:
                    SaveData.ActivateStatus = ActivateLevel.Grade2;
                    license = "GGHS 10th";
                    break;
                default:
                    return;
            }
            SaveData.IsActivated = true;
            MessageDialog message = new($"Activated as {license}.", "Activated successfully");
            _ = message.ShowAsync();
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        protected override void OnPreviewKeyDown(KeyRoutedEventArgs e)
        {
            if (e.Key is VirtualKey.Space)
            {
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }

        private void keyBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (keyBox1.Text.Length == keyBox1.MaxLength)
            {
                keyBox2.Focus(FocusState.Keyboard);
            }
        }

        private void keyBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (keyBox2.Text.Length == keyBox1.MaxLength)
            {
                keyBox3.Focus(FocusState.Keyboard);
            }
        }

        private void keyBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (keyBox3.Text.Length == keyBox1.MaxLength)
            {
                keyBox4.Focus(FocusState.Keyboard);
            }
        }

        private void keyBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (keyBox4.Text.Length == keyBox1.MaxLength)
            {
                keyBox5.Focus(FocusState.Keyboard);
            }
        }

        private void keyBox5_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
