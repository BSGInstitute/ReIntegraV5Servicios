using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp;
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
    /// Controlador: CampaniaGeneralDetalleController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 25/11/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CampaniaGeneralDetalleController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CampaniaGeneralDetalleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para insertar una entidades
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna la entidad insertada </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CampaniaGeneralDetalleEnvioDTO entidad)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleService(unitOfWork);
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
        /// Realiza una peticion para insertar una lsita de entidades
        /// </summary>
        /// <param name="listado">Entidades a insertar</param>
        /// <returns>Retorna las listas insertadas </returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<CampaniaGeneralDetalle> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleService(unitOfWork);
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
        /// Realiza una peticion para actualziar una entidad
        /// </summary>
        /// <param name="entidad">Entidada a actualizar</param>
        /// <returns>Retorna el objeto actualizado </returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CampaniaGeneralDetalleEnvioDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CampaniaGeneralDetalleService(unitOfWork);
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
        /// Realiza una peticion para eliminar una entidad por id
        /// </summary>
        /// <param name="id">Identificador de campania general</param>
        /// <returns>Retorna un valor boleano </returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var servicio = new CampaniaGeneralDetalleService(unitOfWork);
                var respuesta = servicio.Delete(id, _respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// Obtiene todos los registros guardados en T_CampaniaGeneralDetalle
        /// </summary>
        /// <returns> List<CampaniaGeneralDetalleDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCampaniaGeneralDetalle()
        {
            try
            {
                var servicio = new CampaniaGeneralDetalleService(unitOfWork);
                return Ok(servicio.ObtenerCampaniaGeneralDetalle());
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
        /// Realiza un apeticion para obtener el detalle general de una campania mas el listado de areas sub areas y programas
        /// </summary>
        /// <param name="idCamapniaGeneral">Identificador de campania general</param>
        /// <param name="idCampaniaGeneralDetalle">Identificador campania general detalle</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpGet("Obtener/{idCampaniaGeneralDetalle}/{idCamapniaGeneral}")]
        public IActionResult ObtenerCampaniaGeneralMasAreaSubAreaYprogramaById(int idCampaniaGeneralDetalle, int idCamapniaGeneral)
        {
            try
            {
                var servicio = new CampaniaMailingFiltradoService(unitOfWork);
                return Ok(servicio.ObtenerCampaniaGeneralMasAreaSubAreaYprogramaById(idCampaniaGeneralDetalle,idCamapniaGeneral));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


