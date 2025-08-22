using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CampaniaGeneralDetalleSubAreaController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 25/11/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CampaniaGeneralDetalleSubAreaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CampaniaGeneralDetalleSubAreaController(IUnitOfWork unitOfWork)
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
        public IActionResult Insertar([FromBody] CampaniaGeneralDetalleSubArea entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleSubAreaService(unitOfWork);
                var respuesta = servicio.Add(entidad,_respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// <returns>Retorna las entidades insertadas</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<CampaniaGeneralDetalleSubArea> listado)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleSubAreaService(unitOfWork);
                var respuesta = servicio.Add(listado, _respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// Realiza una peticion para Actualizar una entidade
        /// </summary>
        /// <param name="entidad">Entidad a actualizar</param>
        /// <returns>Retorna la entidad insertada</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CampaniaGeneralDetalleSubArea entidad)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleSubAreaService(unitOfWork);
                var respuesta = servicio.Update(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// <param name="id">identificador unico de entidad a eliminar</param>
        /// <returns>Retorna un valor boleano</returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleSubAreaService(unitOfWork);
                var respuesta = servicio.Delete(id, _respuestaCorrecta.RegistroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}


