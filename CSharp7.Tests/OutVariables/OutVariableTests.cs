using NUnit.Framework;

namespace CSharp7.Tests.OutVariables
{
    public class OutVariableTests
    {
        [Test]
        public void TryParse_UsingTheOldApproach()
        {
            int n;
            var isInt = int.TryParse("1", out n);

            Assert.That(isInt, Is.True);
            Assert.That(n, Is.EqualTo(1));
        }

        [Test]
        public void TryParseWithOutVariable()
        {
            var isInt = int.TryParse("1", out int n);

            Assert.That(isInt, Is.True);
            Assert.That(n, Is.EqualTo(1));
        }

        [Test]
        public void TryParse_IgnoringOutVariables()
        {
            var isInt1 = int.TryParse("3.5", out var _);
            var isInt2 = int.TryParse("305", out var _); 

            Assert.That(isInt1, Is.False);
            Assert.That(isInt2, Is.True);

            // The following does not compile. _ is special
            //Assert.That(_, Is.EqualTo(305));
        }

        [Test]
        public void TryParse_UsingOutVariableWithTheSameName()
        {
            int.TryParse("1", out var n);

            // The following does not compile!
            // int.TryParse("2", out var n);

            int.TryParse("2", out n);

            Assert.That(n, Is.EqualTo(2));
        }
    }
}
