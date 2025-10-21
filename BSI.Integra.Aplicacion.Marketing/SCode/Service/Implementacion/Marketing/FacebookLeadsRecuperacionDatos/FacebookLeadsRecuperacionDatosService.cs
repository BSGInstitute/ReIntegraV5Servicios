using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.FacebookLeadsRecuperacionDatos;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.FacebookLeadsRecuperacionDatos;
namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.FacebookLeadsRecuperacionDatos
{



    public class FacebookLeadsRecuperacionDatosService : IFacebookLeadsRecuperacionDatosService
    {
        private readonly HttpClient _httpClient;


        private const string FacebookAccessToken = "EAB7E1KH121kBOzQctQKIdoU3YhPHHA1EScQfuyGCLnCkliE8zXfq6ZCPvVZBiJV5BK4SZAxKAxosROF7d6SXoR1ZCIuZBgqGa5AGI8oZBdeSZCHlAMmvWIcH2QE9vnHdZBE7HHn5Ha42xKN4QTHNi1PnooMKbttHwbWECVrOfPjQ5Pha9f2ZADRMSZCNGikEtf1Tgs";
        public FacebookLeadsRecuperacionDatosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FacebookLeadsRecuperacionDatosResponseDTO> ObtenerPorIdAsync(string idLead)
        {
            var dto = new FacebookLeadsRecuperacionDatosResponseDTO
            {
                LeadId = idLead,
                Formulario = new FacebookFormularioDTO(),
                Campania = new FacebookCampaniaDTO(),
                ConjuntoAnuncio = new FacebookConjuntoAnuncioDTO(),
                Anuncio = new FacebookAnuncioDTO()
            };

            // Obtener datos del lead
            var leadUrl = $"https://graph.facebook.com/v23.0/{idLead}?fields=id,created_time,field_data,ad_id,adset_id,campaign_id,form_id&access_token={FacebookAccessToken}";
            var leadResponse = await _httpClient.GetStringAsync(leadUrl);
            var leadJson = JObject.Parse(leadResponse);

            //dto.FechaRegistro = DateTime.Parse(leadJson.Value<string>("created_time"));
            dto.FechaRegistro = leadJson.Value<DateTime?>("created_time");




            // ✅ Manejo correcto del campo "field_data"
            if (leadJson.TryGetValue("field_data", out var fieldToken) && fieldToken is JArray fieldDataArray)
            {
                foreach (var field in fieldDataArray)
                {
                    var name = field["name"]?.ToString() ?? "";
                    var value = field["values"]?.First?.ToString() ?? "";

                    switch (name.ToLower())
                    {
                        case "full_name": dto.Formulario.Nombre = value; break;
                        case "email": dto.Formulario.Correo = value; break;
                        case "phone_number": dto.Formulario.Movil = value; break;
                        case "ciudad":
                        case "city": dto.Formulario.Ciudad = value; break;
                        case "pais": dto.Formulario.Pais = value; break;
                        case "área_de_formación": dto.Formulario.AreaFormacion = value; break;
                        case "área_de_trabajo": dto.Formulario.AreaTrabajo = value; break;
                        case "cargo": dto.Formulario.Cargo = value; break;
                        case "industria": dto.Formulario.Industria = value; break;
                    }
                }
            }

            // Campaña
            var campaignId = leadJson.Value<string>("campaign_id") ?? "";
            if (!string.IsNullOrEmpty(campaignId))
            {
                var campUrl = $"https://graph.facebook.com/v23.0/{campaignId}?fields=name,status,objective,created_time&access_token={FacebookAccessToken}";
                var campJson = JObject.Parse(await _httpClient.GetStringAsync(campUrl));
                dto.Campania.Nombre = campJson.Value<string>("name") ?? "";
                dto.Campania.Estado = campJson.Value<string>("status") ?? "";
                dto.Campania.Objetivo = campJson.Value<string>("objective") ?? "";
            }

            // Conjunto de anuncios
            var adsetId = leadJson.Value<string>("adset_id") ?? "";
            if (!string.IsNullOrEmpty(adsetId))
            {
                var adsetUrl = $"https://graph.facebook.com/v23.0/{adsetId}?fields=name,status&access_token={FacebookAccessToken}";
                var adsetJson = JObject.Parse(await _httpClient.GetStringAsync(adsetUrl));
                dto.ConjuntoAnuncio.Nombre = adsetJson.Value<string>("name") ?? "";
                dto.ConjuntoAnuncio.Estado = adsetJson.Value<string>("status") ?? "";
            }

            // Anuncio
            var adId = leadJson.Value<string>("ad_id") ?? "";
            if (!string.IsNullOrEmpty(adId))
            {
                var adUrl = $"https://graph.facebook.com/v23.0/{adId}?fields=name,status&access_token={FacebookAccessToken}";
                var adJson = JObject.Parse(await _httpClient.GetStringAsync(adUrl));
                dto.Anuncio.Nombre = adJson.Value<string>("name") ?? "";
                dto.Anuncio.Estado = adJson.Value<string>("status") ?? "";
            }

            return dto;
        }
    }
}
