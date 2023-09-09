using MVVMHookupDemo.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Zza.Data;

namespace MVVMHookupDemo.Customers;

public class CustomerListViewModel
{
    private readonly ICustomersRepository _repository = new CustomersRepository();
    private ObservableCollection<Customer> customers = new();

    public ObservableCollection<Customer> Customers {
        get => customers;
        set => customers = value; 
    }

    public CustomerListViewModel()
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

        Customers = new ObservableCollection<Customer>(
            Task.Run(_repository.GetCustomersAsync).Result);
    }
}
