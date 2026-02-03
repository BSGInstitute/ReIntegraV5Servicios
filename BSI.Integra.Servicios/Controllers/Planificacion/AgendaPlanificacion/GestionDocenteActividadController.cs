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

        [HttpPost("InsertarCabecera")]
        public async Task<IActionResult> InsertarCabecera([FromBody] GestionDocenteActividadCabeceraDTO dto)
        {
            try
            {
                var id = await _gestionDocenteActividadService.InsertarCabeceraAsync(dto);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpPost("InsertarDetalle")]
        public async Task<IActionResult> InsertarDetalle([FromBody] InsertarActividadDetalleRequestDTO request)
        {
            try
            {
                var id = await _gestionDocenteActividadService.InsertarDetalleAsync(request);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpPost("InsertarOcurrencia")]
        public async Task<IActionResult> InsertarOcurrencia([FromBody] GestionDocenteOcurrenciaDTO dto)
        {
            try
            {
                var id = await _gestionDocenteActividadService.InsertarOcurrenciaAsync(dto);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpPost("AsociarAFlujo")]
        public async Task<IActionResult> AsociarAFlujo([FromBody] GestionDocenteActividadCabeceraFlujoDTO dto)
        {
            try
            {
                var id = await _gestionDocenteActividadService.AsociarActividadAFlujoAsync(dto);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpDelete("DesasociarDeFlujo")]
        public async Task<IActionResult> DesasociarDeFlujo(int id, string usuario)
        {
            try
            {
                var rpta = await _gestionDocenteActividadService.DesasociarActividadDeFlujoAsync(id, usuario);
                return Ok(new { Exito = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("ObtenerPorFlujo/{idFlujo}")]
        public async Task<IActionResult> ObtenerPorFlujo(int idFlujo)
        {
            try
            {
                var lista = await _gestionDocenteActividadService.ObtenerActividadesPorFlujoAsync(idFlujo);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
    }
}