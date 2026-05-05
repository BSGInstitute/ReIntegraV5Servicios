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
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: LinkedinResenaController
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Gestión de testimonios de LinkedIn para BSG Institute.
    /// Módulo 100% manual — Marketing carga los testimonios autorizados desde modales.
    /// Flujo: Controller → LinkedinResenaService → LinkedinResenaRepository → SP / EF Core.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class LinkedinResenaController : ControllerBase
    {
        private readonly IUnitOfWork   _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public LinkedinResenaController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork   = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// <summary>Instancia el servicio de dominio con el UnitOfWork actual.</summary>
        private ILinkedinResenaService CrearServicio()
            => new LinkedinResenaService(_unitOfWork);

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta un testimonio individual desde el modal de creación.</summary>
        /// <param name="entidad">Entidad LinkedinResena a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] LinkedinResena entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    entidad.UsuarioCreacion ??= _respuestaCorrecta.RegistroClaimToken.UserName;
                    entidad.UsuarioModificacion ??= _respuestaCorrecta.RegistroClaimToken.UserName;
                    return Ok(CrearServicio().Add(entidad));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta un listado de testimonios en bloque.</summary>
        /// <param name="listado">Lista de entidades LinkedinResena a insertar.</param>
        /// <returns>Lista de entidades insertadas.</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<LinkedinResena> listado)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    var userName = _respuestaCorrecta.RegistroClaimToken.UserName;
                    listado.ForEach(e =>
                    {
                        e.UsuarioCreacion ??= userName;
                        e.UsuarioModificacion ??= userName;
                    });
                    return Ok(CrearServicio().Add(listado));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        /// Tipo Función: PUT
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza un testimonio existente desde el modal de edición.</summary>
        /// <param name="entidad">Entidad LinkedinResena con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] LinkedinResena entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    entidad.UsuarioModificacion ??= _respuestaCorrecta.RegistroClaimToken.UserName;
                    return Ok(CrearServicio().Update(entidad));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        /// Tipo Función: PUT
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza un listado de testimonios en bloque.</summary>
        /// <param name="listado">Lista de entidades LinkedinResena a actualizar.</param>
        /// <returns>Lista de entidades actualizadas.</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<LinkedinResena> listado)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    var userName = _respuestaCorrecta.RegistroClaimToken.UserName;
                    listado.ForEach(e => e.UsuarioModificacion ??= userName);
                    return Ok(CrearServicio().Update(listado));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        /// Tipo Función: DELETE
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente un testimonio por su Id.</summary>
        /// <param name="id">Id del testimonio a eliminar.</param>
        /// <returns>true si se eliminó correctamente.</returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    return Ok(CrearServicio().Delete(id, _respuestaCorrecta.RegistroClaimToken.UserName));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        /// Tipo Función: DELETE
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente un listado de testimonios por sus Ids.</summary>
        /// <param name="listadoIds">Lista de Ids a eliminar.</param>
        /// <returns>true si se eliminaron correctamente.</returns>
        [HttpDelete("EliminarListado")]
        public IActionResult EliminarListado([FromBody] List<int> listadoIds)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    return Ok(CrearServicio().Delete(listadoIds, _respuestaCorrecta.RegistroClaimToken.UserName));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna la grilla paginada de testimonios con filtros opcionales.
        /// Ejecuta mkt.SP_LinkedinResenaObtenerDatos (modo Grilla).
        /// </summary>
        /// <param name="filtro">Filtros: visibilidad, país, rango de fechas, paginación.</param>
        /// <returns>LinkedinResenaGrillaPaginadaDTO con datos y metadatos de paginación.</returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerGrilla([FromBody] LinkedinResenaGrillaFiltroDTO filtro)
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
        /// Retorna los países con testimonios activos para el combo de filtros del frontend.
        /// Consulta directa vía EF Core.
        /// </summary>
        /// <returns>Lista de LinkedinResenaPaisComboDTO con bandera.</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPaisesCombo()
        {
            try { return Ok(CrearServicio().ObtenerPaisesCombo()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las ciudades de un país para el combo de filtros del frontend.
        /// Consulta directa vía EF Core.
        /// </summary>
        /// <param name="idPais">Id del país a filtrar.</param>
        /// <returns>Lista de LinkedinResenaCiudadComboDTO.</returns>
        [HttpGet("[action]/{idPais}")]
        public IActionResult ObtenerCiudadesCombo(int idPais)
        {
            try { return Ok(CrearServicio().ObtenerCiudadesCombo(idPais)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca como visibles (Mostrar=true) los testimonios indicados por Id.</summary>
        /// <param name="dto">Ids de testimonios y usuario que realiza la acción.</param>
        /// <returns>true si se actualizaron correctamente.</returns>
        [HttpPost("[action]")]
        public IActionResult MarcarResenaVisible([FromBody] LinkedinResenaMarcarMostrarDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().MarcarResenaVisible(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca como ocultos (Mostrar=false) los testimonios indicados por Id.</summary>
        /// <param name="dto">Ids de testimonios y usuario que realiza la acción.</param>
        /// <returns>true si se actualizaron correctamente.</returns>
        [HttpPost("[action]")]
        public IActionResult MarcarResenaOculta([FromBody] LinkedinResenaMarcarMostrarDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().MarcarResenaOculta(dto)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
