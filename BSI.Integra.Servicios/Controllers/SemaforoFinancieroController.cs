using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SemaforoFinancieroController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión de SemaforoFinanciero
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SemaforoFinancieroController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public SemaforoFinancieroController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] SemaforoFinanciero entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SemaforoFinancieroService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<SemaforoFinanciero> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SemaforoFinancieroService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] SemaforoFinanciero entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SemaforoFinancieroService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<SemaforoFinanciero> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SemaforoFinancieroService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
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
                var servicio = new SemaforoFinancieroService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
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
                var servicio = new SemaforoFinancieroService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_SemaforoFinanciero
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerSemaforoFinanciero")]
        public IActionResult ObtenerSemaforoFinanciero()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SemaforoFinancieroService(unitOfWork);
                return Ok(servicio.ObtenerSemaforoFinanciero());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_SemaforoFinanciero para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SemaforoFinancieroService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 02/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalles del T_SemaforoFinancieroDetalleVariable por el Id
        /// </summary>
		/// <param name="IdSemaforoFinancieroDetalle">id</param>
        /// <returns>SemaforoFinancieroDetalleVariableInformacionDetalladaDTO</returns>
        [HttpPost("ObtenerSemaforoFinancieroDetalleVariable")]
        public ActionResult ObtenerSemaforoFinancieroDetalleVariable([FromBody] int IdSemaforoFinancieroDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var servicioSemaforoFinanciero = new SemaforoFinancieroDetalleService(unitOfWork);
                var resultado = servicioSemaforoFinanciero.ObtenerVariables(IdSemaforoFinancieroDetalle);
                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest("No se encontro semaforo financiero.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 02/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalles de la tabla T_SemaforoFinancieroDetalle por el idSemaforoFinanciero
        /// </summary>
		/// <param name="IdSemaforoFinanciero">id </param>
        /// <returns>SemaforoFinancieroDetalleDTO</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerSemaforoFinancieroDetalle([FromBody] int IdSemaforoFinanciero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var servicioSemaforoFinDet = new SemaforoFinancieroDetalleService(unitOfWork);
                var resultado = servicioSemaforoFinDet.ObtenerSemaforoFinancieroDetallePorIdSemaforoFinanciero(IdSemaforoFinanciero).ToList();
                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest("No se encontro semaforo financiero.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 02/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta nuevo semaforo
        /// </summary>
        /// <param name="Semaforo">DTO enviado desde la interfaz<</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarSemaforoFinanciero([FromBody] SemaforoFinancieroInsertarNuevoDTO Semaforo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SemaforoFinanciero semaforoNuevo = new SemaforoFinanciero();
                SemaforoFinancieroDetalle semaforoDetalleNuevo = new SemaforoFinancieroDetalle();

                var servicioSemaforo = new SemaforoFinancieroService(unitOfWork);
                var servicioSemaforoDetalle = new SemaforoFinancieroDetalleService(unitOfWork);

                semaforoNuevo.IdPais = Semaforo.IdPais;
                semaforoNuevo.Activo = Semaforo.Activo;
                semaforoNuevo.Estado = true;
                semaforoNuevo.UsuarioCreacion = Semaforo.Usuario;
                semaforoNuevo.UsuarioModificacion = Semaforo.Usuario;
                semaforoNuevo.FechaCreacion = DateTime.Now;
                semaforoNuevo.FechaModificacion = DateTime.Now;

                servicioSemaforo.InsertarNuevoSemaforo(semaforoNuevo);

                foreach (var item in Semaforo.Detalle)
                {
                    semaforoDetalleNuevo.IdSemaforoFinanciero = semaforoNuevo.Id;
                    semaforoDetalleNuevo.Nombre = item.Nombre;
                    semaforoDetalleNuevo.Mensaje = item.Mensaje;
                    semaforoDetalleNuevo.Color = item.Color;
                    semaforoDetalleNuevo.Estado = true;
                    semaforoDetalleNuevo.UsuarioCreacion = Semaforo.Usuario;
                    semaforoDetalleNuevo.UsuarioModificacion = Semaforo.Usuario;
                    semaforoDetalleNuevo.FechaCreacion = DateTime.Now;
                    semaforoDetalleNuevo.FechaModificacion = DateTime.Now;

                    servicioSemaforoDetalle.InsertarNuevoSemaforoDetalle(semaforoDetalleNuevo);
                    semaforoDetalleNuevo.Id = 0;
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="Semaforo">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult ActualizarSemaforoFinanciero([FromBody] SemaforoFinancieroInsertarNuevoDTO Semaforo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioSemaforoFinanciero = new SemaforoFinancieroService(unitOfWork);
                var respuesta = servicioSemaforoFinanciero.ActualizarSemaforoFinanciero(Semaforo);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
