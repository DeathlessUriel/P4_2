using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("Pojazd")]
[Index("NumerRejestracyjny", Name = "UQ__Pojazd__581C138883747DF5", IsUnique = true)]
public partial class Pojazd
{
    [Key]
    [Column("ID_Pojazd")]
    public int IdPojazd { get; set; }

    [Column("Numer_rejestracyjny")]
    [StringLength(20)]
    [Unicode(false)]
    public string NumerRejestracyjny { get; set; } = null!;

    [Column("Rok_produkcji")]
    public int? RokProdukcji { get; set; }

    [Column("Dopuszczalna_ladownosc", TypeName = "decimal(10, 2)")]
    public decimal? DopuszczalnaLadownosc { get; set; }

    [Column("Data_przegladu")]
    public DateOnly? DataPrzegladu { get; set; }

    [Column("Data_ubezpieczenia")]
    public DateOnly? DataUbezpieczenia { get; set; }

    [Column("Pojemnosc_LDM", TypeName = "decimal(10, 2)")]
    public decimal? PojemnoscLdm { get; set; }

    [Column("ID_Modelu")]
    public int IdModelu { get; set; }

    [ForeignKey("IdModelu")]
    [InverseProperty("Pojazds")]
    public virtual ModelePojazdow IdModeluNavigation { get; set; } = null!;

    [InverseProperty("IdPojazdNavigation")]
    public virtual ICollection<Przejazd> Przejazds { get; set; } = new List<Przejazd>();
}
