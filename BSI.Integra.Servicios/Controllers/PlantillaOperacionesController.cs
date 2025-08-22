using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestSharp.Extensions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PlantillaOperacionesController
    /// Autor: Jonathan Caipo
    /// Fecha: 30/12/2022
    /// <summary>
    /// Gestión de Alumno
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PlantillaOperacionesController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public PlantillaOperacionesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza en envio de correo
        /// </summary>
        /// <param name="remitente"></param>
        /// <param name="codigoAlumno"></param>
        /// <param name="destinatarios"></param>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        [Route("[action]/{remitente}/{codigoAlumno}/{destinatarios}/{idPlantilla}")]
        [HttpGet]
        public ActionResult Envio(string remitente, string codigoAlumno, string destinatarios, int idPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaOperacionesService plantillaOperacionesService = new PlantillaOperacionesService(unitOfWork);
                var respuesta = plantillaOperacionesService.Envio(remitente, codigoAlumno, destinatarios, idPlantilla);             
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Envia un numero individual de la oportunidad una plantilla y segun el flag
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idPlantilla">Id de la plantilla (PK de la tabla mkt.T_Plantilla)</param>
        /// <param name="flagSeccion">Flag para determinar el reemplazo que se realizara</param>
        /// <returns>ActionResult con estado 200, 400</returns>
        [Route("[action]/{idOportunidad}/{idPlantilla}/{flagSeccion}")]
        [HttpGet]
        public ActionResult EnvioWhatsappNumeroIndividual(int idOportunidad, int idPlantilla, int flagSeccion)
        {
            try
            {
                PlantillaOperacionesService plantillaOperacionesService = new PlantillaOperacionesService(unitOfWork);
                var respuesta = plantillaOperacionesService.EnvioWhatsappNumeroIndividual(idOportunidad, idPlantilla, flagSeccion);             
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            try
            {
                PlantillaOperacionesService plantillaOperacionesService = new PlantillaOperacionesService(unitOfWork);
                var respuesta = plantillaOperacionesService.ObtenerFiltros();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}