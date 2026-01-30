using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Comercial;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="lineamientoCalificacionFaseDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: LineamientoCalificacionFase </returns>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult Insertar([FromBody] LineamientoCalificacionFaseEntradaDTO lineamientoCalificacionFaseEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionFaseService = new LineamientoCalificacionFaseService(_unitOfWork);
                var lineamientoCalificacionFase = new LineamientoCalificacionFase();
                lineamientoCalificacionFase.IdCriterioCalificacionFaseOportunidad = lineamientoCalificacionFaseEntradaDTO.IdCriterioCalificacionFaseOportunidad;
                lineamientoCalificacionFase.Orden = lineamientoCalificacionFaseEntradaDTO.Orden;
                lineamientoCalificacionFase.IdCriticidadCalificacion = lineamientoCalificacionFaseEntradaDTO.IdCriticidadCalificacion;
                lineamientoCalificacionFase.Nombre = lineamientoCalificacionFaseEntradaDTO.NombreLineamiento;
                lineamientoCalificacionFase.Descripcion = lineamientoCalificacionFaseEntradaDTO.Descripcion;
                lineamientoCalificacionFase.Estado = true;
                lineamientoCalificacionFase.UsuarioCreacion = lineamientoCalificacionFaseEntradaDTO.Usuario;
                lineamientoCalificacionFase.UsuarioModificacion = lineamientoCalificacionFaseEntradaDTO.Usuario;
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

        /// Tipo Función: PUT
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza los datos de la entidad LineamientoCalificacionFase
        /// </summary>
        /// <param name="lineamientoCalificacionFaseDTO"> Datos necesarios para la actualización </param>
        /// <returns> Entidad: LineamientoCalificacionFase </returns>
        [Route("[action]")]
        [Authorize]
        [HttpPut]
        public IActionResult Actualizar([FromBody] LineamientoCalificacionFaseEntradaDTO lineamientoCalificacionFaseEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lineamientoCalificacionFaseService = new LineamientoCalificacionFaseService(_unitOfWork);
                var lineamientoCalificacionFase = new LineamientoCalificacionFase();
                lineamientoCalificacionFase = lineamientoCalificacionFaseService.ObtenerPorId(lineamientoCalificacionFaseEntradaDTO.Id);
                lineamientoCalificacionFase.IdCriterioCalificacionFaseOportunidad = lineamientoCalificacionFaseEntradaDTO.IdCriterioCalificacionFaseOportunidad;
                lineamientoCalificacionFase.Orden = lineamientoCalificacionFaseEntradaDTO.Orden;
                lineamientoCalificacionFase.IdCriticidadCalificacion = lineamientoCalificacionFaseEntradaDTO.IdCriticidadCalificacion;
                lineamientoCalificacionFase.Nombre = lineamientoCalificacionFaseEntradaDTO.NombreLineamiento;
                lineamientoCalificacionFase.Descripcion = lineamientoCalificacionFaseEntradaDTO.Descripcion;
                lineamientoCalificacionFase.Estado = true;
                lineamientoCalificacionFase.UsuarioModificacion = lineamientoCalificacionFaseEntradaDTO.Usuario;
                lineamientoCalificacionFase.FechaModificacion = DateTime.Now;
                var resultado = lineamientoCalificacionFaseService.Update(lineamientoCalificacionFase);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de LineamientoCalificacionFase por Id
        /// </summary>
        /// <param name="id"> Id de la entidad a eliminar </param>
        /// <param name="usuario"> Usuario que ejecuta la eliminación </param>
        /// <returns> true or false </returns>
        [Route("[action]/{id}")]
        [Authorize]
        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var servicio = new ActividadCabeceraService(_unitOfWork);
                var usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var lineamientoCalificacionFaseService = new LineamientoCalificacionFaseService(_unitOfWork);
                var resultado = lineamientoCalificacionFaseService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos de LineamientoCalificacionFase por Id
        /// </summary>
        /// <param name="id"> Id de la entidad a consultar </param>
        /// <returns> Entidad: LineamientoCalificacionFase </returns>
        [Route("[action]/{id}")]
        [Authorize]
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var lineamientoCalificacionFaseService = new LineamientoCalificacionFaseService(_unitOfWork);
                var resultado = lineamientoCalificacionFaseService.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET 
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de TransicionCalificacionFase y su detalles  
        /// </summary> 
        /// <returns> List<TransicionCalificacionFaseDTO> </returns>
        [Route("[action]")]
        [Authorize]
        [HttpGet]
        public IActionResult Obtener()
        {
            try
            {
                var lineamientoCalificacionFaseService = new LineamientoCalificacionFaseService(_unitOfWork);
                var resultado = lineamientoCalificacionFaseService.Obtener();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
