using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: TipoReclamoAlumnoController
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/12/2022
    /// <summary>
    /// Gestión general de tipo de reclamo de alumno
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TipoReclamoAlumnoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public TipoReclamoAlumnoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los regitros de Tipo reclamo de alumno
        /// </summary> 
        /// <returns> Lista de Objeto DTO : List<ComboFiltroDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombo()
        {

            try
            {
                var tipoReclamoAlumnoService = new TipoReclamoAlumnoService(unitOfWork);
                var cmbOrigen = tipoReclamoAlumnoService.ObtenerCombo();
                return Ok(cmbOrigen);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
