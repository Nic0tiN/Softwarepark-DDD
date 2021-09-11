
using System.Collections.Generic;
using System.Text.Json;
using DDDExercice1.Domain.Event;

namespace TestDDDExercice1.Fixture
{
    public class InMemoryLogger
    {
        public List<TransportEvent> EventLogs { get; } = new List<TransportEvent>();
        private JsonSerializerOptions _options = new JsonSerializerOptions
        {
            IgnoreNullValues = true
        };

        public override string ToString()
        {
            return JsonSerializer.Serialize(EventLogs, _options);
        }
    }
}