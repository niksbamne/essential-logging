using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace Essential.LoggerProvider.Ecs
{
    public class Log
    {
        public Log(LogLevel level, string logger)
        {
            Level = level;
            Logger = logger;
        }
        
        // log.level = Some examples are warn, err, i, informational ** LogLevel **
        [DataMember(Name = "level")]
        public LogLevel Level { get; set; }
        
        // log.logger = example: org.elasticsearch.bootstrap.Bootstrap ** CategoryName **
        [DataMember(Name = "logger")]
        public string Logger { get; set; }
        
        // log.syslog = The Syslog metadata of the event
        // log.syslog.facility.code
        // log.syslog.priority
        // log.syslog.severity.code => from LogLevel
        // log.syslog.severity.name

    }
}
