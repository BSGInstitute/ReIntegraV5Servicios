using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PeriodoMesProyeccionController
    /// Autor: MArgiory Ramirez
    /// Fecha: 08/03/2023
    /// <summary>
    /// Gestión de PeriodoMesProyeccion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PeriodoMesProyeccionController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PeriodoMesProyeccionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor:Margiory Ramirez
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        ///Inserta los fregristross  a la  tabal TipoDaato
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult InsertarPeriodoMesProyeccion([FromBody] List<PeriodoMesProyeccionDTO> entidad)
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new PeriodoMesProyeccionService(unitOfWork);
                    var respuesta = servicio.InsertarPeriodoMesProyeccion(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(respuesta);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }

        }
        /// Tipo Función: PUT
        /// Autor:Margiory Ramirez
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        ///Actuliza  los fregristros  a la  tabal 
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>

        [HttpPut("[Action]")]
        public IActionResult ActulizarPeriodoMesProyeccion([FromBody] PeriodoMesProyeccionDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new PeriodoMesProyeccionService(unitOfWork);
                    var respuesta = servicio.ActulizarPeriodoMesProyeccion(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(respuesta);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
        /// Tipo Función: GET   
        /// Autor:Margiory Ramirez
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        /// Obtniene todos los registros de la tabla
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>

        [HttpGet("[Action]")]
        public IActionResult ObtenerPeriodoMesProyeccion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PeriodoMesProyeccionService(unitOfWork);
                return Ok(servicio.ObtenerPeriodoMesProyeccion());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Margiory Ramirez.
        /// Fecha: 08/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PeriodoMesProyeccionService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez.
        /// Fecha: 08/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDato para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerPeriodoMesProyeccionCombo()
        {
            var servicio = new PeriodoMesProyeccionService(unitOfWork);
            return Ok(servicio.ObtenerPeriodoMesProyeccionCombo());
        }
    }
}









































