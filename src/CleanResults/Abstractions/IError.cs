namespace CleanResults.Abstractions;

/// <summary>
/// Represents an error with a message and optional metadata.
/// </summary>
public interface IError
{
    /// <summary>
    /// Gets the error message.
    /// </summary>
    string Message { get; }

    /// <summary>
    /// Gets the metadata associated with the error.
    /// </summary>
    IReadOnlyList<object?> Metadata { get; }
}
