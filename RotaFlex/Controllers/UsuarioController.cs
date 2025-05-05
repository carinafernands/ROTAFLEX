using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaFlex.Datas;
using RotaFlex.Models;

namespace RotaFlex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return Ok(await _context.Usuarios.ToListAsync());
        }

        // GET: api/usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            return Ok(usuario);
        }

        // GET: api/usuario/viagens/5
        [HttpGet("viagens/{id}")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetAllViagensUsuario(int id)
        {
            var todasViagens = await _context.Viagens
                .Include(v => v.Usuario)
                .Include(v => v.Motorista)
                    .ThenInclude(v => v.Carro)
                .Where(v => v.UsuarioId == id)
                .ToListAsync();

            if (todasViagens == null || !todasViagens.Any())
                return NotFound(new { mensagem = "Usuário não tem viagens registradas." });

            var resultado = todasViagens.Select(v => new {
                v.IdCorrida,
                v.Valor,
                Usuario = new
                {
                    v.Usuario.IdUsuario,
                    v.Usuario.Nome,
                    v.Usuario.Email,
                    v.Usuario.Cpf,
                    v.Usuario.Estado,
                    v.Usuario.Cidade
                },
                Motorista = new
                {
                    v.Motorista.IdMotorista,
                    v.Motorista.Nome,
                    v.Motorista.Email,
                    v.Motorista.Cpf,
                    v.Motorista.QtCorrida,
                    v.Motorista.Nota,
                    v.Motorista.Estado,
                    v.Motorista.Cidade,
                },
                Carro = new
                {
                    v.Carro.IdCarro,
                    v.Carro.Marca,
                    v.Carro.Modelo,
                    v.Carro.Placa,
                    v.Carro.Ano,
                    v.Carro.Cor
                }
            });

            return Ok(resultado);
        }

        // POST: api/usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> CriarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }


        // DELETE: api/usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
