using System.IO;
using System.Text.Json;
using DDDExercice1.Domain.Event;

namespace DDDExercice1.Infrastructure.Logger
{
    public class FileLogger
    {
        private string _logFilePath;
        private JsonSerializerOptions _options = new JsonSerializerOptions
        {
            IgnoreNullValues = true
        };

        public FileLogger(string logFile)
        {
            _logFilePath = logFile;
            File.WriteAllText(_logFilePath, "");
        }

        public void Log(TransportEvent transportEvent)
        {
            File.AppendAllText(_logFilePath, JsonSerializer.Serialize(transportEvent, _options) + "\r\n");
        }
    }
}
