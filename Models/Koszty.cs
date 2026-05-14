using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("Koszty")]
public partial class Koszty
{
    [Key]
    [Column("ID_Koszty")]
    public int IdKoszty { get; set; }

    [Column("Rodzaj_kosztu")]
    [StringLength(50)]
    [Unicode(false)]
    public string RodzajKosztu { get; set; } = null!;

    [Column("Opis_kosztu")]
    [StringLength(100)]
    [Unicode(false)]
    public string? OpisKosztu { get; set; }

    [Column("Wartosc_kosztu", TypeName = "decimal(10, 2)")]
    public decimal WartoscKosztu { get; set; }

    [Column("ID_Przejazd")]
    public int IdPrzejazd { get; set; }

    [Column("ID_Kursu")]
    public int? IdKursu { get; set; }

    [ForeignKey("IdKursu")]
    [InverseProperty("Koszties")]
    public virtual KursyWalut? IdKursuNavigation { get; set; }

    [ForeignKey("IdPrzejazd")]
    [InverseProperty("Koszties")]
    public virtual Przejazd IdPrzejazdNavigation { get; set; } = null!;
}
