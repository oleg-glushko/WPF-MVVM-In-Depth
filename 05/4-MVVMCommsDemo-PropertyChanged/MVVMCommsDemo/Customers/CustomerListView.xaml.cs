using System.Windows.Controls;
using Zza.Data;

namespace MVVMCommsDemo.Customers;

public partial class CustomerListView : UserControl
{
    public CustomerListView()
    {
        InitializeComponent();
    }

    private void OnChangeCustomer(object sender, System.Windows.RoutedEventArgs e)
    {
        var cust = customerDataGrid.SelectedItem as Customer;
        if (cust != null)
        {
            cust.FirstName = "Changed in background";
        }
    }
}
