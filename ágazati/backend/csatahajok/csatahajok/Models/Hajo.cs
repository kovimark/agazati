using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace csatahajok.Models;

public partial class Hajo
{
    public string Nev { get; set; } = null!;

    public string Osztaly { get; set; } = null!;

    public int Felavatva { get; set; }

    public int AgyukSzama { get; set; }

    public int Kaliber { get; set; }

    public int Vizkiszoritas { get; set; }

    [JsonIgnore]
    public virtual ICollection<Kimenet> Kimenets { get; set; } = new List<Kimenet>();
}
