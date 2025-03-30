using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BioStat.Core;
using BioStat.MVVM.Model;

namespace BioStat.MVVM.ViewModel;

public class BmiViewModel : INotifyPropertyChanged
{
    private readonly BmiModel bmiM;
    private string heightValue;
    private string weightValue;
    private string bmiValue;

    public string HeightValue
    {
        get => heightValue;
        set
        {
                heightValue = value;
                OnPropertyChanged();
        }
    }

    public string WeightValue
    {
        get => weightValue;
        set
        {
            weightValue = value;
            OnPropertyChanged();
        }
    }

    public string BmiValue
    {
        get => bmiValue;
        set
        {
            bmiValue = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand BmiCommand { get; private set; }
    
    public ICommand ResetCommand { get; private set; }

    private bool CanExecuteCalculate(object parameter)
    {
        return double.TryParse(HeightValue, out _) && double.TryParse(WeightValue, out _);
    }

    private void ExecuteCalculate(object parameter)
    {
        if (double.TryParse(HeightValue, out double heightBmi) && double.TryParse(WeightValue, out double weightBmi))
        {
            double resultBmi = bmiM.CalculateBmi(heightBmi, weightBmi);
            resultBmi = Math.Round(resultBmi, 2);
            BmiValue = resultBmi.ToString();
        }
    }

    private void ExecuteReset(object parameter)
    {
        HeightValue = string.Empty;
        WeightValue = string.Empty;
        BmiValue = string.Empty;
    }
    
    public BmiViewModel()
    {
        bmiM = new BmiModel();
        BmiCommand = new RelayCommand(ExecuteCalculate, CanExecuteCalculate);
        ResetCommand = new RelayCommand(ExecuteReset);
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