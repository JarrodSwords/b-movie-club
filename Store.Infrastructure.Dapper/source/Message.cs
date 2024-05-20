﻿namespace Store.Infrastructure.Dapper;

public record Message(Guid Id, string Type)
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    public static implicit operator Message(DomainMessage source) => new(source.Id, source.Type);
    public static implicit operator DomainMessage(Message source) => new(source.Id, source.Type, source.Timestamp);
}
