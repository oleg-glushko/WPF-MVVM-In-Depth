using MVVMCommsDemo.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Zza.Data;

namespace MVVMCommsDemo.Customers;

public class CustomerListViewModel : INotifyPropertyChanged
{
    private readonly ICustomersRepository _repository = new CustomersRepository();
    private ObservableCollection<Customer> _customers = new();
    private Customer? _selectedCustomer;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Customer> Customers {
        get => _customers;
        set
        {
            if (_customers != value)
            {
                _customers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Customers)));
            }
        }
    }

    public Customer? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            if (_selectedCustomer != value)
            {
                _selectedCustomer = value;
                DeleteCommand.RiseCanExecuteChanged();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCustomer)));
            }
        }
    }

    public RelayCommand DeleteCommand { get; init; }

    public CustomerListViewModel()
    {
        DeleteCommand = new RelayCommand(OnDelete, CanDelete);

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

    public async void LoadCustomers()
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

        Customers = new ObservableCollection<Customer>(await _repository.GetCustomersAsync());
    }
}
