using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AsignacionManualOportunidadOperacionesController
    /// Autor: Jonathan Caipo
    /// Fecha: 19/12/2022
    /// <summary>
    /// Gestión de AsignacionManualOportunidadOperaciones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AsignacionManualOportunidadOperacionesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public AsignacionManualOportunidadOperacionesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// TipoFuncion: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 19/12/2022
		/// Versión: 1.0
		/// <summary>
		/// Obtiene combos
		/// </summary>
		/// <returns> Objetos <returns>
		[Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombos(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                EstadoMatriculaService estadoMatriculaService = new EstadoMatriculaService(_unitOfWork);
                CentroCostoService centroCostoService = new CentroCostoService(_unitOfWork);
                AgendaTabService agendaTabService = new AgendaTabService(_unitOfWork);
                PersonalService personalService = new PersonalService(_unitOfWork);

                var personal = personalService.ObtenerPersonalAsignadoOperacionesTotalV2(idPersonal);
                var subestadomatricula = matriculaCabeceraService.ObtenerSubEstadoMatricula();
                var centroCosto = centroCostoService.ObtenerCombo();
                var estadomatricula = estadoMatriculaService.ObtenerCombo();
                var tabs = _unitOfWork.AgendaTabRepository.CombosTabsAtencionAlCliente();

                return Ok(new { Personal = personal, CentroCosto = centroCosto, EstadoMatricula = estadomatricula, Subestadomatricula = subestadomatricula, Tabs = tabs });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Oportunidades
        /// </summary>
        /// <returns> objeto <returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerOportunidades([FromBody] AsignacionManualOportunidadOperacionesFiltroGrillaDTO objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ResultadoFiltroAsignacionOportunidadDTO OportunidadManual = new ResultadoFiltroAsignacionOportunidadDTO();
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                PersonalService personalService = new PersonalService(_unitOfWork);

                if (objeto.Filtro.ListaPersonal.Count() == 0)
                {
                    var asistentesCargo = personalService.ObtenerPersonalAsignadoOperacionesTotalV2(objeto.Filtro.Personal);
                    List<int> ListaCoordinadortmp = new List<int>();
                    foreach (var item in asistentesCargo)
                    {
                        ListaCoordinadortmp.Add(item.Id);
                    }
                    objeto.Filtro.ListaPersonal = ListaCoordinadortmp;

                    var codigo = new List<string>();
                    if (objeto.Filtro.ListaCodigoMatricula != null) codigo = objeto.Filtro.ListaCodigoMatricula;
                    OportunidadManual = oportunidadService.ObtenerPorFiltroPaginaManualOperaciones(objeto.Paginador, objeto.Filtro, objeto.Filter, codigo);
                }
                else
                {
                    var codigo = new List<string>();
                    if (objeto.Filtro.ListaCodigoMatricula != null) codigo = objeto.Filtro.ListaCodigoMatricula;
                    OportunidadManual = oportunidadService.ObtenerPorFiltroPaginaManualOperaciones(objeto.Paginador, objeto.Filtro, objeto.Filter, codigo);
                }
                return Ok(OportunidadManual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// Asigna Oportunidad en su tab actual
        /// </summary>
        /// <returns><returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult AsignarOportunidadTabActual([FromBody] AsignarOportunidadOperacionesFiltroDTO objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AsignacionManualOportunidadOperacionesService asignacionManualOportunidadOperacionesService = new AsignacionManualOportunidadOperacionesService(_unitOfWork);
                var respuesta = asignacionManualOportunidadOperacionesService.AsignarOportunidadTabActual(objeto);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + (ex.InnerException != null ? (" - " + ex.InnerException.Message) : ""));
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 06/01/2023
        /// Version: 1.0
        /// <summary>
        /// Asigna Oportunidad Operaciones
        /// Modificacion: se agrego la modificacion de los gestores en el cronograma
        /// </summary>
        /// <returns><returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult AsignarOportunidadOperaciones([FromBody] AsignarOportunidadOperacionesFiltroDTO objeto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AsignacionManualOportunidadOperacionesService asignacionManualOportunidadOperacionesService = new AsignacionManualOportunidadOperacionesService(_unitOfWork);
                var respuesta = asignacionManualOportunidadOperacionesService.AsignarOportunidadOperaciones(objeto);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + (ex.InnerException != null ? (" - " + ex.InnerException.Message) : ""));
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 09/01/2023
        /// Version: 1.0
        /// <summary>
        /// Enlista los codigos de matricula del excel
        /// </summary>
        /// <returns> Objeto <returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerOportunidadesExcel([FromForm] IFormFile archivoExcel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaginadorDTO pag = new PaginadorDTO();
                pag.take = 10;
                pag.page = 1;
                pag.pageSize = 10;
                pag.skip = 0;
                AsignacionManualOportunidadOperacionesFiltroDTO asignacion = new AsignacionManualOportunidadOperacionesFiltroDTO();
                GridFiltersDTO grid = new GridFiltersDTO();

                List<string> codigos = new List<string>();
                using (var paquete = new ExcelPackage(archivoExcel.OpenReadStream()))
                {

                    var worksheet = paquete.Workbook.Worksheets[0];
                    var inicio = worksheet.Dimension.Start;
                    var final = worksheet.Dimension.End;

                    #region Inicializacion Valores
                    List<CampoObligatorioCeldaDTO> listaValoresExcel = new List<CampoObligatorioCeldaDTO>();
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "matriculas", Columna = 0, FlagObligatorio = true });
                    #endregion

                    object[,] valoresExcel = worksheet.Cells.GetValue<object[,]>();
                    for (int i = inicio.Row; i < final.Row; i++)
                    {
                        codigos.Add((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "matriculas").Columna] ?? string.Empty).ToString());
                    }
                }
                return Ok(codigos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
