using Microsoft.EntityFrameworkCore;
using RotaFlex.Models;

namespace RotaFlex.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<TransportePublico> TransportesPublico { get; set; }
    }
}
