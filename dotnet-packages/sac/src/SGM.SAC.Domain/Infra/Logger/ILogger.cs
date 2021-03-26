using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.SAC.Domain.Infra.Logger
{
    public interface ILogger
    {
        Task DebugAsync(string message, CancellationToken cancellationToken);

        Task InfoAsync(string message, CancellationToken cancellationToken);

        Task WarnAsync(string message, CancellationToken cancellationToken);

        Task ErrorAsync(string message, Exception exception, CancellationToken cancellationToken);

        Task LogAsync(LogLevel level, string message, CancellationToken cancellationToken , Exception exception = null);
    }

    public enum LogLevel
    {
        Error = 0,
        Warning = 1,
        Info = 2,
        Debug = 3
    }
}
