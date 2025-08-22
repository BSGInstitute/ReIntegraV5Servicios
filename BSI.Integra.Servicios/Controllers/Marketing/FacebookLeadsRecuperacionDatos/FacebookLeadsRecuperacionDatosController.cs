using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.FacebookLeadsRecuperacionDatos;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.FacebookLeadsRecuperacionDatos;
using Microsoft.AspNetCore.Cors;

namespace BSI.Integra.Servicios.Controllers.Marketing.FacebookLeadsRecuperacionDatos
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CorsVista")]

    public class FacebookLeadsRecuperacionDatosController : ControllerBase
    {
        private readonly IFacebookLeadsRecuperacionDatosService _servicio;

        public FacebookLeadsRecuperacionDatosController(IFacebookLeadsRecuperacionDatosService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("{idLead}")]
        public async Task<ActionResult<FacebookLeadsRecuperacionDatosResponseDTO>> ObtenerPorId(string idLead)
        {
            try
            {
                var resultado = await _servicio.ObtenerPorIdAsync(idLead);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = "Error al obtener los datos desde Facebook.", detalle = ex.Message });
            }
        }
    }
}
