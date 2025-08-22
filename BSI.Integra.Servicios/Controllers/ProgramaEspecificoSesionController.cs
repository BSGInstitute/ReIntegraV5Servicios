using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using System.Globalization;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using System.Security.Claims;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProgramaEspecificoSesionController
    /// Autor: Daniel Huaita CArpio
    /// Fecha: 08/02/2023
    /// <summary>
    /// Gestión de PespecificoSesion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaEspecificoSesionController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public ProgramaEspecificoSesionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Cancela solicitudes de Operaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        [Route("[action]/{IdPEspecifico}/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerSesionesAsociadosPEspecifico(int IdPEspecifico, int IdMatriculaCabecera)
        {
            try
            {
                PEspecificoSesionService _repPespecificoSesion = new PEspecificoSesionService(unitOfWork);

                var listaSesiones = _repPespecificoSesion.ObtenerSesionesPorPEspecifico(IdPEspecifico, IdMatriculaCabecera).OrderBy(w => w.FechaInicio);

                return Ok(listaSesiones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Daniel Huaita Carpio
        /// Fecha: 02/13/2023
        /// Versión: 1.0
        /// <summary>
        /// Recueperar Sesion
        /// </summary>
        /// <param name="sesiones" name="idMatriculaCabecera" name="UsuarioRecuperacionSesion"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult RegistrarRecuperacion([FromBody] List<RecuperacionSesionDTO> sesiones)
        {
            RecuperacionSesionService _repRecuperacionSesion = new RecuperacionSesionService(unitOfWork);
            try
            {
                return Ok(_repRecuperacionSesion.RegistrarRecuperacion(sesiones));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 20/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Recueperar Sesion
        /// </summary>
        /// <param name="detalleSesionesFiltro" > Parametros de entrada </param>
        /// <returns> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult DetalleSesionesPorAlumnosFiltrado([FromBody] SesionFiltroDTO detalleSesionesFiltro)
        {
            try 
            {
                PEspecificoSesionService _repPespecificoSesion = new PEspecificoSesionService(unitOfWork); 
                var listado = _repPespecificoSesion.DetalleSesionesPorAlumnosFiltrado(detalleSesionesFiltro);
                return Ok(listado.OrderBy(o => o.NombreAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
