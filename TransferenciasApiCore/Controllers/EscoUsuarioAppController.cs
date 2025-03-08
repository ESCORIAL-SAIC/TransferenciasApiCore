using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransferenciasApiCore.Models;
using TransferenciasApiCore.Models.Usuario;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscoUsuarioAppController(ESCORIALContext context, IMapper mapper) : ControllerBase
    {
        // GET: api/EscoUsuarioApp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscoUsuarioApp>>> GetEscoUsuarioApps() =>
            await context.EscoUsuarioApp.ToListAsync();

        // GET: api/EscoUsuarioApp/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EscoUsuarioApp?>> GetEscoUsuarioApp(int id) => 
            await context.EscoUsuarioApp.FirstOrDefaultAsync(e => e.Id == id);

        // GET: api/EscoUsuarioApp/codigo/5
        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<EscoUsuarioApp>> GetEscoUsuarioApp(string codigo) =>
            await context.EscoUsuarioApp.FirstOrDefaultAsync(u => u.Codigo == codigo) is EscoUsuarioApp escoUsuarioApp ? Ok(escoUsuarioApp) : NotFound();

        // POST: api/EscoUsuarioApp
        [HttpPost("login")]
        public async Task<ActionResult<EscoUsuarioApp>> PostEscoUsuarioApp(EscoUsuarioAppLoginDto loginDto)
        {
            var login = mapper.Map<EscoUsuarioAppLoginDto, EscoUsuarioApp>(loginDto);
            var resultado = await context
                .EscoUsuarioApp
                .FirstOrDefaultAsync(u =>
                    u.Codigo == login.Codigo &&
                    u.Contrasena == login.Contrasena);
            return resultado is null ? NotFound() : Ok(resultado);
        }
    }
}