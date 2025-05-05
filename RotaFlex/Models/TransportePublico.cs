using RotaFlex.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RotaFlex.Models
{
    public class TransportePublico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTransporte { get; set; }

        public TipoTransporte Tipo { get; set; }
        public double Valor { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }

        public TransportePublico()
        {
        }

        public TransportePublico(TipoTransporte tipo, double valor, string estado, string cidade)
        {
            Tipo = tipo;
            Valor = valor;
            Estado = estado;
            Cidade = cidade;
        }
    }
}
