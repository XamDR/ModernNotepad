using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ModernNotepadLibrary.Helpers
{
    static class StringExtensions
    {
        public static IEnumerable<int> AllIndexesOf(this string str, string value, StringComparison comparisonType) 
            => comparisonType == StringComparison.OrdinalIgnoreCase
                ? Regex.Matches(str, value, RegexOptions.IgnoreCase).Select(m => m.Index)
                : Regex.Matches(str, value).Select(m => m.Index);

        public static IEnumerable<int> AllIndexesOf(this string str, string value) 
            => str.AllIndexesOf(value, StringComparison.OrdinalIgnoreCase);
    }
}
