namespace Store.Domain.Spec;

public class OpenOrderHandler : Handler
{
    public OpenOrderHandler(
        IMessageStore store,
        ISerializer serializer,
        OrderBuilder builder
    ) : base(
        store,
        serializer,
        builder
    )
    {
    }

    protected override string Type { get; }

    public override Result Foo() => throw new NotImplementedException();
    public override Result Foo(Order order) => throw new NotImplementedException();

    private Result OpenOrder(Message message)
    {
        return Serializer.Deserialize<OpenOrder>(message)
            .Then(
                x =>
                {
                    var (userId, timestamp) = x;
                    var stream = new Stream(Category);
                    return stream
                        .Next(new OrderOpened(stream.Id.EntityId, userId, timestamp))
                        .Then(Store.Push);
                }
            );
    }
}

public partial class OrderBuilder
{
    public Result<Order> OrderOpened(Message message, Order target)
    {
        return _serializer.Deserialize<OrderOpened>(message)
            .Then<Order>(
                x =>
                {
                    var (orderId, userId, timestamp) = x;
                    return new Order(orderId, userId, timestamp);
                }
            );
    }
}

public partial class Order
{
    public Order(Guid id, Guid customerId, DateTime opened)
    {
        Id = id;
        CustomerId = customerId;
        Opened = opened;
        State = OrderState.Opened;
    }
}

public record OpenOrder(
    Guid UserId,
    DateTime Timestamp
) : IPayload;

public record OrderOpened(
    Guid OrderId,
    Guid UserId,
    DateTime Timestamp
) : IPayload;
