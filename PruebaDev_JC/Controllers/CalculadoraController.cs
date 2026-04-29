using Microsoft.AspNetCore.Mvc;
using PruebaDev_JC.Models.DTO_s;
using PruebaDev_JC.Services.Interfaces;

namespace PruebaDev_JC.Controllers
{
    [Route("calculadora")]
    [ApiController]
    public class CalculadoraController : ControllerBase
    {
        private readonly ICalculadoraService _service;

        public CalculadoraController(ICalculadoraService service)
        {
            _service = service;
        }

        [HttpPost("calcular")]
        public async Task<IActionResult> Calcular([FromBody] CalcularCuotaRequest request)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var result = await _service.CalcularCuotaAsync(request, ip);

            if (!result.Exitoso)
                return BadRequest(result);

            return Ok(result);
        }
    }
}