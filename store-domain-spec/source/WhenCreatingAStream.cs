namespace Store.Domain.Spec;

public class WhenCreatingAStream
{
    #region Setup

    private static readonly Category _category = Category.Create("Foo");

    #endregion

    #region Requirements

    [Fact]
    public void ThenStreamIdIsGenerated()
    {
        var stream = new Stream(_category);

        stream.Id.Should().NotBeNull();
    }

    #endregion
}
