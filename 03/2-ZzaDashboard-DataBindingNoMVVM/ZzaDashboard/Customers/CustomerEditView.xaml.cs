using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Zza.Data;
using ZzaDashboard.Converters;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers;

public partial class CustomerEditView : UserControl
{
    private readonly ICustomersRepository _customersRepository = new CustomersRepository();
    private Customer? _customer = null;

    public static readonly DependencyProperty CustomerIdProperty =
           DependencyProperty.Register("CustomerId", typeof(Guid),
           typeof(CustomerEditView), new PropertyMetadata(Guid.Empty));

    [TypeConverter(typeof(StringToGuidConverter))]
    public Guid CustomerId
    {
        get { return (Guid)GetValue(CustomerIdProperty); }
        set { SetValue(CustomerIdProperty, value); }
    }

    public CustomerEditView()
    {
        InitializeComponent();
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DesignerProperties.GetIsInDesignMode(this)) return;

        _customer = await _customersRepository.GetCustomerAsync(CustomerId);
        DataContext = _customer;
    }

    private async void OnSave(object sender, RoutedEventArgs e)
    {
        // TODO: Validate input... call business rules... etc...
        if (_customer != null)
        {
            await _customersRepository.UpdateCustomerAsync(_customer);
        }
    }
}
