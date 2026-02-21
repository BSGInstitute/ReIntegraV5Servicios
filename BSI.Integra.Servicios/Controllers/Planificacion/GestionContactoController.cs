using BSI.Integra.Aplicacion.DTO;
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

        /// Autor: Lolo Zaa
        /// Fecha: 12/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre de centros de costo basado en un nombre pracial
        /// <param name=-='filtros'> Filtros que contiene el nombre Parcial </param>
        /// <returns> retorna 200 y lista de objetos para combo y mensaje de error </return>
        [HttpPost("[action]")]
        public IActionResult ObtenerAutocomplete([FromBody] StringDTO filtro)
        {
          if (!ModelState.IsValid)
          {
            return BadRequest(ModelState);
          }
          return Ok(_gestionContactoService.ObtenerFiltroAutocomplete(filtro.Valor));
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de T_PEspecifico filtrado por IdCentroCosto.
        /// </summary>
        /// <param name="idCentroCosto">Identificador del centro de costo</param>
        /// <returns>Lista de objetos con Id y Nombre</returns>
        [HttpGet("[action]/{idCentroCosto}")]
        public IActionResult ObtenerPEspecificoPorCentroCosto(int idCentroCosto)
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerPEspecificoPorCentroCosto(idCentroCosto));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones con datos del proveedor asociado a un PE especifico.
        /// </summary>
        /// <param name="idPEspecifico">Identificador del presupuesto especifico</param>
        /// <returns>Lista de IdPEspecificoSesion, IdProveedor y NombreProveedor</returns>
        [HttpGet("[action]/{idPEspecifico}")]
        public IActionResult ObtenerSesionesProveedorPorPEspecifico(int idPEspecifico)
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerSesionesProveedorPorPEspecifico(idPEspecifico));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los flujos de gestion docente activos.
        /// </summary>
        /// <returns>Lista de Id y Nombre de los flujos activos</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerGestionDocenteFlujos()
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerGestionDocenteFlujos());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los estados de gestion de contacto activos.
        /// </summary>
        /// <returns>Lista de Id, Nombre y Descripcion de los estados activos</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerEstadosGestionContacto()
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerEstadosGestionContacto());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta una Gestión de Contacto como oportunidad docente a partir de
        /// IdCentroCosto e IdProveedor. El IdClasificacionPersona se resuelve
        /// automáticamente desde el proveedor indicado.
        /// </summary>
        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta un registro en T_GestionContactoDocenteFlujo vinculando
        /// una Gestion de Contacto con un Flujo de Gestion Docente.
        /// </summary>
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarGestionContactoDocenteFlujo([FromBody] InsertarGestionContactoDocenteFlujoDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo inválido", Errores = ModelState });

                var idGenerado = await _gestionContactoService.InsertarGestionContactoDocenteFlujoAsync(dto);

                return Ok(new { Exito = true, Mensaje = "Registro creado correctamente", Id = idGenerado });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito   = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message,
                    Inner2  = ex.InnerException?.InnerException?.Message
                });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarOportunidadDocente([FromBody] CrearOportunidadDocenteDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo inválido", Errores = ModelState });

                var idGenerado = await _gestionContactoService.InsertarOportunidadDocenteAsync(dto);

                return Ok(new { Exito = true, Mensaje = "Oportunidad docente creada correctamente", Id = idGenerado });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito   = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message,
                    Inner2  = ex.InnerException?.InnerException?.Message
                });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 21/02/2026
        /// Version: 1.0
        /// <summary>
        /// Congela un flujo de gestión docente con todas sus actividades, disparadores,
        /// ocurrencias y configuración IA asociadas. Crea copias congeladas en estado
        /// POR_EJECUTAR para ejecución posterior.
        /// </summary>
        /// <param name="idGestionContactoDocenteFlujo">ID del vínculo entre gestión contacto y flujo docente a congelar</param>
        /// <returns>ID del flujo congelado creado</returns>
        [HttpPost("[action]/{idGestionContactoDocenteFlujo}")]
        public async Task<IActionResult> CongelarFlujoDocente(int idGestionContactoDocenteFlujo)
        {
            try
            {
                var idFlujoCongelado = await _gestionContactoService.CongelarFlujoDocenteAsync(idGestionContactoDocenteFlujo);

                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Flujo docente congelado correctamente",
                    IdFlujoCongelado = idFlujoCongelado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito   = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message,
                    Inner2  = ex.InnerException?.InnerException?.Message
                });
            }
        }

    }
}
