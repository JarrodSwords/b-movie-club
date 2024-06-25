namespace Store.Domain.Spec;

public abstract class Handler
{
    protected static readonly Category Category = Category.Create("OrderPlacement").Value!;
    protected readonly ISerializer Serializer;
    protected readonly IMessageStore Store;
    private readonly OrderBuilder _builder;

    protected Handler(IMessageStore store, ISerializer serializer, OrderBuilder builder)
    {
        Store = store;
        Serializer = serializer;
        _builder = builder;
    }

    protected Stream Stream => _builder.Stream;
    protected abstract string Type { get; }

    public abstract Result Foo();
    public abstract Result Foo(Order order);

    public Result Handle(Message message)
    {
        if (message.Type != Type)
            return Success();

        if (message.Type == "OpenOrder")
            return Foo();

        return _builder.Build(message)
            .Then(Foo);
    }
}
