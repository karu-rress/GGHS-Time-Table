using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TimeTableUWP.Conet;
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

namespace TimeTableUWP.Dialogs;

public sealed partial class ConetLoginDialog : ContentDialog
{
    private const string inputError = "학번, 이름, 비밀번호를 정확히 입력하세요.";
    private const string idError = "존재하지 않는 계정입니다. 카루에게 문의하세요.";
    private const string pwError = "비밀번호가 잘못 입력되었습니다.";

    public ConetLoginDialog()
    {
        this.InitializeComponent();
        idBox.BorderBrush = nameBox.BorderBrush 
            = pwBox.BorderBrush = Info.Settings.Brush;
    }

    private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        ErrorBox.Visibility = Visibility.Collapsed;
        if (AreNullOrEmpty(idBox.Text, nameBox.Text, pwBox.Password) ||
            idBox.Text.Length is not 4 || nameBox.Text.Length is 1 or 0 || pwBox.Password.Length is 0)
        {
            ShowError(inputError);
            return;
        }

        Disable(idBox, nameBox, pwBox);
        string student = $"{idBox.Text} {nameBox.Text}";
        string password = pwBox.Password;

        using SqlConnection sql = new(ChatMessageDac.ConnectionString);
        ConetUserDac con = new(sql, student, password);
        try
        {
            await sql.OpenAsync();
            if (!await con.IdExistsAsync())
            {
                ShowError(idError);
                return;
            }

            if (!await con.PasswordMachesAsync())
                ShowError(pwError);

            else
                Info.User.Conet = new(Convert.ToInt32(idBox.Text), nameBox.Text);
        }
        catch (Exception ex)
        {
            ShowError("프로그램 오류로 로그인을 할 수 없습니다.");
            await TimeTableException.HandleException(ex);
        }
        finally
        {
            Enable(idBox, nameBox, pwBox);
        }

        void ShowError(string msg)
        {
            ErrorBox.Text = msg;
            ErrorBox.Visibility = Visibility.Visible;
            args.Cancel = true;
        }
    }

    private void idBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (idBox.Text.Length == idBox.MaxLength)
            _ = nameBox.Focus(FocusState.Keyboard);
    }
}
