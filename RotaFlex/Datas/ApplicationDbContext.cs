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
        public DbSet<Viagem> Viagens { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<TransportePublico> TransportesPublico { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chave primária padrão
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Motorista>().HasKey(m => m.IdMotorista);
            modelBuilder.Entity<Carro>().HasKey(c => c.IdCarro);
            modelBuilder.Entity<Viagem>().HasKey(co => co.IdCorrida);
            modelBuilder.Entity<TransportePublico>().HasKey(t => t.IdTransporte);

            modelBuilder.Entity<Motorista>()
                .HasOne(m => m.Carro)
                .WithOne(c => c.Motorista)
                .HasForeignKey<Carro>(c => c.MotoristaId);

            // Relacionamentos (Corrida depende de Usuario, Motorista e Carro)
            modelBuilder.Entity<Viagem>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey("UsuarioId");

            modelBuilder.Entity<Viagem>()
                .HasOne(c => c.Motorista)
                .WithMany()
                .HasForeignKey("MotoristaId");

            modelBuilder.Entity<Viagem>()
                .HasOne(c => c.Carro)
                .WithMany()
                .HasForeignKey("CarroId");

            // Enum mapeado como string
            modelBuilder.Entity<TransportePublico>()
                .Property(t => t.Tipo)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
