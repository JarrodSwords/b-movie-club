using Jgs.Errors.Results;

namespace Store.Domain.Spec;

public partial class OrderPlacementService
{
    private Result OpenOrder(Message message)
    {
        return _serializer.Deserialize<OpenOrder>(message)
            .Then(
                x =>
                {
                    var (userId, timestamp) = x;
                    var stream = new Stream(Category);
                    return stream
                        .Next(new OrderOpened(stream.Id.EntityId, userId, timestamp))
                        .Then(Push);
                }
            );
    }

    private Result Push(CandidateMessage message) => _store.Push(message);
}

public partial class OrderFactory
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
