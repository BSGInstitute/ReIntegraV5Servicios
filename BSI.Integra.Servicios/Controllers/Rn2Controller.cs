using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Rn2Controller
    /// Autor: (pendiente)
    /// Fecha: 2026-02-23
    /// <summary>
    /// Endpoints para validaciones de Regla de Negocio 2
    /// </summary>
    [Route("api/rn2")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class Rn2Controller : ControllerBase
    {
        private readonly IValidacionRn2Service _validacionRn2Service;

        public Rn2Controller(IValidacionRn2Service validacionRn2Service)
        {
            _validacionRn2Service = validacionRn2Service;
        }

        /// Tipo Función: GET
        /// Autor: (pendiente)
        /// Fecha: 2026-02-23
        /// Versión: 1.0
        /// <summary>
        /// Valida si un lead está bloqueado por la RN2 (conflicto de correo/teléfono con oportunidad activa)
        /// </summary>
        [HttpGet("[Action]/{idOportunidad}/{idPersonalAsignado}")]
        public IActionResult ValidarAsync(int idOportunidad, int idPersonalAsignado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = _validacionRn2Service.ValidarLeadRn2Async(idOportunidad, idPersonalAsignado);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
