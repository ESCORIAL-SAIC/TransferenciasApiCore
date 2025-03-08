using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransferenciasApiCore.Models;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController(ESCORIALContext context) : ControllerBase
    {
        private readonly ESCORIALContext _context = context;

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<producto>>> GetProductos() =>
            await _context.Producto.ToListAsync();

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<producto>> GetProducto(Guid id) =>
            await _context.Producto.FindAsync(id) is producto producto ? producto : NotFound();

    }
}
