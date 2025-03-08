using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransferenciasApiCore.Models;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscoTxEtiquetaController(ESCORIALContext context) : ControllerBase
    {
        // GET: api/EscoTxEtiqueta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscoTxEtiqueta>>> GetEscoTxEtiqueta()
        {
            return await context.EscoTxEtiqueta.ToListAsync();
        }

        // GET: api/EscoTxEtiqueta/0022576966473
        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<EscoTxEtiqueta>> GetEscoTxEtiqueta(string codigo)
        {
            var escoTxEtiqueta = await context.EscoTxEtiqueta.FirstOrDefaultAsync(e => e.Codigo == codigo);

            if (escoTxEtiqueta == null)
            {
                return NotFound();
            }

            return escoTxEtiqueta;
        }
    }
}
