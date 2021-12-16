using System;
using System.ComponentModel;
using ZoomDictionary = System.Collections.Generic.Dictionary<string, TimeTableCore.ZoomInfo?>;

namespace TimeTableCore
{
    public record ZoomInfo
    {
        public string? Link { get; init; } = null;
        public string? Id { get; init; } = null;
        public string? Password { get; init; } = null;
        public string? Classroom { get; init; } = null;
        public string Teacher { get; init; } = string.Empty;

        public bool IsZoomAvailable => Id != null && Password != null;
    }
}