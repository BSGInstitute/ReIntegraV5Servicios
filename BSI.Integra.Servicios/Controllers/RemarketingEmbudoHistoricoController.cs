using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RemarketingEmbudoHistoricoController
    /// Autor: Max Mantilla Rodriguez.
    /// Fecha: 27/12/2025
    /// <summary>
    /// Gestión de Tipo de Dato T_RemarketingEmbudoHistorico
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RemarketingEmbudoHistoricoController : Controller
    {
        private IUnitOfWork unitOfWork;
        public RemarketingEmbudoHistoricoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodriguez.
        /// Fecha: 27/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Procesa la oportunidad para clasificación de remarketing en embudo histórico
        /// </summary>
        /// <param name="IdOportunidad">Identificador de la Oportunidad</param>
        /// <returns>Response 200 con el bool, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<bool>> EvaluarEmbudoRemarketing(DateTime? FechaCorte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var remarketingEmbudoHistorico = new RemarketingEmbudoHistoricoService(unitOfWork);

                var resultado = await remarketingEmbudoHistorico.EvaluarEmbudoRemarketing(FechaCorte);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
