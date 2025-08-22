

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{


    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CriterioEvaluacionModalidadCurso : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        private ICriterioEvaluacionModalidadCursoService _criterioEvaluacionModalidadCursoService;


        public CriterioEvaluacionModalidadCurso(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _criterioEvaluacionModalidadCursoService = new CriterioEvaluacionModalidadCursoService(_unitOfWork);
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosCriteriosEvaluacionModalidad()
        {
            var resultado = _criterioEvaluacionModalidadCursoService.ListarCriteriosEvaluacionModalidad();
            return Ok(resultado);
        }
}



}
