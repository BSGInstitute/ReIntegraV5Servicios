using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using BSI.Integra.Servicios.Helpers.InformacionProgramaEstructurada;
using HtmlAgilityPack;
using iText.Kernel.Pdf.Statistics;
using iTextSharp.text.pdf.codec.wmf;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using static BSI.Integra.Servicios.Controllers.AgendaInformacionActividadController;
namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AgendaInformacionActividadController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/07/2022
    /// <summary>
    /// Gestión de Informacion de Actividades para Agenda
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AgendaInformacionActividadController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IAgendaInformacionActividadService _servicioPrincipal;
        public AgendaInformacionActividadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _servicioPrincipal = new AgendaInformacionActividadService(unitOfWork);
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
            var servicio = new PGeneralService(_unitOfWork);
            return Ok(servicio.ObtenerCabeceraSpeechAgenda(idOportunidad, idCentroCosto));
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
                var servicio = new PGeneralService(_unitOfWork);
                return Ok(servicio.ObtenerPublicoObjetivoProgramaParaAgenda(idCentroCosto, idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 09/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Publico Objetivo para un Programa General para la nueva version de la agenda
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo asociado al Programa</param>
        /// <param name="idOportunidad">Id de la Oportunidad asociada a las Respuestas</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerPublicoObjetivoProgramaNuevaAgendaV3/{idCentroCosto}/{idOportunidad}")]
        public IActionResult ObtenerPublicoObjetivoProgramaNuevaAgendaV3(int idCentroCosto, int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PGeneralService(_unitOfWork);
                return Ok(servicio.ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(idCentroCosto, idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Mdificado por: Jose Vega (2025-08-27) - Ajuste para retornar JSON estructurado en la respuesta.
        /// Fecha: 09/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Publico Objetivo para un Programa General para la nueva version de la agenda
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo asociado al Programa</param>
        /// <param name="idOportunidad">Id de la Oportunidad asociada a las Respuestas</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerPublicoObjetivoProgramaNuevaAgendaV3json/{idCentroCosto}/{idOportunidad}")]
        public IActionResult ObtenerPublicoObjetivoProgramaNuevaAgendaV3json(int idCentroCosto, int idOportunidad)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var servicio = new PGeneralService(_unitOfWork);

                // Resultado original
                var resultado = servicio.ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(idCentroCosto, idOportunidad);

                // Proyecta a JToken para limpieza homogénea
                var token = Newtonsoft.Json.Linq.JToken.FromObject(resultado);

                // Limpia HTML/entidades en todos los strings del grafo
                JsonSanitizerHelpers.LimpiarHtmlRecursivo(token);

                // Devuelve JSON limpio e indentado
                return Content(token.ToString(Newtonsoft.Json.Formatting.Indented), "application/json");
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
                var servicio = new ProgramaGeneralCertificacionService(_unitOfWork);
                return Ok(servicio.ObtenerCertificacionesDetalleParaAgendaPorIdOportunidad(idOportunidad));
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
                var servicio = new ProgramaGeneralMotivacionService(_unitOfWork);
                return Ok(servicio.ObtenerMotivacionesDetalleParaAgendaPorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Mdificado por: Jose Vega (2025-08-27) - Ajuste para retornar JSON estructurado en la respuesta.
        /// Fecha: 22/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las Motivaciones y sus Argumentos asociados a un Programa General.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad asociada al Programa</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerArgumentosMotivacionProgramaPorIdOportunidadjson/{idOportunidad}")]
        public IActionResult ObtenerArgumentosMotivacionProgramaPorIdOportunidadjson(int idOportunidad)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var servicio = new ProgramaGeneralMotivacionService(_unitOfWork);

                // Resultado original
                var resultado = servicio.ObtenerMotivacionesDetalleParaAgendaPorIdOportunidad(idOportunidad);

                // Proyecta a JToken para limpieza homogénea
                var token = Newtonsoft.Json.Linq.JToken.FromObject(resultado);

                // Limpia HTML/entidades en todos los strings del grafo (detalle, solucion, etc.)
                JsonSanitizerHelpers.LimpiarHtmlRecursivo(token);

                // Devuelve JSON limpio e indentado
                return Content(token.ToString(Newtonsoft.Json.Formatting.Indented), "application/json");
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
                var servicio = new OportunidadInformacionService(_unitOfWork);
                return Ok(servicio.ObtenerOportunidadInformacion(idAlumno, idClasificacionPersona));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Mdificado por: Jose Vega (2025-08-27) - Ajuste para retornar JSON estructurado en la respuesta.
        /// Fecha: 30/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la informacion de Oportunidades asociada a una ClasificacionPersona y un Alumno.
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerOportunidadInformacionjson/{idAlumno}/{idClasificacionPersona}")]
        public IActionResult ObtenerOportunidadInformacionjson(int idAlumno, int idClasificacionPersona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var servicio = new OportunidadInformacionService(_unitOfWork);

                // Resultado original
                var resultado = servicio.ObtenerOportunidadInformacion(idAlumno, idClasificacionPersona);

                // Proyecta a JToken para limpieza homogénea
                var token = Newtonsoft.Json.Linq.JToken.FromObject(resultado);

                // Limpia HTML/entidades en todos los strings del grafo
                JsonSanitizerHelpers.LimpiarHtmlRecursivo(token);

                // Devuelve JSON limpio e indentado
                return Content(token.ToString(Newtonsoft.Json.Formatting.Indented), "application/json");
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
                var servicio = new ProgramaGeneralProblemaService(_unitOfWork);
                return Ok(servicio.ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Mdificado por: Jose Vega (2025-08-27) - Ajuste para retornar JSON estructurado en la respuesta.
        /// Fecha: 25/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Problemas de ProgramaGeneral y sus Argumentos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProgramaGeneralProblemaDetallePorIdOportunidadjson/{idOportunidad}")]
        public IActionResult ObtenerProgramaGeneralProblemaDetallePorIdOportunidadjson(int idOportunidad)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var servicio = new ProgramaGeneralProblemaService(_unitOfWork);

                // Obtiene el resultado original
                var resultado = servicio.ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidad(idOportunidad);

                // Proyecta a JToken para poder limpiar HTML en todo el grafo
                var token = Newtonsoft.Json.Linq.JToken.FromObject(resultado);

                // Limpia HTML/entidades en todos los strings (detalle, solucion, etc.)
                JsonSanitizerHelpers.LimpiarHtmlRecursivo(token);

                // Devuelve JSON limpio e indentado
                return Content(token.ToString(Newtonsoft.Json.Formatting.Indented), "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 25/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Problemas de ProgramaGeneral y sus Argumentos asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProgramaGeneralProblemaDetallePorIdOportunidadNuevaAgenda/{idOportunidad}")]
        public IActionResult ObtenerProgramaGeneralProblemaDetallePorIdOportunidadNuevaAgenda(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProgramaGeneralProblemaService(_unitOfWork);
                return Ok(servicio.ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidadNuevaAgenda(idOportunidad));
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
            if (idAlumno <= 0)
            {
                return BadRequest("El identificador del Alumno debe ser mayor a 0.");
            }
            if (idPersonal <= 0)
            {
                return BadRequest("El identificador del Personal debe ser mayor a 0.");
            }
            try
            {
                var servicio = new MandrilService(_unitOfWork);
                return Ok(servicio.ObtenerCorreoInteraccionV2EnviadosPorPersonalParaAgenda(idAlumno, idPersonal));
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
            if (idOportunidad <= 0)
            {
                return BadRequest("El identificador de la Oportunidad debe ser mayor a 0.");
            }
            try
            {
                var servicio = new CompetidorService(_unitOfWork);
                return Ok(servicio.ObtenerCompetidorParaAgendaPorIdOportunidad(idOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                var servicioOportunidadCompetidor = new OportunidadCompetidorService(_unitOfWork);
                var servicioDetalleOportunidadCompetidor = new DetalleOportunidadCompetidorService(_unitOfWork);
                var servicioPrerequisito = new ProgramaGeneralPrerequisitoService(_unitOfWork);
                var servicioBeneficio = new ProgramaGeneralBeneficioService(_unitOfWork);
                var servicioBeneficioArgumento = new ProgramaGeneralBeneficioArgumentoService(_unitOfWork);

                var datosOportunidad = new OportunidadPrerequisitoBeneficioCompetidorDTO();


                datosOportunidad.OportunidadCompetidor =
                    servicioOportunidadCompetidor.ObtenerOportunidadCompetidorPorIdOportunidad(idOportunidad)?.FirstOrDefault();
                if (datosOportunidad.OportunidadCompetidor != null)
                {
                    datosOportunidad.Competidores = servicioDetalleOportunidadCompetidor.
                        ObtenerEmpresaCompetidoraPorIdOportunidadCompetidor(datosOportunidad.OportunidadCompetidor.Id).ToList();
                }

                datosOportunidad.PrerequisitosGenerales =
                    servicioPrerequisito.ObtenerProgramaGeneralPrerequisitoPorIdOportunidad(idOportunidad).ToList();

                datosOportunidad.PrerequisitosEspecificos =
                    servicioPrerequisito.ObtenerProgramaGeneralPrerequisitoEspecificoPorIdOportunidad(idOportunidad).ToList();

                var beneficios = servicioBeneficio.ObtenerProgramaGeneralBeneficioPorIdOportunidad(idOportunidad).ToList();
                datosOportunidad.Beneficios = beneficios.Select(
                    b => new ProgramaGeneralBeneficioOportunidadDetalleDTO()
                    {
                        Id = b.IdBeneficio,
                        Nombre = b.NombrePrerequisito,
                        Respuesta = b.Respuesta,
                        Completado = b.Completado,
                        Argumentos = servicioBeneficioArgumento.ObtenerProgramaGeneralBeneficioArgumentoPorIdBeneficio(b.IdBeneficio).ToList()
                    }
                ).ToList();
                return Ok(datosOportunidad);
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
                var servicio = new OportunidadLogService(_unitOfWork);
                return Ok(servicio.ObtenerHistorialComentariosPorIdOportunidad(idOportunidad));
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OportunidadLogService(_unitOfWork);
                var reporte = servicio.ObtenerReporteSeguimientoNWActividadesPorIdOportunidad(idOportunidad);
                var historial = new List<ReporteSeguimientoNWActividadDTO?>();
                historial.Add(reporte.FirstOrDefault(x => x.Estado == "NO EJECUTADO"));
                historial.AddRange(reporte.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());
                return Ok(historial);
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
        /// Obtiene la Oportunidad Compuesta y el Programa Especifico asociados a una Actividad Detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerOportunidadYPEspecificoPorIdActividadDetalle/{idActividadDetalle}")]
        public IActionResult ObtenerOportunidadYPEspecificoPorIdActividadDetalle(int idActividadDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _servicioPrincipal.ObtenerOportunidadYPEspecificoPorIdActividadDetalle(idActividadDetalle);
                return Ok(new
                {
                    resultado.Oportunidad,
                    resultado.PEspecifico
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
                InformacionProgramaService objeto = new InformacionProgramaService(_unitOfWork);
                PGeneralService pGeneralService = new PGeneralService(_unitOfWork);
                PreguntaFrecuentePGeneralService servicioPreguntaFrecuentePGeneral = new PreguntaFrecuentePGeneralService(_unitOfWork);

                var repositorioGeneral = pGeneralService.ObtenerDatosPFrecuentes(idCentroCosto);
                if (repositorioGeneral.IdPGeneral != null)
                {
                    var preguntaFrecuente = servicioPreguntaFrecuentePGeneral.ObtenerPreguntaFrecuente(repositorioGeneral);
                    var data = objeto.CargarInformacionPrograma(preguntaFrecuente);

                    ProgramaGeneralModeloCertificadoService servicioModelo = new ProgramaGeneralModeloCertificadoService(_unitOfWork);
                    var modeloCertificado = servicioModelo.ObtenerModeloCertificadoPrograma(idOportunidad);
                    return Ok(new { data, modeloCertificado });
                }
                else
                {
                    return BadRequest("El Id del Centro Costo no exite!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
            if (idAlumno <= 0)
            {
                return BadRequest("El Id del Alumno no Existe.");
            }
            try
            {
                var servicio = new AlumnoLogService(_unitOfWork);
                return Ok(servicio.ObtenerAlumnoLogParaAgendaPorIdAlumno(idAlumno));
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
                var servicioDocumentoLegal = new DocumentoLegalService(_unitOfWork);
                var alumnoService = new AlumnoService(_unitOfWork);
                var alumno = new Alumno();
                alumno = alumnoService.ObtenerPorId(idAlumno);
                if (alumno.IdCodigoPais.Value == null) { alumno.IdCodigoPais = 0; }
                var documentoLegal = servicioDocumentoLegal.ObtenerDocumentoLegalAgenda(idAreaPersonal, rol, alumno.IdCodigoPais.Value);
                return Ok(documentoLegal);
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
            if (idFaseOportunidad <= 0)
            {
                return BadRequest("El Id de la Fase Oportunidad no Existe.");
            }
            try
            {
                var servicio = new PlantillaClaveValorService(_unitOfWork);
                return Ok(servicio.ObtenerPlantillasPorIdFaseOportunidad(idFaseOportunidad));
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
                var servicio = new OportunidadMaximaPorCategoriaService(_unitOfWork);
                return Ok(servicio.ObtenerSeguimientoAsesor(idAsesor, idCategoriaOrigen, estadoPantalla));
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
                var servicio = new DocumentoAgendaService(_unitOfWork);
                return Ok(servicio.ObtenerDocumentoAgendaDetallePorIdActividadDetalle(idActividadDetalle));
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
                var alumnoService = new AlumnoService(_unitOfWork);
                var empresaService = new EmpresaService(_unitOfWork);
                var servicioSentinel = new SentinelService(_unitOfWork);
                var oportunidadService = new OportunidadService(_unitOfWork);

                var alumno = alumnoService.ObtenerInformacionAlumnoPorIdClasificacionPersona(idClasificacionPersona);

                var idTamanioEmpresa = alumno.IdEmpresa != null ?
                    empresaService.ObtenerIdTamanioEmpresaPorIdEmpresa(alumno.IdEmpresa!.Value) : null;

                var probabilidadsueldo = servicioSentinel.ObtenerSueldoPromedio(new SueldoPromedioArgumentosDTO
                {
                    IdEmpresa = alumno.IdEmpresa,
                    Dni = alumno.DNI,
                    IdCargo = alumno.IdCargo,
                    IdIndustria = alumno.IdIndustria,
                    IdTamanioEmpresa = idTamanioEmpresa?.Valor
                });

                var VisualizarDatos = oportunidadService.ValidarVisualizarAgendaPorIdOportunidad(idOportunidad, idPersonal) ??
                    new ResultadoVisualizarOportunidadDTO()
                    {
                        Id = 0,
                        FechaVisibleHasta = DateTime.Now,
                        Valor = 0
                    };

                return Ok(new { alumno, probabilidadsueldo, VisualizarDatos });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero.
        /// Fecha: 10/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Datos del Alumno para Semáforo de Chat WhatsApp
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerDatosAlumnoSemaforoChatWhatsApp/{idClasificacionPersona}")]
        public IActionResult ObtenerDatosAlumnoSemaforoChatWhatsApp(int idClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var alumnoService = new AlumnoService(_unitOfWork);
                var alumno = alumnoService.ObtenerInformacionAlumnoPorIdClasificacionPersona(idClasificacionPersona);
                return Ok(new { alumno });
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
                var servicio = new PlantillaClaveValorService(_unitOfWork);
                return Ok(servicio.ObtenerPlantillaWhatsAppAgenda());
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
        [HttpGet("ObtenerPlantillaWhatsAppComercial")]
        public IActionResult ObtenerPlantillaWhatsAppComercial()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PlantillaClaveValorService(_unitOfWork);
                return Ok(servicio.ObtenerPlantillaWhatsAppAgendaComercial());
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
        public IActionResult ObtenerProbabilidadSueldo(int idOportunidad, int idPais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new MontoPagoService(_unitOfWork);
                return Ok(servicio.ObtenerProbabilidadSueldoOportunidad(idOportunidad, idPais));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Un Compuesto de Valores de Etiqueta para Remplazar en las Plantillas
        /// </summary>
        /// <param name="obtenerValorEtiqueta">ObtenerValorEtiqueta</param>
        /// <param name="idFaseOportunidad">Id de la Fase Oportunidad</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerValorEtiqueta/{idCentroCosto}/{idOportunidad}")]
        public IActionResult ObtenerValorEtiqueta(int idCentroCosto, int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var plantillaPwService = new PlantillaPwService(_unitOfWork);
                var oportunidadService
                    = new OportunidadService(_unitOfWork);
                var pGeneralTipoDescuentoService = new PGeneralTipoDescuentoService(_unitOfWork);
                var pEspecificoService = new PEspecificoService(_unitOfWork);
                var montoPagoCronogramaService = new MontoPagoCronogramaService(_unitOfWork);
                plantillaPwService.ObtenerValorEtiqueta(idCentroCosto, idOportunidad);

                var datosOportunidad = oportunidadService.ObtenerDatosCompuestosPorIdOportunidad(idOportunidad);
                string FechaInicioPrograma = "";

                var Promocion = pGeneralTipoDescuentoService.ObtenerFlagPromocion(datosOportunidad.IdPgeneral.Value, 143); // Descuento 25%
                if (Promocion != null)
                {
                    datosOportunidad.Promocion25 = Promocion.Valor;
                }

                FechaInicioPrograma = pEspecificoService.FechaInicioProgramaV2(datosOportunidad.IdPgeneral.Value, datosOportunidad.IdPespecifico.GetValueOrDefault());
                datosOportunidad.CostoTotalConDescuento = montoPagoCronogramaService.ObtenerCostoTotalConDescuento(idOportunidad);
                plantillaPwService.ObtenerDatosProgramaGeneral(datosOportunidad.IdPgeneral.Value);

                // En caso no sea necesario Eliminar
                var fechaActual = DateTime.Now;
                plantillaPwService.DatosOportunidadAlumno().DiaFechaActual = fechaActual.Day.ToString();
                plantillaPwService.DatosOportunidadAlumno().NombreMesFechaActual = fechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                plantillaPwService.DatosOportunidadAlumno().AnioFechaActual = fechaActual.Year.ToString();

                var objeto = new
                {
                    CronogramaPagos = plantillaPwService.CronogramaPagos(),
                    EatosOportunidadAlumno = plantillaPwService.DatosOportunidadAlumno(),
                    EtiquetaMontosPagoPaquetes = plantillaPwService.EtiquetaMontosPagoPaquetes(),
                    //FechaInicioPrograma = plantillaPwService.FechaInicioPrograma(),
                    ListaProblemasCausa = plantillaPwService.ListaProblemasCausa(),
                    ListaTemplateV2ReemplazoEtiqueta = plantillaPwService.ListaTemplateV2ReemplazoEtiqueta(),
                    UrlCursosRelacionados = plantillaPwService.UrlCursosRelacionados(),
                };
                return Ok(new { Objeto = objeto, datosOportunidad, FechaInicioPrograma });
                //return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 10/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Un Compuesto de Valores de Etiqueta para Remplazar en las Plantillas
        /// </summary>
        /// <param name="idCentroCosto">Id Centro Costo</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerValorEtiquetaAsync/{idCentroCosto}/{idOportunidad}")]
        public async Task<IActionResult> ObtenerValorEtiquetaAsync(int idCentroCosto, int idOportunidad)
        {
            try
            {
                var plantillaPwService = new PlantillaPwService(_unitOfWork);
                var resultado = await plantillaPwService.CargarValoresEtiqueta(idCentroCosto, idOportunidad);
                return Ok(resultado);
            }
            catch
            {
                throw;
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
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);

                var idCentroCosto = Convert.ToInt32(filtro["idCentroCosto"]);
                var codigoPais = Convert.ToInt32(filtro["codigoPais"]);
                var idMatriculaCabecera = Convert.ToInt32(filtro["idMatriculaCabecera"]);
                var idOportunidad = Convert.ToInt32(filtro["idOportunidad"]);

                var respuesta = informacionProgramaService.CargarInformacionProgramaAutomatico(idCentroCosto, codigoPais, idMatriculaCabecera, idOportunidad);
                return Ok(new { respuesta });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información programa todo
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramaTodo")]
        public async Task<IActionResult> ObtenerInformacionProgramaTodo([FromBody] CargarInformacionProgramaTodoRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Error = "Modelo de solicitud inválido", Detalle = ModelState });
            }

            try
            {
                var servicio = new InformacionProgramaService(_unitOfWork);
                var respuesta = await servicio.CargarInformacionProgramaTodoAsync(request.idCentroCosto, request.codigoPais);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = "Error interno del servidor", Detalle = e.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información programa inversión
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramaInversion")]
        public async Task<IActionResult> ObtenerInformacionProgramaInversion([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null)
                {
                    return BadRequest(new { Error = "El cuerpo de la solicitud no puede estar vacío." });
                }

                if (!filtro.TryGetValue("idPGeneral", out var idPGeneralStr) ||
                    !int.TryParse(idPGeneralStr, out int idPGeneral))
                {
                    return BadRequest(new { Error = "El campo 'idPGeneral' es requerido y debe ser un número entero." });
                }

                int codigoPais = 604;
                if (filtro.TryGetValue("codigoPais", out var codigoPaisStr))
                {
                    if (!int.TryParse(codigoPaisStr, out codigoPais))
                    {
                        return BadRequest(new { Error = "El campo 'codigoPais' debe ser un número entero válido." });
                    }
                }

                var servicio = new InformacionProgramaService(_unitOfWork);
                var respuesta = await servicio.CargarInformacionProgramaInversionAsync(idPGeneral, codigoPais);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información programa presentación
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramaPresentacion")]
        public async Task<IActionResult> ObtenerInformacionProgramaPresentacion([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null || !filtro.TryGetValue("idPGeneral", out var idStr) ||
                    !int.TryParse(idStr, out int idPGeneral))
                {
                    return BadRequest(new { Error = "El campo 'idPGeneral' es requerido y debe ser un número entero." });
                }

                var servicio = new InformacionProgramaService(_unitOfWork);
                var respuesta = await servicio.CargarInformacionProgramaPresentacionAsync(idPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información programa público objetivo
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramaPublicoObjetivo")]
        public async Task<IActionResult> ObtenerInformacionProgramaPublicoObjetivo([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null || !filtro.TryGetValue("idPGeneral", out var idStr) ||
                    !int.TryParse(idStr, out int idPGeneral))
                {
                    return BadRequest(new { Error = "El campo 'idPGeneral' es requerido y debe ser un número entero." });
                }

                var servicio = new InformacionProgramaService(_unitOfWork);
                var respuesta = await servicio.CargarInformacionProgramaPublicoObjetivoAsync(idPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información programa duración horarios
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramaDuracionHorariosAsync")]
        public async Task<IActionResult> ObtenerInformacionProgramaDuracionHorariosAsync([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null || !filtro.TryGetValue("idPGeneral", out var idPGeneralStr))
                {
                    return BadRequest(new { Error = "El campo 'idPGeneral' es requerido." });
                }

                if (!int.TryParse(idPGeneralStr, out int idPGeneral))
                {
                    return BadRequest(new { Error = "El valor de 'idPGeneral' debe ser un número entero válido." });
                }

                var servicio = new InformacionProgramaService(_unitOfWork);
                var respuesta = await servicio.CargarInformacionProgramaDuracionHorariosAsync(idPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información programa prerrequisitos
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramaPrerrequisitosAsync")]
        public async Task<IActionResult> ObtenerInformacionProgramaPrerrequisitosAsync([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null || !filtro.TryGetValue("idPGeneral", out var idPGeneralStr))
                {
                    return BadRequest(new { Error = "El campo 'idPGeneral' es requerido." });
                }

                if (!int.TryParse(idPGeneralStr, out int idPGeneral))
                {
                    return BadRequest(new { Error = "El valor de 'idPGeneral' debe ser un número entero válido." });
                }

                var servicio = new InformacionProgramaService(_unitOfWork);
                var respuesta = await servicio.CargarInformacionProgramaPrerrequisitosAsync(idPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener estructura curricular
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerEstructuraCurricular")]
        public async Task<IActionResult> ObtenerEstructuraCurricular([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null || !filtro.TryGetValue("idPGeneral", out var idStr) ||
                    !int.TryParse(idStr, out int idPGeneral))
                {
                    return BadRequest(new { Error = "El campo 'idPGeneral' es requerido y debe ser un número entero." });
                }

                var servicio = new InformacionProgramaService(_unitOfWork);
                var respuesta = await servicio.CargarInformacionProgramaEstructuraCurricularAsync(idPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener información programa expositores
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramaExpositores")]
        public async Task<IActionResult> ObtenerInformacionProgramaExpositores([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                if (filtro == null || !filtro.TryGetValue("idPGeneral", out var idPGeneralStr))
                {
                    return BadRequest(new { Error = "El campo 'idPGeneral' es requerido." });
                }

                if (!int.TryParse(idPGeneralStr, out int idPGeneral))
                {
                    return BadRequest(new { Error = "El valor de 'idPGeneral' debe ser un número entero válido." });
                }

                var servicio = new InformacionProgramaService(_unitOfWork);
                var respuesta = await servicio.CargarInformacionProgramaExpositoresAsync(idPGeneral);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener resumen programa modalidades
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <param name="idCentroCosto">Id de Centro de Costo</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerResumenProgramaModalidades/{idPGeneral}/{idCentroCosto}")]
        public async Task<IActionResult> ObtenerResumenPrograma(int idPGeneral, int idCentroCosto)
        {
            try
            {
                if (idPGeneral <= 0 || idCentroCosto <= 0)
                {
                    return BadRequest(new { Error = "Los parámetros idPGeneral y idCentroCosto deben ser mayores a 0" });
                }

                var resultado = await _servicioPrincipal.ObtenerResumenPrograma(idPGeneral, idCentroCosto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener objetivos programa
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerObjetivosPrograma/{idPGeneral}")]
        public async Task<IActionResult> ObtenerObjetivosPrograma(int idPGeneral)
        {
            try
            {
                if (idPGeneral <= 0)
                {
                    return BadRequest(new { Error = "El parámetro idPGeneral debe ser mayor a 0" });
                }

                using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(900));
                var result = await _servicioPrincipal.GetObjetivosAsync(idPGeneral);

                Response.Headers.Add("Cache-Control", "public, max-age=1800"); // 30 minutos
                Response.Headers.Add("ETag", $"\"{idPGeneral}_{result?.Objetivos?.Count ?? 0}\"");

                return Ok(result);
            }
            catch (TaskCanceledException)
            {
                return StatusCode(408, new { Error = "Request timeout", IdPGeneral = idPGeneral });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Get beneficios programa
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("beneficios/{idPGeneral}")]
        public async Task<IActionResult> GetBeneficiosPrograma(int idPGeneral)
        {
            try
            {
                if (idPGeneral <= 0)
                {
                    return BadRequest(new { Error = "El parámetro idPGeneral debe ser mayor a 0" });
                }

                var result = await _servicioPrincipal.GetBeneficiosProgramaAsync(idPGeneral);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Get certificaciones programa
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("certificaciones/{idPGeneral}")]
        public async Task<IActionResult> GetCertificacionesPrograma(int idPGeneral)
        {
            try
            {
                if (idPGeneral <= 0)
                {
                    return BadRequest(new { Error = "El parámetro idPGeneral debe ser mayor a 0" });
                }

                var result = await _servicioPrincipal.GetCertificacionesProgramaAsync(idPGeneral);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Get metodologia programa
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("metodologia/{idPGeneral}")]
        public async Task<IActionResult> GetMetodologiaPrograma(int idPGeneral)
        {
            try
            {
                if (idPGeneral <= 0)
                {
                    return BadRequest(new { Error = "El parámetro idPGeneral debe ser mayor a 0" });
                }

                var result = await _servicioPrincipal.GetMetodologiaProgramaAsync(idPGeneral);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Get pautas complementarias programa
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("pautas-complementarias/{idPGeneral}")]
        public async Task<IActionResult> GetPautasComplementariasPrograma(int idPGeneral)
        {
            try
            {
                if (idPGeneral <= 0)
                {
                    return BadRequest(new { Error = "El parámetro idPGeneral debe ser mayor a 0" });
                }

                var result = await _servicioPrincipal.GetPautasComplementariasProgramaAsync(idPGeneral);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Get perfil profesional cliente
        /// </summary>
        /// <param name="idAlumno">Id de Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("perfil-profesional-cliente/{idAlumno}")]
        public async Task<IActionResult> GetPerfilProfesionalCliente(int idAlumno)
        {
            try
            {
                if (idAlumno <= 0)
                {
                    return BadRequest(new { Error = "El parámetro idAlumno debe ser mayor a 0" });
                }

                var result = await _servicioPrincipal.GetPerfilProfesionalClienteAsync(idAlumno);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Error interno del servidor.", Detalle = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Get detalle programa o curso
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("detalle-programa-curso-v2/{idPGeneral}")]

        public async Task<IActionResult> GetDetalleProgramaOCursoAsync(int idPGeneral)

        {

            if (idPGeneral <= 0)

                return BadRequest(new { Error = "IdPGeneral debe ser mayor a 0" });

            try

            {

                var result = await _servicioPrincipal.ObtenerSilaboPorIdAsync(idPGeneral);

                return Ok(result);

            }

            catch (Exception ex)

            {

                return StatusCode(500, new { Error = ex.Message, IdPGeneral = idPGeneral });

            }

        }


        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Mdificado por: Jose Vega (2025-08-27) - Ajuste para retornar JSON estructurado en la respuesta.
        /// Fecha: 11/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas Versión 1
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramav1json")]
        public IActionResult ObtenerInformacionProgramav1json([FromBody] Dictionary<string, string> filtro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);

                var idCentroCosto = Convert.ToInt32(filtro["idCentroCosto"]);
                var codigoPais = Convert.ToInt32(filtro["codigoPais"]);
                var idMatriculaCabecera = Convert.ToInt32(filtro["idMatriculaCabecera"]);
                var idOportunidad = Convert.ToInt32(filtro["idOportunidad"]);

                var respuesta = informacionProgramaService.CargarInformacionProgramaAutomaticojson(
                    idCentroCosto, codigoPais, idMatriculaCabecera, idOportunidad
                );

                var raiz = JObject.FromObject(respuesta);

                // NO limpiar todo aún. NormalizarEstructura ya:
                //  - parsea InformacionPrograma a JSON estructurado (si aplica)
                //  - luego aplica LimpiarHtmlRecursivo sobre el grafo resultante
                JsonSanitizerHelpers.NormalizarEstructura(raiz);

                return Content(raiz.ToString(Newtonsoft.Json.Formatting.Indented), "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
        [HttpPost("ObtenerInformacionProgramav5M")]
        public IActionResult ObtenerInformacionProgramav5M([FromBody] Dictionary<string, string> filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);

                var idCentroCosto = Convert.ToInt32(filtro["idCentroCosto"]);
                var codigoPais = Convert.ToInt32(filtro["codigoPais"]);
                var idMatriculaCabecera = Convert.ToInt32(filtro["idMatriculaCabecera"]);
                var idOportunidad = Convert.ToInt32(filtro["idOportunidad"]);

                var respuesta = informacionProgramaService.CargarInformacionProgramaAutomatico5M(idCentroCosto, codigoPais, idMatriculaCabecera, idOportunidad);
                return Ok(new { respuesta });
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
        /// Obtiene las plantillas disponibles por fase
        /// </summary>
        /// <param name="idFaseOportunidad">Id de Fase Oportunidad</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerPlantillaPorFase/{idFaseOportunidad}")]
        public IActionResult ObtenerPlantillaPorFase(int idFaseOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idFaseOportunidad <= 0)
            {
                return BadRequest("El Id FaseOportunidad no Existe");
            }
            try
            {
                var servicio = new PlantillaClaveValorService(_unitOfWork);
                return Ok(servicio.ObtenerPlantillaGenerarMensaje(idFaseOportunidad).OrderBy(p => p.Nombre));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
            if (idFaseOportunidad <= 0)
            {
                return BadRequest("El Id FaseOportunidad no Existe");
            }
            try
            {
                var plantillaClaveValorService = new PlantillaClaveValorService(_unitOfWork);
                var personalAreaTrabajoService = new PersonalAreaTrabajoService(_unitOfWork);
                var plantillaFase = plantillaClaveValorService.ObtenerPlantillaGenerarMensaje(idFaseOportunidad).ToList();
                if (!_unitOfWork.PersonalAreaTrabajoRepository.ExistePorId(idPersonalAreaTrabajo))
                {
                    return BadRequest("No existe el PersonalAreaTrabajo");
                }
                //if (idPersonalAreaTrabajo == ValorEstatico.IdPersonalAreaTrabajoOperaciones)
                if (idPersonalAreaTrabajo == 3)
                {
                    plantillaFase.AddRange(plantillaClaveValorService.ObtenerPlantillaGenerarMensajeOperaciones());
                }
                return Ok(plantillaFase.OrderBy(w => w.Nombre));
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
        /// Obtiene Fecha y Hora de Reprogramación Automática
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <param name="codigoFase">Codigo de Fase</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idOportunidad}/{codigoFase}/{idOcurrencia}")]
        public IActionResult ObtenerFechaHoraActividadReprogramacionAutomatica(int idOportunidad, string codigoFase, int idOcurrencia)
        {
            try
            {
                var oportunidadService = new OportunidadService(_unitOfWork);
                var personalHorarioService = new PersonalHorarioService(_unitOfWork);
                var personalService = new PersonalService(_unitOfWork);
                var alumnoService = new AlumnoService(_unitOfWork);
                var servicioHoraReprogramacion = new HoraReprogramacionAutomaticaService(_unitOfWork);

                var datosOportunidad = oportunidadService.ObtenerDatosOportunidad(idOportunidad);

                var oportunidad = oportunidadService.ObtenerDatosParaReprogramacionAutomatica(idOportunidad);
                var horario = personalHorarioService.ObtenerHorarioAsTable(oportunidad.IdPersonalAsignado);

                var personal = personalService.ObtenerDatoPersonal(oportunidad.IdPersonalAsignado);
                var alumno = alumnoService.ObtenerDatosAlumnoPorId(datosOportunidad == null ? 0 : datosOportunidad.IdAlumno);

                try
                {
                    //si el asesor es de peru o colombia y el dato es de chile le resto 2 horas a la hora de salida
                    if ((personal.CodigoPaisDiferenciaHoraria == 51 || personal.CodigoPaisDiferenciaHoraria == 57) && alumno.IdCodigoPais == 56)
                    {
                        foreach (var dia in horario)
                        {
                            if (dia[3] != null)
                            {
                                TimeSpan tiempo = new TimeSpan(2, 0, 0);
                                //hora_inicio = hora_inicio.Value.Add(tiempo);
                                dia[3] = dia[3].Value.Add(-tiempo);
                            }
                            else if (dia[1] != null)
                            {
                                TimeSpan tiempo = new TimeSpan(2, 0, 0);
                                //hora_inicio = hora_inicio.Value.Add(tiempo);
                                dia[1] = dia[1].Value.Add(-tiempo);
                            }
                        }
                    }
                }
                catch (Exception e) { }

                var respuesta = servicioHoraReprogramacion.ObtenerFechaHoraActividadReprogramacionAutomatica(
                    oportunidad.IdActividadCabeceraUltima,
                    oportunidad.IdCategoriaOrigen,
                    oportunidad.IdPersonalAsignado,
                    codigoFase,
                    idOcurrencia,
                    horario);

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                throw;
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
                var servicio = new OcurrenciaService(_unitOfWork);

                return Ok(servicio.ObtenerHojaActividadesPorIdOcurrenciaAlterno(idOcurrencia));
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
                var servicioSentinel = new SentinelService(_unitOfWork);
                var servicioEstandarItem = new SentinelSdtEstandarItemService(_unitOfWork);
                var servicioInfGen = new SentinelSdtInfGenService(_unitOfWork);
                var servicioLincreItem = new SentinelSdtLincreItemService(_unitOfWork);
                var servicioPoshis = new SentinelSdtPoshisItemService(_unitOfWork);
                var servicioRepLeg = new SentinelRepLegItemService(_unitOfWork);
                var servicioRepSBS = new SentinelSdtRepSbsitemService(_unitOfWork);
                var servicioResVen = new SentinelSdtResVenItemService(_unitOfWork);
                var alumnoService = new AlumnoService(_unitOfWork);

                var resultadoSentinel = servicioSentinel.ObtenerIdSentinelPorDni(dni);
                var idSentinel = 0;
                if (resultadoSentinel != null && resultadoSentinel.Valor != null)
                {
                    idSentinel = resultadoSentinel.Valor.Value;
                }
                var alumno = alumnoService.ObtenerPorId(idContacto);
                SentinelDTO sentinel = new SentinelDTO();

                bool rpta = false;
                bool estado = true;
                if (idSentinel != null && idSentinel > 0)
                {
                    sentinel = servicioSentinel.ObtenerSentinelPorDni(dni);
                    var sentinelSdtEstandarItem = servicioEstandarItem.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtInfGen = servicioInfGen.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtLincreItem = servicioLincreItem.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtPoshisItem = servicioPoshis.ObtenerPorIdSentinel(idSentinel);
                    var sentinelRepLegItem = servicioRepLeg.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtRepSBSItem = servicioRepSBS.ObtenerPorIdSentinel(idSentinel);
                    var sentinelSdtResVenItem = servicioResVen.ObtenerPorIdSentinel(idSentinel);

                    if (sentinelSdtEstandarItem != null && sentinelSdtEstandarItem.Count() > 0) { servicioEstandarItem.Delete(sentinelSdtEstandarItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtInfGen != null && sentinelSdtInfGen.Count() > 0) { servicioInfGen.Delete(sentinelSdtInfGen.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtLincreItem != null && sentinelSdtLincreItem.Count() > 0) { servicioLincreItem.Delete(sentinelSdtLincreItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtPoshisItem != null && sentinelSdtPoshisItem.Count() > 0) { servicioPoshis.Delete(sentinelSdtPoshisItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelRepLegItem != null && sentinelRepLegItem.Count() > 0) { servicioRepLeg.Delete(sentinelRepLegItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtRepSBSItem != null && sentinelSdtRepSBSItem.Count() > 0) { servicioRepSBS.Delete(sentinelSdtRepSBSItem.Select(p => p.Id).ToList(), usuario); }
                    if (sentinelSdtResVenItem != null && sentinelSdtResVenItem.Count() > 0) { servicioResVen.Delete(sentinelSdtResVenItem.Select(p => p.Id).ToList(), usuario); }
                    /* Estado = 1 */
                    var resultadoActualizar = servicioSentinel.ActualizarSentinelAlumno(dni, usuario);
                    if (resultadoActualizar.DatosGenerales.Dni == "")
                    {
                        estado = false;
                    }
                    if (estado)
                    {
                        alumno.Dni = dni;
                        alumno = alumnoService.ValidarEstadoContactoWhatsAppTemporalAlterno(alumno);
                        if (resultadoActualizar.DatosGenerales != null)
                        {
                            alumno.FechaNacimiento = resultadoActualizar.DatosGenerales.FechaNacimiento;
                        }

                        alumno.IdEmpresa = (alumno.IdEmpresa == 0 || alumno.IdEmpresa == -1) ? null : alumno.IdEmpresa;
                        alumnoService.Update(alumno);
                        var entidadSentinel = servicioSentinel.MapeoEntidadDesdeDTO(sentinel);
                        entidadSentinel.SentinelRepLegItems = servicioRepLeg.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Cargo);
                        entidadSentinel.SentinelSdtEstandarItems = servicioEstandarItem.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DniRuc);
                        entidadSentinel.SentinelSdtInfGens = servicioInfGen.MapeoEntidadesDesdeListaDTO(new SentinelSdtInfGenDTO[] { resultadoActualizar.DatosGenerales }.ToList());
                        entidadSentinel.SentinelSdtLincreItems = servicioLincreItem.MapeoEntidadesDesdeListaDTO(resultadoActualizar.LineaCredito);
                        entidadSentinel.SentinelSdtPoshisItems = servicioPoshis.MapeoEntidadesDesdeListaDTO(resultadoActualizar.PosicionHistoria);
                        entidadSentinel.SentinelSdtRepSbsitems = servicioRepSBS.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Deuda);
                        entidadSentinel.SentinelSdtResVenItems = servicioResVen.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DatosVencidas);
                        entidadSentinel.Dni = dni;
                        entidadSentinel.UsuarioModificacion = usuario;
                        entidadSentinel.FechaModificacion = DateTime.Now;
                        servicioSentinel.Update(entidadSentinel);
                        rpta = true;
                    }
                }
                else
                {
                    sentinel.Dni = dni;
                    sentinel.Estado = true;
                    sentinel.UsuarioCreacion = usuario;
                    sentinel.UsuarioModificacion = usuario;
                    sentinel.FechaCreacion = DateTime.Now;
                    sentinel.FechaModificacion = DateTime.Now;
                    var resultadoActualizar = servicioSentinel.ActualizarSentinelAlumno(dni, usuario);
                    if (resultadoActualizar.DatosGenerales.Dni == "")
                    {
                        estado = false;
                        //return BadRequest("El numero de DNI a consultar es invalido o no esta registrado en sentinel");
                    }
                    if (estado)
                    {
                        alumno = alumnoService.ValidarEstadoContactoWhatsAppTemporalAlterno(alumno);
                        if (resultadoActualizar.DatosGenerales != null)
                        {
                            alumno.FechaNacimiento = resultadoActualizar.DatosGenerales.FechaNacimiento;
                            alumno.UsuarioModificacion = usuario;
                            alumno.FechaModificacion = DateTime.Now;
                        }
                        alumno.Dni = dni;
                        alumnoService.Update(alumno);
                        var entidadSentinel = servicioSentinel.MapeoEntidadDesdeDTO(sentinel);
                        entidadSentinel.SentinelRepLegItems = servicioRepLeg.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Cargo);
                        entidadSentinel.SentinelSdtEstandarItems = servicioEstandarItem.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DniRuc);
                        entidadSentinel.SentinelSdtInfGens = servicioInfGen.MapeoEntidadesDesdeListaDTO(new SentinelSdtInfGenDTO[] { resultadoActualizar.DatosGenerales }.ToList());
                        entidadSentinel.SentinelSdtLincreItems = servicioLincreItem.MapeoEntidadesDesdeListaDTO(resultadoActualizar.LineaCredito);
                        entidadSentinel.SentinelSdtPoshisItems = servicioPoshis.MapeoEntidadesDesdeListaDTO(resultadoActualizar.PosicionHistoria);
                        entidadSentinel.SentinelSdtRepSbsitems = servicioRepSBS.MapeoEntidadesDesdeListaDTO(resultadoActualizar.Deuda);
                        entidadSentinel.SentinelSdtResVenItems = servicioResVen.MapeoEntidadesDesdeListaDTO(resultadoActualizar.DatosVencidas);
                        servicioSentinel.Add(entidadSentinel);
                        rpta = true;
                    }
                }
                return Ok(new { rpta, idSentinel = sentinel.Id, estado });
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
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerSemaforoSentinelAlumno/{idAlumno}")]
        public IActionResult ObtenerSemaforoSentinelAlumno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idAlumno <= 0)
            {
                return BadRequest("El Id del Alumno no Existe");
            }
            try
            {
                var servicioSentinel = new SentinelService(_unitOfWork);
                var datosSentinel = servicioSentinel.ObtenerDatosAlumnoSentinel(idAlumno);
                var cabecera = new SentinelDatosCabeceraDTO();
                if (datosSentinel != null)
                {
                    cabecera = servicioSentinel.ObtenerCabeceraSentinel(datosSentinel.IdSentinel.Value);
                }
                return Ok(cabecera);
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
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                Oportunidad oportunidad = oportunidadService.ObtenerPorId(oportunidadTiempoCapacitacionDTO.Id);
                oportunidad.IdTiempoCapacitacionValidacion = oportunidadTiempoCapacitacionDTO.IdTiempoCapacitacionValidacion ?? 0;
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.UsuarioModificacion = oportunidadTiempoCapacitacionDTO.Usuario;
                oportunidadService.Update(oportunidad);
                return Ok(true);
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
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                return Ok(alumnoService.ActualizarAlumno(alumno, usuario, areaTrabajo));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualizar el Area de Formacion
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarAFormacion/{usuario}")]
        public IActionResult ActualizarAFormacion([FromBody] AlumnoPerfilActualizarDTO alumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.ActualizarAlumnoAFormacion(alumno.IdAlumno, alumno.idNuevo, usuario);
                return Ok(new { rpta = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 05/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualizar el Cargo
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarCargo/{usuario}")]
        public IActionResult ActualizarCargo([FromBody] AlumnoPerfilActualizarDTO alumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.ActualizarAlumnoCargo(alumno.IdAlumno, alumno.idNuevo, usuario);
                return Ok(new { rpta = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 05/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualizar la Industria
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarIndustria/{usuario}")]
        public IActionResult ActualizarIndustria([FromBody] AlumnoPerfilActualizarDTO alumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.ActualizarAlumnoIndustria(alumno.IdAlumno, alumno.idNuevo, usuario);
                return Ok(new { rpta = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 05/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualizar el Area de Trabajo
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarATrabajo/{usuario}")]
        public IActionResult ActualizarATrabajo([FromBody] AlumnoPerfilActualizarDTO alumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.ActualizarAlumnoAreaTrabajo(alumno.IdAlumno, alumno.idNuevo, usuario);
                return Ok(new { rpta = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 05/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Empresa
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarEmpresa/{usuario}")]
        public IActionResult ActualizarEmpresa([FromBody] AlumnoPerfilActualizarDTO alumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.ActualizarAlumnoEmpresa(alumno.IdAlumno, alumno.idNuevo, usuario);
                return Ok(new { rpta = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 05/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Tamaño Empresa
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarTamanioEmpresa/{usuario}")]
        public IActionResult ActualizarTamanioEmpresa([FromBody] AlumnoPerfilActualizarDTO alumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.ActualizarAlumnoTamanioEmpresaAgenda(alumno.IdAlumno, alumno.idNuevo, usuario);
                return Ok(new { rpta = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 05/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Tiempo Experiencia
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarTiempoExperiencia/{usuario}")]
        public IActionResult ActualizarTiempoExperiencia([FromBody] AlumnoPerfilActualizarDTO alumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.ActualizarAlumnoExperiencia(alumno.IdAlumno, alumno.idNuevo, usuario);
                return Ok(new { rpta = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 05/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Principal Responsabilidad
        /// </summary>
        /// <param name="alumno">Datos del Alumno</param>
        /// <param name="usuario">Usuario que modifica al alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ActualizarPrincipalResponsabilidad/{usuario}")]
        public IActionResult ActualizarPrincipalResponsabilidad([FromBody] AlumnoPerfilActualizarDTO alumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.ActualizarAlumnoPrincipalResponsabilidad(alumno.IdAlumno, alumno.descripcion, usuario);
                return Ok(new { rpta = true });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// TipoFuncion: POST
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Beneficios Solicitados
        /// </summary>
        /// <returns> Beneficios Registrados </returns>
        /// <returns> Lista de Objeto DTO : List<BeneficioSolicitadoReporteDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerTodoInformacionBeneficioSolicitado([FromBody] FiltroBeneficiosSolicitadosPorAlumnos FiltroReporteSolicitud)
        {
            try
            {
                IMatriculaCabeceraService _repMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
                var solicitados = _repMatriculaCabecera.ObtenerTodoBeneficioSolicitado(FiltroReporteSolicitud);
                return Ok(solicitados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Beneficios Adicionales por Código de Matrícula
        /// </summary>
        /// <returns> Beneficios Datos Adicionales </returns>
        /// <returns> Lista de Objeto DTO : List<DatoAdicionalPWDTO> </returns>
        [Route("[action]/{idMatriculaCabeceraBeneficios:int}")]
        [HttpGet]
        public IActionResult ObtenerDatosAdicionalesPorCodigo(int idMatriculaCabeceraBeneficios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IMatriculaCabeceraService _repMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
                var solicitados = _repMatriculaCabecera.ObtenerDatosAdicionalesPorCodigo(idMatriculaCabeceraBeneficios);
                return Ok(solicitados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Entrega Beneficio
        /// </summary>
        /// <returns> Confirmación de Entrega: Objeto </returns>
        /// <returns> int </returns>
        [Route("[action]/{idMatriculaCabeceraBeneficio:int}/{usuario}")]
        [HttpGet]
        public IActionResult EntregarBeneficio(int idMatriculaCabeceraBeneficio, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IMatriculaCabeceraService _repMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);

                var beneficiosmatricula = _repMatriculaCabecera.EntregarBeneficio(idMatriculaCabeceraBeneficio, usuario);

                return Ok(beneficiosmatricula);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
            if (idAlumno <= 0)
            {
                return BadRequest("El Id del Alumno no Existe");
            }
            try
            {
                var sentinelService = new SentinelService(_unitOfWork);
                SentinelDatosContactoDTO datosSentinel = new SentinelDatosContactoDTO();
                var sentinelSdtLincreItemService = new SentinelSdtLincreItemService(_unitOfWork);
                var sentinelSdtRepSbsitemService = new SentinelSdtRepSbsitemService(_unitOfWork);
                datosSentinel = sentinelService.ObtenerDatosAlumnoSentinel(idAlumno);
                if (datosSentinel != null)
                {
                    datosSentinel.lineaCredito = sentinelSdtLincreItemService.ObtenerLineaCreditoPorIdSentinel(datosSentinel.IdSentinel.Value);
                    datosSentinel.lineaDeuda = sentinelSdtRepSbsitemService.ObtenerLineaDeudaVigente(datosSentinel.IdSentinel.Value);
                    datosSentinel.lineaDeudaVencida = sentinelSdtRepSbsitemService.ObtenerLineaDeudaVencida(datosSentinel.IdSentinel.Value);
                }
                return Ok(datosSentinel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Sentinel por Alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno </param> 
        /// <returns> Información de Sentinel por Alumno </returns>
        /// <returns> objetoDTO :  SentinelDatosContactoDTO </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerDatoSentinelAlumnoAlterno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idAlumno <= 0)
            {
                return BadRequest("El Id del Alumno no Existe");
            }
            var sentinelService = new SentinelService(_unitOfWork);
            var sentinelSdtLincreItemService = new SentinelSdtLincreItemService(_unitOfWork);
            var sentinelSdtRepSbsitemService = new SentinelSdtRepSbsitemService(_unitOfWork);
            var datosSentinel = new SentinelDatosContactoDTO();
            datosSentinel = sentinelService.ObtenerDatosAlumnoSentinel(idAlumno);
            if (datosSentinel != null)
            {
                datosSentinel.lineaCredito = sentinelSdtLincreItemService.ObtenerLineaCreditoPorIdSentinel(datosSentinel.IdSentinel.Value);
                datosSentinel.lineaDeuda = sentinelSdtRepSbsitemService.ObtenerLineaDeudaVigente(datosSentinel.IdSentinel.Value);
                datosSentinel.lineaDeudaVencida = sentinelSdtRepSbsitemService.ObtenerLineaDeudaVencida(datosSentinel.IdSentinel.Value);
            }
            return Ok(datosSentinel);
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
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                var idPGeneral = Convert.ToInt32(filtros["idPGeneral"]);
                var idCodigoPais = Convert.ToInt32(filtros["codigoPais"]);
                var respuesta = informacionProgramaService.CargarInformacionPrograma(idPGeneral, idCodigoPais, 0, 0);

                return Ok(new { respuesta.InformacionPrograma });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerInformacionProgramaRefresh([FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                var idPGeneral = Convert.ToInt32(filtros["idPGeneral"]);
                var idCodigoPais = Convert.ToInt32(filtros["codigoPais"]);
                var respuesta = informacionProgramaService.CargarInformacionProgramaSpeech(idPGeneral, idCodigoPais, 0, 0);

                return Ok(new { respuesta.InformacionPrograma });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Juan D. Huanaco Quispe.
        /// Fecha: 15/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Genera JSON de la secciones de un programa
        /// </summary>
        /// <param name="filtros">Filtros de busqueda </param>
        /// <returns> Lista de objeto DTO de resumen de programas </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerInformacionProgramaV2([FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                var idPGeneral = Convert.ToInt32(filtros["idPGeneral"]);
                var idCodigoPais = Convert.ToInt32(filtros["codigoPais"]);
                var respuesta = informacionProgramaService.CargarInformacionProgramaV2(idPGeneral, idCodigoPais);

                return Ok(new { respuesta });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Juan D. Huanaco Quispe.
        /// Fecha: 15/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los cursos y programas disponibles tal cual en PW por codigo pais (Solo incluye aquellos con categorias Programa o Curso)
        /// </summary>
        /// <param name="filtros">Filtros de busqueda </param>
        /// <returns> Lista de ProgramasYCursosPorCodigoPaisComboDTO </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerProgramasPorCodigoPais([FromBody] Dictionary<string, int> filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                var idCodigoPais = filtros["codigoPais"];
                var respuesta = informacionProgramaService.ObtenerProgramasPorCodigoPais(idCodigoPais);

                return Ok(respuesta);
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
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);
                return Ok(informacionProgramaService.CargarResumenProgramasV2(filtros));
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
                var mandrilService = new MandrilService(_unitOfWork);
                return Ok(mandrilService.VerCorreoAlumnoSpeech(correoReceptor, messageId));
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
                var empresaService = new EmpresaService(_unitOfWork);
                return Ok(empresaService.ObtenerTodoCompetidores());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
                var matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                var rptas = matriculaCabeceraService.ObtenerFechaFinalizacion(idMatriculaCabecera);
                return Ok(new { rptas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 07/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el CodigoCupon por el Id del alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno </param>
        /// <returns> Objeto DTO </returns>
        /// <returns> objetoDTO: AlumnoCuponDTO </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerCuponAlumno(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var alumnoService = new AlumnoService(_unitOfWork);
                var cuponAlumno = alumnoService.ObtenerCuponPorIdAlumno(idAlumno);
                return Ok(cuponAlumno);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico con la ultima interaccion
        /// </summary>
        /// <param name="idAlumno">Id del Alumno </param>
        /// <param name="idAsesor">Id del Asesor </param>
        /// <param name="messageId">Id del Message-correo </param>
        /// <returns> Documentos Whatsapp por Alumno </returns>
        /// <returns> Lista de Objeto DTO : List<CorreoInteraccionesAlumnoDTO> </returns>
        [Route("[Action]/{idAlumno}/{idAsesor}/{messageId}")]
        [HttpGet]
        public ActionResult ObtenerInteraccionesCorreosEnviados(int idAlumno, int idAsesor, string messageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (idAlumno <= 0 || idAsesor <= 0)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var mandrilService = new MandrilService(_unitOfWork);
                return Ok(mandrilService.ListaInteraccionCorreoAlumno(idAlumno, idAsesor, messageId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                var documentoOportunidadService = new DocumentoOportunidadService(_unitOfWork);
                var documento = documentoOportunidadService.ObtenerDocumentosPorOportunidad(idOportunidad);
                return Ok(documento);
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
                var speechBienvenidaDespedidaDTO = new SpeechBienvenidaDespedidaDTO();
                var plantillaBaseService = new PlantillaBaseService(_unitOfWork);
                var alumnoService = new AlumnoService(_unitOfWork);


                //var idSpeechBienvenida = plantillaBaseService.ObtenerIdPorNombre("speech");
                //speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = plantillaBaseService.ObtenerIdPlantillaSpeechBienvenida(idActividadDetalle, idSpeechBienvenida.Id).IdPlantillaBienvenida;
                var alumno = alumnoService.ObtenerPorIdActividadDetalle(idActividadDetalle);

                //Por defecto PERU:
                speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1675;//para todos los casos se muestra el convenio de grabacion de contrato


                //si son nulos
                if (alumno.IdCodigoPais == null || alumno.IdCodigoPais == 0)
                {
                    alumno.IdCodigoPais = 51;
                }
                if (alumno.Modalidad == null || alumno.Modalidad == "")
                {
                    alumno.Modalidad = "Online Asincronica";
                }

                //PERU
                //PRESENCIAL//1448
                //ONLINE SINCRONICA//1671
                //ONLINE ASINCRONICA//1675
                if (alumno.IdCodigoPais == 51 && alumno.Modalidad == "Presencial")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1448;
                }
                else if (alumno.IdCodigoPais == 51 && alumno.Modalidad == "Online Sincronica")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1671;
                }
                else if(alumno.IdCodigoPais == 51 && alumno.Modalidad == "Online Asincronica")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1675;
                }


                //COLOMBIA
                //PRESENCIAL//1449
                //ONLINE SINCRONICA//1672
                //ONLINE ASINCRONICA//1676
                if (alumno.IdCodigoPais == 57 && alumno.Modalidad == "Presencial")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1449;
                }
                else if (alumno.IdCodigoPais == 57 && alumno.Modalidad == "Online Sincronica")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1672;
                }
                else if (alumno.IdCodigoPais == 57 && alumno.Modalidad == "Online Asincronica")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1676;
                }

                //MEXICO
                //PRESENCIAL//1451
                //ONLINE SINCRONICA//1673
                //ONLINE ASINCRONICA//1677
                if (alumno.IdCodigoPais == 52 && alumno.Modalidad == "Presencial")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1451;
                }
                else if (alumno.IdCodigoPais == 52 && alumno.Modalidad == "Online Sincronica")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1673;
                }
                else if (alumno.IdCodigoPais == 52 && alumno.Modalidad == "Online Asincronica")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1677;
                }

                //CHILE
                //PRESENCIAL//1518
                //ONLINE SINCRONICA//1674
                //ONLINE ASINCRONICA//1678
                if (alumno.IdCodigoPais == 56 && alumno.Modalidad == "Presencial")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1518;
                }
                else if (alumno.IdCodigoPais == 56 && alumno.Modalidad == "Online Sincronica")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1674;
                }
                else if (alumno.IdCodigoPais == 56 && alumno.Modalidad == "Online Asincronica")
                {
                    speechBienvenidaDespedidaDTO.IdPlantillaBienvenida = 1678;
                }


                var idSpeechDespedida = plantillaBaseService.ObtenerIdPorNombre("Speech Despedida");
                speechBienvenidaDespedidaDTO.IdPlantillaDespedida = plantillaBaseService.ObtenerIdPlantillaSpeechDespedida(idActividadDetalle, idSpeechDespedida.Id).IdPlantillaDespedida;

                return Ok(new { data = speechBienvenidaDespedidaDTO });
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
                var contactoConfiguracionService = new ContactoConfiguracionService(_unitOfWork);
                var resultado = contactoConfiguracionService.ObtenerConfiguracionContactoPorIdTipoDato(idTipoDato);
                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest("No se encontro dato.");
                }

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
                var referidoConfiguracionService = new ReferidoConfiguracionService(_unitOfWork);
                return Ok(referidoConfiguracionService.ObtenerConfiguracionReferidos());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Los Valores Para Etiquetas Lista de Programas
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <param name="idAreaEtiqueta">Id del area etiqueta</param>
        /// <returns> Valores Para Etiquetas </returns>
        /// <returns> string </returns>
        [Route("[Action]/{idOportunidad}/{idAreaEtiqueta}")]
        [HttpGet]
        public ActionResult ObtenerValorEtiquetaListas(int idOportunidad, int idAreaEtiqueta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var plantillaPwService = new PlantillaPwService(_unitOfWork);
                return Ok(plantillaPwService.ObtenerValorEtiquetaListas(idOportunidad, idAreaEtiqueta));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
                var alumnoService = new AlumnoService(_unitOfWork);

                var VisualizarDatos = alumnoService.InsertarSolicitudVisualizarDatosOportunidad(DTO.IdOportunidad, DTO.IdPersonal);//0:no puede ver pero puede ingresar solicitud,1:puede verlo hasta la fecha,2:no puede verlo y no puede ingresar

                if (VisualizarDatos.Valor == 1)
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
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="MaterialPEspecificoDetalle"> Parametros de entrada </param>
        /// <param name="Files"> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult SubirDocumentosOportunidad([FromForm] DocumentosOportunidadDTO MaterialPEspecificoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var materialVersionService = new MaterialVersionService(_unitOfWork);
                var documentoOportunidadService = new DocumentoOportunidadService(_unitOfWork);
                string nombreArchivotemp = "";
                string contentType = "";
                var urlArchivoRepositorio = "";

                if (MaterialPEspecificoDetalle.Files != null)
                {
                    foreach (var file in MaterialPEspecificoDetalle.Files)
                    {
                        contentType = file.ContentType;
                        nombreArchivotemp = file.FileName;
                        nombreArchivotemp = string.Concat(nombreArchivotemp);
                        urlArchivoRepositorio = materialVersionService.SubirDocumentosOportunidadRepositorio(file, file.ContentType, nombreArchivotemp);
                    }
                }
                else
                {
                    return BadRequest("Necesita adjuntar ");
                }
                var documentoOportunidad = new DocumentoOportunidad
                {
                    IdAlumno = MaterialPEspecificoDetalle.IdAlumno,
                    IdOportunidad = MaterialPEspecificoDetalle.IdOportunidad,
                    IdClasificacionPersona = MaterialPEspecificoDetalle.IdClasificacionPersona,
                    NombreArchivo = nombreArchivotemp,
                    Ruta = urlArchivoRepositorio,
                    Comentario = MaterialPEspecificoDetalle.ComentarioSubida,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = MaterialPEspecificoDetalle.NombreUsuario,
                    UsuarioModificacion = MaterialPEspecificoDetalle.NombreUsuario,
                    Estado = true,
                    IdDocumentoOportunidadTipo = MaterialPEspecificoDetalle.Tipo
                };
                var idAnterior = documentoOportunidadService.ObtenerDocOportunidadPorIdYTipo(MaterialPEspecificoDetalle.IdOportunidad, MaterialPEspecificoDetalle.Tipo);

                if (idAnterior.Valor != null)
                {
                    documentoOportunidadService.Delete(idAnterior.Id, MaterialPEspecificoDetalle.NombreUsuario);
                }
                documentoOportunidadService.Add(documentoOportunidad);
                DocumentoOportunidadInsertadoDTO resultado = new DocumentoOportunidadInsertadoDTO();
                resultado.Url = urlArchivoRepositorio;
                resultado.Comentario = MaterialPEspecificoDetalle.ComentarioSubida;
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Envía mensaje de texto a Alumno
        /// </summary>
        /// <param name="mensaje"> Parametros de envío </param>
        /// <returns> Confirmación de envío </returns>
        /// <returns> Bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarMensajeTexto([FromBody] AgendaMensajeTextoDTO mensaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var mensajeTextoService = new MensajeTextoService(_unitOfWork);
                mensajeTextoService.EnviarMensajeTexto(mensaje);
                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 29/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Información Usuario y Fecha Modificación de Alumno
        /// </summary>
        /// <param name="datosOportunidad"> Parametros de entrada datos oportundidad </param>
        /// <returns> Vacío </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult CrearOportunidadSinValidarOportunidadEnSeguimientoActualizarAlumno([FromBody] OportunidadDatosDTO datosOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var alumnoService = new AlumnoService(_unitOfWork);
                if (datosOportunidad.UltimaFechaProgramada != null)
                {
                    datosOportunidad.IdEstadoOportunidad = 6;   //ValorEstatico.IdEstadoOportunidadProgramada;
                }
                else
                {
                    datosOportunidad.IdEstadoOportunidad = 2;   //ValorEstatico.IdEstadoOportunidadNoProgramada;
                }
                var datosAlumno = alumnoService.ObtenerPorId(datosOportunidad.IdAlumno.Value);
                datosAlumno.HoraContacto = datosOportunidad.alumnoHoraDTO.HoraContacto;
                datosAlumno.HoraPeru = datosOportunidad.alumnoHoraDTO.HoraPeru;
                if (alumnoService.ExisteContacto(datosAlumno.Email1, datosAlumno.Email2, datosAlumno.Id))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        datosAlumno = alumnoService.ValidarEstadoContactoWhatsAppTemporalAlterno(datosAlumno);
                        datosAlumno.FechaModificacion = DateTime.Now;
                        datosAlumno.UsuarioModificacion = datosOportunidad.alumnoHoraDTO.Usuario;
                        alumnoService.Update(datosAlumno);
                        //Crear Oportunidad
                        scope.Complete();
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Personal Id, Nombre AutoComplete
        /// </summary>
        /// <returns> Información de Personal Id, Nombre </returns>
        /// <returns> objeto DTO : List<PersonalAutocompleteDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPersonalAutocomplete([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                PersonalService personalService = new PersonalService(_unitOfWork);
                if (filtro != null && filtro["valor"] != null)
                {
                    return Ok(personalService.CargarPersonalAutoComplete(filtro["valor"].ToString()));
                }
                return Ok(new { });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Información de Empresas Id, Nombre AutoComplete
        /// </summary>
        /// <returns> Información de Empresas Id, Nombre </returns>
        /// <returns> objeto DTO : List<ComboDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerEmpresaAutocomplete([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                EmpresaService empresaService = new EmpresaService(_unitOfWork);
                if (filtro != null && filtro["valor"] != null)
                {
                    return Ok(empresaService.CargarEmpresaAutoComplete(filtro["valor"].ToString()));
                }
                return Ok(new { });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Beneficios Solicitados por Código de Matrícula
        /// </summary>
        /// <returns> Beneficios por CodigoMatricula </returns>
        /// <returns> Lista de Objeto DTO : List<InformacionBeneficioSolicitadoDTO> </returns>
        [Route("[action]/{codigoMatricula}")]
        [HttpGet]
        public IActionResult ObtenerInformacionBeneficioSolicitado(string codigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                var solicitados = matriculaCabeceraService.ObtenerBeneficiosSolicitadosPorMatricula(codigoMatricula);
                return Ok(solicitados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Beneficios por Código de Matrícula
        /// </summary>
        /// <returns> Beneficios por CodigoMatricula </returns>
        /// <returns> Objeto DTO : CorrespondeBeneficiosDTO </returns>
        [Route("[action]/{codigoMatricula}")]
        [HttpGet]
        public IActionResult ObtenerBeneficiosPorMatricula(string codigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                var beneficios = matriculaCabeceraService.ObtenerBeneficiosPorMatricula(codigoMatricula);
                return Ok(beneficios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cronograma de Autoevaluaciones por IdMatriculaCabecera
        /// </summary>
        /// <returns> Cronograma de Autoevaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionV2DTO>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionesPorMatriculaV2(int idMatriculaCabecera)
        {
            try
            {
                var listaCronogramaAutoEvaluacionV2DTO = new List<CronogramaAutoEvaluacionV2DTO>();
                var moodleCronogramaEvaluacionService = new MoodleCronogramaEvaluacionService(_unitOfWork);
                listaCronogramaAutoEvaluacionV2DTO = moodleCronogramaEvaluacionService.ObtenerCronogramaAutoEvaluacionUltimaVersion(idMatriculaCabecera);
                var listaVersionCronogramaAutoEvaluacionDTO = moodleCronogramaEvaluacionService.ObtenerVersionesCronograma(idMatriculaCabecera);
                return Ok(new { Versiones = listaVersionCronogramaAutoEvaluacionDTO, CronogramaUltimaVersion = listaCronogramaAutoEvaluacionV2DTO });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Cursos IRCA por Id de Matrícula Cabecera
        /// </summary>
        /// <returns> Lista de CUrsos IRCA </returns>
        /// <returns> Lista de Objeto DTO : List<PgeneralCursoIRCADTO> </returns>
        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCursoIRCA(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var pGeneralService = new PGeneralService(_unitOfWork);
                var irca = new { listaCursos = pGeneralService.ObtenerCursosIrcaAlumno(idMatriculaCabecera) };
                return Ok(irca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Lista de Cursos Moodle por Codigo de Matricula
        /// </summary>
        /// <returns> Lista de Cursos Moodle </returns>
        /// <returns> Lista de Objeto DTO : List<CursoMoodleDTO> </returns>
        [Route("[Action]/{codigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCrongramaMoodle(string codigoMatricula)
        {
            try
            {
                var matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                var CursoMoodle = matriculaCabeceraService.ObtenerCursoMoodle(codigoMatricula);
                return Ok(CursoMoodle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Seguimiento de Alumnos y categorías
        /// </summary>
        /// <returns> Evaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionV2DTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerSeguimientoAlumnoCategoria()
        {
            try
            {
                SeguimientoAlumnoCategoriaService seguimientoAlumnoCategoriaService = new SeguimientoAlumnoCategoriaService(_unitOfWork);
                return Ok(seguimientoAlumnoCategoriaService.ObtenerSeguimientoAlumnoCategoria());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene versiones de cronograma por Id de Matrícula Cabecera
        /// </summary>
        /// <returns> versiones de cronograma </returns>
        /// <returns> Lista de Objeto DTO : List<VersionCronogramaAutoEvaluacionDTO> </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerVersionesCronogramaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                var moodleCronogramaEvaluacionService = new MoodleCronogramaEvaluacionService(_unitOfWork);
                var versiones = moodleCronogramaEvaluacionService.ObtenerVersionesCronograma(idMatriculaCabecera);
                return Ok(versiones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Costos Administrativos por IdMatriculaCabecera
        /// </summary>
        /// <returns> Lista de Costos Administrativos </returns>
        /// <returns> Lista de Objeto DTO : List<CostosAdministrativosDTO> </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCostosAdministrativos(int idMatriculaCabecera)
        {
            try
            {
                var matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                var costosAdministrativos = matriculaCabeceraService.ObtenerCostosAdministrativos(idMatriculaCabecera);
                return Ok(costosAdministrativos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Programas Especificos asociados a una matricula
        /// </summary>
        /// <returns> Programas Especificos asociados a una matricula </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoPorMatricula(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PEspecificoMatriculaAlumnoService pEspecificoMatriculaAlumnoService = new PEspecificoMatriculaAlumnoService(_unitOfWork);
                var listaCursosMatriculados = pEspecificoMatriculaAlumnoService.ObtenerPEspecificoPorMatricula(idMatriculaCabecera);
                return Ok(listaCursosMatriculados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Juan Diego Huanaco Quispe.
        /// Fecha: 18/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Programas Especificos asociados a una matricula (modificado para el modulo de ATC Portal Academico)
        /// </summary>
        /// <returns> Programas Especificos asociados a una matricula </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoPorMatriculaParaPortalAcademico(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PEspecificoMatriculaAlumnoService pEspecificoMatriculaAlumnoService = new PEspecificoMatriculaAlumnoService(_unitOfWork);
                var listaCursosMatriculados = pEspecificoMatriculaAlumnoService.ObtenerPEspecificoPorMatriculaParaPortalAcademico(idMatriculaCabecera);
                return Ok(listaCursosMatriculados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Juan Diego Huanaco Quispe.
        /// Fecha: 10/05/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalle de los entregables de un Alumno para un criterio de un curso en especifico.
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<PEspecificoCriterioDetalleEntregableDelAlumno> </returns>
        [Route("[Action]/{idCriterioEvaluacion:int}/{idPEspecifico:int}/{idMatriculaCabecera:int}")]
        [HttpGet]
        public ActionResult ObtenerCriterioDetalleEntregablesAlumno(int idCriterioEvaluacion, int idPEspecifico, int idMatriculaCabecera)
        {
            var pEspecificoMatriculaAlumnoService = new PEspecificoMatriculaAlumnoService(_unitOfWork);
            var listaCursosMatriculados = pEspecificoMatriculaAlumnoService.ObtenerCriterioDetalleEntregablesAlumno(idCriterioEvaluacion, idPEspecifico, idMatriculaCabecera);
            return Ok(listaCursosMatriculados);


        }



        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los Programas Especificos asociados a una matricula
        /// </summary>
        /// <returns> Programas Especificos asociados a una matricula </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerDatosCursosPorMatricula(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PEspecificoMatriculaAlumnoService pEspecificoMatriculaAlumnoService = new PEspecificoMatriculaAlumnoService(_unitOfWork);
                var listaCursosMatriculados = pEspecificoMatriculaAlumnoService.ObtenerDatosCursosPorMatricula(idMatriculaCabecera);
                return Ok(listaCursosMatriculados);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        ///// TipoFuncion: GET
        ///// Autor: Gilmer Quispe.
        ///// Fecha: 12/11/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Obtiene los Programas Especificos asociados a una matricula
        ///// </summary>
        ///// <returns> Programas Especificos asociados a una matricula </returns>
        ///// <returns> Lista de Objeto DTO : List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        //[Route("[Action]/{idMatriculaCabecera}")]
        //[HttpGet]
        //public ActionResult ObtenerDatosCursoPorMatricula(int idMatriculaCabecera)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        PEspecificoMatriculaAlumnoService pEspecificoMatriculaAlumnoService = new PEspecificoMatriculaAlumnoService(unitOfWork);
        //        var listaCursosMatriculados = pEspecificoMatriculaAlumnoService.ObtenerPEspecificoPorMatricula(idMatriculaCabecera);
        //        return Ok(listaCursosMatriculados);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Carga una lista de oportunidades con venta cruzada por idAlumno y Carga un historial de oportunidades por idAlumno
        /// </summary>
        /// <returns> Devuelve un objeto de la Oportunidad </returns>
        /// <returns> Objeto : OportunidadInformacion </returns>
        [Route("[action]/{idAlumno}/{idClasificacionPersona}")]
        [HttpGet]
        public ActionResult ObtenerInformacionOportunidad(int idAlumno, int idClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadInformacionService oportunidadInformacionService = new OportunidadInformacionService(_unitOfWork);
                var respuestaOportunidadInformacion = oportunidadInformacionService.ObtenerInformacionOportunidad(idAlumno, idClasificacionPersona);
                return Ok(respuestaOportunidadInformacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los comentarios de operaciones por tipo
        /// </summary>
        /// <returns> Lista de Comentarios </returns>
        /// <returns> Lista de Objeto DTO : List<ObtenerSeguimientoAlunoComentarioDTO>
        [Route("[Action]/{idOportunidad}/{idTipoSeguimientoAlumnoCategoria}")]
        [HttpGet]
        public ActionResult ObtenerComentarioOperaciones(int idOportunidad, int idTipoSeguimientoAlumnoCategoria)
        {
            try
            {
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                var comentario = oportunidadService.ObtenerComentariosOperaciones(idOportunidad, idTipoSeguimientoAlumnoCategoria);
                return Ok(comentario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los comentarios de operaciones por tipo
        /// </summary>
        /// <returns> Lista de Comentarios </returns>
        /// <returns> Lista de Objeto DTO : List<ObtenerSeguimientoAlunoComentarioDTO>
        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerComentariosOperacionesPagosAcademicos(int idOportunidad)
        {
            try
            {
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                var comentario = oportunidadService.ObtenerComentariosOperacionesPagosAcademicos(idOportunidad);
                return Ok(comentario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerComentariosOperacionesPagosAcademicos2(int idOportunidad)
        {
            try
            {
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                var comentario = oportunidadService.ObtenerComentariosOperacionesPagosAcademicos2(idOportunidad);
                return Ok(comentario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 16/11/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene Historial de Interacciones de Oportunidad
        /// </summary>
        /// <returns> Información de Interacciones de Oportunidad  </returns>
        /// <returns> Lista de Objeto DTO : List<ReporteSeguimientoOportunidadLogGridDTO> </returns>
        [Route("[action]/{idAlumno}/{idOportunidad}/{idPadre}")]
        [HttpGet]
        public ActionResult ObtenerHistorialInteraccionesOportunidad(int idAlumno, int? idOportunidad, int? idPadre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                ReporteService reporteService = new ReporteService(_unitOfWork);
                int idOportunidadtemp = idOportunidad == null ? 0 : idOportunidad.Value;
                int idPadretemp = idPadre == null ? 0 : idPadre.Value;
                var resultado = reporteService.ObtenerOportunidadesLogPorAlumno(idAlumno, idOportunidadtemp, idPadretemp);
                var listanueva = new List<ReporteSeguimientoOportunidadLogGridDTO>
                {
                    resultado.Where(x => x.Estado == "NO EJECUTADO").FirstOrDefault()
                };
                listanueva.AddRange(resultado.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());

                return Ok(listanueva);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 16/11/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene Historial de Interacciones de Oportunidad
        /// </summary>
        /// <returns> Información de Interacciones de Oportunidad  </returns>
        /// <returns> Lista de Objeto DTO : List<ReporteSeguimientoOportunidadLogGridDTO> </returns>
        [Route("[action]/{idAlumno}/{idOportunidad}/{idPadre}/{pageNumber}/{pageSize}")]
        [HttpGet]
        public ActionResult ObtenerHistorialInteraccionesOportunidadATC(int idAlumno, int? idOportunidad, int? idPadre, int pageNumber, int pageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                ReporteService reporteService = new ReporteService(_unitOfWork);
                int idOportunidadtemp = idOportunidad == null ? 0 : idOportunidad.Value;
                int idPadretemp = idPadre == null ? 0 : idPadre.Value;
                var resultado = reporteService.ObtenerOportunidadesLogPorAlumnoATC(idAlumno, idOportunidadtemp, idPadretemp, pageNumber, pageSize);
                var listanueva = new List<ReporteSeguimientoNWActividadAlternoATCDTO>
                {
                    resultado.Items.Where(x => x.Estado == "NO EJECUTADO").FirstOrDefault()
                };
                listanueva.AddRange(resultado.Items.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());

                return Ok(new { Actividades = listanueva, TotalActividades = resultado.TotalActividades });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 16/11/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene Historial de Interacciones de Oportunidad
        /// </summary>
        /// <returns> Información de Interacciones de Oportunidad  </returns>
        /// <returns> Lista de Objeto DTO : List<ReporteSeguimientoOportunidadLogGridDTO> </returns>
        [Route("[action]/{idAlumno}/{idOportunidad}/{idPadre}/{pageNumber}/{pageSize}")]
        [HttpGet]
        public ActionResult ObtenerHistorialInteraccionesOportunidadOperaciones(int idAlumno, int? idOportunidad, int? idPadre,int pageNumber, int pageSize)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                ReporteService reporteService = new ReporteService(_unitOfWork);
                int idOportunidadtemp = idOportunidad == null ? 0 : idOportunidad.Value;
                int idPadretemp = idPadre == null ? 0 : idPadre.Value;
                var resultado = reporteService.ObtenerOportunidadesLogPorAlumnoOperaciones(idAlumno, idOportunidadtemp, idPadretemp, pageNumber, pageSize);
                var listanueva = new List<ReporteSeguimientoNWActividadAlternoATCDTO>
                {
                    resultado.Where(x => x.Estado == "NO EJECUTADO").FirstOrDefault()
                };
                listanueva.AddRange(resultado.Where(x => x.Estado != "NO EJECUTADO").OrderByDescending(x => x.FechaModificacion).ToList());

                return Ok(listanueva);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las plantillas WhatsApp Operaciones
        /// </summary>
        /// <returns> Información de Plantillas WhatsApp Operaciones </returns>
        /// <returns> Lista Objeto DTO : List<PlantillaWhatsAppDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenterPlantillaWhatsAppOperaciones()
        {
            try
            {
                var plantillaClaveValorService = new PlantillaClaveValorService(_unitOfWork);
                return Ok(plantillaClaveValorService.ObtenterPlantillaWhatsAppOperaciones().OrderBy(w => w.Nombre));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Documentos Whatsapp por Alumno
        /// </summary>
        /// <returns> Documentos Whatsapp por Alumno </returns>
        /// <returns> Objeto BO : DocumentosBO </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerDocumentosWhatsApp(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DocumentoService documentoService = new DocumentoService(_unitOfWork);
                documentoService.ObtenerDocumentosIdAlumno(idAlumno);
                return Ok(documentoService.documentoObjDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza la Fecha de visualización de Whatsapp
        /// </summary>
        /// <returns> Confirmación de Actualización </returns>
        /// <returns> Bool </returns>
        [Route("[action]/{idActividadDetalle}/{usuario}")]
        [HttpGet]
        public IActionResult ActualizarFechaOcultarWhatsapp(int idActividadDetalle, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ActividadDetalleService actividadDetalleService = new ActividadDetalleService(_unitOfWork);

                var actividad = actividadDetalleService.ObtenerPorId(idActividadDetalle);
                actividad.FechaModificacion = DateTime.Now;
                actividad.FechaOcultarWhatsapp = DateTime.Now;
                actividad.UsuarioModificacion = usuario;
                actividadDetalleService.Update(actividad);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
                OcurrenciaActividadService ocurrenciaActividadService = new OcurrenciaActividadService(_unitOfWork);
                List<ArbolOcurrenciaDTO> listaArbolOcurrencia = ocurrenciaActividadService.ObtenerArbolOcurrencia(idActividadCabecera, idOcurrenciaActividadPadre);
                return Ok(listaArbolOcurrencia);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 22/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Fecha de Reprogramación Automática
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param>
        /// <returns> Fecha y de Reprogramación Automática </returns>
        /// <returns> String </returns>
        [Route("[action]/{idOportunidad}")]
        [HttpGet]
        public IActionResult ObtenerFechaReprogramacionAutomatica(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var oportunidadService = new OportunidadService(_unitOfWork);
                var datosOportunidad = oportunidadService.ObtenerDatosParaReprogramacionAutomatica(idOportunidad);
                var personalHorarioService = new PersonalHorarioService(_unitOfWork);
                if (datosOportunidad.IdPersonalAsignado < 1) return BadRequest("No existe horario para el personal con Id 0");
                var horaReprogramacionAutomaticaService = new HoraReprogramacionAutomaticaService(_unitOfWork);
                horaReprogramacionAutomaticaService.IdPersonal = datosOportunidad.IdPersonalAsignado;
                horaReprogramacionAutomaticaService.PersonalHorario = personalHorarioService.ObtenerHorarioAsTable(datosOportunidad.IdPersonalAsignado);
                var respuesta = horaReprogramacionAutomaticaService.ObtenerFechaHoraReprogramacionAutomaticaOperaciones(idOportunidad);

                return Ok(respuesta);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 10/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza la Programación de Actividades en Operaciones
        /// </summary>
        /// <returns> Información de Programación de Actividad </returns>
        /// <returns> Id de la Oportunidad trabajada </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarYProgramarActividadOperaciones([FromBody] ParametroFinalizarActividadDTO dto)
        {
            string parametros = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                parametros = JsonConvert.SerializeObject(dto);
                var oportunidadService = new OportunidadService(_unitOfWork);
                var preCalculadaCambioFaseService = new PreCalculadaCambioFaseService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var oportunidadBeneficioService = new OportunidadBeneficioService(_unitOfWork);
                var horaBloqueadaService = new HoraBloqueadaService(_unitOfWork);
                //var correoService = new CorreoService(_unitOfWork);
                IAgendaInformacionActividadService correoService = new AgendaInformacionActividadService(_unitOfWork);
                var alumnoService = new AlumnoService(_unitOfWork);
                var horaBloqueada = new HoraBloqueada();
                int idReprogramacionCabecera = 0;
                if (dto.datosOportunidad.UltimaFechaProgramada != null)
                {
                    dto.datosOportunidad.UltimaFechaProgramada = dto.datosOportunidad.UltimaFechaProgramada + " " + dto.datosOportunidad.UltimaHoraProgramada;
                    if (dto.tipoProgramacion == "manual")
                    {
                        var datosOportunidad = oportunidadService.ObtenerDatosParaReprogramacionAutomatica(dto.ActividadAntigua.IdOportunidad.Value);

                        var personalHorarioService = new PersonalHorarioService(_unitOfWork);

                        var HorarioReprogramacion = new HoraReprogramacionAutomaticaService(_unitOfWork);
                        HorarioReprogramacion.PersonalHorario = personalHorarioService.ObtenerHorarioAsTable(datosOportunidad.IdPersonalAsignado);
                        HorarioReprogramacion.IdPersonal = datosOportunidad.IdPersonalAsignado;
                        HorarioReprogramacion.ReprogramarAlumnoClasesOnline(dto.ActividadAntigua.IdAlumno.Value);
                        dto.datosOportunidad.UltimaFechaProgramada = HorarioReprogramacion.ObtenerFechaHoraReprogramacionManualOperaciones(DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada));
                    }
                    horaBloqueada.Fecha = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                    horaBloqueada.Hora = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                }
                horaBloqueada.FechaCreacion = DateTime.Now;
                horaBloqueada.FechaModificacion = DateTime.Now;
                horaBloqueada.UsuarioCreacion = dto.filtro.Usuario;
                horaBloqueada.UsuarioModificacion = dto.filtro.Usuario;
                horaBloqueada.IdPersonal = dto.filtro.IdPersonal;
                horaBloqueada.Estado = true;

                var oportunidadEntidad = oportunidadService.ObtenerPorId(dto.ActividadAntigua.IdOportunidad.Value);
                oportunidadService._oportunidadBo = new OportunidadBoDTO();
                oportunidadService._oportunidadBo = oportunidadService.MapeoOportunidadBaseObjetDesdeEntidad(oportunidadEntidad);
                oportunidadService._oportunidadBo.Usuario = dto.Usuario;
                oportunidadService._oportunidadBo.IdFaseOportunidadIp = dto.datosOportunidad.IdFaseOportunidadIp;
                oportunidadService._oportunidadBo.IdFaseOportunidadIc = dto.datosOportunidad.IdFaseOportunidadIc;
                oportunidadService._oportunidadBo.FechaEnvioFaseOportunidadPf = dto.datosOportunidad.FechaEnvioFaseOportunidadPf;
                oportunidadService._oportunidadBo.FechaPagoFaseOportunidadPf = dto.datosOportunidad.FechaPagoFaseOportunidadPf;
                oportunidadService._oportunidadBo.FechaPagoFaseOportunidadIc = dto.datosOportunidad.FechaPagoFaseOportunidadIc;
                oportunidadService._oportunidadBo.IdFaseOportunidadPf = dto.datosOportunidad.IdFaseOportunidadPf;
                oportunidadService._oportunidadBo.CodigoPagoIc = dto.datosOportunidad.CodigoPagoIc;

                var ActividadAntigua = new ActividadDetalle();
                ActividadAntigua.Id = dto.ActividadAntigua.Id;
                ActividadAntigua.IdActividadCabecera = dto.ActividadAntigua.IdActividadCabecera;
                ActividadAntigua.FechaProgramada = DateTime.Parse(dto.datosOportunidad.UltimaFechaProgramada);
                ActividadAntigua.FechaReal = dto.ActividadAntigua.FechaReal;
                ActividadAntigua.DuracionReal = dto.ActividadAntigua.DuracionReal;
                ActividadAntigua.IdOcurrencia = dto.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdEstadoActividadDetalle = dto.ActividadAntigua.IdEstadoActividadDetalle;
                ActividadAntigua.Comentario = dto.ActividadAntigua.Comentario;
                ActividadAntigua.IdAlumno = dto.ActividadAntigua.IdAlumno;
                ActividadAntigua.Actor = dto.ActividadAntigua.Actor;
                ActividadAntigua.IdOportunidad = dto.ActividadAntigua.IdOportunidad.Value;
                ActividadAntigua.IdCentralLlamada = dto.ActividadAntigua.IdCentralLlamada;
                ActividadAntigua.RefLlamada = dto.ActividadAntigua.RefLlamada;
                ActividadAntigua.IdOcurrenciaActividad = dto.ActividadAntigua.IdOcurrenciaActividad;
                ActividadAntigua.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona;

                oportunidadService._oportunidadBo.ActividadAntigua = ActividadAntigua;

                var ActividadNueva = actividadDetalleService.ObtenerPorId(dto.ActividadAntigua.Id);
                if (oportunidadService._oportunidadBo.Id > 0 && oportunidadService._oportunidadBo.Id != null)
                {
                    oportunidadService._oportunidadBo.ActividadNueva = ActividadNueva;
                    // ActividadNueva.LlamadaActividad = null;
                    oportunidadService.FinalizarActividad(false, dto.datosOportunidad);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            if (oportunidadService._oportunidadBo.PreCalculadaCambioFase != null)
                            {
                                oportunidadService._oportunidadBo.PreCalculadaCambioFase.Contador = preCalculadaCambioFaseService.ExistePreCalculadaCambioFase(oportunidadService._oportunidadBo.PreCalculadaCambioFase);
                                preCalculadaCambioFaseService.Add(oportunidadService._oportunidadBo.PreCalculadaCambioFase);
                            }
                            horaBloqueadaService.Add(horaBloqueada);

                            oportunidadService.ProgramaActividadV2();
                            if (dto.filtro.IdActividadCabecera == 47 || dto.filtro.IdActividadCabecera == 63)
                            {
                                oportunidadService._oportunidadBo.ActividadNuevaProgramarActividad.IdActividadCabecera = 48;
                                oportunidadService._oportunidadBo.IdActividadCabeceraUltima = 48;
                            }
                            var nuevaActividadDetalle = actividadDetalleService.Add(oportunidadService._oportunidadBo.ActividadNuevaProgramarActividad);
                            oportunidadService._oportunidadBo.IdActividadDetalleUltima = oportunidadService._oportunidadBo.ActividadNuevaProgramarActividad.Id;
                            oportunidadService._oportunidadBo.IdActividadDetalleUltima = nuevaActividadDetalle.Id;
                            oportunidadService._oportunidadBo.ActividadNuevaProgramarActividad = null;
                            var mapeoOportunidad = oportunidadService.MapeoBoDTO(oportunidadService._oportunidadBo);
                            oportunidadService.Update(mapeoOportunidad);

                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            var Mailservice = new TMK_MailService();

                            var mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error FinalizarYProgramarActividad Transaction";
                            mailData.Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros;
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                }
                else
                {
                    return BadRequest("No se pudo trabajar la oportunidad");
                }
                var cantNoEjecutadas = 0;
                var cantNumerooEjecutadas = oportunidadService.ObtenerCalculoReprogramaciones(dto.ActividadAntigua.IdOportunidad.Value);
                var subEstado = oportunidadService.ObtenerSubEstadoAlumno(dto.ActividadAntigua.IdOportunidad.Value);
                var datosAlumno = oportunidadService.ObtenerDatosAlumno(dto.ActividadAntigua.IdOportunidad.Value);
                if (cantNumerooEjecutadas == null)
                {
                    cantNoEjecutadas = 0;
                }
                else
                {
                    if (cantNumerooEjecutadas.NroReprogramacionesNE == null)
                    {
                        cantNoEjecutadas = 0;
                    }
                    else
                    {
                        cantNoEjecutadas = cantNumerooEjecutadas.NroReprogramacionesNE;
                    }
                }
                var subEstadoAlumno = subEstado.SubEstadoMatricula;
                switch (subEstadoAlumno.ToString())
                {
                    case "Seguimiento académico":
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Por abandonar":
                        if (cantNoEjecutadas >= 4)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Aprobado BSGI CF + Partner CF":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                        }
                        break;
                    case "Aprobado BSGI CF + Partner CD":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Aprobado BSGI CD + Partner CD":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Aprobado BSGI CF - Pendiente Partner":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Aprobado BSGI CD - Pendiente Partner":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Aprobado BSGI CF":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Aprobado BSGI CD":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Completado BSGI - No envio PF CF (solo es para casos donde hay PF)":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Completado BSGI - No envio PF CD (solo es para casos donde hay PF)":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Completado BSGI CF (no corrresponde PF)":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Completado BSGI CD (no corrresponde PF)":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Modulos BSGI":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Aprobado BSGI CF - Pendiente ":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Aprobado BSGI CF - Pendiente beneficios":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Aprobado BSGI CD - Pendiente Beneficios":
                        if (cantNoEjecutadas >= 3)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Sin Deuda":
                        if (cantNoEjecutadas >= 2)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Con deuda":
                        if (cantNoEjecutadas >= 2)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Con devolución":
                        if (cantNoEjecutadas >= 2)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);
                        }
                        break;
                    case "Sin devolución":
                        if (cantNoEjecutadas >= 2)
                        {
                            correoService.EnvioCorreoAlumno( /*idplantilla*/1396, datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                            alumnoService.EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera, 1478, datosAlumno.IdPersonal);

                        }
                        break;
                };


                //EnvioCorreoAlumno( /*idplantilla*/1396,datosAlumno.IdPersonal, datosAlumno.EmailPersonal, datosAlumno.EmailAlumno, dto.ActividadAntigua.IdOportunidad.Value);
                // EnviarSMS( /*subEstado.Id*/ datosAlumno.IdMatriculaCabecera , 1478, datosAlumno.IdPersonal);
                return Ok(new { idOportunidad = oportunidadService._oportunidadBo.Id, IdReprogramacionCabecera = idReprogramacionCabecera });
            }
            catch (Exception ex)
            {
                try
                {
                    List<string> correos = new List<string>();
                    correos.Add("sistemas@bsginstitute.com");

                    var Mailservice = new TMK_MailService();
                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "jcayo@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Error FinalizarYProgramarActividad General";
                    mailData.Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros;
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;
                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                catch (Exception)
                {
                }
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: , Edgar S.
        /// Fecha: 10/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Lista de Notas Promedio por Programa Específico
        /// </summary>
        /// <returns> Lista de Notas Promedio por Programa Específico </returns>
        /// <returns> Lista de Objeto DTO : List<NotaPresencialPromedioEspecificoDTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}/{IdEspecifico}/{TipoPrograma}")]
        [HttpGet]
        public IActionResult ObtenerPEspecificoPorMatriculaPorIdEspecifico(int IdMatriculaCabecera, int IdEspecifico, int TipoPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                NotaService _repoNota = new NotaService(_unitOfWork);
                if (TipoPrograma == 1)
                {
                    var _listadoNotasPorMatricula = _repoNota.ListadoNotaPorMatriculaCabeceraPromedioIdEspecifico(IdMatriculaCabecera, IdEspecifico);
                    return Ok(_listadoNotasPorMatricula);
                }
                else
                {
                    List<NotaPresencialPromedioEspecificoDTO> _datosRegistroDetalle = new List<NotaPresencialPromedioEspecificoDTO>();
                    MoodleCronogramaEvaluacionService evaluacionesService = new MoodleCronogramaEvaluacionService(_unitOfWork);
                    EsquemaEvaluacionService esquemaEvaluacionService = new EsquemaEvaluacionService(_unitOfWork);
                    var _resultado = evaluacionesService.ObtenerCronogramaAutoEvaluacionUltimaVersionPorCurso(IdMatriculaCabecera, IdEspecifico);
                    if (_resultado.Count == 0)
                    {
                        var listado = esquemaEvaluacionService.ListadoCriteriosEvaluacionPorCurso(IdMatriculaCabecera, IdEspecifico, 0);
                        foreach (var item in listado.DetalleCalificacion)
                        {
                            NotaPresencialPromedioEspecificoDTO _dato = new NotaPresencialPromedioEspecificoDTO();
                            _dato.IdPEspecifico = IdEspecifico;
                            _dato.NombrePEspecifico = item.CriterioEvaluacion;
                            _dato.Nota = item.Valor != null ? item.Valor.ToString("0") : "0";
                            _dato.Promedio = item.Valor != null ? item.Valor.ToString("0") : "0";
                            _dato.Porcentaje = item.Ponderacion != null ? item.Ponderacion.ToString("0") + "%" : "0";
                            _datosRegistroDetalle.Add(_dato);
                        }

                    }
                    else
                    {
                        foreach (var item in _resultado)
                        {
                            NotaPresencialPromedioEspecificoDTO _dato = new NotaPresencialPromedioEspecificoDTO();
                            _dato.IdPEspecifico = IdEspecifico;
                            _dato.NombrePEspecifico = item.NombreEvaluacion;
                            _dato.Nota = item.Nota != null ? item.Nota.Value.ToString("0") : "0";
                            _dato.Promedio = item.Nota != null ? item.Nota.Value.ToString("0") : "0";
                            _dato.Porcentaje = "100%";
                            _datosRegistroDetalle.Add(_dato);
                        }
                    }
                    return Ok(_datosRegistroDetalle);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 09/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Fecha de Programación Ejecutada
        /// </summary>
        /// <returns> Fecha de Programación Ejecutada </returns>
        /// <returns> Objeto : HoraReprogramacionAutomaticaBO </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public IActionResult ObtenerFechaReprogramacionEjecutada(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                var datosOportunidad = oportunidadService.ObtenerCasosReprogramacionManualOperacionesAlterno(idOportunidad);
                var oportunidad = oportunidadService.ObtenerDatosParaReprogramcionAutomatica(idOportunidad);

                if (datosOportunidad.FechaProximaCuota != null)
                {
                    datosOportunidad.FechaProximaCuotaTexto = datosOportunidad.FechaProximaCuota.Value.Year + "/" + datosOportunidad.FechaProximaCuota.Value.Month + "/" + datosOportunidad.FechaProximaCuota.Value.Day + " " + datosOportunidad.FechaProximaCuota.Value.Hour + ":" + datosOportunidad.FechaProximaCuota.Value.Minute + ":" + datosOportunidad.FechaProximaCuota.Value.Second;
                }
                PersonalHorarioService personalHorarioService = new PersonalHorarioService(_unitOfWork);
                datosOportunidad.PersonalHorario = personalHorarioService.ObtenerHorarioAsTable(oportunidad.IdPersonalAsignado);
                return Ok(new { records = datosOportunidad });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 31/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cronograma de Evaluaciones por Id de MatriculaCabecera y Id de Curso Moodle
        /// </summary>
        /// <returns> cronograma de Evaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionV2DTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}/{IdCursoMoodle}")]
        [HttpGet]
        public IActionResult ObtenerEvaluacionesPorMatriculaV3(int IdMatriculaCabecera, int IdCursoMoodle)
        {
            try
            {
                List<CronogramaAutoEvaluacionV2DTO> resultado = new List<CronogramaAutoEvaluacionV2DTO>();
                MoodleCronogramaEvaluacionService evaluacionesBO = new MoodleCronogramaEvaluacionService(_unitOfWork);
                resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersionPorCurso(IdMatriculaCabecera, IdCursoMoodle);
                var versiones = evaluacionesBO.ObtenerVersionesCronograma(IdMatriculaCabecera);
                return Ok(new { Versiones = versiones, CronogramaUltimaVersion = resultado });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Evaluaciones por Id de Matrícula Cabecera y Versión
        /// </summary>
        /// <returns> Evaluaciones </returns>
        /// <returns> Lista de Objeto DTO : List<CronogramaAutoEvaluacionV2DTO> </returns>
        [Route("[Action]/{IdMatriculaCabecera}/{Version}")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionesPorVersion(int IdMatriculaCabecera, int Version)
        {
            try
            {
                List<CronogramaAutoEvaluacionV2DTO> resultado = new List<CronogramaAutoEvaluacionV2DTO>();
                MoodleCronogramaEvaluacionService evaluacionesBO = new MoodleCronogramaEvaluacionService(_unitOfWork);
                resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionPorVersion(IdMatriculaCabecera, Version);

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 02/08/2023
        /// Versión: 2.0
        /// <summary>
        /// Obtiene Lista de Programas Específicos Relacionados por Programa General
        /// </summary>
        /// <returns> Lista de Programas Específicos Relacionados </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoComboDTO> </returns>
        [Route("[Action]/{IdPEspecifico}/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoRelacionadoPorIdPGeneral(int IdPEspecifico, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PEspecificoService pespecificoRepositorio = new PEspecificoService(_unitOfWork);
                return Ok(pespecificoRepositorio.ObtenerPEspecificoRelacionadoPorIdPGeneral(IdPEspecifico, IdMatriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 02/08/2023
        /// Versión: 2.0
        /// <summary>
        /// Obtiene Sesiones Relacionadas por Programa General
        /// </summary>
        /// <returns> Lista de Cursos Relacionados por Programa General </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoComboDTO> </returns>
        [Route("[Action]/{IdPEspecifico}/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoRelacionadoPGeneral(int IdPEspecifico, int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PEspecificoService pespecificoRepositorio = new PEspecificoService(_unitOfWork);
                return Ok(pespecificoRepositorio.ObtenerPEspecificoRelacionadoPGeneral(IdPEspecifico, IdMatriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 08/02/2023
        /// Versión: 2.0
        /// <summary>
        /// Obtiene cursos relacionados de irca por programa especifico
        /// </summary>
        /// <returns> Cursos relacionados de irca por programa especifico </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoComboDTO> </returns>
        [Route("[Action]/{IdPEspecifico}/{IdMatriculaCabecera}/{EsCursoDSig}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoRelacionadoIrca(int IdPEspecifico, int IdMatriculaCabecera, bool EsCursoDSig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PEspecificoService pespecificoRepositorio = new PEspecificoService(_unitOfWork);
                return Ok(pespecificoRepositorio.ObtenerPEspecificoRelacionadoIrca(IdPEspecifico, IdMatriculaCabecera, EsCursoDSig));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Daniel Huaita
        /// Fecha: 02/09/2023
        /// Versión: 2.0
        /// <summary>
        /// Inserta Programa Específico por Matrícula y Envia Correo de Confirmación
        /// </summary>
        /// <returns> PEspecificos asociados a una matricula </returns>
        /// <returns> Lista de Objeto DTO : List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarPEspecificoMatriculaAlumnoRepositorio([FromBody] PEspecificoMatriculaAlumnoDTO pEspecificoMatriculaAlumnoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PEspecificoMatriculaAlumnoService pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoService(_unitOfWork);
                return Ok(pEspecificoMatriculaAlumnoRepositorio.InsertarPEspecificoMatriculaAlumnoRepositorio(pEspecificoMatriculaAlumnoDTO));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: miguel Quiñones
        /// Fecha: 1/03/2023
        /// Versión: 1.1
        /// <summary>
        /// Actualiza Estado Beneficios por Matrícula y Estado de Mactrícula Beneficio
        /// </summary>
        /// <param name="IdConfiguracionBeneficioProgramaGeneral">Id Configuración de Programa General</param>
        /// <param name="IdMatriculaCabeceraBeneficio">Id de Beneficio de Matrícula de Cabecera</param>
        /// <param name="Usuario">Usuario de Módulo</param>
        /// <returns>Objeto Agrupado</returns>        
        [Route("[action]/{IdMatriculaCabeceraBeneficio}/{IdConfiguracionBeneficioProgramaGeneral}/{Usuario}")]
        [HttpGet]
        public IActionResult ActualizarEstadoMatriculaBeneficio(int IdMatriculaCabeceraBeneficio, int IdConfiguracionBeneficioProgramaGeneral, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                return Ok(matriculaCabeceraService.actualizarEstadoMatriculaBeneficio(IdMatriculaCabeceraBeneficio, IdConfiguracionBeneficioProgramaGeneral, Usuario));



            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Detalle de Tiempo Capacitacion para Agenda asociado a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerTiempoCapacitacionPorIdOportunidad/{idOportunidad}")]
        public IActionResult ObtenerTiempoCapacitacionPorIdOportunidad(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TiempoCapacitacionService servicioTiempoCapacitacion = new TiempoCapacitacionService(_unitOfWork);
                var tiempoCapacitacion = servicioTiempoCapacitacion.ObtenerCombo();
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                var oportunidad = oportunidadService.ObtenerTiempoCapacitacionPorIdOportunidad(idOportunidad);
                //ObtenerOportunidadTiempoCapacitacionAsync
                if (oportunidad.IdTiempoCapacitacion == null)
                {
                    if (oportunidad.IdTiempoCapacitacionValidacion == null)
                    {
                        return Ok(new { Records = tiempoCapacitacion, Record = oportunidad.IdTiempoCapacitacion, Lista = false, ListaValidacion = false, RecordValidado = oportunidad.IdTiempoCapacitacionValidacion });
                    }
                    else
                    {
                        return Ok(new { Records = tiempoCapacitacion, Record = oportunidad.IdTiempoCapacitacion, Lista = false, ListaValidacion = true, RecordValidado = oportunidad.IdTiempoCapacitacionValidacion });
                    }
                }
                else
                {
                    if (oportunidad.IdTiempoCapacitacionValidacion == null)
                    {
                        return Ok(new { Records = tiempoCapacitacion, Record = oportunidad.IdTiempoCapacitacion, Lista = true, ListaValidacion = false, RecordValidado = oportunidad.IdTiempoCapacitacionValidacion });
                    }
                    else
                    {
                        return Ok(new { Records = tiempoCapacitacion, Record = oportunidad.IdTiempoCapacitacion, Lista = true, ListaValidacion = true, RecordValidado = oportunidad.IdTiempoCapacitacionValidacion });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 24/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Aprobar Solicitud de Beneficio
        /// </summary>
        /// <param name="IdMatriculaCabeceraBeneficio">Id de Beneficio de matrícula</param>
        /// <param name="Usuario">Usuario de Interfaz</param>
        /// <param name="IdEstadoSolicitudAprobado">Id de Estado de Solicitud Aprobado</param>
        /// <returns>bool, string</returns>
        [Route("[action]/{IdMatriculaCabeceraBeneficio}/{Usuario}/{IdEstadoSolicitudAprobado}")]
        [HttpGet]
        public IActionResult AprobarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario, int IdEstadoSolicitudAprobado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var agendaActividadesService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadesService.AprobarSolicitudBeneficio(IdMatriculaCabeceraBeneficio, Usuario, IdEstadoSolicitudAprobado));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 03/24/2023
        /// Versión: 1.0
        /// <summary>
        /// Rechaza Solicitud de Beneficio
        /// </summary>
        /// <param name="IdMatriculaCabeceraBeneficio">Id de Beneficio de matrícula</param>
        /// <param name="Usuario">Usuario de Interfaz</param>
        /// <param name="IdEstadoSolicitudRechazado">Id de Estado de Solicitud Rechazado</param>
        /// <param name="IdEstadoMatriculaCabeceraBeneficio">Estado de Beneficio</param>
        /// <returns>bool, string</returns>
        [Route("[action]/{IdMatriculaCabeceraBeneficio}/{Usuario}/{IdEstadoSolicitudRechazado}/{IdEstadoMatriculaCabeceraBeneficio}")]
        [HttpGet]
        public IActionResult RechazarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario, int IdEstadoSolicitudRechazado, int IdEstadoMatriculaCabeceraBeneficio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var agendaActividadesService = new AgendaActividadService(_unitOfWork);
                return Ok(agendaActividadesService.RechazarSolicitudBeneficio(IdMatriculaCabeceraBeneficio, Usuario, IdEstadoSolicitudRechazado, IdEstadoMatriculaCabeceraBeneficio));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// TipoFuncion: GET
        /// Autor: Joseph LLanque.
        /// Fecha: 05/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los acceso del alumno al portalweb
        /// </summary>
        /// <param name="idAlumno">Correo del receptor </param>
        /// <returns> Acceso al portal </returns>
        /// <returns> obtejo : AlumnoAccesosDTO </returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult obtenerAccesosPortalAlumno(int idalumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var accesoAlumno = new AlumnoService(_unitOfWork);
                var accesos = accesoAlumno.obteneAccesoAlumno(idalumno);
                return Ok(accesos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{idAlumno:int}/{usuario}")]
        [HttpGet]
        public ActionResult registrarLoginPortal(int idAlumno, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var accesoAlumno = new AlumnoService(_unitOfWork);
                var stringValor = accesoAlumno.RegistrarLoginPortal(idAlumno, usuario);
                return Ok(stringValor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// TipoFuncion: GET
        /// Autor: Joseph LLanque.
        /// Fecha: 05/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos de cobranza por alumno
        /// </summary>
        /// <param name="idAlumno">Correo del receptor </param>
        /// <returns> Acceso al portal </returns>
        /// <returns> obtejo : AlumnoAccesosDTO </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerDatosCobranzaAlumno(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var datosCobranzaAlumno = new AlumnoService(_unitOfWork);
                var datosCobranza = datosCobranzaAlumno.obtenerDatosCobranzaAlumno(idMatriculaCabecera);
                return Ok(datosCobranza);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph LLanque.
        /// Fecha: 05/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos de cobranza por alumno
        /// </summary>
        /// <param name="idAlumno">Correo del receptor </param>
        /// <returns> Acceso al portal </returns>
        /// <returns> obtejo : AlumnoAccesosDTO </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerDatosAvanceAonline(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var datosAvanceAonline = new AlumnoService(_unitOfWork);
                var datosCobranza = datosAvanceAonline.obtenerDatosAvanceAonline(idMatriculaCabecera);
                return Ok(datosCobranza);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph LLanque.
        /// Fecha: 05/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos de cobranza por alumno
        /// </summary>
        /// <param name="idAlumno">Correo del receptor </param>
        /// <returns> Acceso al portal </returns>
        /// <returns> obtejo : AlumnoAccesosDTO </returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerDatosAvanceOnline(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var datosAvanceOnline = new AlumnoService(_unitOfWork);
                var datosCobranza = datosAvanceOnline.obtenerDatosAvanceOnline(idMatriculaCabecera);
                return Ok(datosCobranza);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Daniel Huaita
        /// Fecha: 13/04/2023
        /// Versión: 2.0
        /// <summary>
        /// Restablece Solicitud de Beneficio
        /// </summary>
        /// <param name="IdMatriculaCabeceraBeneficio">Id de Beneficio de matrícula</param>
        /// <param name="Usuario">Usuario de Interfaz</param>
        /// <returns>bool, string</returns>
        [Route("[action]/{IdMatriculaCabeceraBeneficio}/{Usuario}")]
        [HttpGet]
        public ActionResult RestablecerSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var agendaActividadesService = new AgendaActividadService(_unitOfWork);

                return Ok(agendaActividadesService.RestablecerSolicitudBeneficio(IdMatriculaCabeceraBeneficio, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerPrecioFinalProgramaAlumno(string CodigoMatricula)
        {

            try
            {
                var agendaActividadesService = new AgendaActividadService(_unitOfWork);

                return Ok(agendaActividadesService.ObtenerPrecioFinalProgramaAlumno(CodigoMatricula));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        /// Tipo Función: GET
        /// Autor:Marco Jose Villanueva Torres.
        /// Fecha: 30/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Argumentos de ProgramaGeneral y sus Soluciones asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("ObtenerProgramaGeneralPresentacionArgumentoDetallePorIdOportunidad/{idOportunidad}")]
        public IActionResult ObtenerProgramaGeneralPresentacionArgumentoDetallePorIdOportunidad(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ProgramaGeneralPresentacionArgumentoService(_unitOfWork);
                return Ok(servicio.ObtenerProgramaGeneralPresentacionArgumentoParaAgendaPorIdOportunidad(idOportunidad));
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
        [HttpPost("ObtenerInformacionProgramaSpeech")]
        public IActionResult ObtenerInformacionProgramaSpeech([FromBody] Dictionary<string, string> filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);

                var idCentroCosto = Convert.ToInt32(filtro["idCentroCosto"]);
                var codigoPais = Convert.ToInt32(filtro["codigoPais"]);
                var idMatriculaCabecera = Convert.ToInt32(filtro["idMatriculaCabecera"]);
                var idOportunidad = Convert.ToInt32(filtro["idOportunidad"]);

                var respuesta = informacionProgramaService.CargarInformacionProgramaAutomaticoSpeech(idCentroCosto, codigoPais, idMatriculaCabecera, idOportunidad);
                return Ok(new { respuesta });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 16/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Sentinel por Id
        /// </summary>
        /// <param name="dni">DNI de la Persona</param>
        /// <returns> Retorna true o false </returns>
        [HttpGet("ObtenerSentinelPorDni/{dni}")]
        public IActionResult ObtenerSentinelPorDni(string dni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioSentinel = new SentinelService(_unitOfWork);
                return Ok(servicioSentinel.ObtenerSentinelPorDni(dni));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Encuestas Por Matricula 
        /// </summary>
        /// <param name="idMatricula">Matricula del Alumno</param>
        /// <returns> List<EncuestaAsignadoMatriculaDTO> </returns>
        [HttpGet("[action]/{idMatricula}")]
        public IActionResult ObtenerEncuestaAlumnoMatriculaCurso(int idMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _servicioPrincipal.ObtenerEncuestaAlumnoMatriculaCurso(idMatricula);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Agrega encuesta de alumno 
        /// </summary>
        /// <param name="Encuesta">Datos para insertar Encuesta</param>
        /// <returns>retorna true o false </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult AgregarPEspecificoSesionEncuestaAlumno([FromBody] AgregarPEspecificoSesionEncuestaAlumnoDTO Encuesta)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _servicioPrincipal.AgregarPEspecificoSesionEncuestaAlumno(Encuesta);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un comentario a una encuesta
        /// </summary>
        /// <param name="Encuesta">Datos para agregar comentario a un Alumno</param>
        /// <returns>Retorna true o false</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult AgregarComentarioEncuesta([FromBody] EncuestaComentarioDTO Encuesta)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _servicioPrincipal.AgregarComentarioEncuesta(Encuesta);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }


        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Mdificado por: Jose Vega (2025-08-27) - Ajuste para retornar JSON estructurado en la respuesta.
        /// Fecha: 11/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera HTML de resumen de programas Versión 1
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpPost("ObtenerInformacionProgramaSpeechjson")]
        public IActionResult ObtenerInformacionProgramaSpeechjson([FromBody] Dictionary<string, string> filtro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var informacionProgramaService = new InformacionProgramaService(_unitOfWork);

                var idCentroCosto = Convert.ToInt32(filtro["idCentroCosto"]);
                var codigoPais = Convert.ToInt32(filtro["codigoPais"]);
                var idMatriculaCabecera = Convert.ToInt32(filtro["idMatriculaCabecera"]);
                var idOportunidad = Convert.ToInt32(filtro["idOportunidad"]);


                var respuesta = informacionProgramaService.CargarInformacionProgramaAutomaticoSpeechjson(
                    idCentroCosto, codigoPais, idMatriculaCabecera, idOportunidad
                );


                var raiz = JObject.FromObject(respuesta);

                JsonSanitizerHelpers.NormalizarEstructura(raiz);

                return Content(raiz.ToString(Newtonsoft.Json.Formatting.Indented), "application/json");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
    
}
