using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers.Marketing.WhatsApp
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReemplazoDeEtiquetasController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ReemplazoDeEtiquetasController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 03/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesa los responsables Whatsapp de la prioridad Mailing General - Individual
        /// </summary>
        /// <param name="PreprocesamientoWhatsAppCampaniaGeneral">Objeto de tipo PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO</param>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarPreProcesamientoWhatsApp([FromBody] PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO PreprocesamientoWhatsAppCampaniaGeneral)
        {
            try
            {

                return Ok(new WhatsAppRemplazoEtiquetaService(unitOfWork).FinalizarPreProcesamientoWhatsApp(PreprocesamientoWhatsAppCampaniaGeneral));
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Resultado = "ERROR",
                    ex.Message
                });
            }
        }
    }
}
