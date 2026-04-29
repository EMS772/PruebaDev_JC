using PruebaDev_JC.Models.DTO_s;

namespace PruebaDev_JC.Repositories.Interfaces
{
    public interface ICalculadoraRepository
    {
        Task<CalcularCuotaResponse> CalcularCuotaAsync(CalcularCuotaRequest request, string ip);
    }
}
