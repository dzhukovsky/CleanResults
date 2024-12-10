using CleanResults.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace CleanResults;

/// <summary>
/// Represents a struct-based result of an operation that can either be successful or contain an error.
/// </summary>
public readonly struct ValueResult : IResult
{
    /// <summary>
    /// Gets the error associated with the result, if any.
    /// </summary>
    public IError? Error { get; }

    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => Error == default;

    /// <summary>
    /// Gets a value indicating whether the result is a failure.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => Error != default;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueResult"/> struct with the specified error.
    /// </summary>
    /// <param name="error">The error associated with the result.</param>
    public ValueResult(IError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        Error = error;
    }

    /// <summary>
    /// Determines whether the result contains an error of the specified type.
    /// </summary>
    /// <typeparam name="TError">The type of error to check for.</typeparam>
    /// <returns><c>true</c> if the result contains an error of the specified type; otherwise, <c>false</c>.</returns>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool HasError<TError>() where TError : IError
        => IsFailure && Error is TError;

    /// <summary>
    /// Tries to get the error associated with the result.
    /// </summary>
    /// <param name="error">When this method returns, contains the error associated with the result, if any.</param>
    /// <returns><c>true</c> if the result contains an error; otherwise, <c>false</c>.</returns>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool TryGetError([NotNullWhen(true)] out IError? error)
        => (error = Error) != default;

    /// <summary>
    /// Tries to get the error of the specified type associated with the result.
    /// </summary>
    /// <typeparam name="TError">The type of error to get.</typeparam>
    /// <param name="error">When this method returns, contains the error of the specified type associated with the result, if any.</param>
    /// <returns><c>true</c> if the result contains an error of the specified type; otherwise, <c>false</c>.</returns>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool TryGetError<TError>([NotNullWhen(true)] out TError? error)
        where TError : IError
    {
        if (Error is TError err)
        {
            error = err;
            return true;
        }

        error = default;
        return false;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful <see cref="ValueResult"/>.</returns>
    public static ValueResult Ok() => default;

    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error associated with the result.</param>
    /// <returns>A failed <see cref="ValueResult"/>.</returns>
    public static ValueResult Fail(IError error) => new(error);

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value associated with the result.</param>
    /// <returns>A successful <see cref="ValueResult{TValue}"/>.</returns>
    public static ValueResult<TValue> Ok<TValue>(TValue value) => new(value);

    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="error">The error associated with the result.</param>
    /// <returns>A failed <see cref="ValueResult{TValue}"/>.</returns>
    public static ValueResult<TValue> Fail<TValue>(IError error) => new(error);

    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a <see cref="ValueResult"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator ValueResult(Error error) => new(error);
}
