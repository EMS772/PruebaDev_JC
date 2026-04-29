namespace PruebaDev_JC.Models.DTO_s
{
    public class CalcularCuotaResponse
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public decimal? ValorCuota { get; set; }
        public int Edad { get; set; }
        public decimal? Tasa { get; set; }
    }
}
