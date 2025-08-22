using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueContactosDTO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CampaniaGeneralController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 25/11/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CampaniaGeneralController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CampaniaGeneralController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CampaniaGeneralEnvioDTO entidad)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new CampaniaGeneralService(unitOfWork);
                var respuesta = servicio.Add(entidad, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="id">Identificador unico de campania general</param>
        /// <returns>retorna un objeto</returns>
        [HttpGet("obtener/ById/{id}")]
        public IActionResult ObtenerCamapaniaGeneralPorId(int id)
        {
            try
            {
                return Ok(new CampaniaGeneralService(unitOfWork).ObtenerPorId(id));
            }catch(Exception e)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="listado">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<CampaniaGeneral> listado)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CampaniaGeneralService(unitOfWork);
                var respuesta = servicio.Add(listado,_respuestaCorrecta.RegistroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a actualizar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CampaniaGeneralEnvioDTO entidad)
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CampaniaGeneralService(unitOfWork);
                var respuesta = servicio.Update(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion basica a la tabla
        /// </summary>
        /// <param name="id">identificador unico de entidad a eliminar</param>
        /// <returns>Retorna un valor boleano</returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralService(unitOfWork);
                var respuesta = servicio.Delete(id, _respuestaCorrecta.RegistroClaimToken.UserName) ;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 25/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CampaniaGeneral
        /// </summary>
        /// <returns> List<CampaniaGeneralDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCampaniaGeneral()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CampaniaGeneralService(unitOfWork);
                return Ok(servicio.ObtenerCampaniaGeneral());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 25/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CampaniaGeneral para whatasapp
        /// </summary>
        /// <returns> List<CampaniaGeneralDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCampaniaGeneralSoloDatosParaWhatsApp()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CampaniaGeneralService(unitOfWork);
                return Ok(servicio.ObtenerCampaniaGeneralSoloWhatsApp());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 27/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros Para la cofiguracion de envio de WhatsApp
        /// </summary>
        /// <returns> List<ConfiguracionDeEnvioParaWhatsAppMasPlantilla> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerConfiguracionDeEnvioParaWhatsAppMasPlantilla()
        {
            try
            {
                var servicio = new CampaniaGeneralService(unitOfWork);
                return Ok(servicio.ObtenerConfiguracionDeEnvioParaWhatsAppMasPlantilla());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="CampaniaGeneralDTO">Entidad a insertar</param>
        /// <returns>Retorna 200 o 400 y mensaje de error </returns>
        [Route("[Action]/")]
        [HttpPost]
        public ActionResult InsertarCampaniaGeneral([FromBody] CampaniaGeneralDTO CampaniaGeneralDTO)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralService(unitOfWork);
                servicio.InsertarOActualizarCampaniaGeneral(CampaniaGeneralDTO, _respuestaCorrecta.RegistroClaimToken.UserName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("ObtenerAlumnosParaSubirALista")]
        [HttpPost]
        public ActionResult ObtenerAlumnosParaSubirALista([FromBody] ListaIdsDtos lista)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralService(unitOfWork);
                var respuesta = servicio.ObtenerAlumnosParaSubirALista(lista);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("agregarListaAlumnos")]
        [HttpPost]
        public ActionResult agregarListaAlumnos([FromBody] agregarListaContactosDTO lista)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralService(unitOfWork);
                var respuesta = servicio.agregarListaAlumnos(lista);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

    }
}
