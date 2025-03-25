using System.ComponentModel;
using System.Runtime.CompilerServices;
using BioStat.MVVM.Model;

namespace BioStat.MVVM.ViewModel;

public class BmiViewModel : INotifyPropertyChanged
{
    private readonly BmiModel bmiM;
    private string heightValue;

    public BmiViewModel()
    {
        bmiM = new BmiModel();
    }

    public string HeightValue
    {
        get => heightValue;
        set
        {
            if (heightValue != value)
            {
                heightValue = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}