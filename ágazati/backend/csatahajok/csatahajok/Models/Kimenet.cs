using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace csatahajok.Models;

public partial class Kimenet
{
    public string Hajo { get; set; } = null!;

    public string Csata { get; set; } = null!;

    public string Eredmeny { get; set; } = null!;

    // A [JsonIgnore] teljesen kiszedi a Swaggerből az adott mezőt. A ? csak engedélyezi a null értéket, de ott marad. (akármilyen csúnyán is néz ki).

    /*Példa a Swaggerből:
     
     Alapból megjelenő:

    {
  "hajo": "string",
  "csata": "string",
  "eredmeny": "string",
  "csataNavigation": {
    "nev": "string",
    "kezdes": "2026-05-13T17:30:05.013Z",
    "befejezes": "2026-05-13T17:30:05.013Z",
    "kimenets": [
      "string"
    ]
  },
  "hajoNavigation": {
    "nev": "string",
    "osztaly": "string",
    "felavatva": 0,
    "agyukSzama": 0,
    "kaliber": 0,
    "vizkiszoritas": 0
  }
}

    -------------------------------------------------

    Teszteléshez tökéletes: (Ha nincs már ilyen az adtbázisban)
     
     {
  "hajo": "Iowa",
  "csata": "North Cape",
  "eredmeny": "Ok",
  "csataNavigation": null,
  "hajoNavigation": null
}
     
     */


    //public virtual Csatum CsataNavigation { get; set; } = null!; NEM JÓ!!
    public virtual Csatum? CsataNavigation { get; set; } // Ilyenre kell átírni. A ? a null engedélyezi, így a végéről is ki kell törölni a null-t.


    public virtual Hajo? HajoNavigation { get; set; } // Ugyanaz mint a CsataNavigation.
}
