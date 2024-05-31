﻿using FluentAssertions.Execution;

namespace Store.Domain.Spec;

public class WhenRehydratingAMessage
{
    #region Implementation

    public static IEnumerable<object[]> GetMessageParameters()
    {
        yield return new object[] { NewGuid(), "FooCreated", DateTime.UtcNow, 1, 2 };
        yield return new object[] { NewGuid(), "FooRenamed", DateTime.UtcNow, 4, 7 };
    }

    #endregion

    #region Requirements

    [Theory]
    [MemberData(nameof(GetMessageParameters))]
    public void ThenFieldsAreExpected(Guid id, string type, DateTime timestamp, ulong position, ulong globalPosition)
    {
        var message = new Message(id, type, timestamp, position, globalPosition);

        using var scope = new AssertionScope();

        message.Id.Value.Should().Be(id);
        message.Type.Value.Should().Be(type);
        message.Timestamp.Should().Be(timestamp);
        message.Position.Should().Be(position);
        message.GlobalPosition.Should().Be(globalPosition);
    }

    #endregion
}
