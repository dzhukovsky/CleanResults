using CleanResults.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace CleanResults;

/// <summary>
/// Represents a struct-based result that holds a value of type <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public readonly struct ValueResult<TValue> : IResult<TValue>
{
    private readonly TValue _value = default!;

    /// <summary>
    /// Gets the value of the result if the operation was successful.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the result is in a failed state.</exception>
    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Result is in failed state. Value is not set.");

    /// <summary>
    /// Gets the error associated with the result if the operation failed.
    /// </summary>
    public IError? Error { get; }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess => Error == default;

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => Error != default;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueResult{TValue}"/> struct with a value.
    /// </summary>
    /// <param name="value">The value of the result.</param>
    public ValueResult(TValue value)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueResult{TValue}"/> struct with an error.
    /// </summary>
    /// <param name="error">The error associated with the result.</param>
    public ValueResult(IError error)
    {
        ArgumentNullException.ThrowIfNull(error);
        Error = error;
    }

    /// <summary>
    /// Returns a failed result if the value is null.
    /// </summary>
    /// <param name="error">The error to associate with the result if the value is null.</param>
    /// <returns>A failed result if the value is null; otherwise, the current result.</returns>
    public ValueResult<TValue> FailIfNull(IError error)
        => IsSuccess && Value == null ? new(error) : this;

    /// <summary>
    /// Tries to get the value of the result.
    /// </summary>
    /// <param name="value">When this method returns, contains the value of the result if the operation was successful; otherwise, the default value for the type of the value parameter.</param>
    /// <returns><c>true</c> if the operation was successful; otherwise, <c>false</c>.</returns>
    public bool TryGetValue([NotNullWhen(true)] out TValue? value)
    {
        value = _value;
        return IsSuccess;
    }

    /// <summary>
    /// Determines whether the result has an error of the specified type.
    /// </summary>
    /// <typeparam name="TError">The type of the error.</typeparam>
    /// <returns><c>true</c> if the result has an error of the specified type; otherwise, <c>false</c>.</returns>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool HasError<TError>() where TError : IError
        => IsFailure && Error is TError;

    /// <summary>
    /// Tries to get the error associated with the result.
    /// </summary>
    /// <param name="error">When this method returns, contains the error associated with the result if the operation failed; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the operation failed; otherwise, <c>false</c>.</returns>
    [MemberNotNullWhen(true, nameof(Error))]
    public bool TryGetError([NotNullWhen(true)] out IError? error)
        => (error = Error) != default;

    /// <summary>
    /// Tries to get the error of the specified type associated with the result.
    /// </summary>
    /// <typeparam name="TError">The type of the error.</typeparam>
    /// <param name="error">When this method returns, contains the error of the specified type associated with the result if the operation failed; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the operation failed and the error is of the specified type; otherwise, <c>false</c>.</returns>
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
    /// Implicitly converts a value of type <typeparamref name="TValue"/> to a <see cref="ValueResult{TValue}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="ValueResult{TValue}"/> containing the value.</returns>
    public static implicit operator ValueResult<TValue>(TValue value) => new(value);

    /// <summary>
    /// Implicitly converts an error of type <see cref="Error"/> to a <see cref="ValueResult{TValue}"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    /// <returns>A <see cref="ValueResult{TValue}"/> containing the error.</returns>
    public static implicit operator ValueResult<TValue>(Error error) => new(error);

    /// <summary>
    /// Implicitly converts a <see cref="ValueResult{TValue}"/> to a <see cref="ValueResult"/>.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    /// <returns>A <see cref="ValueResult"/> containing the error if the result is in a failed state; otherwise, a default <see cref="ValueResult"/>.</returns>
    public static implicit operator ValueResult(ValueResult<TValue> result)
        => result.IsSuccess ? default : new(result.Error);
}
