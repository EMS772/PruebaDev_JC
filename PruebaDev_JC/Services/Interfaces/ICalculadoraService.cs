using PruebaDev_JC.Models.DTO_s;

namespace PruebaDev_JC.Services.Interfaces
{
    public interface ICalculadoraService
    {
        Task<CalcularCuotaResponse> CalcularCuotaAsync(CalcularCuotaRequest request, string ip);
    }
}
