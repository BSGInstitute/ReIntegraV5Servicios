using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MatriculaIntenaController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de MatriculaIntena
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MatriculaIntenaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public MatriculaIntenaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Crea la Matricula
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("GenerarMatriculaCabecera")]
        public IActionResult GenerarMatriculaCabecera( [FromBody] MatriculaCronogramaDTO MatriculaCronograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicio.GenerarMatriculaCabecera(MatriculaCronograma);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el cronograma segun la matricula
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCronogramaPagoPorCodigoMatricula/{CodigoMatricula}")]
        public IActionResult ObtenerCronogramaPagoPorCodigoMatricula(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CronogramaPagoService(unitOfWork);
                var respuesta = servicio.ObtenerCronogramaPagoPorCodigoMatricula(CodigoMatricula);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el IdPEspecifico
        /// </summary>
        /// <param name="CodigoMatricula">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCronogramaBusqueda/{CodigoMatricula}")]
        public IActionResult ObtenerCronogramaBusqueda(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicio.ObtenerCronogramaBusqueda(CodigoMatricula);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el IdPEspecifico
        /// </summary>
        /// <param name="CodigoMatricula">Entidad a insertar</param>
        /// /// <param name="IdPEspecifico">id Pescifico</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpGet("CargarMatricula/{CodigoMatricula}/{IdPEspecifico}")]
        public IActionResult CargarMatricula(string CodigoMatricula, int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MatriculaCabeceraService(unitOfWork);
                var respuesta = servicio.CargarMatricula(CodigoMatricula, IdPEspecifico);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el IdPEspecifico
        /// </summary>
        /// <param name="CodigoMatricula">Entidad a insertar</param>
        /// /// <param name="IdPEspecifico">id Pescifico</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("ActualizarCronogramaPago")]
        public IActionResult ActualizarCronogramaPago([FromBody]CronogramaPagoAlumnoDTO CronogramaPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CronogramaPagoService(unitOfWork);
                var respuesta = servicio.ActualizarCronogramaPago(CronogramaPago);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
