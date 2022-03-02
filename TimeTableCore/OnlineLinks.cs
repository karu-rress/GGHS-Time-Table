using OnlineDictionary = System.Collections.Generic.Dictionary<string, TimeTableCore.OnlineLink?>;

namespace TimeTableCore
{
    public record OnlineLink
    {
        public string? Zoom { get; init; } = null;
        public string? Id { get; init; } = null;
        public string? Password { get; init; } = null;
        public string? Classroom { get; init; } = null;
        public string Teacher { get; init; } = string.Empty;

        public bool IsZoomAvailable => Id != null && Password != null;
    }

    public interface IOnlineLinks
    {
        OnlineDictionary Class1 { get; }
        OnlineDictionary Class2 { get; }
        OnlineDictionary Class3 { get; }
        OnlineDictionary Class4 { get; }
        OnlineDictionary Class5 { get; }
        OnlineDictionary Class6 { get; }
        OnlineDictionary Class7 { get; }
        OnlineDictionary Class8 { get; }
    }

    namespace Grade3.Semester1
    {
        public class OnlineLinks : IOnlineLinks
        {
            private record Common
            {
                public static OnlineLink EnglishA1to4 { get; set; } = new()
                {
                    Zoom = "https://zoom.us/j/5472878985?pwd=RlIzSmJnWHBRaWhkRTNkOEJ6UUF5UT09",
                    Id = "547 287 8985",
                    Password = "0512",
                    Classroom = "https://classroom.google.com/c/MjI4MDUyNjE4MjE5",
                    Teacher = "김찬미"
                };

                public static OnlineLink EngLiterature1to3 { get; set; } = new()
                {
                    Zoom = "https://zoom.us/j/3947441557",
                    Id = "394 744 1557",
                    Password = "1234",
                    Classroom = "https://classroom.google.com/c/NDc0NjQ1MTQzMjQy",
                    Teacher = "오경은"
                };

                public static OnlineLink EngLiterature4to8 { get; set; } = EngLiterature1to3 with
                {
                    Zoom = "https://zoom.us/j/5031101343?pwd=QXZMWUVoZEkzVnUyY0poanAyclBHdz09",
                    Id = "503 110 1343",
                    Password = "1111",
                    Teacher = "김한나  "
                };

                public static OnlineLink Reading { get; set; } = new()
                {
                    Classroom = "https://classroom.google.com/c/NDY4NjkyMjU3MDkx",
                    Teacher = "윤채영",
                };
                // public static OnlineLink 언어와매체 { get; set; } = new();
                // TODO
            }

            public OnlineDictionary Class1 { get; } = new()
            {
                [Global1.SocialResearch.Name] = new() { Classroom = "https://classroom.google.com/c/NDc0ODM1NDQ3MDk0", Teacher = "김용지" },

                [Subjects.AdvancedEnglish.Name + 'A'] = Common.EnglishA1to4,

                [Subjects.EnglishLiterature.Name] = Common.EngLiterature1to3,
                [Subjects.Reading.Name] = Common.Reading,
            };

            public OnlineDictionary Class2 { get; } = new()
            {
                [Subjects.AdvancedEnglish.Name + 'A'] = Common.EnglishA1to4,

                [Subjects.EnglishLiterature.Name] = Common.EngLiterature1to3,
                [Subjects.Reading.Name] = Common.Reading,
            };

            public OnlineDictionary Class3 { get; } = new()
            {
                [Subjects.AdvancedEnglish.Name + 'A'] = Common.EnglishA1to4,

                [Subjects.EnglishLiterature.Name] = Common.EngLiterature1to3,
                [Subjects.Reading.Name] = Common.Reading,
            };

            public OnlineDictionary Class4 { get; } = new()
            {
                [Subjects.AdvancedEnglish.Name + 'A'] = Common.EnglishA1to4,

                [Subjects.EnglishLiterature.Name] = Common.EngLiterature4to8,
                [Subjects.Reading.Name] = Common.Reading,
            };

            public OnlineDictionary Class5 { get; } = new()
            {
                [Subjects.EnglishLiterature.Name] = Common.EngLiterature4to8,
                [Subjects.Reading.Name] = Common.Reading,
            };

            public OnlineDictionary Class6 { get; } = new()
            {
                [Subjects.EnglishLiterature.Name] = Common.EngLiterature4to8,
                [Subjects.Reading.Name] = Common.Reading,
            };

            public OnlineDictionary Class7 { get; } = new()
            {
                [Subjects.EnglishLiterature.Name] = Common.EngLiterature4to8,
                [Subjects.Reading.Name] = Common.Reading,
            };

            public OnlineDictionary Class8 { get; } = new()
            {
                [Subjects.EnglishLiterature.Name] = Common.EngLiterature4to8,
                [Subjects.Reading.Name] = Common.Reading,
            };
        }
    }
}

