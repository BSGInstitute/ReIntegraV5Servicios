using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Messenger;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Transactions;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: OportunidadController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/06/2022
    /// <summary>
    /// Gestión de Oportunidad
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class OportunidadController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private ITokenManager _tokenManager;
        private readonly IMessengerFacebookChatService _messengerFacebookChatService;

        public OportunidadController(IUnitOfWork unitOfWork, ITokenManager tokenManager, IMessengerFacebookChatService messengerFacebookChatService)
        {
            this.unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _messengerFacebookChatService = messengerFacebookChatService;
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 20/102022
        /// Versión: 1.0
        /// <summary>
        /// Se crea la oportunidad y el alumno en ventas
        /// </summary>
        /// <returns>ActionResult con estado 200 con objeto anonimo (Ok en cadena y el objeto de clase OportunidadBO)</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult CrearOportunidadCrearAlumnoVentas([FromBody] RegistroOportunidadAlumnoDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var oportunidadService = new OportunidadService(unitOfWork);
                var email1 = Regex.Replace(Formulario.Alumno.Email1, @"\s", "");
                var email2 = Regex.Replace(Formulario.Alumno.Email2, @"\s", "");

                Alumno alumno = new Alumno
                {
                    Nombre1 = Formulario.Alumno.Nombre1,
                    Nombre2 = Formulario.Alumno.Nombre2,
                    ApellidoPaterno = Formulario.Alumno.ApellidoPaterno,
                    ApellidoMaterno = Formulario.Alumno.ApellidoMaterno,
                    Direccion = Formulario.Alumno.Direccion,
                    Telefono = Formulario.Alumno.Telefono,
                    Celular = Formulario.Alumno.Celular,
                    Email1 = email1,
                    Email2 = email2,
                    IdCargo = Formulario.Alumno.IdCargo,
                    IdAformacion = Formulario.Alumno.IdAFormacion,
                    IdAtrabajo = Formulario.Alumno.IdATrabajo,
                    IdIndustria = Formulario.Alumno.IdIndustria,
                    IdReferido = Formulario.Alumno.IdReferido,
                    IdCodigoPais = Formulario.Alumno.IdCodigoPais,
                    IdCiudad = Formulario.Alumno.IdCodigoCiudad,
                    HoraContacto = Formulario.Alumno.HoraContacto,
                    HoraPeru = Formulario.Alumno.HoraPeru,
                    IdEmpresa = (Formulario.Alumno.IdEmpresa == 0 || Formulario.Alumno.IdEmpresa == -1) ? null : Formulario.Alumno.IdEmpresa,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = Formulario.Usuario,
                    UsuarioModificacion = Formulario.Usuario,
                    Comentario = Formulario.Alumno.Comentario
                };

                OportunidadBoDTO oportunidad = new OportunidadBoDTO
                {
                    IdCentroCosto = Formulario.Oportunidad.IdCentroCosto,
                    IdPersonalAsignado = Formulario.Oportunidad.IdPersonal_Asignado,
                    IdTipoDato = Formulario.Oportunidad.IdTipoDato,
                    IdFaseOportunidad = Formulario.Oportunidad.IdFaseOportunidad,
                    IdOrigen = Formulario.Oportunidad.IdOrigen,
                    UltimoComentario = Formulario.Oportunidad.UltimoComentario,
                    IdTipoInteraccion = Formulario.Oportunidad.fk_id_tipointeraccion,
                    FechaRegistroCampania = DateTime.Now,
                    Estado = true,
                    UsuarioCreacion = Formulario.Usuario,
                    UsuarioModificacion = Formulario.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Alumno = alumno
                };

                if (oportunidad.UltimaFechaProgramada != null)
                    oportunidad.IdEstadoOportunidad = 6;    // ValorEstatico.IdEstadoOportunidadProgramada;//Programada  6
                else
                    oportunidad.IdEstadoOportunidad = 2;    // ValorEstatico.IdEstadoOportunidadNoProgramada;//No programada 2
                oportunidadService.CrearOportunidadCrearPersona(ref oportunidad, false, TipoPersona.Alumno);

                try
                {
                    var nuevaProbabilidad = oportunidadService.ObtenerProbabilidadModeloPredictivo(oportunidad.Id);
                }
                catch (Exception e)
                {
                }

                try
                {
                    oportunidadService.MetodoODyOM(oportunidad.Id);
                }
                catch (Exception ex)
                {
                }

                // Mailing
                try
                {
                    oportunidadService.EnviarCorreoOportunidad(oportunidad.Id);
                }
                catch (Exception)
                {
                }

                // SMS
                try
                {
                    string uriSms = string.Empty;

                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                    {
                        if (DateTime.Now.Hour == 18)
                            uriSms = DateTime.Now.Minute < 30 ? $"https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1457}" : "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1457}";
                        else if (DateTime.Now.Hour > 18)
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                    }

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uriSms);
                    }
                }
                catch (Exception)
                {
                }

                if(!string.IsNullOrWhiteSpace(Formulario.Alumno.IdentificadorAmbitoPagina))
                    _messengerFacebookChatService.GuardarAlumnoOportunidadRegistro(Formulario.Alumno.IdentificadorAmbitoPagina, oportunidad.Id, Formulario.Usuario);

                return Ok(new { Rpta = "Ok", Records = oportunidad });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 03/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el alumno y crea la oportunidad de ventas
        /// </summary>
        /// <param name="formulario">Objeto de clase RegistroOportunidadAlumnoDTO</param>
        /// <returns>Response 200 con el objeto de clase OportunidadBO, caso contrario Response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarAlumnoCrearOportunidadVentas([FromBody] RegistroOportunidadAlumnoDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadService oportunidadService = new OportunidadService(unitOfWork);
                AlumnoService alumnoService = new AlumnoService(unitOfWork);

                var alumno = alumnoService.ObtenerPorId(Formulario.Alumno.Id);

                alumno.Nombre1 = Formulario.Alumno.Nombre1;
                alumno.Nombre2 = Formulario.Alumno.Nombre2;
                alumno.ApellidoPaterno = Formulario.Alumno.ApellidoPaterno;
                alumno.ApellidoMaterno = Formulario.Alumno.ApellidoMaterno;
                alumno.Direccion = Formulario.Alumno.Direccion;
                alumno.Telefono = Formulario.Alumno.Telefono;
                alumno.Celular = Formulario.Alumno.Celular;
                //alumno.Email1 = Formulario.Alumno.Email1;
                alumno.Email2 = Formulario.Alumno.Email2;
                alumno.IdCargo = Formulario.Alumno.IdCargo;
                alumno.IdAformacion = Formulario.Alumno.IdAFormacion;
                alumno.IdAtrabajo = Formulario.Alumno.IdATrabajo;
                alumno.IdIndustria = Formulario.Alumno.IdIndustria;
                alumno.IdReferido = Formulario.Alumno.IdReferido;
                alumno.IdCodigoPais = Formulario.Alumno.IdCodigoPais;
                alumno.IdCiudad = Formulario.Alumno.IdCodigoCiudad;
                alumno.HoraContacto = Formulario.Alumno.HoraContacto;
                alumno.HoraPeru = Formulario.Alumno.HoraPeru;
                var empresaAlumno = Formulario.Alumno.IdEmpresa;
                alumno.IdEmpresa = (empresaAlumno == 0 || empresaAlumno == -1) ? null : empresaAlumno;
                alumno.IdEmpresa = Formulario.Alumno.IdEmpresa;
                alumno.FechaModificacion = DateTime.Now;
                alumno.UsuarioModificacion = Formulario.Usuario;

                OportunidadBoDTO oportunidad = new OportunidadBoDTO();
                oportunidad.IdCentroCosto = Formulario.Oportunidad.IdCentroCosto;
                oportunidad.IdPersonalAsignado = Formulario.Oportunidad.IdPersonal_Asignado;
                oportunidad.IdTipoDato = Formulario.Oportunidad.IdTipoDato;
                oportunidad.IdFaseOportunidad = Formulario.Oportunidad.IdFaseOportunidad;
                oportunidad.IdOrigen = Formulario.Oportunidad.IdOrigen;
                oportunidad.UltimoComentario = Formulario.Oportunidad.UltimoComentario;
                oportunidad.IdTipoInteraccion = Formulario.Oportunidad.fk_id_tipointeraccion;
                oportunidad.Estado = true;
                oportunidad.FechaRegistroCampania = DateTime.Now;
                oportunidad.UsuarioCreacion = Formulario.Usuario;
                oportunidad.UsuarioModificacion = Formulario.Usuario;
                oportunidad.FechaCreacion = DateTime.Now;
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.Alumno = alumno;
                oportunidad.IdAlumno = alumno.Id;

                if (oportunidad.UltimaFechaProgramada != null)
                    oportunidad.IdEstadoOportunidad = 6;    // ValorEstatico.IdEstadoOportunidadProgramada;//Programada
                else
                    oportunidad.IdEstadoOportunidad = 2;    // ValorEstatico.IdEstadoOportunidadNoProgramada;//No programada

                oportunidadService.CrearOportunidadActualizarPersona(ref oportunidad, false, TipoPersona.Alumno);

                ///01/02/2021
                ///Calculo nuevo modelo predictivo
                ///Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = oportunidadService.ObtenerProbabilidadModeloPredictivo(oportunidad.Id);
                }
                catch (Exception e)
                {
                    //throw;
                }

                try
                {
                    oportunidadService.MetodoODyOM(oportunidad.Id);
                }
                catch (Exception ex)
                {
                    //solo si no funciona MetodoODyOM
                }

                try
                {
                    oportunidadService.EnviarCorreoOportunidad(oportunidad.Id);
                }
                catch (Exception ex)
                {
                }

                // SMS
                try
                {
                    string uriSms = string.Empty;

                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                    {
                        if (DateTime.Now.Hour == 18)
                            uriSms = DateTime.Now.Minute < 30 ? $"https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1456}" : "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else if (DateTime.Now.Hour > 18)
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                    }

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uriSms);
                    }
                }
                catch (Exception)
                {
                }

                if (!string.IsNullOrWhiteSpace(Formulario.Alumno.IdentificadorAmbitoPagina))
                    _messengerFacebookChatService.GuardarAlumnoOportunidadRegistro(Formulario.Alumno.IdentificadorAmbitoPagina, oportunidad.Id, Formulario.Usuario);

                return Ok(new { Rpta = "Ok", Records = oportunidad });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult CrearOportunidadesMasivo([FromBody] List<RegistroOportunidadAlumnoDTO> Formularios)
        {
            if (Formularios == null || Formularios.Count == 0)
            {
                return BadRequest("La lista de oportunidades está vacía.");
            }

            try
            {
                OportunidadService oportunidadService = new OportunidadService(unitOfWork);
                int oportunidadesProcesadas = 0;

                foreach (var formulario in Formularios) // Procesar cada oportunidad en secuencia
                {
                    bool resultado = oportunidadService.ActualizarAlumnoCrearOportunidadVentas(formulario);
                    if (resultado)
                    {
                        oportunidadesProcesadas++;
                    }
                }

                return Ok(new { mensaje = "Todas las oportunidades fueron procesadas en secuencia", totalRegistros = oportunidadesProcesadas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        /// TipoFuncion: GET
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 22/02/2022
        /// Versión: 1.0
        /// <summary>
        /// Procesar Oportunidades Portal Web
        /// </summary>
        /// <returns>bool true<returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ProcesarOportunidadesPortalWeb()
        {
            try
            {
                IOportunidadService oportunidadService = new OportunidadService(unitOfWork);
                return Ok(oportunidadService.ProcesarOportunidadesPortalWeb());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor:  Margiory Ramirez Neyra
        /// Fecha: 28/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Crear la oportunidad segun el IdAsignacionAutomatica enviado
        /// </summary>
        /// <returns>Response 200</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult CrearOportunidadesPortalWeb()
        {
            try
            {
                IOportunidadService servicio = new OportunidadService(unitOfWork);
                var idAsignacion = 0;
                return Ok(new { texto = servicio.CrearOportunidadesPortalWeb(idAsignacion) });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Margiory Ramrez
        /// Fecha: 28/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Valida las oportunidades del portal web
        /// </summary>
        /// <returns>Response 200, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ValidarOportunidadesPortalWeb()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OportunidadService(unitOfWork);
                return Ok(servicio.ValidarOportunidadesPortalWeb());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera la oportunidad de operaciones basandoe en la oportundiades padre de ventas
        /// </summary>
        /// <returns>OK-BADREQUEST</returns>
        [Route("[action]/{idOportunidad}/{nombreUsuario}/{idCentroCosto}/{idActividadCabecera}/{idAsesorAutomaticoOperaciones}/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult GenerarOportunidadOperaciones(int idOportunidad, string nombreUsuario, int idCentroCosto, int idActividadCabecera, int idAsesorAutomaticoOperaciones, int idMatriculaCabecera)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                OportunidadService oportunidadService = new OportunidadService(unitOfWork);
                var oportunidadVentas2 = oportunidadService.ObtenerPorId(idOportunidad);
                if (oportunidadVentas2.Id == null)
                {
                    return BadRequest("IdOportunidad no existe");
                }
                oportunidadService._oportunidadBo = oportunidadService.MapeoOportunidadBaseObjetDesdeEntidad(oportunidadVentas2);
                oportunidadService.GenerarOportunidadOperaciones();
                var idPadre = oportunidadService._oportunidadBo.Id;
                if (idAsesorAutomaticoOperaciones == 0)
                {
                    if (!oportunidadService.TienePersonalOperaciones(oportunidadService._oportunidadBo.Id))
                    {
                        return BadRequest("No se pudo calcular un personal operaciones");
                    }
                }

                var oportunidadOperaciones = oportunidadService._oportunidadBo;
                oportunidadOperaciones.Id = 0;
                if (idAsesorAutomaticoOperaciones != 0)
                {
                    oportunidadOperaciones.IdPersonalAsignado = idAsesorAutomaticoOperaciones;//_repOportunidad.ObtenerIdPersonalOperaciones(idPadre).Id;
                }
                else
                {
                    oportunidadOperaciones.IdPersonalAsignado = oportunidadService.ObtenerIdPersonalOperaciones(idPadre).Id;
                }
                oportunidadOperaciones.IdPersonalAreaTrabajo = 3;
                oportunidadOperaciones.IdCentroCosto = idCentroCosto;
                oportunidadOperaciones.UltimaFechaProgramada = null;
                oportunidadOperaciones.IdActividadCabeceraUltima = idActividadCabecera;
                oportunidadOperaciones.FechaCreacion = DateTime.Now;
                oportunidadOperaciones.UsuarioCreacion = nombreUsuario;
                oportunidadOperaciones.FechaModificacion = DateTime.Now;
                oportunidadOperaciones.UsuarioModificacion = nombreUsuario;
                oportunidadVentas2.IdMigracion = null;
                oportunidadVentas2.RowVersion = null;
                oportunidadOperaciones.Estado = true;
                oportunidadOperaciones.IdPadre = idPadre;
                oportunidadService.CrearOportunidadActualizarPersona(ref oportunidadOperaciones, false, TipoPersona.Alumno);

                ///03/02/2021
                ///Calculo nuevo modelo predictivo
                ///Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = oportunidadService.ObtenerProbabilidadModeloPredictivo(oportunidadOperaciones.Id);
                }
                catch (Exception e)
                {
                    //throw;
                }
                if (idMatriculaCabecera > 0)
                {
                    MoodleCronogramaEvaluacionService moodleCronogramaEvaluacionService = new MoodleCronogramaEvaluacionService(unitOfWork);
                    moodleCronogramaEvaluacionService.ObtenerCronogramaAutoEvaluacionUltimaVersion(idMatriculaCabecera);
                }
                return Ok(oportunidadOperaciones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 07/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Se corrige el registro erroneo 
        /// </summary>
        /// <returns>ActionResult con estado 200 con objeto anonimo (Ok en cadena)</returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public IActionResult CorregirRegistroErroneo([FromBody] AsignacionAutomaticaCompuestoDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    var IdUsuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                    IOportunidadErradoService oportunidadErradoService = new OportunidadErradoService(unitOfWork);
                    return Ok(oportunidadErradoService.CorregirDatoOportunidad(obj, IdUsuario));

                }
                else
                {
                    return Ok(_respuestaCorrecta);
                }
            }
        }
        /// Tipo Función: POST
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 07/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Se corrige el registro erroneo 
        /// </summary>
        /// <returns>ActionResult con estado 200 con objeto anonimo (Ok en cadena)</returns>
        [Authorize]
        [Route("[Action]")]
        [HttpPost]
        public IActionResult VerificarManualmenteDatos([FromBody] VerificacarManualmemnteDatosDTO obj)
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
                        var IdUsuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                        obj.Usuario = IdUsuario;
                        obj.UsuarioModificacion = IdUsuario;
                        var servicio = new AsignacionAutomaticaService(unitOfWork);
                        var servicioError = new AsignacionAutomaticaErrorService(unitOfWork);
                        var servicioOportunidad = new OportunidadService(unitOfWork);
                        var servicioCategoriaOrigen = new CategoriaOrigenService(unitOfWork);
                        var _repAlumno = new AlumnoService(unitOfWork);

                        AsignacionAutomatica AsignacionAutomaticaAntigua = new AsignacionAutomatica();

                        AsignacionAutomaticaAntigua.IdAlumno = obj.IdAlumno;
                        AsignacionAutomaticaAntigua.Nombre1 = obj.Nombre1;
                        AsignacionAutomaticaAntigua.Nombre2 = obj.Nombre2;
                        AsignacionAutomaticaAntigua.ApellidoPaterno = obj.ApellidoPaterno;
                        AsignacionAutomaticaAntigua.ApellidoMaterno = obj.ApellidoMaterno;
                        AsignacionAutomaticaAntigua.Celular = obj.Celular;
                        AsignacionAutomaticaAntigua.Email = obj.Email;
                        AsignacionAutomaticaAntigua.IdCentroCosto = obj.IdCentroCosto;
                        AsignacionAutomaticaAntigua.NombrePrograma = obj.NombrePrograma;
                        AsignacionAutomaticaAntigua.IdAreaFormacion = obj.IdAreaFormacion;
                        AsignacionAutomaticaAntigua.IdAreaTrabajo = obj.IdAreaTrabajo;
                        AsignacionAutomaticaAntigua.IdIndustria = obj.IdIndustria;
                        AsignacionAutomaticaAntigua.IdCargo = obj.IdCargo;
                        AsignacionAutomaticaAntigua.IdPais = obj.IdPais;
                        AsignacionAutomaticaAntigua.IdCiudad = obj.IdCiudad;
                        AsignacionAutomaticaAntigua.FechaModificacion = DateTime.Now;
                        AsignacionAutomaticaAntigua.UsuarioModificacion = obj.Usuario;
                        AsignacionAutomaticaAntigua.IdCategoriaDato = obj.IdCategoriaDato;
                        AsignacionAutomaticaAntigua.IdCategoriaOrigen = obj.IdCategoriaOrigen;

                        try
                        {
                            OportunidadBoDTO Oportunidad = new OportunidadBoDTO();
                            var hoy = DateTime.Now;
                            var cadena = hoy.DayOfWeek;
                            DateTime.Now.ToString("hh:mm:ss");
                            Dictionary<string, string> Dias = new Dictionary<string, string>() {
                                    { "Monday","Lunes"},
                                    { "Tuesday","Martes"},
                                    { "Wednesday","Miercoles"},
                                    { "Thursday","Jueves"},
                                    { "Friday","Viernes"},
                                    { "Saturday","Sabado"},
                                    { "Sunday","Domingo"}
                                };
                            var Horacio = hoy.TimeOfDay;
                            var dia = Dias[cadena.ToString()];
                            var diaDto = unitOfWork.BloqueHorarioRepository.ObtenerConfiguracion(dia);

                            int idTipoCategoriaOrigen = servicioCategoriaOrigen.ObtenerTipoCategoriaOrigenPorId(AsignacionAutomaticaAntigua.IdCategoriaOrigen == null ? 0 : AsignacionAutomaticaAntigua.IdCategoriaOrigen.Value);

                            if (AsignacionAutomaticaAntigua.IdAlumno != null && AsignacionAutomaticaAntigua.IdAlumno != 0)
                            {
                                var alumnoEntidad = unitOfWork.AlumnoRepository.FirstById(AsignacionAutomaticaAntigua.IdAlumno.Value);
                                var alumno = new Alumno
                                {
                                    Id = alumnoEntidad.Id,
                                    Nombre1 = alumnoEntidad.Nombre1,
                                    Nombre2 = alumnoEntidad.Nombre2,
                                    ApellidoPaterno = alumnoEntidad.ApellidoPaterno,
                                    ApellidoMaterno = alumnoEntidad.ApellidoMaterno,
                                    Direccion = alumnoEntidad.Direccion,
                                    Telefono = obj.Telefono,
                                    Celular = obj.Celular,
                                    Email1 = obj.Email,
                                    Email2 = obj.Email,
                                    IdCargo = obj.IdCargo,
                                    IdAformacion = obj.IdAreaFormacion,
                                    IdAtrabajo = obj.IdAreaTrabajo,
                                    IdIndustria = obj.IdIndustria,
                                    IdReferido = alumnoEntidad.IdReferido,
                                    IdCodigoPais = alumnoEntidad.IdCodigoPais,
                                    IdCiudad = alumnoEntidad.IdCodigoRegionCiudad,
                                    HoraContacto = alumnoEntidad.HoraContacto,
                                    HoraPeru = alumnoEntidad.HoraPeru,
                                    IdEmpresa = alumnoEntidad.IdEmpresa,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = obj.Usuario,
                                    UsuarioModificacion = obj.Usuario,
                                };
                                Oportunidad.Alumno = alumno;
                            }
                            else
                            {
                                var alumno = new Alumno
                                {
                                    Id = 0,
                                    Nombre1 = obj.Nombre1,
                                    Nombre2 = obj.Nombre2,
                                    ApellidoPaterno = obj.ApellidoPaterno,
                                    ApellidoMaterno = obj.ApellidoMaterno,
                                    Telefono = obj.Telefono,
                                    Celular = obj.Celular,
                                    Email1 = obj.Email,
                                    IdCargo = obj.IdCargo,
                                    IdAformacion = obj.IdAreaFormacion,
                                    IdAtrabajo = obj.IdAreaTrabajo,
                                    IdIndustria = obj.IdIndustria,
                                    IdCodigoPais = obj.IdPais,
                                    IdCiudad = obj.IdCiudad,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = obj.Usuario,
                                    UsuarioModificacion = obj.Usuario,
                                };
                                Oportunidad.Alumno = alumno;
                            }

                            //se agrego el flag-venta-cruzada; 1:proceza; 0:No proceza
                            Oportunidad.IdAlumno = obj.IdAlumno;
                            Oportunidad.IdOrigen = obj.IdOrigen;
                            Oportunidad.IdCentroCosto = obj.IdCentroCosto;
                            Oportunidad.IdPersonalAsignado = 125;
                            Oportunidad.IdTipoDato = obj.IdTipoDato.Value;
                            Oportunidad.IdFaseOportunidad = obj.IdFaseOportunidad.Value;
                            Oportunidad.FechaRegistroCampania = DateTime.Now;
                            Oportunidad.Estado = true;
                            Oportunidad.IdPersonalAsignado = 125;
                            Oportunidad.FechaCreacion = DateTime.Now;
                            Oportunidad.FechaModificacion = DateTime.Now;
                            Oportunidad.UsuarioCreacion = obj.Usuario;
                            Oportunidad.UsuarioModificacion = obj.Usuario;
                            if (Oportunidad.Alumno.Id == 0)
                            {
                                servicioOportunidad.CrearOportunidadCrearPersona(ref Oportunidad, false, TipoPersona.Alumno); //se agrego el flag-venta-cruzada
                            }
                            else
                            {
                                servicioOportunidad.CrearOportunidadActualizarPersona(ref Oportunidad, false, TipoPersona.Alumno); //se agrego el flag-venta-cruzada
                            }

                            ///15/03/2021
                            ///Calculo nuevo modelo predictivo
                            ///Carlos Crispin Riquelme
                            try
                            {
                                var nuevaProbabilidad = servicioOportunidad.ObtenerProbabilidadModeloPredictivo(Oportunidad.Id);
                            }
                            catch (Exception e)
                            {
                                //throw;
                            }

                            return Ok(Oportunidad.Id);

                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Equals("Se Actualizo contacto pero NO se creo la OPORTUNIDAD porque tiene Un BNC del tipo lanzamiento"))
                            {
                                AsignacionAutomaticaAntigua.Validado = true;
                                AsignacionAutomaticaAntigua.Corregido = false;
                                AsignacionAutomaticaAntigua.Estado = false;
                                AsignacionAutomaticaAntigua.FechaModificacion = DateTime.Now;
                                AsignacionAutomaticaAntigua.UsuarioModificacion = obj.Usuario;
                                servicio.Update(AsignacionAutomaticaAntigua);

                                try
                                {
                                    string URI = "https://integrav4-syncv3.bsginstitute.com/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + AsignacionAutomaticaAntigua.Id;
                                    using (WebClient wc = new WebClient())
                                    {
                                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        wc.DownloadString(URI);
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            throw ex;
                        }

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

        [Route("[Action]")]
        [HttpGet]
        public ActionResult CalcularProgramacionManualConsecutivos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new OportunidadService(unitOfWork);
                return Ok(servicio.CalcularProgramacionManualConsecutivos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerDatosOportunidad(int idOportunidad)
        {
            var servicio = new OportunidadService(unitOfWork);
            return Ok(servicio.ObtenerDatosOportunidad(idOportunidad));
        }

        /// Tipo Función: GET
        /// Autor:  Margiory Ramirez Neyra
        /// Fecha: 06/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Validacion del formulario
        /// </summary>
        /// <returns>Response 200</returns>
        [Route("[Action]/{IdAsignacionAutomaticaTemp}/{OrigenDato}")]
        [HttpGet]
        public ActionResult ValidarFormulario(int IdAsignacionAutomaticaTemp, int OrigenDato)
        {
            try
            {
                IOportunidadService servicio = new OportunidadService(unitOfWork);
                return Ok(new { texto = servicio.ValidarFormulario(IdAsignacionAutomaticaTemp, OrigenDato) });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

     

        /// Tipo Función: GET
        /// Autor:  Margiory Ramirez Neyra
        /// Fecha: 06/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Validacion del formulario
        /// </summary>
        /// <returns>Response 200</returns>
        [Route("[action]/{IdAsignacionAutomatica}")]
        [HttpGet]
        public ActionResult CrearOportunidadWebHookFacebook(int IdAsignacionAutomatica)
        {
            try
            {
                IOportunidadService servicio = new OportunidadService(unitOfWork);
                return Ok(new { texto = servicio.CrearOportunidadWebHookFacebook(IdAsignacionAutomatica) });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        ///// Tipo Función: POST
        ///// Autor:  Margiory Ramirez Neyra
        ///// Fecha:30/07/2024
        ///// Versión: 1.0
        ///// <summary>
        /////  Procesa la lista de IDs  Para Pendientes de Validacion
        ///// </summary>
        ///// <returns>Response 200</returns>

        //[Route("[action]")]
        //[HttpPost]
        //public ActionResult CrearOportunidadesWebHookFacebookLista([FromBody] List<int> idAsignacionAutomaticaList)
        //{
        //    try
        //    {
        //        IOportunidadService servicio = new OportunidadService(unitOfWork);


        //        if (idAsignacionAutomaticaList == null || !idAsignacionAutomaticaList.Any())
        //        {
        //            return BadRequest("La lista de IdAsignacionAutomatica no puede estar vacía.");
        //        }
        //        var resultados = servicio.CrearOportunidadesWebHookFacebookLista(idAsignacionAutomaticaList);
        //        return Ok(resultados);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { mensaje = ex.Message });
        //    }
        //}
        [Route("[action]")]
        [HttpPost]
        public ActionResult CrearOportunidadesWebHookFacebookLista([FromBody] List<int> idAsignacionAutomaticaList)
        {
            if (idAsignacionAutomaticaList == null || !idAsignacionAutomaticaList.Any())
            {
                return BadRequest("La lista de IdAsignacionAutomatica no puede estar vacía.");
            }

            Task.Run(() =>
            {
                using (var scope = HttpContext.RequestServices.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var servicio = new OportunidadService(unitOfWork);
                    servicio.CrearOportunidadesWebHookFacebookLista(idAsignacionAutomaticaList);
                }
            });

            return Ok(new { mensaje = "El procesamiento se está ejecutando en segundo plano." });
        }




        /// Tipo Función: POST
        /// Autor: Margiory
        /// Fecha: 07/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Registra en la tabla conf.T_Log
        /// </summary>
        /// <returns>Response 200, caso contrario Response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult RegistrarLogError([FromBody] RegistroLogDTO RegistroLog)
        {
            try
            {
                IOportunidadService servicio = new OportunidadService(unitOfWork);
                return Ok(new { texto = servicio.RegistrarLogError(RegistroLog) });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Procesa el formulario nuevo del portal
        /// </summary>
        /// <param name="IdRegistroPortalWeb">Cadena con el registro del portal web</param>
        /// <param name="IdPagina">Id de la pagina de donde proviene el dato</param>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>
        [Route("[Action]/{IdRegistroPortalWeb}/{IdPagina}")]
        [HttpGet]
        public ActionResult ProcesarFormularioNuevoPortal(string IdRegistroPortalWeb, int IdPagina)
        {
            try
            {
                IOportunidadService servicio = new OportunidadService(unitOfWork);
                return Ok(new { texto = servicio.ProcesarFormularioNuevoPortal(IdRegistroPortalWeb, IdPagina) });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Procesa el formulario nuevo del portal
        /// </summary>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarOportunidadesMkt(IFormFile file)
        {
            try
            {
                IOportunidadService servicio = new OportunidadService(unitOfWork);
                var resultado = servicio.ProcesarOportunidadesMkt(file, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [JwtExpirationValidation]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarOportunidadesMktLinkedIn(IFormFile file)
        {
            try
            {
                IOportunidadService servicio = new OportunidadService(unitOfWork);
                var resultado = servicio.ProcesarOportunidadesMktVersionLinkedIn(file, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Procesa el formulario nuevo del portal
        /// </summary>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarInformacionOportunidades([FromBody] List<InformacionBaseOportunidad> dto)
        {
            try
            {
                IOportunidadService servicio = new OportunidadService(unitOfWork);
                var resultado = servicio.ProcesarInformacionOportunidades(dto, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
