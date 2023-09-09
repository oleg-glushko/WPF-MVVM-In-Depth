using System;
using System.ComponentModel.DataAnnotations;

namespace ZzaDesktop.Customers;

public class SimpleEditableCustomer : ValidatableBindableBase
{
    private Guid _id;

    public Guid Id {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    private string _firstName = string.Empty;

    [Required]
    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    private string _lastName = string.Empty;

    [Required]
    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    private string _email = string.Empty;

    [EmailAddress]
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    private string _phone = string.Empty;

    [Phone]
    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }
}
