using ConfigurationSwitcherGUI.Presenter;
using ConfigurationSwitcherGUI.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OVAConfigSwitcher.Business.Contracts.Models;
using System;
using System.IO;
using System.Windows.Forms;
using Unity;

namespace ConfigurationSwitcherGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfiguerServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var configurationSwitcherPresenter = serviceProvider.GetRequiredService<ConfigurationSwitcherPresenter>();
                Application.Run(configurationSwitcherPresenter.ShowView() as Form);
            }
        }

        private static void ConfiguerServices(ServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            services.Configure<AppSettings>(configuration.GetSection("App"));

            services.AddTransient<IConfigurationSwitcherView, ConfigurationSwitcherView>();
            services.AddTransient<ConfigurationSwitcherPresenter>();
        }
    }
}
