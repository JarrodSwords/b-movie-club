namespace Store.Infrastructure.Dapper.Spec;

public class WhenConvertingToInfrastructure
{
    #region Requirements

    [Fact]
    public void ThenFieldsAreExpected()
    {
        var source = new DomainMessage(MessageType.Create("FooCreated"));

        Message message = source;

        using var scope = new AssertionScope();

        message.Id.Should().Be(source.Id);
        message.Type.Should().Be(source.Type);
    }

    #endregion
}
