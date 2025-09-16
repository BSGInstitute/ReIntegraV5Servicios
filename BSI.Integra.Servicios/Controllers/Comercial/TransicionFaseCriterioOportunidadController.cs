using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: SolicitudTipoReporteController
    /// Autor: Jose Vega.
    /// Fecha: 15/09/2025
    /// <summary>
    /// Gestión de Transicion de Fase Criterio Oportunidad
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TransicionFaseCriterioOportunidadController : Controller
    {
            private IUnitOfWork _unitOfWork;

            public TransicionFaseCriterioOportunidadController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            /// Tipo Función: POST
            /// Autor: Jose Vega
            /// Fecha: 15/09/2025
            /// Versión: 1.0
            /// <summary>
            /// Realiza una inserción básica en la tabla TransicionFaseCriterioOportunidad.
            /// </summary>
            /// <param name="transicionFaseCriterioOportunidadEntradaDTO">Datos necesarios para la inserción.</param>
            /// <returns>Entidad: TransicionFaseCriterioOportunidad</returns>
            [Route("[action]")]
            [Authorize]
            [HttpPost]
            public IActionResult Insertar([FromBody] TransicionFaseCriterioOportunidadEntradaDTO transicionFaseCriterioOportunidadEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var transicionFaseCriterioOportunidadService = new TransicionFaseCriterioOportunidadService(_unitOfWork);
                    var transicionFaseCriterioOportunidad = new TransicionFaseCriterioOportunidad();
                    transicionFaseCriterioOportunidad.IdTransicionFaseOportunidad = transicionFaseCriterioOportunidadEntradaDTO.IdTransicionFaseOportunidad;
                    transicionFaseCriterioOportunidad.IdCriterioCalificacionFaseOportunidad = transicionFaseCriterioOportunidadEntradaDTO.IdCriterioCalificacionFaseOportunidad;
                    transicionFaseCriterioOportunidad.Estado = true;
                    transicionFaseCriterioOportunidad.UsuarioCreacion = transicionFaseCriterioOportunidadEntradaDTO.Usuario;
                    transicionFaseCriterioOportunidad.UsuarioModificacion = transicionFaseCriterioOportunidadEntradaDTO.Usuario;
                    transicionFaseCriterioOportunidad.FechaCreacion = DateTime.Now;
                    transicionFaseCriterioOportunidad.FechaModificacion = DateTime.Now;
                    var resultado = transicionFaseCriterioOportunidadService.Add(transicionFaseCriterioOportunidad);
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
            /// Actualiza los datos de la entidad TransicionFaseCriterioOportunidad.
            /// </summary>
            /// <param name="transicionFaseCriterioOportunidadEntradaDTO">Datos necesarios para la actualización.</param>
            /// <returns>Entidad: TransicionFaseCriterioOportunidad</returns>
            [HttpPut("[Action]")]
            public IActionResult Actualizar([FromBody] TransicionFaseCriterioOportunidadEntradaDTO transicionFaseCriterioOportunidadEntradaDTO)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var transicionCalificacionFaseService = new TransicionFaseCriterioOportunidadService(_unitOfWork);
                    var transicionCalificacionFase = new TransicionFaseCriterioOportunidad();
                    transicionCalificacionFase = transicionCalificacionFaseService.ObtenerPorId(transicionFaseCriterioOportunidadEntradaDTO.Id);
                    transicionCalificacionFase.IdTransicionFaseOportunidad = transicionFaseCriterioOportunidadEntradaDTO.IdTransicionFaseOportunidad;
                    transicionCalificacionFase.IdCriterioCalificacionFaseOportunidad = transicionFaseCriterioOportunidadEntradaDTO.IdCriterioCalificacionFaseOportunidad;
                    transicionCalificacionFase.UsuarioModificacion = transicionFaseCriterioOportunidadEntradaDTO.Usuario;
                    transicionCalificacionFase.FechaModificacion = DateTime.Now;
                    var resultado = transicionCalificacionFaseService.Update(transicionCalificacionFase);
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
            /// Realiza una eliminación lógica en la tabla TransicionFaseCriterioOportunidad.
            /// </summary>
            /// <param name="id">Id de la entidad a eliminar.</param>
            /// <param name="usuario">Usuario que ejecuta la eliminación.</param>
            /// <returns>bool</returns>
            [HttpDelete("Eliminar/{id}/{usuario}")]
            public IActionResult Eliminar(int id, string usuario)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {

                    var transicionFaseCriterioOportunidadService = new TransicionFaseCriterioOportunidadService(_unitOfWork);
                    var resultado = transicionFaseCriterioOportunidadService.Delete(id, usuario);
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
            /// Obtiene la lista de TransicionFaseCriterioOportunidad y sus detalles.
            /// </summary>
            /// <returns>List<TransicionFaseCriterioOportunidadDTO></returns>
            [Route("[action]")]
            [HttpGet]
            public IActionResult Obtener()
            {
                try
                {
                var transicionFaseCriterioOportunidadService = new TransicionFaseCriterioOportunidadService(_unitOfWork);
                var resultado = transicionFaseCriterioOportunidadService.Obtener();
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
            /// Obtiene la información de una TransiciónFaseCriterioOportunidad por Id.
            /// </summary>
            /// <param name="id">Id de la transición de fase criterio oportunidad.</param>
            /// <returns>Entidad: TransicionFaseCriterioOportunidadDTO</returns>
            [Route("[action]/{id}")]
            [HttpGet]
            public IActionResult ObtenerPorId(int id)
            {
                try
                {
                var transicionFaseCriterioOportunidadService = new TransicionFaseCriterioOportunidadService(_unitOfWork);
                var resultado = transicionFaseCriterioOportunidadService.ObtenerPorId(id);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            
            } 
    }
}

