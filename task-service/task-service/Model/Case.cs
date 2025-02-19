using System;
using System.Collections.Generic;

namespace task_service.Model;

public partial class Case
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime DateOfCreation { get; set; }

    public DateTime DateEnd { get; set; }

    public virtual ICollection<ToDoList> ToDoLists { get; set; } = new List<ToDoList>();
}
