using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("Adresy")]
public partial class Adresy
{
    [Key]
    [Column("ID_Adres")]
    public int IdAdres { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Kraj { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Miasto { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Ulica { get; set; } = null!;

    [Column("Numer_domu")]
    [StringLength(10)]
    [Unicode(false)]
    public string NumerDomu { get; set; } = null!;

    [InverseProperty("AdresDocelowyNavigation")]
    public virtual ICollection<Przejazd> PrzejazdAdresDocelowyNavigations { get; set; } = new List<Przejazd>();

    [InverseProperty("AdresStartuNavigation")]
    public virtual ICollection<Przejazd> PrzejazdAdresStartuNavigations { get; set; } = new List<Przejazd>();

    [InverseProperty("AdresRozladunkuNavigation")]
    public virtual ICollection<Zlecenie> ZlecenieAdresRozladunkuNavigations { get; set; } = new List<Zlecenie>();

    [InverseProperty("AdresZaladunkuNavigation")]
    public virtual ICollection<Zlecenie> ZlecenieAdresZaladunkuNavigations { get; set; } = new List<Zlecenie>();
}
