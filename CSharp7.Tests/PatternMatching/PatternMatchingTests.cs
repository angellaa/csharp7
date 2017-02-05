using NUnit.Framework;

namespace CSharp7.Tests.PatternMatching
{
    public class PatternMatchingTests
    {
        [Test]
        public void AsExpression_FromOldCSharpVersions()
        {
            object obj = "Andrea";

            string str = obj as string;
            if (str != null)
            {
                Assert.That(str, Is.EqualTo("Andrea"));
            }

            Assert.Fail();
        }

        [Test]
        public void IsExpression_IsTrue()
        {
            object obj = "Andrea";

            bool isString = obj is string str;

            Assert.That(isString, Is.True);

            // The compiler never assign str for optimization!
            // Error: Use of unassigned variable str
            // Assert.That(str, Is.EqualTo("Andrea"));
        }

        [Test]
        public void IsExpression_Match()
        {
            object obj = "Andrea";

            if (obj is string str)
            {
                Assert.That(str, Is.EqualTo("Andrea"));
            }
            else
            {
                Assert.Fail();
            }

            str = "You can access str here!";
        }

        [Test]
        public void IsExpression_NoMatch()
        {
            object obj = 5;

            if (obj is string str)
            {
                Assert.Fail();
            }
            else
            {
                str = "You can access str here!";
            }

            str = "You can access str here!";
        }
    }
}
