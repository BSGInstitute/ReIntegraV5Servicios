using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: AgendaInformacionActividadController
    /// Autor: Gilmer Quispe.
    /// Fecha: 18/02/2023
    /// <summary>
    /// Gestión de Actividades para Agenda
    /// </summary>
    [Route("api/Comercial/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AgendaActividadController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IAgendaActividadService _agendaActividadService;
        public AgendaActividadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _agendaActividadService = new AgendaActividadService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los dias sin contacto
        /// </summary>
        /// <returns> 0 nro de dias sin contacto </returns>
        [HttpGet("[action]/{idOportunidad}")]
        public IActionResult ObtenerDiasSinContactoPorOportunidad(int idOportunidad)
        {
            IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
            int diasSinContacto = agendaActividadService.ObtenerDiasSinContactoPorOportunidad(idOportunidad);
            return Ok(diasSinContacto);
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida si puede mostrarse o no en la agenda
        /// </summary>
        /// <param name="DTO">Parametros de entrada [IdOportunidad] y []</param>
        /// <returns> Confirmación de Validación </returns>
        /// <returns> Bool </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ValidarVisualizacionDatosOportunidad([FromBody] SolicitudVisualizarOportunidadDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                var VisualizarDatos = agendaActividadService.ValidarVisualizacionDatosOportunidad(DTO.IdOportunidad, DTO.IdPersonal);//0:no puede ver pero puede ingresar solicitud,1:puede verlo hasta la fecha,2:no puede verlo y no puede ingresar
                if (VisualizarDatos == 1)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest("No se puede solicitar ya que cuenta con una solicitud pendiente");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cabecera Speech para Agenda
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCabeceraSpeech/{idOportunidad}/{idCentroCosto}")]
        public IActionResult ObtenerCabeceraSpeech(int idOportunidad, int idCentroCosto)
        {
            IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
            return Ok(agendaActividadService.ObtenerCabeceraSpeech(idOportunidad, idCentroCosto));
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Publico Objetivo para un Programa General
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo asociado al Programa</param>
        /// <param name="idOportunidad">Id de la Oportunidad asociada a las Respuestas</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerPublicoObjetivoPrograma/{idCentroCosto}/{idOportunidad}")]
        public IActionResult ObtenerPublicoObjetivoPrograma(int idCentroCosto, int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerPublicoObjetivoPrograma(idCentroCosto, idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Requisitos de Certificacion de un Programa General.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad asociada al Programa</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerRequisitosCertificacionProgramaPorIdOportunidad/{idOportunidad}")]
        public IActionResult ObtenerRequisitosCertificacionProgramaPorIdOportunidad(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerRequisitosCertificacionProgramaPorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Motivaciones y sus Argumentos asociados a un Programa General.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad asociada al Programa</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerArgumentosMotivacionProgramaPorIdOportunidad/{idOportunidad}")]
        public IActionResult ObtenerArgumentosMotivacionProgramaPorIdOportunidad(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerArgumentosMotivacionProgramaPorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 30/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la informacion de Oportunidades asociada a una ClasificacionPersona y un Alumno.
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerOportunidadInformacion/{idAlumno}/{idClasificacionPersona}")]
        public IActionResult ObtenerOportunidadInformacion(int idAlumno, int idClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerOportunidadInformacion(idAlumno, idClasificacionPersona));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{idPGeneral}")]
        public IActionResult ObtenerCentroCostoVentaCruzadaV2(int idPGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerCentroCostoVentaCruzada(idPGeneral));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 30/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la informacion de Oportunidades asociada a una ClasificacionPersona y un Alumno.
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerOportunidadInformacionPersonalizado/{idOportunidad}")]
        public IActionResult ObtenerOportunidadInformacionPersonalizado(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                var oportunidad = oportunidadService.ObtenerPorId(idOportunidad);

                if (oportunidad?.IdClasificacionPersona == null)
                {
                    return BadRequest("La oportunidad no tiene una clasificación de persona asociada.");
                }
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerOportunidadInformacion(oportunidad.IdAlumno.Value, oportunidad.IdClasificacionPersona.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Problemas de ProgramaGeneral y sus Argumentos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProgramaGeneralProblemaDetallePorIdOportunidad/{idOportunidad}")]
        public IActionResult ObtenerProgramaGeneralProblemaDetallePorIdOportunidad(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerProgramaGeneralProblemaDetallePorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el ultimo Tipo de Interaccion de Correos asociados a un Alumno y un Personal.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCorreoInteraccionV2EnviadosPorPersonal/{idAlumno}/{idPersonal}")]
        public IActionResult ObtenerCorreoInteraccionV2EnviadosPorPersonal(int idAlumno, int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerCorreoInteraccionV2EnviadosPorPersonal(idAlumno, idPersonal));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Competidores relacionados a una Oportunidad para Agenda.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerCompetidorPorIdOportunidad/{idOportunidad}")]
        public IActionResult ObtenerCompetidorPorIdOportunidad(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerCompetidorPorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 14/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información de competidores
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionCompetidores")]
        public async Task<IActionResult> ObtenerInformacionCompetidores([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null || !filtro.TryGetValue("idOportunidad", out var idOportunidadStr))
                {
                    return BadRequest(new { Error = "El campo 'idOportunidad' es requerido." });
                }

                if (!int.TryParse(idOportunidadStr, out int idOportunidad))
                {
                    return BadRequest(new { Error = "El valor de 'idOportunidad' debe ser un número entero válido." });
                }

                var servicio = new AgendaActividadService(_unitOfWork);
                var respuesta = await servicio.CargarInformacionCompetidoresAsync(idOportunidad);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Prerequisitos, Beneficios y Competidores asociados a una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerPrerequisitosBeneficiosCompetidoresPorIdOportunidad/{idOportunidad}")]
        public IActionResult ObtenerPrerequisitosBeneficiosCompetidoresPorIdOportunidad(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerPrerequisitosBeneficiosCompetidoresPorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Comentarios por Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerHistorialComentariosPorIdOportunidad/{idOportunidad}")]
        public IActionResult ObtenerHistorialComentariosPorIdOportunidad(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerHistorialComentariosPorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Interacciones de la Oportunidad por su Id
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerHistorialInteraccionesPorIdOportunidad/{idOportunidad}")]
        public IActionResult ObtenerHistorialInteraccionesPorIdOportunidad(int idOportunidad)
        {

            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerHistorialInteraccionesPorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27//11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Interacciones de la Oportunidad por su Id
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idOportunidad}")]
        public IActionResult ObtenerHistorialInteraccionesPorIdOportunidad3cx(int idOportunidad)
        {
            return Ok(_agendaActividadService.ObtenerHistorialInteraccionesPorIdOportunidad3cx(idOportunidad));
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin Riquelme
        /// Fecha: 27//11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Interacciones de la Oportunidad por su Id
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idActividadDetalle}")]
        public IActionResult ObtenerRecomendacionesPorIdActividadDetalle(int idActividadDetalle)
        {
            return Ok(_agendaActividadService.ObtenerRecomendacionesPorIdActividadDetalle(idActividadDetalle));
        }


        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 27//12/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Interacciones de la Oportunidad por su Id con transcripciones
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerHistorialInteraccionesPorIdOportunidadAnalisisMensajePersonalizado/{idOportunidad}")]
        public async Task<IActionResult> ObtenerHistorialInteraccionesPorIdOportunidadAnalisisMensajePersonalizado(int idOportunidad)
        {
            var result = await _agendaActividadService.ObtenerHistorialInteraccionesPorIdOportunidadMensajePersonalizado(idOportunidad);
            return Ok(result);
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Datos del Alumno
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDatosAlumnoPersonalizado/{idOportunidad}")]
        public IActionResult ObtenerDatosAlumnoPersonalizado(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Obtener idClasificacionPersona desde la oportunidad
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                var oportunidad = oportunidadService.ObtenerPorId(idOportunidad);

                if (oportunidad?.IdClasificacionPersona == null)
                {
                    return BadRequest("La oportunidad no tiene una clasificación de persona asociada.");
                }

                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                var result = agendaActividadService.ObtenerDatosAlumnoPersonalizado(oportunidad.IdClasificacionPersona.Value, idOportunidad);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de Preguntas Frecuentes asociadas a un Centro de Costo
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [Route("[Action]/{idCentroCosto}/{idOportunidad}")]
        [HttpGet]
        public IActionResult ObtenerPreguntasFrecuentes(int idCentroCosto, int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                var resultado = agendaActividadService.ObtenerPreguntasFrecuentes(idCentroCosto, idOportunidad);
                return Ok(new
                {
                    resultado.Data,
                    resultado.ModeloCertificado
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Preguntas Frecuentes de Cambio
        /// </summary>
        /// <param name="idCentroCosto">Id del centro de costo</param>
        /// <param name="idPrograma">Id del programa general/param>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <returns> Lista de Preguntas Frecuentes de Cambio </returns>
        /// <returns> Lista objeto DTO : List<PreguntaFrecuenteSeccionesDTO> </returns>
        /// <returns> Lista objeto DTO : List<ProgramaGeneralModeloCertificadoDTO> </returns>
        [Route("[Action]/{idCentroCosto}/{idPrograma}/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerPreguntasFrecuentesCambio(int idCentroCosto, int idPrograma, int idOportunidad)
        {
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                var resultado = agendaActividadService.ObtenerPreguntasFrecuentesCambio(idCentroCosto, idPrograma, idOportunidad);
                return Ok(new
                {
                    resultado.Data,
                    resultado.ModeloCertificado
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("[Action]/{idCentroCosto}/{idPrograma}/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerPreguntasFrecuentesCambioV2(int idCentroCosto, int idPrograma, int idOportunidad)
        {
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                var resultado = agendaActividadService.ObtenerPreguntasFrecuentesCambioV2(idCentroCosto, idPrograma, idOportunidad);
                return Ok(new
                {
                    resultado.Data,
                    resultado.ModeloCertificado
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Arbol de Ocurrencias Alterno.
        /// </summary>
        /// <param name="idActividadCabecera">Id de la Actividad Cabecera</param>
        /// <param name="idOcurrenciaPadre">Id de la Ocurrencia Padre</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerArbolOcurrenciaAlterno/{idActividadCabecera}/{idOcurrenciaPadre?}")]
        public IActionResult ObtenerArbolOcurrenciaAlterno(int idActividadCabecera, int idOcurrenciaPadre = 0)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IOcurrenciaActividadAlternoService ocurrenciaActividadAlterno = new OcurrenciaActividadAlternoService(_unitOfWork);
                return Ok(ocurrenciaActividadAlterno.ObtenerArbolOcurrenciaAlterno(idActividadCabecera, idOcurrenciaPadre));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Arbol de Ocurrencias Alterno.
        /// </summary>
        /// <param name="idActividadCabecera">Id de la Actividad Cabecera</param>
        /// <param name="idOcurrenciaPadre">Id de la Ocurrencia Padre</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idActividadCabecera}")]
        public IActionResult ObtenerOcurrenciaMarcador(int idActividadCabecera)
        {
            IOcurrenciaActividadAlternoService ocurrenciaActividadAlterno = new OcurrenciaActividadAlternoService(_unitOfWork);
            return Ok(ocurrenciaActividadAlterno.ObtenerOcurrenciaMarcador(idActividadCabecera));
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Arbol de Ocurrencias por Actividad Cabecera e IdOcurrencia Padre
        /// </summary>
        /// <returns> Arbol de Ocurrencias </returns>
        /// <returns> lista de objeto DTO : List<ArbolOcurenciaDTO> </returns>
        [Route("[action]/{idActividadCabecera}/{idOcurrenciaActividadPadre?}")]
        [HttpGet]
        public ActionResult ObtenerArbolOcurrencias(int idActividadCabecera, int idOcurrenciaActividadPadre = 0)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerArbolOcurrencias(idActividadCabecera, idOcurrenciaActividadPadre));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Arbol de Ocurrencias por Actividad Cabecera e IdOcurrencia Padre
        /// </summary>
        /// <returns> Arbol de Ocurrencias </returns>
        /// <returns> lista de objeto DTO : List<ArbolOcurenciaDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult CargarReporteIncidencia([FromBody] FiltroReporteIncidenciaDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.CargarReporteIncidencia(filtro));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Historial de Modificaciones del Alumno asociado a su Identificador
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerHistorialModificacionAlumnoPorIdAlumno/{idAlumno}")]
        public IActionResult ObtenerHistorialModificacionAlumnoPorIdAlumno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerHistorialModificacionAlumnoPorIdAlumno(idAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Documentos Legales dependiendo de Datos del Personal y Pais del Alumno.
        /// </summary>
        /// <param name="idAreaPersonal">Id de PersonalAreaTrabajo</param>
        /// <param name="rol">Nombre del Rol del Personal</param>
        /// <param name="idAlumno"></param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDocumentoLegal/{idAreaPersonal}/{rol}/{idAlumno}")]
        public IActionResult ObtenerDocumentoLegal(int idAreaPersonal, string rol, int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerDocumentoLegal(idAreaPersonal, rol, idAlumno));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el valor de las Plantillas asociadas a una Fase Oportunidad
        /// </summary>
        /// <param name="idFaseOportunidad">Id de la Fase Oportunidad</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerPlantillasPorIdFaseOportunidad/{idFaseOportunidad}")]
        public IActionResult ObtenerPlantillasPorIdFaseOportunidad(int idFaseOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerPlantillasPorIdFaseOportunidad(idFaseOportunidad));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el seguimiento del Asesor
        /// </summary>
        /// <param name="idAsesor">Id del Personal</param>
        /// <param name="idCategoriaOrigen">Id de Categoria Origen</param>
        /// <param name="estadoPantalla">Flag: 0 -> IS, 1 -> Opo. Cerrada, 2-> Solo para mostrar</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerSeguimientoAsesor/{idAsesor}/{idCategoriaOrigen}/{estadoPantalla}")]
        public IActionResult ObtenerSeguimientoAsesor(int idAsesor, int idCategoriaOrigen, int estadoPantalla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idAsesor <= 0)
            {
                return BadRequest("El Id del Asesor no Existe.");
            }
            if (idCategoriaOrigen <= 0)
            {
                return BadRequest("El Id de la Categoria Origen no Existe.");
            }
            if (estadoPantalla < 0 || estadoPantalla > 2)
            {
                return BadRequest("El Estado Pantalla no Existe.");
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerSeguimientoAsesor(idAsesor, idCategoriaOrigen, estadoPantalla));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los documentos asociados a una Actividad Detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDocumentosPorIdActividadDetalle/{idActividadDetalle}")]
        public IActionResult ObtenerDocumentosPorIdActividadDetalle(int idActividadDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerDocumentosPorIdActividadDetalle(idActividadDetalle));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Datos del Alumno
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDatosAlumno/{idClasificacionPersona}/{idOportunidad}/{idPersonal}")]
        public IActionResult ObtenerDatosAlumno(int idClasificacionPersona, int idOportunidad, int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerDatosAlumno(idClasificacionPersona, idOportunidad, idPersonal));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene datos principales de Plantillas asociadas a WhatsApp para la Agenda.
        /// </summary>
        /// <returns> List<PlantillaMailingAgendaDTO> </returns>
        [HttpGet("ObtenerPlantillaWhatsApp")]
        public IActionResult ObtenerPlantillaWhatsApp()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerPlantillaWhatsApp());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la Probabilidad de Sueldo asociado a la Oportunidad y Pais.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProbabilidadSueldoOportunidad/{idOportunidad}/{idPais}")]
        public IActionResult ObtenerProbabilidadSueldoOportunidad(int idOportunidad, int idPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerProbabilidadSueldoOportunidad(idOportunidad, idPais));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas Versión 1
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramav1")]
        public IActionResult ObtenerInformacionProgramav1([FromBody] Dictionary<string, string> filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerInformacionProgramav1(filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos
        /// Fecha: 13/02/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene configuraciones necesarias para Agenda.
        /// </summary>
        /// <returns>Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerConfiguraciones")]
        public ActionResult ObtenerConfiguraciones()
        {
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerConfiguraciones());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 22/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene plantillas disponibles por fase
        /// </summary>
        /// <param name="idFaseOportunidad">Id de Fase de Oportunidad</param>
        /// <param name="idPersonalAreaTrabajo">Id de Arera de Trabajo de Personal</param>
        /// <returns> List<FiltroDTO> </returns>
        [Route("[action]/{idFaseOportunidad}/{idPersonalAreaTrabajo}")]
        [HttpGet]
        public IActionResult ObtenerPlantillaPorFase(int idFaseOportunidad, int idPersonalAreaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerPlantillaPorFase(idFaseOportunidad, idPersonalAreaTrabajo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Valida si existe una oportunidad en seguimiento para el mismo centro costo
        /// </summary>
        /// <param name="idContacto">Id Contacto</param>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ValidarRN2/{idContacto}/{idCentroCosto}/{idOcurrencia}")]
        public IActionResult ValidarRN2(int idContacto, int idCentroCosto, int idOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ValidarRN2(idContacto, idCentroCosto, idOcurrencia));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Hoja de Actividades por Ocurrencia Alterno
        /// </summary>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerHojaActividadesPorIdOcurrenciaAlterno/{idOcurrencia}")]
        public IActionResult ObtenerHojaActividadesPorIdOcurrenciaAlterno(int idOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerHojaActividadesPorIdOcurrenciaAlterno(idOcurrencia));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Versión: 1.1
        /// <summary>
        /// Actualiza Información de Sentinel por Alumno
        /// </summary>
        /// <param name="dni">DNI de la Persona</param>
        /// <param name="idContacto">Id del Contacto</param>
        /// <param name="usuario">Usuario de modificacion</param>
        /// <returns> Retorna true o false </returns>
        [HttpGet("ActualizarSentinelAlumno/{dni}/{idContacto}/{usuario}")]
        public IActionResult ActualizarSentinelAlumno(string dni, int idContacto, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ActualizarSentinelAlumno(dni, idContacto, usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Sentinel para la cabecera de agenda
        /// </summary>
        /// <param name="idAlumno">Id de Ocurrencia</param>
        /// <returns> SentinelDatosCabeceraDTO </returns>
        [HttpGet("ObtenerSemaforoSentinelAlumno/{idAlumno}")]
        public IActionResult ObtenerSemaforoSentinelAlumno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerSemaforoSentinelAlumno(idAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Información de Tiempo de Capacitación
        /// </summary>
        /// <param name="oportunidadTiempoCapacitacionDTO">Id de Ocurrencia</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarTiempoCapacitacion")]
        public IActionResult ActualizarTiempoCapacitacion([FromBody] OportunidadTiempoCapacitacionDTO oportunidadTiempoCapacitacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ActualizarTiempoCapacitacion(oportunidadTiempoCapacitacionDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Datos Personales Alumno
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <param name="areaTrabajo">Area de Trabajo del Personal</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarAlumno/{usuario}/{areaTrabajo}")]
        public IActionResult ActualizarAlumno([FromBody] AlumnoActualizarDTO alumno, string usuario, string areaTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ActualizarAlumno(alumno, usuario, areaTrabajo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/08/20222
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Sentinel por Alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno </param>
        /// <returns> Información de Sentinel por Alumno </returns>
        /// <returns> objetoDTO :  SentinelDatosContactoDTO </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerDatoSentinelAlumno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerDatoSentinelAlumno(idAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas
        /// </summary>
        /// <param name="filtros">Filtros de busqueda </param>
        /// <returns> Lista de objeto DTO de resumen de programas </returns>
        /// <returns> Lista objeto DTO : CargarInformacionProgramaRespuestaDTO </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerInformacionPrograma([FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerInformacionPrograma(filtros));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas V2
        /// </summary>
        /// <param name="filtros">Filtros de busqueda</param>
        /// <returns> Lista de objeto DTO de resumen de programas V2 </returns>
        /// <returns> Lista objeto DTO : List<ResumenProgramaV2DTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerResumenProgramasV2([FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerResumenProgramasV2(filtros));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico con la ultima interaccion
        /// </summary>
        /// <param name="correoReceptor">Correo del receptor </param>
        /// <param name="messageId">Id del Message-Correo </param>
        /// <returns> Documentos Whatsapp por Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<CorreoInteraccionesAlumnoDTO> </returns>
        [Route("[Action]/{correoReceptor}/{messageId}")]
        [HttpGet]
        public ActionResult ObtenerCorreosEnviadosSpeech(string correoReceptor, string messageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerCorreosEnviadosSpeech(correoReceptor, messageId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 06/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtener Competidores Registrados
        /// </summary>
        /// <returns> Lista de competidores Registrados </returns>
        /// <returns> Lita de objeto DTO : List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCompetidores()
        {
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerCompetidores());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Devuelve la informacion de los documentos subidos por oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerDocumentosPorIdOportunidad(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerDocumentosPorIdOportunidad(idOportunidad));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Speech Bienvenida y Despedida por actividad detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la ActividadDetalle </param>
        /// <returns> Speech Bienvenida y Despedida por actividad detalle </returns>
        /// <returns> Objeto DTO : SpeechBienvenidaDespedidaDTO </returns>
        [Route("[action]/{idActividadDetalle}")]
        [HttpGet]
        public ActionResult ObtenerIdSpeechBienvenidaDespedida(int idActividadDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerIdSpeechBienvenidaDespedida(idActividadDetalle));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 14/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el registro de configuración de contacto de acuerdo al IdTipoDato.
        /// </summary>
        /// <param name="idTipoDato">Id del Tipo Dato</param>
        /// <returns> Confirmación de envío </returns>
        /// <returns> objeto DTO : ContactoConfiguracionDTO </returns>
        [Route("[Action]/{idTipoDato}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionContacto(int idTipoDato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerConfiguracionContacto(idTipoDato));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 14/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el primer registro de configuración de contacto de acuerdo al IdTipoDato.
        /// </summary>
        /// <returns> Confirmación de envío </returns>
        /// <returns> objeto DTO : ReferidoConfiguracionDTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionReferidos()
        {
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerConfiguracionReferidos());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Campo fechaFinalizacion de Matricula
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la MatriculaCabecera </param>
        /// <returns> FechaFinalizacion de Matricula </returns>
        /// <returns> String </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerFechaFinalizacionMatricula(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerFechaFinalizacionMatricula(idMatriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cantidad de Registros de T_MatriculaCabeceraDatosCertificadoMensajes basado en un UserName.
        /// </summary>
        /// <param name="userName">Username de AspNetUsers</param>
        /// <returns> int </returns>
        [HttpGet("[Action]/{userName}")]
        public IActionResult ObtenerCantidadMensajesPorUsername(string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerCantidadMensajesPorUsername(userName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 08/09/2022
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns> Objeto DTO </returns>
        /// <returns> objetoDTO: SeguimientoAsesorDTO </returns>
        /// [Route("[Action]/{numero}/{apellido}/{usuario}")]
        [Route("[Action]/{documento}/{idAlumno}/{usuario}")]
        [HttpGet]
        public ActionResult ActualizarInformacionDataCredito(string documento, int idAlumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ActualizarInformacionDataCredito(documento, idAlumno, usuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PGeneral para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerComboProgramaGeneral()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.ObtenerComboProgramaGeneral());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Hace el envio de sms a los contactos por reprogramacion automatica por dia y ocurrencia
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> bool </returns>
        [HttpGet("EnviarIndividualSMSPorOcurrencia/{idOportunidad}/{idOcurrencia}")]
        public IActionResult EnviarIndividualSMSPorOcurrencia(int idOportunidad, int idOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadService.EnviarIndividualSMSPorOcurrencia(idOportunidad, idOcurrencia));
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 28/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera plantilla para envio WhatsApp.
        /// </summary>
        /// <returns> ObjetoDTO: PlantillaWhatsAppCalculadoDTO </returns>
        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerValoresEtiquetaWhatsapp(int idOportunidad)
        {
            IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
            var resultado = agendaActividadService.ObtenerValoresEtiquetaWhatsapp(idOportunidad);
            return Ok(resultado);
        }

        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 12/03/2024
        /// Version: 1.0
        /// <summary>
        /// Conteo de actividades totales, ejecutadas, its generados, ips generados
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns> ControlActividadAgendaDTO </returns>
        [Route("[Action]/{idAsesor}")]
        [HttpGet]
        public ActionResult<ControlActividadAgendaDTO> ObtenerReporteControlActividadesAgenda(int idAsesor)
        {
            IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
            var resultado = agendaActividadService.ObtenerReporteControlActividadesAgenda(idAsesor);
            return Ok(resultado);
        }


        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 28/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene métricas comparativas diarias de actividades de un asesor
        /// </summary>
        /// <param name="idAsesor">ID del asesor</param>
        /// <param name="fecha">Fecha opcional para consultar (por defecto: día actual)</param>
        /// <returns>
        /// Métricas del día especificado comparadas con el día anterior:
        /// - TotalActividades, Ejecutadas, ItsGenerados, IpsGenerados
        /// - Cada métrica incluye: valores de hoy y ayer, porcentaje comparativo y estado (Incremento/Estable/Decremento)
        /// - El porcentaje representa: (hoy / ayer) * 100
        /// - Estados: Incremento (>110%), Estable (90-110%), Decremento (<90%)
        /// </returns>
        /// <remarks>
        /// Uso:
        /// - Consulta datos de HOY usando ObtenerReporteControlActividadesFecha
        /// - Consulta datos de AYER desde T_ActividadesAsesorHistorico
        /// - Calcula porcentaje como: hoy es el X% de lo que hizo ayer
        ///
        /// Ejemplos de interpretación:
        /// - 125% = Hoy hizo 25% más que ayer
        /// - 100% = Mismo rendimiento que ayer
        /// - 12% = Hoy solo hizo el 12% de lo que hizo ayer
        /// </remarks>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerMetricasComparativasDiarias([FromQuery] int idAsesor, [FromQuery] DateTime? fecha = null)
        {
            try
            {
                if (idAsesor <= 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        mensaje = "El ID del asesor debe ser mayor a 0"
                    });
                }

                var fechaConsulta = fecha ?? DateTime.Today;
                if (fechaConsulta.Date > DateTime.Today)
                {
                    return BadRequest(new
                    {
                        success = false,
                        mensaje = "No se pueden consultar métricas de fechas futuras",
                        fecha = fechaConsulta.ToString("yyyy-MM-dd")
                    });
                }

                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);

                var resultado = agendaActividadService.ObtenerMetricasComparativasDiarias(idAsesor, fechaConsulta);

                return Ok(resultado);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(new
                {
                    success = false,
                    mensaje = argEx.Message,
                    error = "Validación de parámetros"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    mensaje = "Error al obtener métricas comparativas diarias",
                    error = ex.Message,
                    errorCode = "#AAC-OMCD-001"
                });
            }
        }

        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 25/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las métricas comparativas de actividades ATC de un personal entre el día actual y el anterior
        /// </summary>
        /// <param name="idPersonal">ID del personal a consultar</param>
        /// <param name="fecha">Fecha opcional a consultar (por defecto: hoy)</param>
        /// <returns>Métricas comparativas con porcentajes de variación</returns>
        /// GET: api/Comercial/AgendaActividad/ObtenerMetricasActividadesATC?idPersonal=6588&fecha=2026-02-20
        [HttpGet]
        [Route("ObtenerMetricasActividadesATC")]
        public IActionResult ObtenerMetricasActividadesATC([FromQuery] int idPersonal, [FromQuery] DateTime? fecha = null)
        {
            try
            {
                // Validación: idPersonal debe ser mayor a 0
                if (idPersonal <= 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        mensaje = "El ID del personal debe ser mayor a 0"
                    });
                }

                // Validación: fecha no puede ser futura
                var fechaConsulta = fecha ?? DateTime.Today;
                if (fechaConsulta.Date > DateTime.Today)
                {
                    return BadRequest(new
                    {
                        success = false,
                        mensaje = "No se pueden consultar métricas de fechas futuras",
                        fecha = fechaConsulta.ToString("yyyy-MM-dd")
                    });
                }

                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);

                var resultado = agendaActividadService.ObtenerMetricasActividadesATC(idPersonal, fechaConsulta);

                return Ok(resultado);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(new
                {
                    success = false,
                    mensaje = argEx.Message,
                    error = "Validación de parámetros"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    mensaje = "Error al obtener métricas de actividades ATC",
                    error = ex.Message,
                    errorCode = "#AAC-OMAATC-001"
                });
            }
        }

        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 01/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene los códigos de descuento asociados a un alumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Lista de códigos de descuento del alumno</returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerCodigoDescuentoAlumno([FromQuery] string idAlumno)
        {
            try
            {
                if (string.IsNullOrEmpty(idAlumno))
                {
                    return BadRequest(new
                    {
                        success = false,
                        mensaje = "El valor Id Alumno es requerido"
                    });
                }

                if (!int.TryParse(idAlumno, out int idAlumnoInt))
                {
                    return BadRequest(new
                    {
                        success = false,
                        mensaje = "El Id Alumno debe ser un número válido"
                    });
                }

                IAgendaActividadService agendaService = new AgendaActividadService(_unitOfWork);
                var resultado = agendaService.ObtenerCodigoDescuentoAlumno(idAlumnoInt);

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    mensaje = "Error al obtener códigos de descuento asociados",
                    error = e.Message,
                    errorCode = "#AAC-OCDA-001"
                });
            }
        }


        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/24/2024
        /// Version: 1.0
        /// <summary>
        /// Conteo de actividades totales, ejecutadas, its generados, ips generados
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns> ControlActividadAgendaDTO </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult<List<ResultadoBusquedaFichaAlumnoDTO>> BuscarFichaPorCelular([FromBody] string celular)
        {
            IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
            var resultado = agendaActividadService.BuscarFichaPorCelular(celular);
            return Ok(resultado);
        }

        /// TipoFuncion: GET
        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 05/27/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el idagente segun el nro de alumno
        /// </summary>
        /// <param name="celular"></param>
        /// <returns> ControlActividadAgendaDTO </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerIdSkillporCelular([FromBody] ClienteWolkVoxDTO filtro)
        {
            IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
            var resultado = agendaActividadService.ObtenerIdSkillPorCelular(filtro.Numero);
            var respuesta = new ResultadoWolkvoxDTO { IdSkill = resultado };
            return Ok(respuesta);
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/24/2024
        /// Version: 1.0
        /// <summary>
        /// Conteo de actividades totales, ejecutadas, its generados, ips generados
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns> ControlActividadAgendaDTO </returns>
        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public IActionResult ObtenerColorPerfilProgramaPorIdOportunidad(int idOportunidad)
        {
            IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
            var resultado = agendaActividadService.ObtenerColorPerfilProgramaPorIdOportunidad(idOportunidad);
            return Ok(resultado);
        }

        /// TipoFuncion: POST
        /// Autor: Junior Llerena
        /// Fecha: 29/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el centro de costo de una actividad
        /// </summary>
        /// <param name="dto">Objeto que contiene IdCentroCosto e IdActividad</param>
        /// <returns>Retorna 200 si se actualizó correctamente o 400 con mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult UpdateCentroCosto([FromBody] ActualizarCentroCostoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                var resultado = agendaActividadService.ActualizarCentroCosto(dto.IdCentroCosto, dto.IdActividad);

                if (resultado)
                {
                    return Ok(new { success = true, message = "Centro de costo actualizado correctamente" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "No se pudo actualizar el centro de costo" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 23/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de empresa paga por código de matrícula, incluyendo coordinador y asesor
        /// </summary>
        /// <param name="codigoMatricula">Código de matrícula de la oportunidad</param>
        /// <returns>Retorna información de empresa paga con indicador si es empresa, nombre de coordinador y asesor</returns>
        [HttpGet("ObtenerInformacionEmpresaPorCodigoMatricula/{codigoMatricula}")]
        public IActionResult ObtenerInformacionEmpresaPorCodigoMatricula(string codigoMatricula)
        {
            if (string.IsNullOrWhiteSpace(codigoMatricula))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "El código de matrícula es requerido y no puede estar vacío"
                });
            }

            try
            {
                IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
                var informacionEmpresa = _unitOfWork.OportunidadRepository.ObtenerEmpresaPagaPorCodigoMatricula(codigoMatricula);

                if (informacionEmpresa == null)
                {
                    return Ok(new
                    {
                        success = true,
                        esEmpresa = false,
                        nombreCoordinador = (string)null,
                        nombreAsesor = (string)null,
                        message = "No se encontró información para el código de matrícula proporcionado"
                    });
                }

                bool esEmpresa = !string.IsNullOrEmpty(informacionEmpresa.EmpresaPaga) &&
                                 informacionEmpresa.EmpresaPaga.Trim().Equals("Si", StringComparison.OrdinalIgnoreCase);

                return Ok(new
                {
                    success = true,
                    esEmpresa = esEmpresa,
                    nombreCoordinador = informacionEmpresa.NombreCoordinador,
                    nombreAsesor = informacionEmpresa.NombreAsesor,
                    message = esEmpresa ? "La oportunidad corresponde a una empresa" : "La oportunidad no corresponde a una empresa"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error al obtener información de empresa",
                    error = ex.Message,
                    errorCode = "#AAC-OIEP-001"
                });
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Vega
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el programa y la probabilidad de inscripción registrada de la oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> ColorPerfilProgramaV2DTO </returns>
        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public IActionResult ObtenerProgramaYProbabilidadV2(int idOportunidad)
        {
            IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
            return Ok(agendaActividadService.ObtenerProgramaYProbabilidad(idOportunidad));
        }

        /// TipoFuncion: GET
        /// Autor: Jose Vega
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene analisis de programas del alumno
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> AnalisisProgramasAlumnoDTO </returns>
        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public IActionResult ObtenerAnalisisProgramasAlumno(int idOportunidad)
        {
            IAgendaActividadService agendaActividadService = new AgendaActividadService(_unitOfWork);
            return Ok(agendaActividadService.ObtenerAnalisisProgramasAlumno(idOportunidad));
        }

    }
}
