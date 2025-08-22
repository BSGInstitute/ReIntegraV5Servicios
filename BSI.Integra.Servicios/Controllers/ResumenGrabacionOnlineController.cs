using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.Excel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ResumenGrabacionOnlineController
    /// Autor: Jorge Gamero
    /// Fecha: 10/02/2025
    /// <summary>
    /// Gestión general de ResumenGrabacionOnline
    /// </summary>
    [Route("api/ResumenGrabacionOnline")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ResumenGrabacionOnlineController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ResumenGrabacionOnlineController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// TipoFuncion: GET
        /// Autor: Jorge Gamero
        /// Fecha: 10/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener registros de ObtenerResumenGrabacionOnline 
        /// </summary>
        /// <param> Parametros de entrada </param>
        /// <returns>  </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerResumenGrabacionOnline()
        {
            try
            {
                var ResumenGrabacionOnlineService = new ResumenGrabacionOnlineService(unitOfWork);
                var resultado = ResumenGrabacionOnlineService.ObtenerResumenGrabacionOnline();
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jorge Gamero
        /// Fecha: 10/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener registros de ObtenerResumenGrabacionOnline filtrado por Id
        /// </summary>
        /// <param> Parametros de entrada </param>
        /// <returns>  </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerResumenGrabacionOnlinePorId(int id)
        {
            try
            {
                var ResumenGrabacionOnlineService = new ResumenGrabacionOnlineService(unitOfWork);
                var resultado = ResumenGrabacionOnlineService.ObtenerResumenGrabacionOnlinePorId(id);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
