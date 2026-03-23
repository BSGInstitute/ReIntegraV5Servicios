using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers.Planificacion.AgendaPlanificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GestionDocenteFlujoController : ControllerBase
    {
        private readonly IGestionDocenteFlujoService _gestionDocenteFlujoService;

        public GestionDocenteFlujoController(IGestionDocenteFlujoService gestionDocenteFlujoService)
        {
            _gestionDocenteFlujoService = gestionDocenteFlujoService;
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que inserta un nuevo flujo docente.
        /// </summary>
        /// <param name="dto">DTO con los datos del flujo a crear.</param>
        /// <returns>ActionResult con el Id del flujo creado.</returns>
        [HttpPost("Insertar")]
        public async Task<IActionResult> Insertar([FromBody] GestionDocenteFlujoDTO dto)
        {
            try
            {
                var id = await _gestionDocenteFlujoService.InsertarAsync(dto);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: PUT
        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que actualiza un flujo docente existente.
        /// </summary>
        /// <param name="dto">DTO con los datos actualizados del flujo.</param>
        /// <returns>ActionResult con el resultado de la operación.</returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] GestionDocenteFlujoDTO dto)
        {
            try
            {
                var rpta = await _gestionDocenteFlujoService.ActualizarAsync(dto);
                return Ok(new { Exito = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que elimina lógicamente un flujo docente.
        /// </summary>
        /// <param name="id">Identificador del flujo a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la operación.</param>
        /// <returns>ActionResult con el resultado de la operación.</returns>
        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar(int id, string usuario)
        {
            try
            {
                var rpta = await _gestionDocenteFlujoService.EliminarAsync(id, usuario);
                return Ok(new { Exito = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene todos los flujos docentes activos.
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteFlujoDTO.</returns>
        [HttpGet("ObtenerTodo")]
        public async Task<IActionResult> ObtenerTodo()
        {
            try
            {
                var lista = await _gestionDocenteFlujoService.ObtenerTodoAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de estados de flujo docente.
        /// </summary>
        /// <returns>ActionResult con la lista de estados de flujo.</returns>
        [HttpGet("ObtenerEstadosFlujo")]
        public IActionResult ObtenerEstadosFlujo()
        {
            try
            {
                var lista = _gestionDocenteFlujoService.ObtenerEstadosFlujo();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de categorías de flujo docente.
        /// </summary>
        /// <returns>ActionResult con la lista de categorías.</returns>
        [HttpGet("ObtenerCategorias")]
        public IActionResult ObtenerCategorias()
        {
            try
            {
                var lista = _gestionDocenteFlujoService.ObtenerCategorias();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 03/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene todas las actividades cabecera disponibles para asociar a flujos, permitiendo filtrar por categoría.
        /// </summary>
        /// <returns>ActionResult con la lista de actividades cabecera.</returns>
        [HttpGet("ObtenerActividadesCabecera")]
        public IActionResult ObtenerActividadesCabecera([FromQuery] int? idCategoria = null)
        {
            try
            {
                var lista = _gestionDocenteFlujoService.ObtenerActividadesCabecera(idCategoria);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene un flujo docente por su identificador con nombres de estado y categoría resueltos.
        /// </summary>
        /// <param name="id">Identificador del flujo.</param>
        /// <returns>ActionResult con GestionDocenteFlujoOutputDTO.</returns>
        [HttpGet("ObtenerFlujoPorId/{id}")]
        public IActionResult ObtenerFlujoPorId(int id)
        {
            try
            {
                var flujo = _gestionDocenteFlujoService.ObtenerFlujoPorId(id);
                if (flujo == null) return NotFound(new { Exito = false, Mensaje = "No se encontró el flujo." });
                return Ok(flujo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene un flujo completo con todas sus actividades, detalles, disparadores, reglas,
        /// sesiones, ocurrencias, configuración IA y ejemplos de entrenamiento.
        /// </summary>
        /// <param name="id">Identificador del flujo.</param>
        /// <returns>ActionResult con FlujoCompletoDTO.</returns>
        [HttpGet("ObtenerFlujoCompleto/{id}")]
        public async Task<IActionResult> ObtenerFlujoCompleto(int id)
        {
            try
            {
                var flujo = await _gestionDocenteFlujoService.ObtenerFlujoCompletoAsync(id);
                if (flujo == null) return NotFound(new { Exito = false, Mensaje = "No se encontró el flujo." });
                return Ok(flujo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 26/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que duplica un flujo docente completo: crea un nuevo flujo con un nuevo nombre
        /// y realiza una copia profunda de todas sus actividades, detalles, disparadores, reglas,
        /// sesiones, ocurrencias, configuración IA y ejemplos de entrenamiento.
        /// </summary>
        /// <param name="request">DTO con IdFlujoOriginal, NuevoNombre y Usuario.</param>
        /// <returns>ActionResult con el Id del nuevo flujo creado.</returns>
        [HttpPost("Duplicar")]
        public async Task<IActionResult> Duplicar([FromBody] DuplicarFlujoRequestDTO request)
        {
            try
            {
                var id = await _gestionDocenteFlujoService.DuplicarFlujoAsync(request);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
    }
}
