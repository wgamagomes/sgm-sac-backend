using SGM.GEP.Domain.Infra.Logger;
using SGM.GEP.Infra.Data.Mongo.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.GEP.Infra.Data.Mongo.Log
{
    public class Logger : ILogger
    {
        private readonly GEPContextMongo _context;

        public Logger(GEPContextMongo context)
        {
            _context = context;
        }

        public async Task DebugAsync(string message, CancellationToken cancellationToken)
        {
            await _context.GetCollection<Log>().InsertOneAsync(new Log
            {
                Level = "Debug",
                ErrorMessage = message
            }
          , cancellationToken: cancellationToken);
        }

        public async Task ErrorAsync(string message, Exception exception, CancellationToken cancellationToken)
        {
            await _context.GetCollection<Log>().InsertOneAsync(new Log
            {
                Level = "Error",
                ErrorMessage = exception.Message,
                Message = message,
                StackTrace = exception.StackTrace,
                InnerExceptionMessage = exception.InnerException?.Message
            }
            , cancellationToken: cancellationToken);
        }

        public async Task InfoAsync(string message, CancellationToken cancellationToken)
        {
            await _context.GetCollection<Log>().InsertOneAsync(new Log
            {
                Level = "Info",
                ErrorMessage = message                
            }
            , cancellationToken: cancellationToken); 
        }

        public async Task WarnAsync(string message, CancellationToken cancellationToken)
        {
            await _context.GetCollection<Log>().InsertOneAsync(new Log
            {
                Level = "Warn",
                ErrorMessage = message
            }
            , cancellationToken: cancellationToken);
        }
        public async Task LogAsync(LogLevel level, string message, CancellationToken cancellationToken, Exception exception = null)
        {
            switch (level)
            {
                case LogLevel.Error:
                    await ErrorAsync(message, exception, cancellationToken);
                    break;

                case LogLevel.Warning:
                    await WarnAsync(message, cancellationToken);
                    break;

                case LogLevel.Info:
                    await InfoAsync(message, cancellationToken);
                    break;

                case LogLevel.Debug:
                    await DebugAsync(message, cancellationToken);
                    break;

                default:
                    break;
            }
        }

    }
}
