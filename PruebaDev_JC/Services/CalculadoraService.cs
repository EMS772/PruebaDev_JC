using PruebaDev_JC.Models.DTO_s;
using PruebaDev_JC.Repositories.Interfaces;
using PruebaDev_JC.Services.Interfaces;

namespace PruebaDev_JC.Services
{
    public class CalculadoraService : ICalculadoraService
    {
        private readonly ICalculadoraRepository _repository;

        public CalculadoraService(ICalculadoraRepository repository)
        {
            _repository = repository;
        }

        public async Task<CalcularCuotaResponse> CalcularCuotaAsync(CalcularCuotaRequest request, string ip)
        {
            // Validaciones basicas antes de ir a la BD
            if (request.Monto <= 0)
                return new CalcularCuotaResponse { Exitoso = false, Mensaje = "El monto debe ser mayor a 0." };

            if (request.FechaNacimiento >= DateTime.Today)
                return new CalcularCuotaResponse { Exitoso = false, Mensaje = "La fecha de nacimiento no es valida." };

            if (request.Monto > 999999999.99m)
                return new CalcularCuotaResponse { Exitoso = false, Mensaje = "El monto ingresado excede el limite permitido. Por favor visita una de nuestras sucursales." };

            return await _repository.CalcularCuotaAsync(request, ip);
        }
    }
}
