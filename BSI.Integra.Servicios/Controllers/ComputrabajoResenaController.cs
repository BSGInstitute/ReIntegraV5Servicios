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
    /// Controlador: ComputrabajoResenaController
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Gestión de reseñas de empleador en Computrabajo para BSG Institute.
    /// Módulo 100% manual — Marketing carga las reseñas autorizadas desde modales.
    /// Computrabajo NO tiene API pública — captura manual periódica (quincenal).
    /// Flujo: Controller → ComputrabajoResenaService → ComputrabajoResenaRepository → SP / EF Core.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ComputrabajoResenaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public ComputrabajoResenaController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// <summary>Instancia el servicio de dominio con el UnitOfWork actual.</summary>
        private IComputrabajoResenaService CrearServicio()
            => new ComputrabajoResenaService(_unitOfWork);

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una reseña individual desde el modal de creación.</summary>
        /// <param name="entidad">Entidad ComputrabajoResena a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] ComputrabajoResena entidad)
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
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        /// <param name="listado">Lista de entidades ComputrabajoResena a insertar.</param>
        /// <returns>Lista de entidades insertadas.</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<ComputrabajoResena> listado)
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
        /// <summary>Actualiza una reseña existente desde el modal de edición.</summary>
        /// <param name="entidad">Entidad ComputrabajoResena con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] ComputrabajoResena entidad)
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
        /// <summary>Actualiza un listado de reseñas en bloque.</summary>
        /// <param name="listado">Lista de entidades ComputrabajoResena a actualizar.</param>
        /// <returns>Lista de entidades actualizadas.</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<ComputrabajoResena> listado)
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
        /// <summary>Elimina lógicamente una reseña por su Id.</summary>
        /// <param name="id">Id de la reseña a eliminar.</param>
        /// <returns>true si se eliminó correctamente.</returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
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
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        /// <param name="listadoIds">Lista de Ids a eliminar.</param>
        /// <returns>true si se eliminaron correctamente.</returns>
        [HttpDelete("EliminarListado")]
        public IActionResult EliminarListado([FromBody] List<int> listadoIds)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
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
        /// Retorna la grilla paginada de reseñas con filtros opcionales.
        /// Ejecuta mkt.SP_ComputrabajoResenaObtenerDatos.
        /// </summary>
        /// <param name="filtro">Filtros: visibilidad, país, tipo empleado, rango de fechas, paginación.</param>
        /// <returns>ComputrabajoResenaGrillaPaginadaDTO con datos y metadatos de paginación.</returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerGrilla([FromBody] ComputrabajoResenaGrillaFiltroDTO filtro)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try { return Ok(CrearServicio().ObtenerGrilla(filtro)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna los países con reseñas activas para el combo de filtros del frontend.</summary>
        /// <returns>Lista de ComputrabajoResenaPaisComboDTO con bandera.</returns>
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
        /// <summary>Retorna las ciudades de un país para el combo de filtros del frontend.</summary>
        /// <param name="idPais">Id del país a filtrar.</param>
        /// <returns>Lista de ComputrabajoResenaCiudadComboDTO.</returns>
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
        /// <summary>Marca como visibles (Mostrar=true) las reseñas indicadas por Id.</summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>true si se actualizaron correctamente.</returns>
        [HttpPost("[action]")]
        public IActionResult MarcarResenaVisible([FromBody] ComputrabajoResenaMarcarMostrarDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    dto.Usuario ??= _respuestaCorrecta.RegistroClaimToken.UserName;
                    return Ok(CrearServicio().MarcarResenaVisible(dto));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca como ocultas (Mostrar=false) las reseñas indicadas por Id.</summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>true si se actualizaron correctamente.</returns>
        [HttpPost("[action]")]
        public IActionResult MarcarResenaOculta([FromBody] ComputrabajoResenaMarcarMostrarDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    dto.Usuario ??= _respuestaCorrecta.RegistroClaimToken.UserName;
                    return Ok(CrearServicio().MarcarResenaOculta(dto));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }
    }
}
