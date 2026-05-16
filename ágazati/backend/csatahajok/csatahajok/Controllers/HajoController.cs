using csatahajok.DTOs;
using csatahajok.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csatahajok.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HajoController : ControllerBase
    {
        [HttpGet]
        public IActionResult All() // Minden hajót visszaad
        {
            try
            {
                using (var cx = new CsatahajokContext())
                {
                    var hajok = cx.Hajos.Select(x => new Hajo // Select-tel egy új Hajo típusú objektumot készítünk.
                    {
                        Nev = x.Nev,
                        Osztaly = x.Osztaly,
                        Felavatva = x.Felavatva,
                        AgyukSzama = x.AgyukSzama,
                        Kaliber = x.Kaliber,
                        Vizkiszoritas = x.Vizkiszoritas
                    }).ToList(); // Mivel több lehet, kötelező a ToList, egyébként hibát fog adni!! Ugyanígy a Where-nél is. Mindennél aminél több mint egy adat a válasz.

                    return Ok(hajok); // Visszaadjuk a megkreált hajókat.
                }

            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba a művelet végrehajtása közben. \n {ex.Message}");
            }
        }

        [HttpGet("kimenet/{csatanev}/{hajonev}")]
        public IActionResult TobbtablasGet(string csatanev, string hajonev) // Több táblát összekötő Get. Feladat: Egy hajó és egy csatanévből adjuk meg csata nevét és végét, a hajo nevét és vízkiszorítását, és a csata kimenetelét.
        {
            try
            {
                using (var cx = new CsatahajokContext())
                {
                    var kimenet = cx.Kimenets.FirstOrDefault(k => k.Hajo==hajonev && k.Csata == csatanev); // Kiválasztjuk / megkeressük a kimenetet airől beszélgetünk.

                    if (kimenet == null)
                    {
                        return StatusCode(404, "A kimenet nem található a megadott adatokkal."); // Ha nincs, hibaüzenet
                    }

                    var hajo = cx.Hajos.FirstOrDefault(h => h.Nev == kimenet.Hajo); // Ha van megkeressük a hajót, mivel a kimenet táblából csak a nevét tudjuk, vízkiszorítását nem.
                    var csata = cx.Csata.FirstOrDefault(c => c.Nev == kimenet.Csata); // Ha van megkeressük a csatát, mivel a kimenet táblából csak a helyszínt tudjuk, a csata végét nem.

                    if(hajo == null || csata == null)
                    {
                        return StatusCode(404, "A hajó vagy csata nem található."); // Ha nincs, hibaüzenet
                    }

                    var valasz = new // Készítünk egy új objektumot, egy helyi DTO-t. Ilyen esetben nem feltétlenül kell külön DTO. Az adattagjait mi soroljuk fel és adjuk meg.
                    {
                        CsataNev = csata.Nev, // Tetszőleges név és a csata változóból, amit megtaláltunk a nevét kiszedjük.
                        CsataVég = csata.Befejezes, // Tetszőleges név és a csata változóból, amit megtaláltunk a csata végét kiszedjük.
                        HajoNev = hajo.Nev, // Tetszőleges név és a hajó változóból, amit megtaláltunk a nevét kiszedjük.
                        HajoSuly = hajo.Vizkiszoritas, // Tetszőleges név és a hajó változóból, amit megtaláltunk a vízkiszotítását kiszedjük.
                        Eredmeny = kimenet.Eredmeny // Tetszőleges név és a kimenet változóból, amit megtaláltunk az eredményt kiszedjük.
                    };


                    return Ok(valasz); // Visszaadjuk a kreált objektumot.
                }

            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba a művelet végrehajtása közben. \n {ex.Message}");
            }
        }

        [HttpGet("kimenetDTO/{csatanev}/{hajonev}")]
        public IActionResult TobbtablasGetDTOval(string csatanev, string hajonev) // Több táblát összekötő Get. Feladat: Egy hajó és egy csatanévből adjuk meg csata nevét és végét, a hajo nevét és vízkiszorítását, és a csata kimenetelét.
        {
            try
            {
                using (var cx = new CsatahajokContext())
                {
                    var kimenet = cx.Kimenets.FirstOrDefault(k => k.Hajo == hajonev && k.Csata == csatanev); // Kiválasztjuk / megkeressük a kimenetet airől beszélgetünk.

                    if (kimenet == null)
                    {
                        return StatusCode(404, "A kimenet nem található a megadott adatokkal."); // Ha nincs, hibaüzenet
                    }

                    var hajo = cx.Hajos.FirstOrDefault(h => h.Nev == kimenet.Hajo); // Ha van megkeressük a hajót, mivel a kimenet táblából csak a nevét tudjuk, vízkiszorítását nem.
                    var csata = cx.Csata.FirstOrDefault(c => c.Nev == kimenet.Csata); // Ha van megkeressük a csatát, mivel a kimenet táblából csak a helyszínt tudjuk, a csata végét nem.

                    if (hajo == null || csata == null)
                    {
                        return StatusCode(404, "A hajó vagy csata nem található."); // Ha nincs, hibaüzenet
                    }

                    var valasz = new KimenetDTO // Készítünk egy új objektumot, most DTO-t használva. Itt a DTO-ban megadott mezőket kell használnunk. Ezt tényleg csak legvégső esetben kell használni.
                    {
                        CsataNev = csata.Nev, // DTO név és a csata változóból, amit megtaláltunk a nevét kiszedjük.
                        CsataKezdes = csata.Kezdes, // DTO név és a csata változóból, amit megtaláltunk a csata kezdetét kiszedjük.
                        HajoNev = hajo.Nev, // DTO név és a hajó változóból, amit megtaláltunk a nevét kiszedjük.
                        HajoOsztaly = hajo.Osztaly, // DTO név és a hajó változóból, amit megtaláltunk az osztályt kiszedjük.
                        Eredmeny = kimenet.Eredmeny // DTO név és a kimenet változóból, amit megtaláltunk az eredményt kiszedjük.
                    };


                    return Ok(valasz); // Visszaadjuk a kreált objektumot.
                }

            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba a művelet végrehajtása közben. \n {ex.Message}");
            }
        }


        [HttpGet("ByName/{name}")] //Minden végpont ezzel kezdődik, ez határozza meg a végpont típusát. Lehet HttpGet, HttpPost, HttpPut, HttpDelete. Több azonos típusú kérés esetén bővíteni kell az url, vagy hibát fog adni. -> HttpGet("valami"). Ha paramétert/változót szeretnél az urlben kérni, akkor azt így lehet. -> HttpGet("{változó}").
        
        public IActionResult ByName(string name) //Végpont. Pont ugyanaz mint egy normál függvény, csak nem static, void, string stb, hanem IActionResult. Paraméterként az url-be írt változónevet kell megadni (ugyanazzal a névvel), és a típusát.
        {
            try //try catch. A feladatok első hibaüzenetét simán megoldják, és általában elég is.
            {
                using (var cx = new CsatahajokContext()) //Adatbázis using. A cx (context) változóban elmenti az egész adatbázis összes tábláját és oszlopát. A valamiContext, tartalmazza a Scaffold után az egész adatbázist, kapcsolatokkal együtt.
                {
                    var hajo = cx.Hajos.FirstOrDefault(h => h.Nev == name); // A hajo változóban (minden változó legyen var típusú!) elmentjük a Hajo tábla első olyan hajóját amelnyek neve megegyezik az url-ben kapott névvel.

                    if(hajo != null) 
                    {
                        return Ok(hajo); // Ha megtaláltuk visszaadjuk
                    }
                    else
                    {
                        return StatusCode(404, $"Nincs ilyen nevű hajó: {name}"); // Ha nem hibával térünk vissza. Érdemes StatusCode-ot használni, mert itt egy az egyben megadhatod a feladat által várt hibakódot.
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba a művelet végrehajtása közben. \n {ex.Message}"); // Catch, hasonló hibaüzenet.
            }
        }
    }
}
