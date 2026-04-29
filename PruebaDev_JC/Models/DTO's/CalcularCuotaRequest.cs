namespace PruebaDev_JC.Models.DTO_s
{
    public class CalcularCuotaRequest
    {
        public DateTime FechaNacimiento { get; set; }
        public decimal Monto { get; set; }
        public int Meses { get; set; }
    }
}
