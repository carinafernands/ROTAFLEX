using RotaFlex.Models.Enums;

namespace RotaFlex.Models
{
    public class TransportePublico
    {
        public int IdTransporte { get; set; }
        public TipoTransporte Tipo { get; set; }
        public double Valor { get; set; }
        public char Estado { get; set; }
        public string Cidade { get; set; }

        public TransportePublico()
        {
        }

        public TransportePublico(TipoTransporte tipo, double valor, char estado, string cidade)
        {
            Tipo = tipo;
            Valor = valor;
            Estado = estado;
            Cidade = cidade;
        }
    }
}
