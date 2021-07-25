using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace RollingRess
{
    public static class Extensions
    {
        /// <summary>
        /// Make the given combobox empty.
        /// </summary>
        public static void MakeEmpty(this ComboBox cb)
            => cb.SelectedIndex = -1;

        /// <summary>
        /// (Extended) Return the string value if it is in the string[] list.
        /// </summary>
        /// <param name="var">this string value</param>
        /// <param name="else">Returned if var is not in array</param>
        /// <param name="array">Containers to check if var is in</param>
        /// <returns>original variable if in the list, else return @else</returns>
        public static string ReturnIfHasInOrElse(this string var, string @else, params string[] array) 
            => Array.IndexOf(array, var) > -1 ? var : @else;

    }

    public static class Librarys
    {
        public static bool IsOneOf(this object item, params object[] options)
        {
            return options.Contains(item);
        }

        public static bool AreNullOrEmpty(params string[] arr)
        {
            foreach (var item in arr)
            {
                if (string.IsNullOrEmpty(item))
                    return true;
            }

            return false;
        }

        public static void EmptyComboBox(ref ComboBox cb)
        => cb.SelectedIndex = -1;

        /// <summary>
        /// Disables all given comboboxes.
        /// </summary>
        /// <param name="controls">All that can have .IsEnabled property</param>
        public static void Disable(params Control[] controls)
        {
            foreach (var item in controls)
                item.IsEnabled = false;
        }

        /// <summary>
        /// Enables all given comboboxes.
        /// </summary>
        /// <param name="controls">All that can have .IsEnabled property</param>
        public static void Enable(params Control[] controls)
        {
            foreach (var item in controls)
                item.IsEnabled = true;
        }

        public static void Empty(params Control[] controls)
        {
            foreach (var item in controls)
            {
                switch (item)
                {
                    case ComboBox cb: 
                        cb.SelectedIndex = -1; 
                        break;
                    default: 
                        throw new ArgumentException();
                }
            }
        }

        public static void AddText(this TextBlock tb, string text)
        {
            tb.Inlines.Add(new Run() { Text = text });
        }

        public static void AddTextLine(this TextBlock tb, string text)
        {
            tb.Inlines.Add(new Run() { Text = text + Environment.NewLine });
        }
    }
}