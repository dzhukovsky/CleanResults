using System.Diagnostics.CodeAnalysis;

namespace CleanResults.Abstractions;

/// <summary>
/// Represents the result of an operation, indicating success or failure.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    bool IsFailure { get; }

    /// <summary>
    /// Gets the error associated with the operation, if any.
    /// </summary>
    IError? Error { get; }
}
