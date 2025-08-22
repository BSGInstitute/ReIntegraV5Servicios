using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: InformacionWebinarController
    /// Autor: Giancarlo Romero
    /// Fecha: 22/05/2023
    /// <summary>
    /// Gestion de endpoints para la informacion de webinar y los alumnos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class InformacionWebinarController : Controller
    {
        private InformacionWebinarService _informacionWebinarService;
        public InformacionWebinarController(IUnitOfWork unitOfWork)
        {
            _informacionWebinarService = new InformacionWebinarService(unitOfWork);
        }
        /// Tipo Función: POST 
        /// Autor: Giancarlo Romero
        /// Fecha: 22/05/2023
        /// Versión: 2.0
        /// <summary>
        /// Obtiene los combos para el modulo de informacion webinar
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerCombosModulo()
        {
            try
            {
                var resultado = _informacionWebinarService.ObtenerCombosModulo();
                return Ok(new
                {
                    resultado.PGenerals,
                    resultado.Pespecificos,
                    resultado.CentroCostos,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 19/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los webinars por filtro
        /// </summary>
        /// <param name="filtro">Objeto de clase WebinarReporteFiltroDTO</param>
        /// <returns>Response 200 con objeto anonimo (Lista de objetos de clase WebinarDDetalleSesionDTO ordenados, caso contrario response 400 con el mensaje de error</returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerWebinarPorFiltro([FromBody] WebinarReporteFiltroDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var webinar = _informacionWebinarService.ObtenerWebinarPorFiltro(filtro);
                return Ok(webinar.OrderBy(w => w.Fecha));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 19/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los webinars por filtro
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Response 200 con objeto anonimo (Lista de objetos de clase ComboGenericoDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoWebinar(int idPGeneral)
        { 
            try
            {
                var rpta = _informacionWebinarService.ObtenerPEspecificoWebinar(idPGeneral);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
