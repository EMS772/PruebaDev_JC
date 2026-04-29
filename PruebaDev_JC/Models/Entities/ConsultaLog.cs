namespace PruebaDev_JC.Models.Entities
{
    public class ConsultaLog
    {
        public int Id { get; set; }
        public DateTime FechaConsulta { get; set; }
        public int Edad { get; set; }
        public decimal Monto { get; set; }
        public int Meses { get; set; }
        public decimal? ValorCuota { get; set; }
        public string? IpConsulta { get; set; }
        public string? MensajeError { get; set; }
    }
}