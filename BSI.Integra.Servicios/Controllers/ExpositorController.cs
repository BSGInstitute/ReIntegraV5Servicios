using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ExpositorController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión de Expositor
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ExpositorController : ControllerBase
    {
        private IExpositorService _expositorService;
        private ITokenManager _tokenManager;
        public ExpositorController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _expositorService = new ExpositorService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: Delete
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/08/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina expositor por id
        /// </summary>
        /// <param name="idExpositor">Id Expositor</param>
        /// <returns></returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]/{idExpositor}")]
        [HttpDelete]
        public ActionResult Eliminar(int idExpositor)
        {
            var resultado = _expositorService.Eliminar(idExpositor, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: PUT
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] ExpositorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _expositorService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad ExpositorDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        //[Authorize]
        ///[JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] ExpositorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _expositorService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/08/2023
        /// Version: 1.0
        /// <summary>
        /// Registra el archivo en el blob storage
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public IActionResult RegistrarArchivoFotoExpositor([FromForm] IFormFile Files)
        {
            var resultado = _expositorService.RegistrarArchivoFotoExpositor(Files);
            if (resultado.UrlArchivo == null && resultado.NombreArchivo == null)
            {
                return Ok(new { Resultado = "Error" });
            }
            else
            {
                return Ok(new { Resultado = "Ok", resultado.UrlArchivo, resultado.NombreArchivo });
            }
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los combos para el modulo expositores
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCombosModulo()
        {
            var resultado = await _expositorService.ObtenerCombosModulo();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de expositores
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult Obtener()
        {
            var resultado = _expositorService.Obtener();
            if (_tokenManager.UserName == "AdminInst")
            {
                resultado = resultado.Where(x => x.DocenteInstituto == true).ToList();
                
            }
            return Ok(resultado);
        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco Garcia
        /// Fecha: 26/06/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de expositores
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombo()
        {
            var resultado = _expositorService.ObtenerCombo();
            return Ok(resultado);
        }

        /// Tipo Función: POST
        /// Autor: Claude Code
        /// Fecha: 2026-03-30
        /// Version: 1.0
        /// <summary>
        /// Busca expositores por email, celular o nro de documento para vincular con proveedores docentes.
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public IActionResult BuscarPorContacto([FromBody] BuscarExpositorContactoDTO dto)
        {
            var resultado = _expositorService.BuscarPorContacto(dto.Email, dto.Celular, dto.NroDocumento);
            return Ok(resultado);
        }
    }
}
