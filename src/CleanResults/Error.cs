using CleanResults.Abstractions;

namespace CleanResults;

/// <summary>
/// Represents an error with a message and optional metadata.
/// </summary>
public class Error : IError
{
    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets the metadata associated with the error.
    /// </summary>
    public IReadOnlyList<object?> Metadata { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class with the specified message and metadata.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="metadata">The metadata associated with the error.</param>
    /// <exception cref="ArgumentException">Thrown when the message is null or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the metadata is null.</exception>
    public Error(string message, params object?[] metadata)
    {
        ArgumentException.ThrowIfNullOrEmpty(message);
        ArgumentNullException.ThrowIfNull(metadata);

        Message = message;
        // Directly assigns object?[] to IReadOnlyList<object?> to avoid unnecessary .ToArray() call 
        // when passing params to ILogger.Log(..., params object?[] args).
        Metadata = metadata;
    }
}
