using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransferenciasApiCore.Models;
using TransferenciasApiCore.Models.Transferencia;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscoTxTransferenciaController(ESCORIALContext context, IMapper mapper) : ControllerBase
    {

        // GET: api/EscoTxTransferencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscoTxTransferencia>>> GetEscoTxTransferencia()
        {
            var transferencias = await context.EscoTxTransferencia.ToListAsync();
            foreach (var transferencia in transferencias)
            {
                var itemsTransferencia = await context.EscoTxItemTransferencia.Where(i => i.TransferenciaId == transferencia.Id).ToListAsync();
                transferencia.ItemsTransferencia = itemsTransferencia;
            }
            return Ok(transferencias);
        }

        // GET: api/EscoTxTransferencia/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EscoTxTransferencia>> GetEscoTxTransferencia(int id)
        {
            var transferencia = await context.EscoTxTransferencia.FindAsync(id);
            if (transferencia is null)
                return NotFound();
            transferencia.ItemsTransferencia = await context.EscoTxItemTransferencia.Where(i => i.TransferenciaId == transferencia.Id).ToListAsync();
            return Ok(transferencia);
        }

        // GET: api/EscoTxTransferencia/estado/5
        [HttpGet("estado/{estadoId:int}")]
        public async Task<ActionResult<IEnumerable<EscoTxTransferencia>>> GetEscoTxTransferenciaPorEstado(int estadoId)
        {
            var transferencias = await context.EscoTxTransferencia.Where(e => e.EstadoId == estadoId).ToListAsync();
            foreach (var transferencia in transferencias)
            {
                var itemsTransferencia = await context.EscoTxItemTransferencia.Where(i => i.TransferenciaId == transferencia.Id).ToListAsync();
                transferencia.ItemsTransferencia = itemsTransferencia;
            }
            return Ok(transferencias);
        }

        // PUT: api/EscoTxTransferencia/5/estado
        [HttpPut("{id:int}/estado")]
        public async Task<IActionResult> PutEscoTxTransferencia(int id, [FromBody] EscoTxTransferenciaUpdateEstadoDto updateEstadoDto)
        {
            if (id != updateEstadoDto.Id)
                return BadRequest();
            var transferencia = new EscoTxTransferencia { Id = id };
            context.EscoTxTransferencia.Attach(transferencia);
            transferencia.EstadoId = updateEstadoDto.EstadoId;
            context.Entry(transferencia).Property(e => e.EstadoId).IsModified = true;

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
                    return !EscoTxTransferenciaExists(id) ? NotFound() : BadRequest(ex.Message);
                }
            }
            return NoContent();
        }

        // POST: api/EscoTxTransferencia
        [HttpPost]
        public async Task<ActionResult<EscoTxTransferencia>> PostEscoTxTransferencia(EscoTxTransferenciaInsertDto transferenciaDto)
        {
            var transferencia = mapper.Map<EscoTxTransferenciaInsertDto, EscoTxTransferencia>(transferenciaDto);
            transferencia.Fecha = DateTime.Now;
            transferencia.FechaModificacion = DateTime.Now;
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                context.EscoTxTransferencia.Add(transferencia);
                await context.SaveChangesAsync();
                transferencia.ItemsTransferencia.ForEach(item =>
                {
                    item.TransferenciaId = transferencia.Id;
                    context.EscoTxItemTransferencia.Add(item);
                }); 
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return CreatedAtAction("GetEscoTxTransferencia", new { id = transferencia.Id }, transferencia);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private bool EscoTxTransferenciaExists(int id) =>
            context.EscoTxTransferencia.Any(e => e.Id == id);
    }
}
