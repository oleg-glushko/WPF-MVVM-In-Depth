using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers;

public partial class CustomerEditView : UserControl
{
    private readonly ICustomersRepository _customersRepository = new CustomersRepository();
    private Customer? _customer = null;

    public static readonly DependencyProperty CustomerIdProperty =
           DependencyProperty.Register("CustomerId", typeof(string),
           typeof(CustomerEditView), new PropertyMetadata(string.Empty));

    public string CustomerId
    {
        get { return (string)GetValue(CustomerIdProperty); }
        set { SetValue(CustomerIdProperty, value); }
    }


    public CustomerEditView()
    {
        InitializeComponent();
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DesignerProperties.GetIsInDesignMode(this)) return;

        _customer = await _customersRepository.GetCustomerAsync(new Guid(CustomerId));
        if (_customer == null) return;
        firstNameTextBox.Text = _customer.FirstName;
        lastNameTextBox.Text = _customer.LastName;
        phoneTextBox.Text = _customer.Phone;
    }

    private async void OnSave(object sender, RoutedEventArgs e)
    {
        // TODO: Validate input... call business rules... etc...
        if (_customer != null)
        {
            _customer.FirstName = firstNameTextBox.Text;
            _customer.LastName = lastNameTextBox.Text;
            _customer.Phone = phoneTextBox.Text;
            await _customersRepository.UpdateCustomerAsync(_customer);
        }
    }
}
