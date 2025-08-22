using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PersonalAreaTrabajoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión de PersonalAreaTrabajo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class PersonalAreaTrabajoController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPersonalAreaTrabajoService _personalAreaTrabajoService;

        public PersonalAreaTrabajoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _personalAreaTrabajoService = new PersonalAreaTrabajoService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Metodo HTTP: POST.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 12/01/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="PersonalAreaTrabajoDTO">  </param>
        /// <returns> bool </returns>
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] PersonalAreaTrabajoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _personalAreaTrabajoService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);

        }

        /// Metodo HTTP: PUT.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 16/01/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una ACTUALIZACION basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="PersonalAreaTrabajoDTO">  </param>
        /// <returns> bool </returns>
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] PersonalAreaTrabajoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _personalAreaTrabajoService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Metodo HTTP: Delete.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 16/01/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _personalAreaTrabajoService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PersonalAreaTrabajo
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            return Ok(_personalAreaTrabajoService.Obtener());
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PersonalAreaTrabajo para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            return Ok(_personalAreaTrabajoService.ObtenerCombo());
        }
    }
}
