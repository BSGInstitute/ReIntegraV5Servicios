using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Marketing.CampaniasMailingWhatsapp
{
    /// Controlador: CampaniaMailingFiltradoController
    /// Autor: Rodrigo Montesinos.
    /// Fecha: 05/12/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CampaniaWhatsappFiltradoController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CampaniaWhatsappFiltradoController( IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para el filtrado de data necesaria para whatsapp
        /// </summary>
        /// <param name="datosFiltro">Entidad necesaria para el filtrado</param>
        /// <returns>Retorna objeto RespuestaGenerica </returns>
        [AllowAnonymous]
        [Route("whatsapp")]
        [HttpPost]
        public async Task<IActionResult> FiltradoDeDatosParaWhatsapp([FromBody] CampaniaMailingWhatsAppFiltradoDTO.CampaniaWhatsAppFiltrado datosFiltro)
        {
            try
            {
                return Ok(new CampaniaWhatsAppFiltradoService(unitOfWork).FiltradoDeDatosParaWhatsapp(datosFiltro));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para el Eliminado de data de WhatsApp
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador uncio de campaniaGeneral</param>
        /// <returns>Retorna un dato boleano </returns>
        [Route("EliminarRegistrosPasados/WhatsApp/{IdcampaniaGeneral}")]
        [HttpDelete]
        public IActionResult EliminarRegistroPasado(int IdcampaniaGeneral)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                return Ok(new CampaniaWhatsAppFiltradoService(unitOfWork).EliminacionLogicaDeFiltroWhatsApp(IdcampaniaGeneral, _respuestaCorrecta.RegistroClaimToken.UserName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para el Envio de correos y generacion de urls
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador uncio de campaniaGeneral</param>
        /// <returns>Retorna un dato boleano </returns>
        [Route("Envio/WhatsApp/{IdcampaniaGeneral}")]
        [HttpGet]
        public IActionResult EnvioDeMailsParaCorresoDeSendingBLue(int IdcampaniaGeneral)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                return Ok(new CampaniaWhatsAppFiltradoService(unitOfWork).SendMail(_respuestaCorrecta.RegistroClaimToken.UserName, IdcampaniaGeneral));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener el filtrado de data whatsapp por prioridad
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador unico de campania general</param>
        /// <param name="Prioridad">prioridad buscada para el filtrado</param>
        /// <returns>Retorna objeto RespuestaGenerica </returns>
        [Route("whatsapp/{IdcampaniaGeneral}/{Prioridad}")]
        [HttpGet]
        public IActionResult FiltradoDeDatosParaWhatsappObtenerData(int IdcampaniaGeneral, int Prioridad)
        {
            return Ok(new CampaniaWhatsAppFiltradoService(unitOfWork).FiltradoDeDatosParaWhatsappObtenerData(IdcampaniaGeneral, Prioridad));
        }
    }
}
