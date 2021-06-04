using System;
using Windows.ApplicationModel.Contacts;

namespace TimeTableUWP
{
    public static class Data
    {

        public static Contact karu = new()
        {
            Nickname = "Karu",
            FirstName = "Sunwoo",
            LastName = "Na",
        };

        static Data()
        {
            ContactEmail email = new()
            {
                Address = "nsun527@naver.com",
                Kind = ContactEmailKind.Personal
            };
            karu.Emails.Add(email);
        }
    }

    namespace ComboboxItem
    {
        public static class Grade
        {
            public const string Grade1 = "Grade 1";
            public const string Grade2 = "Grade 2";
            public const string Grade3 = "Grade 3";
        }
        public static class Class
        {
            public const string Class1 = "Class 1";
            public const string Class2 = "Class 2";
            public const string Class3 = "Class 3";
            public const string Class4 = "Class 4";
            public const string Class5 = "Class 5";
            public const string Class6 = "Class 6";
            public const string Class7 = "Class 7";
            public const string Class8 = "Class 8";
        }
    }

    public static class MessageTitle
    {
        public const string FeatrueNotImplemented = "Featrue not implemented yet";
    }
}