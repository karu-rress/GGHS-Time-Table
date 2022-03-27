#nullable enable

using Windows.UI.Xaml.Media.Animation;

namespace TimeTableUWP.Conet;

public sealed partial class ConetAddPage : Page
{
    public static ConetHelp? Conet { get; set; }

    public ConetAddPage()
    {
        InitializeComponent();
        idText.Text = $"{Info.User.Conet!.Id} {Info.User.Conet.Name}";

        if (Conet is not null) // Not creating, but modifying
        {
            idText.Text = $"{Conet.Uploader.Id} {Conet.Uploader.Name}";
            mainText.Text = "Conet Details";
            TitleTextBox.Text = Conet.Title;
            eggTextBox.Text = $"{Conet.Price?.Value}";
            BodyTextBox.Text = Conet.Body ?? string.Empty;

            // 다른 사람 글이라면
            if (Conet.Uploader.Id != Info.User.Conet.Id)
            {
                ReadOnly(TitleTextBox, eggTextBox, BodyTextBox);
                Disable(PostButton);
            }
            // 내가 쓴 글이라면
            else
            {
                Visible(DeleteButton);
            }
        }
    }

    private async void PostButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
        {
            await ShowMessageAsync("제목을 입력하세요.", "Error", Info.Settings.Theme);
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

            ConetHelp conet = new(DateTime.Now, Info.User.Conet!, TitleTextBox.Text,
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
            || (BodyTextBox.IsNullOrEmpty() ? null : BodyTextBox.Text) != Conet.Body
            || (eggTextBox.IsNullOrEmpty() ? null : eggTextBox.Text) != Conet.Price.ToString());
}
