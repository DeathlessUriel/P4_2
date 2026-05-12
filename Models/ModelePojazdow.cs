using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Table("Modele_Pojazdow")]
[Index("Marka", "Model", Name = "UQ_Marka_Model", IsUnique = true)]
public partial class ModelePojazdow
{
    [Key]
    [Column("ID_Modelu")]
    public int IdModelu { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Marka { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    [InverseProperty("IdModeluNavigation")]
    public virtual ICollection<Pojazd> Pojazds { get; set; } = new List<Pojazd>();
}
