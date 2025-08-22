using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: UrlSubContenedorController
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión de UrlSubContenedor
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class UrlSubContenedorController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public UrlSubContenedorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("ObtenerRutaSubContenedor/{idSubContenedor}")]
        public IActionResult ObtenerRutaSubContenedor(int idSubContenedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new UrlSubContenedorService(unitOfWork);
                return Ok(servicio.ObtenerRutaSubContenedor(idSubContenedor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
