namespace task_service.Domain.Entities;

public partial class Case
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime DateOfCreation { get; set; }

    public DateTime DateEnd { get; set; }

    public virtual ICollection<ToDoList> ToDoLists { get; set; } = new List<ToDoList>();
}
