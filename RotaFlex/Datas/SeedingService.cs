using RotaFlex.Models;
using RotaFlex.Models.Enums;

namespace RotaFlex.Datas
{
    public class SeedingService
    {
        private ApplicationDbContext _context;

        public SeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed() 
        {
            if (_context.Usuarios.Any() || _context.Carros.Any() || _context.Motoristas.Any() || _context.TransportesPublico.Any() || 
                _context.Viagens.Any())
            {
                Console.WriteLine("Database has been feeded");
                return;
            }

            // Criar Usuários e gerar Hashs
            Usuario usuario1 = new Usuario("João da Silva", "joao123@gmail.com", "14225336475", "RJ", "Rio de Janeiro");
            Usuario usuario2 = new Usuario("Mariana Oliveira", "mariana.oliveira@gmail.com", "39876215400", "SP", "São Paulo");
            Usuario usuario3 = new Usuario("Carlos Mendes", "carlos.mendes@hotmail.com", "62738491028", "MG", "Belo Horizonte");
            Usuario usuario4 = new Usuario("Ana Beatriz Souza", "ana.b.souza@yahoo.com", "51273849563", "BA", "Salvador");
            Usuario usuario5 = new Usuario("Pedro Henrique Lima", "pedro.henrique@outlook.com", "80371264951", "RS", "Porto Alegre");

            usuario1.GerarHash("Ab123!");
            usuario2.GerarHash("Bc456!");
            usuario3.GerarHash("Cd789!");
            usuario4.GerarHash("De012!");
            usuario5.GerarHash("Ef345!");

            _context.Usuarios.AddRange(usuario1, usuario2, usuario3, usuario4, usuario5);
            _context.SaveChanges();

            // Criar Motoristas
            Motorista motorista1 = new Motorista("Luiz Fernando", "luiz.fernando@gmail.com", "23456789100", 120, 5, "SP", "São Paulo");
            Motorista motorista2 = new Motorista("Patrícia Gomes", "patricia.gomes@hotmail.com", "34567891201", 85, 4, "RJ", "Niterói");
            Motorista motorista3 = new Motorista("Rafael Souza", "rafael.souza@outlook.com", "45678912302", 200, 5, "MG", "Uberlândia");
            Motorista motorista4 = new Motorista("Juliana Rocha", "juliana.rocha@yahoo.com", "56789123403", 60, 3, "BA", "Feira de Santana");
            Motorista motorista5 = new Motorista("André Lima", "andre.lima@gmail.com", "67891234504", 150, 4, "RS", "Caxias do Sul");

            _context.Motoristas.AddRange(motorista1, motorista2, motorista3, motorista4, motorista5);
            _context.SaveChanges();

            // Salvar Carros
            Carro carro1 = new Carro("Toyota", "Corolla", "ABC1D23", 2020, "Prata", motorista1);
            Carro carro2 = new Carro("Honda", "Civic", "XYZ4E56", 2019, "Preto", motorista2);
            Carro carro3 = new Carro("Chevrolet", "Onix", "JHG7F89", 2022, "Branco", motorista3);
            Carro carro4 = new Carro("Volkswagen", "Gol", "LMN3G12", 2018, "Vermelho", motorista4);
            Carro carro5 = new Carro("Fiat", "Argo", "QRS9H45", 2021, "Azul", motorista5);

            _context.Carros.AddRange(carro1, carro2, carro3, carro4, carro5);
            _context.SaveChanges();

            // Salvar Corridas
            Viagem viagem1 = new Viagem(usuario1, carro1, motorista1, 25.50);
            Viagem viagem2 = new Viagem(usuario2, carro2, motorista2, 32.80);
            Viagem viagem3 = new Viagem(usuario3, carro3, motorista3, 18.75);
            Viagem viagem4 = new Viagem(usuario4, carro4, motorista4, 45.00);
            Viagem viagem5 = new Viagem(usuario5, carro5, motorista5, 28.90);
            Viagem viagem6 = new Viagem(usuario1, carro2, motorista2, 22.10);
            Viagem viagem7 = new Viagem(usuario2, carro3, motorista3, 19.60);
            Viagem viagem8 = new Viagem(usuario3, carro1, motorista1, 30.20);
            Viagem viagem9 = new Viagem(usuario4, carro5, motorista5, 26.40);
            Viagem viagem10 = new Viagem(usuario5, carro4, motorista4, 35.75);

            _context.Viagens.AddRange(viagem1, viagem2, viagem3, viagem4, viagem5, viagem6, viagem7, viagem8, viagem9, viagem10);
            _context.SaveChanges();

            // Salvar Transportes Públicos conforme Estados
            // Estado: SP
            TransportePublico spOnibus = new TransportePublico(TipoTransporte.Onibus, 4.40, "SP", "São Paulo");
            TransportePublico spMetro = new TransportePublico(TipoTransporte.Metro, 5.00, "SP", "São Paulo");
            TransportePublico spTrem = new TransportePublico(TipoTransporte.Trem, 4.90, "SP", "Campinas");
            TransportePublico spVlt = new TransportePublico(TipoTransporte.Vlt, 3.80, "SP", "Santos");

            // Estado: RJ
            TransportePublico rjOnibus = new TransportePublico(TipoTransporte.Onibus, 4.30, "RJ", "Rio de Janeiro");
            TransportePublico rjMetro = new TransportePublico(TipoTransporte.Metro, 5.60, "RJ", "Rio de Janeiro");
            TransportePublico rjTrem = new TransportePublico(TipoTransporte.Trem, 4.70, "RJ", "Duque de Caxias");
            TransportePublico rjVlt = new TransportePublico(TipoTransporte.Vlt, 3.80, "RJ", "Rio de Janeiro");

            // Estado: MG
            TransportePublico mgOnibus = new TransportePublico(TipoTransporte.Onibus, 4.20, "MG", "Belo Horizonte");
            TransportePublico mgMetro = new TransportePublico(TipoTransporte.Metro, 3.90, "MG", "Belo Horizonte");
            TransportePublico mgTrem = new TransportePublico(TipoTransporte.Trem, 4.10, "MG", "Contagem");

            _context.TransportesPublico.AddRange(spOnibus, spMetro, spTrem, spVlt);
            _context.TransportesPublico.AddRange(rjOnibus, rjMetro, rjTrem, rjVlt);
            _context.TransportesPublico.AddRange(mgOnibus, mgMetro, mgTrem);
            _context.SaveChanges();
        }
    }
}
