using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RegistroLandingPageController
    /// Autor: Margiory Ramirez Neyra
    /// Fecha: 26/10/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RegistroLandingPageController : Controller
    {
        private IUnitOfWork unitOfWork;
        public RegistroLandingPageController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }




        /// TipoFuncion: GET
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 26/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera el Reporte Landing Page Facebook
        /// </summary>
        /// <param name="FechaInicial"> Fecha inicial. </param>
        /// <param name="FechaFinal"> Fecha final. </param>
        /// <returns> Lista de objetos: List<ReporteLandingPagePortalFacebookDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerLandingPageFacebook([FromBody] FiltroLandingPagePortalFacebookDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var _filtro = new FiltroLandingPagePortalFacebookDTO()
                //{
                //    FechaInicial = FechaInicial,
                //    FechaFinal = FechaFinal
                //};
                var servicioAsignacionAutomatica = new AsignacionAutomaticaService(unitOfWork);
                var listado = servicioAsignacionAutomatica.ObtenerReporteLandingPagePortalFacebook(filtro);
                var total = listado.Count() == 0 ? 0 : listado.FirstOrDefault().Total;
                if (listado != null)
                {
                    //??
                }
                //return Ok(listado);
                return Ok( listado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }




        /// TipoFuncion: Post
        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 11/05/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera los landing page del portal
        /// </summary>
        /// <param Entidad="filtro"> filtro. </param>
        /// <returns> Lista de objetos: List<ReporteLandingPagePortalDTO> </returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerLandingPagePortal([FromBody] FiltroLandingPagePortalDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var servicioAsignacionAutomatica = new AsignacionAutomaticaService(unitOfWork);
                var listado = servicioAsignacionAutomatica.ObtenerReporteLandingPagePortal(filtro);
                var total = listado.Count() == 0 ? 0 : listado.FirstOrDefault().Total;
                return Ok(listado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



    }
}
