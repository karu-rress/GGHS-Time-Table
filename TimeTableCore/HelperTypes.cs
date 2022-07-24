namespace TimeTableCore
{
    public enum LoadStatus
    {
        NewlyInstalled,
        Updated,
        Normal,
    }

    /*
     * 
     *  Activation Levels
     *  
     *  Developer: Karu, full access to GGHS Time Table
     *  Azure: The ones who helped making GTT
     *  Bisque: The ones who used GTT a lot
     *  Coral: Normal GGHS 10th
     *  None: Not activated
     *  
     */
    public enum ActivationLevel
    {
        Developer,
        Azure,
        Bisque,
        Coral,
        None,
    }

    public class BaseUser
    {
        public int Class { get; set; }
    }
}

namespace System.Runtime.CompilerServices
{
    public static class IsExternalInit { }
}

[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public sealed class GTT6Attribute : System.Attribute
{

}