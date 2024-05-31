namespace Store.Infrastructure.Dapper.Spec;

[Obsolete]
public class WhenConvertingToDomain
{
    #region Setup

    private readonly DomainMessage _message;
    private readonly Message _source;

    public WhenConvertingToDomain()
    {
        _source = new Message(Guid.NewGuid(), "FooCreated", 123, 3454);
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
        _message.Metadata.Timestamp.Should().Be(_source.Timestamp);
        _message.Metadata.Position.Should().Be(_source.Position);
    }

    #endregion
}
