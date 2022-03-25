#nullable enable

using TimeTableUWP.Conet;

namespace TimeTableUWP.Dialogs;

public sealed partial class ConetLoginDialog : ContentDialog
{
    private const string inputError = "학번, 이름, 비밀번호를 정확히 입력하세요.";
    private const string idError = "존재하지 않는 계정입니다. 카루에게 문의하세요.";
    private const string idExistsError = "이미 존재하는 계정입니다.";
    private const string pwError = "비밀번호가 잘못 입력되었습니다.";

    private int snum;
    private bool IsAllFilled => !AreNullOrEmpty(idBox.Text, nameBox.Text, pwBox.Password)
        && idBox.Text.Length is 4 && nameBox.Text.Length is (>= 2 and < 5)
        && int.TryParse(idBox.Text, out snum);

    public ConetLoginDialog()
    {
        InitializeComponent();
        idBox.BorderBrush = nameBox.BorderBrush 
            = pwBox.BorderBrush = Info.Settings.Brush;
    }

    private async void ContentDialog_PrimaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args)
    {
        args.Cancel = true;
        ErrorBox.Visibility = Visibility.Collapsed;
        if (!IsAllFilled)
        {
            ShowError(inputError);
            return;
        }

        Disable(idBox, nameBox, pwBox);

        using SqlConnection sql = new(ChatMessageDac.ConnectionString);
        ConetUserDac con = new(sql, $"{idBox.Text} {nameBox.Text}", pwBox.Password);

        try
        {
            await sql.OpenAsync();
            if (!await con.IdExistsAsync())
            {
                ShowError(idError);
                return;
            }

            if (!await con.PasswordMachesAsync())
            {
                ShowError(pwError);
                return;
            }

            Info.User.Conet = new(snum, nameBox.Text);
            Hide();
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

    }
    void ShowError(string msg)
    {
        ErrorBox.Text = msg;
        ErrorBox.Visibility = Visibility.Visible;
    }

    private void idBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (idBox.Text.Length == idBox.MaxLength)
            _ = nameBox.Focus(FocusState.Keyboard);
    }

    private async void ContentDialog_SecondaryButtonClick(ContentDialog _, ContentDialogButtonClickEventArgs args)
    {
        args.Cancel = true;
        ErrorBox.Visibility = Visibility.Collapsed;
        if (!IsAllFilled)
        {
            ShowError(inputError);
            return;
        }

        Disable(idBox, nameBox, pwBox);
        using SqlConnection sql = new(ChatMessageDac.ConnectionString);
        ConetUserDac con = new(sql, $"{idBox.Text} {nameBox.Text}", pwBox.Password);

        try
        {
            await sql.OpenAsync();
            if (await con.IdExistsAsync())
            {
                ShowError(idExistsError);
                return;
            }

            await con.InsertAsync();
            Info.User.Conet = new(snum, nameBox.Text);
            Hide();
        }
        catch (Exception ex)
        {
            ShowError("프로그램 오류로 회원가입을 할 수 없습니다.");
            await TimeTableException.HandleException(ex);
        }
        finally
        {
            Enable(idBox, nameBox, pwBox);
        }
    }
}