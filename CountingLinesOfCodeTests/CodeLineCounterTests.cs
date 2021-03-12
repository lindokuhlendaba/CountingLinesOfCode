using CountingLinesOfCode;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CountingLinesOfCodeTests
{
    public class CodeLineCounterTests
    {
        public class CountLinesOfCode
        {
            [Test]
            public void GivenNoContent_ShouldReturnZero()
            {
                //arrange
                var content = string.Empty;
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 0);
            }

            [Test]
            public void GivenOneLineWithWhitespace_ShouldReturnZero()
            {
                //arrange
                var content = "   ";
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 0);
            }

            [Test]
            public void GivenContentIsNull_ShouldThrow()
            {
                //arrange
                string content = null;
                var sut = CreateSut();
                var expected = "Cannot process file content of null";

                //act
                var exception = Assert.Throws<ArgumentNullException>(() => sut.CountLinesOfCode(content));

                //assert
                exception.Message.Should().Contain(expected);
            }

            [TestCase("/*Some commented out line that is here*/")]
            [TestCase("//Some commented out line that is here")]
            [TestCase("/*this is a comment*/ // things")]
            public void GivenContentContainsOneLineWithAComment_ShouldReturnZero(string content)
            {
                //arrange
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                result.Should().Be(0);
            }

            [Test]
            public void GivenOneLineOnlyCode_ShouldReturnOne()
            {
                //arrange
                var content = "var things = 2;";
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 1);
            }

            [Test]
            public void GivenOneCodeLineAndOneCommentLine_ShouldReturnOne()
            {
                //arrange
                var content = "//this is a comment\nvar things = 2;";
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 1);
            }
            
            [TestCase("/*this is a comment*/var things = 2;")]
            [TestCase("var things = 2;/*this is a comment*/")]
            [TestCase("/*this is a comment*/var things = 2; //comment")]
            public void GivenMultiStyleLineCommentAndCodeOnSingleLine_ShouldReturnOne(string content)
            {
                //arrange
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 1);
            }
        }

        private static CodeLineCounter CreateSut()
        {
            return new CodeLineCounter();
        }

    }
    
}