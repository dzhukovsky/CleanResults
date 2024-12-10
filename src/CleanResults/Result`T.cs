using CleanResults.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace CleanResults;

/// <summary>
/// Represents the result of an operation that can either be successful or fail, with a value of type <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public class Result<TValue> : Result, IResult<TValue>
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
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class with a successful result and the specified value.
    /// </summary>
    /// <param name="value">The value of the result.</param>
    public Result(TValue value)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class with a failed result and the specified error.
    /// </summary>
    /// <param name="error">The error that caused the result to fail.</param>
    public Result(IError error) : base(error)
    {
    }

    /// <summary>
    /// Returns a failed result if the value is null; otherwise, returns the current result.
    /// </summary>
    /// <param name="error">The error to use if the value is null.</param>
    /// <returns>A failed result if the value is null; otherwise, the current result.</returns>
    public Result<TValue> FailIfNull(IError error)
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
    /// Implicitly converts a value of type <typeparamref name="TValue"/> to a <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="Result{TValue}"/> containing the specified value.</returns>
    public static implicit operator Result<TValue>(TValue value) => new(value);

    /// <summary>
    /// Implicitly converts an error of type <see cref="Error"/> to a <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    /// <returns>A <see cref="Result{TValue}"/> containing the specified error.</returns>
    public static implicit operator Result<TValue>(Error error) => new(error);
}
