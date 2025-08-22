using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: CrucigramaProgramaCapacitacionController
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 11/05/2023
    /// <summary>
    /// Gestión de FeedbackTipo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CrucigramaProgramaCapacitacionController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private ICrucigramaProgramaCapacitacionService _crucigramaProgramaCapacitacionService;
        public CrucigramaProgramaCapacitacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _crucigramaProgramaCapacitacionService = new CrucigramaProgramaCapacitacionService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_FeedbackTipo
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombos()
        {
            try
            {
                var resultado = _crucigramaProgramaCapacitacionService.ObtenerCombos();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
		/// Autor: Jonathan Caipo
		/// Fecha: 07/09/2023
		/// Versión: 1.0
		/// <summary>
		/// Obtiene el reporte de crucigramas para su exportación en excel
		/// </summary>
		/// <returns>Lista de objetos (PreguntaProgramaCapacitacionRegistradaDTO) con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerReporteCrucigramasExportacionExcel()
        {
            return Ok(_crucigramaProgramaCapacitacionService.ObtenerReporteCrucigramasExportacionExcel());
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los crucigramas
        /// </summary>
        /// <returns> Lista de objetos (CrucigramaProgramaCapacitacionDTO) con respuesta 200 o 400 con el mensaje de error </returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerCrucigramas()
        {
            return Ok(_crucigramaProgramaCapacitacionService.ObtenerCrucigramasRegistrados());
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
		/// Fecha: 07/09/2023
		/// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas de un crucigrama segun un id especifico
        /// </summary>
		/// <param name="idPreguntaProgramaCapacitacion">Id de la pregunta de programa capacitacion(PK de la tabla pla.T_CrucigramaProgramaCapacitacion)</param>
        /// <returns> Lista de objetos (CrucigramaProgramaCapacitacionDetalleDTO) con respuesta 200 o 400 con el mensaje de error </returns>
		[Route("[Action]/{idCrucigramaProgramaCapacitacion}")]
        [HttpGet]
        public IActionResult ObtenerRespuestasCrucigrama(int idCrucigramaProgramaCapacitacion)
        {
            return Ok(_crucigramaProgramaCapacitacionService.ObtenerRespuestaCrucigramaProgramaCapacitacion(idCrucigramaProgramaCapacitacion));
        }
        /// Tipo Función: DELETE
        /// Autor: Jonathan Caipo
		/// Fecha: 07/09/2023
		/// Versión: 1.0
        /// <summary>
        /// Elimina un crucigrama especifico
        /// </summary>
		/// <param name="id"></param>
        /// <returns> Booleano con respuesta 200 o 400 con el mensaje de error </returns>
		[Route("[Action]/{id}")]
        [Authorize]
        [HttpDelete]
        public IActionResult EliminarCrucigrama(int id)
        {
            try
            {
                return Ok(_crucigramaProgramaCapacitacionService.EliminarCrucigrama(id, _tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Jonathan Caipo
		/// Fecha: 07/09/2023
		/// Versión: 1.0
		/// <summary>
		/// Elimina una lista de crucigramas seleccionados
		/// </summary>
		/// <param name="ids">Lista de enteros</param>
		/// <returns> Booleano con respuesta 200 o 400 con el mensaje de error </returns>
		[Route("[Action]")]
        [Authorize]
        [HttpDelete]
        public IActionResult EliminarCrucigramasSeleccionados([FromBody] List<int> ids)
        {
            try
            {
                return Ok(_crucigramaProgramaCapacitacionService.EliminarCrucigramasSeleccionados(ids, _tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
		/// Fecha: 07/09/2023
		/// Versión: 1.0
        /// <summary>
        /// Funcion para importar desde un archivo CSV a la base de datos
        /// </summary>
		/// <param name="files">Objeto de tipo IFormFile con las preguntas</param>
        /// <returns> Booleano con respuesta 200 y la objeto anonimo con las propiedades({ Total, Correctos, Error, Errores }) o 400 con el mensaje de error </returns>
		[Route("[Action]")]
        [HttpPost]
        public IActionResult ImportarExcel([FromForm] IFormFile files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_crucigramaProgramaCapacitacionService.ImportarExcel(files));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Inserta un crucigrama especifico
        /// </summary>
		/// <param name="dto">Objeto de tipo CompuestoCrucigramaProgramaCapacitacionDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[Action]")]
        [HttpPost]
        public IActionResult InsertarCrucigrama([FromBody] CompuestoCrucigramaProgramaCapacitacionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(_crucigramaProgramaCapacitacionService.InsertarCrucigrama(dto, _tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Luis Huallpa - Jorge Rivera - Gian Miranda
        /// Fecha: 21/02/2021
        /// Versión: 1.5
        /// <summary>
        /// Actualiza un crucigrama en especifico
        /// </summary>
		/// <param name="dto">Objeto de tipo CompuestoCrucigramaProgramaCapacitacionDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
		[Route("[Action]")]
        [HttpPut]
        public IActionResult ActualizarCrucigrama([FromBody] CompuestoCrucigramaProgramaCapacitacionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(_crucigramaProgramaCapacitacionService.ActualizarCrucigrama(dto, _tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }
    }
}