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
        None = 7
    }

    public sealed partial class ActivateDialog : ContentDialog
    {
        public ActivateDialog()
        {
            InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog s, ContentDialogButtonClickEventArgs args)
        {
            if (AreNullOrEmpty(keyBox1.Text, keyBox2.Text))
            {
                MessageDialog messageDialog = new("Please enter the entire key", "Error");
                _ = messageDialog.ShowAsync();
                return;
            }

            string key = $"{keyBox1.Text}-{keyBox2.Text}";
            string license;

            switch (key.ToUpper())
            {
                case ActivateKeys.Developer:
                    SaveData.ActivateStatus = ActivateLevel.Developer;
                    license = "developer"; // TODO: make this as an enum? class?
                    break;
                case ActivateKeys.Grade2:
                    SaveData.ActivateStatus = ActivateLevel.Grade2;
                    license = "GGHS 10th";
                    break;
                default:
                    await ShowMessageAsync("Please double-check your activation key.", "Activation Failed");
                    return;
            }
            SaveData.IsActivated = true;
            MessageDialog message = new($"Activated as {license}.", "Activated successfully");
            _ = message.ShowAsync();
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
    }
}
