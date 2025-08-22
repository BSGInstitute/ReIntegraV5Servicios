using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: TiempoLibreController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión de TiempoLibre
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TiempoLibreController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public TiempoLibreController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
    }
}
