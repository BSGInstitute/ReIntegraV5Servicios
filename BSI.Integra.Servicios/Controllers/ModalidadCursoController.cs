using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ModalidadCursoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión de ModalidadCurso
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ModalidadCursoController : ControllerBase
    {
        private IModalidadCursoService _servicio;
        public ModalidadCursoController(IUnitOfWork unitOfWork)
        {
            _servicio = new ModalidadCursoService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_ModalidadCurso para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombo()
        {
            return Ok(_servicio.ObtenerCombo());
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las modalidades curso
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult Obtener()
        {
            return Ok(_servicio.Obtener());
        }
    }
}
