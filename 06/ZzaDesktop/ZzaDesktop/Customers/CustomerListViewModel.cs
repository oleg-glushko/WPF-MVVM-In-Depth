using System;
using System.Collections.ObjectModel;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers;

public class CustomerListViewModel : BindableBase
{
    private readonly ICustomersRepository _repo = new CustomersRepository();
    private ObservableCollection<Customer> _customers = new();

    public ObservableCollection<Customer> Customers
    {
        get => _customers;
        set => SetProperty(ref _customers, value);
    }

    public RelayCommand<Customer> PlaceOrderCommand { get; set; }
    public RelayCommand AddCustomerCommand { get; set; }
    public RelayCommand<Customer> EditCustomerCommand { get; set; }

    public event Action<Guid>? PlaceOrderRequested;
    public event Action<Customer>? AddCustomerRequested;
    public event Action<Customer>? EditCustomerRequested;

    public CustomerListViewModel()
    {
        PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
        AddCustomerCommand = new RelayCommand(OnAddCustomer);
        EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
    }    

    private void OnPlaceOrder(Customer customer)
    {
        PlaceOrderRequested?.Invoke(customer.Id);
    }

    public async void LoadCustomers()
    {
        Customers = new ObservableCollection<Customer>(
            await _repo.GetCustomersAsync());
    }

    private void OnAddCustomer()
    {
        AddCustomerRequested?.Invoke(new Customer { Id = Guid.NewGuid() });
    }

    private void OnEditCustomer(Customer customer)
    {
        EditCustomerRequested?.Invoke(customer);
    }
}
