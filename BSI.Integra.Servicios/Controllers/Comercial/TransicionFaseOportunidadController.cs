using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
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
        [Route("[action]")]
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
        [HttpPut("[Action]")]
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
                transicionCalificacionFase.UsuarioModificacion = TransicionFaseOportunidadEntradaDTO.Usuario;
                transicionCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = transicionCalificacionFaseService.Update(transicionCalificacionFase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

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