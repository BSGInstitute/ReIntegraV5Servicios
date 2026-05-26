using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: MoodleCursoController
    /// Autor Creación: Gilmer Qm.
    /// Fecha: 02/05/2023
    /// <summary>
    /// Gestión de Moodle Curso
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[AllowAnonymous]
    public class MaestroPreguntaProgramaCapacitacionController : ControllerBase
    {
        private IPreguntaProgramaCapacitacionService _preguntaProgramaCapacitacionService;
        private ITokenManager _tokenManager;
        public MaestroPreguntaProgramaCapacitacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _preguntaProgramaCapacitacionService = new PreguntaProgramaCapacitacionService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1
        /// <summary>
        /// Obtiene el reporte de preguntas interactivas para su exportación en excel
        /// </summary>
        /// <returns> Lista de objetos (PreguntaProgramaCapacitacionRegistradaDTO) con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public async Task<ActionResult> ObtenerReportePreguntasInteractivasExportacionExcel()
        {
            try
            {
                var respuesta = await _preguntaProgramaCapacitacionService.ObtenerReportePreguntasInteractivasExportacionExcel();
                return Ok(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Obtiene las respuestas de una pregunta segun un id especifico
        /// </summary>
        /// <param name="idPreguntaProgramaCapacitacion">Id de la pregunta de programa capacitacion(PK de la tabla ope.T_PreguntaProgramaCapacitacion)</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[action]/{idPreguntaProgramaCapacitacion}")]
        [HttpGet]
        public ActionResult ObtenerRespuestasPregunta(int idPreguntaProgramaCapacitacion)
        {
            try
            {
                var respuesta = _preguntaProgramaCapacitacionService.ObtenerRespuestasPregunta(idPreguntaProgramaCapacitacion);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Obtiene los intentos que tiene la configuracion por pregunta
        /// </summary>
        /// <param name="idPreguntaIntento">Id del intento de pregunta(PK de la tabla gp.T_PreguntaIntento)</param>
        /// <returns>Lista de objeto de tipo (PreguntaIntentoDetalleDTO) con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[Action]/{idPreguntaIntento}")]
        [HttpGet]
        public ActionResult ObtenerIntentosPregunta(int idPreguntaIntento)
        {
            try
            {
                var res = _preguntaProgramaCapacitacionService.ObtenerPorIdPreguntaIntento(idPreguntaIntento);
                return Ok(res);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Elimina una pregunta especifica
        /// </summary>
        /// <param name="id"> (PK) a eliminar </param>
        /// <returns> bool </returns>
        [Route("[action]/{id}")]
        [HttpDelete]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _preguntaProgramaCapacitacionService.Eliminar(id, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Actualiza el dato y sus detalles
        /// </summary>
        /// <param name="compuestoPreguntaProgramaCapacitacionDTO"> Objeto y detalles </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] CompuestoPreguntaProgramaCapacitacionDTO compuestoPreguntaProgramaCapacitacionDTO)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _preguntaProgramaCapacitacionService.Actualizar(compuestoPreguntaProgramaCapacitacionDTO, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Qm
        /// Fecha: 26/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Inserta una pregunta especifica
        /// </summary>
		/// <param name="compuestoPreguntaProgramaCapacitacionDTO">Objeto de tipo CompuestoCrucigramaProgramaCapacitacionDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CompuestoPreguntaProgramaCapacitacionDTO compuestoPreguntaProgramaCapacitacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _preguntaProgramaCapacitacionService.Insertar(compuestoPreguntaProgramaCapacitacionDTO, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Elimina una lista de PreguntaProgramaCapacitacion y sus detalles
        /// </summary>
        /// <param name="id"> Lista de (PK) a eliminar </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar(List<int> id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var respuesta = _preguntaProgramaCapacitacionService.Eliminar(id, registroClaimToken.UserName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Obtiene los combos necesarios para el funcionamiento del modulo
        /// </summary>
        /// <returns>Objeto anonimo { PGeneral, TipoMarcador, ProgramaEspecifico }</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosModulo()
        {
            try
            {
                var respuesta = _preguntaProgramaCapacitacionService.ObtenerCombosModulo();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Funcion para obtener la lista de capitulos y sus sesiones respectivas con relacion al programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Booleano con respuesta 200 y la lista de objeto (CapituloSesionProgramaCapacitacionDTO) o 400 con el mensaje de error</returns>
        [Route("[Action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerCapituloSesionesPGeneral(int idPGeneral)
        {
            try
            {
                var respuesta = _preguntaProgramaCapacitacionService.ObtenerCapituloSesionesPGeneral(idPGeneral);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: Put
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza por el IdPGeneral y GrupoPregunta
        /// </summary>
        /// <param name="grupoPreguntaProgramaCapacitacions"> Lista de objetos a actualizarse </param> 
        /// <returns> Booleano con respuesta 200  o 400 con el mensaje de error </returns> 
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarRespuestaPorSecuenciaVideo([FromBody] List<GrupoPreguntaProgramaCapacitacionDTO> grupoPreguntaProgramaCapacitacions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _preguntaProgramaCapacitacionService.ActualizarRespuestaPorSecuenciaVideo(grupoPreguntaProgramaCapacitacions, _tokenManager.UserName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: Get
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros filtrados por la estructura 
        /// </summary>
        /// <param name="idPgeneral"> (PK) de T_PGeneral </param> 
        /// <param name="grupoPregunta"> GrupoPregunta de T_PreguntaProgramaCapacitacion </param> 
        /// <returns> Booleano con respuesta 200  o 400 con el mensaje de error </returns> 
        [Route("[action]/{idPgeneral}/{grupoPregunta}")]
        [HttpGet]
        public IActionResult ObtenerPorEstructura(int idPgeneral, string grupoPregunta)
        {
            try
            {
                var respuesta = _preguntaProgramaCapacitacionService.ObtenerPorEstructura(idPgeneral, grupoPregunta);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo: GET
        /// Metodo Http: Get
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// Versión: 1.5
        /// <summary>
        /// Obtiene las preguntas
        /// </summary>
        /// <returns>Lista de objetos (PreguntaProgramaCapacitacionRegistradaDTO) con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult Obtener()
        {
            try
            {
                var registros = _preguntaProgramaCapacitacionService.ObtenerPreguntasRegistradas();
                return Ok(registros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1.5
        /// <summary>
        /// Funcion para importar desde un archivo CSV a la base de datos
        /// </summary>
        /// <param name="archivo">Objeto de tipo IFormFile con las preguntas</param>
        /// <returns>Booleano con respuesta 200 y la objeto anonimo con las propiedades({ Total, Correctos, Error, Errores }) o 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ImportarExcel([FromForm] IFormFile archivo)
        {
            try
            {
                var respuesta = _preguntaProgramaCapacitacionService.ImportarExcel(archivo, _tokenManager.UserName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Qm
        /// Fecha: 03/08/2023
        /// Versión: 1.5
        /// <summary>
        /// Funcion para importar desde un archivo CSV a la base de datos
        /// </summary>
        /// <param name="importarRespuestasExcel">Objeto de tipo IFormFile con las preguntas</param>
        /// <returns>Booleano con respuesta 200 y la objeto anonimo con las propiedades({ Total, Correctos, Error, Errores }) o 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ImportarRespuestasExcel([FromForm] ImportarRespuestasExcelDTO importarRespuestasExcel)
        {
            try
            {
                var respuesta = _preguntaProgramaCapacitacionService.ImportarRespuestasExcel(importarRespuestasExcel, _tokenManager.UserName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Humberto Oscata
        /// Fecha: 14/05/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el listado de niveles de dificultad disponibles para preguntas
        /// </summary>
        /// <returns>Lista de objetos (PreguntaProgramaCapacitacionDificultadDTO) con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerDificultades()
        {
            try
            {
                var registros = _preguntaProgramaCapacitacionService.ObtenerDificultades();
                return Ok(registros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: PUT
        /// Autor: Humberto Oscata
        /// Fecha: 14/05/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el nivel de dificultad de una pregunta de programa de capacitacion
        /// </summary>
        /// <param name="actualizarDificultadPreguntaDTO">DTO con Id de la pregunta e Id del nivel de dificultad</param>
        /// <returns>true con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPut]
        public IActionResult ActualizarDificultad([FromBody] ActualizarDificultadPreguntaDTO actualizarDificultadPreguntaDTO)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                _preguntaProgramaCapacitacionService.ActualizarDificultad(actualizarDificultadPreguntaDTO, registroClaimToken.UserName);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Humberto Oscata
        /// Fecha: 14/05/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el nivel de dificultad asociado a una pregunta de programa de capacitacion
        /// </summary>
        /// <param name="id">(PK) de la pregunta</param>
        /// <returns>Objeto (DificultadPorPreguntaDTO) con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerDificultadPorIdPregunta(int id)
        {
            try
            {
                var resultado = _preguntaProgramaCapacitacionService.ObtenerDificultadPorIdPregunta(id);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
