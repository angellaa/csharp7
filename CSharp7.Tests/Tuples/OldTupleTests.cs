using System;
using NUnit.Framework;

namespace CSharp7.Tests.Tuples
{
    public class OldTupleTests
    {
        [Test]
        public void Items()
        {
            var tuple = new Tuple<string, int>("Andrea", 32);

            Assert.That(tuple.Item1, Is.EqualTo("Andrea"));
            Assert.That(tuple.Item2, Is.EqualTo(32));
        }

        [Test]
        public void String()
        {
            var tuple = Tuple.Create("A", 32, true);

            Assert.That(tuple.ToString(), Is.EqualTo("(A, 32, True)"));
        }

        [Test]
        public void Equality()
        {
            var tuple1 = Tuple.Create("A", 32, true);
            var tuple2 = Tuple.Create("A", 32, true);

            Assert.That(tuple1, Is.EqualTo(tuple2));
        }
    }
}