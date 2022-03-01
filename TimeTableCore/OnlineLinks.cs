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
                // public static OnlineLink 언어와매체 { get; set; } = new();
                // TODO
            }

            public OnlineDictionary Class1 { get; } = new()
            {
                [Global1.SocialResearch.Name] = new() { Classroom = "https://classroom.google.com/c/NDc0ODM1NDQ3MDk0", Teacher = "김용지" }
            };

            public OnlineDictionary Class2 { get; } = new()
            {

            };

            public OnlineDictionary Class3 { get; } = new()
            {

            };

            public OnlineDictionary Class4 { get; } = new()
            {

            };

            public OnlineDictionary Class5 { get; } = new()
            {

            };

            public OnlineDictionary Class6 { get; } = new()
            {

            };

            public OnlineDictionary Class7 { get; } = new()
            {

            };

            public OnlineDictionary Class8 { get; } = new()
            {

            };
        }
    }
}

