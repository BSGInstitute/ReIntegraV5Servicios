using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ChatController
    /// Autor: Jonathan Caipo
    /// Fecha: 17/10/2022
    /// <summary>
    /// Gestión del chat
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ChatBotController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ChatBotController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Margiory ramirez
        /// Fecha: 17/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos filtrados de ContactoOportunidad
        /// </summary>
        /// <returns></returns>



        [HttpPost("[action]")]
        
        public IActionResult ReporteBot( [FromBody]FiltroBotDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IFacebookFormularioLeadgenService servi = new FacebookFormularioLeadgenService(unitOfWork);
                var respuesta = servi.Reportebot(filtro);
                return Ok(respuesta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
