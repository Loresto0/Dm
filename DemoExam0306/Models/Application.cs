using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace DemoExam0306.Models;

public partial class Application
{
    public int Id { get; set; }

    public string Numberapplication { get; set; } = null!;

    public DateTime Dateaddapllication { get; set; }

    public string? Rent { get; set; }

    public int Type { get; set; }

    [NotMapped]
    public string TypeNav
    {
        get
        {
            return TypeNavigation.Nametype;
        }
    }

    public string? Description { get; set; }

    public int Client { get; set; }
    
    [NotMapped]
    public string ClientNav
    {
        get
        {
            return ClientNavigation.Nameuser;
        }
    }

    public int Status { get; set; }
    
    [NotMapped]
    public string StatusNav
    {
        get
        {
            return StatusNavigation.Statusname;
        }
    }

    public int Ispol { get; set; }
    
    [NotMapped]
    public string IspolNav
    {
        get
        {
            if (Ispol == 0 || Ispol == null)
            {
                return "Не выбран";
            }
            else
            {
                return IspolNavigation.Nameuser;
            }
        }
    }
    
    public string? Commentispol { get; set; }
    public virtual User ClientNavigation { get; set; } = null!;

    public virtual User? IspolNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Status StatusNavigation { get; set; } = null!;

    public virtual TypesApplic TypeNavigation { get; set; } = null!;
}


