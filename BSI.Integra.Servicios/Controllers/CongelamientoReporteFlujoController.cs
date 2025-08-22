using BSI.Integra.Aplicacion.DTO;
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
    /// Controlador: CongelamientoReporteFlujoController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/09/2023
    /// <summary>
    /// Gestión de CongelamientoReporteFlujo
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CongelamientoReporteFlujoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public CongelamientoReporteFlujoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// TipoFuncion: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("GenerarCongelamientoReporte")]
        [HttpPost]
        public ActionResult GenerarCongelamientoReporte(List<FlujoCongelamientoDTO> FlujoCongelamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.GenerarCongelamientoReporte(FlujoCongelamiento));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ReporteFlujoMaestro")]
        [HttpPost]
        public ActionResult ReporteFlujoMaestro(ReporteFlujoMaestroFiltroDTO Parametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.ReporteFlujoMaestro(Parametros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("ObtenerIdMatriculaPorCodigo")]
        [HttpPost]
        public ActionResult ObtenerIdMatriculaPorCodigo(StringDTO codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.ObtenerIdMatriculaPorCodigo(codigo.Valor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("ObternerTodosCoordinadores")]
        [HttpGet]
        public ActionResult ObternerTodosCoordinadores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.ObternerTodosCoordinadores());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("ObtenerListaInHouse")]
        [HttpGet]
        public ActionResult ObtenerListaInHouse()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.ObtenerListaInHouse());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("InsertarCambiosPeriodo")]
        [HttpPost]
        public ActionResult InsertarCambiosPeriodo(List<SubidaExcelDTO> Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.InsertarCambiosPeriodo(Json, usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EditarReporteFlujoMaestro")]
        [HttpPost]
        public ActionResult EditarReporteFlujoMaestro(EditarReporteFlujoMaestroFiltroDTO Parametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.EditarReporteFlujoMaestro(Parametros, usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("EliminarReporteFlujoMaestro")]
        [HttpPost]
        public ActionResult EliminarReporteFlujoMaestro(IdDTO id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var servicio = new ReporteFlujoCongeladoPorDiumService(unitOfWork);
                return Ok(servicio.Delete(id.Id, usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ExportarCongelados")]
        [HttpPost]
        public ActionResult ExportarCongelados(FechaInicioFinDTO fechas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.ExportarCongelados(fechas));
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
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("CongelarReporteDeFlujoPorDia")]
        [HttpPost]
        public ActionResult CongelarReporteDeFlujoPorDia([FromBody] CongelarFlujoDTO fecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                var servicio = new ReporteService(unitOfWork);
                return Ok(servicio.CongelarReporteOriginalesPorDia(fecha.FechaCongelamiento.Value, _respuestaCorrecta.RegistroClaimToken.UserName));
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
        /// Obtiene los registros de Codigo de Matricula para filtro
        /// </summary>
        /// <param></param>
        /// <returns> objeto lista DTO : List<MatriculaCabeceraComboDTO> </returns>
        [Route("CongelarReporteOriginalesPorPeriodo")]
        [HttpPost]
        public ActionResult CongelarReporteOriginalesPorPeriodo([FromBody] CongelarFlujoDTO fecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                var servicio = new ReporteService(unitOfWork);
                return Ok(servicio.CongelarReporteDeFlujoPorPeriodo(_respuestaCorrecta.RegistroClaimToken.UserName, fecha.IdPeriodo.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CongelarReporteOriginalesPorDia")]
        [HttpPost]
        public ActionResult CongelarReporteOriginalesPorDia([FromBody] CongelarFlujoDTO fecha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                var servicio = new ReporteService(unitOfWork);
                return Ok(servicio.CongelarReporteOriginalesPorDia(fecha.FechaCongelamiento.Value, _respuestaCorrecta.RegistroClaimToken.UserName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizarEstadoInHouseMatricula")]
        [HttpPost]
        public ActionResult ActualizarEstadoInHouseMatricula(ActualizarInhouseDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.ActualizarEstadoInHouseMatricula(datos.IdMatriculaCabecera, datos.EsInhouse));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizarEstadoInHouseCodigoMatricula")]
        [HttpPost]
        public ActionResult ActualizarEstadoInHouseCodigoMatricula(ActualizarInhouseCodigoDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicio = new CongelamientoReporteFlujoService(unitOfWork);
                return Ok(servicio.ActualizarEstadoInHouseCodigoMatricula(datos.CodigoMatricula, datos.EsInhouse, usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}