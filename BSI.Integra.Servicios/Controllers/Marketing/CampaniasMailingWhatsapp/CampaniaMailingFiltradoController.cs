using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp;
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
    public class CampaniaMailingFiltradoController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CampaniaMailingFiltradoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para el filtrado de data necesaria para mailing
        /// </summary>
        /// <param name="datosFiltro">Entidad necesaria para el filtrado</param>
        /// <returns>Retorna objeto RespuestaGenerica </returns>
        [AllowAnonymous]
        [Route("mailing")]
        [HttpPost]
        public IActionResult FiltradoDeDatosParaMailing([FromBody] CampaniaMailingWhatsAppFiltradoDTO.CampaniaMailingFiltrado datosFiltro)
        {
            try
            {
                return Ok(new CampaniaMailingFiltradoService(unitOfWork).FiltradoDeDatosParaMailing(datosFiltro));
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
        /// Realiza una peticion para el Eliminado de data de mailing
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador uncio de campaniaGeneral</param>
        /// <returns>Retorna un dato boleano </returns>
        [Route("EliminarRegistrosPasados/mail/{IdcampaniaGeneral}")]
        [HttpDelete]
        public IActionResult EliminarRegistroPasado(int IdcampaniaGeneral)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                return Ok(new CampaniaMailingFiltradoService(unitOfWork).EliminacionLogicaDeFiltroMialing(IdcampaniaGeneral, _respuestaCorrecta.RegistroClaimToken.UserName));
            }catch(Exception ex)
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
        [Route("Envio/Mail/SendingBlue/{IdcampaniaGeneral}")]
        [HttpGet]
        public IActionResult EnvioDeMailsParaCorresoDeSendingBLue(int IdcampaniaGeneral)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                return Ok(new CampaniaMailingFiltradoService(unitOfWork).SendinMail(_respuestaCorrecta.RegistroClaimToken.UserName, IdcampaniaGeneral));
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
        /// Realiza una peticion para obtener los datos proesados por el filtro
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador de la campania general</param>
        /// <param name="Prioridad">Prioridad del detalle buscado</param>
        /// <returns>Retorna objeto RespuestaGenerica </returns>
        [Route("mailing/{IdcampaniaGeneral}/{Prioridad}")]
        [HttpGet]
        public IActionResult FiltradoDeDatosParaMailingObtenerData(int IdcampaniaGeneral, int Prioridad)
        {
            return Ok(new CampaniaMailingFiltradoService(unitOfWork).FiltradoDeDatosParaMailingObtenerData(IdcampaniaGeneral, Prioridad));
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener los datos proesados por el filtro y relacionados con el alumno
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador de la campania general</param>
        /// <param name="Prioridad">Prioridad del detalle buscado</param>
        /// <returns>Retorna objeto RespuestaGenerica </returns>
        [Route("mailing/data/{IdcampaniaGeneral}/{Prioridad}")]
        [HttpGet]
        public IActionResult FiltradoDeDatosParaMailingObtenerDataMailing(int IdcampaniaGeneral, int Prioridad)
        {
            return Ok(new CampaniaMailingFiltradoService(unitOfWork).FiltradoDeDatosParaMailingObtenerDataMailing(IdcampaniaGeneral, Prioridad));
        }
        /// Tipo Función: DELETE
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para eliminar lso datos relacionados a campania general detalle
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador de la campania general</param>
        /// <returns>Retorna objeto RespuestaGenerica </returns>
        [Route("mailing/delete/{IdcampaniaGeneral}")]
        [HttpDelete]
        public IActionResult EliminadoLogicoDeDatosDeFiltradoMailing(int IdcampaniaGeneral)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            return Ok(new CampaniaMailingFiltradoService(unitOfWork).EliminacionLogicaDeFiltroMialing(IdcampaniaGeneral, _respuestaCorrecta.RegistroClaimToken.UserName));
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza un apeticion para obtener los detalles de campnia general detalle por el id de campania general completa
        /// </summary>
        /// <param name="idCampaniaGenral">Identificador de la campania general</param>
        /// <returns>Retorna lista de campania general con relaciones</returns>
        [Route("ListaDetalle/{idCampaniaGenral}")]
        [HttpGet]
        public IActionResult ObtenerDetalleCampaniaGeneralPorIdDeCampaniaGeneral(int idCampaniaGenral)
        {
            return Ok(new CampaniaMailingFiltradoService(unitOfWork).ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerlaCompleta(idCampaniaGenral));
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 06/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera el url de la campania general
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador de la campania general</param>
        /// <returns>Retorna una variable boleana</returns>
        [Route("GenerarUrl/{IdcampaniaGeneral}")]
        [HttpGet]
        public IActionResult SendinMail(int IdcampaniaGeneral)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            return Ok(new CampaniaMailingFiltradoService(unitOfWork).SendinMail(_respuestaCorrecta.RegistroClaimToken.UserName, IdcampaniaGeneral));
        }
    }
}
