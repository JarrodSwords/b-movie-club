namespace Store.Domain.Spec;

public static class ObjectProvider
{
    public static Category Category = Category.Create("ExecuteDomainLogic");
}

public record BarAdded(Bar Bar);

public record FooRenamed(string Name);

public record Bar(Guid Id);

public record FooCreated;

public record CreateFoo;

public record RenameFoo;
