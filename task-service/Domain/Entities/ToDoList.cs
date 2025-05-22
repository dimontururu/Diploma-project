namespace task_service.Domain.Entities;

public partial class ToDoList
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid IdTask { get; set; }

    public Guid? IdUser { get; set; }

    public virtual Case IdTaskNavigation { get; set; } = null!;

    public virtual User? IdUserNavigation { get; set; }
}
