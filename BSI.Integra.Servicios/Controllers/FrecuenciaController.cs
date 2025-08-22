using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FrecuenciaController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de Frecuencia
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FrecuenciaController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public FrecuenciaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Frecuencia
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerFrecuencia")]
        public IActionResult ObtenerFrecuencia()
        {
            return Ok(_unitOfWork.FrecuenciaRepository.ObtenerFrecuencia());
        }
    }
}
