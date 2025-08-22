using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: TipoDatoController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TipoCambioEntreMonedaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public TipoCambioEntreMonedaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Tipo Función: GET
        /// Autor: Margiory Ramirez.
        /// Fecha:30/01/2023
        /// Versión: 1.0
        /// <summary>
        ///
        /// </summary>
      
        [HttpGet("[Action]")]
        public IActionResult ObtenerParaFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new TipoCambioEntreMonedaService(unitOfWork);
                return Ok(servicio.ObtenerParaFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
