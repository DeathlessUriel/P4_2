using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("Waluty")]
[Index("KodIso", Name = "UQ__Waluty__1F4DE9F36C74FBFC", IsUnique = true)]
public partial class Waluty
{
    [Key]
    [Column("ID_Valuty")]
    public int IdValuty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nazwa { get; set; } = null!;

    [Column("Kod_ISO")]
    [StringLength(3)]
    [Unicode(false)]
    public string KodIso { get; set; } = null!;

    [InverseProperty("IdValutyNavigation")]
    public virtual ICollection<KursyWalut> KursyWaluts { get; set; } = new List<KursyWalut>();
}
