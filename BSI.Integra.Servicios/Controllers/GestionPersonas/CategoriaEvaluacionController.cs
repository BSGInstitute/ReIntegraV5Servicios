using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: CategoriaEvaluacionController
    /// Autor: Villanueva Torres Marco Jose
    /// Fecha: 03/04/2024
    /// <summary>
    /// Categoria Evaluacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CategoriaEvaluacionController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private ICategoriaEvaluacionService _categoriaEvaluacionService;
        public CategoriaEvaluacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _categoriaEvaluacionService = new CategoriaEvaluacionService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 03/04/2024
        /// Versión: 1.0    
        /// <summary>
        /// Obtiene datos de CategoriaEvaluacion
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _categoriaEvaluacionService.Obtener();
            return Ok(resultado);
        }

        /// Tipo Función: POST
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 03/04/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad ComboDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] CategoriaEvaluacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _categoriaEvaluacionService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: PUT
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 03/04/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] CategoriaEvaluacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _categoriaEvaluacionService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }


        /// Tipo Función: DELETE
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 03/04/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una Eliminacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <returns>Retorna 200 y objeto eliminado o 400 y mensaje de error</returns>

        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _categoriaEvaluacionService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }

    }
  
}
