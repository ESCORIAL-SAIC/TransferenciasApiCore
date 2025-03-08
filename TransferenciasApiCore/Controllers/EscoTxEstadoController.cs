using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransferenciasApiCore.Models;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscoTxEstadoController(ESCORIALContext context) : ControllerBase
    {
        // GET: api/EscoTxEstado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscoTxEstado>>> GetEscoTxEstados()
        {
            return await context.EscoTxEstado.ToListAsync();
        }

        // GET: api/EscoTxEstado/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EscoTxEstado>> GetEscoTxEstado(int id)
        {
            var escoTxEstado = await context.EscoTxEstado.FindAsync(id);

            if (escoTxEstado == null)
            {
                return NotFound();
            }

            return escoTxEstado;
        }
    }
}
