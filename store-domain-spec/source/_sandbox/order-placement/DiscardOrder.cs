namespace Store.Domain.Spec;

public partial class OrderPlacementService
{
    private Result DiscardOrder(Message message)
    {
        return _builder.Build(message)
            .Then(order => order.Discard())
            .Then(() => _builder.Stream.Next(new OrderDiscarded()))
            .Catch(error => _builder.Stream.Next(Order.DiscardOrderFailed()))
            .Then(_store.Push);
    }
}

public partial class Order
{
    public Result Discard()
    {
        if (State == OrderState.Discarded)
            return OrderAlreadyDiscarded();

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
