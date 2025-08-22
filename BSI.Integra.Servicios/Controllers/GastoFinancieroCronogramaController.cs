using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GastoFinancieroCronogramaController
    /// Autor: Griselberto Huaman.
    /// Fecha: 17/12/2022
    /// <summary>
    /// Gestión de GastoFinancieroCronograma
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GastoFinancieroCronogramaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public GastoFinancieroCronogramaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] CronogramaEnvioDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<GastoFinancieroCronograma> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] CronogramaEnvioDTO entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<GastoFinancieroCronograma> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_GastoFinancieroCronograma.
        /// </summary>
        /// <returns> IEnumerable<GastoFinancieroCronogramaDatosDTO> </returns>
        [HttpGet("ObtenerGastoFinancieroCronograma")]
        public IActionResult ObtenerGastoFinancieroCronograma()
        {
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.ObtenerGastoFinancieroCronograma();
                return Ok(respuesta);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_GastoFinancieroCronogramaDetalle por Id GastoFinancieroCronograma..
        /// </summary>
        /// <returns> IEnumerable<GastoFinancieroCronogramaDatosDTO> </returns>
        [HttpGet("ObtenerListaGastoFinancieroCronogramaDetalle/{IdCronograma}")]
        public IActionResult ObtenerListaGastoFinancieroCronogramaDetalle(int IdCronograma)
        {
            try
            {
                var servicio = new GastoFinancieroCronogramaDetalleService(unitOfWork);
                var respuesta = servicio.ObtenerListaGastoFinancieroCronogramaDetallePorIdGastoFinanciero(IdCronograma);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla y su detalle
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensajse de error</returns>
        [HttpDelete("EliminarCrogramayDetalle/{id}/{usuario}")]
        public IActionResult EliminarCrogramayDetalle(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.EliminarCrogramayDetalle(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla Cronograma y Detalle
        /// </summary>
        /// <param name="Json">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y variable bool y mensaje de error</returns>
        [HttpPost("InsertarCronogramaYDetalle")]
        public IActionResult InsertarCronogramaYDetalle(CronogramaYDetalleDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.InsertarCronogramaYDetalle(Json);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla y Detalle
        /// </summary>
        /// <param name="Json">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("ActualizarCronogramaYDetalle")]
        public IActionResult ActualizarCronogramaYDetalle(CronogramaYDetalleDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.ActualizarCronogramaYDetalle(Json);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez.
        /// Fecha:  04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Realize Reporte Detallado 
        /// </summary>
        /// <param name="Filtro">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Route("GenerarReportePrestamos")]
        [HttpPost]
        public ActionResult GenerarReportePrestamos([FromBody] FiltroReportePrestamoDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.GenerarReportePrestamos(Filtro);
                return Ok(respuesta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez.
        /// Fecha:  04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Entidades Financieras de las que se tiene un prestamo (utilizado para combobox)
        /// </summary>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>

        [Route("ObtenerListaPrestamos")]
        [HttpGet]
        public ActionResult ObtenerListaPrestamos()
        {
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.ObtenerListaPrestamos();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez.
        /// Fecha:  04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la Lista de Prestamos realizados registrados en GastoFinancieroCronograma
        /// </summary>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>

        [Route("ObtenerListaEntidadesFinancierasConPrestamo")]
        [HttpGet]
        public ActionResult ObtenerListaEntidadesFinancierasConPrestamo()
        {
            try
            {
                var servicio = new GastoFinancieroCronogramaService(unitOfWork);
                var respuesta = servicio.ObtenerListaEntidadesFinancierasConPrestamo();
                return Ok(respuesta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
