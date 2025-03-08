using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransferenciasApiCore.Models;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemInventarioController(ESCORIALContext context, IMapper mapper) : ControllerBase
    {

        // POST api/<ItemInventarioController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<iteminventario>>> Post([FromBody] iteminventarioDto item)
        {
            var items = await context
                .ItemInventario
                .Where(i =>
                    i.ProductoId.Equals(item.ProductoId) &&
                    i.DepositoId.Equals(item.DepositoId))
                .FirstOrDefaultAsync();
            return Ok(items);
        }
    }
}
