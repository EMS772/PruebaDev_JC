using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaDev_JC.Models.Data;
using PruebaDev_JC.Models.DTO_s;
using PruebaDev_JC.Repositories.Interfaces;
using System.Data;

namespace PruebaDev_JC.Repositories
{
    public class CalculadoraRepository : ICalculadoraRepository
    {
        private readonly AppDbContext _context;

        public CalculadoraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CalcularCuotaResponse> CalcularCuotaAsync(CalcularCuotaRequest request, string ip)
        {
            var paramFecha = new SqlParameter("@FechaNacimiento", request.FechaNacimiento.Date);

            var paramMonto = new SqlParameter("@Monto", SqlDbType.Decimal)
            {
                Value = request.Monto,
                Precision = 18,
                Scale = 2
            };

            var paramMeses = new SqlParameter("@Meses", request.Meses);

            var paramIp = new SqlParameter("@IpConsulta", SqlDbType.VarChar, 45)
            {
                Value = ip ?? (object)DBNull.Value
            };

            var paramCuota = new SqlParameter("@ValorCuota", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };

            var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_CalcularCuota @FechaNacimiento, @Monto, @Meses, @IpConsulta, @ValorCuota OUTPUT, @Mensaje OUTPUT",
                paramFecha, paramMonto, paramMeses, paramIp, paramCuota, paramMensaje
            );

            var mensaje = paramMensaje.Value?.ToString() ?? "";
            var cuota = paramCuota.Value == DBNull.Value ? (decimal?)null : (decimal)paramCuota.Value;

            return new CalcularCuotaResponse
            {
                Exitoso = string.IsNullOrEmpty(mensaje),
                Mensaje = string.IsNullOrEmpty(mensaje) ? "Cuota calculada correctamente." : mensaje,
                ValorCuota = cuota
            };
        }
    }
}
