﻿using FluentAssertions.Execution;

namespace Store.Domain.Spec.Store;

/// <summary>
/// </summary>
/// <remarks>
///     This integration test should be inherited by any candidate implementations.
/// </remarks>
public abstract partial class WhenPushingAMessage
{
    #region Requirements

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
        message1.Metadata.GlobalPosition.Should().Be(0ul);
        message2.Metadata.GlobalPosition.Should().Be(1ul);
        message3.Metadata.GlobalPosition.Should().Be(2ul);
    }

    [Fact]
    public void ThenTimestampIsGenerated()
    {
        var createFoo = new CandidateMessage(MessageType.Create("CreateFoo"), null, 0);
        var renameFoo = new CandidateMessage(MessageType.Create("RenameFoo"), null, 0);

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
