using MVVMHookupDemo.Customers;

namespace MVVMHookupDemo;

public class MainWindowViewModel
{
    public object CurrentViewModel { get; set; }

    public MainWindowViewModel()
    {
        CurrentViewModel = new CustomerListViewModel();
    }
}
