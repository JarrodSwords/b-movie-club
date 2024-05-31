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
        yield return new object[] { new FooRenamed("MyFoo") };
        yield return new object[] { new BarAdded(new Bar(NewGuid())) };
    }

    public static IEnumerable<object[]> GetExpectedPositionTestCases()
    {
        var fooCreated = new Message(NewGuid(), NewGuid(), "FooManagement", 0, false, 0, UtcNow, "FooCreated");
        var fooRenamed = new Message(NewGuid(), NewGuid(), "FooManagement", 1, false, 1, UtcNow, "FooRenamed");

        yield return new object[] { 1, fooCreated };
        yield return new object[] { 2, fooCreated, fooRenamed };
    }

    #endregion

    #region Requirements

    [Fact]
    public void GivenAnEmptyStream_ThenPositionIs0()
    {
        var candidateMessage = _stream.Next(MessageType.Create("FooRegistered")).Value!;

        candidateMessage.Expected.Should().Be(0u);
    }

    [Theory]
    [MemberData(nameof(GetDataTestCases))]
    public void ThenDataIsExpected(object data)
    {
        var candidateMessage = _stream.Next(data).Value!;

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
    [MemberData(nameof(GetExpectedPositionTestCases))]
    public void ThenPositionIsStreamVersionPlus1(uint expectedPosition, params Message[] messages)
    {
        var stream = new Stream(messages);
        var barAdded = new BarAdded(new Bar(NewGuid()));

        var candidateMessage = stream.Next(barAdded).Value!;

        candidateMessage.Expected.Should().Be(expectedPosition);
    }

    #endregion
}
