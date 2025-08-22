using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{

    /// Controlador: TamanioEmpresaController
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/12/2022
    /// <summary>
    /// Gestión de Tipo TamanioEmpresa
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TamanioEmpresaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public TamanioEmpresaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Metodo: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con el estado = 1
        /// </summary> 
        /// <returns> List<TamanioEmpresaComboDTO> </returns> 
        /// 
		[Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                var tamanioEmpresaService = new TamanioEmpresaService(unitOfWork);
                return Ok(tamanioEmpresaService.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
