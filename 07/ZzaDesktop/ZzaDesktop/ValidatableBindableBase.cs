using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ZzaDesktop;

public class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errors = new();

    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        return propertyName != null && _errors.ContainsKey(propertyName)
            ? _errors[propertyName]
            : Enumerable.Empty<string>();
    }

    protected override void SetProperty<T>(ref T member, T val, [CallerMemberName] string? propertyName = null)
    {
        base.SetProperty(ref member, val, propertyName);
        ValidateProperty(propertyName, val);
    }

    private void ValidateProperty<T>(string? propertyName, T? val)
    {
        if (propertyName is null) return;

        var results = new List<ValidationResult>();
        ValidationContext context = new(this) { MemberName = propertyName };
        Validator.TryValidateProperty(val, context, results);

        if (results.Any())
        {
            _errors[propertyName] = results.Select(c => c.ErrorMessage ?? string.Empty).ToList();
        } else
        {
            _errors.Remove(propertyName);
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
