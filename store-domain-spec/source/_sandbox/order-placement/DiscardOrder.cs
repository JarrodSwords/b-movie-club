namespace Store.Domain.Spec;

public partial class OrderPlacementService
{
    private Result DiscardOrder(Message message)
    {
        var stream = _store.Find(message.Metadata.StreamId).Value!;
        var order = OrderFactory.Create(stream);

        var result = order.Discard();

        IPayload payload = new DiscardOrderFailed();

        if (result.IsSuccess)
            payload = new OrderDiscarded();

        else if (result == Order.OrderAlreadyDiscarded())
            payload = new OrderAlreadyDiscarded();

        return _store.Push(stream.Next(payload));
    }
}

public partial class Order
{
    public Result Discard()
    {
        if (State == OrderState.Discarded)
            return DiscardOrderFailed();

        State = OrderState.Discarded;

        return Success();
    }

    public static Error DiscardOrderFailed() => new(nameof(DiscardOrderFailed));
    public static Error OrderAlreadyDiscarded() => new(nameof(OrderAlreadyDiscarded));
}

public record DiscardOrder;

public record OrderDiscarded : IPayload;

public record DiscardOrderFailed : IPayload;

public record OrderAlreadyDiscarded : IPayload;
