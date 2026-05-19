using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("ZdjeciaPojazdu")] 
public partial class ZdjeciaPojazdu
{
    [Key] 
    [Column("ID_Zdjecia")]
    public int IdZdjecia { get; set; }

    [Column("ID_Pojazd")]
    public int IdPojazd { get; set; }

    [Column("Sciezka")]
    public string Sciezka { get; set; } = null!;

    [ForeignKey("IdPojazd")]
    [InverseProperty("ZdjeciaPojazdus")] 
    public virtual Pojazd IdPojazdNavigation { get; set; } = null!;
}