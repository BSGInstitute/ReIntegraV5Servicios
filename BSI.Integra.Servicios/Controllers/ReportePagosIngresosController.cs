using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReportePagosIngresosController
    /// Autor: Gilmer Quispe
    /// Fecha: 15/12/2022
    /// <summary>
    /// Gestión general de reportes de ingresos
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReportePagosIngresosController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReportePagosIngresosController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 15/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatriculaAutocomplete([FromBody] Dictionary<string, string> filtros)
        {

            try
            {
                if (filtros != null)
                {
                    var matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                    if (filtros != null && filtros["Valor"] != null)
                    {
                        return Ok(matriculaCabeceraService.ObtenerCodigoMatriculaAutocompleto(filtros["Valor"].ToString()));
                    }
                    return Ok(matriculaCabeceraService.ObtenerCodigoMatriculaAutocompleto(""));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
