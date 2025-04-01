using BioStat.Core;
using BioStat.MVVM.Model;
using Microsoft.Extensions.Configuration;

namespace BioStat.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    private readonly IConfiguration _config;
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand BmiViewCommand { get; set; }
    
    public HomeViewModel HomeVM { get; set; }
    
    public BmiViewModel BmiVM { get; set; }
    
    private object currentViewModel;

    public object CurrentViewModel
    {
        get {return currentViewModel;}
        set {currentViewModel = value; OnPropertyChanged();}
    }
    
    public MainViewModel(IConfiguration config)
    {
        _config = config;
        HomeVM = new HomeViewModel();
        BmiVM = new BmiViewModel(config);
        CurrentViewModel = HomeVM;

       HomeViewCommand = new RelayCommand(o =>
       {
           CurrentViewModel = HomeVM;
        });

        BmiViewCommand = new RelayCommand(o =>
       {
           CurrentViewModel = BmiVM;
        });
    }
    
}