using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: DataCreditoController
    /// Autor: Gilmer Quispe.
    /// Fecha: 08/09/2022
    /// <summary>
    /// Gestión general DataCredito
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class DataCreditoController : Controller
    {
        private IUnitOfWork unitOfWork;
        public DataCreditoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 08/09/2022
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns> Objeto DTO </returns>
        /// <returns> objetoDTO: SeguimientoAsesorDTO </returns>
        /// [Route("[Action]/{numero}/{apellido}/{usuario}")]
        [Route("[Action]/{documento}/{idAlumno}/{usuario}")]
        [HttpGet]
        public ActionResult ActualizarInformacionDataCredito(string documento, int idAlumno, string usuario)
        {
            try
            {
                var servicioDataCredito = new DataCreditoService(unitOfWork);
                var servcicioDataCreditoBusqueda = new DataCreditoBusquedumService(unitOfWork);

                var idDataCredito = servcicioDataCreditoBusqueda.ObtenerIdDataCreditoDeAlumnoPorId(idAlumno);

                servicioDataCredito.ConsultarAlumnoColombia(documento, idDataCredito.ApellidoPaterno, 1, usuario);
                idDataCredito = servcicioDataCreditoBusqueda.ObtenerIdDataCreditoDeAlumnoPorId(idAlumno);

                return Ok(idDataCredito);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 14/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la informacion de datacredito del Alumno
        /// </summary>
        /// <returns>DataCreditoRespuestaDTO</returns>
        /// [Route("[Action]/{idAlumno}/{usuario}")]
        [Route("[Action]/{idAlumno}/{usuario}")]
        [HttpGet]
        public ActionResult ObtenerInformacionDataCredito(int idAlumno, string usuario)
        {
            try
            {
                var servicioDCBusqueda = new DataCreditoBusquedumService(unitOfWork);
                var idDataCredito = servicioDCBusqueda.ObtenerIdDataCreditoDeAlumnoPorId(idAlumno);
                var respuesta = new DataCreditoRespuestaDTO();

                if (idDataCredito.Id != null)
                {
                    var informacionPersonal = servicioDCBusqueda.ObtenerInformacionDataCreditoPorId((int)idDataCredito.Id);
                    var tarjetas = servicioDCBusqueda.ObtenerHistorialTarjetasDataCreditoPorId((int)idDataCredito.Id);
                    var deudas = servicioDCBusqueda.ObtenerHistorialDeudasDataCreditoPorId((int)idDataCredito.Id);

                    respuesta.Informacion = informacionPersonal;
                    respuesta.Tarjeta = tarjetas;
                    respuesta.Credito = deudas;
                }
                else
                {
                    return BadRequest("Alumno sin informacion data credito");
                }
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
