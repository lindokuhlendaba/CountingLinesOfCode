using System;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace CountingLinesOfCodeTests
{
    [TestFixture]
    public class FileReaderTests
    {
        public class Read
        {
            [Test]
            public void GivenEmptyFile_ShouldReturnEmptyContent()
            {
                //arrange
                var sut = new FileReader();
                var filePath = GetFilePath("FileWithoutContents.cs");
                //act
                var content = sut.Read(filePath);
                //assert
                content.Should().Be(string.Empty);
            }


            [Test]
            public void GivenFileNotFound_ShouldThrowAFileNotFoundException()
            {
                //arrange
                var sut = new FileReader();
                var filePath = GetFilePath("123.txt");
                const string expectedException = "File not found";
                //act
                var exception = Assert.Throws<FileNotFoundException>(() => sut.Read(filePath));
                //assert
                exception.Message.Should().Be(expectedException);
                exception.FileName.Should().Be(filePath);
            }

            [Test]
            public void GivenFileWith1Line_ShouldReturnTheLineContent()
            {
                //arrange
                var sut = new FileReader();
                var filePath = GetFilePath("FileWith1Line.cs");
                const string expected = "interface IFile { }";
                //act
                var content = sut.Read(filePath);
                //assert
                content.Should().Be(expected);
            }

            [Test]
            public void GivenFileWithMultipleLines_ShouldReturnAllContent()
            {
                //arrange
                var sut = new FileReader();
                var filePath = GetFilePath("FileWithMultipleLines.cs");
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("namespace CountingLinesOfCodeTests.files");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("public class FileWithMultipleLines");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("}");
                stringBuilder.Append("}");
                //act
                var content = sut.Read(filePath);
                //assert
                var expected = stringBuilder.ToString();
                content.Should().Be(expected);
            }

            private static string GetFilePath(string fileName)
            {
                return Path.Combine(Directory.GetCurrentDirectory(), $@"files\{fileName}");
            }

        }
    }

    public class FileReader
    {
        public string Read(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            var allText = File.ReadAllText(filePath);
            return allText;
        }
    }
}
