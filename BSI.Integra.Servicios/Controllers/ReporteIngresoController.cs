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
    /// Controlador: ReporteIngresoController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ReporteIngreso
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteIngresoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteIngresoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerReporteIngresosVentas")]
        [HttpPost]
        public ActionResult ObtenerReporteIngresosVentas(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerReporteIngresosVentas(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerReporteIngresosOperaciones")]
        [HttpPost]
        public ActionResult ObtenerReporteIngresosOperaciones(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerReporteIngresosOperaciones(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerReporteIngresosOperacionesTipoCambio")]
        [HttpPost]
        public ActionResult ObtenerReporteIngresosOperacionesTipoCambio(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerReporteIngresosOperacionesTipoCambio(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerReporteIngresosOtrosIngresos")]
        [HttpPost]
        public ActionResult ObtenerReporteIngresosOtrosIngresos(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerReporteIngresosOtrosIngresos(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerPagosIngresos")]
        [HttpPost]
        public ActionResult ObtenerPagosIngresos(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerPagosIngresos(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerPagosIngresosPosterior")]
        [HttpPost]
        public ActionResult ObtenerPagosIngresosPosterior(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerPagosIngresosPosterior(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerPagosIngresosAnterior")]
        [HttpPost]
        public ActionResult ObtenerPagosIngresosAnterior(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerPagosIngresosAnterior(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerPagosIngresosGestionCobranza")]
        [HttpPost]
        public ActionResult ObtenerPagosIngresosGestionCobranza(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerPagosIngresosGestionCobranza(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerPagosTasasAcademicas")]
        [HttpPost]
        public ActionResult ObtenerPagosTasasAcademicas(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerPagosTasasAcademicas(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerPagosIngresosAnteriorConDeposito")]
        [HttpPost]
        public ActionResult ObtenerPagosIngresosAnteriorConDeposito(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerPagosIngresosAnteriorConDeposito(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerPagosIngresosPosteriorConDeposito")]
        [HttpPost]
        public ActionResult ObtenerPagosIngresosPosteriorConDeposito(FiltroFechaDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerPagosIngresosPosteriorConDeposito(Filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerReporteIngresosFinal")]
        [HttpPost]
        public ActionResult ObtenerReporteIngresosFinal(ReporteCompletoDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerReporteIngresosFinal(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta un Registro de Reporte Ingreso COngelamiento
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("InsertarReporteIngresoCongelamiento")]
        [HttpPost]
        public ActionResult InsertarReporteIngresoCongelamiento(ReporteIngresoCongelamientoRecibidoDTO data)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.InsertarReporteIngresoCongelamiento(data, _respuestaCorrecta.RegistroClaimToken.UserName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta un Registro de Reporte Ingreso COngelamiento
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("EliminarReporteIngresoCongelamiento/{Id}")]
        [HttpDelete]
        public ActionResult EliminarReporteIngresoCongelamiento(int Id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.EliminarReporteIngresoCongelamiento(Id, _respuestaCorrecta.RegistroClaimToken.UserName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta un Registro de Reporte Ingreso COngelamiento
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        [Route("ObtenerReporteIngresoCongelamiento")]
        [HttpGet]
        public ActionResult ObtenerReporteIngresoCongelamiento()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReporteIngresoService(unitOfWork);
                var result = servicio.ObtenerReporteIngresoCongelamiento();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
