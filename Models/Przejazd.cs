using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("Przejazd")]
public partial class Przejazd
{
    [Key]
    [Column("ID_Przejazd")]
    public int IdPrzejazd { get; set; }

    [Column("Data_rozpoczecia", TypeName = "datetime")]
    public DateTime DataRozpoczecia { get; set; }

    [Column("Data_zakonczenia", TypeName = "datetime")]
    public DateTime DataZakonczenia { get; set; }

    [Column("Adres_startu")]
    public int AdresStartu { get; set; }

    [Column("Adres_docelowy")]
    public int AdresDocelowy { get; set; }

    [Column("Stan_licznika_start")]
    public int? StanLicznikaStart { get; set; }

    [Column("Stan_licznika_stop")]
    public int? StanLicznikaStop { get; set; }

    [Column("ID_Pojazd")]
    public int IdPojazd { get; set; }

    [Column("ID_Kierowca")]
    public int IdKierowca { get; set; }

    [ForeignKey("AdresDocelowy")]
    [InverseProperty("PrzejazdAdresDocelowyNavigations")]
    public virtual Adresy AdresDocelowyNavigation { get; set; } = null!;

    [ForeignKey("AdresStartu")]
    [InverseProperty("PrzejazdAdresStartuNavigations")]
    public virtual Adresy AdresStartuNavigation { get; set; } = null!;

    [ForeignKey("IdKierowca")]
    [InverseProperty("Przejazds")]
    public virtual Kierowca IdKierowcaNavigation { get; set; } = null!;

    [ForeignKey("IdPojazd")]
    [InverseProperty("Przejazds")]
    public virtual Pojazd IdPojazdNavigation { get; set; } = null!;

    [InverseProperty("IdPrzejazdNavigation")]
    public virtual ICollection<Koszty> Koszties { get; set; } = new List<Koszty>();

    [ForeignKey("IdPrzejazd")]
    [InverseProperty("IdPrzejazds")]
    public virtual ICollection<Zlecenie> IdZlecenies { get; set; } = new List<Zlecenie>();
}
