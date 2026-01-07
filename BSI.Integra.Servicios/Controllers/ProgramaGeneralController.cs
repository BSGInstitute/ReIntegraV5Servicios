using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProgramaGeneralController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 09/06/2022
    /// <summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IPGeneralService _pGeneralService;
        private ITokenManager _tokenManager;
        public ProgramaGeneralController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _pGeneralService = new PGeneralService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PGeneral para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            return Ok(_pGeneralService.ObtenerCombo());
        }

        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PGeneral para combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]/{IdArea}")]
        public IActionResult ObtenerComboPorIdArea(int IdArea)
        {
            var servicio = new PGeneralService(_unitOfWork);
            return Ok(servicio.ObtenerComboPorIdArea(IdArea));
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_PGeneral
        /// </summary>
        /// <returns> List<PGeneralAlternoDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerProgramaGeneral()
        {
            try
            {
                return Ok(_pGeneralService.ObtenerPGeneral());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Guarda Problema
        /// </summary>
        /// <param name="Json">DTO de ProblemaVentas</param>
        /// <returns>bool</returns>
        [Route("GuardarProblemasVentasV2")]
        [HttpPost]
        public ActionResult GuardarProblemasVentasV2([FromBody] ProblemaVentasDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioProblema = new ProgramaGeneralProblemaService(_unitOfWork);
                var servicioModalidad = new ProgramaGeneralProblemaModalidadService(_unitOfWork);
                var servicioDetalleSolucion = new ProgramaGeneralProblemaDetalleSolucionService(_unitOfWork);

                List<ProgramaGeneralProblemaDetalleSolucion> argumentos;
                List<ProgramaGeneralProblemaModalidad> modalidadProblemas;
                bool flagBeficios = false;
                ProgramaGeneralProblema motivacion;
                ProgramaGeneralProblema resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (servicioProblema.ExistePoblemaPorId(Json.Problemas!.IdProblema))
                    {
                        motivacion = servicioProblema.ObtenerEntidadPorId(Json.Problemas.IdProblema);
                        motivacion.IdPgeneral = Json.Problemas.IdPGeneral;
                        motivacion.Nombre = Json.Problemas.NombreProblema;
                        motivacion.EsVisibleAgenda = Json.Problemas.EsVisibleAgenda;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaModificacion = DateTime.Now;
                        servicioModalidad.EliminacionLogicaPorProblema(Json.Problemas.IdProblema, Json.Usuario, Json.Problemas.Modalidades);
                    }
                    else
                    {
                        motivacion = new ProgramaGeneralProblema();
                        motivacion.IdPgeneral = Json.Problemas.IdPGeneral;
                        motivacion.Nombre = Json.Problemas.NombreProblema;
                        motivacion.EsVisibleAgenda = Json.Problemas.EsVisibleAgenda;
                        motivacion.UsuarioCreacion = Json.Usuario;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaCreacion = DateTime.Now;
                        motivacion.FechaModificacion = DateTime.Now;
                        motivacion.Estado = true;
                        flagBeficios = true;
                    }
                    argumentos = new List<ProgramaGeneralProblemaDetalleSolucion>();

                    modalidadProblemas = new List<ProgramaGeneralProblemaModalidad>();
                    foreach (var subItem in Json.Problemas.Modalidades)
                    {
                        ProgramaGeneralProblemaModalidad modalidad;
                        if (servicioDetalleSolucion.ExistePoblemaPorId(subItem.Id.Value))
                        {
                            modalidad = servicioModalidad.ObtenerEntidadPorId(subItem.Id.Value);
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Problemas.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralProblemaModalidad();
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Problemas.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioCreacion = Json.Usuario;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        modalidadProblemas.Add(modalidad);
                    }
                    motivacion.ProgramaGeneralProblemaDetalleSolucion = argumentos;
                    motivacion.ProgramaGeneralProblemaModalidad = modalidadProblemas;

                    if (flagBeficios)
                    {
                        resultado = servicioProblema.Add(motivacion);
                        motivacion.Id = resultado.Id;
                    }
                    else
                    {
                        resultado = servicioProblema.Update(motivacion);
                        motivacion.Id = resultado.Id;
                    }
                    scope.Complete();
                }

                return Ok(motivacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        /// Tipo Función: PUT
        /// Autor: Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Problema
        /// </summary>
        /// <param name="Json">DTO de ProblemaVentas</param>
        /// <returns>bool</returns>
        [Route("ActualizarProblemasVentasV2")]
        [HttpPut]
        public ActionResult ActualizarProblemasVentasV2([FromBody] ProblemaVentasDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioProblema = new ProgramaGeneralProblemaService(_unitOfWork);
                var servicioModalidad = new ProgramaGeneralProblemaModalidadService(_unitOfWork);
                var servicioDetalleSolucion = new ProgramaGeneralProblemaDetalleSolucionService(_unitOfWork);

                List<ProgramaGeneralProblemaDetalleSolucion> argumentos;
                List<ProgramaGeneralProblemaModalidad> modalidadProblemas;
                bool flagBeficios = false;
                ProgramaGeneralProblema motivacion;
                ProgramaGeneralProblema resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (servicioProblema.ExistePoblemaPorId(Json.Problemas!.IdProblema))
                    {
                        motivacion = servicioProblema.ObtenerEntidadPorId(Json.Problemas.IdProblema);
                        motivacion.IdPgeneral = Json.Problemas.IdPGeneral;
                        motivacion.Nombre = Json.Problemas.NombreProblema;
                        motivacion.EsVisibleAgenda = Json.Problemas.EsVisibleAgenda;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaModificacion = DateTime.Now;
                        servicioModalidad.EliminacionLogicaPorProblema(Json.Problemas.IdProblema, Json.Usuario, Json.Problemas.Modalidades);
                    }
                    else
                    {
                        motivacion = new ProgramaGeneralProblema();
                        motivacion.IdPgeneral = Json.Problemas.IdPGeneral;
                        motivacion.Nombre = Json.Problemas.NombreProblema;
                        motivacion.EsVisibleAgenda = Json.Problemas.EsVisibleAgenda;
                        motivacion.UsuarioCreacion = Json.Usuario;
                        motivacion.UsuarioModificacion = Json.Usuario;
                        motivacion.FechaCreacion = DateTime.Now;
                        motivacion.FechaModificacion = DateTime.Now;
                        motivacion.Estado = true;
                        flagBeficios = true;
                    }
                    argumentos = new List<ProgramaGeneralProblemaDetalleSolucion>();

                    modalidadProblemas = new List<ProgramaGeneralProblemaModalidad>();
                    foreach (var subItem in Json.Problemas.Modalidades)
                    {
                        ProgramaGeneralProblemaModalidad modalidad;
                        if (servicioDetalleSolucion.ExistePoblemaPorId(subItem.Id.Value))
                        {
                            modalidad = servicioModalidad.ObtenerEntidadPorId(subItem.Id.Value);
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Problemas.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            modalidad = new ProgramaGeneralProblemaModalidad();
                            modalidad.Nombre = subItem.Nombre;
                            modalidad.IdPgeneral = Json.Problemas.IdPGeneral;
                            modalidad.IdModalidadCurso = subItem.IdModalidad;
                            modalidad.UsuarioCreacion = Json.Usuario;
                            modalidad.UsuarioModificacion = Json.Usuario;
                            modalidad.FechaCreacion = DateTime.Now;
                            modalidad.FechaModificacion = DateTime.Now;
                            modalidad.Estado = true;
                        }
                        modalidadProblemas.Add(modalidad);
                    }
                    motivacion.ProgramaGeneralProblemaDetalleSolucion = argumentos;
                    motivacion.ProgramaGeneralProblemaModalidad = modalidadProblemas;
                    if (flagBeficios)
                    {
                        resultado = servicioProblema.Add(motivacion);
                        motivacion.Id = resultado.Id;
                    }
                    else
                    {
                        resultado = servicioProblema.Update(motivacion);
                        motivacion.Id = resultado.Id;
                    }
                    scope.Complete();
                }

                return Ok(motivacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de beneficios por programa general asociado
        /// </summary>
        /// <param name="IdBeneficio">Id de Beneficio</param>
        /// <param name="IdPGeneral">Id de Programa General</param>
        /// <returns>Objeto Agrupado, Bool de Estado y mensaje para interfaz</returns>
        [Route("[Action]/{idBeneficio}/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerBeneficioDetalleRequisito(int idBeneficio, int idPGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioConfiguracionBeneficioPGeneral = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);
                var registro = servicioConfiguracionBeneficioPGeneral.BeneficioDetalleRequisitoPorPGeneralYBeneficio(idBeneficio, idPGeneral);
                if (registro != null)
                {
                    return Ok(new { Respuesta = true, Datos = registro });
                }
                else
                {
                    return Ok(new { Respuesta = false });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //Combo para el modulo conjunto Anuncio 

        [HttpGet("[Action]")]
        public IActionResult ObtenerComboUrl()
        {
            var servicio = new PGeneralService(_unitOfWork);
            return Ok(servicio.ProgramaGneralconUrlVersion());
        }



        [HttpGet("[Action]")]
        public IActionResult ProgramaGneralconPEspecifico()
        {
            try
            {
                var servicio = new PGeneralService(_unitOfWork);
                return Ok(servicio.ProgramaGneralconPEspecifico());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo de estados y sub estados
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosEstadosSubEstados()
        {
            try
            {
                IEstadoMatriculaService servicioEstadoMatricula = new EstadoMatriculaService(_unitOfWork);
                IMatriculaCabeceraService servicioMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
                IPaisService servicioPais = new PaisService(_unitOfWork);
                IMonedaService servicioMoneda = new MonedaService(_unitOfWork);

                var detalles = new
                {
                    comboEstadoMatricula = servicioEstadoMatricula.ObtenerCombo(),
                    coomboSubEstadoMatricula = servicioMatriculaCabecera.ObtenerSubEstadoMatricula(),
                    comboPaises = servicioPais.ObtenerCombo(),
                    comboMoneda = servicioMoneda.ObtenerMonedaTodo()
                };
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        ///  Obtiene la lista de Programas  registradas en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerProgramasGeneral([FromBody] FiltroProgramaGeneralDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _pGeneralService.ListarProgramaGeneral(dto);
                if (_tokenManager.UserName == "AdminInst")
                {
                    resultado = resultado.Where(w => w.IdTipoProgramaCarrera == 2).ToList();
                }
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda el perfil del contacto
        /// </summary>
        /// <param name="dto"></param>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarPerfilContactoPrograma([FromBody] CompuestoPerfilContactoProgramaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_pGeneralService.GuardarPerfilContactoPrograma(dto, _tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda modelo predictivo 
        /// </summary>
        /// <param name="dto"></param>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public IActionResult GuardarModeloPredictivo([FromBody] CompuestoModeloPredictivoProgramaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_pGeneralService.GuardarModeloPredictivo(dto, _tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda un PreRequisito en Especifico - LPPG
        /// </summary>
        /// <param name="dto"></param>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarPreRequisitos([FromBody] CompuestoPreRequisitoModalidadAlternaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                return Ok(_pGeneralService.GuardarPreRequisitos(dto, _tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Guarda un PreRequisito en Especifico - LPPG
        /// </summary>
        /// <param name="dto"></param>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPut]
        public ActionResult AsociarProgramasAsociados([FromBody] ProgramaRelacionadoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                return Ok(_pGeneralService.AsociarProgramasAsociados(dto, _tokenManager.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener IdModalidad según el tipo de programa
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns></returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerIdModalidad(int idPGeneral)
        {
            return Ok(_pGeneralService.ListarModalidadesCurso(idPGeneral));
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para el moduloo de Programa General
        /// </summary>
        /// <returns> PGeneralComboModuloDTO </returns>
        [HttpGet]
        [Route("ObtenerCombosModuloAsync")]
        public async Task<ActionResult> ObtenerCombosModuloAsync()
        {
            try
            {
                var respuesta = await _pGeneralService.ObtenerCombosModuloAsync();
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para el moduloo de Programa General - Modal Monto Pagos
        /// </summary>
        /// <returns> PGeneralComboMontoPagoModuloDTO </returns> 
        [HttpGet]
        [Route("ObtenerCombosConfiguracionPlantillaAsync")]
        public async Task<ActionResult> ObtenerCombosConfiguracionPlantillaAsync()
        {
            var respuesta = await _pGeneralService.ObtenerCombosConfiguracionPlantillaAsync();
            return Ok(respuesta);
        }

        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 13/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos hijos de un programa General para el CRUD
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> PlantillaDocumentoDTO </returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerDocumentosAsociadosYNoAsociados(int idPGeneral)
        {
            try
            {
                var resultado = _pGeneralService.ObtenerDocumentosAsociadosYNoAsociados(idPGeneral);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <summary>
        /// Asocia documentos del PGeneral
        /// </summary>
        /// <param name="documentoAsociadoProgramaDTO"> Objeto que contiene los datos para su asociacion </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarDocumentosAsociados([FromBody] DocumentoAsociadoProgramaDTO documentoAsociadoProgramaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var resultado = _pGeneralService.ActualizarDocumentosAsociados(documentoAsociadoProgramaDTO, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene los cursos relacionados y No relaciados por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> Cursos relacionados y No relacionados </returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerRelacionCursos(int idPGeneral)
        {
            var resultado = _pGeneralService.ObtenerRelacionCursos(idPGeneral);
            return Ok(new { CursosRelacionados = resultado.Item1, CursosNoRelacionados = resultado.Item2 });
        }

        /// <summary>
        /// Obtiene información de configuracion de plantillas
        /// </summary>
        /// <param name="idProgramaGeneral">Id de Programa General</param>
        /// <returns>DetallesProgramasDTO</returns>
        [Route("[action]/{idProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerConfiguracionPlantillas(int idProgramaGeneral)
        {
            try
            {
                IPGeneralService servicioPGeneral = new PGeneralService(_unitOfWork);
                var registro = servicioPGeneral.ObtenerConfiguracionPlantilla(idProgramaGeneral);
                return Ok(registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        /// TipoFuncion: GET
        /// Autor:Joseph Llanque.
        /// Fecha: 26/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de configuracion de plantillas
        /// </summary>
        /// <param name="IdProgramaGeneral">Id de Programa General</param>
        /// <returns>DetallesProgramasDTO</returns>
        [Route("[action]/{IdProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerPgeneralConfiuracionBeneficios(int IdProgramaGeneral)
        {
            try
            {
                var servicioPGeneral = new PGeneralService(_unitOfWork);
                var registro = servicioPGeneral.ObtenerPgeneralConfiuracionBeneficios(IdProgramaGeneral);
                if (registro != null)
                {
                    return Ok(new { Respuesta = true, Datos = registro });
                }
                else
                {
                    return Ok(new { Respuesta = false });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// TipoFuncion: GET
        /// Autor:Joseph Llanque.
        /// Fecha: 26/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos configuracion Plantilla
        /// </summary>
        /// <param name="IdProgramaGeneral">Id de Programa General</param>
        /// <returns>DetallesProgramasDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosConfiguracionPlantilla(int IdProgramaGeneral)
        {
            try
            {
                var servicioPGeneral = new PGeneralService(_unitOfWork);
                var servicioPlantilla = new PlantillaService(_unitOfWork);
                var servicioModalidadCurso = new ModalidadCursoService(_unitOfWork);
                var servicioEstadoMatricula = new EstadoMatriculaService(_unitOfWork);
                var servicioMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
                var servicioOperadorComparacion = new OperadorComparacionService(_unitOfWork);
                var servicioPais = new PaisService(_unitOfWork);
                var detalles = new
                {
                    filtroPlantilla = servicioPlantilla.ObtenerListaPlantillaCertificadoOperaciones(),
                    filtroModalidadCurso = servicioModalidadCurso.ObtenerCombo(),
                    filtroEstadoMatricula = servicioEstadoMatricula.ObtenerEstadoMatricula(),
                    filtroOperadorComparacion = servicioOperadorComparacion.ObtenerCombo(),
                    filtroSubEstadoMatricula = servicioMatriculaCabecera.ObtenerSubEstadoMatricula(),
                    filtroPaises = servicioPais.ObtenerPais(),

                };

                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene los valores de Criterios Evaluacion Aonline
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> Cursos relacionados y No relacionados </returns> 
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerPGCriteriosEvaluacionAOnline(int idPGeneral)
        {
            var resultado = _pGeneralService.ObtenerPGcriteriosEvaluacionAOnline(idPGeneral);
            return Ok(resultado);
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene los valores de Criterios Evaluacion Aonline
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> Cursos relacionados y No relacionados </returns> 
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerPGCriteriosEvaluacionPresencial(int idPGeneral)
        {
            var resultado = _pGeneralService.ObtenerPGcriteriosEvaluacionPresencial(idPGeneral);
            return Ok(resultado);
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene los valores de Criterios Evaluacion Aonline
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> Cursos relacionados y No relacionados </returns> 
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerPGcriteriosEvaluacionOnline(int idPGeneral)
        {
            var resultado = _pGeneralService.ObtenerPGcriteriosEvaluacionOnline(idPGeneral);
            return Ok(resultado);
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 20/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información con detalles del programa por Id
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> DetallesProgramasDTO </returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerDetalleProgramas(int idPGeneral)
        {
            return Ok(_pGeneralService.ObtenerDetalleProgramas(idPGeneral));
        }
        /// TipoFuncion: GET
		/// Autor: Jonathan Caipo
		/// Fecha: 17/05/2023
		/// Version: 1.0
		/// <summary>
		/// Obtiene Programas Generales
		/// </summary>
		/// <returns> Retorma Lista de Programas Generales </returns>
		[Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerProgramasGenerales()
        {
            IPGeneralService pGeneralService = new PGeneralService(_unitOfWork);
            return Ok(pGeneralService.ObtenerProgramasFiltro());
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 12/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos para el modulo (C) Videos y evaluaciones en estructura del programa
        /// </summary>
        /// <returns> PGeneralComboModuloConfigurarVideoProgramaDTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerCombosConfigurarVideoPrograma()
        {
            var respuesta = _pGeneralService.ObtenerCombosConfigurarVideoPrograma();
            return Ok(respuesta);
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Qm
        /// Fecha: 12/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene perfil contacto por el IdPGeneral
        /// </summary>
        /// <returns>   </returns>
        [Route("[action]/{idPGeneral}")]
        [HttpGet]
        public IActionResult ObtenerPerfilContacto(int idPGeneral)
        {
            var respuesta = _pGeneralService.ObtenerPerfilContacto(idPGeneral);
            return Ok(respuesta);
        }
        /// TipoFuncion: PUT
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]/{idPrograma}/{estado}")]
        [HttpPut]
        public IActionResult ActualizarEstadoPrograma(int idPrograma, bool estado)
        {

            var respuesta = _pGeneralService.ActualizarEstadoPrograma(idPrograma, estado, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarBeneficiosVentas([FromBody] CompuestoBeneficioModalidadDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var respuesta = _pGeneralService.GuardarBeneficiosVentas(json, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]/{idProgramaGeneralBeneficio}")]
        [HttpDelete]
        public ActionResult EliminarBeneficioVenta(int idProgramaGeneralBeneficio)
        {


            var respuesta = _pGeneralService.EliminarBeneficioVenta(idProgramaGeneralBeneficio, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarCertificacionesVentas([FromBody] CompuestoCertificacionModalidadDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IProgramaGeneralCertificacionService service = new ProgramaGeneralCertificacionService(_unitOfWork);
            var resultado = service.GuardarCertificacionesVentas(json, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult ObtenerPPadreCEvaluacionPresencial(int idPgeneral)
        {
            var respuesta = _pGeneralService.ObtenerPPadreCEvaluacionPresencial(idPgeneral);
            return Ok(respuesta);
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult ObtenerPPadreCEvaluacionAonline(int idPgeneral)
        {
            var respuesta = _pGeneralService.ObtenerPPadreCEvaluacionAonline(idPgeneral);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarInsertarPGCEvaluacionHijo([FromBody] PgeneralCriterioEvaluacionHijoV2DTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _pGeneralService.ActualizarInsertarPGCEvaluacionHijo(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]/{idPgeneral}")]
        [HttpGet]
        public ActionResult ObtenerPPadreCEvaluacionOnline(int idPgeneral)
        {
            var respuesta = _pGeneralService.ObtenerPPadreCEvaluacionOnline(idPgeneral);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("[Action]")]
        [JwtExpirationValidation]
        [HttpPost]
        public ActionResult ActualizarInsertarPGCEvaluacion([FromBody] PGeneralCriterioEvaluacionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _pGeneralService.ActualizarInsertarPGCEvaluacion(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]/{idCriterioEvaluacion}")]
        [HttpDelete]
        public ActionResult EliminarCriterioEvaluacion(int idCriterioEvaluacion)
        {

            var respuesta = _pGeneralService.EliminarCriterioEvaluacion(idCriterioEvaluacion, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarModeloCertificado([FromForm] CompuestoModeloCertificadoModalidadDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _pGeneralService.GuardarModeloCertificado(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]/{IdProgramaGeneralModelo}")]
        [HttpGet]
        public ActionResult EliminarModeloCertificado(int IdProgramaGeneralModelo, string Usuario)
        {

            var respuesta = _pGeneralService.EliminarModeloCertificado(IdProgramaGeneralModelo, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("[action]/{idProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerModeloPredictivo(int idProgramaGeneral)
        {
            var respuesta = _pGeneralService.ObtenerModeloPredictivo(idProgramaGeneral);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{IdProgramaGeneralMotivacion}")]
        public ActionResult EliminarMotivacionVenta(int IdProgramaGeneralMotivacion)
        {
            var respuesta = _pGeneralService.EliminarMotivacionVenta(IdProgramaGeneralMotivacion, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarMotivacionesVentas([FromBody] CompuestoMotivacionModalidadDTO jsonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var respuesta = _pGeneralService.GuardarMotivacionesVentas(jsonDTO, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarProblemasVentas([FromBody] CompuestoProblemaModalidadDTO jsonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _pGeneralService.GuardarProblemasVentas(jsonDTO, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("[Action]/{idProgramaGeneral}/{idBeneficio}")]
        [HttpGet]
        public ActionResult ObtenerInformacionBeneficioRequisitpDetalle(int idProgramaGeneral, int idBeneficio)
        {
            var respuesta = _pGeneralService.ObtenerInformacionBeneficioRequisitpDetalle(idProgramaGeneral, idBeneficio);
            return Ok(respuesta);
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarBeneficiosPreRequisitos([FromBody] BeneficioPreRequisitoProgramaDTO jsonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _pGeneralService.GuardarBeneficiosPreRequisitos(jsonDTO, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("[action]/{idPlantillaF}/{idPlantillaP}/{idPrograma}")]
        [HttpGet]
        public IActionResult GenerarVistaPreviaCertificado(int idPlantillaF, int idPlantillaP, int idPrograma)
        {
            var respuesta = _pGeneralService.GenerarVistaPreviaCertificado(idPlantillaF, idPlantillaP, idPrograma);
            return Ok(respuesta);
        }
       
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("[action]/{idProgramaGeneral}")]
        [HttpGet]
        public IActionResult ObtenerBeneficiosYPreRequisitos(int idProgramaGeneral)
        {
            var respuesta = _pGeneralService.ObtenerBeneficiosYPreRequisitos(idProgramaGeneral);
            return Ok(respuesta);
        }
        /// <summary>
        /// </summary>
        /// <param name="programaGeneralDTO"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPut]
        public IActionResult Actualizar(ProgramaGeneralDTO programaGeneralDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var actualizar = _pGeneralService.Actualizar(programaGeneralDTO, _tokenManager.UserName);
            return Ok(actualizar);
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult ActualizarVersionPrograma([FromBody] UpdateOnlyVersionProgramaDTO jsonDTO)
        {
            var actualizar = _pGeneralService.ActualizarVersionPrograma(jsonDTO, "ctumir_sis");
            return Ok(actualizar);
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Qm.
        /// Fecha: 13/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Agrega registro nuevo de programa General
        /// </summary>
        /// <param name="programaGeneralDTO">Información Compuesta de Programa General</param>
        /// <returns> PGeneral </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ProgramaGeneralDTO programaGeneralDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _pGeneralService.Insertar(programaGeneralDTO, _tokenManager.UserName);
            return Ok(respuesta);
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza información de detalle beneficio 
        /// </summary>
        /// <param name="dto">Información codificada de Detalle de Requisitos</param>
        /// <returns>Objeto Agrupado, Bool de Estado y mensaje para interfaz</returns>
        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarInformacionBeneficioDetalleRequisito([FromBody] ConfiguracionBeneficioProgramaGeneralAlternoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _pGeneralService.ActualizarInformacionBeneficioDetalleRequisito(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
