using System;
using Zza.Data;

namespace ZzaDesktop.Customers;

public class AddEditCustomerViewModel : BindableBase
{
    private bool _editMode;

    public bool EditMode {
        get => _editMode;
        set => SetProperty(ref _editMode, value);
    }

    private Customer? _editingCustomer;


    public void SetCustomer(Customer customer)
    {
        _editingCustomer = customer;
    }
}
