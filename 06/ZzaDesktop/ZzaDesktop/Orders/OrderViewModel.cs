using System;

namespace ZzaDesktop.Orders;

public class OrderViewModel : BindableBase
{
    private Guid _customerId;

    public Guid CustomerId {
        get => _customerId;
        set => SetProperty(ref _customerId, value);
    }
}
