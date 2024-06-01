using FluentAssertions.Execution;

namespace Store.Domain.Spec;

public class WhenRehydratingAMessage
{
    #region Implementation

    public static IEnumerable<object[]> GetMessageParameters()
    {
        var entityId = NewGuid();

        yield return new object[]
            { NewGuid(), entityId, "FooManagement", new FooCreated(), 2, false, 1, UtcNow, "FooCreated" };
        yield return new object[]
            { NewGuid(), entityId, "FooManagement", new FooRenamed("OtherFoo"), 7, false, 4, UtcNow, "FooRenamed" };
    }

    #endregion

    #region Requirements

    [Theory]
    [MemberData(nameof(GetMessageParameters))]
    public void ThenFieldsAreExpected(
        Guid id,
        Guid entityId,
        string category,
        object data,
        ulong globalPosition,
        bool isCommand,
        uint position,
        DateTime timestamp,
        string type
    )
    {
        var message = new Message(
            id,
            entityId,
            category,
            data,
            globalPosition,
            isCommand,
            position,
            timestamp,
            type
        );

        using var scope = new AssertionScope();

        message.Id.Should().Be(id);
        message.Type.Should().Be(type);
        message.Metadata.Should().Be(
            new Metadata(
                globalPosition,
                position,
                new StreamId(Category.Create(category), new(entityId), isCommand),
                timestamp
            )
        );
    }

    #endregion
}
