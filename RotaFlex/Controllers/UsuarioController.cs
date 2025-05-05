using Microsoft.AspNetCore.Mvc;
using RotaFlex.Models;
using System.Collections.Generic;
using System.Linq;

namespace RotaFlex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        // Simula um banco de dados com dados em memória
        private static List<Usuario> usuarios = new List<Usuario>();
        private static int proximoId = 1;

        // GET: api/usuario
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Listar()
        {
            return Ok(usuarios);
        }

        // GET: api/usuario/5
        [HttpGet("{id}")]
        public ActionResult<Usuario> ObterPorId(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        // POST: api/usuario
        [HttpPost]
        public ActionResult<Usuario> Criar(Usuario usuario)
        {
            usuario.Id = proximoId++;
            usuarios.Add(usuario);
            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
        }

        // PUT: api/usuario/5
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuarioAtualizado)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound();

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Origem = usuarioAtualizado.Origem;
            usuario.Destino = usuarioAtualizado.Destino;

            return NoContent();
        }

        // DELETE: api/usuario/5
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound();

            usuarios.Remove(usuario);
            return NoContent();
        }
    }
}