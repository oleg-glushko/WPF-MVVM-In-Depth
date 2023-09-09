using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ZzaDesktop;

public class BindableBase : INotifyPropertyChanged
{
    protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(member, val) || propertyName is null) return;

        member = val;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
