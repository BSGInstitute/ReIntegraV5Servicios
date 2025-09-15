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
    public class TransicionFaseController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private ITransicionCalificacionFaseService _transicionCalificacionFaseService;

        public TransicionFaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _transicionCalificacionFaseService = new TransicionCalificacionFaseService(_unitOfWork);
        }

        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult Insertar([FromBody] TransicionCalificacionFaseCreateDTO TransicionCalificacionFaseCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _transicionCalificacionFaseService.InsertarTransicionCalificacionFase(TransicionCalificacionFaseCreateDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult Insertar2([FromBody] TransicionCalificacionFaseCreateDTO TransicionCalificacionFaseCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var transicionCalificacionFaseService = new TransicionCalificacionFaseService(_unitOfWork);
                var transicionCalificacionFase = new TransicionCalificacionFase();
                transicionCalificacionFase.IdFaseOportunidadOrigen = TransicionCalificacionFaseCreateDTO.IdFaseOportunidadOrigen;
                transicionCalificacionFase.IdFaseOportunidadDestino = TransicionCalificacionFaseCreateDTO.IdFaseOportunidadDestino;
                transicionCalificacionFase.Estado = true;
                transicionCalificacionFase.UsuarioCreacion = TransicionCalificacionFaseCreateDTO.Usuario;
                transicionCalificacionFase.UsuarioModificacion = TransicionCalificacionFaseCreateDTO.Usuario;
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

        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] TransicionCalificacionFaseUpdateDTO transicionCalificacionFaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var transicionCalificacionFaseService = new TransicionCalificacionFaseService(_unitOfWork);
                var transicionCalificacionFase = new TransicionCalificacionFase();
                transicionCalificacionFase = transicionCalificacionFaseService.ObtenerTransicionCalificacionFasePorId(transicionCalificacionFaseDTO.Id);
                transicionCalificacionFase.IdFaseOportunidadOrigen = transicionCalificacionFaseDTO.IdFaseOportunidadOrigen;
                transicionCalificacionFase.IdFaseOportunidadDestino = transicionCalificacionFaseDTO.IdFaseOportunidadDestino;
                transicionCalificacionFase.UsuarioModificacion = transicionCalificacionFaseDTO.Usuario;
                transicionCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = transicionCalificacionFaseService.ActualizarTransicionCalificacionFase(transicionCalificacionFase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: [Su Nombre]
        /// Fecha: [Fecha Actual]
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica a la tabla TransicionCalificacionFase y sus tablas detalles
        /// </summary>
        /// <returns> bool </returns>  
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var transicionCalificacionFaseService = new TransicionCalificacionFaseService(_unitOfWork);
                var resultado = transicionCalificacionFaseService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET 
        /// Autor: [Su Nombre]
        /// Fecha: [Fecha Actual]
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de TransicionCalificacionFase y su detalles  
        /// </summary> 
        /// <returns> List<TransicionCalificacionFaseDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = new { transicionesFase = _transicionCalificacionFaseService.ObtenerTransicionesCalificacionFase() };
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: [Su Nombre]
        /// Fecha: [Fecha Actual]
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de combos para el módulo de Transición de Fases  
        /// </summary> 
        /// <returns> Dictionary<string, object> </returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerFasesOportunidad()
        {
            try
            {
                var resultado = await _transicionCalificacionFaseService.ObtenerFasesOportunidad();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: [Su Nombre]
        /// Fecha: [Fecha Actual]
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la información de una Transición de Fase por Id
        /// </summary> 
        /// <returns> TransicionCalificacionFaseDTO </returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var resultado = _transicionCalificacionFaseService.ObtenerTransicionCalificacionFasePorId(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: [Su Nombre]
        /// Fecha: [Fecha Actual]
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de Transiciones de Fases
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombo()
        {
            try
            {
                var resultado = _transicionCalificacionFaseService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}