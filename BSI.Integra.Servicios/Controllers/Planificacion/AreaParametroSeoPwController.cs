using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[Authorize]
    public class AreaParametroSeoPwController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public AreaParametroSeoPwController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Klebert Layme.
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el informacion contenido de ParametroSeoPw por el id
        /// </summary> k
        /// <returns> List<ParametroContenidoDTO> </returns>
        [Route("[action]/{idAreaCapacitacion}")]
        [HttpGet]
        public ActionResult ObtenerContenidoParametroSEO(int idAreaCapacitacion)
        {
            try
            {
                IAreaParametroSeoPwService servicio = new AreaParametroSeoPwService(_unitOfWork);
                return Ok(servicio.ObtenerAreaParametrosSeoPorIdArea(idAreaCapacitacion));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
