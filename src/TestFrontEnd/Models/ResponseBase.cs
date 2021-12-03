namespace TestFrontEnd.Models;

public abstract class ResponseBase<TValue>
{
    public bool Success { get; set; }

    public TValue Value { get; set; }
}
