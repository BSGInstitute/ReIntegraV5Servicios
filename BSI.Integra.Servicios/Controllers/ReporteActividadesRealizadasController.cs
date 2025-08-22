using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteActividadesRealizadas
    /// Autor: Jonathan Caipo
    /// Fecha: 30/09/2022
    /// <summary>
    /// Gestión Reporte: Reporte de Actividades Realizadas
    /// </summary>
    [Route("api/ReporteActividadesRealizadas")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteActividadesRealizadasController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private IConfiguracionAccesoPersonalService _configuracionAccesoPersonalService;
        private IReporteActividadesRealizadasService _reporteActividadesRealizadasService;
        public ReporteActividadesRealizadasController(IUnitOfWork unitOfWork, IConfiguracionAccesoPersonalService configuracionAccesoPersonalService)
        {
            this.unitOfWork = unitOfWork;
            _configuracionAccesoPersonalService = configuracionAccesoPersonalService;
            _reporteActividadesRealizadasService = new ReporteActividadesRealizadasService(unitOfWork);
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 30/09/2022
        /// Autor Modificacion: Flavio R. Mamani Fabian
        /// Fecha Modificacion: 06/10/2023
        /// Versión: 2.0
        /// <summary>
        /// Obtiene información para combos de interfaz
        /// </summary>
        /// <returns> Combos para modulo comercial <returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public async Task<ActionResult<FiltroReporteActividadRealizadaAlternoDTO>> ObtenerCombo(int idPersonal)
        {
            IReporteSeguimientoOportunidadService servicio = new ReporteSeguimientoOportunidadService(unitOfWork);
            idPersonal = _configuracionAccesoPersonalService.ObtenerIdPersonalAcceso(idPersonal, "Comercial/ReporteActividadRealizada");
            var resultado = await _reporteActividadesRealizadasService.ObtenerCombo(idPersonal);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 30/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Actividades Realizadas
        /// </summary>
        /// <returns> Información de Actividades Realizadas: List<ProcesadoDataActividadesRealizadasDTO> <returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteActividadesRealizadasFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _reporteActividadesRealizadasService.ReporteActividadesRealizadas(filtro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte Actividades Realizadas Operaciones
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<ProcesadoDataActividadesRealizadasDTO> <returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteOperaciones([FromBody] ReporteActividadesRealizadasFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteService servicioReporte = new ReporteService(unitOfWork);
                //Retorna grabaciones en medio de HTML - Identico a V4
                var resultado = servicioReporte.ReporteActividadesRealizadasOperaciones(filtros);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 03/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información para combos de interfaz Operaciones y Asesores por IdPersonal
        /// </summary>
        /// <returns> objeto Agrupado <returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosOperaciones(int idPersonal)
        {
            try
            {
                PersonalService servicioPersonal = new PersonalService(unitOfWork);
                FaseOportunidadService servicioFaseOportunidad = new FaseOportunidadService(unitOfWork);
                TipoDatoService servicioTipoDato = new TipoDatoService(unitOfWork);
                ProbabilidadRegistroPwService servicioProbabilidadRegistro = new ProbabilidadRegistroPwService(unitOfWork);
                TipoCategoriaOrigenService servicioTipoCategoriaOrigen = new TipoCategoriaOrigenService(unitOfWork);
                EstadoOcurrenciaService servicioEstadoOcurrencia = new EstadoOcurrenciaService(unitOfWork);
                EstadoMatriculaService servicioEstadoMatricula = new EstadoMatriculaService(unitOfWork);
                EstadoMatriculaService servicioSubEstadoMatricula = new EstadoMatriculaService(unitOfWork);
                FiltroReporteActividadRealizadaDTO filtros = new FiltroReporteActividadRealizadaDTO();

                filtros.EstadoOcurrencia = servicioEstadoOcurrencia.ObtenerCombo();
                filtros.FaseOportunidad = servicioFaseOportunidad.ObtenerCombo();
                filtros.TipoDato = servicioTipoDato.ObtenerCombo();
                filtros.Probabilidad = servicioProbabilidadRegistro.ObtenerCombo();
                filtros.CategoriaOrigen = servicioTipoCategoriaOrigen.ObtenerCombo();
                filtros.EstadoMatricula = servicioEstadoMatricula.ObtenerTodoFiltroConfiguracionCoordinadora();
                filtros.SubEstadoMatricula = servicioSubEstadoMatricula.ObtenerComboOficialSubEstadoMatricula();
                List<PersonalAsignadoDTO> asistentes = servicioPersonal.PersonalAsignadoOperacionesTotal(idPersonal);
                //activos
                filtros.AsistentesActivos = asistentes.Where(w => w.Activo == true).ToList();
                //todos
                filtros.AsistentesTotales = asistentes;
                //inactivo
                filtros.AsistentesInactivos = asistentes.Where(w => w.Activo == false).ToList();
                return Ok(filtros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 03/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Valida un patrón de palabras
        /// </summary>
        /// <returns> Confirmación de Patrón: Bool <returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult WordPattern()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                bool flag = true;
                string pattern = "abba";
                string str = "dog cat cat dog";
                string[] arrStr = str.Split(" ");
                string temporalStr = "";
                string patternStr = "";
                if (pattern.Length == arrStr.Length)
                {
                    for (int i = 0; i < arrStr.Length; i++)
                    {
                        string letter = pattern.Substring(i, 1);
                        if (!dic.ContainsKey(letter))
                        {
                            dic.Add(letter, arrStr[i]);
                        }
                        temporalStr = arrStr[i];
                        patternStr = dic[letter];
                        if (temporalStr != patternStr)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                else
                {
                    flag = false;
                }
                return Ok(flag);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
