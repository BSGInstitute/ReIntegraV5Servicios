using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers.Planificacion.AgendaPlanificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GestionDocenteActividadController : ControllerBase
    {
        private readonly IGestionDocenteActividadService _gestionDocenteActividadService;

        public GestionDocenteActividadController(IGestionDocenteActividadService gestionDocenteActividadService)
        {
            _gestionDocenteActividadService = gestionDocenteActividadService;
        }

        [HttpPost("InsertarMaestro")]
        public async Task<IActionResult> InsertarMaestro([FromBody] MaestroGestionDocenteActividadDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo inválido", Errores = ModelState });

                var idGenerado = await _gestionDocenteActividadService.ProcesarMaestroActividadAsync(dto);

                return Ok(new { Exito = true, Mensaje = "Actividad y jerarquía creadas correctamente", Id = idGenerado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
    }
}
