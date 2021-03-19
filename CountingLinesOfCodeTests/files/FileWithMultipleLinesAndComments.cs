/* This class does things that will change and I wont update this comment, because ill forget */
namespace CountingLinesOfCodeTests.files
{
    public class FileWithMultipleLinesAndComments
    {

        //public FooBar(string bar)
        //{
        //    Bar = bar;
        //}

        /* I dont know what this is for */
        private int Bar { get; set; } // Please dont comment like this

        // Give them the foo
        public string GiveMeTheFoo()
        {
            return "Foo"; // Foo is only for Bar
        }
    }
}