using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransferenciasApiCore.Models;

namespace TransferenciasApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscoTxConfigController(ESCORIALContext context) : ControllerBase
    {
        [Route("/api/appversion")]
        [HttpGet]
        public async Task<ActionResult<EscoTxConfig>> GetAppVersion()
        {
            var appVersion = await context
                .EscoTxConfig
                .FirstOrDefaultAsync(config => config.Clave == "AppVersion");

            if (appVersion == null)
                return NotFound();
            
            return Ok(appVersion);
        }
    }
}
