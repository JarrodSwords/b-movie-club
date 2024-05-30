namespace Store.Infrastructure.InMemory.Spec.Integration;

public class WhenSavingAMessage : Domain.Spec.Integration.WhenSavingAMessage
{
    public override IMessageStore CreateMessageStore() => new InMemoryMessageStore();
}
