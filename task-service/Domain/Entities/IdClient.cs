namespace task_service.Domain.Entities;

public partial class IdClient
{
    public Guid IdUser { get; set; }

    public string IdClient1 { get; set; } = null!;

    public Guid? IdClientType { get; set; }

    public virtual ClientType? IdClientTypeNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
