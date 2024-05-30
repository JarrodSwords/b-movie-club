namespace Store.Infrastructure.InMemory.Spec.Integration;

public class WhenPushingAMessage : Domain.Spec.Integration.WhenPushingAMessage
{
    public override IMessageStore CreateMessageStore() => new InMemoryMessageStore();
}
