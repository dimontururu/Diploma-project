using System;
using System.Collections.Generic;

namespace task_service.Model;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ToDoList> ToDoLists { get; set; } = new List<ToDoList>();

    public virtual ICollection<Award> IdAwards { get; set; } = new List<Award>();
}
