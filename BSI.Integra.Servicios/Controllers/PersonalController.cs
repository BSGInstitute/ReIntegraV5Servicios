
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: PersonalController
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 08/06/2022
    /// <summary>
    /// Gestión de Personal
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[Authorize]
    public class PersonalController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private IPersonalService _personalService;
        private ITokenManager _tokenManager;
        public PersonalController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this.unitOfWork = unitOfWork;
            _personalService = new PersonalService(unitOfWork);
            _tokenManager = tokenManager;
        }

        
        
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Personal para combo.
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            var servicio = new PersonalService(unitOfWork);
            return Ok(servicio.ObtenerCombo());
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Personal para combo.
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        [HttpGet("[Action]/{idPersonal}")]
        public IActionResult ObtenerPaisSedPersonal(int idPersonal)
        {
            var servicio = new PersonalService(unitOfWork);
            var resultado = servicio.ObtenerPaisSedePersonal(idPersonal);
            return Ok(new
            {
                IdPaisSede = resultado
            });
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 08/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Personal
        /// </summary>
        /// <returns> List<PersonalDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerPersonal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PersonalService(unitOfWork);
                return Ok(servicio.ObtenerPersonal());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Victor Hinojosa
        /// Fecha: 20/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el personal
        /// </summary>
        /// <returns> List<ObtenerTodoPersonal> </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerTodoPersonal()
        {
            PersonalService personalService = new PersonalService(unitOfWork);
            var gestionPersonal = personalService.ObtenerTodoPersonal();
            return Ok(gestionPersonal);
        }
        /// Autor: Victor Hinojosa
        /// Fecha: 07/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el personal activo
        /// </summary>
        /// <returns> List<ObtenerTodoPersonal> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerPersonalActivo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new PersonalService(unitOfWork);
                return Ok(servicio.ObtenerPersonalActivo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.  
        /// Fecha: 06/09/2022
        /// Versión: 1.0
        /// <summary>
        ///Obtiene los datos del personal segun el id enviado
        /// </summary>
        /// <returns>retorna un objeto tipo PersonalInformacionAgendaDTO</returns>
        [Route("[action]/{idPersonal}")]
        [HttpPost]
        public IActionResult ObtenerDatosPersonal(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioPersonal = new PersonalService(unitOfWork);
                var personalInformacionAgendaDTO = new PersonalInformacionAgendaDTO();
                personalInformacionAgendaDTO.DatosPersonal = servicioPersonal.ObtenerDatosPersonalAgenda(idPersonal);
                personalInformacionAgendaDTO.Asignados = servicioPersonal.ObtenerPersonalAsignado(idPersonal);
                return Ok(personalInformacionAgendaDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el personal asignado por idPersonal
        /// </summary>
        /// <returns>Lista PersonalAsignadoDTO</returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public IActionResult ObtenerPersonalAsignado(int idPersonal)
        {
            try
            {
                IPersonalService personalService = new PersonalService(unitOfWork);
                var resultado = personalService.ObtenerPersonalAsignado(idPersonal);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.  
        /// Fecha: 06/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del personal Para ventas
        /// </summary>
        /// <returns>retorna un objeto tipo List<ReportePersonalDTO> </returns>
        [HttpGet("ObtenerAsesoresVentasOficial")]
        public IActionResult ObtenerAsesoresVentasOficial()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioPersonal = new PersonalService(unitOfWork);
                return Ok(servicioPersonal.ObtenerAsesoresVentasOficial());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.  
        /// Fecha: 08/11/2022
        /// Versión: 1
        /// <summary>
        /// Obtiene la configuracion OpenVox por el Id del personal
        /// </summary>
        /// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <returns>Response 200 con lista de objetos de clase PersonalConfiguracionOpenVoxDTO o response 400 con mensaje de error</returns>
        [Route("[Action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerConfiguracionOpenVox(int idPersonal)
        {
            try
            {
                var personalService = new PersonalService(unitOfWork);
                var resultado = personalService.ObtenerConfiguracionOpenVoxPorIdPersonal(idPersonal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[Action]")]
        public IActionResult ObtenerPersonalPorMarketing()
        {
            var servicio = new PersonalService(unitOfWork);

            return Ok(servicio.ObtenerPersonalPorMarketing());
        }

        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Asesores y Coordinadores de Ventas
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerAsesorCoordinadorVentasCombo(int idPersonal)
        {
            try
            {
                IPersonalService servicioPersonal = new PersonalService(unitOfWork);
                return Ok(servicioPersonal.ObtenerAsesorCoordinadorVentasCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Griselberto
        /// Fecha: 03/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los nombres de personal para autocomplete
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("[action]/{valor}")]
        [HttpGet]
        public ActionResult ObtenerNombresFiltroAutoComplete(string valor)
        {
            try
            {
                IPersonalService servicioPersonal = new PersonalService(unitOfWork);
                return Ok(servicioPersonal.ObtenerNombresFiltroAutoComplete(valor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Griselberto
        /// Fecha: 03/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los nombres de personal para autocomplete
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("[action]/{valor}")]
        [HttpGet]
        public ActionResult ObtenerAsistenteAcademicoMatricula(string valor)
        {
            try
            {
                IPersonalService servicioPersonal = new PersonalService(unitOfWork);
                return Ok(servicioPersonal.ObtenerAsistenteAcademicoMatricula(valor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 13/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los asesores de operaciones que solicitaron tener agenda liberada 
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPersonalAgendaLiberadaOperaciones()
        {
            try
            {
                IPersonalService servicioPersonal = new PersonalService(unitOfWork);
                return Ok(servicioPersonal.ObtenerPersonalAgendaLiberadaOperaciones());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Griselberto
        /// Fecha: 03/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los nombres de personal para autocomplete
        /// </summary>
        /// <returns> int </returns>
        [Route("[action]/{valor}")]
        [HttpGet]
        public ActionResult ObtenerPersonalAutocomplete(string valor)
        {
            try
            {
                return Ok(unitOfWork.PersonalRepository.ObtenerPersonalAutocomplete(valor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 16/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las areas del personal activas
        /// </summary>
        /// <returns> Obtiene objeto de tipo List <ComboDTOAbrev></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPersonalAreaTrabajo()
        {
            try
            {
                IPersonalService servicioPersonal = new PersonalService(unitOfWork);
                return Ok(servicioPersonal.ObtenerPersonaAreaTrabajo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Junior Llerena
        /// Fecha: 16/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las areas del personal activas
        /// </summary>
        /// <returns> Obtiene objeto de tipo List <ComboDTOAbrev></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboZonaHorarioActivo()
        {
            try
            {
                IPaisService servicioPersonal = new PaisService(unitOfWork);
                return Ok(servicioPersonal.ObtenerComboZonaHorarioActivo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Max Mantilla
        /// Fecha: 03/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el IdPersonal por el nombre del usuario
        /// </summary>
        /// <returns> int </returns>
        [AllowAnonymous]
        [Route("[action]/{Username}")]
        [HttpGet]
        public ActionResult ObtenerPersonalUserName(string Username)
        {
            try
            {
                return Ok(unitOfWork.PersonalRepository.ObtenerIdPersonalPorUserName(Username));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        // Modulo de  Ficha Datos Personal
        #region
        // Modulo de  Ficha Datos Personal

        [HttpGet("[action]")]
        public IActionResult ObtenerFichaDatosPersonal()
        {
            var resultado = _personalService.ObtenerFichaDatosPersonal();
            return Ok(resultado);
        }
        [HttpGet("[action]")]
        public IActionResult ObtenerCombosFichaDatosPersonal()
        {
            var resultado = _personalService.ObtenerCombosFichaDatosPersonal();
            return Ok(resultado);
        }
        [HttpGet("[action]")]
        public IActionResult ObtenerPEspecificoPersonalAccesoTemporalCombo()
        {
            var resultado = _personalService.ObtenerPEspecificoPersonalAccesoTemporalCombo();
            return Ok(resultado);
        }


        [HttpGet("[action]/{idPersonal}")]
        public IActionResult ObtenerInformacionPersonal(int idPersonal)
        {
            var resultado = _personalService.ObtenerInformacionPersonal(idPersonal);
            return Ok(resultado);
        }


        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] MaestroPersonalCompuestoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _personalService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] MaestroPersonalCompuestoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _personalService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [Route("[Action]/{IdPersonal}")]
        [HttpPost]
        public ActionResult ObtenerDatosPersonalPorID(int IdPersonal)
        {
            try
            {
                IPersonalService servicioPersonal = new PersonalService(unitOfWork);
                return Ok(servicioPersonal.ObtenerDatosPersonalPorID(IdPersonal));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Eliot Arias
        /// Fecha: 26/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la vista V_ReporteInduccionPersonal
        /// </summary>
        /// <returns>List<InduccionPersonalDTO></InduccionPersonalDTO></returns>|
        [HttpGet]
        [Route("[action]")]
        public ActionResult ObtenerReporteInduccionPersonal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                return Ok(_personalService.ObtenerReportePersonal());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Eliot Arias F.
        /// Fecha: 26/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la vista V_ReporteInduccionPersonal filtrados mediante un objeto FiltroInduccionPersonalDTO
        /// </summary>
        /// <returns>List<InduccionPersonalDTO></returns>|
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteInduccionPersonalFiltro([FromBody] FiltroInduccionPersonalDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_personalService.ObtenerReportePersonalFiltro(Filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: GET
        /// Autor: Eliot Arias F.
        /// Fecha: 11/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene y junta los combos de las tablas de area, sede trabajo, procesos
        /// </summary>
        /// <returns>Objetos agrupados</returns>|
        [HttpGet]
        [Route("[action]")]
        public ActionResult ObtenerCombosInduccion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                return Ok(_personalService.ObtenerCombosInduccion());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// 
        /// Fecha: 23/03/2021
        /// Versión: 1
        /// <summary>
        /// Obtiene el horario por idPersonal
        /// </summary>
        /// <param name="Id">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <returns>Response 200 con la lista de objetos de clase Horario DTO o response 400 con el mensaje de error</returns>
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObteneHorarioPorId(int Id)
        {
            try
            {
                return Ok(unitOfWork.PersonalHorarioRepository.ObtenerHorario(Id));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{idPersonalArchivo}")]
        public IActionResult ObtenerArchivoPersonal(int idPersonalArchivo)
        {
            var resultado = _personalService.ObtenerArchivoPersonal(idPersonalArchivo);
            return Ok(resultado);
        }

        [HttpGet("[action]/{idPersonalArchivo}")]
        public IActionResult DescargarArchivoPersonal(int idPersonalArchivo)
        {
            var resultado = _personalService.DescargarArchivoPersonal(idPersonalArchivo);
            return Ok(resultado);
        }


        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _personalService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult ActualizarAccesoTemporal([FromBody] ActualizarAccesoTemporalDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dto.Usuario = _tokenManager.UserName;
            var respuesta = _personalService.ActualizarAccesoTemporal(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
        [HttpDelete("[action]")]
        public IActionResult EliminarAccesoTemporal(EliminarAccesoTemporalDTO AccesoTemporal)
        {
            var respuesta = _personalService.EliminarAccesoTemporal(AccesoTemporal, _tokenManager.UserName);
            return Ok(respuesta);
        }
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        [RequestSizeLimit(200000000)]
        public IActionResult AdjuntarArchivoPersonal([FromForm] ArchivoPersonalDTO ArchivoPersonal)
        {
            var resultado = _personalService.RegistrarArchivoFotoExpositor(ArchivoPersonal, _tokenManager.UserName);
            return Ok(resultado);
        }


        //Guardar Horario

        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult GuardarHorario([FromBody] HorarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dto.Usuario = _tokenManager.UserName;
            var respuesta = _personalService.GuardarHorario(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }


        #endregion


        //Reporte Personal Jefatura

        #region
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerReporteTodoPersonal([FromBody] FiltroPersonalJefaturaDTO filtro)
        {
            var resultado = _personalService.ObtenerReporteTodoPersonal(filtro);
            return Ok(resultado);
        }
        [HttpGet("[action]")]
        public IActionResult ObtenerCombosJefatura()
        {
            var resultado = _personalService.ObtenerCombosJefatura();
            return Ok(resultado);
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpGet("[action]")]
        public IActionResult ObtenergenerarReportePersonalActivo()
        {
            var resultado = _personalService.ObtenerPersonalEncargadoJefatura();
            return Ok(resultado);
        }





        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        [Consumes("application/json")]
        public ActionResult GetPersonalAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null && Filtros.ContainsKey("valor"))
                {
                    var resultado = _personalService.CargarPersonalAutoCompleteContrato(Filtros["valor"].ToString());
                    return Ok(resultado);
                }
                else
                {
                    return Ok(new List<FiltroDTO>());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion


        //victor hinojosa

        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 19/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Guardar el horario por personal
        /// </summary>
        /// <returns> Json </returns>
        /// Se comenta para revision posterior 
        //[AllowAnonymous]
        //[Route("[action]")]
        //[HttpPost]
        //public ActionResult GuardarHorario(PersonalHorarioDTO Json)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        var claimsIdentity = User.Identity as ClaimsIdentity;
        //        var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

        //        string usuarioIntegra = _respuestaCorrecta.RegistroClaimToken.UserName;

        //        var servicioHorario = new PersonalService(unitOfWork);
        //        var resultado = servicioHorario.GuardarHorario(Json, "usuarioIntegra");

        //        if (resultado)
        //        {
        //            return Ok(new { message = "Horario guardado con éxito." });
        //        }
        //        else
        //        {
        //            return BadRequest(new { message = "Error al guardar el horario." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = $"Error: {ex.Message}" });
        //    }
        //}
        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 19/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Inserta un personal nuevo
        /// </summary>
        /// <returns> Json </returns>
        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarPersonal(PersonalDetalleDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                string usuarioIntegra = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicioPersonal = new PersonalService(unitOfWork);
                var resultado = servicioPersonal.InsertarPersonal(Json, usuarioIntegra);

                if (resultado.Exito == false)
                {
                    return BadRequest(new { success = false, message = resultado.Mensaje });
                }

                return Ok(new { success = true, message = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 04/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza los datos de un personal
        /// </summary>
        /// <returns> Json </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarPersonal(PersonalDetalleDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                string usuarioIntegra = _respuestaCorrecta.RegistroClaimToken.UserName;

                var servicioPersonal = new PersonalService(unitOfWork);
                var resultado = servicioPersonal.ActualizarPersonal(Json, usuarioIntegra);

                if (resultado.Exito == false)
                {
                    return BadRequest(new { success = false, message = resultado.Mensaje });
                }

                return Ok(new { success = true, message = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

        }
        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 14/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza validaciones de Acceso
        /// </summary>
        /// <returns> Obtiene confirmación de envio: Bool </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult EnviarMensajeValidacionAcceso([FromBody] EnvioCorreoValidacionAccesoDTO json)
        {
            try
            {
                var servicioValidacion = new PersonalService(unitOfWork);
                var resultado = servicioValidacion.EnviarMensajeValidacionAcceso(json);

                if (resultado)
                {
                    return Ok(new { mensaje = "Clave de aplicación correcta." });
                }
                else
                {
                    return BadRequest(new { mensaje = "Clave de aplicación incorrecta." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = $"Error: {ex.Message}" });
            }
        }
        /// TipoFuncion: POST
        /// Autor: Victor Hinojosa
        /// Fecha: 11/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Resetea ips de un personal
        /// </summary>
        /// <param name="Json">Información Compuesta de Personal</param>
        /// <returns>MacDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ResetarIp([FromBody] MacDTO json)
        {
            try
            {
                var resetearIp = new PersonalService(unitOfWork);
                var resultado = resetearIp.ResetarIp(json);

                if (resultado)
                {
                    return Ok(new { mensaje = "IPs reseteadas correctamente." });
                }
                else
                {
                    return BadRequest(new { mensaje = "Hubo un problema al resetear las IPs." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = "Ocurrió un error.", detalle = ex.Message });
            }
        }
    }
}
