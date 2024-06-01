using Jgs.Errors.Results;

namespace Store.Domain.Spec;

public partial class OrderPlacementService
{
    private Result OpenOrder(Message message)
    {
        var stream = new Stream(Category);
        var (userId, timestamp) = message.Data as OpenOrder;

        var @event = stream.Next(
            new OrderOpened(stream.Id.EntityId, userId, timestamp)
        );

        return _store.Push(@event);
    }
}

public partial class OrderFactory
{
    public static Order Reduce(OrderOpened @event, Order target)
    {
        var (orderId, userId, timestamp) = @event;

        return new Order(orderId, userId, timestamp);
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
