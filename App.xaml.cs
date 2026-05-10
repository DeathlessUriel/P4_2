using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TransportApp.Models;
using TransportApp.Services;
using TransportApp.ViewModels;


namespace TransportApp;

public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddSingleton(configuration);

        services.AddDbContextFactory<AppDbContext>(options =>
        { 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<PojazdService>();
        services.AddScoped<KierowcaService>();
        services.AddScoped<ZlecenieService>();
        services.AddScoped<PrzejazdService>();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();

        ServiceProvider = services.BuildServiceProvider();

        var window = ServiceProvider.GetRequiredService<MainWindow>();
        window.Show();
        DispatcherUnhandledException += App_DispatcherUnhandledException;
        base.OnStartup(e);
    }
    private void App_DispatcherUnhandledException(
       object sender,
       System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(
            e.Exception.Message,
            "Błąd",
            MessageBoxButton.OK,
            MessageBoxImage.Error);

        e.Handled = true;
    }
}