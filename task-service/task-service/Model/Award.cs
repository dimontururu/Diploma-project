using System;
using System.Collections.Generic;

namespace task_service.Model;

public partial class Award
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> IdUsers { get; set; } = new List<User>();
}
