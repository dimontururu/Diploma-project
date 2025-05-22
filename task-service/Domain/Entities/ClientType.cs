namespace task_service.Domain.Entities;

public partial class ClientType
{
    public Guid Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<IdClient> IdClients { get; set; } = new List<IdClient>();
}
