using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult InsertarDocumento([FromBody] CompuestoDocumentoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_documentoPwService.InsertarDocumento(dto, _tokenManager.UserName));
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
        public IActionResult ActualizarDocumento([FromBody] CompuestoDocumentoPwDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_documentoPwService.ActualizarDocumento(dto, _tokenManager.UserName));
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



    }
}
