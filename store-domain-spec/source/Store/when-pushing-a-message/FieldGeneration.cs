using FluentAssertions.Execution;

namespace Store.Domain.Spec.Store;

public abstract partial class WhenPushingAMessage
{
    #region Implementation

    public static IEnumerable<object[]> GetData()
    {
        yield return new object[] { new FooCreated() };
        yield return new object[] { new FooRenamed("TheFoo") };
        yield return new object[] { new BarAdded(new Bar(NewGuid())) };
    }

    #endregion

    #region Requirements

    [Fact]
    public void ThenGlobalPositionIsPreviousMaxPlus1()
    {
        var createFoo1 = new CandidateMessage(new CreateFoo(), 0);
        var renameFoo = new CandidateMessage(new RenameFoo(), 0);
        var createFoo2 = new CandidateMessage(new CreateFoo(), 0);

        _store.Push(createFoo1);
        _store.Push(renameFoo);
        _store.Push(createFoo2);

        var message1 = _store.Find(createFoo1.MessageId).Value!;
        var message2 = _store.Find(renameFoo.MessageId).Value!;
        var message3 = _store.Find(createFoo2.MessageId).Value!;

        using var scope = new AssertionScope();
        message1.Metadata.GlobalPosition.Should().Be(0ul);
        message2.Metadata.GlobalPosition.Should().Be(1ul);
        message3.Metadata.GlobalPosition.Should().Be(2ul);
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void ThenMessageTypeIsDataTypeName(object data)
    {
        var candidateMessage = new CandidateMessage(data, 0);

        _store.Push(candidateMessage);

        var message = _store.Find(candidateMessage.MessageId).Value!;

        message.Type.Should().Be(data.GetType().Name);
    }

    [Fact]
    public void ThenTimestampIsGenerated()
    {
        var createFoo = new CandidateMessage(new CreateFoo(), 0);
        var renameFoo = new CandidateMessage(new RenameFoo(), 0);

        _store.Push(createFoo);
        _store.Push(renameFoo);

        var message1 = _store.Find(createFoo.MessageId).Value!;
        var message2 = _store.Find(renameFoo.MessageId).Value!;

        using var scope = new AssertionScope();
        message1.Metadata.Timestamp.Should().NotBe(MinValue);
        message2.Metadata.Timestamp.Should().BeAfter(message1.Metadata.Timestamp);
    }

    #endregion
}
