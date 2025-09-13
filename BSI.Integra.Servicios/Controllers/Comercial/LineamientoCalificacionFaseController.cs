using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Comercial;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: LineamientoCalificacionController
    /// Autor: Jose Vega
    /// Fecha: 12/09/2025
    /// <summary>
    /// Gestión de Transiciones entre Fases de Oportunidad
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class LineamientoCalificacionFaseController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public LineamientoCalificacionFaseController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 13/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="lineamientoCalificacionFaseDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacionFase </returns>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult Insertar([FromBody] LineamientoCalificacionFaseDTO lineamientoCalificacionFaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionFaseService = new LineamientoCalificacionFaseService(_unitOfWork);
                var lineamientoCalificacionFase = new LineamientoCalificacionFase();
                lineamientoCalificacionFase.IdCriterioCalificacionFaseOportunidad = lineamientoCalificacionFaseDTO.IdCriterioCalificacionFaseOportunidad;
                lineamientoCalificacionFase.Orden = lineamientoCalificacionFaseDTO.Orden;
                lineamientoCalificacionFase.IdCriticidadCalificacion = lineamientoCalificacionFaseDTO.IdCriticidadCalificacion;
                lineamientoCalificacionFase.Nombre = lineamientoCalificacionFaseDTO.Nombre;
                lineamientoCalificacionFase.Descripcion = lineamientoCalificacionFaseDTO.Descripcion;
                lineamientoCalificacionFase.Estado = true;
                lineamientoCalificacionFase.UsuarioCreacion = lineamientoCalificacionFaseDTO.Usuario;
                lineamientoCalificacionFase.UsuarioModificacion = lineamientoCalificacionFaseDTO.Usuario;
                lineamientoCalificacionFase.FechaCreacion = DateTime.Now;
                lineamientoCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = lineamientoCalificacionFaseService.Add(lineamientoCalificacionFase);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 13/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: SolicitudTipoReporte </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] LineamientoCalificacionFaseDTO lineamientoCalificacionFaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionFaseService = new LineamientoCalificacionFaseService(_unitOfWork);
                var lineamientoCalificacionFase = new LineamientoCalificacionFase();
                lineamientoCalificacionFase = lineamientoCalificacionFaseService.ObtenerPorId(lineamientoCalificacionFaseDTO.Id.Value);
                lineamientoCalificacionFase.IdCriterioCalificacionFaseOportunidad = lineamientoCalificacionFaseDTO.IdCriterioCalificacionFaseOportunidad;
                lineamientoCalificacionFase.Orden = lineamientoCalificacionFaseDTO.Orden;
                lineamientoCalificacionFase.IdCriticidadCalificacion = lineamientoCalificacionFaseDTO.IdCriticidadCalificacion;
                lineamientoCalificacionFase.Nombre = lineamientoCalificacionFaseDTO.Nombre;
                lineamientoCalificacionFase.Descripcion = lineamientoCalificacionFaseDTO.Descripcion;
                lineamientoCalificacionFase.Estado = true;
                lineamientoCalificacionFase.UsuarioModificacion = lineamientoCalificacionFaseDTO.Usuario;
                lineamientoCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = lineamientoCalificacionFaseService.Update(lineamientoCalificacionFase);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var lineamientoCalificacionFaseService = new LineamientoCalificacionFaseService(unitOfWork);
                var resultado = lineamientoCalificacionFaseService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
