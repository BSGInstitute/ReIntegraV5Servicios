using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: RegistrarFurPagoController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de RegistrarFurPago
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RegistrarFurPagoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public RegistrarFurPagoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_FurPago
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        /// /// <paramref name="data"/> GRupo de parametros
        [HttpPost("BuscarListaFurPagos")]
        public IActionResult BuscarListaFurPagos([FromBody] FiltroFurPagoDTO data)
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
                    var servicio = new FurPagoService(unitOfWork);
                    return Ok(servicio.BuscarListaFurPagos(data));
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

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_FurPago
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        /// /// <paramref name="data"/> GRupo de parametros
        [HttpPost("AsociarComprobante")]
        public IActionResult AsociarComprobante([FromBody] AsociarComprobateDTO data)
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
                    data.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new ComprobantePagoPorFurService(unitOfWork);
                    return Ok(servicio.AsociarComprobante(data));
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

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_FurPago
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        /// <paramref name="IdFur"/> IdFur
        [HttpGet("ObtenerPagosRealizadosPorFur/{IdFur}")]
        public IActionResult ObtenerPagosRealizadosPorFur(int IdFur)
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
                    var servicio = new FurPagoService(unitOfWork);
                    return Ok(servicio.ObtenerPagosRealizadosPorFur(IdFur));
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


        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_FurPago
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        /// <paramref name="IdFur"/> IdFur
        [HttpGet("ObtenerComprobantesPorFurParaPago/{IdFur}")]
        public IActionResult ObtenerComprobantesPorFurParaPago(int IdFur)
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
                    var servicio = new ComprobantePagoService(unitOfWork);
                    return Ok(servicio.ObtenerComprobantesPorFurParaPago(IdFur));
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

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Insertar pago para fur
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        /// <paramref name="IdFur"/> IdFur
        [HttpPost("InsertarFurPago")]
        public IActionResult InsertarFurPago([FromBody] RegistrarFurPagoDTO Json)
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
                    Json.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new FurPagoService(unitOfWork);
                    return Ok(servicio.InsertarFurPago(Json));
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


        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Insertar pago para fur
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        /// <paramref name="IdFur"/> IdFur
        [HttpPost("ActualizarFurPago")]
        public IActionResult ActualizarFurPago([FromBody] RegistrarFurPagoDTO Json)
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
                    Json.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new FurPagoService(unitOfWork);
                    return Ok(servicio.ActualizarFurPago(Json));
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

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Insertar pago para fur
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        /// <paramref name="IdFur"/> IdFur
        [HttpDelete("ElminarFurPago/{idFurPago}")]
        public IActionResult ElminarFurPago(int idFurPago)
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
                    var Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new FurPagoService(unitOfWork);
                    return Ok(servicio.Delete(idFurPago, Usuario));
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


        [HttpGet("ObtenerListaFormaPago")]
        public IActionResult ObtenerListaFormaPago()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new FurPagoService(unitOfWork);
                    return Ok(servicio.ObtenerListaFormaPago());
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

        /// Tipo Función: GET
        /// Autor: Miguel Valdivia
        /// Fecha: 24/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Convierte un monto de una moneda origen a una moneda destino usando el tipo de cambio mas reciente
        /// </summary>
        /// <param name="idMonedaOrigen">Id de la moneda de origen (ej: 19=USD, 20=PEN, 9=CLP)</param>
        /// <param name="idMonedaDestino">Id de la moneda de destino</param>
        /// <param name="monto">Monto a convertir</param>
        /// <returns>Retorna 200 con ConversionMonedaDTO o 400 con mensaje de error</returns>
        [AllowAnonymous] // TODO: Remover después de las pruebas
        [HttpGet("ConvertirMoneda/{idMonedaOrigen}/{idMonedaDestino}/{monto}")]
        public IActionResult ConvertirMoneda(int idMonedaOrigen, int idMonedaDestino, decimal monto)
        {
            // TODO: Descomentar después de las pruebas
            //var claimsIdentity = User.Identity as ClaimsIdentity;
            //var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            //if (_respuestaCorrecta.TokenValida)
            //{
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var servicio = new FurPagoService(unitOfWork);
                    return Ok(servicio.ConvertirMoneda(idMonedaOrigen, idMonedaDestino, monto));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

    }
}
