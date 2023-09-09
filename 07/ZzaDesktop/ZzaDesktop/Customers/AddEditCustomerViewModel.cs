using System;
using System.ComponentModel;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers;

public class AddEditCustomerViewModel : BindableBase
{
    private readonly ICustomersRepository _repo;
    private bool _editMode;

    public bool EditMode {
        get => _editMode;
        set => SetProperty(ref _editMode, value);
    }

    private SimpleEditableCustomer? _customer;

    public SimpleEditableCustomer? Customer
    {
        get => _customer;
        set => SetProperty(ref _customer, value);
    }

    public RelayCommand CancelCommand { get; private set; }
    public RelayCommand SaveCommand { get; private set; }

    private Customer _editingCustomer = new();

    public event Action? Done;

    public AddEditCustomerViewModel(ICustomersRepository repo)
    {
        _repo = repo;
        CancelCommand = new RelayCommand(OnCancel);
        SaveCommand = new RelayCommand(OnSave, CanSave);

    }

    public void SetCustomer(Customer customer)
    {
        _editingCustomer = customer;
        if (Customer != null) Customer.ErrorsChanged -= RaiseCanExecuteChanged;
        Customer = new SimpleEditableCustomer();
        Customer.ErrorsChanged += RaiseCanExecuteChanged;
        CopyCustomer(customer, Customer);
    }

    private void RaiseCanExecuteChanged(object? sender, DataErrorsChangedEventArgs e)
    {
        SaveCommand.RaiseCanExecuteChanged();
    }

    private void CopyCustomer(Customer source, SimpleEditableCustomer target)
    {
        target.Id = source.Id;
        if (EditMode)
        {
            target.FirstName = source.FirstName ?? string.Empty;
            target.LastName = source.LastName ?? string.Empty;
            target.Phone = source.Phone ?? string.Empty;
            target.Email = source.Email ?? string.Empty;

        }
    }

    private void OnCancel()
    {
        Done?.Invoke();
    }

    private async void OnSave()
    {
        if (Customer == null) return;

        UpdateCustomer(Customer, _editingCustomer);

        if (EditMode)
            await _repo.UpdateCustomerAsync(_editingCustomer);
        else
            await _repo.AddCustomerAsync(_editingCustomer);

        Done?.Invoke();
    }

    private void UpdateCustomer(SimpleEditableCustomer source, Customer target)
    {
        target.FirstName = source.FirstName;
        target.LastName = source.LastName;
        target.Phone = source.Phone;
        target.Email = source.Email;
    }

    private bool CanSave()
    {
        return !Customer?.HasErrors ?? false;
    }
}
