using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportApp.Models;

[Keyless]
public partial class WidokHistoriaKierowcy
{
    [Column("ID_Kierowca")]
    public int IdKierowca { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Imie { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Nazwisko { get; set; } = null!;

    [Column("ID_Przejazd")]
    public int IdPrzejazd { get; set; }

    [Column("Data_rozpoczecia", TypeName = "datetime")]
    public DateTime DataRozpoczecia { get; set; }

    [Column("Data_zakonczenia", TypeName = "datetime")]
    public DateTime DataZakonczenia { get; set; }
}
