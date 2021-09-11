using System.Linq;
using DDDExercice1.Application;
using DDDExercice1.Infrastructure.Logger;
using NUnit.Framework;
using TestDDDExercice1.Fixture;

namespace TestDDDExercice1
{
    public class Tests
    {

        [Test]
        [TestCase("A", 15)]
        [TestCase("AB", 15)]
        [TestCase("BB", 10)]
        [TestCase("ABB", 15)]
        [TestCase("AABABBAB", 29)]
        [TestCase("ABBBABAAABBB", 44)]
        public void TestDelivery(string route, int expectedResult)
        {
            var application = new WorldApplication(route.Select(x => x.ToString()));
            application.Delivery();
            Assert.AreEqual(expectedResult, WorldApplication.CurrentTime.TotalHours);
        }

        [Test]
        [TestCase("A")]
        public void TestEventRouteA(string route)
        {
            string expectedLog =
@"[{""event"":""LOAD"",""time"":0,""duration"":0,""transport_id"":0,""kind"":""TRUCK"",""location"":""FACTORY"",""destination"":""PORT"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},{""event"":""DEPART"",""time"":0,""transport_id"":0,""kind"":""TRUCK"",""location"":""FACTORY"",""destination"":""PORT"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},{""event"":""ARRIVE"",""time"":1,""transport_id"":0,""kind"":""TRUCK"",""location"":""PORT"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},{""event"":""UNLOAD"",""time"":1,""duration"":0,""transport_id"":0,""kind"":""TRUCK"",""location"":""PORT"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},{""event"":""DEPART"",""time"":1,""transport_id"":0,""kind"":""TRUCK"",""location"":""PORT"",""destination"":""FACTORY""},{""event"":""LOAD"",""time"":1,""duration"":1,""transport_id"":2,""kind"":""SHIP"",""location"":""PORT"",""destination"":""A"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},{""event"":""ARRIVE"",""time"":2,""transport_id"":0,""kind"":""TRUCK"",""location"":""FACTORY""},{""event"":""DEPART"",""time"":2,""transport_id"":2,""kind"":""SHIP"",""location"":""PORT"",""destination"":""A"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},{""event"":""ARRIVE"",""time"":8,""transport_id"":2,""kind"":""SHIP"",""location"":""A"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},{""event"":""UNLOAD"",""time"":8,""duration"":1,""transport_id"":2,""kind"":""SHIP"",""location"":""A"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},{""event"":""DEPART"",""time"":9,""transport_id"":2,""kind"":""SHIP"",""location"":""A"",""destination"":""PORT""},{""event"":""ARRIVE"",""time"":15,""transport_id"":2,""kind"":""SHIP"",""location"":""PORT""}]";
            var application = new WorldApplication(route.Select(x => x.ToString()));
            var logger = new InMemoryLogger();
            application.TransportEventHandler += (sender, @event) => logger.EventLogs.Add(@event);
            application.Delivery();
            Assert.AreEqual(expectedLog, logger.ToString());
        }

        [Test]
        [TestCase("AB")]
        public void TestEventRouteAB(string route)
        {
            string expectedLog =
@"[{""event"":""LOAD"",""time"":0,""duration"":0,""transport_id"":0,""kind"":""TRUCK"",""location"":""FACTORY"",""destination"":""PORT"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},
{""event"":""DEPART"",""time"":0,""transport_id"":0,""kind"":""TRUCK"",""location"":""FACTORY"",""destination"":""PORT"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},
{""event"":""LOAD"",""time"":0,""duration"":0,""transport_id"":1,""kind"":""TRUCK"",""location"":""FACTORY"",""destination"":""B"",""cargo"":[{""cargo_id"":1,""destination"":""B"",""origin"":""FACTORY""}]},
{""event"":""DEPART"",""time"":0,""transport_id"":1,""kind"":""TRUCK"",""location"":""FACTORY"",""destination"":""B"",""cargo"":[{""cargo_id"":1,""destination"":""B"",""origin"":""FACTORY""}]},
{""event"":""ARRIVE"",""time"":1,""transport_id"":0,""kind"":""TRUCK"",""location"":""PORT"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},
{""event"":""UNLOAD"",""time"":1,""duration"":0,""transport_id"":0,""kind"":""TRUCK"",""location"":""PORT"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},
{""event"":""DEPART"",""time"":1,""transport_id"":0,""kind"":""TRUCK"",""location"":""PORT"",""destination"":""FACTORY""},
{""event"":""LOAD"",""time"":1,""duration"":1,""transport_id"":2,""kind"":""SHIP"",""location"":""PORT"",""destination"":""A"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},
{""event"":""ARRIVE"",""time"":2,""transport_id"":0,""kind"":""TRUCK"",""location"":""FACTORY""},
{""event"":""DEPART"",""time"":2,""transport_id"":2,""kind"":""SHIP"",""location"":""PORT"",""destination"":""A"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},
{""event"":""ARRIVE"",""time"":5,""transport_id"":1,""kind"":""TRUCK"",""location"":""B"",""cargo"":[{""cargo_id"":1,""destination"":""B"",""origin"":""FACTORY""}]},
{""event"":""UNLOAD"",""time"":5,""duration"":0,""transport_id"":1,""kind"":""TRUCK"",""location"":""B"",""cargo"":[{""cargo_id"":1,""destination"":""B"",""origin"":""FACTORY""}]},
{""event"":""DEPART"",""time"":5,""transport_id"":1,""kind"":""TRUCK"",""location"":""B"",""destination"":""FACTORY""},
{""event"":""ARRIVE"",""time"":8,""transport_id"":2,""kind"":""SHIP"",""location"":""A"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},
{""event"":""UNLOAD"",""time"":8,""duration"":1,""transport_id"":2,""kind"":""SHIP"",""location"":""A"",""cargo"":[{""cargo_id"":0,""destination"":""A"",""origin"":""FACTORY""}]},
{""event"":""DEPART"",""time"":9,""transport_id"":2,""kind"":""SHIP"",""location"":""A"",""destination"":""PORT""},
{""event"":""ARRIVE"",""time"":10,""transport_id"":1,""kind"":""TRUCK"",""location"":""FACTORY""},
{""event"":""ARRIVE"",""time"":15,""transport_id"":2,""kind"":""SHIP"",""location"":""PORT""}]
";
            var application = new WorldApplication(route.Select(x => x.ToString()));
            var logger = new InMemoryLogger();
            var fileLogger = new FileLogger(@".\domain-event.json");
            application.TransportEventHandler += (sender, @event) => logger.EventLogs.Add(@event);
            application.TransportEventHandler += (sender, @event) => fileLogger.Log(@event);
            application.Delivery();
            Assert.AreEqual(expectedLog.Replace("\r\n", null), logger.ToString());
        }
    }
}