﻿using Jgs.Errors.Results;

namespace Store.Domain.Spec;

public partial class OrderPlacementService
{
    private readonly Dictionary<string, Func<Message, Result>> _handlers = new();
    private readonly IMessageStore _store;
    private static readonly Category Category = Category.Create("OrderPlacement").Value!;

    private OrderPlacementService(IMessageStore store)
    {
        _store = store;

        void Add(Func<Message, Result> handler) => _handlers.Add(handler.GetType().Name, handler);

        Add(OpenOrder);
        Add(DiscardOrder);
    }

    public Result Handle(Message message) =>
        _handlers.ContainsKey(message.Type)
            ? _handlers[message.Type](message)
            : Success();
}
