using System;
using System.Windows.Input;

namespace ZzaDashboard;

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


public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _TargetExecuteMethod;
    private readonly Func<T?, bool>? _TargetCanExecuteMethod;

    public event EventHandler? CanExecuteChanged;

    public RelayCommand(Action<T> executeMethod)
    {
        _TargetExecuteMethod = executeMethod;
    }

    public RelayCommand(Action<T> executeMethod, Func<T?, bool> canExecuteMethod)
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
        {
            var param = (T?)parameter;
            return _TargetCanExecuteMethod(param);
        }
        return true;
    }

    public void Execute(object? parameter)
    {
        T? param = (T?)parameter;
        if (param != null)
            _TargetExecuteMethod(param);
    }
}
