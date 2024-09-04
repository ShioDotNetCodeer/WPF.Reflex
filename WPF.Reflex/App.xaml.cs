using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using WPF.Reflex.Service;
using WPF.Reflex.ViewModels;
using WPF.Reflex.Views;

namespace WPF.Reflex
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }
        public string DllPath { get; }
        public App()
        {
            Services = ConfigureServices();
            DllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DLL");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Ioc.Default.ConfigureServices(Services);
            base.OnStartup(e);
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<InstancesService>();
            services.AddSingleton<DialogService>();
            services.AddTransient<CheckDllService>();



            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<RegisterViewViewModel>();
            services.AddTransient(sp => new RegisterView { DataContext = sp.GetRequiredService<RegisterViewViewModel>() });

            return services.BuildServiceProvider();
        }
    }

}
