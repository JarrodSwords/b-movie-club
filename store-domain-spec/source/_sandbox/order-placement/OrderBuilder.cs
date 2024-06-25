namespace Store.Domain.Spec;

public partial class OrderBuilder
{
    private readonly ISerializer _serializer;
    private readonly IMessageStore _store;
    private static readonly Dictionary<string, Func<Message, Order, Result<Order>>> Reducers = new();

    public OrderBuilder(IMessageStore store, ISerializer serializer)
    {
        _store = store;
        _serializer = serializer;

        void Register<T>(Func<Message, Order, Result<Order>> reducer) =>
            Reducers.Add(
                typeof(T).Name,
                reducer
            );

        Register<OrderOpened>(OrderOpened);
    }

    public Stream Stream { get; private set; }

    public Result<Order> Build(Message message)
    {
        return _store.Find(message.Metadata.StreamId)
            .Then<Order>(
                stream =>
                {
                    Stream = stream;
                    return stream.Aggregate<Message, Order>(
                        null,
                        (current, message) => Reducers[message.Type](message, current)
                    );
                }
            );
    }
}
