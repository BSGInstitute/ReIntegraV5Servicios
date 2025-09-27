
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FacturamaApiController : ControllerBase
    {
        private FacturamaService _facturamaApiService;
        private IUnitOfWork unitOfWork;


        public FacturamaApiController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _facturamaApiService = new FacturamaService(unitOfWork);

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CrearFactura(FacturamaFacturaDTO factura)
        {
            if (factura == null)
            {
                return BadRequest("La factura no puede ser nula.");
            }
            var (resultado, statusCode) = await _facturamaApiService.CrearFacturaAsync(factura);

            if (statusCode == HttpStatusCode.Created || statusCode == HttpStatusCode.OK)
            {
                return Ok(new { mensaje = "Factura creada con éxito.", resultado });
            }
            else
            {
                return StatusCode((int)statusCode, new { error = resultado });
            }
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> CrearCliente(FacturamaClienteDTO cliente)
        {
            var (resultado, statusCode) = await _facturamaApiService.CrearClienteAsync(cliente);

            if (statusCode == HttpStatusCode.Created || statusCode == HttpStatusCode.OK)
            {
                return Ok(new { mensaje = "Cliente creado con éxito.", resultado });  // Cliente creado correctamente
            }
            else
            {
                return StatusCode((int)statusCode, new { error = resultado });  // Error en la respuesta
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DatosCompletosFacturama([FromBody] FacturamaFacturaClienteDTO datos)
        {
            if (datos == null || datos.factura == null || datos.cliente == null)
            {
                return BadRequest(new { mensaje = "Los datos de la factura o del cliente no pueden ser nulos." });
            }

            try
            {
                // Llamada al servicio para validar cliente y crear factura
                var (resultado, statusCode) = await _facturamaApiService.DatosCompletosFacturama(datos);

                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)
                {
                    return Ok(new { mensaje = "Factura creada con éxito.", resultado });
                }
                else
                {
                    return StatusCode((int)statusCode, new { error = resultado });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error inesperado: {ex.Message}" });
            }
        }

        [HttpGet("BuscarCliente")]
        public async Task<IActionResult> BuscarCliente([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new { mensaje = "El parámetro 'keyword' es obligatorio y no puede estar vacío o nulo." });
            }

            try
            {
                var (resultado, statusCode) = await _facturamaApiService.BuscarClienteAsync(keyword);

                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.NoContent)
                {
                    if (string.IsNullOrWhiteSpace(resultado) || resultado == "[]")
                    {
                        return NotFound(new { mensaje = "No se encontraron clientes que coincidan con el keyword proporcionado." });
                    }
                    return Ok(new { mensaje = "Clientes encontrados.", clientes = resultado });
                }
                else
                {
                    return StatusCode((int)statusCode, new { error = resultado });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error inesperado: {ex.Message}" });
            }
        }



        [HttpGet("ObtenerListaRegimenFiscal")]
        public IActionResult ObtenerListaRegimenFiscal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _facturamaApiService.ObtenerListaRegimenFiscal();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObtenerListaUsoCfdi")]
        public IActionResult ObtenerListaUsoCfdi()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _facturamaApiService.ObtenerListaUsoCfdi();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("ObtenerFormapagoFacturama")]
        public IActionResult ObtenerFormapagoFacturama()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _facturamaApiService.ObtenerFormapagoFacturama();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ActualizarFacturamaEnvio/{id}/{usuarioModificacion}")]
        public IActionResult ActualizarFacturamaEnvio(int id, string usuarioModificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var respuesta = _facturamaApiService.ActualizaEnviadoFacturama(id, usuarioModificacion);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarRegimenFiscal(RegimenFiscalDatosDTO datos)
        {
            try
            {
                string idNuevo = _facturamaApiService.InsertarRegimenFiscal(datos.Clave, datos.Descripcion, datos.UsuarioCreacion);
                return Ok(new { message = "Historial de oportunidad insertado correctamente." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("[action]")]
        public ActionResult ActualizarRegimenFiscal([FromBody] RegimenFiscalAcDTO datos)
        {
            try
            {
                bool actualizado = _facturamaApiService.ActualizarRegimenFiscal(datos.Id, datos.Clave, datos.Descripcion, datos.UsuarioModificacion);

                if (actualizado)
                    return Ok(new { message = "Registro actualizado correctamente." });

                return BadRequest("No se pudo actualizar el registro.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar: " + ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult EliminarRegimenFiscal([FromBody] RegimenFiscalEliDTO datos)
        {
            try
            {
                bool eliminado = _facturamaApiService.EliminarRegimenFiscal(datos.Id, datos.UsuarioModificacion);
                if (eliminado)
                    return Ok(new { message = "Registro eliminado correctamente." });
                return BadRequest("No se pudo eliminar el registro.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarUsoComprobante(UsoCfdiDatosDTO datos)
        {
            try
            {
                string idNuevo = _facturamaApiService.InsertarUsoComprobante(datos.Clave, datos.Descripcion, datos.UsuarioCreacion);
                return Ok(new { message = "Historial de oportunidad insertado correctamente." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("[action]")]
        public ActionResult ActualizarUsoComprobante([FromBody] RegimenFiscalAcDTO datos)
        {
            try
            {
                bool actualizado = _facturamaApiService.ActualizarUsoComprobante(datos.Id, datos.Clave, datos.Descripcion, datos.UsuarioModificacion);

                if (actualizado)
                    return Ok(new { message = "Registro actualizado correctamente." });

                return BadRequest("No se pudo actualizar el registro.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al actualizar: " + ex.Message);
            }
        }
        [HttpPost("[action]")]
        public ActionResult EliminarUsoComprobante([FromBody] RegimenFiscalEliDTO datos)
        {
            try
            {
                bool eliminado = _facturamaApiService.EliminarUsoComprobante(datos.Id, datos.UsuarioModificacion);
                if (eliminado)
                    return Ok(new { message = "Registro eliminado correctamente." });
                return BadRequest("No se pudo eliminar el registro.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("ObtenerResumenMatriculas")]
        public ActionResult ObtenerResumenMatriculas([FromBody] FiltroFechaDTO filtro)
        {
            try
            {
                var resultado = _facturamaApiService.ObtenerResumenMatriculas(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = "Error al obtener el resumen de matrículas.", detalle = ex.Message });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarFacturaInterna([FromBody] FacturamaFacturaClienteDTO datos, [FromQuery] string codigoMatricula, [FromQuery] int idCronogramaPagoDetalleFinal)
        {
            if (datos == null || datos.factura == null || datos.cliente == null)
                return BadRequest("Datos incompletos.");

            var usuario = "sistemv5";

            var idFactura = await _facturamaApiService.GuardarFacturaClienteCompleta(datos, codigoMatricula, idCronogramaPagoDetalleFinal,usuario);

            return Ok(new { mensaje = "Factura guardada internamente con éxito.", idFactura });
        }

        [HttpPost("EnviarFacturaApi")]
        public async Task<IActionResult> EnviarFacturaApi([FromBody] EnvioApiDTO datos)
        {
            try
            {
                await _facturamaApiService.EnviarFacturaDesdeBaseDeDatos(datos.idFactura, datos.usuario);
                return Ok(new { mensaje = "Factura enviada correctamente a Facturama." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    



        [Route("[action]/{codigoMatricula}")]
        [HttpGet]
        public async Task<IActionResult> ObtenerFacturaPorCodigoMatricula(string codigoMatricula)
        {
            try
            {
                var datos = _facturamaApiService.ObtenerFacturaClientePorCodigoMatricula(codigoMatricula);
                if (datos == null)
                    return Ok(null);

                return Ok(datos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       

        [Route("[action]/{codigoMatricula}")]
        [HttpGet]
        public IActionResult ObtenerIdFacturaPorCodigo(string codigoMatricula)
        {
            var idFactura = _facturamaApiService.ObtenerIdFacturaPorCodigoMatricula(codigoMatricula);
            return Ok(new { idFactura });
        }

        [HttpGet("ListarPendientesEnvio")]
        public IActionResult ListarPendientesEnvio()
        {
            var resultado = _facturamaApiService.ObtenerFacturasPendientesEnvio();
            return Ok(resultado);
        }



        [HttpPost("EnviarFacturasMasivasLote")]
        public async Task<IActionResult> EnviarFacturasMasivasLote([FromBody] EnvioMasivoLoteDTO datos)
        {
            try
            {
                await _facturamaApiService.EnviarFacturasMasivasDesdeBaseDeDatos(datos);
                return Ok(new { mensaje = "Facturación masiva completada con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarFacturaGlobalInterna([FromBody] FacturamaFacturaClienteDTO datos, [FromQuery] string codigoMatricula, [FromQuery] int idCronogramaPagoDetalleFinal)
        {
            if (datos == null || datos.factura == null || datos.cliente == null)
                return BadRequest("Datos incompletos.");

            var usuario = "sistemv5";

            var idFactura = await _facturamaApiService.GuardarFacturaClienteCompleta(datos, codigoMatricula, idCronogramaPagoDetalleFinal, usuario);

            return Ok(new { mensaje = "Factura guardada internamente con éxito.", idFactura });
        }

        [HttpGet("ExisteFacturaConfigurada")]
        public IActionResult ExisteFacturaConfigurada([FromQuery] int idCronogramaPagoDetalleFinal)
        {
            var resultado = _facturamaApiService.ExisteFacturaConfigurada(idCronogramaPagoDetalleFinal);
            return Ok(resultado);
        }

        /// Autor: Humberto Oscata
        /// Fecha: 18/09/2025
        /// Version: 1.0
        /// <summary>
        /// Elimina facturas creadas y pendientes de emitir Facturama
        /// </summary>
        /// <param name="idsFacturas">Lista de ids de las facturas a eliminar</param>
        /// <returns>Resultado (correcto o incorrecto) del eliminar</returns>
        [Authorize]
        [Route("EliminarFacturasPendientesFacturama")]
        [HttpPost]
        public IActionResult EliminarFacturasPendientesFacturama([FromBody] EliminarFacturasRequest request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var service = new ActividadCabeceraService(unitOfWork);
                var usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                if (string.IsNullOrWhiteSpace(usuario))
                    return Unauthorized("No se pudo identificar el usuario.");

                var resultado = _facturamaApiService.EliminarFacturasPendientesFacturama(request.IdsFacturas, usuario);
                if (resultado)
                    return Ok(new { Message = "Registros eliminados correctamente." });
             
                return BadRequest("No se puedieron eliminar todos los registros");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

