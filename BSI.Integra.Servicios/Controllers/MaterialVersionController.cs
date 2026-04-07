using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MaterialVersionController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión de MaterialVersion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MaterialVersionController : ControllerBase
    {
        private IMaterialVersionService _materialVersionService;
        public MaterialVersionController(IMaterialVersionService materialVersionService)
        {
            this._materialVersionService = materialVersionService;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] MaterialVersionDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _materialVersionService.Insertar(entidad, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<MaterialVersion> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _materialVersionService.InsertarLista(listado, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] MaterialVersionDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _materialVersionService.Actualizar(entidad, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<MaterialVersion> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _materialVersionService.ActualizarLista(listado, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _materialVersionService.Eliminar(id, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("[Action]/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _materialVersionService.EliminarLista(listadoIds, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_MaterialVersion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _materialVersionService.Obtener();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Carlos Crispin Riquelme.
        /// Fecha: 13/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la grabaciones de celulares corporativos
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerGrabacionesCelularCorporativo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _materialVersionService.ObtenerGrabacionesCelularCorporativo();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Carlos Crispin Riquelme.
        /// Fecha: 13/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarGrabacionesCelularCorporativo([FromForm] GrabacionCelularCorporativoDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);

                var resultado = _materialVersionService.InsertarGrabacionesCelularCorporativo(entidad, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin Riquelme.
        /// Fecha: 06/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarGrabacionesReporteLlamadasPorDia([FromForm] GrabacionCelularLlamadasPorDiaDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    try
                    {
                        var resultado = _materialVersionService.GenerarNuevaLlamadaActividad(entidad, _respuestaCorrecta.RegistroClaimToken.UserName);
                        return Ok();
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                    throw new UnauthorizedAccessException("Usted no tiene acceso");

                //var resultado = _materialVersionService.InsertarGrabacionesCelularCorporativo(entidad, registroClaimToken.UserName);
                //return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
