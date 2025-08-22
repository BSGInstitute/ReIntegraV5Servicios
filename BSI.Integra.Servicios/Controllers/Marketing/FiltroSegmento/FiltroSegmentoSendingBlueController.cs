using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.FiltroSegmentoFolder;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.FiltroSegmentoTipoContacto;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.FiltroSegmentoFolder;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Servicios.Controllers.Marketing.FiltroSegmento
{
    /// Controlador: CampaniaMailingFiltradoController
    /// Autor: Rodrigo Montesinos.
    /// Fecha: 05/12/2022
    /// <summary>
    /// Gestión de Tipo de Dato
    /// </summary>

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FiltroSegmentoSendingBlueController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;
        public FiltroSegmentoSendingBlueController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
        {
            this.unitOfWork = unitOfWork;
            this.sendingblue = sendingblue;
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// retorna un lsitado de filtro segmento
        /// </summary>
        /// <returns>Retorna una lista de filtro segmento</returns>
        [HttpGet("ObtenerFiltroSegmentoPanel")]
        public IActionResult ObtenerFiltroSegmentoPanel()
        {
            try
            {
                var res = new FiltroSegmentoSendingBlueService(unitOfWork);
                var respuesta = res.ObtenerFiltroSegmentoPanel();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// obtener resultado de filtro segmento 
        /// </summary>
        /// <param name="id">identificador unico de filtro segmento</param>
        /// <param name="idFiltroSegmentoTipoContacto">identificador unico de filtro segmento tipo contacto</param>
        /// <returns>Retorna una lista de respuesta de filtro segmento</returns>
        [HttpGet("ObtenerResultadoFiltroSegmento/{id}/{idFiltroSegmentoTipoContacto}")]
        public IActionResult ObtenerResultadoFiltroSegmento(int id, int idFiltroSegmentoTipoContacto)
        {
            try
            {
                var res = new FiltroSegmentoSendingBlueService(unitOfWork);
                var respuesta = res.ObtenerResultadoFiltroSegmento(id,idFiltroSegmentoTipoContacto);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet("Recursivo")]
        public IActionResult EjecutarRecursivo()
        {
            try
            {
                new FiltroSegmentoSendingBlueService(unitOfWork).Ejecutarrecursivo();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


      

     
    }
}