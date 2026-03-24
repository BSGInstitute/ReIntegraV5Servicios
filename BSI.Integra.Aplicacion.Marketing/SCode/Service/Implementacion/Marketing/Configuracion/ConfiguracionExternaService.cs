using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Configuracion;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Configuracion;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Configuracion
{
    /// Autor: Miguel Valdivia
    /// Fecha: 18/03/2026
    /// Version: 1.0
    /// <summary>
    /// Obtiene los datos del esquema via repositorio, construye el JSON de interaccion
    /// y lo envia via PATCH a la API externa del asistente de marketing WhatsApp.
    /// </summary>
    public class ConfiguracionExternaService : IConfiguracionExternaService
    {
        private const string ApiBaseUrl = "http://ia-asistente-marketing-whatsapp-api.bsginstitute.com/testing/";

        private readonly IConfiguracionExternaRepository _configuracionExternaRepository;

        public ConfiguracionExternaService(IConfiguracionExternaRepository configuracionExternaRepository)
        {
            _configuracionExternaRepository = configuracionExternaRepository;
        }

        public async Task<ConfiguracionApiResponseDTO> SincronizarEsquemaInteraccionAsync(int idChatbotEsquema)
        {
            var patch = await _configuracionExternaRepository.ObtenerEsquemaInteraccionAsync(idChatbotEsquema);

            using var client  = new HttpClient { BaseAddress = new Uri(ApiBaseUrl) };
            var json    = JsonConvert.SerializeObject(patch);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Patch, "api/configuracion/interaccion/")
            {
                Content = content
            };

            var httpResponse = await client.SendAsync(request);
            var responseBody = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception($"Error {(int)httpResponse.StatusCode} desde API externa: {responseBody}");

            return JsonConvert.DeserializeObject<ConfiguracionApiResponseDTO>(responseBody);
        }
    }
}
