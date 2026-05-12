using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("Zlecenie")]
[Index("NumerZlecenia", Name = "UQ__Zlecenie__73693E0EE731BF07", IsUnique = true)]
public partial class Zlecenie
{
    [Key]
    [Column("ID_Zlecenie")]
    public int IdZlecenie { get; set; }

    [Column("Numer_zlecenia")]
    [StringLength(50)]
    [Unicode(false)]
    public string NumerZlecenia { get; set; } = null!;

    [Column("NIP_Klienta")]
    [StringLength(10)]
    [Unicode(false)]
    public string NipKlienta { get; set; } = null!;

    [Column("Data_przyjecia")]
    public DateOnly DataPrzyjecia { get; set; }

    [Column("Opis_transportu")]
    [StringLength(100)]
    [Unicode(false)]
    public string? OpisTransportu { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Waga { get; set; }

    [Column("Termin_zaladunku")]
    public DateOnly? TerminZaladunku { get; set; }

    [Column("Termin_rozladunku")]
    public DateOnly? TerminRozladunku { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Kwota { get; set; }

    [Column("ID_Kursu")]
    public int? IdKursu { get; set; }

    [Column("Adres_Zaladunku")]
    public int AdresZaladunku { get; set; }

    [Column("Adres_Rozladunku")]
    public int AdresRozladunku { get; set; }

    [Column("LDM", TypeName = "decimal(10, 2)")]
    public decimal? Ldm { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Status { get; set; }

    [ForeignKey("AdresRozladunku")]
    [InverseProperty("ZlecenieAdresRozladunkuNavigations")]
    public virtual Adresy AdresRozladunkuNavigation { get; set; } = null!;

    [ForeignKey("AdresZaladunku")]
    [InverseProperty("ZlecenieAdresZaladunkuNavigations")]
    public virtual Adresy AdresZaladunkuNavigation { get; set; } = null!;

    [ForeignKey("IdKursu")]
    [InverseProperty("Zlecenies")]
    public virtual KursyWalut? IdKursuNavigation { get; set; }

    [ForeignKey("IdZlecenie")]
    [InverseProperty("IdZlecenies")]
    public virtual ICollection<Przejazd> IdPrzejazds { get; set; } = new List<Przejazd>();
}
