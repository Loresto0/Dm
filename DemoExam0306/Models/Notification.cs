using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoExam0306.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int Nameemploye { get; set; }

    public int Status { get; set; }

    public int Numberapplication { get; set; }

    [NotMapped]
    public string GetNameEmploye
    {
        get
        {
            return "Исполнитель: " +  NameemployeNavigation.Nameuser;
        }
    }
    
    [NotMapped]
    public string GetStatus
    {
        get
        {
            return "Статус: " +  StatusNavigation.Statusname;
        }
    }
    
    
    [NotMapped]
    public string GetNumberApplication
    {
        get
        {
            return "Номер заявки: " +  NumberapplicationNavigation.Numberapplication;
        }
    }

    public virtual User NameemployeNavigation { get; set; } = null!;

    public virtual Application NumberapplicationNavigation { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;
}
