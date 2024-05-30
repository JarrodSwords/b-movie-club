namespace Store.Infrastructure.Dapper.Spec.Integration;

public class WhenSavingAMessage : Domain.Spec.Integration.WhenSavingAMessage
{
    public override IMessageStore CreateMessageStore() => new MessageStore();
}
