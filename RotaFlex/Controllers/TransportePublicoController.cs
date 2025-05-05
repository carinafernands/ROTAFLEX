using Microsoft.AspNetCore.Mvc;
using RotaFlex.Models;
using System.Collections.Generic;
using System.Linq;

namespace RotaFlex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportePublicoController : ControllerBase
    {
        // Lista simulando um banco de dados em memória
        private static List<Transporte> transportes = new List<Transporte>();
        private static int proximoId = 1;

        // GET: api/transportePublico
        [HttpGet]
        public ActionResult<IEnumerable<Transporte>> Listar()
        {
            return Ok(transportes);
        }

        // GET: api/transportePublico/5
        [HttpGet("{id}")]
        public ActionResult<Transporte> ObterPorId(int id)
        {
            var transporte = transportes.FirstOrDefault(t => t.Id == id);
            if (transporte == null)
                return NotFound();
            return Ok(transporte);
        }

        // POST: api/transportePublico
        [HttpPost]
        public ActionResult<Transporte> Criar(Transporte transporte)
        {
            transporte.Id = proximoId++;
            transportes.Add(transporte);
            return CreatedAtAction(nameof(ObterPorId), new { id = transporte.Id }, transporte);
        }

        // PUT: api/transportePublico/5
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Transporte transporteAtualizado)
        {
            var transporte = transportes.FirstOrDefault(t => t.Id == id);
            if (transporte == null)
                return NotFound();

            transporte.Tipo = transporteAtualizado.Tipo;
            transporte.Cidade = transporteAtualizado.Cidade;
            transporte.Valor = transporteAtualizado.Valor;

            return NoContent();
        }

        // DELETE: api/transportePublico/5
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var transporte = transportes.FirstOrDefault(t => t.Id == id);
            if (transporte == null)
                return NotFound();

            transportes.Remove(transporte);
            return NoContent();
        }
    }
}