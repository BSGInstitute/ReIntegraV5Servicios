using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: MaterialPespecificoDetalleController
    /// Autor: Jonathan Caipo
    /// Fecha: 19/09/2023
    /// <summary>
    /// Gestion de Material de PEspecificoDetalle
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MaterialPespecificoDetalleController : ControllerBase
    {
        private IMaterialPespecificoDetalleService _materialPespecificoDetalleService;
        private ITokenManager _tokenManager;
        public MaterialPespecificoDetalleController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _materialPespecificoDetalleService = new MaterialPespecificoDetalleService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 19/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Sube archivos (materiales) por Programa Específico
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> bool </returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult SubirMaterialArchivo([FromForm] SubirMaterialPEspecificoDetalleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_materialPespecificoDetalleService.SubirMaterialArchivo(dto, _tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }
    }
}
