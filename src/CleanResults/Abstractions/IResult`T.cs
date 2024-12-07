namespace CleanResults.Abstractions;
public interface IResult<out TValue> : IResult
{
    TValue Value { get; }
}