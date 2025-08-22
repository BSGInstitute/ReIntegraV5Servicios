using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PreguntaFrecuenteController
    /// Autor: Margiory Ramirez.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PreguntaFrecuenteController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IPreguntaFrecuenteService _preguntaFrecuenteService;
        public PreguntaFrecuenteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _preguntaFrecuenteService = new PreguntaFrecuenteService(_unitOfWork);
        }
        /// Metodo HTTP: GET.
        /// Autor: Gilmer Qm.
        /// Fecha: 20/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las preguntas frecuentes y sus detalles
        /// </summary>
        /// <returns> List<PreguntaFrecuenteComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                var respuesta = _preguntaFrecuenteService.Obtener();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: GET.
        /// Autor: Gilmer Qm
        /// Fecha: 21/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combos para el modulo de (C) Preguntas frecuentes del aula virtual
        /// </summary> 
        /// <returns> PreguntaFrecuentaComboModuloDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosModulo()
        {
            try
            {
                var respuesta = _preguntaFrecuenteService.ObtenerCombosModulo();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: POST.
        /// Autor: Gilmer Qm.
        /// Fecha: 20/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las preguntas frecuentes y sus detalles por filtros
        /// </summary>
        /// <param name="filtro"> Filtros </param>
        /// <returns> List<PreguntaFrecuenteFiltroResultadoDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPorFiltro([FromBody] FiltroPreguntaFrecuenteDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _preguntaFrecuenteService.ObtenerPorFiltro(filtro);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: POST.
        /// Autor: Gilmer Qm.
        /// Fecha: 21/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la tabla y sus detalles
        /// </summary>
        /// <returns> nueva Pregunta Frecuente: PreguntaFrecuenteDTO  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar(PreguntaFrecuenteParametrosDTO preguntaFrecuenteParametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _preguntaFrecuenteService.Insertar(preguntaFrecuenteParametros, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: PUT.
        /// Autor: Gilmer Qm.
        /// Fecha: 21/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla y sus detalles
        /// </summary>
        /// <param name="preguntaFrecuenteParametros"> parametros entrada </param> 
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar(PreguntaFrecuenteParametrosDTO preguntaFrecuenteParametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _preguntaFrecuenteService.Actualizar(preguntaFrecuenteParametros, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: DELETE.
        /// Autor: Gilmer Qm.
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica a la tabla y sus detalles
        /// </summary>
        /// <param name="id"> (PK) de la tabla </param> 
        /// <returns> bool  </returns>
        [Route("[action]/{id}")]
        [HttpDelete]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _preguntaFrecuenteService.Eliminar(id, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
