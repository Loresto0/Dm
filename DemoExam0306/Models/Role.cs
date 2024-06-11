using System;
using System.Collections.Generic;

namespace DemoExam0306.Models;

public partial class Role
{
    public int Idrole { get; set; }

    public string Namerole { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
