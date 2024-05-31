using FluentAssertions.Execution;

namespace Store.Domain.Spec.Store;

public abstract partial class WhenPushingAMessage
{
    #region Requirements

    [Fact]
    public void GivenANewEntity_ThenPositionIs0()
    {
        var createFoo1 = new CandidateMessage(new CreateFoo(), 0);
        var createFoo2 = new CandidateMessage(new CreateFoo(), 0);

        _store.Push(createFoo1);
        _store.Push(createFoo2);

        var message1 = _store.Find(createFoo1.MessageId).Value!;
        var message2 = _store.Find(createFoo2.MessageId).Value!;

        using var scope = new AssertionScope();
        message1.Metadata.Position.Should().Be(0u);
        message2.Metadata.Position.Should().Be(0u);
    }

    #endregion

    //[Fact]
    //public void GivenAnExistingEntity_ThenPositionIsPreviousMaxPlus1()
    //{
    //    throw new NotImplementedException();
    //}
}
