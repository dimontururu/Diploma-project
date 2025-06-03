namespace task_service.Domain.Entities;

public partial class ToDoList
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? IdUser { get; set; }

    public virtual ICollection<Case> cases { get; set; } = new List<Case>();

    public virtual User? IdUserNavigation { get; set; }
}
