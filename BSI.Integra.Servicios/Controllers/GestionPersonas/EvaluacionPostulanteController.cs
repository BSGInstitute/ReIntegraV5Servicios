using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: ReportePostulanteController
    /// Autor: Flavio R.M.F.
    /// Fecha: 06/04/2024
    /// <summary>
    /// Controller de Reporte de postulante
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class EvaluacionPostulanteController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IEvaluacionPostulanteService _evaluacionPostulanteService;
        public EvaluacionPostulanteController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _evaluacionPostulanteService = new EvaluacionPostulanteService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Flavio R.M.F
        /// Fecha: 10/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos para el modelo de Reporte de Evaluacion Postulante
        /// </summary>
        /// <returns>Una RespuestaGenerica </returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerCombosModulo()
        {
            var resultado = await _evaluacionPostulanteService.ObtenerCombosModulo();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R.M.F
        /// Fecha: 10/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Estados, etapas y puntaje de postulantes
        /// </summary>
        /// <returns>Una RespuestaGenerica </returns>
        [HttpPost("[action]")]
        public IActionResult GenerarReporte([FromBody] EvaluacionPostulanteFiltroReporteDTO filtro)
        {
            var resultado = _evaluacionPostulanteService.GenerarReporte(filtro);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R.M.F
        /// Fecha: 10/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Estados, etapas y puntaje de postulantes
        /// </summary>
        /// <returns>Una RespuestaGenerica </returns>
        [HttpPost("[action]")]
        public IActionResult GenerarReporteIntegra([FromBody] EvaluacionPostulanteFiltroReporteDTO filtro)
        {
            var resultado = _evaluacionPostulanteService.GenerarReporteIntegra(filtro);
            return Ok(new
            {
                EtapaAprobada = resultado,
                CantidadEtapaAprobada = resultado.Count()
            });
        }
        /// TipoFuncion: POST
        /// Autor: FlaviO R.M.F.
        /// Fecha: 17/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tipo de examen y Id de Examen
        /// </summary>
        /// <returns> TipoEvaluacionDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerTipoExamen([FromBody] FiltroTipoExamenDTO filtro)
        {
            var resultado = _evaluacionPostulanteService.ObtenerTipoExamen(filtro);
            return Ok(resultado);
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 17/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene evaluaciones asignadas al evaluador de postulantes por filtro
        /// </summary>
        /// <returns> evaluaciones asignadas al evaluador de postulantes por filtro </returns>
        /// <returns> Lista de Objeto DTO : List<EvaluacionesAsignadasEvaluador> </returns>
        [HttpPost]
        [Route("[action]/{idPostulante}/{idProcesoSeleccion}")]
        public ActionResult ObtenerEvaluacionesAsignadasEvaluador(int idPostulante, int idProcesoSeleccion)
        {
            var resultado = _evaluacionPostulanteService.ObtenerEvaluacionesAsignadasEvaluador(idPostulante, idProcesoSeleccion);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
		/// Autor: Flavio R.M.F.
		/// Fecha: 17/06/2024
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Preguntas y Respuestas Realizadas de Test Evaluador
		/// </summary>
		/// <returns> Lista Agrupada de ObjetoDTO : List<PreguntaTestDTO> </returns>
		[HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerPreguntasRespuestasRealizadasTestEvaluador([FromBody] TestInformacionDTO dto)
        {
            var resultado = _evaluacionPostulanteService.ObtenerPreguntasRespuestasRealizadasTestEvaluador(dto);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
		/// Autor: Flavio R.M.F.
		/// Fecha: 03/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Preguntas Respuestas Test de Evaluador
		/// </summary>
		/// <returns> Lista Agrupada de ObjetoDTO : List<PreguntaTestDTO> </returns>
		[HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerPreguntasRespuestasTestEvaluador([FromBody] TestInformacionDTO dto)
        {
            var resultado = _evaluacionPostulanteService.ObtenerPreguntasRespuestasTestEvaluador(dto);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R.M.F.
        /// Fecha: 17/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene evaluaciones de los cursos temporales.
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<EvaluacionesAsignadasEvaluador> </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerEvaluacionesPortalPostulante([FromBody] EvaluacionPostulanteFiltroReporteDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _evaluacionPostulanteService.ObtenerEvaluacionesPortalPostulante(Filtro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
		/// Autor: Flavio R.M.F
		/// Fecha: 19/06/2024
		/// Versión: 2.0
		/// <summary>
		/// Guarda respuestas realizadas por evaluador y califica etapa de evaluación
		/// </summary>
		/// <returns> Confirmación de inserción : Bool </returns>
		[HttpPost]
        [Route("[action]")]
        public ActionResult ActualizacionManualEtapaExamenAsignado([FromBody] CalificacionManualDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _evaluacionPostulanteService.ActualizacionManualEtapaExamenAsignado(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
		/// Autor: Flavio R.M.F.
		/// Fecha: 19/06/2024
		/// Versión: 2.0
		/// <summary>
		/// Guarda respuestas realizadas por evaluador y califica etapa de evaluación
		/// </summary>
		/// <returns> Confirmación de inserción : Bool </returns>
		[HttpPost]
        [Route("[action]")]
        public ActionResult EnviarRespuestasTest([FromBody] RespuestaEvaluacionEvaluadorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _evaluacionPostulanteService.EnviarRespuestasTest(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult ObtenerNotasMatriculaReporte([FromBody] List<int> idsPostulantes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _evaluacionPostulanteService.ObtenerNotasMatriculaReporte(idsPostulantes);
            return Ok(resultado);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult RestablecerNotas([FromBody] EnvioDatosReestablecerDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _evaluacionPostulanteService.RestablecerNotas(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R.M.F.
        /// Fecha: 24/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Registra Accesos, envia  correo a postulante para inserción en aula virtual
        /// </summary>
        /// <param name="dto"> Id de Postulante, Id de Examen y Usuario </param>
        /// <returns> Retorna StatusCodes, 200 si se eliminó existasamente con Bool de confirmación y Mensaje a Interfaz </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EnviarAccesoAulaVirtualPostulante([FromBody] EnviarAccesoPostulanteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = _evaluacionPostulanteService.EnviarAccesoAulaVirtualPostulante(dto, _tokenManager.UserName);
            return Ok(new
            {
                resultado.Respuesta,
                resultado.Mensaje
            });
        }

    }
}
