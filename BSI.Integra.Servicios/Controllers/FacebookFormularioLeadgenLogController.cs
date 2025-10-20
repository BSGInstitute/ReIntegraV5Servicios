using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FacebookFormularioLeadgenLogController
    /// Autor: Max Mantilla Rodriguez.
    /// Fecha: 09/10/2025
    /// <summary>
    /// Gestión de Tipo de Dato T_FacebookFormularioLeadgenLog
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FacebookFormularioLeadgenLogController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FacebookFormularioLeadgenLogController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodriguez.
        /// Fecha: 09/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Procesa la oportunidad para envío a Facebook por CRM
        /// </summary>
        /// <param name="IdOportunidad">Identificador de la Oportunidad</param>
        /// <returns>Response 200 con el bool, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<bool>> EvaluarConversionFacebook(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var facebookFormularioLeadgenLog = new FacebookFormularioLeadgenLogService(unitOfWork);

                facebookFormularioLeadgenLog.EvaluarConversionFacebook(IdOportunidad);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
