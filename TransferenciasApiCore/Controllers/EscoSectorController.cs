using Microsoft.AspNetCore.Mvc;
using TransferenciasApiCore.Models;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscoSectorController(ESCORIALContext context) : ControllerBase
    {
        // GET: api/EscoSector/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EscoSector>> GetEscoSector(int id)
        {
            var escoSector = await context.EscoSector.FindAsync(id);

            if (escoSector == null)
            {
                return NotFound();
            }
            return escoSector;
        }
    }
}
