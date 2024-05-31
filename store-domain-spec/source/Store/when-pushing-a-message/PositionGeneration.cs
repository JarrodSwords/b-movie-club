using FluentAssertions.Execution;

namespace Store.Domain.Spec.Store;

public abstract partial class WhenPushingAMessage
{
    #region Setup

    private readonly StreamId _commandStreamId = new(Category.Create("FooManagement"), new(), true);

    #endregion

    #region Requirements

    [Fact]
    public void GivenANewEntity_ThenPositionIs0()
    {
        var createFoo1 = new CandidateMessage(new CreateFoo(), 0, _commandStreamId);
        var createFoo2 = new CandidateMessage(new CreateFoo(), 0, new(Category.Create("FooManagement"), new(), true));

        _store.Push(createFoo1);
        _store.Push(createFoo2);

        var message1 = _store.Find(createFoo1.MessageId).Value!;
        var message2 = _store.Find(createFoo2.MessageId).Value!;

        using var scope = new AssertionScope();
        message1.Metadata.Position.Should().Be(0u);
        message2.Metadata.Position.Should().Be(0u);
    }

    [Fact]
    public void GivenAnExistingEntity_ThenPositionIsPreviousMaxPlus1()
    {
        var createFoo = new CandidateMessage(new CreateFoo(), 0, _commandStreamId);
        var renameFoo1 = new CandidateMessage(new RenameFoo(), 1, _commandStreamId);
        var renameFoo2 = new CandidateMessage(new RenameFoo(), 2, _commandStreamId);

        _store.Push(createFoo);
        _store.Push(renameFoo1);
        _store.Push(renameFoo2);

        var message1 = _store.Find(createFoo.MessageId).Value!;
        var message2 = _store.Find(renameFoo1.MessageId).Value!;
        var message3 = _store.Find(renameFoo2.MessageId).Value!;

        using var scope = new AssertionScope();
        message1.Metadata.GlobalPosition.Should().Be(0u);
        message2.Metadata.GlobalPosition.Should().Be(1u);
        message3.Metadata.GlobalPosition.Should().Be(2u);
    }

    #endregion
}
