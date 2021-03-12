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

            if (content.Trim() == string.Empty)
            {
                return 0;
            }

            var lines = content.Split("\n").ToList();

            return lines.Count(x => !IsCommentedLine(x));
        }

        private static bool IsCommentedLine(string line)
        {
            if (line.StartsWith(SingleLineStart))
            {
                return true;
            }

            if (IsWholeLineAMultiLineComment(line))
            {
                return true;
            }

            return !HasCode(line);
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