using Microsoft.AspNetCore.Mvc;
using RotaFlex.Models.DTO;
using RotaFlex.Services;

namespace RotaFlex.Controllers
{
    [ApiController]
    [Route("api/rota")]
    public class ViagemController : ControllerBase
    {
        private readonly GeoLocalizacaoService _geoService;

        public ViagemController(GeoLocalizacaoService geoService)
        {
            _geoService = geoService;
        }

        [HttpPost("calcular")]
        public async Task<ActionResult<List<OpcaoTransporteDTO>>> CalcularRota([FromBody] RotaDTO rota)
        {
            var distanciaKm = await _geoService.CalcularDistanciaAsync(rota.Origem, rota.Destino);

            if (distanciaKm <= 0)
                return BadRequest("Não foi possível calcular a distância.");

            var publico = new OpcaoTransporteDTO
            {
                Tipo = "Público",
                Descricao = "Ônibus + Metrô",
                ValorEstimado = 9.40,
                MeiosDeTransporte = new List<string> { "Ônibus", "Metrô" },
                DistanciaKm = distanciaKm
            };


            var privado = new OpcaoTransporteDTO
            {
                Tipo = "Privado",
                Descricao = "Corrida com motorista parceiro",
                ValorEstimado = Math.Round(distanciaKm * 2.7, 2),
                MeiosDeTransporte = new List<string> { "Carro Particular" },
                DistanciaKm = distanciaKm
            };

            return Ok(new List<OpcaoTransporteDTO> { publico, privado });
        }
    }
}
