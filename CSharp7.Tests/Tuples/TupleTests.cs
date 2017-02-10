using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CSharp7.Tests.Tuples
{
    public class TuplesTests
    {
        [Test]
        public void Elements()
        {
            var tuple = ("Andrea", 32);

            Assert.That(tuple.Item1, Is.EqualTo("Andrea"));
            Assert.That(tuple.Item2, Is.EqualTo(32));
        }

        [Test]
        public void NamedElements()
        {
            var tuple = (Name: "Andrea", Age: 32);

            Assert.That(tuple.Name, Is.EqualTo("Andrea"));
            Assert.That(tuple.Age, Is.EqualTo(32));
        }

        [Test]
        public void TargetTyping()
        {
            // Target typing is needed (the tuple literal does not have a natural type)
            (string, int) tuple = (null, 32);

            Assert.That(tuple.Item1, Is.EqualTo(null));
            Assert.That(tuple.Item2, Is.EqualTo(32));
        }

        [Test]
        public void String()
        {
            var tuple = ("A", 32, true);

            Assert.That(tuple.ToString(), Is.EqualTo("(A, 32, True)"));
        }

        [Test]
        public void Equality()
        {
            var tuple1 = ("A", 32, true);
            var tuple2 = ("A", 32, true);

            Assert.That(tuple1, Is.EqualTo(tuple2));
        }

        [Test]
        public void TuplesInFunctions()
        {
            (int X, int Y) GetCenter((int X, int Y) p, (int X, int Y) q)
            {
                return ((p.X + q.X) / 2, (p.Y + q.Y) / 2);
            }

            var center = GetCenter((10, 20), (30, 40));

            Assert.That(center.X, Is.EqualTo(20));
            Assert.That(center.Y, Is.EqualTo(30));
        }

        [Test]
        public async Task TuplesInAsyncFunctions()
        {
            async Task<(int Count, int Sum)> Compute(params int[] numbers)
            {
                var count = await Task.Run(() => numbers.Count());
                var sum = await Task.Run(() => numbers.Sum());

                return (count, sum);
            }

            var result = await Compute(10, 20, 30, 40);

            Assert.That(result, Is.EqualTo((4, 100)));
        }

        [Test]
        public void IdentityConversion()
        {
            Assert.That(Run((1, 2)), Is.EqualTo("(int, int)"));
        }

        [Test]
        public void ImplicitConversion()
        {
            Assert.That(Run((1, 2.1)), Is.EqualTo("(double, double)"));
        }

        private static string Run((int, int) tuple) => "(int, int)";
        public static string Run((double, double) tuple) => "(double, double)";

        [Test]
        public void ListOfTuples()
        {
            var points = new List<(int X, int Y)>()
            {
                (1, 2),
                (3, 4),
                (5, 6)
            };

            var sumX = points.Select(p => p.X).Sum();

            Assert.That(sumX, Is.EqualTo(1 + 3 + 5));
        }

        [Test]
        public void DictionaryWithTuples()
        {
            var dictionary = new Dictionary<(int X, int Y), (string Color, int Size)>
            {
                { (1, 2), ("green", 10) },
                { (2, 3), ("red", 5) }
            };

            Assert.That(dictionary[(1, 2)].Color, Is.EqualTo("green"));
            Assert.That(dictionary[(2, 3)].Size, Is.EqualTo(5));
        }

        [Test]
        public void NestedTuples()
        {
            var point = (Name: "p1", Location: (X: 10, Y: 20));

            Assert.That(point.Name, Is.EqualTo("p1"));
            Assert.That(point.Location.X, Is.EqualTo(10));
            Assert.That(point.Location.Y, Is.EqualTo(20));
        }

        [Test]
        public void TupleLiteralIsNotAConstantExpression()
        {
            var point = (1, 2);

            if (point is ValueTuple<int, int>)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void DeconstructionDeclaration_ToNewVariables()
        {
            var tuple = (Name: "Andrea", Age: 32);

            (string name, var age) = tuple;

            Assert.That(name, Is.EqualTo("Andrea"));
            Assert.That(age, Is.EqualTo(32));
        }

        [Test]
        public void DeconstructionDeclaration_DecostructToNewVarVariables()
        {
            var tuple = (Name: "Andrea", Age: 32);

            var (name, age) = tuple;

            Assert.That(name, Is.EqualTo("Andrea"));
            Assert.That(age, Is.EqualTo(32));
        }

        [Test]
        public void DeconstructionDeclaration_WithUnderscores()
        {
            var tuple = (X: 1, Y: 2, Z: 3);

            // This syntax was proposed but won't make into C# 7 (it does not compile in RC3)
            // (var x, *) = tuple;

            // You can ignore variables using _
            var (x, _, _) = tuple;

            Assert.That(x, Is.EqualTo(1));
        }
        
        [Test]
        public void DeconstructionAssignment_DeconstructToExistingVariables()
        {
            string name;
            int age;

            var tuple = (Name: "Andrea", Age: 32);

            (name, age) = tuple;

            Assert.That(name, Is.EqualTo("Andrea"));
            Assert.That(age, Is.EqualTo(32));
        }

        [Test]
        public void DeconstructCustomType()
        {
            var person = new Person("Andrea", 32);

            var (name, age) = person;
            
            Assert.That(name, Is.EqualTo("Andrea"));
            Assert.That(age, Is.EqualTo(32));
        }

        [Test]
        public void NestedDeconstructionDeclaration()
        {
            var point = (new Person("Andrea", 32), Location: (X: 10, Y: 20));

            var ((name, age), (x, y)) = point;

            Assert.That(name, Is.EqualTo("Andrea"));
            Assert.That(age, Is.EqualTo(32));
            Assert.That(x, Is.EqualTo(10));
            Assert.That(y, Is.EqualTo(20));
        }

        private class Person
        {
            private readonly string name;
            private readonly int age;

            public Person(string name, int age)
            {
                this.name = name;
                this.age = age;
            }

            [UsedImplicitly]
            public void Deconstruct(out string Name, out int Age)
            {
                Name = name;
                Age = age;
            }
        }

        [Test]
        public void Deconstruct_UsingDeconstructAsExtensionMethod()
        {
            var date = new DateTime(2017, 2, 1);

            var (year, month, day) = date;

            Assert.That(year, Is.EqualTo(2017));
            Assert.That(month, Is.EqualTo(2));
            Assert.That(day, Is.EqualTo(1));
        }
    }

    public static class DateTimeExtensions
    {
        public static void Deconstruct(this DateTime date, out int Year, out int Month, out int Day)
        {
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
        }
    }
}