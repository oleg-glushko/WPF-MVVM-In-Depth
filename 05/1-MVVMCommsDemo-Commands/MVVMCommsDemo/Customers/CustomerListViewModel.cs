using MVVMCommsDemo.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zza.Data;

namespace MVVMCommsDemo.Customers;

public class CustomerListViewModel
{
    private readonly ICustomersRepository _repository = new CustomersRepository();
    private ObservableCollection<Customer> customers = new();
    private Customer? selectedCustomer;

    public ObservableCollection<Customer> Customers {
        get => customers;
        set => customers = value; 
    }

    public Customer? SelectedCustomer
    {
        get => selectedCustomer;
        set
        {
            selectedCustomer = value;
            DeleteCommand.RiseCanExecuteChanged();
        }
    }

    public RelayCommand DeleteCommand { get; init; } = null!;

    public CustomerListViewModel()
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

        DeleteCommand = new RelayCommand(OnDelete, CanDelete);

        Customers = new ObservableCollection<Customer>(
            Task.Run(_repository.GetCustomersAsync).Result);
    }

    private bool CanDelete()
    {
        return SelectedCustomer != null;
    }

    private void OnDelete()
    {
        if (SelectedCustomer != null)
        {
            Customers.Remove(SelectedCustomer);
        }
    }
}
