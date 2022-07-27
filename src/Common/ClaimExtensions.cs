using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Brandaris.Common;

public static class ClaimExtensions
{
    public static string GetDisplayName(this IEnumerable<Claim> claims)
    {
        return claims.GetClaimValue("app_displayname");
    }

    public static string GetName(this IEnumerable<Claim> claims)
    {
        return claims.GetClaimValue("name");
    }

    public static Guid GetOid(this IEnumerable<Claim> claims)
    {
        // The immutable identifier for an object in the Microsoft identity system, in this case, a user account.
        // This ID uniquely identifies the user across applications - two different applications signing in the same user will receive the same value in the oid claim.
        // The Microsoft Graph will return this ID as the id property for a given user account.
        // Because the oid allows multiple apps to correlate users, the profile scope is required to receive this claim. Note that if a single user exists in multiple tenants,
        // the user will contain a different object ID in each tenant - they're considered different accounts, even though the user logs into each account with the same credentials.
        // The oid claim is a GUID and cannot be reused.
        string applicationIdClaim = claims.GetClaimValue("oid");

        return Guid.TryParse(applicationIdClaim, out Guid applicationId)
            ? applicationId
            : Guid.Empty;
    }

    private static string GetClaimValue(this IEnumerable<Claim> claims, string claimType)
    {
        return claims.FirstOrDefault(c => c.Type == claimType)
                    ?.Value ??
               string.Empty;
    }
}