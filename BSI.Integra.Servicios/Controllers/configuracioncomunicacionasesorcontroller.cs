using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// =============================================
// Controller: ConfiguracionComunicacionAsesorController
// Ubicación: BSI.Integra.Servicios\Controllers\
// Autor: Miguel Valdivia
// Fecha: 20/11/2025
// =============================================

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionComunicacionAsesorController
    /// Autor: Miguel Valdivia
    /// Fecha: 20/11/2025
    /// <summary>
    /// Gestión de configuración de comunicación académica desde sistema asesor
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfiguracionComunicacionAsesorController : ControllerBase
    {
        private readonly IMatriculaConfiguracionComunicacionAsesorService _service;
        private readonly ITokenManager _tokenManager;

        public ConfiguracionComunicacionAsesorController(
            IMatriculaConfiguracionComunicacionAsesorService service,
            ITokenManager tokenManager)
        {
            _service = service;
            _tokenManager = tokenManager;
        }

        /// Tipo Función: POST
        /// Autor: Miguel Valdivia
        /// Fecha: 20/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el catálogo de horarios de contacto disponibles
        /// </summary>
        /// <returns>Retorna 200 y lista de horarios o 500 y mensaje de error</returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerCatalogoHorariosContacto()
        {
            try
            {
                var catalogoHorarios = _service.ObtenerCatalogoHorariosContacto();
                return Ok(new { success = true, datos = catalogoHorarios });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al obtener el catálogo de horarios.",
                    detalle = ex.Message
                });
            }
        }

        /// Tipo Función: POST
        /// Autor: Miguel Valdivia
        /// Fecha: 20/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración de comunicación de un alumno por su IdAlumno
        /// </summary>
        /// <param name="filtro">Objeto con IdAlumno</param>
        /// <returns>Retorna 200 y configuración del alumno o 400 y mensaje de error</returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerConfiguracionPorAlumno([FromBody] FiltroIdAlumnoDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (filtro.IdAlumno <= 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "El IdAlumno es requerido y debe ser mayor a 0."
                    });
                }

                var response = _service.ObtenerConfiguracionPorAlumno(filtro.IdAlumno);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Error al obtener la configuración de comunicación.",
                    detalle = ex.Message
                });
            }
        }

        /// Tipo Función: POST
        /// Autor: Miguel Valdivia
        /// Fecha: 20/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Guarda (inserta o actualiza) la configuración de comunicación de un alumno en todas sus matrículas activas
        /// </summary>
        /// <param name="request">Objeto con IdAlumno y lista de configuraciones</param>
        /// <returns>Retorna 200 y resultado de la operación o 400 y mensaje de error</returns>
        [HttpPost("[action]")]
        public IActionResult GuardarConfiguracionPorAlumno([FromBody] GuardarConfiguracionRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Validaciones básicas
                if (request.IdAlumno <= 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "El IdAlumno es requerido y debe ser mayor a 0."
                    });
                }

                if (request.Configuraciones == null || request.Configuraciones.Count == 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "La lista de configuraciones no puede estar vacía."
                    });
                }

                // Obtener usuario del token
                string usuario = _tokenManager.UserName;
                if (string.IsNullOrEmpty(usuario))
                {
                    usuario = "sistema"; // Usuario por defecto si no hay token
                }

                // Llamar al servicio
                var response = _service.GuardarConfiguracionPorAlumno(
                    request.IdAlumno,
                    request.Configuraciones,
                    usuario
                );

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Ocurrió un error al guardar la configuración.",
                    detalle = ex.Message
                });
            }
        }
    }

    /// <summary>
    /// DTO para filtro por IdAlumno
    /// </summary>
    public class FiltroIdAlumnoDTO
    {
        public int IdAlumno { get; set; }
    }

    /// <summary>
    /// DTO para request de guardar configuración
    /// </summary>
    public class GuardarConfiguracionRequestDTO
    {
        public int IdAlumno { get; set; }
        public List<GuardarConfiguracionComunicacionAsesorDTO> Configuraciones { get; set; }
    }
}