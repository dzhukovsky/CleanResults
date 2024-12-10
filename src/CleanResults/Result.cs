using CleanResults.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace CleanResults;

/// <summary>
/// Represents the result of an operation, indicating success or failure.
/// </summary>
public class Result : IResult
{
    private static readonly Result ResultOk = new();

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
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    public Result() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with the specified error.
    /// </summary>
    /// <param name="error">The error associated with the result.</param>
    public Result(IError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        Error = error;
    }

    /// <summary>
    /// Determines whether the result has a specific type of error.
    /// </summary>
    /// <typeparam name="TError">The type of error to check for.</typeparam>
    /// <returns>True if the result has the specified type of error; otherwise, false.</returns>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool HasError<TError>() where TError : IError
        => IsFailure && Error is TError;

    /// <summary>
    /// Tries to get the error associated with the result.
    /// </summary>
    /// <param name="error">When this method returns, contains the error associated with the result, if any.</param>
    /// <returns>True if the result has an error; otherwise, false.</returns>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool TryGetError([NotNullWhen(true)] out IError? error)
        => (error = Error) != default;

    /// <summary>
    /// Tries to get the error of a specific type associated with the result.
    /// </summary>
    /// <typeparam name="TError">The type of error to get.</typeparam>
    /// <param name="error">When this method returns, contains the error of the specified type, if any.</param>
    /// <returns>True if the result has an error of the specified type; otherwise, false.</returns>
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
    /// <returns>A successful result.</returns>
    public static Result Ok() => ResultOk;

    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error associated with the result.</param>
    /// <returns>A failed result.</returns>
    public static Result Fail(IError error) => new(error);

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value associated with the result.</param>
    /// <returns>A successful result with the specified value.</returns>
    public static Result<TValue> Ok<TValue>(TValue value) => new(value);

    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="error">The error associated with the result.</param>
    /// <returns>A failed result with the specified error.</returns>
    public static Result<TValue> Fail<TValue>(IError error) => new(error);

    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result(Error error) => new(error);
}
