using csatahajok.DTOs;
using csatahajok.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csatahajok.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KimenetController : ControllerBase
    {
        [HttpPost] // Post. Itt bonyolult mert több táblás, nézd meg a Modelt is. Ha már létezik olyan amit fel akarsz tölteni nem fog működni!!!
        public async Task<IActionResult> UjKimenet([FromBody] Kimenet dto) // Aszinkron végpont. Annyiba különbözik, hogy ide kell írni, hogy async, illetve Task-ot kell használni. Paraméterként erősen ajánlott a FromBody Típus változónév. A FromBody a Swagger-ben a Típus szerint fogja megjeleníteni azt, hogy mit vár el. Mindent megfog jeleníteni, mindegy mi van. Tesztelni a feladatban megadott adatokkal kell, máshogy nem biztos, hogy az elvárt működést fogja hozni. Amint teszteljük a végpontot, a változónévben (itt dto), megkapod az összes adatot ami be lett írva.
        {
            try
            {
                using (var cx = new CsatahajokContext())
                {
                    var ujhajo = new Kimenet // Egy új Kimenet típusú objektumot hozunk létre
                    {
                        Hajo /*Az ujhajo adattagja Kimenet típusú.*/ = dto.Hajo, /*A dto-ban (változóban) kapott érték.*/
                        Csata = dto.Csata,
                        Eredmeny = dto.Eredmeny,
                        HajoNavigation = null, // A feldatban null-t kapott így itt is null értéket állítunk be. Hogy elkerüljük a körkörös Json hivatkozást, azaz ne legyen benne egyik tábla a másikba többször, null-t állítunk be. De a Modelt is módosítani kell.
                        CsataNavigation = null,
                    };

                    cx.Kimenets.Add(ujhajo); // Hozzáadjuk a Kiments listához a készített hajónkat. Post esetén Add, Put esetén Update, Delete esetén Remove.

                    //cx.Kimenets.Add(dto); Ha a beírandó értékek és a tábla teljesen megegyezik, így is lehet menteni, ekkor az előző részeket nem kell megírni.

                    await cx.SaveChangesAsync(); // Aszinkron miatt a mentés így alakul. await kötelező, és a SaveChangesAsync is. Ha a végpont nem aszinkron, akkor simán így néz ki: cx.SaveChanges(). Ez azért fontos sor, mert ha nincs itt a módosítások nem lesznek elmentve az adatbázisba.

                    return Ok("Sikeres mentés."); // Ha minden jó, üzenet. Ha a feladat kéri, itt is lehet értéket visszaadni. Projektben például, telefonfeltöltés után az új telefon azonosítója van visszaküldve, nem egy üzenet.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba rögzítés közben.\n{ex.Message}");
            }
        }

        

        [HttpDelete("/KimenetTorles/{csata}/{hajonev}")] // Több változó kérése url-ben.
        public IActionResult KimenetTorles(string csata, string hajonev) //Ekkor minden használni kívánt változót meg kell adni.
        {
            try
            {
                using(var cx = new CsatahajokContext())
                {
                    var torolhajo = cx.Kimenets.FirstOrDefault(k=>k.Hajo == hajonev && k.Csata == csata);
                    if(torolhajo == null)
                    {
                        return StatusCode(404, $"Nincs megfelelő csata vagy hajó: {csata}, {hajonev}");
                    }
                    

                    cx.Kimenets.Remove(torolhajo); // Törlés esetén Remove.
                    cx.SaveChanges(); // Nem aszinkron, így elég a sima SaveChanges().
                    return Ok("Sikeres törlés.");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba a trölés közben. \n {ex.Message}");
            }
        }

        //A feladat kérhet olyat, hogy csak egy megadott kód után végezzük el az adott lekérdezést. Ilyenkor a főprogramban létre kell hozni, !Main-en belül! egy public static string = "valami", hogy itt tudjunk rá hivatkozni.

        [HttpDelete("/KimenetTorlesKoddal/{csata}/{hajonev}/{uid}")] // Több változó kérése url-ben.
        public IActionResult KimenetTorles(string csata, string hajonev, string uid) //Ekkor minden használni kívánt változót meg kell adni.
        {
            try
            {
                using (var cx = new CsatahajokContext())
                {
                    if (uid == Program.UID) //Viszgáljuk, hogy a paraméterben megadott azonosító megegyezik-e az eltárolttal. Ha igen, szokásos Delete, ha nem, hibaüzenet.
                    {
                        var torolhajo = cx.Kimenets.FirstOrDefault(k => k.Hajo == hajonev && k.Csata == csata);
                        if (torolhajo == null)
                        {
                            return StatusCode(404, $"Nincs megfelelő csata vagy hajó: {csata}, {hajonev}");
                        }


                        cx.Kimenets.Remove(torolhajo); // Törlés esetén Remove.
                        cx.SaveChanges(); // Nem aszinkron, így elég a sima SaveChanges().
                        return Ok("Sikeres törlés.");

                    }
                    else
                    {
                        return StatusCode(400, "Nincs jogosultsága törölni.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Hiba a trölés közben. \n {ex.Message}");
            }
        }
    }


}