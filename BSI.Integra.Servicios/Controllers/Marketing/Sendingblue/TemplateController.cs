using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Servicios.Controllers.Marketing.Sendingblue
{
    /// Controlador: Template
    /// Autor: Rodrigo Montesinos Paredes.
    /// Fecha: 11/22/20222
    /// <summary>
    /// Gestion de los templates de sendinblue
    /// </summary>
    [Authorize]
    [Route("api/marketing/sendinblue/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TemplateController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;
        public TemplateController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
        {
            this.unitOfWork = unitOfWork;
            this.sendingblue = sendingblue;
        }

        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la obtencion de los templates desde sendinblue
        /// </summary>
        /// <param name="limit">Cantidad maxima de registros a obtener</param>
        /// <param name="offset">Principo de donde sera obtenidos los registros</param>
        /// <returns>Una RespuestaGenerica </returns>
        [HttpGet("offset/{limit}/{offset}")]
        public IActionResult ObtenerTemplates(int limit,int offset)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.ObtenerTemplate(limit, offset,"desc", true);
                return Ok(respuesta);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
