using System;
using System.Collections.Generic;

namespace csatahajok.Models;

public partial class Csatum
{
    public string Nev { get; set; } = null!;

    public DateTime Kezdes { get; set; }

    public DateTime Befejezes { get; set; }

    public virtual ICollection<Kimenet> Kimenets { get; set; } = new List<Kimenet>();
}
