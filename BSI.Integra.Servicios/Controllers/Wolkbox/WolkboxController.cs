using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace BSI.Integra.Servicios.Controllers.Wolkbox
{
    /// Controlador: WolkboxController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de Wolkbox
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class WolkboxController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public WolkboxController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 13/05/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns> entidad </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Marcar()
        {
            try
            {
                var wolkvox_server = "0057";
                var date_ini = "20240501000000";
                var date_end = "20240510235959";
                var url = $"https://wv{wolkvox_server}.wolkvox.com/api/v2/reports_manager.php?api=cdr_1&date_ini={date_ini}&date_end={date_end}";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("ContentType", "application/json");
                    client.DefaultRequestHeaders.Add("wolkvox-token", "7b69645f6469737472697d2d3230323430353032313730393234");
                    client.DefaultRequestHeaders.Add("wolkvox_server", "0057");
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var resultado = JsonConvert.DeserializeObject<object>(responseContent);
                    }
                    else
                    {
                    }
                }
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
