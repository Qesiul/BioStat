using System.Windows.Input;

namespace BioStat.Core;

public class RelayCommand : ICommand
{
    
    public bool CanExecute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public void Execute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public event EventHandler? CanExecuteChanged;
}