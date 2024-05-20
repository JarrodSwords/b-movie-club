﻿namespace Store.Infrastructure.Dapper.Spec;

public class WhenConvertingToDomain
{
    #region Setup

    private readonly DomainMessage _message;
    private readonly Message _source;

    public WhenConvertingToDomain()
    {
        _source = new Message(Guid.NewGuid(), "FooCreated");
        _message = _source;
    }

    #endregion

    #region Requirements

    [Fact]
    public void ThenFieldsAreExpected()
    {
        using var scope = new AssertionScope();

        _message.Id.Should().Be(_source.Id);
        _message.Type.Value.Should().Be(_source.Type);
        _message.Timestamp.Should().Be(_source.Timestamp);
    }

    #endregion
}
