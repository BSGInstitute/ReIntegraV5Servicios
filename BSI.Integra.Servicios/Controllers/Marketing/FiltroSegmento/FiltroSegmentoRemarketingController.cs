using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.FacebookAudiencia;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.FacebookCuentaPublicitaria;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.FacebookAudiencia;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;


namespace BSI.Integra.Servicios.Controllers.Marketing.FiltroSegmentoRemarketing
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
    public class FiltroSegmentoRemarketingController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;
        public FiltroSegmentoRemarketingController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboFacebookAudiencia()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repFacebookAudienciaService = new FacebookAudienciaService(unitOfWork);
                return Ok(_repFacebookAudienciaService.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerHistorialAudiencia(int IdFiltroSegmento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repFacebookAudienciaService = new FacebookAudienciaService(unitOfWork);
                return Ok(_repFacebookAudienciaService.ObtenerHistorialPorIdFiltroSegmento(IdFiltroSegmento));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboFacebookCuentaPublicitaria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repFacebookCuentaPublicitariaService = new FacebookCuentaPublicitariaService(unitOfWork);
                return Ok(_repFacebookCuentaPublicitariaService.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosRemarketing()
        {
            try
            {
                var  _facebookAudienciaService=new FacebookAudienciaService(unitOfWork); 
                var _facebookCuentaPublicitariaService = new FacebookCuentaPublicitariaService(unitOfWork);

                FiltroSegmentoRemarketingCombosDTO filtroSegmentoRemarketingCombosDTO = new FiltroSegmentoRemarketingCombosDTO();

                filtroSegmentoRemarketingCombosDTO.ListaFacebookAudiencia = _facebookAudienciaService.ObtenerComboFacebookAudiencia();
                filtroSegmentoRemarketingCombosDTO.ListaFacebookCuentaPublicitaria =_facebookCuentaPublicitariaService.ObtenerComboFacebookCuentaPublicitaria();
                return Ok(filtroSegmentoRemarketingCombosDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaPublico()
        {
            try
            {

                var _facebookAudienciaService = new FacebookAudienciaService(unitOfWork);
                return Ok(_facebookAudienciaService.ObtenerComboListaPublico());
              
              

                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
