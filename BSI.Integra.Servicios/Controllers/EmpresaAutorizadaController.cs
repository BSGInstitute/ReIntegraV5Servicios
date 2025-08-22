using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: EmpresaAutorizadaController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de EmpresaAutorizada
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsVista")]
    public class EmpresaAutorizadaController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public EmpresaAutorizadaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] EmpresaAutorizadaDTO entidad)
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
                    var servicio = new EmpresaAutorizadaService(_unitOfWork);
                    var respuesta = servicio.Add(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] EmpresaAutorizadaDTO entidad)
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
                    var servicio = new EmpresaAutorizadaService(_unitOfWork);
                    var respuesta = servicio.Update(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
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
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
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
                    var servicio = new EmpresaAutorizadaService(_unitOfWork);
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
        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{listadoIds}/{usuario}")]
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
                    var servicio = new EmpresaAutorizadaService(_unitOfWork);
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_EmpresaAutorizada
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _unitOfWork.EmpresaAutorizadaRepository.Obtener();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_EmpresaAutorizada para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            IEmpresaAutorizadaService servicio = new EmpresaAutorizadaService(_unitOfWork);
            return Ok(servicio.ObtenerCombo());
        }
        [HttpGet("[action]/{idCiudad}")]
        public IActionResult ObtenerComboPorCiudad(int idCiudad)
        {
            return Ok(_unitOfWork.EmpresaAutorizadaRepository.ObtenerComboPorCiudad(idCiudad));
        }
    }
}
