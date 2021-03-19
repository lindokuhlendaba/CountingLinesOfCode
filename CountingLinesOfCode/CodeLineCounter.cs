using System;
using System.Linq;

namespace CountingLinesOfCode
{
    public class CodeLineCounter
    {
        private const string MultiLineStart = "/*";
        private const string MultiLineEnd = "*/";
        private const string SingleLineStart = "//";

        public int CountLinesOfCode(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content), "Cannot process file content of null");
            }

            var lines = content.Split("\r\n").Select(x => x.Trim('\t', ' ')).ToList();

            return lines.Count(IsLineOfCode);
        }

        private static bool IsLineOfCode(string line)
        {
            if (line == string.Empty)
            {
                return false;
            }

            if (line.StartsWith(SingleLineStart))
            {
                return false;
            }

            if (IsWholeLineAMultiLineComment(line))
            {
                return false;
            }

            return HasCode(line);
        }

        private static bool IsWholeLineAMultiLineComment(string line)
        {
            return (line.StartsWith(MultiLineStart) && line.EndsWith(MultiLineEnd));
        }

        private static bool HasCode(string line)
        {
            var codeSegments = line.Split(MultiLineEnd).Select(x => x.Trim()).ToList();
            return codeSegments.Count == 1 || codeSegments.Skip(1).Any(IsCode);
        }

        private static bool IsCode(string segment)
        {
            return !segment.StartsWith(MultiLineStart) && !segment.StartsWith(SingleLineStart);
        }
    }
}