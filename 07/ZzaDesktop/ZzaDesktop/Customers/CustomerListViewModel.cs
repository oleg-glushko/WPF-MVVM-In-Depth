using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Zza.Data;
using ZzaDesktop.Services;

namespace ZzaDesktop.Customers;

public class CustomerListViewModel : BindableBase
{
    private readonly ICustomersRepository _repo;

    private List<Customer> _allCustomers = new();

    private ObservableCollection<Customer> _customers = new();

    public ObservableCollection<Customer> Customers
    {
        get => _customers;
        set => SetProperty(ref _customers, value);
    }

    private string? _searchInput;

    public string? SearchInput {
        get => _searchInput;
        set {
            SetProperty(ref _searchInput, value);
            FilterCustomers(_searchInput);
        }
    }

    public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
    public RelayCommand AddCustomerCommand { get; private set; }
    public RelayCommand<Customer> EditCustomerCommand { get; private set; }
    public RelayCommand ClearSearchCommand { get; private set; }

    public event Action<Guid>? PlaceOrderRequested;
    public event Action<Customer>? AddCustomerRequested;
    public event Action<Customer>? EditCustomerRequested;

    public CustomerListViewModel(ICustomersRepository repo)
    {
        _repo = repo;
        PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
        AddCustomerCommand = new RelayCommand(OnAddCustomer);
        EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
        ClearSearchCommand = new RelayCommand(OnClearSearch);
    }

    private void OnPlaceOrder(Customer customer)
    {
        PlaceOrderRequested?.Invoke(customer.Id);
    }

    public async void LoadCustomers()
    {
        _allCustomers = await _repo.GetCustomersAsync();
        Customers = new ObservableCollection<Customer>(_allCustomers);
    }

    private void OnAddCustomer()
    {
        AddCustomerRequested?.Invoke(new Customer { Id = Guid.NewGuid() });
    }

    private void OnEditCustomer(Customer customer)
    {
        EditCustomerRequested?.Invoke(customer);
    }

    private void FilterCustomers(string? searchInput)
    {
        if (string.IsNullOrWhiteSpace(searchInput))
        {
            Customers = new ObservableCollection<Customer>(_allCustomers);
            return;
        };

        Customers = new ObservableCollection<Customer>(_allCustomers
            .Where(c => c.FullName.ToLower().Contains(searchInput)));
    }

    private void OnClearSearch()
    {
        SearchInput = null;
    }
}
