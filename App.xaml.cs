using System.Collections.Immutable;
using System.Configuration;
using System.Data;
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
    protected override void OnStartup(StartupEventArgs e)
    {
        AppDomain.CurrentDomain.SetData("DataDirectory", AppContext.BaseDirectory);
        base.OnStartup(e);
        
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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