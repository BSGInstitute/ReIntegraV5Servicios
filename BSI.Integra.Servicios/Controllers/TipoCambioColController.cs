using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Api.Ads.AdWords.v201809;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: TipoCambioColController
    /// Autor: Jorge Gamero.
    /// Fecha: 20/09/2024
    /// <summary>
    /// Gestión de TipoCambioCol
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TipoCambioColController : Controller
    {
        private IUnitOfWork unitOfWork;
        public TipoCambioColController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 21/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene último registro de cambio de Colombia
        /// </summary>
        /// <returns> TipoCambioColService </returns>
        [Route("[action]/{fecha}")]
        [HttpGet]
        public ActionResult ObtenerPesosDolaresTipoCambioColombia(string fecha)
        {
            try
            {
                var tipoCambioColService = new TipoCambioColService(unitOfWork);
                var resultado = tipoCambioColService.ObtenerPesosDolaresTipoCambioColombia(fecha);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
