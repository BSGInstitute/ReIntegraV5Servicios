using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: EstadoProyeccionFurController
    /// Autor: Griselberto Huaman
    /// Fecha: 14/03/2023
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class EstadoProyeccionFurController : Controller
    {
        private IUnitOfWork unitOfWork;
        public EstadoProyeccionFurController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        ///Inserta los fregristross  a la  tabal TipoDaato
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult InsertarEstadoProyeccionFur([FromBody] EstadoProyeccionFurDTO data)
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
                    data.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new EstadoProyeccionFurService(unitOfWork);
                    var respuesta = servicio.Add(data);
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
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        ///Actualiza  los regristros  a la  tabla 
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>

        [HttpPut("[Action]")]
        public IActionResult ActualizarEstadoProyeccionFur([FromBody] EstadoProyeccionFurDTO data)
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
                    data.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new EstadoProyeccionFurService(unitOfWork);
                    var respuesta = servicio.Update(data);
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

        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 08/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la data a eliminar</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarEstadoProyeccionFur/{id}")]
        public IActionResult EliminarEstadoProyeccionFur(int id)
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
                    var servicio = new EstadoProyeccionFurService(unitOfWork);
                    var respuesta = servicio.Delete(id, _respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// Autor:Griselberto Huaman
        /// Fecha:  08/03/2023
        /// Versión: 1.                 
        /// <summary>
        ///Obtiene el Combo para Estados de Proyeccion
        /// </summary>
        /// <returns> bool </returns>
        [Authorize]
        [HttpGet("ObtenerComboEstadoProyeccionFur")]
        public IActionResult ObtenerComboEstadoProyeccionFur()
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new EstadoProyeccionFurService(unitOfWork);
                    var respuesta = servicio.ObtenerComboEstadoProyeccionFur();
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


    }
}









































