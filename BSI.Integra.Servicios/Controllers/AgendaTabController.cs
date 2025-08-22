using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AgendaTabController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión de AgendaTab
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AgendaTabController : ControllerBase
    {
        private IAgendaTabService _agendaTabService;
        private ITokenManager _tokenManager;
        public AgendaTabController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _agendaTabService = new AgendaTabService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022 
        /// Autor modificación : Gilmer Quispe.
        /// Fecha: 28/12/2022
        /// Descripcion de modificacion: Se corrige logica de codigo para la inserción basica a la tabla
        /// Versión: 1.1
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto"> objeto AgendaTabAlternoDTO que contiene los datos necesarios para la inserción básica a la tabla </param>
        /// <returns> Nueva entidad AgendaTab </returns>
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] AgendaTabAlternoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _agendaTabService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Autor modificación : Gilmer Quispe.
        /// Fecha: 28/12/2022
        /// Descripcion de modificacion: Se corrige logica de codigo para la actualización basica a la tabla
        /// Versión: 1.1
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="dto"> objeto AgendaTabAlternoDTO que contiene los datos necesarios para la inserción básica a la tabla </param>
        /// <returns> Entidad actualizada AgendaTab </returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] AgendaTabAlternoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _agendaTabService.Actualizar(dto, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _agendaTabService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_AgendaTab
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            return Ok(_agendaTabService.Obtener());
        }
    }
}
