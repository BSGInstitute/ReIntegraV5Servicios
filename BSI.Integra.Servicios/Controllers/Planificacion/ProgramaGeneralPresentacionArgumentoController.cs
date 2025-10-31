using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralPresentacionArgumentoController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IProgramaGeneralPresentacionArgumentoService _programaGeneralPresentacionArgumentoService;
        public ProgramaGeneralPresentacionArgumentoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _programaGeneralPresentacionArgumentoService = new ProgramaGeneralPresentacionArgumentoService(unitOfWork);
            _tokenManager = tokenManager;
        }


        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva
        /// Fecha: 25-09-2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_ProgramaGeneralPresentacionArgumento
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _programaGeneralPresentacionArgumentoService.Obtener();
            return Ok(resultado);

        }
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            var resultado = _programaGeneralPresentacionArgumentoService.ObtenerCombo();
            return Ok(resultado);

        }


        /// Tipo Función: POST
        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad ProgramaGeneralPresentacionArgumentoDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] CompuestoPresentacionArgumentoModalidadDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralPresentacionArgumentoService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] ProgramaGeneralPresentacionArgumentoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralPresentacionArgumentoService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _programaGeneralPresentacionArgumentoService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }

       


    }
}
