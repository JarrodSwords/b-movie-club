﻿namespace Store.Spec;

public class WhenCreatingAMessage
{
    #region Requirements

    [Fact]
    public void ThenIdIsGenerated()
    {
        var message = new Message(MessageType.Create("FooCreated"));

        message.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void ThenMessageTypeIsSet()
    {
        MessageType type = MessageType.Create("FooCreated");
        var message = new Message(type);

        message.Type.Should().Be(type);
    }

    #endregion
}
