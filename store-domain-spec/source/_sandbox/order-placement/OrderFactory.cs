using Jgs.Errors.Results;

namespace Store.Domain.Spec;

public partial class OrderFactory
{
    private readonly ISerializer _serializer;
    private static readonly Dictionary<string, Func<Message, Order, Result<Order>>> Reducers = new();

    public OrderFactory(ISerializer serializer)
    {
        _serializer = serializer;

        void Register<T>(Func<Message, Order, Result<Order>> reducer) =>
            Reducers.Add(
                typeof(T).Name,
                reducer
            );

        Register<OrderOpened>(OrderOpened);
    }

    public static Order Create(Stream stream) =>
        stream.Aggregate<Message, Order>(
            null,
            (current, message) => Reducers[message.Type](message, current)
        );
}
