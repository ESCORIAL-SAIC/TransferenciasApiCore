using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransferenciasApiCore.Models;
using TransferenciasApiCore.Models.SolicitudTransferencia;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscoTxItemSolicitudTransferenciaController(ESCORIALContext context) : ControllerBase
    {
        // PUT: api/EscoTxItemSolicitudTransferencia/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutEscoTxItemSolicitudTransferencia(int id, EscoTxItemSolicitudTransferenciaUpdatePickeadoDto updatePickeado)
        {
            if (id != updatePickeado.Id)
                return BadRequest();
            var itemSolicitud = new EscoTxItemSolicitudTransferencia { Id = id };
            context.EscoTxItemSolicitudTransferencia.Attach(itemSolicitud);
            itemSolicitud.Pickeado = updatePickeado.Pickeado;
            context.Entry(itemSolicitud).Property(e => e.Pickeado).IsModified = true;
            await using (var transaction = await context.Database.BeginTransactionAsync()) 
            try
            {
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                if (!EscoTxItemSolicitudTransferenciaExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        private bool EscoTxItemSolicitudTransferenciaExists(int id) =>
            context.EscoTxItemSolicitudTransferencia.Any(e => e.Id == id);
        
    }
}
