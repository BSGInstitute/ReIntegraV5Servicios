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
    public class GestionDocenteFlujoController : ControllerBase
    {
        private readonly IGestionDocenteFlujoService _gestionDocenteFlujoService;

        public GestionDocenteFlujoController(IGestionDocenteFlujoService gestionDocenteFlujoService)
        {
            _gestionDocenteFlujoService = gestionDocenteFlujoService;
        }

        [HttpPost("Insertar")]
        public async Task<IActionResult> Insertar([FromBody] GestionDocenteFlujoDTO dto)
        {
            try
            {
                var id = await _gestionDocenteFlujoService.InsertarAsync(dto);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] GestionDocenteFlujoDTO dto)
        {
            try
            {
                var rpta = await _gestionDocenteFlujoService.ActualizarAsync(dto);
                return Ok(new { Exito = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar(int id, string usuario)
        {
            try
            {
                var rpta = await _gestionDocenteFlujoService.EliminarAsync(id, usuario);
                return Ok(new { Exito = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("ObtenerTodo")]
        public async Task<IActionResult> ObtenerTodo()
        {
            try
            {
                var lista = await _gestionDocenteFlujoService.ObtenerTodoAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("ObtenerEstadosFlujo")]
        public IActionResult ObtenerEstadosFlujo()
        {
            try
            {
                var lista = _gestionDocenteFlujoService.ObtenerEstadosFlujo();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("ObtenerCategorias")]
        public IActionResult ObtenerCategorias()
        {
            try
            {
                var lista = _gestionDocenteFlujoService.ObtenerCategorias();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("ObtenerActividadesCabecera")]
        public IActionResult ObtenerActividadesCabecera()
        {
            try
            {
                var lista = _gestionDocenteFlujoService.ObtenerActividadesCabecera();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("ObtenerSesiones")]
        public IActionResult ObtenerSesiones()
        {
            try
            {
                var lista = _gestionDocenteFlujoService.ObtenerSesiones();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
    }
}
