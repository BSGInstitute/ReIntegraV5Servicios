using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.Excel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionResumenGrabacionOnlineController
    /// Autor: Jorge Gamero
    /// Fecha: 28/01/2025
    /// <summary>
    /// Gestión general de ConfiguracionResumenGrabacionOnline
    /// </summary>
    [Route("api/ConfiguracionResumenGrabacionOnline")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfiguracionResumenGrabacionOnlineController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ConfiguracionResumenGrabacionOnlineController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// TipoFuncion: POST
        /// Autor: Jorge Gamero
        /// Fecha: 28/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Insertar ConfiguracionResumenGrabacionOnline 
        /// </summary>
        /// <param ConfiguracionResumenGrabacionOnlineEntradaDTO> Parametros de entrada </param>
        /// <returns>  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarEliminarConfiguracionResumenGrabacionOnline([FromBody] List<ConfiguracionResumenGrabacionOnlineEntradaDTO> detalles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var configuracionResumenGrabacionOnlineService = new ConfiguracionResumenGrabacionOnlineService(unitOfWork);
                var resultado = configuracionResumenGrabacionOnlineService.ObtenerConfiguracionResumenGrabacionOnlinePorSesion(detalles[0].IdPEspecificoSesion);
                if (resultado != null && resultado.Any()) //Existe, se actualizan (eliminan) los registros que llegaron en 0
                {
                    var listadoActualizarIdsInactivos = resultado
                        .Where(r => detalles.Any(d => d.IdResumenGrabacionOnline == r.IdResumenGrabacionOnline && d.Estado == false))
                        .Select(r => r.Id)
                        .ToList();
                    var listadoActualizarIdsActivos = resultado
                        .Where(r => detalles.Any(d => d.IdResumenGrabacionOnline == r.IdResumenGrabacionOnline && d.Estado == true))
                        .Select(r => r.Id)
                        .ToList();

                    List<int> eliminados = new List<int>();
                    List<int> actualizados = new List<int>();

                    if (listadoActualizarIdsInactivos.Any())
                    {
                        configuracionResumenGrabacionOnlineService.ActualizaInactivo(listadoActualizarIdsInactivos, detalles[0].IdPEspecificoSesion, detalles[0].Usuario);
                        eliminados.AddRange(listadoActualizarIdsInactivos);
                    }
                    
                    if (listadoActualizarIdsActivos.Any())
                    {
                        configuracionResumenGrabacionOnlineService.ActualizaActivo(listadoActualizarIdsActivos, detalles[0].IdPEspecificoSesion, detalles[0].Usuario);
                        actualizados.AddRange(listadoActualizarIdsActivos);
                    }

                    return Ok(new
                    {
                        Eliminados = eliminados,
                        Actualizados = actualizados
                    });
                }
                else //No existe, inserta registros
                {
                    var entidades = detalles.Select(detalle => new ConfiguracionResumenGrabacionOnline
                    {
                        IdPEspecificoSesion = detalle.IdPEspecificoSesion,
                        IdResumenGrabacionOnline = detalle.IdResumenGrabacionOnline,
                        Estado = detalle.Estado,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = detalle.Usuario,
                        UsuarioModificacion = detalle.Usuario
                    }).ToList();
                    var nuevasEntidades = configuracionResumenGrabacionOnlineService.Add(entidades);
                    return Ok(nuevasEntidades);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener configuración de ConfiguracionResumenGrabacionOnline 
        /// </summary>
        /// <param int> Parametros de entrada </param>
        /// <returns>  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerConfiguracionResumenGrabacionOnlinePorSesion([FromBody] int idPEspecificoSesion)
        {
            try
            {
                var configuracionResumenGrabacionOnlineService = new ConfiguracionResumenGrabacionOnlineService(unitOfWork);
                var resultado = configuracionResumenGrabacionOnlineService.ObtenerConfiguracionResumenGrabacionOnlinePorSesion(idPEspecificoSesion);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Proceso para generar resúmenes de grabaciones
        /// </summary>
        /// <param IniciarProcesoResumenGrabacionesDTO> Parametros de entrada </param>
        /// <returns> Task<(string resultado, HttpStatusCode statusCode)> </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GenerarResumenGrabaciones(IniciarProcesoResumenGrabacionesDTO datos)
        {
            var configuracionResumenGrabacionOnlineService = new ConfiguracionResumenGrabacionOnlineService(unitOfWork);
            var resultado = await configuracionResumenGrabacionOnlineService.GenerarResumenGrabaciones(datos);
            return StatusCode((int)resultado.statusCode, resultado.resultado);
        }

        /// TipoFuncion: POST
        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Proceso para enviar resumen de grabaciones
        /// </summary>
        /// <param idPEspecificoSesion> Parametros de entrada </param>
        /// <returns>  </returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<bool> EnvioResumenGrabaciones(int idPEspecificoSesion)
        {
            var configuracionResumenGrabacionOnlineService = new ConfiguracionResumenGrabacionOnlineService(unitOfWork);
            var resultado = await configuracionResumenGrabacionOnlineService.EnvioResumenGrabaciones(idPEspecificoSesion);
            return resultado;
        }

        /// TipoFuncion: POST
        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene campo TextoTranscripcion de tabla T_ProcesamientoSesionOnline filtrado por id
        /// </summary>
        /// <param int> Parametros de entrada </param>
        /// <returns>  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTextoTranscripcionPorId([FromBody] int id)
        {
            try
            {
                var configuracionResumenGrabacionOnlineService = new ConfiguracionResumenGrabacionOnlineService(unitOfWork);
                var resultado = configuracionResumenGrabacionOnlineService.ObtenerTextoTranscripcionPorId(id);
                return Ok(new { texto = resultado });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene campo TextoGuionAudio de tabla T_ProcesamientoSesionOnline filtrado por id
        /// </summary>
        /// <param int> Parametros de entrada </param>
        /// <returns>  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTextoGuionAudioPorId([FromBody] int id)
        {
            try
            {
                var configuracionResumenGrabacionOnlineService = new ConfiguracionResumenGrabacionOnlineService(unitOfWork);
                var resultado = configuracionResumenGrabacionOnlineService.ObtenerTextoGuionAudioPorId(id);
                return Ok(new { texto = resultado });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
