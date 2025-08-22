using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CriterioCalificaciónController
    /// Autor: Jonathan Caipo
    /// Fecha: 05/10/2022
    /// <summary>
    /// Gestión de Criterio para la  Calificación
    /// </summary>
    [Route("api/CriterioCalificacion")]
    [EnableCors("CorsVista")]
    public class CriterioCalificacionController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CriterioCalificacionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repCriterioCalificacion = new CriterioCalificacionService(unitOfWork);
                return Ok(_repCriterioCalificacion.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
