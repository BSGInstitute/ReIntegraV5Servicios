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
    /// Controlador: TipoCambioMonedaController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de TipoCambioMonedum
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TipoCambioMonedaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public TipoCambioMonedaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoCambioMonedum
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerTipoCambioMonedum")]
        public IActionResult ObtenerTipoCambioMonedum()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new TipoCambioMonedumService(unitOfWork);
                return Ok(servicio.ObtenerTipoCambioMonedum());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Periodo para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerTipoCambioFiltro")]
        public IActionResult ObtenerTipoCambioFiltro([FromBody] TipoCambioFiltroDTO filtro)
        {
            try
            {
                var servicio = new TipoCambioService(unitOfWork);
                return Ok(servicio.ObtenerTipoCambioFiltro(filtro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: PUT
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza en T_TipoCambioMoneda,T_TipoCambio,T_TipoCAmbioCol.
        /// </summary>
        /// <returns> TTipoCambioMonedum </returns>
        /// <param name="TipoCambioMonedaDTO">Grupo de Datos, para guardar.</param>
        [HttpPut("ActualizarGeneral")]
        public IActionResult ActualizarGeneral(FiltroTipoCambioMonedaDTO TipoCambioMonedaDTO)
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
                    TipoCambioMonedaDTO.NombreUsuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new TipoCambioMonedumService(unitOfWork);
                    return Ok(servicio.ActualizarGeneral(TipoCambioMonedaDTO));
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
        /// Tipo Función: DELETE
        /// Autor: Griselberto Huaman.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza en T_TipoCambioMoneda,T_TipoCambio,T_TipoCAmbioCol.
        /// </summary>
        /// <returns> Ture, ó error. </returns>
        /// <param name="Usuario">Usuario de eliminacion</param>
        /// <param name="Id">Id de registro a eleiminar</param>
        [HttpDelete("EliminarGeneral/{Id}")]
        public IActionResult EliminarGeneral(int Id)
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
                    var servicio = new TipoCambioMonedumService(unitOfWork);
                    return Ok(servicio.EliminarGeneral(Id, _respuestaCorrecta.RegistroClaimToken.UserName));
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
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="TipoCambioDTO">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("InsertarGeneral")]
        public IActionResult InsertarGeneral(FiltroTipoCambioMonedaDTO tipoCambioMonedaDTO)
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
                    tipoCambioMonedaDTO.NombreUsuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                    var servicio = new TipoCambioMonedumService(unitOfWork);
                    var respuesta = servicio.InsertarGeneral(tipoCambioMonedaDTO);
                    return Ok(respuesta);
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

        /// Tipo Función:GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza en T_TipoCambioMoneda,T_TipoCambio,T_TipoCAmbioCol.
        /// </summary>
        /// <returns> Ture, ó error. </returns>
        /// <param name="Usuario">Usuario de eliminacion</param>
        /// <param name="Id">Id de registro a eleiminar</param>
        [HttpGet("ObtenerTasaCambioMoneda/{idMoneda}")]
        public IActionResult ObtenerTasaCambioMoneda(int idMoneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new TipoCambioMonedumService(unitOfWork);
                return Ok(servicio.ObtenerTasaCambioMoneda(idMoneda));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

    }
}
