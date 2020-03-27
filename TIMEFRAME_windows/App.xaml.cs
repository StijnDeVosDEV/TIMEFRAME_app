using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TIMEFRAME_windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
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
