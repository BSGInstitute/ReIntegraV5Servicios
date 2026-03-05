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

        public GestionContactoController(
            IGestionContactoService gestionContactoService)
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

        /// Autor: Joseph Llanque
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de docentes para el combo del formulario General.
        /// </summary>
        [HttpGet("[action]")]
        public IActionResult ObtenerDocentes()
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerDocentes());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado paginado de oportunidades docentes para la grilla.
        /// </summary>
        [HttpGet("[action]")]
        public IActionResult ObtenerOportunidades(
            [FromQuery] string? busqueda = null,
            [FromQuery] int pagina = 1,
            [FromQuery] int porPagina = 10)
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerOportunidadesDocente(
                    busqueda ?? "", pagina, porPagina));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 28/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades de un flujo docente congelado organizadas jerarquicamente.
        /// Para categoria General (1): retorna estructura { Actividades: [{ Detalles: [{ Disparadores: [...] }] }] }
        /// Para categoria Ejecucion Curso (2): retorna estructura { Sesiones: [{ Actividades: [{ Detalles: [{ Disparadores: [...] }] }] }] }
        /// </summary>
        /// <param name="idGestionContactoDocenteFlujo">ID del vinculo entre gestion contacto y flujo docente</param>
        /// <returns>Estructura jerarquica de actividades segun categoria del flujo</returns>
        [HttpGet("[action]/{idGestionContactoDocenteFlujo}")]
        public async Task<IActionResult> ObtenerActividadesFlujoPorCategoria(int idGestionContactoDocenteFlujo)
        {
            try
            {
                var resultado = await _gestionContactoService.ObtenerActividadesFlujoPorCategoriaAsync(idGestionContactoDocenteFlujo);
                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Actividades obtenidas correctamente",
                    Datos = resultado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message,
                    Inner2 = ex.InnerException?.InnerException?.Message
                });
            }
        }

        // =============================================
        // ENDPOINTS PARA HANGFIRE
        // =============================================

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de actividades pendientes listas para ejecutar por Hangfire.
        /// Incluye actividades con disparadores de TIEMPO_FIJO y TIEMPO_RELATIVO que estan
        /// dentro de la ventana de ejecucion (5 minutos).
        /// </summary>
        /// <returns>Lista de actividades pendientes con datos de plantilla y contacto</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerActividadesPendientes()
        {
            try
            {
                var actividades = await _gestionContactoService.ObtenerActividadesPendientesAsync();
                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Actividades pendientes obtenidas correctamente",
                    Cantidad = actividades.Count,
                    Datos = actividades
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de una actividad congelada despues de su ejecucion por Hangfire.
        /// Registra el resultado en el historial de ejecuciones y actualiza los estados de
        /// la actividad y disparador. Maneja reintentos automaticamente (maximo 3 intentos).
        /// </summary>
        /// <param name="request">Datos de la actualizacion de estado</param>
        /// <returns>Resultado de la operacion con el ID del registro de ejecucion creado</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> ActualizarEstadoActividad([FromBody] ActualizarEstadoRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo invalido", Errores = ModelState });

                var resultado = await _gestionContactoService.ActualizarEstadoActividadAsync(request);

                if (resultado.Exitoso)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = resultado.Mensaje,
                        IdEjecucion = resultado.IdRegistro
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                        Mensaje = resultado.Error
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Marca una ocurrencia y activa disparadores dependientes convirtiendolos a TIEMPO_FIJO.
        /// Busca todas las actividades con disparador OCURRENCIA_PREVIA que dependen de la
        /// ocurrencia marcada, calcula su fecha de ejecucion y las convierte a TIEMPO_FIJO
        /// para que Hangfire las procese.
        /// </summary>
        /// <param name="request">Datos de la ocurrencia a marcar</param>
        /// <returns>Resultado de la operacion con el ID de la ocurrencia marcada</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> MarcarOcurrencia([FromBody] MarcarOcurrenciaRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo invalido", Errores = ModelState });

                var resultado = await _gestionContactoService.MarcarOcurrenciaAsync(request);

                if (resultado.Exitoso)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = resultado.Mensaje,
                        IdOcurrenciaMarcada = resultado.IdRegistro
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                        Mensaje = resultado.Error
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

    }
}
