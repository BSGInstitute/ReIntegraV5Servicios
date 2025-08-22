using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Actividad Cabecera
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 07/06/2023
    /// <summary>
    /// Gestión de Actividad Cabecera
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ActividadCabeceraController : ControllerBase
    {


        private IUnitOfWork unitOfWork;

        public ActividadCabeceraController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los datos de actividad cabecera
        /// </summary>

        [HttpGet("ObtenerTodoActividadAutomatica")]
        public IActionResult ObtenerTodoActividadAutomatica()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ActividadCabeceraService(unitOfWork);
                return Ok(servicio.ObtenerTodoActividadAutomatica());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Conjunto lista
        /// </summary>
        /// 
        [Route("[action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ObtenerConjuntoListaMailingMasivo(int IdConjuntoLista)
        {
            try
            {
                var servicio = new ConjuntoListaDetalleService(unitOfWork);
                return Ok(servicio.ObtenerListasMailingMasivo(IdConjuntoLista));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina Actividad Cabecera
        /// </summary>
        /// <param name="id">Id de la actividad cabecera</param>
        /// <returns> true </returns>

        [Route("[action]/{id}")]
        [HttpPost]
        public IActionResult EliminarActividadCabecera(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var servicio = new ActividadCabeceraService(unitOfWork);
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                return Ok(servicio.EliminarActividadCabecera(id, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta Actividad Cabecera
        /// </summary>
        /// <param name="ObjetoDTO">Entidad a insertar</param>
        /// <returns> true </returns>

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarActividadCabecera(ListaActividadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var servicio = new ActividadCabeceraService(unitOfWork);
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
    
                return Ok(servicio.InsertarActividadCabecera(ObjetoDTO, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Actividad Cabecera
        /// </summary>
        /// <param name="ObjetoDTO">Entidad a insertar</param>
        /// <returns> true </returns>

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarActividadCabecera(ListaActividadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var servicio = new ActividadCabeceraService(unitOfWork);
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
 
                return Ok(servicio.ActualizarActividadCabecera(ObjetoDTO, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Frecuencia de la actividad
        /// </summary>
  

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaFrecuenciaActividad()
        {
            try
            {
                var servicio = new FrecuenciaService(unitOfWork);
               
                return Ok(servicio.ObtenerListaFrecuenciaActividad());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Áctividad Base
        /// </summary>

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerActividadesBaseMasivo()
        {
            try
            {
                var servicio = new ActividadCabeceraService(unitOfWork);
               
                return Ok(servicio.ObtenerActividadesBaseMasivo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Actividad por id
        /// </summary>
        ///  /// <param name="IdActividadCabecera">id Actividad Cabecera/param>
        /// <returns> true </returns>

        [Route("[action]/{IdActividadCabecera}")]
        [HttpGet]
        public ActionResult ObtenerActividadPorId(int IdActividadCabecera)
        {
            try
            {
                var servicio = new ActividadCabeceraService(unitOfWork);

                return Ok(servicio.ObtenerActividadPorId(IdActividadCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Actividad dia por id Actividad Cabecera
        /// </summary>
        ///  /// <param name="idActividadCabecera">id Actividad Cabecera/param>
        /// <returns> true </returns>

        [Route("[action]/{idActividadCabecera}")]
        [HttpGet]
        public ActionResult ObtenerActividadDiaPorID(int idActividadCabecera)
        {
            try
            {
                var servicio = new ActividadCabeceraService(unitOfWork);

                return Ok(servicio.ObtenerActividadDiaPorID(idActividadCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 07/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta configuracion de envio mailing
        /// </summary>
        /// <param name="ObjetoJson">Entidad a insertar</param>
        /// <returns> true </returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarConfiguracionEnvioMailing(ConfiguracionEnvioMailingDTO ObjetoJson)
        {
            try
            {
                var servicio = new ConfiguracionEnvioMailingService(unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                return Ok(servicio.Insertar(ObjetoJson, Usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}
