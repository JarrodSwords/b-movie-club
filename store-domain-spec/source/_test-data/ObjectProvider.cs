namespace Store.Domain.Spec;

public static class ObjectProvider
{
    public static Category Category = Category.Create("Foo");
}

public record BarAdded(Bar Bar);

public record FooRenamed(string Name);

public record Bar(Guid Id);

public record FooCreated;
