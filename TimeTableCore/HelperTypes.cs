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
}

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}