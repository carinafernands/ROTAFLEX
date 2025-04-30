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


        public DbSet<Carro> Carros { get; set; }
        public DbSet<Corrida> Corridas { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<TransportePublico> TransportesPublico { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
