using Newtonsoft.Json;
using RotaFlex.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RotaFlex.Services
{
    public class GeoLocalizacaoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5b3ce3597851110001cf6248b03715a9e74c4790b843c5f08cc33e0d";

        public GeoLocalizacaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> CalcularDistanciaAsync(string origem, string destino)
        {
            var url = "https://api.openrouteservice.org/v2/directions/driving-car";

            var coordenadasOrigem = await ObterCoordenadas(origem);
            var coordenadasDestino = await ObterCoordenadas(destino);

            Console.WriteLine($"Origem: {coordenadasOrigem.Latitude}, {coordenadasOrigem.Longitude}");
            Console.WriteLine($"Destino: {coordenadasDestino.Latitude}, {coordenadasDestino.Longitude}");

            var body = new
            {
                coordinates = new List<List<double>>
                {
                    new List<double> { coordenadasOrigem.Longitude, coordenadasOrigem.Latitude },
                    new List<double> { coordenadasDestino.Longitude, coordenadasDestino.Latitude }
                }
            };

            var jsonBody = JsonConvert.SerializeObject(body);

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Authorization", _apiKey); 

            var response = await _httpClient.SendAsync(request); 

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erro ao consultar rota: {response.StatusCode}");
                throw new Exception("Erro ao calcular a rota");
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            try
            {
                var distanciaMetros = doc.RootElement
            .GetProperty("routes")[0]
            .GetProperty("summary")
            .GetProperty("distance")
            .GetDouble();

                var tempoEstimado = doc.RootElement
                    .GetProperty("routes")[0]
                    .GetProperty("summary")
                    .GetProperty("duration")
                    .GetDouble();

                var instrucoes = new List<string>();

                foreach (var step in doc.RootElement.GetProperty("routes")[0].GetProperty("segments")[0].GetProperty("steps").EnumerateArray())
                {
                    var instruction = step.GetProperty("instruction").GetString();
                    instrucoes.Add(instruction);
                }

                return distanciaMetros / 1000;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar a resposta: " + ex.Message);
                throw;
            }
        }


        private async Task<(double Latitude, double Longitude)> ObterCoordenadas(string endereco)
        {
            var encoded = Uri.EscapeDataString(endereco);
            var response = await _httpClient.GetAsync($"https://api.openrouteservice.org/geocode/search?api_key={_apiKey}&text={encoded}");

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var coords = doc.RootElement
            .GetProperty("features")[0]
            .GetProperty("geometry")
            .GetProperty("coordinates");

            double longitude = coords[0].GetDouble();
            double latitude = coords[1].GetDouble();

            return (latitude, longitude);
        }
    }
}
