using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Security.Claims;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ConfigurarVideoProgramaController
    /// Autor:  Gilmer Quispe
    /// Fecha: 18/04/2023
    /// <summary>
    /// Gestion general de ConfigurarVideoPrograma
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfigurarVideoProgramaController : Controller
    {
        private ITokenManager _tokenManager;
        private IConfigurarVideoProgramaService _configurarVideoProgramaService;
        public ConfigurarVideoProgramaController(IUnitOfWork unitOfWork , ITokenManager tokenManager)
        {
            _configurarVideoProgramaService = new ConfigurarVideoProgramaService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Importa el archivo Excel de la seccion ConfigurarSecuenciaVideo
        /// </summary>
        /// <param name="ArchivoExcel">IFormFile a importar de formato Excel</param> 
        /// <returns> int </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ImportarExcelConfigurarSecuenciaVideo([FromForm] IFormFile ArchivoExcel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _configurarVideoProgramaService.ImportarExcelConfigurarSecuenciaVideo(ArchivoExcel, registroClaimToken.UserName);

                return Ok(new { CantidadCorrecto = respuesta.cantidadCorrecto, CantidadIncorrecto = respuesta.cantidadIncorrecto });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion video programa por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param> 
        /// <returns> List<EstructuraCapituloProgramaAlternoDTO> </returns>
        [Route("[Action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionVideoPrograma(int idPGeneral)
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerConfiguracionVideoPrograma(idPGeneral);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion video programa Examen  por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param> 
        /// <returns> List<EstructuraCapituloProgramaAlternoDTO> </returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionExamenPrograma(int idPGeneral)
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerConfiguracionExamenPrograma(idPGeneral);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el enuncuado pregunta por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param> 
        /// <returns> List<PreguntaPorProgramaDTO> </returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerEnunciadoPregunta(int idPGeneral)
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerEnunciadoPreguntaPorIdPGeneral(idPGeneral);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de DocumentoProgramaGeneralTrabajosEvaluacion 
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerDocumentoProgramaGeneral(int idPGeneral)
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerDocumentoProgramaGeneral(idPGeneral);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de DocumentoProgramaGeneralTrabajosEvaluacion 
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionProyecto(int idPGeneral)
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerConfigurarProyecto(idPGeneral);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion de sesiones y programas
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idDocumentoSeccionPw">Id de la seccion del documento (PK de la tabla pla.T_DocumentoSeccion_PW)</param>
        /// <param name="numeroFila">Numero de fila de la configuracion del programa general</param>
        /// <returns>Response 200 (objeto de clase ConfigurarVideoProgramaDTO) o 200 sin objeto</returns>
        [Route("[Action]/{idPGeneral}/{idDocumentoSeccionPw}/{numeroFila}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionSesionPrograma(int idPGeneral, int idDocumentoSeccionPw, int numeroFila)
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerConfiguracionSesionPrograma(idPGeneral, idDocumentoSeccionPw, numeroFila);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion de preguntas estructura
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="seccion">Id de la seccion del documento (PK de la tabla pla.T_DocumentoSeccion_PW)</param>
        /// <param name="fila">Numero de fila de la configuracion del programa general</param>
        /// <returns>Response 200 (objeto de clase List<GrupoPreguntaProgramaCapacitacionDTO>) o 200 sin objeto</returns>
        [Route("[action]/{idPGeneral}/{seccion}/{fila}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionPreguntasEstructura(int idPGeneral, int seccion, int fila)
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerConfiguracionPreguntasEstructura(idPGeneral, seccion, fila);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Gilmer Qm
        /// Fecha: 14/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina Configuracion del Video
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[Action]/{idPGeneral}")]
        [HttpDelete]
        public ActionResult EliminarConfiguracionPrograma(int idPGeneral)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _configurarVideoProgramaService.EliminarConfiguracionPrograma(idPGeneral, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Descarga la plantilla para llenar de ConfigurarSecuenciaVideo
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Response 200 (Archivo Excel) o 400, dependiendo del flujo</returns>
        [Route("[Action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult DescargarPlantillaExcelConfigurarSecuenciaVideo(int idPGeneral)
        {
            try
            {
                byte[] archivo = _configurarVideoProgramaService.ObtenerPlantillaExcelConfigurarSecuenciaVideo(idPGeneral);
                string nombreArchivo = string.Concat("PlantillaExcelConfigurarSecuenciaVideo-", idPGeneral, ".xlsx");
                return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/07/2023
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <param name="idSeccion"> (PK) T_Seccion_PW </param>
        /// <param name="fila"> numero de fila </param>
        /// <summary>
        /// Obtiene el el registro con detalles por el IdPGeneral
        /// </summary>
        [Route("[action]/{idPGeneral}/{idSeccion}/{fila}")]
        [HttpGet]
        public ActionResult ObtenerConfigurarEvaluacionTrabajoPorConfiguracion(int idPGeneral, int idSeccion, int fila)
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerConfigurarEvaluacionTrabajoPorConfiguracion(idPGeneral, idSeccion, fila);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: POST.
        /// Autor: Gilmer Qm
        /// Fecha: 17/07/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la ta tabla y sus detalles
        /// </summary>   
        /// <param name="configurarVideoProgramaDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] ConfigurarVideoProgramaDTO configurarVideoProgramaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _configurarVideoProgramaService.Insertar(configurarVideoProgramaDTO, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: PUT.
        /// Autor: Gilmer Qm
        /// Fecha: 17/07/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la ta tabla y sus detalles
        /// </summary>   
        /// <param name="configurarVideoProgramaDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPut]
        public IActionResult Actualizar([FromBody] ConfigurarVideoProgramaDTO configurarVideoProgramaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _configurarVideoProgramaService.Actualizar(configurarVideoProgramaDTO, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: Delete.
        /// Autor: Gilmer Qm
        /// Fecha: 31/07/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina el registro y sus detalles
        /// </summary>   
        /// <param name="videoId"> VideoId de T_ConfigurarVideoPrograma </param>
        /// <returns> bool </returns>
        [Route("[action]/{videoId}")]
        [HttpDelete]
        public IActionResult EliminarSesionConfigurarVideo(string videoId)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _configurarVideoProgramaService.EliminarSesionConfigurarVideoPorVideoId(videoId, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET 
        /// Autor: Gilmer Qm
        /// Fecha: 31/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Descarga la plantilla para llenar de ConfiguracionDeVideo
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[Action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult DescargarPlantillaExcel(int idPGeneral)
        {
            try
            {
                byte[] archivo = _configurarVideoProgramaService.ObtenerPlantillaExcelConfiguracionDeVideo(idPGeneral);
                string nombreArchivo = string.Concat("PlantillaExcelConfiguracionDeVideo-", idPGeneral, ".xlsx");
                return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Importa el archivo Excel de la seccion ConfiguracionDeVideo
        /// </summary>
        /// <param name="ArchivoExcel">IFormFile a importar de formato Excel</param> 
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ImportarExcel([FromForm] IFormFile ArchivoExcel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _configurarVideoProgramaService.ImportarExcel(ArchivoExcel, registroClaimToken.UserName);
                return Ok(new { CantidadCorrecta = respuesta.cantidadCorrecto, CantidadIncorrecta = respuesta.cantidadIncorrecto });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1
        /// <summary>
        /// Obtiene los combos necesarios para el funcionamiento del modulo
        /// </summary>
        /// <returns>Objeto anonimo { Capitulo (Lista de objeto de clase PreEstructuraCapituloProgramaBO), Sesion (Lista de objeto de clase PreEstructuraCapituloProgramaBO) }</returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerCombosModulo()
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerCombosModulo();
                return Ok(new { EstructuraProgramaCapitulo = respuesta.estructuraProgramaCapitulo, EstructuraProgramaSesion = respuesta.estructuraProgramaSesion });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarDescargaReproduccionVideo([FromBody] ActualizarDescargaReproduccionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _configurarVideoProgramaService.ActualizarDescargaReproduccionVideo(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }




        [HttpGet("[action]/{idPGeneral}")]
        public IActionResult ObtenerConteosdeVideosTipo(int idPGeneral)
        {
            var resultado = _configurarVideoProgramaService.ObtenerConteosdeVideosTipo(idPGeneral);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla
        /// Fecha: 2026-01-23
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion video programa por el IdPGeneral para Tutor Virtual
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param> 
        /// <returns> List<EstructuraCapituloProgramaAlternoDTO> </returns>
        [Route("[Action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionTutorVirtualAonline(int idPGeneral)
        {
            try
            {
                var respuesta = _configurarVideoProgramaService.ObtenerConfiguracionTutorVirtualAonline(idPGeneral);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Max Mantilla
        /// Fecha: 2026-01-26
        /// Version: 1.0
        /// <summary>
        /// Proceso para generar base de conocimiento en Tutor Virtual
        /// </summary>
        /// <param IniciarProcesoResumenGrabacionesDTO> Parametros de entrada </param>
        /// <returns> Task<(string resultado, HttpStatusCode statusCode)> </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> ProcesarTutorVirtualAonline(ProcesamientoVideosAonlineEnvioDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = await _configurarVideoProgramaService.ProcesarTutorVirtualAonline(datos, registroClaimToken.UserName);
                return StatusCode((int)resultado.statusCode, resultado.resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Max Mantilla
        /// Fecha: 2026-01-26
        /// Version: 1.0
        /// <summary>
        /// Envio de correo cuando culmina el procesamiento de videos para Tutor Virtual
        /// </summary>
        /// <param IniciarProcesoResumenGrabacionesDTO> Parametros de entrada </param>
        /// <returns> Task<(string resultado, HttpStatusCode statusCode)> </returns>
        [HttpPost("[action]")]
        public ActionResult EnviarCorreoProcesamientoVideosTutorAonline(ProcesamientoVideosAonlineEnvioCorreoDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _configurarVideoProgramaService.EnviarCorreoProcesamientoVideosTutorAonline(datos);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
