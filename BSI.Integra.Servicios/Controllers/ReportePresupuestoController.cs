using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReportePresupuestoController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ReportePresupuesto
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReportePresupuestoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReportePresupuestoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("ObtenerReportePresupuestoFinanzas")]
        [HttpPost]
        public ActionResult ObtenerReportePresupuestoFinanzas(FiltroPresupuestoDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReportePresupuestoService(unitOfWork);
                var result = servicio.ObtenerReportePresupuestoFinanzas(filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: PUT
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("ActualizarEsDiferidoListaFur")]
        [HttpPut]
        public ActionResult  ActualizarEsDiferidoListaFur(DiferirFurDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                datos.Usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new ReportePresupuestoService(unitOfWork);
                var result = servicio.ActualizarEsDiferidoListaFur(datos);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
