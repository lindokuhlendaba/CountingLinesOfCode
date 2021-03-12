using System;
using FluentAssertions;
using NUnit.Framework;

namespace CountingLinesOfCodeTests
{
    public class FileReaderTests
    {
        public class Read
        {
            [Test]
            public void GivenEmptyFile_ShouldReturnEmptyContent()
            {
                //arrange
                var sut = new FileReader();
                var filePath = "";
                //act
                var content = sut.Read(filePath);
                //assert
                content.Should().Be(string.Empty);
            }
        }
    }

    public class FileReader
    {
        public string Read(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
