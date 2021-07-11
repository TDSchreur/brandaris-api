namespace Features
{
    public abstract class ResponseBase<TValue>
    {
        protected ResponseBase(TValue value, bool success = true)
        {
            Success = success;
            Value = value;
        }

        public bool Success { get; }

        public TValue Value { get; }
    }
}
