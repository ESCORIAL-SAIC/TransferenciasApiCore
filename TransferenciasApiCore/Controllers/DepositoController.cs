using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TransferenciasApiCore.Models;

namespace TransferenciasApiCore.Controllers
{
    // rama desarrollo
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController(ESCORIALContext context) : ControllerBase
    {
        private readonly ESCORIALContext _context = context;

        // GET: api/Deposito
        [HttpGet]
        public ActionResult<IEnumerable<deposito>> GetDepositos()
        {
            var cultureInfo = new CultureInfo("es-ES");
            var depositos = _context.Deposito
                .Where(d => d.Activestatus == 0)
                .AsEnumerable()
                .Where(d => cultureInfo.CompareInfo.IndexOf(d.Nombre, "tecnico", CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) < 0)
                .ToList();
            return depositos.ToList();
        }

        // GET: api/Deposito/5
        [HttpGet("{id}")]
        public ActionResult<deposito> GetDeposito(Guid id)
        {
            var cultureInfo = new CultureInfo("es-ES");
            var depositos = _context.Deposito
                .Where(d => d.Activestatus == 0)
                .AsEnumerable()
                .Where(d => cultureInfo.CompareInfo.IndexOf(d.Nombre, "tecnico", CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) < 0)
                .ToList();
            var deposito = depositos.Find(d => d.Id == id);

            if (deposito == null)
            {
                return NotFound();
            }

            return deposito;
        }
    }
}
