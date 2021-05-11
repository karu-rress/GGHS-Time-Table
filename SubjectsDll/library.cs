using System;

namespace RollingRess
{
    public static class Extensions
    {
        public static bool AreNullOrEmpty(params string[] arr)
        {
            foreach (var item in arr)
            {
                if (string.IsNullOrEmpty(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}