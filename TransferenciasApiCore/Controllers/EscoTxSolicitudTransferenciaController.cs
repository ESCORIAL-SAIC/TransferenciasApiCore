using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransferenciasApiCore.Models;
using TransferenciasApiCore.Models.SolicitudTransferencia;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscoTxSolicitudTransferenciaController(ESCORIALContext context, IMapper mapper) : ControllerBase
    {
        // GET: api/EscoTxSolicitudTransferencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscoTxSolicitudTransferencia>>> GetEscoTxSolicitudTransferencia()
        {
            var solicitudes = await context.EscoTxSolicitudTransferencia.ToListAsync();
            foreach (var solicitud in solicitudes)
            {
                var itemsSolicitud = await context.EscoTxItemSolicitudTransferencia.Where(i => i.SolicitudTransferenciaId == solicitud.Id).ToListAsync();
                solicitud.ItemsSolicitudTransferencia = itemsSolicitud;
            }
            return Ok(solicitudes);
        }

        // GET: api/EscoTxSolicitudTransferencia/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EscoTxSolicitudTransferencia>> GetEscoTxSolicitudTransferencia(int id)
        {
            var escoTxSolicitudTransferencia = await context.EscoTxSolicitudTransferencia.FindAsync(id);
            if (escoTxSolicitudTransferencia == null)
                return NotFound();
            escoTxSolicitudTransferencia.ItemsSolicitudTransferencia = await context.EscoTxItemSolicitudTransferencia.Where(i => i.SolicitudTransferenciaId == escoTxSolicitudTransferencia.Id).ToListAsync();
            return Ok(escoTxSolicitudTransferencia);
        }

        // GET: api/EscoTxSolicitudTransferencia/estado/5
        [HttpGet("estado/{estadoId:int}")]
        public async Task<ActionResult<EscoTxSolicitudTransferencia>> GetEscoTxSolicitudTransferenciaPorEstado(int estadoId)
        {
            var solicitudes = await context.EscoTxSolicitudTransferencia.Where(e => e.EstadoId == estadoId).ToListAsync();
            foreach (var solicitud in solicitudes)
            {
                var itemsSolicitud = await context.EscoTxItemSolicitudTransferencia.Where(i => i.SolicitudTransferenciaId == solicitud.Id).ToListAsync();
                solicitud.ItemsSolicitudTransferencia = itemsSolicitud;
            }
            return Ok(solicitudes);
        }

        // PUT: api/EscoTxSolicitudTransferencia/5/estado
        [HttpPut("{id:int}/estado")]
        public async Task<IActionResult> PutEscoTxSolicitudTransferencia(int id, [FromBody] EscoTxSolicitudTransferenciaUpdateEstadoDto updateEstadoDto)
        {
            if (id != updateEstadoDto.Id)
                return BadRequest();
            var solicitudTransferencia = new EscoTxSolicitudTransferencia { Id = id };
            context.EscoTxSolicitudTransferencia.Attach(solicitudTransferencia);
            solicitudTransferencia.EstadoId = updateEstadoDto.EstadoId;
            context.Entry(solicitudTransferencia).Property(e => e.EstadoId).IsModified = true;
            await using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    await transaction.RollbackAsync();
                    return !EscoTxSolicitudTransferenciaExists(id) ? NotFound() : BadRequest(ex.Message);
                }
            }
            return NoContent();
        }

        // POST: api/EscoTxSolicitudTransferencia
        [HttpPost]
        public async Task<ActionResult<EscoTxSolicitudTransferencia>> PostEscoTxSolicitudTransferencia(EscoTxSolicitudTransferenciaInsertDto solicitudTransferenciaDto)
        {
            var solicitudTransferencia = mapper.Map<EscoTxSolicitudTransferenciaInsertDto, EscoTxSolicitudTransferencia>(solicitudTransferenciaDto);
            solicitudTransferencia.Fecha = DateTime.Now;
            solicitudTransferencia.FechaModificacion = DateTime.Now;
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                context.EscoTxSolicitudTransferencia.Add(solicitudTransferencia);
                await context.SaveChangesAsync();
                solicitudTransferencia.ItemsSolicitudTransferencia.ForEach(item =>
                {
                    item.SolicitudTransferenciaId = solicitudTransferencia.Id;
                    context.EscoTxItemSolicitudTransferencia.Add(item);
                });
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return CreatedAtAction("GetEscoTxSolicitudTransferencia", new { id = solicitudTransferencia.Id }, solicitudTransferencia);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private bool EscoTxSolicitudTransferenciaExists(int id) =>
            context.EscoTxSolicitudTransferencia.Any(e => e.Id == id);
    }
}
