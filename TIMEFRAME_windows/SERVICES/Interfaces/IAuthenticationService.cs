using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TIMEFRAME_windows.SERVICES.Interfaces
{
    interface IAuthenticationService
    {
        public LoginResult loginResult { get; set; }
        public MODELS.User User { get; set; }

        public Task Login();
    }
}
