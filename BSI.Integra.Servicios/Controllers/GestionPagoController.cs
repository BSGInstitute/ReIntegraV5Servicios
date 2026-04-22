using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GestionPagoController
    /// Autor: Jose Vega
    /// Fecha: 30/03/2026
    /// <summary>
    /// Gestion del Modulo de Pagos - Escenario B (Cuotas Dependientes)
    /// Endpoints para CRUD de gestion de pago, cronograma, archivos y catalogos.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GestionPagoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public GestionPagoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Consultas

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene gestiones de pago con filtros opcionales
        /// </summary>
        [HttpPost("ObtenerGestionesPago")]
        public IActionResult ObtenerGestionesPago([FromBody] FiltroGestionPagoDTO filtro)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ObtenerGestionesPago(filtro));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene comprobantes combinados con gestiones de pago (si existen)
        /// </summary>
        [HttpPost("ObtenerReporteComprobantesYPagos")]
        public IActionResult ObtenerReporteComprobantesYPagos([FromBody] FiltroGestionPagoDTO filtro)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ObtenerReporteComprobantesYPagos(filtro));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene una gestion de pago por su Id
        /// </summary>
        [HttpGet("ObtenerPorId/{idGestionPago}")]
        public IActionResult ObtenerPorId(int idGestionPago)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ObtenerGestionPagoPorId(idGestionPago));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene una gestion de pago por IdComprobantePago
        /// </summary>
        [HttpGet("ObtenerPorComprobante/{idComprobantePago}")]
        public IActionResult ObtenerPorComprobante(int idComprobantePago)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ObtenerGestionPagoPorComprobante(idComprobantePago));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        #endregion

        #region Cronograma

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma de cuotas de una gestion de pago
        /// </summary>
        [HttpGet("ObtenerCronograma/{idGestionPago}")]
        public IActionResult ObtenerCronograma(int idGestionPago)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ObtenerCronogramaPorGestionPago(idGestionPago));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Registra la fecha real de pago de una cuota
        /// </summary>
        [HttpPost("RegistrarPagoCuota")]
        public IActionResult RegistrarPagoCuota([FromBody] GestionPagoCronogramaPagoDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    dto.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.RegistrarPagoCuota(dto));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        #endregion

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta una cuota individual al cronograma
        /// </summary>
        [HttpPost("InsertarCuota")]
        public IActionResult InsertarCuota([FromBody] CronogramaInsertarDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    dto.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.InsertarCuota(dto));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza una cuota individual del cronograma
        /// </summary>
        [HttpPut("ActualizarCuota")]
        public IActionResult ActualizarCuota([FromBody] CronogramaActualizarDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    dto.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ActualizarCuota(dto));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina (soft-delete) una cuota individual del cronograma
        /// </summary>
        [HttpDelete("EliminarCuota/{id}")]
        public IActionResult EliminarCuota(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.EliminarCuota(id, usuario));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        #region Archivos

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los archivos de cabecera de una gestion de pago
        /// </summary>
        [HttpGet("ObtenerArchivosCabecera/{idGestionPago}")]
        public IActionResult ObtenerArchivosCabecera(int idGestionPago)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ObtenerArchivosCabecera(idGestionPago));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los archivos (vouchers) de una cuota especifica
        /// </summary>
        [HttpGet("ObtenerArchivosCronograma/{idGestionPagoCronograma}")]
        public IActionResult ObtenerArchivosCronograma(int idGestionPagoCronograma)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ObtenerArchivosPorCronograma(idGestionPagoCronograma));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Agrega un archivo adjunto a una gestion de pago
        /// </summary>
        [HttpPost("InsertarArchivo/{idGestionPago}")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(50 * 1024 * 1024)]
        public async Task<IActionResult> InsertarArchivo(
            int idGestionPago,
            [FromForm] GestionPagoArchivoInsertarDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    if (dto.Archivo == null || dto.Archivo.Length == 0)
                        return BadRequest("Archivo requerido");

                    dto.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    var ok = await servicio.InsertarArchivoAsync(idGestionPago, dto);
                    return Ok(ok);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Descarga un archivo adjunto de una gestion de pago.
        /// </summary>
        [HttpGet("DescargarArchivo/{idArchivo}")]
        public async Task<IActionResult> DescargarArchivo(int idArchivo)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    var (stream, contentType, nombreArchivo) = await servicio.DescargarArchivoAsync(idArchivo);
                    return File(stream, contentType, nombreArchivo);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina logicamente un archivo adjunto
        /// </summary>
        [HttpDelete("EliminarArchivo/{idArchivo}")]
        public IActionResult EliminarArchivo(int idArchivo)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.EliminarArchivo(idArchivo, usuario));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        #endregion

        #region Catalogos

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tipos de modalidad de pago (Total, Parcial)
        /// </summary>
        [HttpGet("ObtenerModalidadesPago")]
        public IActionResult ObtenerModalidadesPago()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ObtenerModalidadesPago());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los estados de pago (Solicitado, Observado, Pendiente, Pagado)
        /// </summary>
        [HttpGet("ObtenerPagoEstados")]
        public IActionResult ObtenerPagoEstados()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ObtenerPagoEstados());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        #endregion

        #region Operaciones de Negocio

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta una gestion de pago completa (cabecera + cronograma + archivos)
        /// Paso 2 del flujo: Operaciones (PO) crea la gestion
        /// </summary>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] GestionPagoInsertarDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    dto.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.InsertarGestionPago(dto));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza una gestion de pago existente
        /// </summary>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] GestionPagoActualizarDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    dto.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.ActualizarGestionPago(dto));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Finanzas registra conformidad u observacion (afecta todas las cuotas)
        /// Paso 5 del flujo: Finanzas aprueba o observa
        /// </summary>
        [HttpPost("RegistrarConformidad")]
        public IActionResult RegistrarConformidad([FromBody] GestionPagoConformidadDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    dto.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.RegistrarConformidad(dto));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Operaciones levanta la observacion registrada por Finanzas
        /// </summary>
        [HttpPost("LevantarObservacion")]
        public IActionResult LevantarObservacion([FromBody] GestionPagoLevantamientoDTO dto)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    dto.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.LevantarObservacion(dto));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina logicamente una gestion de pago
        /// </summary>
        [HttpDelete("Eliminar/{idGestionPago}")]
        public IActionResult Eliminar(int idGestionPago)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new GestionPagoService(unitOfWork);
                    return Ok(servicio.Delete(idGestionPago, usuario));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        #endregion
    }
}
