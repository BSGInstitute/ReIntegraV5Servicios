using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: TransicionFaseController
    /// Autor: Jose Vega
    /// Fecha: 12/09/2025
    /// <summary>
    /// Gestión de Transiciones entre Fases de Oportunidad
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TransicionFaseOportunidadController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public TransicionFaseOportunidadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica en la tabla TransicionFaseOportunidad.
        /// </summary>
        /// <param name="TransicionCalificacionFaseCreateDTO">Datos necesarios para la inserción de la transición de fase de oportunidad.</param>
        /// <returns>Entidad: TransicionFaseOportunidad</returns>
        /*[Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult Insertar([FromBody] TransicionFaseOportunidadEntradaDTO TransicionFaseOportunidadEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var transicionCalificacionFaseService = new TransicionFaseOportunidadService(_unitOfWork);
                var transicionCalificacionFase = new TransicionFaseOportunidad();
                transicionCalificacionFase.IdFaseOportunidadOrigen = TransicionFaseOportunidadEntradaDTO.IdFaseOportunidadOrigen;
                transicionCalificacionFase.IdFaseOportunidadDestino = TransicionFaseOportunidadEntradaDTO.IdFaseOportunidadDestino;
                transicionCalificacionFase.Estado = true;
                transicionCalificacionFase.UsuarioCreacion = TransicionFaseOportunidadEntradaDTO.Usuario;
                transicionCalificacionFase.UsuarioModificacion = TransicionFaseOportunidadEntradaDTO.Usuario;
                transicionCalificacionFase.FechaCreacion = DateTime.Now;
                transicionCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = transicionCalificacionFaseService.Add(transicionCalificacionFase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción en la tabla TransicionFaseOportunidad.
        /// </summary>
        /// <param name="transicionFaseOportunidadEntradaDTO">Datos necesarios para la inserción de la transición de fase de oportunidad.</param>
        /// <returns>Entidad: TransicionFaseOportunidad</returns>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] TransicionFaseOportunidadDTO transicionFaseOportunidadEntradaDTO)
        {
            if (transicionFaseOportunidadEntradaDTO == null)
            {
                return BadRequest("Payload de transición inválido.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var transicionService = new TransicionFaseOportunidadService(_unitOfWork);
                await transicionService.InsertTransicionAsync(transicionFaseOportunidadEntradaDTO);

                return Ok("Transición insertada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar la transición: {ex.Message}");
            }
        }

        /// Tipo Función: put
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una modificación en la tabla TransicionFaseOportunidad.
        /// </summary>
        /// <param name="TransicionFaseOportunidadDTO">Datos necesarios para la modificación de la transición de fase de oportunidad.</param>
        /// <returns>Entidad: TransicionFaseOportunidad</returns>
        [Route("[action]")]
        [Authorize]
        [HttpPut]

        public async Task<IActionResult> Actualizar([FromBody] TransicionFaseOportunidadDTO dto)
        {
            if (dto == null)
                return BadRequest("Payload de actualización inválido.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var service = new TransicionFaseOportunidadService(_unitOfWork);
                await service.UpdateTransicionAsync(dto);
                return Ok("Transición actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la transición: {ex.Message}");
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica en la tabla TransicionFaseOportunidad.
        /// </summary>
        /// <param name="TransicionCalificacionFaseCreateDTO">Datos necesarios para la inserción de la transición de fase de oportunidad.</param>
        /// <returns>Entidad: TransicionFaseOportunidad</returns>
        /*[HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] TransicionFaseOportunidadEntradaDTO TransicionFaseOportunidadEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var transicionCalificacionFaseService = new TransicionFaseOportunidadService(_unitOfWork);
                var transicionCalificacionFase = new TransicionFaseOportunidad();
                transicionCalificacionFase = transicionCalificacionFaseService.ObtenerPorId(TransicionFaseOportunidadEntradaDTO.Id);
                transicionCalificacionFase.IdFaseOportunidadOrigen = TransicionFaseOportunidadEntradaDTO.IdFaseOportunidadOrigen;
                transicionCalificacionFase.IdFaseOportunidadDestino = TransicionFaseOportunidadEntradaDTO.IdFaseOportunidadDestino;
                transicionCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = transicionCalificacionFaseService.Update(transicionCalificacionFase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        /// Tipo Función: DELETE
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminación lógica en la tabla TransicionFaseOportunidad.
        /// </summary>
        /// <param name="id">Id de la transición de fase de oportunidad a eliminar.</param>
        /// <param name="usuario">Usuario que ejecuta la eliminación.</param>
        /// <returns>bool</returns>
        [Route("[action]/{id}")]
        [Authorize]
        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var servicio = new ActividadCabeceraService(_unitOfWork);
                var usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();

                var transicionCalificacionFaseService = new TransicionFaseOportunidadService(_unitOfWork);
                var resultado = transicionCalificacionFaseService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de TransicionFaseOportunidad y sus detalles.
        /// </summary>
        /// <returns>List<TransicionFaseOportunidadDTO></returns>
        [Route("[action]")]
        [Authorize]
        [HttpGet]
        public IActionResult Obtener()
        {
            try
            {
                var transicionFaseOportunidadService = new TransicionFaseOportunidadService(_unitOfWork);
                var resultado = transicionFaseOportunidadService.Obtener();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la información de una TransiciónFaseOportunidad por Id.
        /// </summary>
        /// <param name="id">Id de la transición de fase de oportunidad.</param>
        /// <returns>Entidad: TransicionFaseOportunidadDTO</returns>
        [Route("[action]/{id}")]
        [Authorize]
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var transicionFaseOportunidadService = new TransicionFaseOportunidadService(_unitOfWork);
                var resultado = transicionFaseOportunidadService.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}