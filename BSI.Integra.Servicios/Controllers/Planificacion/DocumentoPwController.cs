using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: DocumentoPwController
    /// Autor: Jonathan Caipo
    /// Fecha: 20/09/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión de PGeneralDocumentoPw
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class DocumentoPwController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IDocumentoPwService _documentoPwService;
        public DocumentoPwController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _documentoPwService = new DocumentoPwService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo de de T_DocumentoPW en orden descendente
        /// </summary>
        /// <returns> Lista DTO - List<DocumentoPwDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerTodo()
        {
            try
            {
                return Ok(_documentoPwService.Obtener());
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta documento
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> InsertarDocumento(
            [FromForm] string dto,
            IFormFile? urlArchivoInstruccionTarea,
            IFormFile? urlArchivoCalificacionExcelente)
        {
            if (string.IsNullOrEmpty(dto))
                return BadRequest("El parámetro dto es requerido.");

            CompuestoDocumentoDTO compuestoDto;
            try
            {
                compuestoDto = JsonConvert.DeserializeObject<CompuestoDocumentoDTO>(dto)!;
            }
            catch
            {
                return BadRequest("El parámetro dto no tiene un formato JSON válido.");
            }

            try
            {
                var documento = await _documentoPwService.InsertarDocumento(
                    compuestoDto,
                    urlArchivoInstruccionTarea,
                    urlArchivoCalificacionExcelente,
                    _tokenManager.UserName);

                return Ok(documento);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el documento
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPut]
        public async Task<IActionResult> ActualizarDocumento(
            [FromForm] string dto,
            IFormFile? urlArchivoInstruccionTarea,
            IFormFile? urlArchivoCalificacionExcelente)
        {
            if (string.IsNullOrEmpty(dto))
                return BadRequest("El parámetro dto es requerido.");

            CompuestoDocumentoPwDTO compuestoDto;
            try
            {
                compuestoDto = JsonConvert.DeserializeObject<CompuestoDocumentoPwDTO>(dto)!;
            }
            catch
            {
                return BadRequest("El parámetro dto no tiene un formato JSON válido.");
            }

            try
            {
                var documento = await _documentoPwService.ActualizarDocumento(
                    compuestoDto,
                    urlArchivoInstruccionTarea,
                    urlArchivoCalificacionExcelente,
                    _tokenManager.UserName);

                return Ok(documento);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina documento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[Action]/{id}")]
        [HttpDelete]
        public IActionResult EliminarDocumento(int id)
        {
            try
            {
                return Ok(_documentoPwService.EliminarDocumento(id, _tokenManager.UserName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{idDocumentoPW}")]
        public IActionResult ObtenerIntroduccionVersionDocumento(int idDocumentoPW)
        {
            var resultado = _documentoPwService.ObtenerIntroduccionVersionDocumento(idDocumentoPW);
            return Ok(resultado);
        }

        [HttpGet("[action]")]
        public IActionResult ObtenerModalidadPortal()
        {
            var resultado = _documentoPwService.ObtenerModalidadPortal();
            return Ok(resultado);
        }
        [HttpGet("[action]")]
        public IActionResult ObtenerModoFechaInicio()
        {
            var resultado = _documentoPwService.ObtenerModoFechaInicio();
            return Ok(resultado);
        }

        [HttpGet("[action]")]
        public IActionResult ObtenerNotasTipo()
        {
            var resultado = _documentoPwService.ObtenerNotasTipo();
            return Ok(resultado);
        }


        [HttpGet("[action]/{idDocumentoPW}")]
        public IActionResult ObtenerDocumentoPWModalidad(int idDocumentoPW)
        {
            var resultado = _documentoPwService.ObtenerDocumentoPWModalidad(idDocumentoPW);
            return Ok(resultado);
        }

        [HttpGet("[action]/{idDocumentoPW}")]
        public IActionResult ObtenerDocumentoPWDuracion(int idDocumentoPW)
        {
            var resultado = _documentoPwService.ObtenerDocumentoPWDuracion(idDocumentoPW);
            return Ok(resultado);
        }


        [HttpGet("[action]/{idDocumentoPW}")]
        public IActionResult ObtenerDocumentoPWFechaInicio(int idDocumentoPW)
        {
            var resultado = _documentoPwService.ObtenerDocumentoPWFechaInicio(idDocumentoPW);
            return Ok(resultado);
        }

        [HttpGet("[action]/{idDocumentoPW}")]
        public IActionResult ObtenerDocumentoPWNotas(int idDocumentoPW)
        {
            var resultado = _documentoPwService.ObtenerDocumentoPWNotas(idDocumentoPW);
            return Ok(resultado);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SubirArchivoDocumentoPw(
            [FromForm] int id,
            IFormFile? urlArchivoInstruccionTarea,
            IFormFile? urlArchivoCalificacionExcelente)
        {
            if (urlArchivoInstruccionTarea == null && urlArchivoCalificacionExcelente == null)
                return BadRequest("Debe enviar al menos un archivo.");

            try
            {
                string urlInstruccion = string.Empty;
                string urlCalificacion = string.Empty;

                if (urlArchivoInstruccionTarea != null && urlArchivoInstruccionTarea.Length > 0)
                    urlInstruccion = await _documentoPwService.SubirArchivoDocumentoPw(id, urlArchivoInstruccionTarea, nameof(urlArchivoInstruccionTarea), _tokenManager.UserName);

                if (urlArchivoCalificacionExcelente != null && urlArchivoCalificacionExcelente.Length > 0)
                    urlCalificacion = await _documentoPwService.SubirArchivoDocumentoPw(id, urlArchivoCalificacionExcelente, nameof(urlArchivoCalificacionExcelente), _tokenManager.UserName);

                return Ok(new { urlArchivoInstruccionTarea = urlInstruccion, urlArchivoCalificacionExcelente = urlCalificacion });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
