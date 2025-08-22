using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: GestionRemuneracionPuestoTrabajoController
    /// Autor: Sergio Yepez P.
    /// Fecha: 17/12/2024
    /// <summary>
    /// Compensación por Puesto
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class MaestroEvaluacionController : Controller
    {
        private ITokenManager _tokenManager;
        private IExamenService _examenService;
        private readonly IMaestroEvaluacionService _maestroEvaluacionService;
        private readonly IGrupoComponenteEvaluacionService _grupoComponenteEvaluacionService;
        public MaestroEvaluacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _maestroEvaluacionService = new MaestroEvaluacionService(unitOfWork);
            _grupoComponenteEvaluacionService = new GrupoComponenteEvaluacionService(unitOfWork);
            _examenService = new ExamenService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Tipo Funcion: Get
        /// Autor: Sergio Yepez Pillco.
        /// Fecha 2024-01-06
        /// <summary>
        /// Trae toda la informacion de Evaluaciones para el Index de vistas
        /// </summary>
        /// <returns>Retorna una lista</returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _maestroEvaluacionService.Obtener();
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// Tipo Funcion: Get
        /// Autor: Sergio Yepez Pillco.
        /// Fecha 2025-01-07
        /// <summary>
        /// Función que trae listas para el llenado de combobox
        /// </summary>
        /// <returns>Retorna listas</returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosModulo()
        {
            try
            {
                var reporteCombo = _maestroEvaluacionService.ObtenerCombosModulo();
                return Ok(reporteCombo);
            }
            catch
            {
                throw;
            }
        }


        /// Tipo Funcion: Get
        /// Autor: Sergio Yepez Pillco.
        /// Fecha 2025-01-07
        /// <summary>
        /// Función que trae listas de un registro seleccioando
        /// </summary>
        /// <returns>Retorna listas</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionEditar(int idEvaluacion)
        {
            try
            {
                var listaExamenes = _maestroEvaluacionService.ObtenerEvaluacionAgrupado(idEvaluacion);
                var listaIdGrupo = _examenService.ObtenerIdGruposPorEvaluacion(idEvaluacion);
                var listaGrupo = _maestroEvaluacionService.ObtenerEvaluacionEditarGrupoGrilla(idEvaluacion);
                var listaComponente = _examenService.ObtenerComponentePorEvaluacion(idEvaluacion);
                var listaCentiles = _maestroEvaluacionService.ObtenerCentilesPorEvaluacion(idEvaluacion);
                return Ok(new { ListaExamenes = listaExamenes , ListaGrupo = listaGrupo, ListaComponente = listaComponente, ListaCentiles = listaCentiles });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Funcion: Get
        /// Autor: Sergio Yepez Pillco.
        /// Fecha 2025-01-11
        /// <summary>
        /// Obtiene una lista de la entidad Centil que se obtiene a través del GrupoId.
        /// </summary>
        /// <returns>Retorna lista</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCentilComponente(int idGrupoComponenteEvaluacion)
        {
            try
            {
                var listaCentil = _maestroEvaluacionService.ObtenerCentilGrupoComponente(idGrupoComponenteEvaluacion);
                return Ok(new { ListaCentil = listaCentil });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Funcion: Get
        /// Autor: Sergio Yepez Pillco.
        /// Fecha 2025-01-16
        /// <summary>
        /// Obtiene una lista de Examen es relacionados y no relacionados a una Evaluacion.
        /// </summary>
        /// <returns>Retorna listas</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAsignacionEvaluaciones(int IdEvaluacion)
        {
            try
            {
                var examenesAsignados = _maestroEvaluacionService.ObtenerExamenesAsignados(IdEvaluacion);
                var examenesNoAsignados = _maestroEvaluacionService.ObtenerExamenesNoAsignados(IdEvaluacion);
                return Ok(new { ExamenesAsignados = examenesAsignados, ExamenesNoAsignados = examenesNoAsignados });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Funcion: Get
        /// Autor: Sergio Yepez Pillco.
        /// Fecha 2025-01-11
        /// <summary>
        /// Función que trae listas de un registro seleccioando
        /// </summary>
        /// <returns>Retorna listas</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionesAgrupar(int idEvaluacion)
        {
            try
            {
                var listaConfigurar = _maestroEvaluacionService.ObtenerEvaluacionEditarGrupoGrilla(idEvaluacion);
                var listaCalificar = _maestroEvaluacionService.ObtenerGruposComponenteDesglosado(idEvaluacion);
                //var listaCentil = _maestroEvaluacionService.ObtenerCentilGrupoComponente(idEvaluacion);
                var listaIdGrupo = _examenService.ObtenerIdGruposPorEvaluacion(idEvaluacion);
                var listaGrupo = _grupoComponenteEvaluacionService.ObtenerGruposPorIdEvaluacion(listaIdGrupo);
                var listaComponente = _examenService.ObtenerComponentePorEvaluacion(idEvaluacion);
                return Ok(new { ListaConfigurar = listaConfigurar, ListaCalificar = listaCalificar, ListaGrupo = listaGrupo, ListaComponente = listaComponente });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Sergio Yepez P.
        /// Fecha: 17/01/2025
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una Eliminacion lógica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <returns>Retorna 200 y objeto eliminado o 400 y mensaje de error</returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _maestroEvaluacionService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [Route("[action]")]
        [HttpPut]
        public IActionResult ActualizarExamenTest([FromBody] ExamenTestDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _maestroEvaluacionService.Actualizar(Json);
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarCentilGrupoComponente([FromBody] CentilDTO centil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _maestroEvaluacionService.InsertarCentilGrupoComponente(centil, _tokenManager.UserName);
                return Ok(new { respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarCentilGrupoComponente([FromBody] CentilDTO centil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _maestroEvaluacionService.ActualizarCentilGrupoComponente(centil, _tokenManager.UserName);
                return Ok(new { respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarExamenTest([FromBody] ExamenTestDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _maestroEvaluacionService.Insertar(Json);
                return Ok(new { respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
