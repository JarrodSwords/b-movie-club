namespace Store.Domain.Spec;

public partial class OrderFactory
{
    private static readonly Dictionary<Type, Func<IPayload, Order, Order>> _reducers = new();

    static OrderFactory()
    {
        void Register<T>(Func<T, Order, Order> reducer) where T : IPayload =>
            _reducers.Add(
                typeof(T),
                (payload, order) => reducer((T) payload, order)
            );

        Register<OrderOpened>(Reduce);
    }

    public static Order Create(Stream stream) =>
        stream.Aggregate<Message, Order>(
            null,
            (current, message) => _reducers[message.Data.GetType()](message.Data, current)
        );
}
