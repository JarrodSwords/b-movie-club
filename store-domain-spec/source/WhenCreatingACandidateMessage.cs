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

    #region Implementation

    public static IEnumerable<object[]> GetDataTestCases()
    {
        yield return new object[] { "FooRenamed", new FooRenamed("MyFoo") };
        yield return new object[] { "BarAdded", new BarAdded(new Bar(NewGuid())) };
    }

    public static IEnumerable<object[]> GetExpectedPositionTestCases()
    {
        var fooCreated = new Message(NewGuid(), "FooCreated", DateTime.UtcNow, 0, 0);
        var fooRenamed = new Message(NewGuid(), "FooRenamed", DateTime.UtcNow, 1, 1);

        yield return new object[] { 1, fooCreated };
        yield return new object[] { 2, fooCreated, fooRenamed };
    }

    #endregion

    #region Requirements

    [Fact]
    public void GivenAnEmptyStream_ThenPositionIs0()
    {
        var candidateMessage = _stream.Next(MessageType.Create("FooRegistered")).Value!;

        candidateMessage.ExpectedPosition.Should().Be(0);
    }

    [Theory]
    [MemberData(nameof(GetDataTestCases))]
    public void ThenDataIsExpected(string messageType, object data)
    {
        var candidateMessage = _stream.Next(MessageType.Create(messageType), data).Value!;

        candidateMessage.Data.Should().Be(data);
    }

    [Fact]
    public void ThenMessageIdIsGenerated()
    {
        var candidateMessage = _stream.Next(MessageType.Create("FooRenamed")).Value!;

        using var scope = new AssertionScope();
        candidateMessage.MessageId.Should().NotBeNull();
        candidateMessage.MessageId.Value.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("FooRenamed")]
    [InlineData("BarAdded")]
    public void ThenMessageTypeIsExpected(string messageType)
    {
        var candidateMessage = _stream.Next(MessageType.Create(messageType)).Value!;

        candidateMessage.MessageType.Value.Should().Be(messageType);
    }

    [Theory]
    [MemberData(nameof(GetExpectedPositionTestCases))]
    public void ThenPositionIsStreamVersionPlus1(ulong expectedPosition, params Message[] messages)
    {
        var stream = new Stream(messages);

        var candidateMessage = stream.Next(MessageType.Create("BarAdded")).Value!;

        candidateMessage.ExpectedPosition.Should().Be(expectedPosition);
    }

    #endregion
}
