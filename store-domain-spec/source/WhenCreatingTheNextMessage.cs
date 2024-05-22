using FluentAssertions.Execution;

namespace Store.Domain.Spec;

public class WhenCreatingACandidateMessage
{
    #region Setup

    private readonly Stream _stream;

    public WhenCreatingACandidateMessage()
    {
        _stream = new Stream(ObjectProvider.Category);
    }

    #endregion

    #region Requirements

    [Fact]
    public void GivenAnEmptyStream_ThenPositionIs0()
    {
        var candidateMessage = _stream.Next(MessageType.Create("FooRegistered")).Value!;

        candidateMessage.ExpectedPosition.Should().Be(0);
    }

    [Fact]
    public void ThenMessageIdIsGenerated()
    {
        var candidateMessage = _stream.Next(MessageType.Create("FooRenamed")).Value!;

        using var scope = new AssertionScope();
        candidateMessage.MessageId.Should().NotBeNull();
        candidateMessage.MessageId.Value.Should().NotBeEmpty();
    }

    #endregion
}
