namespace Store.Domain.Spec;

public class WhenCreatingAMessageId
{
    #region Requirements

    [Fact]
    public void FromGuid_ThenValueIsExpected()
    {
        var source = Guid.NewGuid();

        MessageId messageId = source;

        messageId.Value.Should().Be(source);
    }

    [Fact]
    public void ThenValueIsNotEmpty()
    {
        var messageId = new MessageId();

        messageId.Value.Should().NotBeEmpty();
    }

    [Fact]
    public void WithGuid_ThenValueIsExpected()
    {
        var source = Guid.NewGuid();

        var messageId = new MessageId(source);

        messageId.Value.Should().Be(source);
    }

    #endregion
}
