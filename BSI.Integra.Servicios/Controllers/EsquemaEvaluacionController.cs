using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: EsquemaEvaluacionController
    /// Autor: Jonathan Caipo
    /// Fecha: 14/11/2022
    /// <summary>
    /// Gestion de los esuqemas de evaluacion de un curso asi como sus tablas hijas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class EsquemaEvaluacionController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private ITokenManager _tokenManager;
        private IEsquemaEvaluacionService _esquemaEvaluacionService;

        public EsquemaEvaluacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _esquemaEvaluacionService = new EsquemaEvaluacionService(_unitOfWork);
        }


        [HttpGet("[action]")]
        public IActionResult ObtenerTodo()
        {
            var resultado = _esquemaEvaluacionService.ObtenerTodo();
            return Ok(resultado);
        }



        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Nombre del esquema evaluacion por id matricula 
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerNombreEsquemaEvaluacionPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                var listado = _esquemaEvaluacionService.ObtenerNombreCongelamientoEsquemaPorMatricula(idMatriculaCabecera);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Esquema Evaluacin por Matricula mediante el idMatricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerEsquemaEvaluacionPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                var listado = _esquemaEvaluacionService.ObtenerCongelamientoEsquemaPorMatricula(idMatriculaCabecera);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el congelamiento del Esquema Evaluacion
        /// </summary>
        /// <param name="Json"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarCongelamientoEsquemaEvaluacion(EditarCongelamientoPEspecificoMatriculaAlumnoDTO json)
        {
            try
            {
                var listado = _esquemaEvaluacionService.ActualizarCongelamientoEsquemaPorMatricula(json);
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Daniel Huaita
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el congelamiento del Esquema Evaluacion
        /// </summary>
        /// <param name="Json"></param>
        /// <returns></returns>
        [Route("[action]/{IdMatriculaCabecera}/{IdPEspecifico}/{grupo}")]
        [HttpGet]
        public ActionResult obtenerListadoCriterio(int IdMatriculaCabecera, int IdPEspecifico, int grupo)
        {
            try
            {
                var listado = _esquemaEvaluacionService.ListadoCriteriosEvaluacionPorCurso(IdMatriculaCabecera, IdPEspecifico, grupo);


                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm.
        /// Fecha: 09/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_AreaFormacion para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerComboAsync")]
        public async Task<ActionResult> ObtenerComboAsync()
        {
            try
            {
                var resultado = await _esquemaEvaluacionService.ObtenerComboAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 20/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Esquema Asiado
        /// </summary>
        /// <param name="idPGeneral"></param>
        [Route("[Action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerEsquemaAsociado(int idPGeneral)
        {
            return Ok(_esquemaEvaluacionService.ObtenerEsquemaAsociado(idPGeneral));
        }
        /// TipoFuncion: PUT
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Modifica o inserta un nuevo esquemas asi como sus detalles ,proveedores y  modalidades 
        /// </summary>
        /// <param name=”esquema”>DTO del esquema de evaluacion</param>
        /// <returns>bool<returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarAsignacion([FromBody] EsquemaEvaluacionRegistrarAsignacionDTO esquema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_esquemaEvaluacionService.ActualizarAsignacion(esquema, _tokenManager.UserName));
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Modifica o inserta un nuevo esquemas asi como sus detalles ,proveedores y  modalidades 
        /// </summary>
        /// <param name=”esquema”>DTO del esquema de evaluacion</param>
        /// <returns>bool<returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public ActionResult RegistrarAsignacion([FromBody] EsquemaEvaluacionRegistrarAsignacionDTO esquema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_esquemaEvaluacionService.RegistrarAsignacion(esquema, _tokenManager.UserName));
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/07/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <param name=”idEsquemaAsignado”></param>
        /// <returns>bool<returns>
        [Route("[action]/{idEsquemaAsignado}")]
        [HttpGet]
        public ActionResult ObtenerDetalleEsquemaAsignado(int idEsquemaAsignado)
        {
            return Ok(_esquemaEvaluacionService.ObtenerDetalleEsquemaAsignado(idEsquemaAsignado));
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/07/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <param name=”idEsquemaEvaluacion”>DTO del esquema de evaluacion</param>
        /// <returns>bool<returns>
        [Route("[Action]/{idEsquemaEvaluacion}")]
        [HttpGet]
        public ActionResult ObtenerDetalleEsquema(int idEsquemaEvaluacion)
        {
            return Ok(_esquemaEvaluacionService.ObtenerDetalleEsquema(idEsquemaEvaluacion));
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/07/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <param name=”archivos”></param>
        /// <returns>bool<returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public ActionResult SubirArchivo([FromForm] IList<IFormFile> archivos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_esquemaEvaluacionService.SubirArchivo(archivos));
        }
        /// TipoFuncion: POST
        /// Autor: Marco Villanueva
        /// Fecha: 19/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta el esquema de evaluacion y sus detalles
        /// </summary>
        /// <param name=”dto”>DTO del esquema de evaluacion y sus detalles</param>
        /// <returns>bool<returns>
        /// 
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] EsquemaEvaluacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _esquemaEvaluacionService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT  
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] EsquemaEvaluacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _esquemaEvaluacionService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: GET
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la FormaCalculoEvaluacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerComboFormaCalculoEvaluacion()
        {
            var resultado = _esquemaEvaluacionService.ObtenerComboFormaCalculoEvaluacion();
            return Ok(resultado);
        }
        /// Tipo Función: DELETE
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{IdEsquemaEvaluacion}")]
        public IActionResult Eliminar(int IdEsquemaEvaluacion)
        {
            var respuesta = _esquemaEvaluacionService.Eliminar(IdEsquemaEvaluacion, _tokenManager.UserName);
            return Ok(respuesta);
        }

    }
}
