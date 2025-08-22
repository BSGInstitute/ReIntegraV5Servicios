using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FurController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión de Fur
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FurController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public FurController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [HttpDelete("[action]/{id}/{usuario}")]

        public IActionResult Eliminar(int id, string usuario)
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
                    usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new FurService(unitOfWork);
                    var respuesta = servicio.Delete(id, usuario);
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [HttpDelete("[action]/{usuario}")]

        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
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
                    usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new FurService(unitOfWork);
                    var respuesta = servicio.Delete(listadoIds, usuario);
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
        /// Autor: Griselberto Huaman
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Fur
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult ObtenerDatosFur()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerDatosFur());
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
        /// Autor: Griselberto Huaman
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Fur
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDatosFurAutocomplete/{valor}")]
        public IActionResult ObtenerDatosFurAutocomplete(string valor)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerDatosFurAutocomplete(valor));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Furs para RegistroEgresoCaja
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        [Authorize]

        public IActionResult ObtenerFursREC()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerFursREC());
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
        /// Autor: Griselberto Huaman
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Furs para RegistroEgresoCaja
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]/{codigo}")]
        public IActionResult ObtenerDatosFurSolicitudEfectivo(string codigo)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerDatosFurSolicitudEfectivo(codigo));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Furs para RegistroEgresoCaja
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]/{codigo}/{IdCajaPorRendirCabecera}")]
        [Authorize]
        public IActionResult ObtenerDatosFurcajaEgreso(string codigo, int IdCajaPorRendirCabecera)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerDatosFurcajaEgreso(codigo, IdCajaPorRendirCabecera));
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
        /// Autor: Griselberto Huaman
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Furs para RegistroEgresoCaja
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idProgramaEspecifico}")]
        public ActionResult<IEnumerable<ProgramaEspecificoFURDTO>> ObtenerFurProgramaEspecifico(int idProgramaEspecifico)
        {
            IFurService servicio = new FurService(unitOfWork);
            return Ok(servicio.ObtenerFurProgramaEspecifico(idProgramaEspecifico, false));
        }
    }
}
