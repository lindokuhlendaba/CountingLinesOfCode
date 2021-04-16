using System.IO;
using CountingLinesOfCode;
using FluentAssertions;
using NUnit.Framework;

namespace CountingLinesOfCodeTests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void GivenFileWithMultipleLinesAndMultipleCommentStyles_ShouldReturnCorrectLineCount()
        {
            //Arrange
            var fileReader = new FileReader();
            var codeLineCounter = new CodeLineCounter();
            var filePath = GetFilePath("FileWithMultipleLinesAndComments.cs");
            //Act
            var content = fileReader.Read(filePath);
            var result = codeLineCounter.CountLinesOfCode(content);
            //Assert
            result.Should().Be(11);
        }

        [Test]
        public void GivenFileWithMultipleMultiLineCommentImplementation_ShouldReturnCorrectLineCount()
        {
            //Arrange
            var fileReader = new FileReader();
            var codeLineCounter = new CodeLineCounter();
            var filePath = GetFilePath("FileWithMultipleLineComments.txt");
            //Act
            var content = fileReader.Read(filePath);
            var result = codeLineCounter.CountLinesOfCode(content);
            //Assert
            result.Should().Be(19);
        }

        private static string GetFilePath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), $@"files\{fileName}");
        }
    }
}