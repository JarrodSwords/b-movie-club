namespace Store.Domain;

public class Stream
{
    public Stream(Category category, bool isCommand = false)
    {
        Id = new StreamId(category, new(), isCommand);
    }

    public StreamId Id { get; }
}
