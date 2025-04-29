namespace RotaFlex.Models
{
    public class Motorista
    {
        public int IdMotorista { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public int QtCorrida { get; set; }
        public int Nota { get; set; }
        public char Estado { get; set; }
        public string Cidade { get; set; }
        public Carro Carro { get; set; }

        public Motorista()
        {
        }

        public Motorista(string nome, string email, string cpf, int qtCorrida, int nota, char estado, string cidade)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            QtCorrida = qtCorrida;
            Nota = nota;
            Estado = estado;
            Cidade = cidade;
        }
    }
}
