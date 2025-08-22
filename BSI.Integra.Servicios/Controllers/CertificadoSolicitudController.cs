using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CertificadoSolicitudController
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/01/2023
    /// <summary>
    /// Gestión de CertificadoSolicitud
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CertificadoSolicitudController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CertificadoSolicitudController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de la entidad
        /// </summary> 
        /// <returns> List<CombotDTO> CertificadoTipo y CertificadoTipoPrograma </CombotDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                var certificadoTipoService = new CertificadoTipoService(unitOfWork);
                var certificadoTipoProgramaService = new CertificadoTipoProgramaService(unitOfWork);
                var filtros = new
                {
                    CertificadoTipo = certificadoTipoService.ObtenerCombo(),
                    CertificadoTipoPrograma = certificadoTipoProgramaService.ObtenerCombo()
                };
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
