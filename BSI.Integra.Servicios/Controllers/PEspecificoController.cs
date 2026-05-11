using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PEspecificoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión de PEspecifico
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PEspecificoController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IPEspecificoService _pEspecificoService;
        private ITokenManager _tokenManager;
        private IAsistenciaWebinarService _asistenciaWebinarService;
        public PEspecificoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _pEspecificoService = new PEspecificoService(unitOfWork);
            _asistenciaWebinarService = new AsistenciaWebinarService(unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/04/2023
        /// Versión: 2.0
        /// <summary>
        /// Obtiene Lista de Programas Especificos por Id Programa General
        /// </summary>
        /// <returns>Json</returns>
        [HttpGet("[Action]/{idPGeneral}")]
        public IActionResult ObtenerFiltroPorIdPGeneral(int idPGeneral)
        {
            try
            {
                return Ok(_pEspecificoService.ObtenerFiltroPorIdPGeneral(idPGeneral));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 23/01/2023
        /// Autor Modificacion: Flavio R. Mamani Fabian
        /// Fecha Modificacion: 27/04/2023
        /// Versión: 2.0
        /// <summary>
        /// Filtra programas especificos por nombre
        /// </summary>
        /// <returns>Json</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerPorNombreAutocomplete(StringDTO valor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_pEspecificoService.ObtenerPorNombreAutocomplete(valor.Valor));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lista de programas especificos padre mediante filtros
        /// </summary>
        /// <returns>Json</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerProgramaEspecificoPadreIndividual([FromBody] PEspecificoFiltroSPDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _unitOfWork.PEspecificoRepository.ObtenerProgramaEspecificoPadreIndividualFiltro(filtro);
            if (_tokenManager.UserName == "AdminInst")
            {
                resultado = resultado.Where(w => w.IdTipoProgramaCarrera == 2).ToList();
            }
            return Ok(resultado);
        }
        /// Tipo Función: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 03/05/2023
		/// Version: 1.0
		/// <summary>
		/// Generar un reporte con los nombre de lso programas especificos por el valor
		/// </summary>
		/// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
		[Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerProgramaEspecificoAutocomplete([FromBody] StringDTO filtro)
        {
            try
            {
                return Ok(_pEspecificoService.ObtenerPorNombreAutocomplete(filtro.Valor));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 18/05/2023
		/// Version: 1.0
		/// <summary>
		/// Obtiene los combos para el modulo de programas especificos
		/// </summary>
		/// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
		[Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosModulo()
        {
            return Ok(_pEspecificoService.ObtenerCombosModulo());
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos para el modulo de programas especificos
        /// </summary>
        /// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerProgramasEspecificosAdicional()
        {
            return Ok(_pEspecificoService.ObtenerProgramasEspecificosAdicional());
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 18/05/2023
		/// Version: 1.0
		/// <summary>
		/// Obtiene los combos para el modulo de programas especificos
		/// </summary>
		/// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
		[Route("ObtenerCombosModuloAsync")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCombosModuloAsync()
        {
            var resultado = await _pEspecificoService.ObtenerCombosModuloAsync();
            return Ok(resultado);
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 18/05/2023
		/// Version: 1.0
		/// <summary>
		/// 
		/// </summary>
		/// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
        [Route("[action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionWebinarPEspecifico(int idPespecifico)
        {
            var resultado = _pEspecificoService.ObtenerConfiguracionWebinarPEspecifico(idPespecifico);
            return Ok(resultado);
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 18/05/2023
		/// Version: 1.0
		/// <summary>
		/// 
		/// </summary>
		/// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
        [Authorize]
        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult<string> GenerarCronogramaGrupal(int idPespecifico)
        {
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            var resultado = _pEspecificoService.GenerarCronogramaGrupal(idPespecifico, registroClaimToken.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 29/05/2023
		/// Version: 1.0
		/// <summary>
        /// Actualiza el estado del programa especifico
		/// </summary>
		/// <returns> Estado e idPespecifico </returns>
        [Authorize]
        [Route("[Action]/{idPespecifico}/{idEstadoPrograma}")]
        [HttpPut]
        public ActionResult ActualizarEstadoPrograma(int idPespecifico, int idEstadoPrograma)
        {
            var resultado = _pEspecificoService.ActualizarEstadoPrograma(idPespecifico, idEstadoPrograma, _tokenManager.UserName);
            return Ok(new
            {
                resultado.Estado,
                resultado.IdProgramaEspecifico
            });
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 29/05/2023
		/// Version: 1.0
		/// <summary>
        /// Generar Centro Costo Codigo Nombre
		/// </summary>
		/// <returns>  </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarCentroCostoCodigoNombre([FromBody] PEspecificoGeneracionAutomaticaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_pEspecificoService.GenerarCentroCostoCodigoNombre(dto));
        }
        /// Tipo Función: PUT
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 29/05/2023
		/// Version: 1.0
		/// <summary>
        /// Actualiza registro Pespecifico
		/// </summary>
		/// <returns>  </returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarPespecifico([FromBody] PEspecificoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _pEspecificoService.ActualizarPespecifico(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza registro Pespecifico
        /// </summary>
        /// <returns>  </returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCrearCursosConCentroCosto([FromBody] FiltroInsertarPEspecificoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _pEspecificoService.InsertarCrearCursosConCentroCosto(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 29/05/2023
		/// Version: 1.0
		/// <summary>
        /// Verifica si existe frecuencia por id pespecifico
		/// </summary>
		/// <returns> estado </returns>
        [Route("[action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult<bool> VerificarFrecuenciaPorIdPespecifico(int idPespecifico)
        {
            return Ok(_unitOfWork.PespecificoFrecuenciaRepository.Exist(w => w.IdPespecifico == idPespecifico));
        }
        /// Tipo Función: POST
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 29/05/2023
		/// Version: 1.0
		/// <summary>
        /// Actualiza registro Pespecifico
		/// </summary>
		/// <returns>  </returns>
        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public IActionResult ObtenerTodoPespecificosRelacionados(int idPespecifico)
        {
            return Ok(_pEspecificoService.ObtenerTodoPespecificosRelacionados(idPespecifico));
        }
        /// Tipo Función: POST
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 29/05/2023
		/// Version: 1.0
		/// <summary>
        /// Actualiza inserta modulo webinar
		/// </summary>
		/// <returns>  </returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarInsertarModuloWebinar([FromBody] InsertarActualizarModuloWebinaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoService.ActualizarInsertarModuloWebinar(dto, registroClaimToken.UserName));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 30/05/2023
		/// Version: 1.0
		/// <summary>
        /// Actualiza la configuración Webinar
		/// </summary>
		/// <returns>  </returns>
        [Route("[action]")]
        [HttpPut]
        public IActionResult ActualizarConfigurarWebinar([FromBody] List<ConfigurarWebinarDTO> dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoService.ActualizarConfigurarWebinar(dto, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 30/05/2023
		/// Version: 1.0
		/// <summary>
        /// Elimina la configuración Webinar
		/// </summary>
		/// <returns>  </returns>
        [Route("[action]")]
        [HttpDelete]
        public ActionResult EliminarConfiguracionWebinar([FromBody] List<int> ids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoService.EliminarConfiguracionWebinar(ids, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 30/05/2023
		/// Fecha: 17/05/2023
		/// Version: 1.0
		/// <summary>
        /// Verifica si tiene padre pespecifico
		/// Funcion que nos trae el nombre y el id de los programas especificos segun el IdPgeneral
		/// </summary>
		/// <returns>  </returns>
        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult<bool> VerificarSiTienePadrePEspecifico(int idPespecifico)
        {
            var resultado = _pEspecificoService.VerificarSiTienePadrePEspecifico(idPespecifico);
            return Ok(new
            {
                resultado.Estado,
                resultado.Nombre
            });
        }
        /// Tipo Función: POST
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 30/05/2023
		/// Version: 1.0
		/// <summary>
        /// 
		/// </summary>
		/// <returns>  </returns>
        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult<bool> VerificarEsPespecificoIndividual(int idPespecifico)
        {
            return Ok(_pEspecificoService.VerificarEsPespecificoIndividual(idPespecifico));
        }
        /// Tipo Función: POST
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 30/05/2023
		/// Version: 1.0
		/// <summary>
        /// 
		/// </summary>
		/// <returns>  </returns>
        [Authorize]
        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult<string> ObtenerCronogramaParaModulo(int idPespecifico)
        {
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoService.ObtenerCronogramaParaModulo(idPespecifico, registroClaimToken.UserName));
        }
        /// Tipo Función: GET
		/// Autor: Jonathan Caipo
		/// Fecha: 30/05/2023
		/// Version: 1.0
		/// <summary>
        /// Otiene los grupos de sesión
		/// </summary>
		/// <returns>  </returns>
        [Route("[Action]/{pEspecificoId}/{cursoIndividual}")]
        [HttpGet]
        public ActionResult ObtenerNumeroGrupos(int pEspecificoId, bool cursoIndividual)
        {
            return Ok(_pEspecificoService.ObtenerNumeroGrupos(pEspecificoId, cursoIndividual));
        }
        /// Tipo Función: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 30/05/2023
		/// Version: 1.0
        /// <summary>
        /// Inserta Configuraciones Webinar
        /// </summary>
        /// <param name="dto"></param>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public ActionResult<ConfigurarWebinarDTO> InsertarConfiguracionWebinar([FromBody] ConfigurarWebinarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User!.Identity as ClaimsIdentity);
                return Ok(_pEspecificoService.InsertarConfiguracionWebinar(dto, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 30/05/2023
		/// Version: 1.0
        /// <summary>
        /// Inserta Configuraciones Webinar
        /// </summary>
        /// <param name="dto"></param>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult<IEnumerable<CronogramaGrupoDTO>> ObtenerCronogramaPEspecifico([FromBody] FiltroObtenerSesionesDTO dto)
        {
            return Ok(_pEspecificoService.ObtenerCronogramaPEspecifico(dto));
        }
        /// Tipo Función: GET
        /// Autor: aarroyoh
        /// Fecha: 07/05/2026
        /// Versión: 1.0
        /// <summary>
        /// Devuelve los IdTroncalPais aplicables al PE (Peru + pais del PE si difiere) para
        /// que el front encadene con /api/Feriado/ListarPorPaises sin lógica adicional.
        /// </summary>
        /// <param name="idPespecifico">Id del programa especifico</param>
        /// <returns>int[] con los IdTroncalPais</returns>
        [HttpGet("ObtenerIdsTroncalPaisFeriado/{idPespecifico}")]
        [Authorize]
        public ActionResult<int[]> ObtenerIdsTroncalPaisFeriado(int idPespecifico)
        {
            return Ok(_pEspecificoService.ObtenerIdsTroncalPaisFeriado(idPespecifico));
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 31/05/2023
		/// Version: 1.0
        /// <summary>
        /// Verifica si tiene duracion
        /// </summary>
        /// <param name="idPespecificoPadre">Id Pespecifico padre</param>
        [Route("[Action]/{idPespecificoPadre}")]
        [HttpGet]
        public ActionResult<bool> VerificarDuracionPorIdPespecificoPadre(int idPespecificoPadre)
        {
            return Ok(_pEspecificoService.VerificarDuracionPorIdPespecificoPadre(idPespecificoPadre));
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 31/05/2023
		/// Version: 1.0
        /// <summary>
        /// Verifica si tiene duracion
        /// </summary>
        /// <param name="idPespecificoPadre">Id Pespecifico padre</param>
        [Route("[Action]/{PEspecificoId}/{IdPespecificoHijo}")]
        [HttpGet]
        public ActionResult<bool> ClonarSesiones(int PEspecificoId, int IdPespecificoHijo)
        {
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User!.Identity as ClaimsIdentity);
            return Ok(_pEspecificoService.ClonarSesiones(PEspecificoId, registroClaimToken.UserName, IdPespecificoHijo));
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 31/05/2023
		/// Version: 1.0
        /// <summary>
        /// Verifica si tiene duracion
        /// </summary>
        /// <param name="idPespecificoPadre">Id Pespecifico padre</param>
        [Route("[Action]/{idPespecificoPadre}")]
        [Authorize]
        [HttpGet]
        public ActionResult<bool> ActualizarFechaPorSesion([FromBody] ActualizarFechaPorSesionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoService.ActualizarFechaPorSesion(dto, registroClaimToken.UserName));
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult GenerarReporteAmbienteExcel(string idProgramaEspecifico, string idCentroCosto, string codigoBs, string idEstadoPEspecifico, string idModalidadCurso, string idPGeneral, string idArea, string idSubArea, int? idCentroCostoD)
        {
            var filtro = new FiltroReporteAmbienteDTO()
            {
                IdProgramaEspecifico = idProgramaEspecifico == "null" ? "" : idProgramaEspecifico,
                IdCentroCosto = idCentroCosto == "null" ? "" : idCentroCosto,
                CodigoBS = codigoBs == "null" ? "" : codigoBs,
                IdEstadoPEspecifico = idEstadoPEspecifico == "null" ? "" : idEstadoPEspecifico,
                IdModalidadCurso = idModalidadCurso == "null" ? "" : idModalidadCurso,
                IdPGeneral = idPGeneral == "null" ? "" : idPGeneral,
                IdArea = idArea == "null" ? "" : idArea,
                IdSubArea = idSubArea == "null" ? "" : idSubArea,
            };
            var resultado = _pEspecificoService.GenerarReporteAmbienteExcel(filtro)!;
            return File(resultado, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte Programa Especifico.xlsx");
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 13/05/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera PDF Cronograma Alterno
        /// </summary>
        /// <returns>rpta<returns>
        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult<string> ObtenerCronogramaParaModuloAlterno(int idPespecifico)
        {
            string rpta = _pEspecificoService.ObtenerCronogramaParaModuloAlterno(idPespecifico);
            return Ok(rpta);
        }
        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult<bool> ValidarPespecificoTieneSesiones(int idPespecifico)
        {
            return Ok(_unitOfWork.PEspecificoSesionRepository.Exist(w => w.IdPespecifico == idPespecifico));
        }
        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult<bool> ObtenerDatosCompletosPespecificoPorId(int idPespecifico)
        {
            return Ok(_unitOfWork.PEspecificoRepository.ObtenerDatosCompletosPespecificoPorId(idPespecifico));
        }
        [Route("[Action]")]
        [Authorize]
        [HttpPut]
        public ActionResult<bool> ModificarFrecuencia([FromBody] ParametrosInsertaFrecuenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoService.ModificarFrecuencia(dto, registroClaimToken.UserName));
        }
        [Route("[Action]/{idPespecifico}/{numeroGrupo}")]
        [Authorize]
        [HttpDelete]
        public ActionResult EliminarCronogramaDuplicado(int idPespecifico, int numeroGrupo)
        {
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoService.EliminarCronogramaDuplicado(idPespecifico, numeroGrupo, registroClaimToken.UserName));
        }
        [Route("[Action]")]
        [Authorize]
        [HttpPost]
        public ActionResult InsertarEventoEspecial([FromBody] FiltroSesionEspecialDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoService.InsertarEventoEspecial(dto, registroClaimToken.UserName));
        }
        [Route("[Action]")]
        [Authorize]
        [HttpPost]
        public ActionResult ActualizarDuracionInsertarSesion([FromBody] InformacionPespecificoSesionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            var resultado = _pEspecificoService.ActualizarDuracionInsertarSesion(dto, registroClaimToken.UserName);
            if (resultado.IdTipoPrograma == 3) { // tipo 3 son programas webinar
                var nuevaFecha = resultado.FechaSesion.Date.AddMinutes(1);
                var jobId = BackgroundJob.Schedule(
                    () => _asistenciaWebinarService.ConfirmacionWebinarAutomatica(resultado.IdPEspecificoSesion),
                    nuevaFecha
                );
            }
            return Ok(true);
        }
        [Route("[Action]")]
        [Authorize]
        [HttpPost]
        public ActionResult InsertarSesionReprogramada([FromBody] ReprogramarSesionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            var resultado = _pEspecificoService.InsertarSesionReprogramada(dto, registroClaimToken.UserName);
            if (resultado.IdTipoPrograma == 3) { // tipo 3 son programas webinar
                var nuevaFecha = resultado.FechaSesion.Date.AddMinutes(1);
                var jobId = BackgroundJob.Schedule(
                    () => _asistenciaWebinarService.ConfirmacionWebinarAutomatica(resultado.IdPEspecificoSesion),
                    nuevaFecha
                );
            }
            return Ok(true);
        }
        [Route("[Action]")]
        [Authorize]
        [HttpPut]
        public ActionResult ActualizarDocenteAmbienteProgramaEspecifico([FromBody] DocenteAmbientePEspecificoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            var resultado = _pEspecificoService.ActualizarDocenteAmbienteProgramaEspecifico(dto, registroClaimToken.UserName);

            return Ok(new
            {
                resultado.EstadoCruce,
                resultado.Cruces,
                IdPespecifico = dto.Id
            });
        }
        /// <param name="idPgeneral">Ids del Programa General</param>
        /// <returns>Retorma una lista del tipo PEspecificoFiltroPGeneralDTO </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarPDFCronogramaModulo([FromBody] FiltroObtenerPDFDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoService.GenerarPDFCronogramaModulo(dto, registroClaimToken.UserName));
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera PDF Cronograma
        /// </summary>
        /// <returns>rpta<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarPDFCronogramaSemanal([FromBody] FiltroObtenerPDFDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoService.GenerarPDFCronogramaSemanal(dto, registroClaimToken.UserName));
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera PDF Cronograma
        /// </summary>
        /// <returns>rpta<returns>
        [Route("[Action]")]
        [Authorize]
        [HttpPost]
        public ActionResult InsertarFrecuencia([FromBody] ParametrosInsertaFrecuenciaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoService.InsertarFrecuencia(dto, registroClaimToken.UserName));
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/06/2023
        /// Versión: 1.0
        /// <summary>
        /// ObtenerCombosPEpecificoPorProgramaGeneral
        /// </summary>
        /// <returns>rpta<returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public IActionResult ObtenerCombosPEpecificoPorProgramaGeneral(List<int> idPGeneral)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(_pEspecificoService.ObtenerCombosPEpecificoPorProgramaGeneral(idPGeneral));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Edmundo LM
        /// Fecha: 24/27/2023
        /// Version: 1.0
        /// <summary>
        /// Ejecuta procedimiento Almacenado
        /// </summary>
        /// <returns>  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPorFiltro([FromBody] ProgramaEspecificoMaterialFiltroDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var res = _pEspecificoService.ObtenerPorFiltro(filtro);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("[Action]/{idPGeneral}")]
        public IActionResult ObtenerFiltroV2PorIdPGeneral(int idPGeneral)
        {
            try
            {
                return Ok(_pEspecificoService.ObtenerFiltroV2PorIdPGeneral(idPGeneral));
            }
            catch (Exception)
            {
                throw;
            }
        }

		[HttpGet]
		[Route("[Action]/{idPGeneral}")]
		public IActionResult ObtenerPEspecificoByPGeneral(int idPGeneral)
		{
			try
			{
				return Ok(_pEspecificoService.ObtenerPEspecificoByPGeneral(idPGeneral));
			}
			catch (Exception)
			{
				throw;
			}
		}
        /// Tipo Función: GET
        /// Autor: Generado automáticamente
        /// Fecha: 2026-03-19
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el catálogo completo de PEspecificos para carga masiva con filtrado local en frontend
        /// </summary>
        /// <returns>Lista de PEspecificoCatalogoDTO (Id, Nombre, Codigo)</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCatalogo()
        {
            try
            {
                return Ok(_pEspecificoService.ObtenerCatalogoPEspecifico());
            }
            catch (Exception)
            {
                throw;
            }
        }
	}
}
