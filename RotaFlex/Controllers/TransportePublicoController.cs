using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaFlex.Datas;
using RotaFlex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaFlex.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportePublicoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransportePublicoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportePublico>>> Listar()
        {
            return Ok(await _context.TransportesPublico.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransportePublico>> ObterPorId(int id)
        {
            var transporte = await _context.TransportesPublico.FindAsync(id);
            if (transporte == null)
                return NotFound();

            return Ok(transporte);
        }

        [HttpPost]
        public async Task<ActionResult<TransportePublico>> Criar(TransportePublico transporte)
        {
            _context.TransportesPublico.Add(transporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = transporte.IdTransporte }, transporte);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, TransportePublico transporteAtualizado)
        {
            if (id != transporteAtualizado.IdTransporte)
                return BadRequest();

            _context.Entry(transporteAtualizado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var transporte = await _context.TransportesPublico.FindAsync(id);
            if (transporte == null)
                return NotFound();

            _context.TransportesPublico.Remove(transporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
