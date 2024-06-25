namespace Store.Domain.Spec;

public partial class Order
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public DateTime Opened { get; }
    public OrderState State { get; private set; }
}
