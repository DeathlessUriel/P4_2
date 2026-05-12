using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Keyless]
public partial class WidokKosztZlecenium
{
    [Column("ID_Zlecenie")]
    public int IdZlecenie { get; set; }

    [Column("Suma_kosztow", TypeName = "decimal(38, 2)")]
    public decimal? SumaKosztow { get; set; }
}
