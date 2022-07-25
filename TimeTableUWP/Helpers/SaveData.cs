#nullable enable
// #define CONET_DONT_SAVE

using System.Runtime.Serialization;
using TimeTableUWP.Conet;
using Windows.Storage;
using static RollingRess.Serializer;

namespace TimeTableUWP;

[GTT6]
public class DataSaver
{
    private static ApplicationDataContainer localSettings => ApplicationData.Current.LocalSettings;

    private static class SettingValues
    {
        public const string Version = "Version";
        public const string Class = "Class"; 
        public const string Subjects = "Subjects";
        public const string Level = "ActivationLevel";
        public const string Settings = "Settings";
        public const string Todo = "TodoList";
        public const string Conet = "ConetUser";
        public const string Egg = "ConetEgg"; 
    }

    public static void Save()
    {
        localSettings.Values[SettingValues.Version] = Serialize(Info.Version);
        localSettings.Values[SettingValues.Class] = Info.User.Class;
        localSettings.Values[SettingValues.Subjects] = Serialize(new SubjectTuple(Social.Selected, Language.Selected, Global1.Selected, Global2.Selected));
        localSettings.Values[SettingValues.Level] = Serialize(Info.User.ActivationLevel);
        localSettings.Values[SettingValues.Conet] = Serialize(Info.User.Conet);
        localSettings.Values[SettingValues.Settings] = Serialize(Info.Settings);
        localSettings.Values[SettingValues.Todo] = Serialize(TodoListPage.TaskList.List);

#if !CONET_DONT_SAVE
        localSettings.Values[SettingValues.Egg] = Info.User.Conet?.Eggs.Value;
#endif
    }

    // value maybe null if return value is false
    public static bool SettingExists<T>(string key, out T value)
    {
        if (Deserialize<T>(localSettings.Values[key]) is T v)
        {
            value = v;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    public static void Load()
    {
        // TODO: ? 없어도 작동하는지
        if (SettingExists<Version>(SettingValues.Version, out var version))
            Info.User.Status = version != Info.Version ? LoadStatus.Updated : LoadStatus.Normal;      

        else // Version not exists? Newly installed.
        {
            Info.User.Status = LoadStatus.NewlyInstalled;
            return;
        }


        // Initializing code sharing with legacy versions
        if (localSettings.Values[SettingValues.Class] is int cls)
            Info.User.Class = cls;

        // SettingExists<Version>(SettingValues.Version, out var version)
        if (SettingExists<ActivationLevel>(SettingValues.Level, out var level))
            Info.User.ActivationLevel = level;

        if (version.IsUpgradedFromGTT5) // Upgraded from GTT5?
        {
            // Cleans legacy settings
            localSettings.Values.Remove(SettingValues.Subjects);
            localSettings.Values.Remove(SettingValues.Settings);
            localSettings.Values.Remove(SettingValues.Conet);
            localSettings.Values.Remove(SettingValues.Egg);
            localSettings.Values.Remove(SettingValues.Todo);
            return;
        }

        if (SettingExists<SubjectTuple>(SettingValues.Subjects, out var list))
            (Social.Selected, Language.Selected, Global1.Selected, Global2.Selected) = list;

        if (SettingExists<Settings>(SettingValues.Settings, out var setting))
            Info.Settings = setting;

#if !CONET_DONT_SAVE
        if (SettingExists<ConetUser>(SettingValues.Conet, out var conet))
            Info.User.Conet = conet;

        if (localSettings.Values[SettingValues.Egg] is uint egg && Info.User.Conet is not null)
            Info.User.Conet.Eggs = egg;
#endif

        if (SettingExists<List<Todo.TodoTask>>(SettingValues.Todo, out var tasklist))
            TodoListPage.TaskList.List = tasklist;
    }

    [DataContract(Name = "Subjects")]
    public class SubjectTuple
    {
        public SubjectTuple(Subject soc, Subject lang, Subject glo1, Subject glo2)
        {
            Social = soc;
            Language = lang;
            Global1 = glo1;
            Global2 = glo2;
        }
        [DataMember] public Subject Social { get; set; }
        [DataMember] public Subject Language { get; set; }
        [DataMember] public Subject Global1 { get; set; }
        [DataMember] public Subject Global2 { get; set; }

        public void Deconstruct(out Subject soc, out Subject lang, out Subject glo1, out Subject glo2)
        {
            soc = Social;
            lang = Language;
            glo1 = Global1;
            glo2 = Global2;
        }
    }
}
