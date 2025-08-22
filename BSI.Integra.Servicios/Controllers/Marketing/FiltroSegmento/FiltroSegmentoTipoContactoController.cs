using BSI.Integra.Aplicacion.Marketing.Service.Implementacion. FiltroSegmentoTipoContacto;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Servicios.Controllers.Marketing. FiltroSegmentoTipoContacto
{
    /// Controlador: CampaniaMailingFiltradoController
    /// Autor: Rodrigo Montesinos.
    /// Fecha: 05/12/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class  FiltroSegmentoTipoContactoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;
        public  FiltroSegmentoTipoContactoController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
        {
            this.unitOfWork = unitOfWork;
            this.sendingblue = sendingblue;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repFiltroSegmentoTipoContactoService = new FiltroSegmentoTipoContactoService(unitOfWork);
                return Ok(_repFiltroSegmentoTipoContactoService.ObtenerTodoFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
