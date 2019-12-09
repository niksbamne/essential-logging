﻿using Microsoft.Extensions.Logging;

namespace LoggingPerformanceBenchmark
{
    public class LoggerRunner : RunnerBase
    {
        private readonly string _description;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger1;
        private readonly ILogger _logger2;
        
        public LoggerRunner(string description, ILoggerFactory loggerFactory, string category1Name, string category2Name)
        {
            _description = description;
            _loggerFactory = loggerFactory;
            _logger1 = loggerFactory.CreateLogger(category1Name);
            _logger2 = loggerFactory.CreateLogger(category2Name);
        }
        
        public override string Name
        {
            get
            {
                return "ILogger:" + _description;
            }
        }

        protected override void LogCritical9101(int id, string message)
        {
            _logger1.LogCritical(id, message);
        }

        protected override void LogCritical9102(int id, string message)
        {
            _logger1.LogCritical(id, message);
        }

        protected override void LogWarning4201(int id, string message, int counter2)
        {
            _logger2.LogWarning(id, message, counter2);
        }

        protected override void LogWarning4202(int id, string message, int counter2)
        {
            _logger2.LogWarning(id, message, counter2);
        }

        protected override void LogWarning4203(int id, string message, int counter2)
        {
            _logger2.LogWarning(id, message, counter2);
        }

        protected override void LogDebug31001(int id, string message, int data1, string data2)
        {
            _logger1.LogDebug(id, message, data1, data2);
        }

        protected override void LogDebug31002(int id, string message, int data1)
        {
            _logger1.LogDebug(id, message, data1);
        }

        protected override void LogDebug32003(int id, string message, int data1, string data2)
        {
            _logger2.LogDebug(id, message, data1, data2);
        }

        protected override void LogDebug32004(int id, string message, int data1)
        {
            _logger2.LogDebug(id, message, data1);
        }
    }
}
