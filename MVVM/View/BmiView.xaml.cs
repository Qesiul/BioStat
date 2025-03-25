using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BioStat.Core;

namespace BioStat.MVVM.View;

public partial class BmiView : UserControl
{
    private readonly ErrorHandler errorHandler;
    public BmiView()
    {
        InitializeComponent();
        errorHandler = new ErrorHandler();
    }

    private void NumberOnlyTextBox(object sender, TextCompositionEventArgs e)
    {
        var bmiM = new BioStat.MVVM.Model.BmiModel();
        bmiM.NumberOnlyTextBox(sender, e);
    }
    
    public void UnitTypeChanged(object sender, RoutedEventArgs e)
    {
        try
        {
            if (MetricUnitsRadioButton.IsChecked == true)
            {
                MetricTextBox.Visibility = Visibility.Visible;
                MetricTextBlock.Visibility = Visibility.Visible;
                ImperialTextBox1.Visibility = Visibility.Collapsed;
                ImperialTextBox2.Visibility = Visibility.Collapsed;
                ImperialTextBlock1.Visibility = Visibility.Collapsed;
                ImperialTextBlock2.Visibility = Visibility.Collapsed;
            }
            else if (ImperialUnitsRadioButton.IsChecked == true)
            {
                MetricTextBox.Visibility = Visibility.Collapsed;
                MetricTextBlock.Visibility = Visibility.Collapsed;
                ImperialTextBox1.Visibility = Visibility.Visible;
                ImperialTextBox2.Visibility = Visibility.Visible;
                ImperialTextBlock1.Visibility = Visibility.Visible;
                ImperialTextBlock2.Visibility = Visibility.Visible;
            }
        }

        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
    }
}