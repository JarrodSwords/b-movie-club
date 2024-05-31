using FluentAssertions.Execution;

namespace Store.Domain.Spec.Integration;

/// <summary>
/// </summary>
/// <remarks>
///     This integration test should be inherited by any candidate implementations.
/// </remarks>
public abstract partial class WhenPushingAMessage
{
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

    #endregion
}
