using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.ExperianSentinel;
using BSI.Integra.Aplicacion.Transversal.ExperianSentinel;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SentinelController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/06/2022
    /// <summary>
    /// Gestión de Sentinel
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SentinelController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public SentinelController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] Sentinel entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SentinelService(unitOfWork);
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
        /// Fecha: 14/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<Sentinel> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SentinelService(unitOfWork);
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
        /// Fecha: 14/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] Sentinel entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SentinelService(unitOfWork);
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
        /// Fecha: 14/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<Sentinel> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SentinelService(unitOfWork);
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
        /// Fecha: 14/06/2022
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
                var servicio = new SentinelService(unitOfWork);
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
        /// Fecha: 14/06/2022
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
                var servicio = new SentinelService(unitOfWork);
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
        /// Fecha: 14/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Sentinel
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerSentinel")]
        public IActionResult ObtenerSentinel()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new SentinelService(unitOfWork);
                return Ok(servicio.ObtenerSentinel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Sentinel para combo.
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
                var servicio = new SentinelService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de T_Sentinel asociados a un Id Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerSentinelParaAgendaPorIdAlumno/{idAlumno}")]
        public IActionResult ObtenerSentinelParaAgendaPorIdAlumno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idAlumno <= 0)
            {
                return BadRequest("El Id del Alumno no existe.");
            }
            try
            {
                var servicio = new SentinelService(unitOfWork);
                return Ok(servicio.ObtenerDatosSentinelDetallePorIdAlumno(idAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 19/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalles del registro de T_Sentinel asociados a un IdSentinel
        /// </summary>
        /// <param name="idSentinel">Id del Alumno</param>
        /// <returns> Retorna 200 y lista de detalles de registro o 400 y mensaje de error </returns>
        [Route("[action]/{idSentinel}")]
        [HttpGet]
        public ActionResult ObtenerDetalleSentinel(int idSentinel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idSentinel <= 0)
            {
                return BadRequest("El Id del Sentinel no existe.");
            }
            try
            {
                var sentinelService = new SentinelService(unitOfWork);
                var resultado = sentinelService.ObtenerDetalleSentinel(idSentinel);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("TipoServicio")]
        public IActionResult ObtenerTipoServicio()
        {
            try
            {
                return Ok(new { TipoServicio = SentinelTipoServicioConfig.TipoActual });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CambiarTipoServicio")]
        public IActionResult CambiarTipoServicio([FromBody] CambiarTipoServicioRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Tipo))
                return BadRequest("El campo 'Tipo' es requerido. Valores válidos: REST, SOAP");

            try
            {
                SentinelTipoServicioConfig.Cambiar(request.Tipo);
                return Ok(new { TipoServicio = SentinelTipoServicioConfig.TipoActual });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ConsultarCrudo/{tipo}/{dni}")]
        public async Task<IActionResult> ConsultarCrudoExperian(string tipo, string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return BadRequest("El DNI es requerido.");

            tipo = tipo?.Trim().ToUpperInvariant() ?? string.Empty;
            if (tipo != "SOAP" && tipo != "REST")
                return BadRequest("El tipo debe ser 'SOAP' o 'REST'.");

            try
            {
                var cliente = ExperianSentinelClientFactory.Crear(tipo, unitOfWork);
                return Ok(await cliente.ConsultarAsyncCrudo(dni, "D"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Consultar/{tipo}/{dni}")]
        public async Task<IActionResult> ConsultarExperian(string tipo, string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return BadRequest("El DNI es requerido.");

            tipo = tipo?.Trim().ToUpperInvariant() ?? string.Empty;
            if (tipo != "SOAP" && tipo != "REST")
                return BadRequest("El tipo debe ser 'SOAP' o 'REST'.");

            try
            {
                var cliente = ExperianSentinelClientFactory.Crear(tipo, unitOfWork);
                return Ok(await cliente.ConsultarAsync(dni, "D"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class CambiarTipoServicioRequest
    {
        public string Tipo { get; set; } = string.Empty;
    }
}
