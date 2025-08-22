using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ParametroSeoPwController
    /// Autor Creación: Gilmer Qm.
    /// Fecha: 02/05/2023
    /// <summary>
    /// Gestión de Moodle Categoria
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[AllowAnonymous]
    public class ParametroSeoPwController : Controller
    {
        private IUnitOfWork unitOfWork;

        public ParametroSeoPwController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm.
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de T_ParametroSeoPw
        /// </summary> 
        /// <returns> List<ParametroSeoPwDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                IParametroSeoPwService parametroSeoPwService = new ParametroSeoPwService(unitOfWork);
                return Ok(parametroSeoPwService.ObtenerCombo());
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
