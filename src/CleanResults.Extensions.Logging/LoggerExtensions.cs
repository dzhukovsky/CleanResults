using CleanResults.Abstractions;
using Microsoft.Extensions.Logging;

namespace CleanResults.Extensions.Logging;

/// <summary>
/// Provides extension methods for logging errors with metadata.
/// </summary>
public static class LoggerExtensions
{
    #region Log

    /// <summary>
    /// Logs an error with the specified log level, event ID, exception, and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="logLevel">The log level.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, Exception? exception, IError? error)
    {
        // TODO: Use spread operator when issue is fixed; tracking link: https://github.com/dotnet/roslyn/issues/71788
        var metadata = error?.Metadata != null ? error.Metadata as object?[] ?? error.Metadata.ToArray() : [];
        logger.Log(logLevel, eventId, exception, error?.Message, metadata);
    }

    /// <summary>
    /// Logs an error with the specified log level, exception, and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="logLevel">The log level.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void Log(this ILogger logger, LogLevel logLevel, Exception? exception, IError? error)
        => logger.Log(logLevel, eventId: default, exception, error);

    /// <summary>
    /// Logs an error with the specified log level, event ID, and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="logLevel">The log level.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="error">The error details.</param>
    public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, IError? error)
        => logger.Log(logLevel, eventId, exception: null, error);

    /// <summary>
    /// Logs an error with the specified log level and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="logLevel">The log level.</param>
    /// <param name="error">The error details.</param>
    public static void Log(this ILogger logger, LogLevel logLevel, IError? error)
        => logger.Log(logLevel, exception: null, error);

    #endregion

    #region LogTrace

    /// <summary>
    /// Logs a trace error with the specified error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="error">The error details.</param>
    public static void LogTrace(this ILogger logger, IError? error)
        => logger.Log(LogLevel.Trace, error);

    /// <summary>
    /// Logs a trace error with the specified exception and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogTrace(this ILogger logger, Exception? exception, IError? error)
        => logger.Log(LogLevel.Trace, exception, error);

    /// <summary>
    /// Logs a trace error with the specified event ID and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="error">The error details.</param>
    public static void LogTrace(this ILogger logger, EventId eventId, IError? error)
        => logger.Log(LogLevel.Trace, eventId, error);

    /// <summary>
    /// Logs a trace error with the specified event ID, exception, and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogTrace(this ILogger logger, EventId eventId, Exception? exception, IError? error)
        => logger.Log(LogLevel.Trace, eventId, exception, error);

    #endregion

    #region LogDebug

    /// <summary>
    /// Logs a debug error with the specified error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="error">The error details.</param>
    public static void LogDebug(this ILogger logger, IError? error)
        => logger.Log(LogLevel.Debug, error);

    /// <summary>
    /// Logs a debug error with the specified exception and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogDebug(this ILogger logger, Exception? exception, IError? error)
        => logger.Log(LogLevel.Debug, exception, error);

    /// <summary>
    /// Logs a debug error with the specified event ID and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="error">The error details.</param>
    public static void LogDebug(this ILogger logger, EventId eventId, IError? error)
        => logger.Log(LogLevel.Debug, eventId, error);

    /// <summary>
    /// Logs a debug error with the specified event ID, exception, and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogDebug(this ILogger logger, EventId eventId, Exception? exception, IError? error)
        => logger.Log(LogLevel.Debug, eventId, exception, error);

    #endregion

    #region LogInformation

    /// <summary>
    /// Logs an informational error with the specified error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="error">The error details.</param>
    public static void LogInformation(this ILogger logger, IError? error)
        => logger.Log(LogLevel.Information, error);

    /// <summary>
    /// Logs an informational error with the specified exception and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogInformation(this ILogger logger, Exception? exception, IError? error)
        => logger.Log(LogLevel.Information, exception, error);

    /// <summary>
    /// Logs an informational error with the specified event ID and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="error">The error details.</param>
    public static void LogInformation(this ILogger logger, EventId eventId, IError? error)
        => logger.Log(LogLevel.Information, eventId, error);

    /// <summary>
    /// Logs an informational error with the specified event ID, exception, and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogInformation(this ILogger logger, EventId eventId, Exception? exception, IError? error)
        => logger.Log(LogLevel.Information, eventId, exception, error);

    #endregion

    #region LogWarning

    /// <summary>
    /// Logs a warning error with the specified error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="error">The error details.</param>
    public static void LogWarning(this ILogger logger, IError? error)
        => logger.Log(LogLevel.Warning, error);

    /// <summary>
    /// Logs a warning error with the specified exception and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogWarning(this ILogger logger, Exception? exception, IError? error)
        => logger.Log(LogLevel.Warning, exception, error);

    /// <summary>
    /// Logs a warning error with the specified event ID and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="error">The error details.</param>
    public static void LogWarning(this ILogger logger, EventId eventId, IError? error)
        => logger.Log(LogLevel.Warning, eventId, error);

    /// <summary>
    /// Logs a warning error with the specified event ID, exception, and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogWarning(this ILogger logger, EventId eventId, Exception? exception, IError? error)
        => logger.Log(LogLevel.Warning, eventId, exception, error);

    #endregion

    #region LogError

    /// <summary>
    /// Logs an error with the specified error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="error">The error details.</param>
    public static void LogError(this ILogger logger, IError? error)
       => logger.Log(LogLevel.Error, error);

    /// <summary>
    /// Logs an error with the specified exception and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogError(this ILogger logger, Exception? exception, IError? error)
        => logger.Log(LogLevel.Error, exception, error);

    /// <summary>
    /// Logs an error with the specified event ID and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="error">The error details.</param>
    public static void LogError(this ILogger logger, EventId eventId, IError? error)
        => logger.Log(LogLevel.Error, eventId, error);

    /// <summary>
    /// Logs an error with the specified event ID, exception, and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogError(this ILogger logger, EventId eventId, Exception? exception, IError? error)
        => logger.Log(LogLevel.Error, eventId, exception, error);

    #endregion

    #region LogCritical

    /// <summary>
    /// Logs a critical error with the specified error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="error">The error details.</param>
    public static void LogCritical(this ILogger logger, IError? error)
       => logger.Log(LogLevel.Critical, error);

    /// <summary>
    /// Logs a critical error with the specified exception and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogCritical(this ILogger logger, Exception? exception, IError? error)
        => logger.Log(LogLevel.Critical, exception, error);

    /// <summary>
    /// Logs a critical error with the specified event ID and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="error">The error details.</param>
    public static void LogCritical(this ILogger logger, EventId eventId, IError? error)
        => logger.Log(LogLevel.Critical, eventId, error);

    /// <summary>
    /// Logs a critical error with the specified event ID, exception, and error details.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="eventId">The event ID.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="error">The error details.</param>
    public static void LogCritical(this ILogger logger, EventId eventId, Exception? exception, IError? error)
        => logger.Log(LogLevel.Critical, eventId, exception, error);

    #endregion
}
