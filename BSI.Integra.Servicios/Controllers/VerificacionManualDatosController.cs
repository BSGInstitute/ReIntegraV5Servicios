using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: VerificacionManualDatosController
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 02/22/2022
    /// <summary>
    /// Gestión de Verificación Manual de Datos
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class VerificacionManualDatosController : Controller
    {
        private IUnitOfWork unitOfWork;
        public VerificacionManualDatosController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 10/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los filtros necesario para Verificacion Manual Datos
        /// </summary>
        /// <returns> VerificacionManualDatosDTO </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerFiltroCombos()
        {
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
                        var centroCostoServicio = new CentroCostoService(unitOfWork);
                        var categoriaDatoServicio = new CategoriaOrigenService(unitOfWork);
                        var paisServicio = new PaisService(unitOfWork);
                        var ciudadServicio = new CiudadService(unitOfWork);
                        var probabilidadServicio = new ProbabilidadRegistroPwService(unitOfWork);
                        var industriaServicio = new IndustriaService(unitOfWork);
                        var cargoServicio = new CargoService(unitOfWork);
                        var areaTrabajoServicio = new AreaTrabajoService(unitOfWork);
                        var areaFormacionServicio = new AreaFormacionService(unitOfWork);
                        VerificacionManualDatosDTO verificacionManualDatosFiltro = new VerificacionManualDatosDTO();
                        verificacionManualDatosFiltro.listaCentroCosto = centroCostoServicio.ObtenerCombo();
                        verificacionManualDatosFiltro.listaCategoriaDato = categoriaDatoServicio.ObtenerCombo();
                        verificacionManualDatosFiltro.listaPais = paisServicio.ObtenerPaisCombo();
                        verificacionManualDatosFiltro.listaCiudad = ciudadServicio.ObtenerCombo();
                        verificacionManualDatosFiltro.listaProbabilidad = probabilidadServicio.ObtenerCombo();
                        verificacionManualDatosFiltro.listaIndustria = industriaServicio.ObtenerCombo();
                        verificacionManualDatosFiltro.listaCargo = cargoServicio.ObtenerCombo();
                        verificacionManualDatosFiltro.listaAreaTrabajo = areaTrabajoServicio.ObtenerCombo();
                        verificacionManualDatosFiltro.listaAreaFormacion = areaFormacionServicio.ObtenerCombo();
                        return Ok(verificacionManualDatosFiltro);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 10/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros importados según filtros para Asignación Automática
        /// </summary>
        /// <returns> AsignacionAutomaticaCompuestoImportadosDTO </returns>
        [HttpPost("[Action]")]
        public IActionResult ObtenerReporteVerificacionManualDatos([FromBody] FiltroBusquedaVerificacionManualDatosCompuestoDTO paginador)
        {
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
                        var servicio = new VerificacionManualDatosService(unitOfWork);
                        var Registros = servicio.ObtenerDatosVerificacion(paginador);
                        var Total = Registros.Count() == 0 ? 0 : Registros.FirstOrDefault().TotalRegistros;
                        return Ok(new { data = Registros, Total });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
    }
}
