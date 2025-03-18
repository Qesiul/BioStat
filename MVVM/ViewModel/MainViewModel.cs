using BioStat.Core;

namespace BioStat.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand BmiViewCommand { get; set; }
    
    public HomeViewModel HomeVM { get; set; }
    
    public BmiViewModel BmiVM { get; set; }

    private object currentView;

    public object currentViewGS
    {
        get {return currentView;}
        set {currentView = value; OnPropertyChanged();}
    }
    
    public MainViewModel()
    {
        HomeVM = new HomeViewModel();
        BmiVM = new BmiViewModel();
        currentViewGS = HomeVM;

       HomeViewCommand = new RelayCommand(o =>
       {
           currentViewGS = HomeVM;
        });

        BmiViewCommand = new RelayCommand(o =>
       {
           currentViewGS = BmiVM;
        });
    }
    
}