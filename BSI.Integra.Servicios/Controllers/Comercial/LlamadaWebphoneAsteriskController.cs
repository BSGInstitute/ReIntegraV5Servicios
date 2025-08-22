using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: LlamadaWebphoneAsterisk
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 18/02/2023
    /// <summary>
    /// Gestión de TLlamadaWebphoneAsterisk
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class LlamadaWebphoneAsteriskController : Controller
    {
        private IUnitOfWork unitOfWork;
        public LlamadaWebphoneAsteriskController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/08/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idOportunidad">Id del Alumno</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult RegularizarContadorCdrId()
        {
            try
            {
                LlamadaWebphoneAsteriskService service = new LlamadaWebphoneAsteriskService(unitOfWork);
                service.RegularizarContadorCdrId();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
