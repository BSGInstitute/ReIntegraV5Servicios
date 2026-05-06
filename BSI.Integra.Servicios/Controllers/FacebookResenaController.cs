using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FacebookResenaController
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Gestión de reseñas de Facebook para las páginas de BSG Institute.
    /// Flujo: Controller → FacebookResenaService → FacebookResenaRepository → SP / EF Core.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FacebookResenaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public FacebookResenaController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// <summary>Instancia el servicio de dominio con el UnitOfWork actual.</summary>
        private IFacebookResenaService CrearServicio()
            => new FacebookResenaService(_unitOfWork);

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta una reseña individual.
        /// </summary>
        /// <param name="entidad">Entidad FacebookResena a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] FacebookResena entidad)
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
        /// <param name="listado">Lista de entidades FacebookResena a insertar.</param>
        /// <returns>Lista de entidades insertadas.</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<FacebookResena> listado)
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
        /// <param name="entidad">Entidad FacebookResena con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] FacebookResena entidad)
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
        /// <param name="listado">Lista de entidades FacebookResena a actualizar.</param>
        /// <returns>Lista de entidades actualizadas.</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<FacebookResena> listado)
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
        /// </summary>
        /// <param name="filtro">Filtros: páginas, visibilidad, rango de fechas, paginación.</param>
        /// <returns>FacebookResenaGrillaPaginadaDTO con datos y metadatos de paginación.</returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerGrilla([FromBody] FacebookResenaGrillaFiltroDTO filtro)
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
        /// Retorna las páginas de Facebook configuradas con estadísticas agregadas de reseñas.
        /// </summary>
        /// <returns>Lista de FacebookResenaPaginaItemDTO.</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPaginas()
        {
            try { return Ok(CrearServicio().ObtenerPaginas()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las cuentas de Facebook para el combo de filtros del frontend.
        /// </summary>
        /// <returns>Lista de FacebookResenaCuentaComboDTO.</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCuentasCombo()
        {
            try { return Ok(CrearServicio().ObtenerCuentasCombo()); }
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
        public IActionResult MarcarResenaVisible([FromBody] FacebookResenaMarcarMostrarDTO dto)
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
        public IActionResult MarcarResenaOculta([FromBody] FacebookResenaMarcarMostrarDTO dto)
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
        /// Sincroniza las reseñas desde la Graph API de Facebook para todas las páginas configuradas.
        /// Envía un correo de resumen al finalizar.
        /// </summary>
        /// <returns>JSON con el resumen de sincronización.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> SincronizarFacebookApi()
        {
            try
            {
                var resultado = await CrearServicio().SincronizarFacebookApi(_tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
