namespace Store.Domain.Spec;

public class WhenCreatingAStream
{
    #region Requirements

    [Fact]
    public void ThenStreamIdIsGenerated()
    {
        var stream = new Stream(ObjectProvider.Category);

        stream.Id.Should().NotBeNull();
    }

    #endregion
}
