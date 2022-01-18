namespace Features;

public abstract record ResponseBase<TValue>(TValue Value, bool Success);
