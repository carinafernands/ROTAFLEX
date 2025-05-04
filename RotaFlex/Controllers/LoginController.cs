using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaFlex.Models.ViewModels;
using RotaFlex.Datas;
using RotaFlex.Models;

namespace SalesWebMvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Senha))
            {
                return BadRequest(new { mensagem = "Email e senha são obrigatórios." });
            }

            var dadosUser = await _context.Usuarios
                .FirstOrDefaultAsync(e => e.Email == model.Email);

            if (dadosUser == null)
            {
                return NotFound(new { mensagem = "Usuário não encontrado." });
            }

            bool senhaCorreta = Usuario.VerificarSenha(model.Senha, dadosUser.PasswordHash, dadosUser.Salt);
            if (!senhaCorreta)
            {
                return Unauthorized(new { mensagem = "Senha inválida." });
            }

            // Opcional: retornar os dados do usuário autenticado (sem senha!)
            return Ok(new
            {
                mensagem = "Login realizado com sucesso!",
                usuario = new
                {
                    dadosUser.Nome,
                    dadosUser.Email,
                    dadosUser.Estado,
                    dadosUser.Cidade
                }
            });
        }

        // POST: Verifica se o email existe no banco
        [HttpPost("EmailCheck")]
        public async Task<IActionResult> EmailCheck([FromBody] LoginViewModel model)
        {
            var emailCheck = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (emailCheck == null)
            {
                return NotFound(new { mensagem = "Email informado não consta no banco de dados" });
            }

            return Ok(new { mensagem = "Email encontrado", usuarioId = emailCheck.IdUsuario });
        }

        [HttpGet("EditPassword/{id}")]
        public async Task<IActionResult> EditPassword(int id)
        {
            var userSearch = await _context.Usuarios.FindAsync(id);

            if (userSearch == null)
            {
                return NotFound(new { mensagem = "Usuário não encontrado" });
            }

            return Ok(new { mensagem = "Usuário encontrado", userSearch.IdUsuario, userSearch.Email });
        }

        // POST: Atualiza a senha do usuário
        [HttpPost("EditPassword/{id}")]
        public async Task<IActionResult> EditPassword(int id, [FromBody] LoginViewModel model)
        {
            var userSearch = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);

            if (userSearch == null)
            {
                return NotFound(new { mensagem = "Usuário não encontrado" });
            }

            userSearch.GerarHash(model.Senha);
            _context.Usuarios.Update(userSearch);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Senha atualizada com sucesso!" });
        }
    }
}
