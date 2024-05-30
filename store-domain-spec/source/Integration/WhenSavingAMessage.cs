using FluentAssertions.Execution;

namespace Store.Domain.Spec.Integration;

/// <summary>
/// </summary>
/// <remarks>
///     This integration test should be inherited by any candidate implementations.
/// </remarks>
public abstract class WhenSavingAMessage
{
    #region Setup

    private readonly IMessageStore _store;

    protected WhenSavingAMessage()
    {
        _store = CreateMessageStore();
    }

    #endregion

    #region Implementation

    public abstract IMessageStore CreateMessageStore();

    #endregion

    #region Requirements

    [Fact]
    public void ThenFieldsAreExpected()
    {
        var createFoo = new CandidateMessage(MessageType.Create("CreateFoo"), null, 0);

        _store.Push(createFoo);

        var message = _store.Find(createFoo.MessageId).Value!;

        using var scope = new AssertionScope();

        message.Id.Should().Be(createFoo.MessageId);
        message.Type.Should().Be(createFoo.MessageType);
        message.Position.Should().Be(createFoo.ExpectedPosition);
    }

    [Fact]
    public void ThenGlobalPositionIsPreviousMaxPlusOne()
    {
        var createFoo1 = new CandidateMessage(MessageType.Create("CreateFoo"), null, 0);
        var renameFoo = new CandidateMessage(MessageType.Create("RenameFoo"), null, 0);
        var createFoo2 = new CandidateMessage(MessageType.Create("CreateFoo"), null, 0);

        _store.Push(createFoo1);
        _store.Push(renameFoo);
        _store.Push(createFoo2);

        var message1 = _store.Find(createFoo1.MessageId).Value!;
        var message2 = _store.Find(renameFoo.MessageId).Value!;
        var message3 = _store.Find(createFoo2.MessageId).Value!;

        using var scope = new AssertionScope();
        message1.GlobalPosition.Should().Be(0);
        message2.GlobalPosition.Should().Be(1);
        message3.GlobalPosition.Should().Be(2);
    }

    [Fact]
    public void ThenMessageIsRetrievable()
    {
        var createFoo = new CandidateMessage(MessageType.Create("CreateFoo"), null, 0);

        _store.Push(createFoo);

        var message = _store.Find(createFoo.MessageId).Value!;

        message.Id.Should().Be(createFoo.MessageId);
    }

    [Fact]
    public void ThenTimestampIsCreated()
    {
        var createFoo = new CandidateMessage(MessageType.Create("CreateFoo"), null, 0);

        _store.Push(createFoo);

        var message = _store.Find(createFoo.MessageId).Value!;

        message.Timestamp.Should().NotBe(DateTime.MinValue);
    }

    #endregion
}
