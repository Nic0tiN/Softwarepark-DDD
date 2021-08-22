using System.Linq;
using DDDExercice1;
using NUnit.Framework;

namespace TestDDDExercice1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("A", 5)]
        [TestCase("AB", 5)]
        [TestCase("BB", 5)]
        [TestCase("ABB", 7)]
        [TestCase("AABABBAB", 29)]
        [TestCase("ABBBABAAABBB", 41)]
        public void TestDelivery(string route, int expectedResult)
        {
            var application = new Application(route.Select(x => x.ToString()));
            application.Delivery();
            Assert.AreEqual(expectedResult, application.CurrentTime.TotalHours);
        }
    }
}