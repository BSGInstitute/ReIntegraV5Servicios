using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Marketing
{
    /// Controlador: ScoreController
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Endpoints relacionados al score predictivo de oportunidades de marketing.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsVista")]
    public class ScoreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("ObtenerP0PorIdOportunidad/{idOportunidad:int}")]
        public IActionResult ObtenerP0PorIdOportunidad(int idOportunidad)
        {
            try
            {
                IScorePrimeraOportunidadService scorePrimeraOportunidadService = new ScorePrimeraOportunidadService(_unitOfWork);
                var resultado = scorePrimeraOportunidadService.ObtenerP0PorIdOportunidad(idOportunidad);
                return Ok(resultado);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
