using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ActivarFurController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ActivarFur
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ActivarFurController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ActivarFurController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Furs que estan en proceso de eliminacion.
        /// </summary>
        /// <returns> entidad FUR</returns>
        [HttpGet("ObtenerFursNoEjecutados")]
        public IActionResult ObtenerFursNoEjecutados()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicio = new FurService(unitOfWork);
                    return Ok(servicio.ObtenerFursNoEjecutados());
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
        /// Version: 1.0
        /// <summary>
        /// Activa el FUR 
        /// </summary>
        /// <returns> true,False </returns>
        [HttpPost("ActivarFurNoEjecutado/{Usuario}")]
        public ActionResult ActivarFurNoEjecutado(List<int> listadoIds, string usuario)
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
                    var servicio = new FurService(unitOfWork);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                        foreach (var id in listadoIds)
                        {
                            var rpt = servicio.ActivarFurNoEjecutado(id, usuario);
                            if (rpt == false) return Ok(false);
                        }
                        scope.Complete();
                    }
                    return Ok(true);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
           
        }
    }
}
