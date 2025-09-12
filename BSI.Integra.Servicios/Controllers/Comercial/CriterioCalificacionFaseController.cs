using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
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
        private ICriterioCalificacionFaseService _criterioCalificacionFaseService;

        public CriterioCalificacionFaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _criterioCalificacionFaseService = new CriterioCalificacionFaseService(_unitOfWork);
        }

        /// Tipo Función: POST 
        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo criterio de calificación de fase con sus lineamientos  
        /// </summary> 
        /// <returns> bool </returns>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult Insertar([FromBody] CriterioCalificacionFaseCreateDTO criterioCalificacionFaseCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criterioCalificacionFaseService = new CriterioCalificacionFaseService(_unitOfWork);
                var criterioCalificacionFase = new CriterioCalificacionFase();
                criterioCalificacionFase.Orden = criterioCalificacionFaseCreateDTO.Orden;
                criterioCalificacionFase.Nombre = criterioCalificacionFaseCreateDTO.Nombre;
                criterioCalificacionFase.Descripcion = criterioCalificacionFaseCreateDTO.Descripcion;
                criterioCalificacionFase.Estado = true;
                criterioCalificacionFase.UsuarioCreacion = criterioCalificacionFaseCreateDTO.Usuario;
                criterioCalificacionFase.UsuarioModificacion = criterioCalificacionFaseCreateDTO.Usuario;
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
        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un criterio de calificación de fase existente con sus lineamientos  
        /// </summary> 
        /// <returns> bool </returns>
        /*  [Route("[action]")]
        [Authorize]
        [HttpPut]
        public IActionResult Actualizar([FromBody] CriterioCalificacionFaseDTO criterioCalificacionFaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioCalificacionFaseService.ActualizarCriterio(criterioCalificacionFaseDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] CriterioCalificacionFaseUpdateDTO criterioCalificacionFaseUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var criterioCalificacionFaseService = new CriterioCalificacionFaseService(_unitOfWork);
                var criterioCalificacionFase = new CriterioCalificacionFase();
                criterioCalificacionFase = criterioCalificacionFaseService.ObtenerCriterioCalificacionFasePorId(criterioCalificacionFaseUpdateDTO.Id);
                criterioCalificacionFase.Orden = criterioCalificacionFaseUpdateDTO.Orden;
                criterioCalificacionFase.Nombre = criterioCalificacionFaseUpdateDTO.Nombre;
                criterioCalificacionFase.Descripcion = criterioCalificacionFaseUpdateDTO.Descripcion;
                criterioCalificacionFase.UsuarioModificacion = criterioCalificacionFaseUpdateDTO.Usuario;
                criterioCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = criterioCalificacionFaseService.ActualizarCriterio(criterioCalificacionFase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: DELETE 
        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminación lógica a la tabla T_CriterioCalificacionFaseOportunidad y sus tablas detalles  
        /// </summary> 
        /// <returns> bool </returns>
        [Route("[action]/{id}/{usuario}")]
        [HttpDelete]
        [Authorize]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioCalificacionFaseService.EliminarCriterio(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de todos los criterios de calificación de fase  
        /// </summary> 
        /// <returns> List<CriterioCalificacionFaseDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = new { criteriosCalificacionFase = _criterioCalificacionFaseService.ObtenerCriteriosCalificacionFase() };
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los criterios de calificación asociados a una transición específica  
        /// </summary> 
        /// <returns> List<CriterioCalificacionFaseDTO> </returns>
        [Route("[action]/{idTransicion}")]
        [HttpGet]
        public IActionResult ObtenerPorTransicion(int idTransicion)
        {
            try
            {
                var resultado = new { criteriosCalificacionFase = _criterioCalificacionFaseService.ObtenerCriteriosPorTransicion(idTransicion) };
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos necesarios para los formularios del módulo  
        /// </summary> 
        /// <returns> Dictionary<string, List<ComboDTO>> </returns>
       /* [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCriticidad()
        {
            try
            {
                var resultado = await _criterioCalificacionFaseService.ObtenerCriticidad();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        /// Tipo Función: GET 
        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de un criterio de calificación de fase por su ID  
        /// </summary> 
        /// <returns> CriterioCalificacionFaseDTO </returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var resultado = _criterioCalificacionFaseService.ObtenerCriterioCalificacionFasePorId(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un combo con todos los criterios de calificación de fase  
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ListaCriterios()
        {
            try
            {
                var resultado = _criterioCalificacionFaseService.ListaCriterios();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}