using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using System;
using System.Security.Claims;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FeedbackConfigurarDetalleController : Controller
    {
        private IFeedbackConfigurarDetalleService _feedbackConfigurarDetalleService;
        private IUnitOfWork _unitOfWork;
        private ITokenManager _tokenManager;
        public FeedbackConfigurarDetalleController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _feedbackConfigurarDetalleService = new FeedbackConfigurarDetalleService(unitOfWork);
            _tokenManager = tokenManager;
        }

        [Route("[Action]/{IdFeedbackConfigurar}")]
        [HttpGet]
        public ActionResult Obtener(int IdFeedbackConfigurar)
        {
            var resultado = _feedbackConfigurarDetalleService.ObtenerDetallePorIdFeedbackConfigurar(IdFeedbackConfigurar);
            return Ok(resultado);
        }

    }
}
