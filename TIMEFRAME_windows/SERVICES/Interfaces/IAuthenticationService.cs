using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TIMEFRAME_windows.SERVICES.Interfaces
{
    interface IAuthenticationService
    {
        // PROPERTIES
        public LoginResult loginResult { get; set; }
        public MODELS.User User { get; set; }

        // METHODS
        public Task<bool> Login();
        public Task<bool> Logout();
    }
}
