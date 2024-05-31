using FluentAssertions.Execution;

namespace Store.Domain.Spec.Store;

public abstract partial class WhenPushingAMessage
{
    #region Requirements

    [Fact]
    public void ThenFieldsAreExpected()
    {
        var createFoo = new CandidateMessage(new CreateFoo(), 0);

        _store.Push(createFoo);

        var message = _store.Find(createFoo.MessageId).Value!;

        using var scope = new AssertionScope();

        message.Id.Should().Be(createFoo.MessageId);
        message.Metadata.Position.Should().Be(createFoo.Expected);
    }

    #endregion
}
