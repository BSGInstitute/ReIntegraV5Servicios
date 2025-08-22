using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.Sendingblue;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace BSI.Integra.Servicios.Controllers.Marketing.Sendingblue
{
    /// Controlador: ReporteCampaniasController
    /// Autor: Rodrigo Montesinos.
    /// Fecha: 03/25/2023
    /// Ruta: api/marketing/sendinblue/ReporteCampanias
    /// <summary>
    /// Se encargara de gestionar lso reportes de campanias de sendinblue y tambien de almacenado de data por webhook
    /// </summary>
    [Authorize]
    [Route("api/marketing/sendinblue/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteCampaniasController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ReporteCampaniasController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos
        /// Fecha: 03/31/2023
        /// Descripcion: Esta fucion es un web hook que obtiene la data de sendinblue
        /// Version :1.0
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public async void ObtenerDataParaReporte()
        {
            try {
                var payload = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                new SendinblueReporteService(unitOfWork).AgregarDatosDeWebhook(JsonConvert.DeserializeObject<Dictionary<string,object>>(payload));
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos
        /// Fecha: 03/31/2023
        /// Descripcion: Esta funcion retorna lso datos necesarios apra el reporte
        /// Version :1.0
        /// </summary>
        [Route("Obtener")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ObtenerReporte(SendinblueReporteParametrosDTO parametros)
        {
            try
            {
                return Ok(new SendinblueReporteService(unitOfWork).ObtenerReporteDeSendinBlue(parametros));
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
       
    }
}
