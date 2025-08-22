using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: PgeneralAsubPgeneralController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión de PgeneralAsubPgeneral
    /// </summary>
    [Route("api/Planificacion/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class PgeneralAsubPgeneralController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPgeneralAsubPgeneralService _pGeneralASubPGeneralService;
        public PgeneralAsubPgeneralController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _pGeneralASubPGeneralService = new PgeneralAsubPgeneralService(unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian.
        /// Fecha: 12/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Inserta un registro pgeneral hijo
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult<PgeneralAsubPgeneralCursoHijoDTO> Insertar([FromBody] PgeneralAsubPgeneralInsertarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _pGeneralASubPGeneralService.Insertar(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("[action]/{id}")]
        public ActionResult<bool> Eliminar(int id)
        {
            var respuesta = _pGeneralASubPGeneralService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// <summary>
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-21
        /// Actualiza [pla].[T_PgeneralAsubPgeneral] e inserta o actualiza [pla].[T_PgeneralASubPgeneralVersionPrograma]    
        /// </summary>
        /// <param name="json"></param>
        /// <returns>bool</returns>
        [HttpPut("[Action]")]
        public ActionResult<PgeneralAsubPgeneralCursoHijoDTO> Actualizar([FromBody] PGeneralASubPGeneralActualizarDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _pGeneralASubPGeneralService.Actualizar(json, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 13/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos hijos de un programa General para el CRUD
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> List<PGeneralASubPGeneralCursoHijo> </returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult<IEnumerable<PgeneralAsubPgeneralCursoHijoDTO>> ObtenerCursosHijosPorIdPgeneral(int idPGeneral)
        {
            var resultado = _pGeneralASubPGeneralService.ObtenerCursosHijosPorIdPgeneral(idPGeneral);
            return Ok(resultado);
        }
    }
}
