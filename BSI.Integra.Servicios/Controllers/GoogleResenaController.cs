using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GoogleResenaController
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Gestión de reseñas de Google Places para las sedes de BSG Institute.
    /// Flujo: Controller → GoogleResenaService → GoogleResenaRepository → SP / EF Core.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GoogleResenaController : ControllerBase
    {
        private readonly IUnitOfWork    _unitOfWork;
        private readonly ITokenManager  _tokenManager;
        private readonly IConfiguration _configuration;

        public GoogleResenaController(IUnitOfWork unitOfWork, ITokenManager tokenManager, IConfiguration configuration)
        {
            _unitOfWork    = unitOfWork;
            _tokenManager  = tokenManager;
            _configuration = configuration;
        }

        /// <summary>Instancia el servicio de dominio con el UnitOfWork actual.</summary>
        private IGoogleResenaService CrearServicio()
            => new GoogleResenaService(_unitOfWork, _configuration);

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta una reseña individual.
        /// </summary>
        /// <param name="entidad">Entidad GoogleResena a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] GoogleResena entidad)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().Add(entidad)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta un listado de reseñas en bloque.
        /// </summary>
        /// <param name="listado">Lista de entidades GoogleResena a insertar.</param>
        /// <returns>Lista de entidades insertadas.</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<GoogleResena> listado)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().Add(listado)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: PUT
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una reseña existente.
        /// </summary>
        /// <param name="entidad">Entidad GoogleResena con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] GoogleResena entidad)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().Update(entidad)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: PUT
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un listado de reseñas en bloque.
        /// </summary>
        /// <param name="listado">Lista de entidades GoogleResena a actualizar.</param>
        /// <returns>Lista de entidades actualizadas.</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<GoogleResena> listado)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().Update(listado)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: DELETE
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Elimina lógicamente una reseña por su Id.
        /// </summary>
        /// <param name="id">Id de la reseña a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>true si se eliminó correctamente.</returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().Delete(id, usuario)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: DELETE
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Elimina lógicamente un listado de reseñas por sus Ids.
        /// </summary>
        /// <param name="listadoIds">Lista de Ids de reseñas a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>true si se eliminaron correctamente.</returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado([FromBody] List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().Delete(listadoIds, usuario)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna la grilla paginada de reseñas con filtros opcionales.
        /// Ejecuta mkt.SP_GoogleResenaObtenerDatos (modo Grilla).
        /// </summary>
        /// <param name="filtro">Filtros: sedes, visibilidad, rating (1-5), rango de fechas, paginación.</param>
        /// <returns>GoogleResenaGrillaPaginadaDTO con datos y metadatos de paginación.</returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerGrilla([FromBody] GoogleResenaGrillaFiltroDTO filtro)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().ObtenerGrilla(filtro)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las sedes de Google Places configuradas con estadísticas agregadas de reseñas.
        /// Ejecuta mkt.SP_GoogleResenaObtenerDatos (modo Sede).
        /// </summary>
        /// <returns>Lista de GoogleResenaSedeItemDTO.</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerSedes()
        {
            try { return Ok(CrearServicio().ObtenerSedes()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las sedes de Google Places para el combo de filtros del frontend.
        /// Consulta directa a mkt.T_GooglePlacesConfiguracion vía EF Core.
        /// </summary>
        /// <returns>Lista de GoogleResenaSedeComboDTO.</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerSedesCombo()
        {
            try { return Ok(CrearServicio().ObtenerSedesCombo()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Marca como visibles (Mostrar=true) las reseñas indicadas por Id.
        /// </summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>true si se actualizaron correctamente.</returns>
        [HttpPost("[action]")]
        public IActionResult MarcarResenaVisible([FromBody] GoogleResenaMarcarMostrarDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().MarcarResenaVisible(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Marca como ocultas (Mostrar=false) las reseñas indicadas por Id.
        /// </summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>true si se actualizaron correctamente.</returns>
        [HttpPost("[action]")]
        public IActionResult MarcarResenaOculta([FromBody] GoogleResenaMarcarMostrarDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().MarcarResenaOculta(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Sincroniza las reseñas desde la Google Places API y Business Profile API
        /// para todas las sedes configuradas. Envía un correo de resumen al finalizar.
        /// Flujo OAuth2: RefreshToken → AccessToken → Accounts → Locations → Reviews (paginado).
        /// Fallback: Places API (New) con API Key si OAuth2 no está disponible.
        /// </summary>
        /// <returns>JSON con el resumen de sincronización y diagnóstico.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> SincronizarGoogleApi()
        {
            try
            {
                var resultado = await CrearServicio().SincronizarGoogleApi(_tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
