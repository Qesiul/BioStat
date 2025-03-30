using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

    public void NumberOnlyTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
    
    private void UnitTypeChanged(object sender, RoutedEventArgs e)
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
                WeightTextBlock.Text = "kg";
            }
            else if (ImperialUnitsRadioButton.IsChecked == true)
            {
                MetricTextBox.Visibility = Visibility.Collapsed;
                MetricTextBlock.Visibility = Visibility.Collapsed;
                ImperialTextBox1.Visibility = Visibility.Visible;
                ImperialTextBox2.Visibility = Visibility.Visible;
                ImperialTextBlock1.Visibility = Visibility.Visible;
                ImperialTextBlock2.Visibility = Visibility.Visible;
                WeightTextBlock.Text = "lbs";
            }
        }

        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
    }
}