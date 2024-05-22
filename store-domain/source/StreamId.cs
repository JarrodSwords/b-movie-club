namespace Store.Domain;

public record StreamId(
    Category Category,
    EntityId EntityId,
    bool IsCommand
);
