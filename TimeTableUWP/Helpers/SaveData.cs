#nullable enable
// #define CONET_DONT_SAVE

using Windows.Storage;
using System.Runtime.Serialization;
using static RollingRess.Serializer;
using TimeTableUWP.Conet;

namespace TimeTableUWP;
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
        localSettings.Values[SettingValues.Subjects] = Serialize(new SubjectTuple(Korean.Selected, ttc::Math.Selected, Social.Selected, Language.Selected, Global1.Selected, Global2.Selected));
        localSettings.Values[SettingValues.Level] = Serialize(Info.User.ActivationLevel);
        localSettings.Values[SettingValues.Conet] = Serialize(Info.User.Conet);
        localSettings.Values[SettingValues.Settings] = Serialize(Info.Settings);
        localSettings.Values[SettingValues.Todo] = Serialize(TodoListPage.TaskList.List);

#if !CONET_DONT_SAVE

        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
        roamingSettings.Values[SettingValues.Egg] = Info.User.Conet?.Eggs.Value;//Uint32
#endif
    }

    public static void Load()
    {
        if (Deserialize<Version?>(localSettings.Values[SettingValues.Version]) is Version version)
            Info.User.Status = version != Info.Version ? LoadStatus.Updated : LoadStatus.Normal;

        else
        {
            Info.User.Status = LoadStatus.NewlyInstalled;
            return;
        }

        if (Deserialize<SubjectTuple>(localSettings.Values[SettingValues.Subjects]) is SubjectTuple list)
            (Korean.Selected, ttc::Math.Selected, Social.Selected, Language.Selected, Global1.Selected, Global2.Selected) = list;

        if (Deserialize<Settings>(localSettings.Values[SettingValues.Settings]) is Settings setting)
            Info.Settings = setting;

#if !CONET_DONT_SAVE
        if (Deserialize<ConetUser>(localSettings.Values[SettingValues.Conet]) is ConetUser conet)
            Info.User.Conet = conet;

        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
        if (roamingSettings.Values[SettingValues.Egg] is UInt32 egg && Info.User.Conet is not null)
            Info.User.Conet.Eggs = egg;
#endif

        if (localSettings.Values[SettingValues.Class] is int cls)
            Info.User.Class = cls;

        if (Deserialize<List<Todo.TodoTask>>(localSettings.Values[SettingValues.Todo]) is List<Todo.TodoTask> tasklist)
            TodoListPage.TaskList.List = tasklist;

        if (Deserialize<ActivationLevel>(localSettings.Values[SettingValues.Level]) is ActivationLevel level)
            Info.User.ActivationLevel = level;
    }

    [DataContract(Name = "Subjects")]
    public class SubjectTuple
    {
        public SubjectTuple(Subject kor, Subject math, Subject soc, Subject lang, Subject glo1, Subject glo2)
        {
            Korean = kor;
            Math = math;
            Social = soc;
            Language = lang;
            Global1 = glo1;
            Global2 = glo2;
        }
        [DataMember] public Subject Korean { get; set; }
        [DataMember] public Subject Math { get; set; }
        [DataMember] public Subject Social { get; set; }
        [DataMember] public Subject Language { get; set; }
        [DataMember] public Subject Global1 { get; set; }
        [DataMember] public Subject Global2 { get; set; }

        public void Deconstruct(out Subject kor, out Subject math, out Subject soc, out Subject lang, out Subject glo1, out Subject glo2)
        {
            kor = Korean;
            math = Math;
            soc = Social;
            lang = Language;
            glo1 = Global1;
            glo2 = Global2;
        }

    }
}
