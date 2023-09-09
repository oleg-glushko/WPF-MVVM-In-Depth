using System.Windows.Controls;

namespace MVVMHookupDemo.Customers;

public partial class CustomerListView : UserControl
{
    public CustomerListView()
    {
        InitializeComponent();
        DataContext = new CustomerListViewModel();
    }
}
