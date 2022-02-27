#nullable enable

namespace TimeTableUWP;

public class User : BaseUser
{
    public LoadStatus Status { get; set; }
    public ActivationLevel ActivationLevel { get; set; } = ActivationLevel.None;
    public bool IsActivated => ActivationLevel != ActivationLevel.None;
    public bool IsSpecialLevel => ActivationLevel is ActivationLevel.Developer or ActivationLevel.Azure or ActivationLevel.Bisque;

    /// <summary>
    /// Shows activation dialog and activate.
    /// </summary>
    /// <param name="msg">The first line showing in activation dialog. If null is given, then shows defualt message</param>
    /// <returns>true if activated. Otherwise, false</returns>
    public static async Task<bool> ActivateAsync(string? msg = null)
    {
        ActivateDialog activateDialog = msg is null ? new() : new(msg);
        ContentDialogResult activeSelection = await activateDialog.ShowAsync();

        if (activeSelection is not ContentDialogResult.Primary || Info.User.ActivationLevel is ActivationLevel.None)
            return false;

        string license = Info.User.ActivationLevel switch
        {
            ActivationLevel.Developer => "developer",
            ActivationLevel.Azure => "Azure",
            ActivationLevel.Bisque => "Bisque",
            ActivationLevel.Coral => "Coral",
            ActivationLevel.None or _ => throw new DataAccessException("MainPage.Activate(): ActivationLevel value error"),
        };
        await ShowMessageAsync($"Activated as {license}.", "Activated successfully", Info.Settings.Theme);
        return true;
    }

    /// <summary>
    /// Shows activation dialog and activate as Azure/Bisque.
    /// </summary>
    /// <param name="msg">The first line showing in activation dialog. If null is given, then shows defualt message</param>
    /// <returns>true if activated as Azure/Bisque. Otherwise, false</returns>
    public static async Task<bool> AuthorAsync(string? msg = null, bool showMessage = true)
    {
        if (Info.User.IsSpecialLevel)
            return true;

        _ = await ActivateAsync(msg ?? "Azure / Bisque 레벨 전용 기능입니다.");
        if (!Info.User.IsSpecialLevel)
        {
            if (showMessage)
                await ShowMessageAsync("You need to be Azure/Bisque level to use this feature", "Limited feature", Info.Settings.Theme);
            return false;
        }
        return true;
    }
}