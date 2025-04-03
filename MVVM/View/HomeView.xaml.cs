using System.Windows.Controls;

namespace BioStat.MVVM.View;

public partial class HomeView : UserControl
{
    public System.Windows.Style CalendarDayButtonStyle { get; set; }
    public HomeView()
    {
        InitializeComponent();
    }
}