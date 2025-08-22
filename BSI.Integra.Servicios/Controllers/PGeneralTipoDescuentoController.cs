using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PGeneralTipoDescuentoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión de PGeneralTipoDescuento
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PGeneralTipoDescuentoController : ControllerBase
    {
        private IPGeneralTipoDescuentoService _pGeneralTipoDescuentoService;
        private ITokenManager _tokenManager;
        public PGeneralTipoDescuentoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _pGeneralTipoDescuentoService = new PGeneralTipoDescuentoService(unitOfWork);
        }
        /// Tipo Función: PUT
        /// Autor: GreteL canasa.
        /// Fecha: 11/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una asociacion basica a la tabla
        /// </summary>
        /// <param name="dto">Tipo Descuento Programa DTO</param>
        /// <returns></returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPut]
        public IActionResult AsociarPrograma([FromBody] TipoDescuentoProgramaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _pGeneralTipoDescuentoService.AsociarPrograma(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa
        /// Fecha: 06/07/2023
        /// Versión: 1.0
        /// <summary>
        [Route("[action]/{idPrograma}")]
        [HttpGet]
        public ActionResult ObtenerDescuentosPorPrograma(int idPrograma)
        {
            var resultado = _pGeneralTipoDescuentoService.ObtenerDescuentosPorPrograma(idPrograma);
            return Ok(new { IdTipoDescuentos = resultado });
        }
        /// Tipo Función: POST
        /// Autor: Gretel Canasa
        /// Fecha: 06/07/2023
        /// Versión: 1.0
        /// <summary>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public ActionResult AsociarDescuentos([FromBody] ProgramaTipoDescuentoDTO jsonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_pGeneralTipoDescuentoService.AsociarDescuentos(jsonDTO, _tokenManager.UserName));
        }
        /// Tipo Función: GET
        /// Autor: Klebert
        /// Fecha: 06/07/2023
        /// Versión: 1.0
        /// <summary>
        [HttpGet("[action]/{idTipoDescuento}")]
        public ActionResult ObtenerProgramaPorDescuento(int idTipoDescuento)
        {
            var descuentos = _pGeneralTipoDescuentoService.ObtenerProgramaPorDescuento(idTipoDescuento);
            return Ok(descuentos);
        }
    }
}
