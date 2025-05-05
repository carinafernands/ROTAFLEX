namespace RotaFlex.Models
{
    public class Viagem
    {
        public int IdCorrida { get; set; }

        public int UsuarioId { get; set; }
        public int MotoristaId { get; set; }
        public int CarroId { get; set; }

        public Usuario Usuario { get; set; }
        public Carro Carro { get; set; }
        public Motorista Motorista { get; set; }
        public double Valor { get; set; }

        public Viagem()
        {
        }

        public Viagem(Usuario usuario, Carro carro, Motorista motorista, double valor)
        {
            Usuario = usuario;
            Carro = carro;
            Motorista = motorista;
            Valor = valor;
        }
    }
}
