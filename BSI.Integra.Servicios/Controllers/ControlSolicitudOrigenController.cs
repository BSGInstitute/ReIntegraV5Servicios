using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ControlSolicitudOrigenController
    /// Autor: Jorge Gamero.
    /// Fecha: 18/07/2024
    /// <summary>
    /// Gestión de Control Origen de Solicitud
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ControlSolicitudOrigenController : Controller
    {
        private IUnitOfWork unitOfWork;
        public ControlSolicitudOrigenController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jorge Gamero.
        /// Fecha: 18/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla
        /// </summary>
        /// <param name="controlSolicitudOrigenEntradaDTO"> Datos necesarios para la inserción de datos </param>
        /// <returns> Entidad: ControlSolicitudOrigen </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] ControlSolicitudOrigenEntradaDTO controlSolicitudOrigenEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var controlSolicitudOrigenService = new ControlSolicitudOrigenService(unitOfWork);
                var controlSolicitudOrigen = new ControlSolicitudOrigen();
                controlSolicitudOrigen.Nombre = controlSolicitudOrigenEntradaDTO.Nombre;
                controlSolicitudOrigen.Descripcion = controlSolicitudOrigenEntradaDTO.Descripcion;
                controlSolicitudOrigen.UsuarioCreacion = controlSolicitudOrigenEntradaDTO.Usuario;
                controlSolicitudOrigen.UsuarioModificacion = controlSolicitudOrigenEntradaDTO.Usuario;
                controlSolicitudOrigen.FechaCreacion = DateTime.Now;
                controlSolicitudOrigen.FechaModificacion = DateTime.Now;
                controlSolicitudOrigen.Estado = true;
                var resultado = controlSolicitudOrigenService.Add(controlSolicitudOrigen);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Jorge Gamero.
        /// Fecha: 18/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualización básica a la tabla
        /// </summary>
        /// <param name="controlSolicitudOrigenEntradaDTO"> Datos necesarios para la actualización de datos </param>
        /// <returns> Entidad: ControlSolicitudOrigen </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] ControlSolicitudOrigenEntradaDTO controlSolicitudOrigenEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var controlSolicitudOrigenService = new ControlSolicitudOrigenService(unitOfWork);
                var controlSolicitudOrigen = new ControlSolicitudOrigen();
                controlSolicitudOrigen = controlSolicitudOrigenService.ObtenerPorId(controlSolicitudOrigenEntradaDTO.Id.Value);
                controlSolicitudOrigen.Nombre = controlSolicitudOrigenEntradaDTO.Nombre;
                controlSolicitudOrigen.Descripcion = controlSolicitudOrigenEntradaDTO.Descripcion;
                controlSolicitudOrigen.UsuarioModificacion = controlSolicitudOrigenEntradaDTO.Usuario;
                controlSolicitudOrigen.FechaModificacion = DateTime.Now;
                var resultado = controlSolicitudOrigenService.Update(controlSolicitudOrigen);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Jorge Gamero.
        /// Fecha: 18/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de la tabla
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <param name="usuario"> Autor de la modificación </param>
        /// <returns> true or false </returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var controlSolicitudOrigenService = new ControlSolicitudOrigenService(unitOfWork);
                var resultado = controlSolicitudOrigenService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 18/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla para combos
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                var controlSolicitudOrigenService = new ControlSolicitudOrigenService(unitOfWork);
                var resultado = controlSolicitudOrigenService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 18/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id, Nombre y Descripción de todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerRegistros()
        {
            try
            {
                var controlSolicitudOrigenService = new ControlSolicitudOrigenService(unitOfWork);
                var resultado = controlSolicitudOrigenService.ObtenerRegistros();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
