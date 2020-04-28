using Auth0.OidcClient;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TIMEFRAME_windows.SERVICES
{
    public class AuthenticationService : Interfaces.IAuthenticationService
    {
        // FIELDS
        private Auth0Client client;
        private LoginResult _loginResult;
        private MODELS.User _User;

        // CONSTRUCTOR
        public AuthenticationService()
        {

        }

        // PROPERTIES
        public LoginResult loginResult {
            get { return _loginResult; }
            set { if (_loginResult != value) { _loginResult = value; } }
        }

        public MODELS.User User
        {
            get { return _User; }
            set { if (value != _User) { _User = value; } }
        }

        // METHODS
        public async Task<bool> Login()
        {
            try
            {
                string domain = ConfigurationManager.AppSettings["Auth0:Domain"];
                string clientId = ConfigurationManager.AppSettings["Auth0:ClientId"];

                client = new Auth0Client(new Auth0ClientOptions
                {
                    Domain = domain,
                    ClientId = clientId
                });

                loginResult = await client.LoginAsync();

                if (!loginResult.IsError)
                {
                    User = new MODELS.User();

                    foreach (Claim claim in loginResult.User.Claims)
                    {
                        switch (claim.Type)
                        {
                            case "name":
                                User.Name = claim.Value;
                                break;

                            case "email":
                                User.Email = claim.Value;
                                break;

                            case "email_verified":
                                if (claim.Value == "true") { User.EmailVerified = true; } else { User.EmailVerified = false; }
                                break;

                            case "picture":
                                User.PicturePath = claim.Value;
                                break;

                            default:
                                break;
                        }
                    }

                    Logger.Write("LOGIN |  User successfully logged in (" + User.Name + ")");
                    return true;
                }
                else
                {
                    this.User = null;
                    Logger.Write("LOGIN |  Error occurred while trying to login in : login service returned empty user");
                    return false;
                }
            }
            catch (Exception e)
            {
                Logger.Write("LOGIN |  Error occurred while trying to login in : " + Environment.NewLine +
                    e.ToString());
                return false;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                BrowserResultType browserResult = await client.LogoutAsync();

                if (browserResult != BrowserResultType.Success)
                {
                    MessageBox.Show(browserResult.ToString(), "Logout error:");
                    return false;
                }

                this.User = new MODELS.User();

                return true;
            }
            catch (Exception e)
            {
                Logger.Write("LOGIN |  Error occurred while trying to logout : " + Environment.NewLine +
                    e.ToString());

                return false;
            }
        }
    }
}
