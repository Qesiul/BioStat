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
    private string heightValueFeet;
    private string weightValue;
    private string bmiValue;
    private string bmiRange;
    private string bmiRangeColour;

    public string HeightValue
    {
        get => heightValue;
        set
        {
                heightValue = value;
                OnPropertyChanged();
        }
    }

    public string HeightValueFeet
    {
        get => heightValueFeet;
        set
        {
            heightValueFeet = value;
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

    public string BmiRange
    {
        get => bmiRange;
        set
        {
            bmiRange = value;
            OnPropertyChanged();
        }
    }

    public string BmiRangeColour
    {
        get => bmiRangeColour;
        set
        {
            bmiRangeColour = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand BmiCommand { get; private set; }
    
    public ICommand ResetCommand { get; private set; }

    private bool CanExecuteCalculate(object parameter)
    {
        return (double.TryParse(HeightValue, out _) && double.TryParse(WeightValue, out _) && double.TryParse(HeightValueFeet, out _));
    }

    private void ExecuteCalculate(object parameter)
    {
        if (double.TryParse(HeightValue, out double heightBmi) && double.TryParse(WeightValue, out double weightBmi) && double.TryParse(HeightValueFeet, out double heightFeetBmi))
        {
            heightBmi = heightBmi + (heightFeetBmi * 12);
            double resultBmi = bmiM.CalculateBmi(heightBmi, weightBmi);
            string bmiRange = bmiM.SetBmiRange(resultBmi);
            string bmiRangeColour = bmiM.SetBmiRangeColour(bmiRange);
            resultBmi = Math.Round(resultBmi, 2);
            BmiValue = resultBmi.ToString();
            BmiRange = bmiRange;
            BmiRangeColour = bmiRangeColour;
        }
    }

    private void ExecuteReset(object parameter)
    {
        HeightValue = string.Empty;
        HeightValueFeet = string.Empty;
        WeightValue = string.Empty;
        BmiValue = string.Empty;
        BmiRange = string.Empty;
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