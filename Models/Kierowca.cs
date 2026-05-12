using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("Kierowca")]
[Index("NumerPrawaJazdy", Name = "UQ__Kierowca__84392B56FFA0FE22", IsUnique = true)]
[Index("NumerKartyKierowcy", Name = "UQ__Kierowca__FB107E3CCF12D1B6", IsUnique = true)]
public partial class Kierowca
{
    [Key]
    [Column("ID_Kierowca")]
    public int IdKierowca { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Imie { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Nazwisko { get; set; } = null!;

    [Column("Numer_prawa_jazdy")]
    [StringLength(50)]
    [Unicode(false)]
    public string NumerPrawaJazdy { get; set; } = null!;

    [Column("Data_zatrudnienia")]
    public DateOnly DataZatrudnienia { get; set; }

    [Column("Telefon_sluzbowy")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TelefonSluzbowy { get; set; }

    [Column("Numer_karty_kierowcy")]
    [StringLength(50)]
    [Unicode(false)]
    public string? NumerKartyKierowcy { get; set; }

    [InverseProperty("IdKierowcaNavigation")]
    public virtual ICollection<Przejazd> Przejazds { get; set; } = new List<Przejazd>();
}
