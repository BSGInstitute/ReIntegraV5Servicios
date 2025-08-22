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
    /// Controlador: AprobarFurController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión de AprobarFur
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AprobarFurController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public AprobarFurController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Inserta Fur.
        /// </summary>
        /// <returns> entidad FUR</returns>
        [HttpPost("ObtenerFurPorAprobar")]
        public IActionResult ObtenerFurPorAprobar([FromBody] FiltroFurPorAprobar data)
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
                    var IdRol = _respuestaCorrecta.RegistroClaimToken.IdRol;
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerFurPorAprobar(data.IdArea, data.Codigo, IdRol, data.tipo));
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
        /// Inserta Fur.
        /// </summary>
        /// <returns> entidad FUR</returns>
        [HttpPost("AprobarObservarFurService")]
        public IActionResult AprobarObservarFurService([FromBody] AprobarObservaFurDTO data)
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
                    data.IdRol = _respuestaCorrecta.RegistroClaimToken.IdRol;
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.AprobarObservarFurService(data));
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
    }
}
