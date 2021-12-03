﻿using TestFrontEnd.Models;

namespace TestFrontEnd.ServiceAgents;

public interface IBrandarisApiServiceAgent
{
    Task<GetPersonResponse> GetPersonAsync(int id);

    Task<IEnumerable<KeyValuePair<string, string>>> GetRemoteClaimsAsync();
}
