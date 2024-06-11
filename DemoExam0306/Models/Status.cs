using System;
using System.Collections.Generic;

namespace DemoExam0306.Models;

public partial class Status
{
    public int Idstatus { get; set; }

    public string Statusname { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
