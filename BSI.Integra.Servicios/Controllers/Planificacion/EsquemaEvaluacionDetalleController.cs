
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class EsquemaEvaluacionDetalleController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private ITokenManager _tokenManager;
        private IEsquemaEvaluacionDetalleService _esquemaEvaluacionDetalleService;

        public EsquemaEvaluacionDetalleController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _esquemaEvaluacionDetalleService = new EsquemaEvaluacionDetalleService(_unitOfWork);
        }
        [Route("[Action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerListadoDetalle(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_esquemaEvaluacionDetalleService.ObtenerPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{idEsquemaEvaluacion}")]
        [HttpGet]
        public ActionResult ObtenerporIdEsquemaEvaluacion(int idEsquemaEvaluacion)
        {
            try
            {
                return Ok(_esquemaEvaluacionDetalleService.ObtenerPorIdEsquemaEvaluacion(idEsquemaEvaluacion));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }





    }


}
