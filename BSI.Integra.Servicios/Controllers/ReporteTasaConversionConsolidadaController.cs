using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteTasaConversionConsolidadaController
    /// Autor: Gilmer Quispe.
    /// Fecha: 05/10/2022
    /// <summary>
    /// Gestión de Reportes tasas de conversion consolidadas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteTasaConversionConsolidadaController : Controller
    {

        private IUnitOfWork unitOfWork;
        private IConfiguracionAccesoPersonalService _configuracionAccesoPersonalService;
        public ReporteTasaConversionConsolidadaController(IUnitOfWork unitOfWork, IConfiguracionAccesoPersonalService configuracionAccesoPersonalService)
        {
            this.unitOfWork = unitOfWork;
            _configuracionAccesoPersonalService = configuracionAccesoPersonalService;
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para reporte
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporte()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicioPersonal = new PersonalService(unitOfWork);
                    var servicioPeriodo = new BSI.Integra.Aplicacion.Marketing.Service.Implementacion.PeriodoService(unitOfWork);

                    var resultado = new ReporteTasaConversionConsolidadaGeneralDTO();
                    var idPersonal = _configuracionAccesoPersonalService.ObtenerIdPersonalAcceso(_respuestaCorrecta.RegistroClaimToken.IdPersonal, "Comercial/ReporteIngresoPorAsesor");

                    resultado.Coordinadores = servicioPersonal.ObtenerCoordinadoresVentasOficialRI(idPersonal).Where(w => w.TipoPersonal == "Coordinador").ToList();
                    resultado.Asesores = servicioPersonal.ObtenerAsesoresVentasOficialRI(idPersonal).Where(w => w.TipoPersonal == "Asesor" || w.TipoPersonal == "otro").ToList();
                    resultado.Periodos = servicioPeriodo.ObtenerCombo();
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
                throw new UnauthorizedAccessException("Usted no tiene acceso");


        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Agregado Lis para buscar asesores correspondientes por coordinadores
        /// </summary>
        /// <returns> Objeto DTO: List<FiltroDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult DarPeriodoActual()
        {
            var servicioPeriodo = new BSI.Integra.Aplicacion.Marketing.Service.Implementacion.PeriodoService(unitOfWork);
            var resultado = servicioPeriodo.ObtenerIdPeriodoFechaActual().Select(o => new { Id = o.Id, Nombre = o.Nombre });
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Gráficas
        /// </summary>
        /// <returns> objeto : ReporteTasaConversionConsolidadaGraficasVistaDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteGraficas([FromBody] ReporteTasaConversionConsolidadaGraficaFiltroDTO? filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                try
                {
                    if (filtro.TipoPeriodo == 2)
                    {
                        var listRpta = servicioReporte.ReporteTasaConversionConsolidadoAsesoresGraficaMensual(filtro);
                        listRpta.Consolidado = listRpta.Consolidado.OrderBy(x => x.Ano).ThenBy(x => x.MesNumero).ToList();
                        return Ok(new { Records = listRpta });
                    }
                    else
                    {
                        var listRpta = servicioReporte.ReporteTasaConversionConsolidadoAsesoresGrafica(filtro);
                        return Ok(new { Records = listRpta });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 05/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del personal segun el id enviado
        /// </summary>
        /// <returns> objeto : PersonalInformacionAgendaDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpPost]
        public IActionResult GenerarAsesoresCoordinadores(int idPersonal)
        {
            try
            {
                var servicioPersonal = new PersonalService(unitOfWork);
                var personalInformacionAgendaDTO = new PersonalInformacionAgendaDTO();
                personalInformacionAgendaDTO.Asignados = servicioPersonal.ObtenerPersonalAsignado(idPersonal);
                return Ok(personalInformacionAgendaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para SubArea
        /// </summary>
        /// <returns> Lista de Objetos: int,string,int </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboSubArea()
        {
            try
            {
                SubAreaCapacitacionService servicioSubAreaCapacitacion = new SubAreaCapacitacionService(unitOfWork);
                var resultado = servicioSubAreaCapacitacion.ObtenerCombo().Select(o => new { id = o.Id, name = o.Nombre, area = o.IdAreaCapacitacion }).ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Reporte de Tasas , es una version igual al reporte de tasa de conversion consolidada , con la diferencia que muestra  desde la grilla de categoria dato consolidado
        /// </summary>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteTasas([FromBody] ReporteTasaConversionConsolidadaFiltroDTO? filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteService reporteServicio = new ReporteService(unitOfWork);
                var listRpta = reporteServicio.ReporteTasaConversionConsolidadoAsesores(filtro);
                var centroCostoOportunidades = reporteServicio.ObtenerCentroCostoPorAsesor(filtro);
                var agrupado = (from p in listRpta.Consolidado
                                group p by p.probabilidadDesc into grupo
                                select new { g = grupo.Key, l = grupo }).ToList();
                var agrupado2 = (from p in listRpta.Desagregado
                                 group p by p.probabilidadDesc into grupo
                                 select new { g = grupo.Key, l = grupo }).ToList();
                var agrupado3 = (from p in centroCostoOportunidades
                                 group p by new
                                 {
                                     p.idasesor
                                 }
                                 into grupo
                                 select new TCRM_CentroCostoPorAsesorAgrupadoAlternoDTO
                                 {
                                     idasesor = grupo.Key.idasesor,
                                     ingresoReal = grupo.Sum(w => w.ingresoReal),
                                     ingresoMes = grupo.Sum(w => w.ingresoMes),
                                     DescuentoPromedio = grupo.Sum(w => w.oportunidadesOCAnyIS) == 0 ? 0 : grupo.Sum(w => w.Descuento) / grupo.Sum(w => w.oportunidadesOCAnyIS),
                                     precioPromedio = grupo.Sum(w => w.precioListaFinal) / grupo.Sum(w => w.oportunidadesOCTotal),
                                     oportunidadesOCAnyIS = grupo.Sum(w => w.oportunidadesOCAnyIS),
                                     oportunidadesOCTotal = grupo.Sum(w => w.oportunidadesOCTotal),
                                     estadoAsesor = grupo.Max(w => w.estadoAsesor)
                                 }).ToList();


                var resultado = new { Records = agrupado, Records2 = agrupado2, Records3 = agrupado3, Records5 = centroCostoOportunidades };
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos Programa Especifico
        /// </summary>
        /// <returns> List<DatosProgramaEspecificoListaDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboPEspecifico()
        {
            try
            {
                PEspecificoService servicioPEspecifico = new PEspecificoService(unitOfWork);
                var resultado = unitOfWork.PEspecificoRepository.ObtenerCombo().ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para reporte de Tasas de Conversión Consolidada
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {

                    var service = new ReporteTasaConversionConsolidadaService(unitOfWork);
                    var idPersonal = _configuracionAccesoPersonalService.ObtenerIdPersonalAcceso(_respuestaCorrecta.RegistroClaimToken.IdPersonal, "Comercial/TasaConversionConsolidada");
                    return Ok(service.ObtenerCombos(idPersonal));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
                throw new UnauthorizedAccessException("Usted no tiene acceso");
            
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos Programa General
        /// </summary>
        /// <returns> Objeto: int,string,int </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboPGeneral()
        {
            try
            {
                PGeneralService servicioPGeneral = new PGeneralService(unitOfWork);
                var resultado = servicioPGeneral.ObtenerTodoGrid().Select(o => new { id = o.IdPgeneral, name = o.Nombre, subarea = o.IdSubArea }).ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2023
        /// Autor Modificacion: Flavio R. Mamani Fabian
        /// Fecha Modificacion: 24/02/2023
        /// Versión: 2.0
        /// <summary>
        /// Genera Reporte Tasas de Conversion Consolidada
        /// </summary>
        /// <returns> Objeto Agrupado </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteTasaConversionConsolidadaFiltroDTO filtros)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IReporteTasaConversionConsolidadaService reporte = new ReporteTasaConversionConsolidadaService(unitOfWork);
                return Ok(reporte.GenerarReporte(filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 07/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para reporte de Tasas de Conversión Consolidada
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteTasaConversionConsolidada(int idPersonal)
        {
            try
            {
                var servicioPersonal = new PersonalService(unitOfWork);
                var resultado = new ReporteTasaConversionConsolidadaGeneralDTO();

                resultado.Asesores = servicioPersonal.ObtenerAsesoresVentasOficial();
                resultado.Coordinadores = servicioPersonal.ObtenerCoordinadoresVentasOficial();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para Área
        /// </summary>
        /// <returns> Lista de Objetos: int,string </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboArea()
        {
            try
            {
                AreaCapacitacionService servicioAreaCapacitacion = new AreaCapacitacionService(unitOfWork);
                var resultado = servicioAreaCapacitacion.ObtenerCombo().Select(o => new { id = o.Id, name = o.Nombre }).ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
