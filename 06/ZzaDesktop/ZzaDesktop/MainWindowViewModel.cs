using System;
using Zza.Data;
using ZzaDesktop.Customers;
using ZzaDesktop.OrderPrep;
using ZzaDesktop.Orders;

namespace ZzaDesktop;

public class MainWindowViewModel : BindableBase
{
    private readonly CustomerListViewModel _customerListViewModel = new();
    private readonly OrderViewModel _orderViewModel = new();
    private readonly OrderPrepViewModel _orderPrepViewModel = new();
    private readonly AddEditCustomerViewModel _addEditCustomerViewModel = new();

    private BindableBase? _currentViewModel;

    public BindableBase? CurrentViewModel {
        get => _currentViewModel;
        set {
            SetProperty(ref _currentViewModel, value);
        }
    }

    public RelayCommand<string> NavCommand { get; private set; }

    public MainWindowViewModel()
    {
        NavCommand = new RelayCommand<string>(OnNav);
        _customerListViewModel.PlaceOrderRequested += NavToOrder;
        _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
        _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
    }

    private void OnNav(string destination)
    {
        CurrentViewModel = destination switch
        {
            "orderPrep" => _orderPrepViewModel,
            _ => _customerListViewModel,
        };
    }

    private void NavToOrder(Guid customerId)
    {
        _orderViewModel.CustomerId = customerId;
        CurrentViewModel = _orderViewModel;
    }

    private void NavToAddCustomer(Customer customer)
    {
        _addEditCustomerViewModel.EditMode = false;
        _addEditCustomerViewModel.SetCustomer(customer);
        CurrentViewModel = _addEditCustomerViewModel;
    }

    private void NavToEditCustomer(Customer customer)
    {
        _addEditCustomerViewModel.EditMode = true;
        _addEditCustomerViewModel.SetCustomer(customer);
        CurrentViewModel = _addEditCustomerViewModel;
    }

}
