#nullable enable

using Windows.System;

namespace TimeTableUWP;
public sealed partial class ActivateDialog : ContentDialog
{
    public ActivateDialog()
    {
        InitializeComponent();
        RequestedTheme = Info.Settings.Theme;
        keyBox1.BorderBrush = keyBox2.BorderBrush = Info.Settings.Brush;
    }

    public ActivateDialog(string msg) : this()
    {
        MainTextBlock.Text = string.Format(Messages.Dialog.Activate, msg);
    }

    private void ContentDialog_PrimaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args)
    {
        if (keyBox1.Text.Length < keyBox1.MaxLength || keyBox2.Text.Length < keyBox2.MaxLength)
        {
            ShowErrorMessage("Please enter the entire key.");
            return;
        }

        switch ($"{keyBox1.Text}-{keyBox2.Text}".ToUpper())
        {
            case ActivateKeys.Developer:
                Info.User.ActivationLevel = ActivationLevel.Developer;
                break;
            case ActivateKeys.Azure:
                Info.User.ActivationLevel = ActivationLevel.Azure;
                break;
            case ActivateKeys.Bisque:
                Info.User.ActivationLevel = ActivationLevel.Bisque;
                break;
            case ActivateKeys.Coral:
                Info.User.ActivationLevel = ActivationLevel.Coral;
                break;
            default:
                ShowErrorMessage("Sorry, please check your activation key.");
                return;
        }

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
            int selectionStart = sender.SelectionStart;
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