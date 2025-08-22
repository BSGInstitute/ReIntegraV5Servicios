using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionEnvioAutomaticoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Alumno
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfiguracionEnvioAutomaticoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ConfiguracionEnvioAutomaticoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todo el filtro completo de EstadoMatricula, MatriculaCabecera, Plantilla, TipoEnvioAutimatico
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoMatriculaService estadoMatriculaService = new EstadoMatriculaService(unitOfWork);
                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                PlantillaService plantillaService = new PlantillaService(unitOfWork);
                TipoEnvioAutomaticoService tipoEnvioAutomaticoService = new TipoEnvioAutomaticoService(unitOfWork);
                var detalles = new
                {
                    filtroEstadoMatricula = estadoMatriculaService.ObtenerCombo(),
                    filtroSubEstadoMatricula = matriculaCabeceraService.ObtenerSubEstadoMatricula(),
                    filtroPlantillas = plantillaService.ObtenerListaPlantillasConfiguracionEnvio(),
                    filtroTipoEnvioAutomatico = tipoEnvioAutomaticoService.ObtenerTodoCombo()
                };
                return Ok(detalles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Configuración de Envío Automático
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerConfiguracionEnvioAutomatico()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionEnvioAutomaticoService configuracionEnvioAutomaticoService = new ConfiguracionEnvioAutomaticoService(unitOfWork);
                var lista = configuracionEnvioAutomaticoService.ObtenerConfiguracionEnvioAutomatico();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta DTO Configuración Envío Automático
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ConfiguracionEnvioAutomaticoDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionEnvioAutomaticoService configuracionEnvioAutomaticoService = new ConfiguracionEnvioAutomaticoService(unitOfWork);
                ConfiguracionEnvioAutomaticoDetalleService configuracionEnvioAutomaticoDetalleService = new ConfiguracionEnvioAutomaticoDetalleService(unitOfWork);

                var lista = configuracionEnvioAutomaticoService.InsertarConfiguracion(json);

                foreach (var item in json.ListaConfiguracionEnvioAutomatico)
                {
                    ConfiguracionEnvioAutomaticoDetalle configuracionDetalle = new ConfiguracionEnvioAutomaticoDetalle();

                    item.IdConfiguracionEnvioAutomatico = lista.FirstOrDefault().Id;
                    configuracionDetalle.IdConfiguracionEnvioAutomatico = item.IdConfiguracionEnvioAutomatico;
                    configuracionDetalle.IdTipoEnvioAutomatico = item.IdTipoEnvioAutomatico;
                    configuracionDetalle.IdTiempoFrecuencia = item.IdTiempoFrecuencia;
                    configuracionDetalle.IdPlantilla = item.IdPlantilla;
                    configuracionDetalle.Valor = item.Valor;
                    if (configuracionDetalle.IdTipoEnvioAutomatico == 1)
                    {
                        configuracionDetalle.EnvioWhatsApp = true;
                        configuracionDetalle.EnvioCorreo = false;
                        configuracionDetalle.EnvioMensajeTexto = false;
                    }
                    else if (configuracionDetalle.IdTipoEnvioAutomatico == 2)
                    {
                        configuracionDetalle.EnvioWhatsApp = false;
                        configuracionDetalle.EnvioCorreo = false;
                        configuracionDetalle.EnvioMensajeTexto = true;
                    }
                    else if (configuracionDetalle.IdTipoEnvioAutomatico == 3)
                    {
                        configuracionDetalle.EnvioWhatsApp = false;
                        configuracionDetalle.EnvioCorreo = true;
                        configuracionDetalle.EnvioMensajeTexto = false;
                    }
                    configuracionDetalle.HoraEnvioAutomatico = item.HoraEnvioAutomatico;
                    configuracionDetalle.Estado = true;
                    configuracionDetalle.UsuarioCreacion = json.Usuario;
                    configuracionDetalle.UsuarioModificacion = json.Usuario;
                    configuracionDetalle.FechaCreacion = DateTime.Now;
                    configuracionDetalle.FechaModificacion = DateTime.Now;
                    configuracionEnvioAutomaticoDetalleService.Add(configuracionDetalle);
                }
                return Ok(json);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Configuración Asociada
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerConfiguracionAsociada([FromBody] int idConfiguracionEnvioAutomatico)
        {
            try
            {
                ConfiguracionEnvioAutomaticoDetalleService configuracionEnvioAutomaticoDetalleService = new ConfiguracionEnvioAutomaticoDetalleService(unitOfWork);
                var listaConfiguracion = configuracionEnvioAutomaticoDetalleService.ObtenerConfiguracionEnvioAutomaticoDetalle(idConfiguracionEnvioAutomatico);
                return Ok(listaConfiguracion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza DTO Configuración Envío Automático
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] ConfiguracionEnvioAutomaticoDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionEnvioAutomaticoService configuracionEnvioAutomaticoService = new ConfiguracionEnvioAutomaticoService(unitOfWork);
                ConfiguracionEnvioAutomaticoDetalleService configuracionEnvioAutomaticoDetalleService = new ConfiguracionEnvioAutomaticoDetalleService(unitOfWork);

                var lista = configuracionEnvioAutomaticoService.ActualizarConfiguracion(json);
                var resetConfiguracionDetalle = configuracionEnvioAutomaticoDetalleService.ObtenerPorIdConfiguracionEnvioAutomatico(json.Id);

                if (resetConfiguracionDetalle.Count() > 0)
                {
                    foreach (var tmp in resetConfiguracionDetalle)
                    {
                        var configuracionDetalleEstado = configuracionEnvioAutomaticoDetalleService.ObtenerPorId(tmp.Id);
                        configuracionDetalleEstado.Estado = false;
                        configuracionEnvioAutomaticoDetalleService.Update(configuracionDetalleEstado);
                    }
                }

                foreach (var item in json.ListaConfiguracionEnvioAutomatico)
                {
                    ConfiguracionEnvioAutomaticoDetalle configuracionDetalle = new ConfiguracionEnvioAutomaticoDetalle();

                    item.IdConfiguracionEnvioAutomatico = lista.FirstOrDefault().Id;
                    configuracionDetalle.IdConfiguracionEnvioAutomatico = item.IdConfiguracionEnvioAutomatico;
                    configuracionDetalle.IdTipoEnvioAutomatico = item.IdTipoEnvioAutomatico;
                    configuracionDetalle.IdTiempoFrecuencia = item.IdTiempoFrecuencia;
                    configuracionDetalle.IdPlantilla = item.IdPlantilla;
                    configuracionDetalle.Valor = item.Valor;
                    if (configuracionDetalle.IdTipoEnvioAutomatico == 1)
                    {
                        configuracionDetalle.EnvioWhatsApp = true;
                        configuracionDetalle.EnvioCorreo = false;
                        configuracionDetalle.EnvioMensajeTexto = false;
                    }
                    else if (configuracionDetalle.IdTipoEnvioAutomatico == 2)
                    {
                        configuracionDetalle.EnvioWhatsApp = false;
                        configuracionDetalle.EnvioCorreo = false;
                        configuracionDetalle.EnvioMensajeTexto = true;
                    }
                    else if (configuracionDetalle.IdTipoEnvioAutomatico == 3)
                    {
                        configuracionDetalle.EnvioWhatsApp = false;
                        configuracionDetalle.EnvioCorreo = true;
                        configuracionDetalle.EnvioMensajeTexto = false;
                    }
                    configuracionDetalle.HoraEnvioAutomatico = item.HoraEnvioAutomatico;
                    configuracionDetalle.Estado = true;
                    configuracionDetalle.UsuarioCreacion = json.Usuario;
                    configuracionDetalle.UsuarioModificacion = json.Usuario;
                    configuracionDetalle.FechaCreacion = DateTime.Now;
                    configuracionDetalle.FechaModificacion = DateTime.Now;
                    configuracionEnvioAutomaticoDetalleService.Add(configuracionDetalle);
                }
                return Ok(json);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina DTO Configuración Envío Automático
        /// </summary>
        /// <returns></returns>
        [Route("[action]/{IdConfiguracion}/{Usuario}")]
        [HttpPost]
        public IActionResult Eliminar(int idConfiguracion, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionEnvioAutomaticoService configuracionEnvioAutomaticoService = new ConfiguracionEnvioAutomaticoService(unitOfWork);
                var lista = configuracionEnvioAutomaticoService.EliminarConfiguracion(idConfiguracion, usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
