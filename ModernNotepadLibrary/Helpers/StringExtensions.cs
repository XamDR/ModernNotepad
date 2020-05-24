using System;
using System.Collections.Generic;

namespace ModernNotepadLibrary.Helpers
{
    static class StringExtensions
    {
        public static IEnumerable<int> AllIndexesOf(this string str, string value, StringComparison comparisonType)
        {
            if (string.IsNullOrEmpty(value))
            {
                yield break;
            }
            int index = str.IndexOf(value, comparisonType);

            while (index != -1)
            {
                yield return index;
                index = str.IndexOf(value, index + 1, comparisonType);                
            }
        }

        public static IEnumerable<int> AllIndexesOf(this string str, string value) 
            => str.AllIndexesOf(value, StringComparison.OrdinalIgnoreCase);
    }
}
