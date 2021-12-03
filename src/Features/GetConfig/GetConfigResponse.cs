namespace Features.GetConfig;

public class GetConfigResponse : ResponseBase<IEnumerable<KeyValuePair<string, string>>>
{
    public GetConfigResponse(IEnumerable<KeyValuePair<string, string>> value, bool success = true) : base(value, success) { }
}
