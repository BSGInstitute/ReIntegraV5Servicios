using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Finanza;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Finanzas
{
    /// Controlador: ReporteEstadoCuentaProveedorController
    /// Autor: Rodrigo Montesinos.
    /// Fecha: 12/01/2023
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class ReporteEstadoCuentaProveedorController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ReporteEstadoCuentaProveedorController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 12/01/2023
        /// Versión: 1.0
        /// <summary>
        /// retorna datos del reporte de estado de proveedor
        /// </summary>
        /// <returns>List<ReporteEstadoCuentaProveedorDTO></returns>
        [AllowAnonymous]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult VizualizarReporteEstadoCuentaProveedor([FromBody] ReporteEstadoCuentaProveedorFiltroDTO Filtro)
        {
            try
            {
                return Ok(new ReporteEstadoCuentaProveedorService(unitOfWork).VizualizarReporteEstadoCuentaProveedor(Filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 12/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de sedes
        /// </summary>
        /// <returns>List<SedeFiltroCiudadDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerComboListaSedes()
        {
            try
            {
                SedeService _repoSede = new SedeService(unitOfWork);
                var Lista = _repoSede.ObtenerComboListaSedes();
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 12/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de proveedores
        /// </summary>
        /// <returns>IEnumerable<ProveedorComboDTO></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerListaProveedores(StringDTO Texto)
       {
            try
            {
                ProveedorService _repoProveedor = new ProveedorService(unitOfWork);
                var Lista = _repoProveedor.ObtenerProveedorCombo(Texto.Valor);

                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 12/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de plan contable
        /// </summary>
        /// <returns>IEnumerable<PlanContableComboDTO></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerListaPlanContable(StringDTO NombreParcial)
        {
            try
            {
                PlanContableService _repoPlanContable = new PlanContableService(unitOfWork);
                var Lista = _repoPlanContable.ObtenerPlanContableFiltro(NombreParcial.Valor);
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 12/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de elemots de provedor
        /// </summary>
        /// <returns>string</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosProveedor(string NombreParcial)
        {
            try
            {
               return Ok(new PlanContableService(unitOfWork).ObtenerListaElementosProveedor(NombreParcial));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
