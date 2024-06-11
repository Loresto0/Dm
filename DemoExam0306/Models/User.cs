using System;
using System.Collections.Generic;

namespace DemoExam0306.Models;

public partial class User
{
    public int Iduser { get; set; }

    public string Nameuser { get; set; } = null!;

    public int Idrole { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Application> ApplicationClientNavigations { get; set; } = new List<Application>();

    public virtual ICollection<Application> ApplicationIspolNavigations { get; set; } = new List<Application>();

    public virtual Role IdroleNavigation { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
