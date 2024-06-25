namespace Store.Domain.Spec;

public class DiscardOrderHandler : Handler
{
    public DiscardOrderHandler(
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

    protected override string Type => nameof(DiscardOrder);
    public override Result Foo() => throw new NotImplementedException();

    public override Result Foo(Order order) =>
        order.Discard()
            .Then(() => Stream.Next(new OrderDiscarded()))
            .Catch(
                error => Stream.Next(
                    error == Order.OrderAlreadyDiscarded()
                        ? new OrderAlreadyDiscarded()
                        : new DiscardOrderFailed()
                )
            )
            .Then(Store.Push);
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

    public static Error OrderAlreadyDiscarded() => new(nameof(OrderAlreadyDiscarded));
}

public record DiscardOrder : IPayload;

public record OrderDiscarded : IPayload;

public record DiscardOrderFailed : IPayload;

public record OrderAlreadyDiscarded : IPayload;
