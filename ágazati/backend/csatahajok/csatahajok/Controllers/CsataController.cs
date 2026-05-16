using csatahajok.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csatahajok.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsataController : ControllerBase
    {
        [HttpGet("{name}")]
        public IActionResult Resztvevok(string name)
        {
            try
            {
                using(var cx = new CsatahajokContext())
                {
                    var csata = cx.Kimenets.Where(c => c.Csata == name).ToList(); // Where után kötelező a ToList.
                    if(csata == null)
                    {
                        return Ok();
                    }


                    List<string> hajok = new List<string>();

                    foreach(var c in csata)
                    {
                        hajok.Add(c.Hajo);
                    }
                    return Ok(hajok);

                }
            }
            catch(Exception ex)
            {
                return StatusCode(400, $"Hiba a művelet végrehajtása közben. \n {ex.Message}");
            }
        }
    }
}
