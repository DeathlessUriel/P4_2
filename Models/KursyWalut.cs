using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("Kursy_Walut")]
[Index("IdValuty", "Data", Name = "UQ_Kurs", IsUnique = true)]
public partial class KursyWalut
{
    [Key]
    [Column("ID_Kursu")]
    public int IdKursu { get; set; }

    [Column("ID_Valuty")]
    public int IdValuty { get; set; }

    public DateOnly Data { get; set; }

    [Column("Wartosc_kursu", TypeName = "decimal(10, 4)")]
    public decimal WartoscKursu { get; set; }

    [ForeignKey("IdValuty")]
    [InverseProperty("KursyWaluts")]
    public virtual Waluty IdValutyNavigation { get; set; } = null!;

    [InverseProperty("IdKursuNavigation")]
    public virtual ICollection<Koszty> Koszties { get; set; } = new List<Koszty>();

    [InverseProperty("IdKursuNavigation")]
    public virtual ICollection<Zlecenie> Zlecenies { get; set; } = new List<Zlecenie>();
}
