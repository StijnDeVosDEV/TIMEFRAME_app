using Auth0.OidcClient;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TIMEFRAME_windows.SERVICES
{
    public class AuthenticationService : Interfaces.IAuthenticationService
    {
        // FIELDS
        private Auth0Client client;
        private LoginResult _loginResult;

        // CONSTRUCTOR
        public AuthenticationService()
        {

        }

        // PROPERTIES
        public LoginResult loginResult {
            get { return _loginResult; }
            set { if (_loginResult != value) { _loginResult = value; } }
        }

        // METHODS
        public async Task Login()
        {
            string domain = ConfigurationManager.AppSettings["Auth0:Domain"];
            string clientId = ConfigurationManager.AppSettings["Auth0:ClientId"];

            client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = domain,
                ClientId = clientId
            });

            loginResult = await client.LoginAsync();

            foreach (Claim claim in loginResult.User.Claims)
            {
                Logger.Write(claim.Subject.ToString() + " = " + claim.Value);
            }

            //return !loginResult.IsError;
        }
    }
}
