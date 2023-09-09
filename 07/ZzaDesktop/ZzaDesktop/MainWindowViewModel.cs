using Microsoft.Extensions.DependencyInjection;
using System;
using Zza.Data;
using ZzaDesktop.Customers;
using ZzaDesktop.OrderPrep;
using ZzaDesktop.Orders;
using ZzaDesktop.Services;

namespace ZzaDesktop;

public class MainWindowViewModel : BindableBase
{
    private readonly OrderViewModel _orderViewModel = new();
    private readonly OrderPrepViewModel _orderPrepViewModel = new();
    private readonly CustomerListViewModel _customerListViewModel;
    private readonly AddEditCustomerViewModel _addEditCustomerViewModel;

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

        var repo = ContainerHelper.Container.GetRequiredService<ICustomersRepository>();
        _customerListViewModel = new(repo);
        _addEditCustomerViewModel = new(repo);

        _customerListViewModel.PlaceOrderRequested += NavToOrder;
        _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
        _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
        _addEditCustomerViewModel.Done += NavToCustomerList;
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

    private void NavToCustomerList()
    {
        CurrentViewModel = _customerListViewModel;
    }
}
