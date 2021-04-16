using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountingLinesOfCode
{
    public static class StringExtensions
    {
        public static bool StartsWithAny(this string line, params string[] startValues)
        {
            return startValues.ToList().Any(line.StartsWith);
        }
    }
}