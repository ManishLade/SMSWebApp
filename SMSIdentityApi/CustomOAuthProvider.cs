using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using InfraStructure.Repository;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace SMSIdentityApi
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        readonly IUserRepository userRepository;

        public CustomOAuthProvider(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            //string clientId = string.Empty;
            //string clientSecret = string.Empty;
            //string symmetricKeyAsBase64 = string.Empty;

            //if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            //{
            //    context.TryGetFormCredentials(out clientId, out clientSecret);
            //}

            //if (context.ClientId == null)
            //{
            //    context.SetError("invalid_clientId", "client_Id is not set");
            //    return Task.FromResult<object>(null);
            //}

            //var audience = AudiencesStore.FindAudience(context.ClientId);

            //if (audience == null)
            //{
            //    context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
            //    return Task.FromResult<object>(null);
            //}


            //return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            System.Diagnostics.Debugger.Break();

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var isAuthenticated = userRepository.VerifyUser(context.UserName, context.Password);

            if (isAuthenticated)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                return;
                //return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Manager"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Supervisor"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", (context.ClientId == null) ? "099153c2625149bc8ecb3e85e03f0022" : context.ClientId
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            //return Task.FromResult<object>(null);
        }



    }
}