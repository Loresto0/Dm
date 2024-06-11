using System;
using System.Collections.Generic;

namespace DemoExam0306.Models;

public partial class TypesApplic
{
    public int Idtype { get; set; }

    public string Nametype { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
