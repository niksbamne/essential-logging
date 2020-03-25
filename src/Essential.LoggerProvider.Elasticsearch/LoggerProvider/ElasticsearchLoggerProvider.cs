using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Essential.LoggerProvider
{
    [ProviderAlias("Elasticsearch")]
    public class ElasticsearchLoggerProvider: ILoggerProvider, ISupportExternalScope
    {
        private readonly ConcurrentDictionary<string, ElasticsearchLogger> _loggers;
        private readonly IOptionsMonitor<ElasticsearchLoggerOptions> _options;

        private readonly IDisposable _optionsReloadToken;
        private IExternalScopeProvider _scopeProvider = default!;

        public ElasticsearchLoggerProvider(IOptionsMonitor<ElasticsearchLoggerOptions> options)
        {
            _options = options;
            _loggers = new ConcurrentDictionary<string, ElasticsearchLogger>();
            ReloadLoggerOptions(options.CurrentValue);
            _optionsReloadToken = _options.OnChange(ReloadLoggerOptions);
        }

        public ILogger CreateLogger(string name)
        {
            return _loggers.GetOrAdd(name,
                loggerName =>
                    new ElasticsearchLogger(name)
                    {
                        Options = _options.CurrentValue, 
                        ScopeProvider = _scopeProvider
                    });
        }

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
        }

        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
            foreach (var logger in _loggers)
            {
                logger.Value.ScopeProvider = scopeProvider;
            }
        }

        private void ReloadLoggerOptions(ElasticsearchLoggerOptions options)
        {
            foreach (var logger in _loggers)
            {
                logger.Value.Options = options;
            }
        }
        
    }
}