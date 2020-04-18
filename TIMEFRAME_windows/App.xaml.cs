using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Identity.Client;
using TIMEFRAME_windows.AUTHENTICATION;
using System.Text;
using System.IO;

namespace TIMEFRAME_windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // CONFIGURATION AUTHENTICATION BASED ON AZURE AD
        private static readonly string Tenant = "https://timeframeb2c.onmicrosoft.com";
        private static readonly string AzureAdB2CHostname = "timeframeb2c.onmicrosoft.com";
        private static readonly string ClientId = "c456c692-848a-4027-a3ee-52d7b936c482";
        public static string PolicySignUpSignIn = "B2C_1_SignUp_SignIn_TimeFrame";
        public static string PolicyEditProfile = "B2C_1_EditProfile_TimeFrame";
        public static string PolicyResetPassword = "B2C_1_ResetPassword_TimeFrame";

        public static string[] ApiScopes = { "https://timeframeb2c.onmicrosoft.com/afc94e3e-5820-44d6-875d-9af168293529/user_impersonation" };
        public static string ApiEndpoint = "https://timeframeb2c.b2clogin.com/oauth2/nativeclient";
        private static string AuthorityBase = $"https://{AzureAdB2CHostname}/tfp/{Tenant}/";
        public static string AuthoritySignUpSignIn = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
        public static string AuthorityResetPassword = $"{AuthorityBase}{PolicyResetPassword}";

        public static IPublicClientApplication PublicClientApp { get; private set; }

        static App()
        {
            PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithB2CAuthority(AuthoritySignUpSignIn)
                .WithLogging(Log, LogLevel.Verbose, false) //PiiEnabled set to false
                .Build();

            TokenCacheHelper.Bind(PublicClientApp.UserTokenCache);
        }

        private static void Log(LogLevel level, string message, bool containsPii)
        {
            string logs = ($"{level} {message}");
            StringBuilder sb = new StringBuilder();
            sb.Append(logs);
            File.AppendAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + ".msalLogs.txt", sb.ToString());
            sb.Clear();
        }



        // DEPENDENCY INJECTION CONFIGURATION
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure SERVICES
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<SERVICES.Interfaces.IBackendService, SERVICES.BackendService>();
            services.AddSingleton<VIEWMODELS.VM_Main, VIEWMODELS.VM_Main>();
            services.AddSingleton<VIEWS.V_Main, VIEWS.V_Main>();

            // Build Service Provider
            ServiceProvider serviceProvider = services.BuildServiceProvider();


            //// Get V_Main service and open
            //VIEWS.V_Main v_Main = serviceProvider.GetService<VIEWS.V_Main>();
            //v_Main.Show();

            //System.Windows.MessageBox.Show("SERVICES CONFIGURED");
        }
    }
}
