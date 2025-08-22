using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProgramaGeneralProblemaDetalleSolucionRespuestaController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión de ProgramaGeneralProblemaDetalleSolucionRespuesta
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralProblemaDetalleSolucionRespuestaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private IProgramaGeneralProblemaDetalleSolucionRespuestaService servicioPrincipal;
        public ProgramaGeneralProblemaDetalleSolucionRespuestaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("GuardarCambiosAgenda")]
        public IActionResult GuardarCambiosAgenda([FromBody] ProgramaGeneralProblemaDetalleSolucionRespuestaDTO entidad)
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
                    servicioPrincipal = new ProgramaGeneralProblemaDetalleSolucionRespuestaService(unitOfWork);
                    return Ok(servicioPrincipal.GuardarCambiosAgenda(entidad, _respuestaCorrecta.RegistroClaimToken.UserName));
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
