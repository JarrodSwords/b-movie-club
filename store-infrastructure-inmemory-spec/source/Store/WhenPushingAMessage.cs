namespace Store.Infrastructure.InMemory.Spec.Store;

public class WhenPushingAMessage : Domain.Spec.Store.WhenPushingAMessage
{
    public override IMessageStore CreateMessageStore() => new InMemoryMessageStore();
}
