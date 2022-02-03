using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableCore
{
    public class User
    {
        public LoadStatus Status { get; set; }
        public int Class { get; set; }
        public ActivationLevel ActivationLevel { get; set; } = ActivationLevel.None;
        public bool IsActivated => ActivationLevel != ActivationLevel.None;
        public bool IsDeveloperOrInsider => ActivationLevel is ActivationLevel.Developer or ActivationLevel.ShareTech or ActivationLevel.Insider;
        public bool IsNotDeveloperOrInsider => !IsDeveloperOrInsider;
    }
}
