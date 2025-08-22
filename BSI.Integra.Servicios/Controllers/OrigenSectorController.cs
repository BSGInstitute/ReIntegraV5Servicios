using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: OrigenSectorController
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión de OrigenSector
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class OrigenSectorController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public OrigenSectorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 26/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] OrigenSector entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.Add(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 26/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<OrigenSector> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 26/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] OrigenSector entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.Update(entidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 26/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<OrigenSector> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 26/08/2022
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
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 26/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{listadoIds}/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 01/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los origenes que ya se configuraron y los no configurados
        /// </summary>
        /// <returns>Retorna 200 mas la lista de proveedores de campania configurados y los no configurados</returns>
        [HttpGet("ObtenerAsignacionOrigen")]
        public IActionResult ObtenerAsignacionOrigen()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.ObtenerAsignacionOrigen();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 01/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Trae los origen sector configurados y activos
        /// </summary>
        /// <returns>Retorna 200 y la lista de sectores activos y configurados</returns>
        [HttpGet("ObtenerOrigenSector")]
        public IActionResult ObtenerOrigenSector()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.ObtenerOrigenSector();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 01/09/2022k
        /// Versión: 1.0
        /// <summary>
        /// Eliminacion de un sector 
        /// </summary>
        /// <param name="IdOrigenSector">Id origen sector</param>
        /// <param name="UsuarioModificacion">Usuario modificacion</param>

        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpDelete("EliminarSector/{IdOrigenSector}/{UsuarioModificacion}")]
        public IActionResult EliminarSector(int IdOrigenSector, string UsuarioModificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.EliminarSector(IdOrigenSector, UsuarioModificacion);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualizar la configuracion de una lista de categoria origen 
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("ActualizarConfiguracionCategoriaOrigen")]
        public IActionResult ActualizarConfiguracionCategoriaOrigen([FromBody] List<ActualizarDatosDeConfiguracionDTO> ListaConfiguracionActualizada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.ActualizarDatosDeConfiguracion(ListaConfiguracionActualizada);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Eliminacion de un sector 
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("ActualizarConfiguracionCategoriaOrigenAgrupado")]
        public IActionResult ActualizarConfiguracionCategoriaOrigenAgrupado([FromBody] ActualizarDatosDeConfiguracionAgrupadoDTO ActualizarConfiguracionDatosAgrupados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OrigenSectorService(unitOfWork);
                var respuesta = servicio.ActualizarDatosDeConfiguracionAgrupados(ActualizarConfiguracionDatosAgrupados);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
