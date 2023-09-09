using MVVMCommsDemo.Customers;

namespace MVVMCommsDemo;

public class MainWindowViewModel
{
    public object CurrentViewModel { get; set; }

    public MainWindowViewModel()
    {
        CurrentViewModel = new CustomerListViewModel();
    }
}
