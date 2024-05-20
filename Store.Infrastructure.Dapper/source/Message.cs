using DomainMessage = Store.Domain.Message;

namespace Store.Infrastructure.Dapper;

public record Message(Guid Id, string Type)
{
    public static implicit operator Message(DomainMessage source) => new(source.Id, source.Type);
}
