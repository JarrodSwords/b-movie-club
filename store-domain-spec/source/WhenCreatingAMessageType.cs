namespace Store.Domain.Spec;

public class WhenCreatingAMessageType
{
    #region Requirements

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void WithoutName_ThenReturnNameRequiredError(string value)
    {
        var error = MessageType.Create(value).Error;

        error.Should().Be(MessageType.NameRequired());
    }

    #endregion
}
