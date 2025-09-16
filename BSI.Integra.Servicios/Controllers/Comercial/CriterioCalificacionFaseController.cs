using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CriterioCalificacionFaseController
    /// Autor: José Vega
    /// Fecha: 20/09/2023
    /// <summary>
    /// Gestión de Criterios de Calificación para Transición de Fases
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CriterioCalificacionFaseController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CriterioCalificacionFaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST 
        /// Autor: Jose Vega
        /// Fecha: 20/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo criterio de calificación de fase con sus lineamientos.
        /// </summary>
        /// <param name="criterioCalificacionFaseCreateDTO">Datos necesarios para la inserción del criterio.</param>
        /// <returns>bool</returns>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult Insertar([FromBody] CriterioCalificacionFaseEntradaDTO criterioCalificacionFaseEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criterioCalificacionFaseService = new CriterioCalificacionFaseService(_unitOfWork);
                var criterioCalificacionFase = new CriterioCalificacionFase();
                criterioCalificacionFase.Orden = criterioCalificacionFaseEntradaDTO.Orden;
                criterioCalificacionFase.Nombre = criterioCalificacionFaseEntradaDTO.Nombre;
                criterioCalificacionFase.Descripcion = criterioCalificacionFaseEntradaDTO.Descripcion;
                criterioCalificacionFase.Estado = true;
                criterioCalificacionFase.UsuarioCreacion = criterioCalificacionFaseEntradaDTO.Usuario;
                criterioCalificacionFase.UsuarioModificacion = criterioCalificacionFaseEntradaDTO.Usuario;
                criterioCalificacionFase.FechaCreacion = DateTime.Now;
                criterioCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = criterioCalificacionFaseService.Add(criterioCalificacionFase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT 
        /// Autor: Jose Vega
        /// Fecha: 20/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un criterio de calificación de fase existente y sus lineamientos.
        /// </summary>
        /// <param name="criterioCalificacionFaseUpdateDTO">Datos necesarios para la actualización del criterio.</param>
        /// <returns>bool</returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] CriterioCalificacionFaseEntradaDTO criterioCalificacionFaseEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criterioCalificacionFaseService = new CriterioCalificacionFaseService(_unitOfWork);
                var criterioCalificacionFase = new CriterioCalificacionFase();
                criterioCalificacionFase = criterioCalificacionFaseService.ObtenerPorId(criterioCalificacionFaseEntradaDTO.Id);
                criterioCalificacionFase.Orden = criterioCalificacionFaseEntradaDTO.Orden;
                criterioCalificacionFase.Nombre = criterioCalificacionFaseEntradaDTO.Nombre;
                criterioCalificacionFase.Descripcion = criterioCalificacionFaseEntradaDTO.Descripcion;
                criterioCalificacionFase.UsuarioModificacion = criterioCalificacionFaseEntradaDTO.Usuario;
                criterioCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = criterioCalificacionFaseService.Update(criterioCalificacionFase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE 
        /// Autor: Jose Vega
        /// Fecha: 20/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminación lógica en la tabla T_CriterioCalificacionFaseOportunidad y sus tablas detalle.
        /// </summary>
        /// <param name="id">Id del criterio de calificación de fase a eliminar.</param>
        /// <param name="usuario">Usuario que ejecuta la eliminación.</param>
        /// <returns>bool</returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpDelete]
        public ActionResult Eliminar(int id, string usuario)
        {
            try
            {

                var criterioCalificacionFaseService = new CriterioCalificacionFaseService(_unitOfWork);
                var resultado = criterioCalificacionFaseService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: Jose Vega
        /// Fecha: 20/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de todos los criterios de calificación de fase.
        /// </summary>
        /// <returns>List<CriterioCalificacionFaseDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult Obtener()
        {
            try
            {
                var criterioCalificacionFaseService = new CriterioCalificacionFaseService(_unitOfWork);
                var resultado = criterioCalificacionFaseService.Obtener();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: Jose Vega
        /// Fecha: 20/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de un criterio de calificación de fase por su ID.
        /// </summary>
        /// <param name="id">Id del criterio de calificación de fase.</param>
        /// <returns>CriterioCalificacionFaseDTO</returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var criterioCalificacionFaseService = new CriterioCalificacionFaseService(_unitOfWork);
                var resultado = criterioCalificacionFaseService.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}