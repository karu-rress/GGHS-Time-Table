#nullable enable

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
using System.Diagnostics.CodeAnalysis;
using static RollingRess.StaticClass;
// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimeTableUWP
{
    public enum ActivateLevel
    {
        Developer = 3,
        Grade2 = 5,
        Insider = 7,
        None = 9
    }

    public sealed partial class ActivateDialog : ContentDialog
    {
        public ActivateDialog()
        {
            InitializeComponent();
            RequestedTheme = SettingsPage.IsDarkMode ? ElementTheme.Dark : ElementTheme.Light;
        }

        public ActivateDialog(string msg) : this()
        {
            MainTextBlock.Text = $@"{msg}
카루에게 제공받은 인증키를 입력하세요.
인증키는 5자리의 영문+7자리의 숫자/영문 조합으로 구성되어 있습니다.
인증키를 모르는 경우 설정 창의 'Send Feedback' 기능을 이용하세요.";
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog s, ContentDialogButtonClickEventArgs args)
        {
            if (keyBox1.Text.Length < keyBox1.MaxLength || keyBox2.Text.Length < keyBox2.MaxLength)
            {
                ShowErrorMessage("Please enter the entire key.");
                return;
            }

            switch ($"{keyBox1.Text}-{keyBox2.Text}".ToUpper())
            {
                case ActivateKeys.Developer:
                    SaveData.ActivateStatus = ActivateLevel.Developer;
                    break;
                case ActivateKeys.Grade2:
                    SaveData.ActivateStatus = ActivateLevel.Grade2;
                    break;
                case ActivateKeys.Insider:
                    SaveData.ActivateStatus = ActivateLevel.Insider;
                    break;
                default:
                    ShowErrorMessage("Sorry, please check your activation key.");
                    return;
            }
            SaveData.IsActivated = true;

            void ShowErrorMessage(string msg)
            {
                ErrorBox.Text = msg;
                ErrorBox.Visibility = Visibility.Visible;
                args.Cancel = true;
            }
        }

        protected override void OnPreviewKeyDown(KeyRoutedEventArgs e)
        {
            if (e.Key is VirtualKey.Space)
                e.Handled = true;
            
            base.OnPreviewKeyDown(e);
        }

        private void UpperTextBox(object textBox)
        {
            if (textBox is TextBox sender)
            {
                var selectionStart = sender.SelectionStart;
                sender.Text = sender.Text.ToUpper();
                sender.SelectionStart = selectionStart;
                sender.SelectionLength = 0;
            }
        }

        private void keyBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpperTextBox(sender);
            if (keyBox1.Text.Length == keyBox1.MaxLength)
                _ = keyBox2.Focus(FocusState.Keyboard);
        }

        private void keyBox2_TextChanged(object sender, TextChangedEventArgs e) => UpperTextBox(sender);

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            keyBox1.Text = keyBox2.Text = string.Empty;
            _ = keyBox1.Focus(FocusState.Keyboard);
        }
    }
}
