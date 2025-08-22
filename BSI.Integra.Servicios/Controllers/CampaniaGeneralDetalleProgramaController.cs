using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CampaniaGeneralDetalleProgramaController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 25/11/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CampaniaGeneralDetalleProgramaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CampaniaGeneralDetalleProgramaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para insertar una entidad
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna la entidad insertada</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CampaniaGeneralDetallePrograma entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleProgramaService(unitOfWork);
                var respuesta = servicio.Add(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
                return Ok(respuesta);
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
        /// Realiza una peticion para insertar una lista de entidades
        /// </summary>
        /// <param name="listado">Entidades a insertar</param>
        /// <returns>Retorna las listas insertadas </returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<CampaniaGeneralDetallePrograma> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleProgramaService(unitOfWork);
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
        /// Realiza una peticion para insertar una entidad
        /// </summary>
        /// <param name="entidad">Entidades a insertar</param>
        /// <returns>Retorna la entidad insertada </returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CampaniaGeneralDetallePrograma entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleProgramaService(unitOfWork);
                entidad.UsuarioModificacion = _respuestaCorrecta.RegistroClaimToken.UserName;
                var respuesta = servicio.Update(entidad);
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
        /// Realiza una peticion para eliminar una entidad
        /// </summary>
        /// <param name="id">Identificador De Id</param>
        /// <returns>Retorna las listas insertadas </returns>
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
                var servicio = new CampaniaGeneralDetalleProgramaService(unitOfWork);
                var respuesta = servicio.Delete(id,_respuestaCorrecta.RegistroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
