using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;


namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfigurarProcesoSeleccionController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IUnitOfWork _unitOfWork;
        private IConfigurarProcesoSeleccionService _configurarProcesoSeleccionService;
        public ConfigurarProcesoSeleccionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _configurarProcesoSeleccionService = new ConfigurarProcesoSeleccionService(unitOfWork);
            _tokenManager = tokenManager;
            _unitOfWork = unitOfWork;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosProcesoSeleccion()
        {
            try
            {
                IConfigurarProcesoSeleccionService configurarProcesoSeleccion = new ConfigurarProcesoSeleccionService(_unitOfWork);


                var resultado = new
                {
                    listaPuestoTrabajo = configurarProcesoSeleccion.ObtenerComboPuestoTrabajo(),
                    listaSedeTrabajo = configurarProcesoSeleccion.ObtenerComboSedeTrabajo(),
                    listaCriterioSeleccion = configurarProcesoSeleccion.ObtenerComboCriterioSeleccion(),
                    listaProcesoSeleccionRango = configurarProcesoSeleccion.ObtenerProcesoSeleccionRango(),
                    listaExamen = configurarProcesoSeleccion.ObtenerExamenes()
                };
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("[action]/{idProcesoSeleccion}")]
        public ActionResult ObtenerEtapaProcesoSeleccion(int IdProcesoSeleccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IConfigurarProcesoSeleccionService configurarProcesoSeleccion = new ConfigurarProcesoSeleccionService(_unitOfWork);

                var Postulante = configurarProcesoSeleccion.ObtenerProcesoSeleccionEtapa(IdProcesoSeleccion);
                return Ok(Postulante);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("[action]/{idProcesoSeleccion}")]
        public ActionResult ObtenerEvaluacionesAsociacion(int idProcesoSeleccion)
        {

            IConfigurarProcesoSeleccionService configurarProcesoSeleccion = new ConfigurarProcesoSeleccionService(_unitOfWork);

            var respuesta = configurarProcesoSeleccion.ObtenerEvaluacionesAsociacion(idProcesoSeleccion);
            return Ok(respuesta);

        }



        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerProcesoSeleccion()
        {
            try
            {
                IConfigurarProcesoSeleccionService configurarProcesoSeleccion = new ConfigurarProcesoSeleccionService(_unitOfWork);

                var respuesta = configurarProcesoSeleccion.ObtenerProcesoSeleccion();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerExamenes()
        {
            try
            {
                IConfigurarProcesoSeleccionService configurarProcesoSeleccion = new ConfigurarProcesoSeleccionService(_unitOfWork);

                var listaExamen = configurarProcesoSeleccion.ObtenerExamenes();

                return Ok(listaExamen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{IdProcesoSeleccion}")]
        public ActionResult ObtenerExamenesNoAsociados(int IdProcesoSeleccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IConfigurarProcesoSeleccionService configurarProcesoSeleccion = new ConfigurarProcesoSeleccionService(_unitOfWork);

                var Postulante = configurarProcesoSeleccion.ObtenerExamenesNoAsociados(IdProcesoSeleccion);
                return Ok(Postulante);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("[action]/{IdProcesoSeleccion}")]
        public ActionResult ObtenerExamenesAsociados(int IdProcesoSeleccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IConfigurarProcesoSeleccionService configurarProcesoSeleccion = new ConfigurarProcesoSeleccionService(_unitOfWork);

                var Postulante = configurarProcesoSeleccion.ObtenerExamenesAsociados(IdProcesoSeleccion);
                return Ok(Postulante);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{idProcesoSeleccion}")]
        public ActionResult ObtenerEvaluacionPuntaje(int IdProcesoSeleccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IConfigurarProcesoSeleccionService configurarProcesoSeleccion = new ConfigurarProcesoSeleccionService(_unitOfWork);

                var Postulante = configurarProcesoSeleccion.ObtenerEvaluacionPuntaje(IdProcesoSeleccion);
                return Ok(Postulante);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] ProcesoSeleccionAgrupadoInsertarModificarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _configurarProcesoSeleccionService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }


        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarProcesoSeleccionConfiguracionCalificacion([FromBody] PuntajeEvaluacionAgrupadaComponenteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _configurarProcesoSeleccionService.ActualizarProcesoSeleccionConfiguracionCalificacion(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }


        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public ActionResult InsertarProcesoSeleccionConfiguracion([FromBody] ProcesoSeleccionAgrupadoInsertarModificarDTO dto)
        {
            try
            {
                var configurarProcesoSeleccionService = new ConfigurarProcesoSeleccionService(_unitOfWork);

                var respuesta = configurarProcesoSeleccionService.InsertarProcesoSeleccionConfiguracion(dto, _tokenManager.UserName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
