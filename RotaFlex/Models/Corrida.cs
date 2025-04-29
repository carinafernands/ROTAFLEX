namespace RotaFlex.Models
{
    public class Corrida
    {
        public int IdCorrida { get; set; }
        public Usuario Usuario { get; set; }
        public Carro Carro { get; set; }
        public Motorista Motorista { get; set; }
        public double Valor { get; set; }

        public Corrida()
        {
        }

        public Corrida(Usuario usuario, Carro carro, Motorista motorista, double valor)
        {
            Usuario = usuario;
            Carro = carro;
            Motorista = motorista;
            Valor = valor;
        }
    }
}
