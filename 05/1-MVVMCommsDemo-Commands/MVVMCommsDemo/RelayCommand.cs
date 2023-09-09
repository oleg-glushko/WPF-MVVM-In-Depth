using System;
using System.Windows.Input;

namespace MVVMCommsDemo;
public class RelayCommand : ICommand
{
    private readonly Action _TargetExecuteMethod;
    private readonly Func<bool>? _TargetCanExecuteMethod;

    public event EventHandler? CanExecuteChanged;

    public RelayCommand(Action executeMethod)
    {
        _TargetExecuteMethod = executeMethod;
    }

    public RelayCommand(Action executeMethod, Func<bool> canExecuteMethod)
    {
        _TargetExecuteMethod = executeMethod;
        _TargetCanExecuteMethod = canExecuteMethod;
    }

    public void RiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(object? parameter)
    {
        if (_TargetCanExecuteMethod != null)
            return _TargetCanExecuteMethod();
        return true;
    }

    public void Execute(object? parameter)
    {
        _TargetExecuteMethod();
    }
}
