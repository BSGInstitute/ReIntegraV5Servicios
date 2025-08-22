using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProgramaGeneralMotivacionRespuestaController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión de ProgramaGeneralMotivacionRespuesta
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    public class ProgramaGeneralMotivacionRespuestaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ProgramaGeneralMotivacionRespuestaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 13/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] ProgramaGeneralMotivacionRespuestaDTO item)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new ProgramaGeneralMotivacionRespuestaService(unitOfWork);
                    var respuesta = servicio.GuardarCambios(item, _respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(respuesta);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
