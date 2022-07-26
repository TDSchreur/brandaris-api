using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Brandaris.Common;

public class IdentityHelper : IIdentityHelper
{
    private readonly IEnumerable<Claim> _claims;

    public IdentityHelper(IHttpContextAccessor httpContextAccessor)
    {
        _claims = httpContextAccessor.HttpContext?.User.Claims ?? Array.Empty<Claim>();
    }

    public string GetName()
    {
        string name = _claims.GetName();

        return string.IsNullOrWhiteSpace(name)
            ? _claims.GetDisplayName()
            : name;
    }

    public Guid GetOid()
    {
        return _claims.GetOid();
    }
}