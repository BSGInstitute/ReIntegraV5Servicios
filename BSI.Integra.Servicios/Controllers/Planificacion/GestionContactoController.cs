using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GestionContactoController : ControllerBase
    {
        private IGestionContactoService _gestionContactoService;

        public GestionContactoController(IGestionContactoService gestionContactoService)
        {
            _gestionContactoService = gestionContactoService;
        }

        /// Autor: Jose Vega
        /// Fecha: 18/12/2025
        /// Version: 1.0
        /// <summary>
        /// Inserta una Gestión de Contacto recibiendo directamente los IDs vinculados.
        /// </summary>
        [HttpPost("Insertar")]
        public async Task<IActionResult> Insertar([FromBody] CrearGestionContactoDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo inválido", Errores = ModelState });

                var idGenerado = await _gestionContactoService.ProcesarInsercionGestionAsync(dto);

                return Ok(new { Exito = true, Mensaje = "Gestión creada correctamente", Id = idGenerado });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
