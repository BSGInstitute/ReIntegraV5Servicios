using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: AgendaReprogramacionController
    /// Autor: Flavio R Mamani Fabian.
    /// Fecha: 15/02/2023
    /// <summary>
    /// Gestión de Reprogramaciones de la agenda
    /// </summary>
    [Route("api/Comercial/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AgendaReprogramacionController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public AgendaReprogramacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// Tipo Función: POST 
        /// Autor: Gilmer Quispe.
        /// Fecha: 24/1|/2022
        /// Versión: 1.0
        /// <summary>
        /// Finaliza una acitividad y programa una nueva
        /// </summary>
        /// <param name="dto">DTO de entrada para la funcion</param>
        /// <returns>Ok</returns>
        [Route("[Action]")]
        [HttpPost]
        public async Task<ActionResult> FinalizarYProgramarActividadAlternoV2([FromBody] FinalizarProgramarActividadAlternoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaReprogramacionService agendaReprogramacionService = new AgendaReprogramacionService(_unitOfWork);
                IAsignacionManualService asignacionManualService = new AsignacionManualService(_unitOfWork);
                IContadorBicService contadorBicService = new ContadorBicService(_unitOfWork);
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);

                var respuesta = await agendaReprogramacionService.FinalizarYProgramarActividadAlternoAsync(dto);

                //COMENTAR PARA PROD Y DESCOMENTAR PARA PREPUBLICACION
                try
                {
                    var calculo = contadorBicService.CalcularDiasParaBICPorIdOportunidadWhatsapp(dto.ActividadAntigua.IdOportunidad);
                    var datosOportunidad = oportunidadService.ObtenerPorId(dto.ActividadAntigua.IdOportunidad);
                    var datosAlumno = alumnoService.ObtenerPorId(dto.ActividadAntigua.IdAlumno);
                    var respuestawhatsapp = asignacionManualService.EnvioWhatsAppTercerDiaSinContacto(dto.ActividadAntigua.IdOportunidad, (datosAlumno.IdCodigoPais == null ? 0 : datosAlumno.IdCodigoPais.Value), datosOportunidad.IdPersonalAsignado.Value, datosOportunidad.IdCategoriaOrigen.Value, calculo.FirstOrDefault());


                    if ((calculo.FirstOrDefault().DiasSinContactoManhana == 3) || (calculo.FirstOrDefault().DiasSinContactoTarde == 3))
                    {
                        var envioInsertado = oportunidadService.InsertarEnviosWhatsappDiasSinContacto(dto.ActividadAntigua.IdOportunidad);
                    }
                    //Validar que sen datos IT o IP
                    if(datosOportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIT || datosOportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIP)
                    {
                        // Validar envio de correo al segundo dia sin contacto
                        if (calculo.FirstOrDefault()!.DiasSinContactoManhana <= 2 && calculo.FirstOrDefault()!.DiasSinContactoTarde <= 2 )
                        {
                            if (calculo.FirstOrDefault()!.DiasSinContactoManhana == 2 || calculo.FirstOrDefault()!.DiasSinContactoTarde == 2)
                            {
                                IAgendaService agendaService = new AgendaService(_unitOfWork);
                                // 134 Correo Confirmación de Participación
                                agendaService.EnviarCorreoOportunidadAutomatico(dto.ActividadAntigua.IdOportunidad, 134, "Automatico134");
                            }
                        }
                    }
                }
                catch
                {

                }

                return Ok(respuesta);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: POST 
        /// Autor: Jose Vega
        /// Fecha: 26/12/2025
        /// Versión: 1.0
        /// <summary>
        /// <summary>
        /// Finaliza una actividad y programa una nueva (Módulo Planificación)
        /// </summary>
        /// <param name="dto">DTO de entrada para la funcion</param>
        /// <returns>Ok</returns>
        [Route("[Action]")]
        [HttpPost]
        public async Task<ActionResult> FinalizarYProgramarActividadPlanificacion([FromBody] FinalizarProgramarGestionPlaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaReprogramacionService agendaReprogramacionService = new AgendaReprogramacionService(_unitOfWork);
                var respuesta = await agendaReprogramacionService.FinalizarYProgramarGestionAsync(dto);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST 
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Finalizacion de la actividad alterno
        /// </summary>
        /// <param name="parametroFinalizarActividadAlternoDTO">DTO de entrada para la funcion</param>
        /// <returns>Ok</returns>
        [Route("[Action]")]
        [HttpPost]
        public async Task<ActionResult> FinalizarActividadCrearOportunidadAlterno([FromBody] VentaCruzadaDTO parametroFinalizarActividadAlternoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var agendaReprogramacionService = new AgendaReprogramacionService(_unitOfWork);
                var respuesta = await agendaReprogramacionService.FinalizarActividadCrearOportunidadAlternoAsync(parametroFinalizarActividadAlternoDTO);
                return Ok(new
                {
                    ActividadEjecutada = respuesta.realizada,
                    IdOportunidad = respuesta.idOportunidad
                });
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: POST 
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Finalizacion de la actividad alterno
        /// </summary>
        /// <param name="jsonDto">DTO de entrada para la funcion</param>
        /// <returns>Ok</returns>
        [Route("[Action]")]
        [HttpPost]
        public async Task<ActionResult> CerrarActividad([FromBody] CerrarActividadDTO jsonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaReprogramacionService agendaReprogramacionService = new AgendaReprogramacionService(_unitOfWork);
                var respuesta = await agendaReprogramacionService.CerrarActividadAsync(jsonDto);
                return Ok(new { Realizada = respuesta.realizada, IdOportunidad = respuesta.idOportunidad });
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Fecha y Hora de Reprogramación Automática
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <param name="codigoFase">Codigo de Fase</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerFechaHoraActividadReprogramacionAutomatica/{idOportunidad}/{codigoFase}/{idOcurrencia}")]
        public IActionResult ObtenerFechaHoraActividadReprogramacionAutomatica(int idOportunidad, string codigoFase, int idOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerFechaHoraActividadReprogramacionAutomatica(idOportunidad, codigoFase, idOcurrencia));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 28/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un boolean de la oportunidad trabajada
        /// </summary>
        /// <param name="dto">DTO: FinalizarProgramarActividadAlternoDTO</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [Route("[Action]/{idOportunidad}/{idFaseOportunidad}/{idActividadDetalle}")]
        [HttpGet]
        public IActionResult ValidacionReprogramacion(int idOportunidad, int idFaseOportunidad, int idActividadDetalle)
        {
            try
            {
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                var validacion = oportunidadService.ValidarFaseOportunidad(idOportunidad, idFaseOportunidad, idActividadDetalle);
                return Ok(validacion);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza el cambio de centro costo 
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <param name="idCentroCosto">Id del centro de costo</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [Route("[Action]/{idOportunidad}/{idCentroCosto}")]
        [HttpPost]
        public IActionResult RealizarCambioCentroCosto(int idOportunidad, int idCentroCosto)
        {
            try
            {
                //Se comenta para evitar el cambio de centro de costo desde V5 
                //IAgendaReprogramacionService servicio = new AgendaReprogramacionService(_unitOfWork);
                //var resultado = servicio.RealizarCambioCentroCosto(idOportunidad, idCentroCosto, _tokenManager.UserName);
                //return Ok(resultado);
                return BadRequest("El cambio de Centro de Costo solo se puede hacer desve la Agenda V6");
            }
            catch
            {
                throw;
            }
        }
    }
}
