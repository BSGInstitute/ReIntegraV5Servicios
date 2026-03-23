using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProyectoAplicacionController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public ProyectoAplicacionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("[action]/{idMatriculaCabecera}")]
        public IActionResult ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                var servicio = new ProyectoAplicacionService(unitOfWork);
                return Ok(servicio.ObtenerPorIdMatriculaCabecera(idMatriculaCabecera));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
