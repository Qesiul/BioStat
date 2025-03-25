using System.Windows;

namespace BioStat.Core;

public class ErrorHandler
{

    public static void HandleError(Exception ex, string title = "Something unexpected happened!")
    {
        Console.Error.WriteLine($"{DateTime.Now}: {ex}");

        MessageBox.Show(
            ex.Message,
            title,
            MessageBoxButton.OK,
            MessageBoxImage.Error
        );
    }
    
}