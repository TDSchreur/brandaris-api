using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;

namespace TestFrontEnd
{
    internal class RejectSessionCookieWhenAccountNotInCacheEvents : CookieAuthenticationEvents
    {
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            try
            {
                ITokenAcquisition tokenAcquisition = context.HttpContext.RequestServices.GetRequiredService<ITokenAcquisition>();
                _ = await tokenAcquisition.GetAccessTokenForUserAsync(new[] { "profile" }, user: context.Principal);
            }
            catch (MicrosoftIdentityWebChallengeUserException ex) when (AccountDoesNotExitInTokenCache(ex))
            {
                context.RejectPrincipal();
            }
        }

        /// <summary>
        ///     Is the exception thrown because there is no account in the token cache?
        /// </summary>
        /// <param name="ex">Exception thrown by <see cref="ITokenAcquisition" />.GetTokenForXX methods.</param>
        /// <returns>A boolean telling if the exception was about not having an account in the cache</returns>
        private static bool AccountDoesNotExitInTokenCache(Exception ex) => ex.InnerException is MsalUiRequiredException { ErrorCode: "user_null" };
    }
}
