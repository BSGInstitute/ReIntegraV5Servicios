using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PartnerPwController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión de PartnerPw
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PartnerPwController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPartnerPwService _partnerPwService;
        public PartnerPwController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _partnerPwService = new PartnerPwService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad PartnerPwDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] PartnerPwDTO dto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var respuesta = _partnerPwService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: PUT
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] PartnerPwDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _partnerPwService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: DELETE
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{idPartner}")]
        public IActionResult Eliminar(int idPartner)
        {
            var respuesta = _partnerPwService.Eliminar(idPartner, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de beneficios y contactos por id Partner
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]/{idPartner}")]
        public IActionResult ObtenerBeneficioContactoPorId(int idPartner)
        {
            var resultado = _partnerPwService.ObtenerBeneficioContactoPorId(idPartner);
            return Ok(new
            {
                resultado.Beneficios,
                resultado.Contactos
            });
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de beneficios y contactos por id Partner
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _partnerPwService.Obtener();
            return Ok(resultado);
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 13/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de T_Partner_PW para combo
        /// </summary>
        /// <returns> </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            var resultado = _partnerPwService.ObtenerCombo();
            return Ok(resultado);
        }
    }
}
