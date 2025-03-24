using System.Windows.Controls;
using System.Windows.Input;

namespace BioStat.MVVM.View;

public partial class BmiView : UserControl
{
    public BmiView()
    {
        InitializeComponent();
    }

    private void NumberOnlyTextBox(object sender, TextCompositionEventArgs e)
    {
        var model = new BioStat.MVVM.Model.BmiModel();
        model.NumberOnlyTextBox(sender, e);
    }
}