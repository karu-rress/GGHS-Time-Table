#nullable enable

using Windows.UI.Xaml.Media.Animation;

namespace TimeTableUWP.Conet;

public sealed partial class ConetAddPage : Page
{
    public static ConetHelp? Conet { get; set; } = null;

    public ConetAddPage()
    {
        InitializeComponent();

        if (Conet is not null) // Not creating, but modifying
        {
            DeleteButton.Visibility = Visibility.Visible;
            mainText.Text = "Modify";
            TitleTextBox.Text = Conet.Title;
            idTextBox.Text = Conet.Uploader.id.ToString();
            nameTextBox.Text = Conet.Uploader.name.TrimEnd();
            eggTextBox.Text = Conet.Price?.Value.ToString() ?? string.Empty;
            BodyTextBox.Text = Conet.Body ?? string.Empty;
        }
    }

    private async void PostButton_Click(object sender, RoutedEventArgs e)
    {
        if (AreNullOrWhiteSpace(TitleTextBox.Text, idTextBox.Text, nameTextBox.Text))
        {
            await ShowMessageAsync("제목과 학번/이름을 입력하세요.", "Error", Info.Settings.Theme);
            return;
        }

        if (idTextBox.Text.Length is not 4 || idTextBox.Text[0] is not ('1' or '2' or '3') || !int.TryParse(idTextBox.Text, out int number))
        {
            await ShowMessageAsync("학번이 올바르지 않습니다.\n4자리 숫자를 정확히 입력하세요.", "Error", Info.Settings.Theme);
            return;
        }

        if (Modified)
        {

            bool eggExists = false;
            uint egg = 0;
            if (!eggTextBox.IsNullOrEmpty())
            {
                eggExists = true;
                if (!uint.TryParse(eggTextBox.Text, out egg))
                {
                    await ShowMessageAsync("에그가 올바르게 입력되지 않았습니다.", "Error", Info.Settings.Theme);
                    return;
                }
            }

            ConetHelp conet = new(DateTime.Now, new(number, nameTextBox.Text), TitleTextBox.Text,
                BodyTextBox.IsNullOrWhiteSpace() ? null : BodyTextBox.Text, eggExists ? new Egg(egg) : null);

            using SqlConnection sql = new(ChatMessageDac.ConnectionString);
            using ConetHelpDac con = new(sql);
            Disable(PostButton);
            try
            {
                await sql.OpenAsync();
                if (Conet is null)
                    await con.InsertAsync(conet);
                else
                    await con.UpdateAsync(Conet.UploadDate, conet);
            }
            catch (Exception ex)
            {
                await Task.WhenAll(
                    ShowMessageAsync("업로드에 실패했습니다.", "에러", Info.Settings.Theme)
                    , TimeTableException.HandleException(ex));
            }
            finally
            {
                Close();
                Enable(PostButton);
            }
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();

    private void Close() { Conet = null; Frame.Navigate(typeof(ConetPage), null, new DrillInNavigationTransitionInfo()); }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (Modified)
        {
            await ShowMessageAsync("This task has been modified. Save or discard changes and try again.", "Couldn't delete", Info.Settings.Theme);
            return;
        }

        using SqlConnection sql = new(ChatMessageDac.ConnectionString);
        using ConetHelpDac con = new(sql);

        Disable(DeleteButton);
        try
        {
            await sql.OpenAsync();
            await con.DeleteAsync(Conet!.UploadDate);
        }
        catch (Exception ex)
        {
            await Task.WhenAll(ShowMessageAsync("업로드에 실패했습니다.", "에러", Info.Settings.Theme),
                TimeTableException.HandleException(ex));
        }
        finally
        {
            Close();
            Enable(DeleteButton);
        }
    }

    private bool Modified => Conet is not null 
        && (TitleTextBox.Text != Conet.Title
                || $"{idTextBox.Text} {nameTextBox.Text}" != Conet.Uploader.ToString()
                || (BodyTextBox.IsNullOrEmpty() ? null : BodyTextBox.Text) != Conet.Body);
}
