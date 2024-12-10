namespace CleanResults.Abstractions;

/// <summary>
/// Represents the result of an operation that can either be successful or fail, with a value of type <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">The type of the value returned by the operation.</typeparam>
public interface IResult<out TValue> : IResult
{
    /// <summary>
    /// Gets the value returned by the operation.
    /// </summary>
    TValue Value { get; }
}
