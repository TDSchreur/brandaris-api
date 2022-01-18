namespace Features.GetConfig;

public record GetConfigResponse(IEnumerable<KeyValuePair<string, string>> Value, bool Success = true) : ResponseBase<IEnumerable<KeyValuePair<string, string>>>(Value, Success);
