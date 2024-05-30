namespace Store.Infrastructure.Dapper.Spec;

public class WhenConvertingToInfrastructure
{
    #region Setup

    private readonly Message _message;
    private readonly DomainMessage _source;

    public WhenConvertingToInfrastructure()
    {
        _source = new DomainMessage(MessageType.Create("FooCreated"), 123);
        _message = _source;
    }

    #endregion

    #region Requirements

    [Fact]
    public void ThenFieldsAreExpected()
    {
        using var scope = new AssertionScope();

        _message.Id.Should().Be(_source.Id);
        _message.Type.Should().Be(_source.Type);
        _message.Position.Should().Be(_source.Position);
    }

    [Fact]
    public void ThenTimestampIsCreated()
    {
        _message.Timestamp.Should().NotBe(DateTime.MinValue);
    }

    #endregion
}
