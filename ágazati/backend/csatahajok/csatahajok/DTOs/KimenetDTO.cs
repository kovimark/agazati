using csatahajok.Models;

namespace csatahajok.DTOs
{
    public class KimenetDTO // A DTO-ban te adhatod meg a kívánt mezőket. Fontos ha Post vagy Put van, az adatbázis elvárja, hogy a neki megfelelő típusokat add át. Ha bármi nem egyezik, nem tudja egy az egyben átalakítani a DTO-t a Modelre, a lekérdezés nem fog működni.
    {
        public string HajoNev { get; set; } = null!;
        public string HajoOsztaly { get; set; } = null!;

        public string CsataNev { get; set; } = null!;
        public DateTime CsataKezdes { get; set; }

        public string Eredmeny { get; set; } = null!;

    }
}
