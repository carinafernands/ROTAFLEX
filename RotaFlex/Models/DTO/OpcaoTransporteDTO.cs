namespace RotaFlex.Models.DTO
{
    public class OpcaoTransporteDTO
    {
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public double ValorEstimado { get; set; }
        public List<string> MeiosDeTransporte { get; set; } = new();
        public double DistanciaKm { get; set; }
    }
}
