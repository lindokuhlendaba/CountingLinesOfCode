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
                const string content = "var things = 2;";
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 1);
            }

            [TestCase("//this is a comment\r\nvar things = 2;")]
            [TestCase("throw new Exception();\r\n/*Throwing for days*/")]
            [TestCase("/* Today is Friday */\r\n return 5;")]
            [TestCase("if(thing == \"bob\")\r\n//Only on Tuesdays")]
            public void GivenOneCodeLineAndOneCommentLine_ShouldReturnOne(string content)
            {
                //arrange
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 1);
            }

            [Test]
            public void GivenTwoLinesOfCode_ShouldReturnTwo()
            {
                //arrange
                const string content = "public class FooThing\r\n{";
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 2);
            }

            [TestCase("public class FooThing\r\n")]
            [TestCase("\r\nvar lineOfCode='this is a line of code';\r\n")]
            public void GivenOneLineOfCodeAndOneEmptyLine_ShouldReturnOne(string content)
            {
                //arrange
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 1);
            }

            [TestCase("\r\n//Im running out of comments to write\r\n")]
            [TestCase("\r\n/*Im running out of comments to write */\r\n")]
            public void GivenOneCommentLineAndOneEmptyLine_ShouldReturnZero(string content)
            {
                //arrange
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 0);
            }

            [Test]
            public void GivenTwoCommentLines_ShouldReturnZero()
            {
                //arrange
                const string content = "/* I need to refactor this */\r\n// I never get round to refactoring";
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 0);
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

            [Test]
            public void GivenOnlyMultiLineCommentThatSpansMultipleLines_ShouldReturnZero()
            {
                //arrange
                var content = "/*this is something\r\n*more text\r\n*/";
                var sut = CreateSut();

                //act
                var result = sut.CountLinesOfCode(content);

                //assert
                Assert.AreEqual(result, 0);
            }
        }

        private static CodeLineCounter CreateSut()
        {
            return new CodeLineCounter();
        }
    }
}