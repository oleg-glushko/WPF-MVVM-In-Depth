using System;
using System.ComponentModel;
using System.Windows.Input;
using Zza.Data;
using ZzaDashboard.Converters;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers;

public class CustomerEditViewModel : INotifyPropertyChanged
{
    private Customer? _customer = null;
    private readonly ICustomersRepository _repository = new CustomersRepository();

    [TypeConverter(typeof(StringToGuidConverter))]
    public Guid CustomerId { get; set; }

    public Customer? Customer
    {
        get => _customer;
        set
        {
            if (value != _customer)
            {
                _customer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Customer)));
            }
        }
    }

    public ICommand SaveCommand { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public CustomerEditViewModel()
    {
        SaveCommand = new RelayCommand(OnSave);
    }

    public async void LoadCustomer()
    {
        Customer = await _repository.GetCustomerAsync(CustomerId);
    }

    private async void OnSave()
    {
        if (Customer != null)
        {
            Customer = await _repository.UpdateCustomerAsync(Customer);
        }
    }
}
