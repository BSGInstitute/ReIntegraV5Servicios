using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AlumnoCasoExitoController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IAlumnoCasoExitoService _alumnoCasoExitoService;
        private IUnitOfWork _unitOfWork;

        public AlumnoCasoExitoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork             = unitOfWork;
            _tokenManager           = tokenManager;
            _alumnoCasoExitoService = new AlumnoCasoExitoService(unitOfWork);
        }

        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _alumnoCasoExitoService.Obtener();
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            try
            {
                var resultado = _alumnoCasoExitoService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("[action]/{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var resultado = _alumnoCasoExitoService.ObtenerPorId(id);
                if (resultado == null) return NotFound();
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarConArchivo([FromForm] AlumnoCasoExitoEntradaDTO dto)
        {
            try
            {
                if (dto.ArchivoFotoPerfil != null)
                {
                    var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
                    if (!tiposPermitidos.Contains(dto.ArchivoFotoPerfil.ContentType.ToLower()))
                        return BadRequest("Tipo de archivo no permitido. Solo se aceptan imágenes (JPG, PNG, GIF, WEBP).");
                }
                var resultado = _alumnoCasoExitoService.Insertar(dto, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarConArchivo([FromForm] AlumnoCasoExitoEntradaDTO dto)
        {
            try
            {
                if (dto.ArchivoFotoPerfil != null)
                {
                    var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
                    if (!tiposPermitidos.Contains(dto.ArchivoFotoPerfil.ContentType.ToLower()))
                        return BadRequest("Tipo de archivo no permitido. Solo se aceptan imágenes (JPG, PNG, GIF, WEBP).");
                }
                var resultado = _alumnoCasoExitoService.Actualizar(dto, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var resultado = _alumnoCasoExitoService.Eliminar(id, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarVisibilidad([FromBody] AlumnoCasoExitoVisibilidadDTO dto)
        {
            try
            {
                var resultado = _alumnoCasoExitoService.ActualizarVisibilidad(dto.Id, dto.Visibilidad, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarPosiciones([FromBody] List<AlumnoCasoExitoPosicionDTO> posiciones)
        {
            try
            {
                var resultado = _alumnoCasoExitoService.ActualizarPosiciones(posiciones, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
