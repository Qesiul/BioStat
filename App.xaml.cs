using System.Collections.Immutable;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using BioStat.MVVM.Services;
using BioStat.MVVM.View;
using BioStat.MVVM.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace BioStat;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static event EventHandler MeasurementAdded;
    
    public static void OnMeasurementAdded()
    {
        MeasurementAdded?.Invoke(null, EventArgs.Empty);
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "BioStat"
        );

        if (!Directory.Exists(appDataPath))
            Directory.CreateDirectory(appDataPath);
        
        string dbPath = Path.Combine(appDataPath, "MeasurmentsTracker.db");
        
        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>("ConnectionStrings:DefaultConnection", $"Data Source={dbPath}")
            })

            .Build();

        var context = new MeasurmentsTrackerDataAccess(config);
        context.Database.EnsureCreated();

        var mainVM = new MainViewModel(config);
        var mainWindow = new MainWindow
        {
            DataContext = mainVM
        };
        mainWindow.Show();
    }
}