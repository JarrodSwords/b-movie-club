namespace Store.Spec;

public class WhenCreatingAMessage
{
    #region Requirements

    [Fact]
    public void ThenIdIsGenerated()
    {
        var message = new Message();

        message.Id.Should().NotBeEmpty();
    }

    #endregion
}
