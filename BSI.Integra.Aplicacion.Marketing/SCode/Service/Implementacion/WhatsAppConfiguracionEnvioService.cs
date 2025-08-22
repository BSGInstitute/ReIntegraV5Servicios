using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WhatsAppMensajeEnviadoAutomaticoDTO = BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp.WhatsAppMensajeEnviadoAutomaticoDTO;
using WhatsAppResultadoConjuntoListaDTO = BSI.Integra.Aplicacion.DTO.Modelos.WhatsAppResultadoConjuntoListaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{

    public class WhatsAppConfiguracionEnvioService : IWhatsAppConfiguracionEnvioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        private AlumnoRepository _repAlumno;
        private PersonalRepository _repPersonal;
        private PlantillaClaveValorRepository _repPlantillaClaveValor;
        private CentroCostoRepository _repCentroCosto;
        private PGeneralRepository _repPgeneral;


        //private readonly PespecificoRepository _repPespecifico;
        private readonly OportunidadRepository _repOportunidad;
        private readonly OportunidadClasificacionOperacionesRepository _repOportunidadClasificacionOperaciones;
        //private readonly PespecificoRepository _repPEspecifico;
        //private readonly PespecificoSesionRepository _repPEspecificoSesion;
        private readonly PlantillaBaseRepository _repPlantillaBase;
        private readonly MatriculaCabeceraRepository _repMatriculaCabecera;
        private readonly DocumentoSeccionPwRepository _repDocumentoSeccionPw;
        //private readonly MaterialPespecificoDetalleRepository _repMaterialPEspecificoDetalle;
        private readonly PartnerPwRepository _repPartnerPw;
        private readonly SolicitudOperacionesRepository _repSolicitudOperaciones;
        private readonly SolicitudCertificadoFisicoRepository _repSolicitudCertificadoFisico;

        private readonly PostulanteRepository _repPostulante;
        private readonly ClasificacionPersonaRepository _repClasificacionP;
        private readonly WhatsAppConfiguracionEnvioDetalleRepository _repWhatsAppConfiguracionEnvioDetalle;
        private readonly PlantillaRepository _repPlantilla;


        public WhatsAppConfiguracionEnvioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppConfiguracionEnvio, WhatsAppConfiguracionEnvioDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public TWhatsAppConfiguracionEnvio Add(WhatsAppConfiguracionEnvioDTO entidad, string usuario)
        {
            entidad.UsuarioCreacion = usuario;
            entidad.UsuarioModificacion = usuario;
            entidad.FechaCreacion = DateTime.Now;
            entidad.FechaModificacion = DateTime.Now;
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.Add(entidad);
        }
        public TWhatsAppConfiguracionEnvio Update(WhatsAppConfiguracionEnvioDTO entidad, string usuario)
        {
            entidad.UsuarioModificacion = usuario;
            entidad.FechaModificacion = DateTime.Now;
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.Update(entidad);
        }
        public TWhatsAppConfiguracionEnvio Update(TWhatsAppConfiguracionEnvio entidad, string usuario)
        {
            var dat = _mapper.Map<WhatsAppConfiguracionEnvioDTO>(entidad);
            dat.Id = entidad.Id;
            dat.UsuarioModificacion = usuario;
            dat.FechaModificacion = DateTime.Now;
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.Update(dat);
        }
        public bool Delete(int id, string usuario)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.Delete(id, usuario);

        }
        public IEnumerable<TWhatsAppConfiguracionEnvio> Add(IEnumerable<WhatsAppConfiguracionEnvioDTO> listadoEntidad, string usuario)
        {
            listadoEntidad = listadoEntidad.Select(x => new WhatsAppConfiguracionEnvioDTO
            {
                FechaModificacion = DateTime.Now,
                Activo = x.Activo,
                Descripcion = x.Descripcion,
                Estado = x.Estado,
                FechaCreacion = DateTime.Now,
                FechaDesactivacion = x.FechaDesactivacion,
                Id = x.Id,
                IdCampaniaGeneralDetalle = x.IdCampaniaGeneralDetalle,
                IdConjuntoListaDetalle = x.IdConjuntoListaDetalle,
                IdPersonal = x.IdPersonal,
                IdPlantilla = x.IdPlantilla,
                Nombre = x.Nombre,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario
            }).ToList();

            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.Add(listadoEntidad);
        }
        public IEnumerable<TWhatsAppConfiguracionEnvio> Update(IEnumerable<WhatsAppConfiguracionEnvioDTO> listadoEntidad, string usuario)
        {
            listadoEntidad = listadoEntidad.Select(x => new WhatsAppConfiguracionEnvioDTO
            {
                FechaModificacion = DateTime.Now,
                Activo = x.Activo,
                Descripcion = x.Descripcion,
                Estado = x.Estado,
                FechaCreacion = x.FechaCreacion,
                FechaDesactivacion = x.FechaDesactivacion,
                Id = x.Id,
                IdCampaniaGeneralDetalle = x.IdCampaniaGeneralDetalle,
                IdConjuntoListaDetalle = x.IdConjuntoListaDetalle,
                IdPersonal = x.IdPersonal,
                IdPlantilla = x.IdPlantilla,
                Nombre = x.Nombre,
                UsuarioCreacion = x.UsuarioCreacion,
                UsuarioModificacion = usuario
            }).ToList();
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.Update(listadoEntidad);
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.Delete(listadoIds, usuario);
        }
        #endregion
        public List<ConjuntoListaDetalleWhatsAppDTO> ObtenerConfiguracionPorIdConjuntoLista(int idConjuntoLista)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.ObtenerConfiguracionPorIdConjuntoLista(idConjuntoLista);
        }
        public int EliminarEnviosProcesados(int idConjuntoLista)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.EliminarEnviosProcesados(idConjuntoLista);
        }
        public void EliminarWhatsAppConfiguracionMailingGeneral(int idCampaniaGeneralDetalle)
        {
            _unitOfWork.WhatsAppConfiguracionEnvioRepository.EliminarWhatsAppConfiguracionMailingGeneral(idCampaniaGeneralDetalle);
        }
        public bool ActualizarEstadoWhatsAppRecuperacion(string tipo, string usuarioResponsable, bool estadoHabilitado, int IdModuloSistemaWhatsAppMailing)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.ActualizarEstadoWhatsAppRecuperacion(tipo, usuarioResponsable, estadoHabilitado, IdModuloSistemaWhatsAppMailing);
        }
        public WhatsAppConfiguracionEnvioDTO InsertarWhatsAppConfiguracionGeneralMailing(int idCampaniaGeneralDetalle)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.InsertarWhatsAppConfiguracionGeneralMailing(idCampaniaGeneralDetalle);
        }

        public bool InsertarRegistroCaidaServidor(string servidor)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.InsertarRegistroCaidaServidor(servidor);
        }
        public List<ConjuntoListaDetalleWhatsAppDTO> ConsultaWhatsAppYConfiguracionEnvio(int IdConjuntoLista)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.ConsultaWhatsAppYConfiguracionEnvio(IdConjuntoLista);
        }
        public TWhatsAppConfiguracionEnvio FirstById(int id)
        {
            return _unitOfWork.WhatsAppConfiguracionEnvioRepository.FirstById(id);
        }

        public void RemplazarEtiquetas(List<WhatsAppResultadoConjuntoListaDTO> NumeroAlumno, int IdPersonal, int IdPlantilla, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaPrincipal, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaSecundario)
        {
            string plantilla = string.Empty;
            string valor = string.Empty;
            string Numero = "";
            //PlantillaPwBO plantillaPw = new PlantillaPwBO();

            foreach (var AlumnoEtiqueta in NumeroAlumno)
            {
                try
                {
                    AlumnoEtiqueta.objetoplantilla = new List<DatoPlantillaWhatsAppDTO>();

                    Numero = AlumnoEtiqueta.Celular;
                    if (Numero.StartsWith("51"))
                    {
                        Numero = Numero.Substring(2, 9);
                    }
                    else if (Numero.StartsWith("57"))
                    {
                        Numero = "00" + Numero;
                    }
                    else if (Numero.StartsWith("591"))
                    {
                        Numero = "00" + Numero;
                    }
                    else
                    {

                    }
                    var Alumno = _repAlumno.FirstBy(w => w.Id == AlumnoEtiqueta.IdAlumno, y => new { y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno });
                    //var Asesor = _repPersonal.FirstBy(w => w.Id == IdPersonal, y => new { y.Nombres, y.Apellidos, y.Anexo3Cx, y.Central, y.MovilReferencia });
                    var Asesor = _repPersonal.ObtenerDatoPersonal(IdPersonal);



                    plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

                    PlantillaCentroCostoDTO rpta = new PlantillaCentroCostoDTO();
                    ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
                    List<ModalidadProgramaDTO> fecha = new List<ModalidadProgramaDTO>();
                    foreach (var item in ProgramaPrincipal)
                    {
                        rpta = _repCentroCosto.ObtenerRemplazoPlantilla(item.IdPgeneral);
                        if (plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                        {
                            fecha = _repPgeneral.ObtenerFechaInicioProgramaGeneral(item.IdPgeneral, AlumnoEtiqueta.IdCodigoPais);

                            if (fecha.Count > 0)
                            {
                                FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                if (FechaInicioPrograma == null)
                                {
                                    FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                }
                            }
                        }
                        //plantillaPw.ObtenerFechaInicioPrograma(item.IdPgeneral, rpta.IdCentroCosto);
                    }


                    foreach (string word in plantilla.Split('{'))
                    {
                        DatoPlantillaWhatsAppDTO plantillaEtiqueValor = new DatoPlantillaWhatsAppDTO();
                        if (word.Contains('}'))
                        {
                            string etiqueta = word.Split('}')[0];
                            //Separamos solo los Id´s

                            if (etiqueta.Contains("tPartner.nombre"))
                            {

                                valor = rpta.NombrePartner;
                            }
                            else if (etiqueta.Contains("tPEspecifico.nombre"))
                            {
                                valor = rpta.NombrePEspecifico;
                            }
                            else if (etiqueta.Contains("tPLA_PGeneral.Nombre"))
                            {
                                valor = rpta.NombrePGeneral;
                            }

                            if (etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    CultureInfo ci = new CultureInfo("es-ES");
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                    valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            else if (etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = FechaInicioetiqueta.Day.ToString();
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            else if (etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    //CultureInfo ci = new CultureInfo("es-Es");
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                    valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            if (etiqueta.Contains("Template"))
                            {

                                valor = "";
                            }
                            else
                            {

                                if ((etiqueta == "tPersonal.email" || etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2") && IdPersonal > 0)
                                {
                                    switch (etiqueta)
                                    {
                                        case "tPersonal.PrimerNombreApellidoPaterno":
                                            valor = Asesor.PrimerNombreApellidoPaterno; break;
                                        case "tPersonal.email":
                                            valor = Asesor.Email; break;
                                        case "tPersonal.nombres":
                                            valor = Asesor.Nombres; break;
                                        case "tPersonal.apellidos":
                                            valor = Asesor.Apellidos; break;
                                        case "tPersonal.Telefono":
                                            {
                                                if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
                                                {
                                                    valor = Asesor.MovilReferencia;
                                                }
                                                else
                                                {
                                                    if (Asesor.Central == "192.168.0.20" || Asesor.Central == "192.168.2.20")
                                                    {
                                                        //Arequipa //lima
                                                        valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else if (Asesor.Central == "192.168.3.20")
                                                    {
                                                        //bogota
                                                        valor = "57 (601) 381 9462 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else if (Asesor.Central == "192.168.4.20")
                                                    {
                                                        //cd mexico
                                                        valor = "52 (55) 4000 3255 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else if (Asesor.Central == "192.168.5.20")
                                                    {
                                                        //santiago
                                                        valor = "56 (2) 2760 9120 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else
                                                    {
                                                        valor = "No registra central asignada";
                                                    }
                                                }
                                            }
                                            break;
                                        case "tAlumnos.apepaterno":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.ApellidoPaterno;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.apematerno":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.ApellidoMaterno;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.nombre1":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.Nombre1;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.nombre2":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.Nombre2;
                                                }
                                            }
                                            break;
                                        default:
                                            valor = ""; break;
                                    }

                                }
                            }
                            if (valor != null)
                            {
                                valor = valor.Replace("#$%", "<br>");
                                plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

                                plantillaEtiqueValor.Codigo = "{ " + etiqueta + "}";
                                plantillaEtiqueValor.Texto = valor;

                            }
                            else
                            {
                                plantilla = plantilla.Replace("{" + etiqueta + "}", "");

                                plantillaEtiqueValor.Codigo = "{ " + etiqueta + "}";
                                plantillaEtiqueValor.Texto = "";
                            }
                            AlumnoEtiqueta.objetoplantilla.Add(plantillaEtiqueValor);
                        }
                    }
                    AlumnoEtiqueta.Plantilla = plantilla;
                    //return Ok(new { plantilla, objetoplantilla });
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailService Mailservice = new TMK_MailService();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Error Proceso Plantillas";
                    mailData.Message = "Alumno: " + AlumnoEtiqueta.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + AlumnoEtiqueta.IdConjuntoListaResultado.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }


        }
        public bool ProcesarListasWhatsAppEnvioAutomaticoOperaciones(List<ConjuntoListaDetalleWhatsAppDTO> ListaConjuntoListaDetalleWhatsApp)
        {
            try
            {
                foreach (var WhatsAppConfiguracionEnvio in ListaConjuntoListaDetalleWhatsApp)
                {
                    WhatsAppConfiguracionLogEjecucion logEjecucion = new WhatsAppConfiguracionLogEjecucion();
                    try
                    {
                        logEjecucion.FechaInicio = DateTime.Now;
                        logEjecucion.FechaFin = null;
                        logEjecucion.IdWhatsAppConfiguracionEnvio = WhatsAppConfiguracionEnvio.Id ?? default(int);
                        logEjecucion.Estado = true;
                        logEjecucion.FechaCreacion = DateTime.Now;
                        logEjecucion.FechaModificacion = DateTime.Now;
                        logEjecucion.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        logEjecucion.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        // Mantener para reversion
                        //_repWhatsAppConfiguracionLogEjecucion.Insert(logEjecucion);

                        int idResultado = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.InsertarWhatsappConfiguracionLogEjecucion(logEjecucion);
                        logEjecucion.Id = idResultado;

                        var listaConjuntoListaResultado = _unitOfWork.ConjuntoListaResultadoRepository.ObtenerConjuntoListaResultadoWhatsAppMasivoOperaciones(WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);

                        ValidarNumeroConjuntoLista(listaConjuntoListaResultado, WhatsAppConfiguracionEnvio.Id ?? default(int));
                        listaConjuntoListaResultado = listaConjuntoListaResultado.Where(w => w.Validado == true).ToList();

                        RemplazarEtiquetas(ref listaConjuntoListaResultado, WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int));

                        //listaConjuntoListaResultado = listaConjuntoListaResultado.Where(w => w.Plantilla != null && w.Plantilla != "" && w.objetoplantilla.Count != 0).ToList();
                        listaConjuntoListaResultado = listaConjuntoListaResultado.Where(w => w.objetoplantilla.Count != 0).ToList();

                        EnvioAutomaticoPlantilla(listaConjuntoListaResultado, WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), logEjecucion.Id);

                        //var logEjecucionFinal = _repWhatsAppConfiguracionLogEjecucion.FirstById(logEjecucion.Id);
                        WhatsAppConfiguracionLogEjecucion logEjecucionFinal = new WhatsAppConfiguracionLogEjecucion();

                        logEjecucionFinal.Id = idResultado;
                        logEjecucionFinal.FechaFin = DateTime.Now;
                        //_repWhatsAppConfiguracionLogEjecucion.Update(logEjecucionFinal);

                        _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.ActualizarWhatsappConfiguracionLogEjecucionFechaFin(logEjecucionFinal);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            if (logEjecucion.Id == 0 || logEjecucion.Id == null)
                            {
                                logEjecucion.FechaFin = DateTime.Now;
                                //_repWhatsAppConfiguracionLogEjecucion.Insert(logEjecucion);

                                _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.InsertarWhatsappConfiguracionLogEjecucion(logEjecucion);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void EnvioAutomaticoPlantilla(List<WhatsAppResultadoConjuntoListaDTO> listaConjuntoListaResultado, int IdPlantilla, int IdWhatsAppConfiguracionLogEjecucion)
        {

            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            var Plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaPorId(IdPlantilla);
            foreach (var conjuntoListaResultado in listaConjuntoListaResultado)
            {
                WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = conjuntoListaResultado.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = conjuntoListaResultado.Plantilla,
                    datosPlantillaWhatsApp = conjuntoListaResultado.objetoplantilla
                };

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionService _repCredenciales = new WhatsAppConfiguracionService(_unitOfWork);
                    WhatsAppUsuarioCredencialService _repTokenUsuario = new WhatsAppUsuarioCredencialService(_unitOfWork);

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(conjuntoListaResultado.IdCodigoPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(conjuntoListaResultado.IdPersonal.Value, conjuntoListaResultado.IdCodigoPais);

                    string urlToPost = _credencialesHost.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    //TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(conjuntoListaResultado.IdPersonal.Value);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        //IRestResponse response = client.Execute(request);

                        //if (response.StatusCode == HttpStatusCode.OK)
                        //{
                        //    var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                        //    foreach (var item in datos.users)
                        //    {
                        //        TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                        //        modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                        //        modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                        //        modelCredencial.UserAuthToken = item.token;
                        //        modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                        //        modelCredencial.EsMigracion = true;
                        //        modelCredencial.Estado = true;
                        //        modelCredencial.FechaCreacion = DateTime.Now;
                        //        modelCredencial.FechaModificacion = DateTime.Now;
                        //        modelCredencial.UsuarioCreacion = "whatsapp";
                        //        modelCredencial.UsuarioModificacion = "whatsapp";

                        //        var rpta = _repTokenUsuario.Insert(modelCredencial);

                        //        _tokenComunicacion = item.token;
                        //    }

                        //    banderaLogin = true;

                        //}
                        //else
                        //{
                        //    banderaLogin = false;
                        //}
                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    if (banderaLogin)
                    {
                        switch (DTO.WaType.ToLower())
                        {
                            case "text":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                _waType = "text";

                                MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                _mensajeTexto.to = DTO.WaTo;
                                _mensajeTexto.type = DTO.WaType;
                                _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                _mensajeTexto.text = new text();

                                _mensajeTexto.text.body = DTO.WaBody;

                                using (WebClient client = new WebClient())
                                {
                                    //client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeTexto);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "hsm":

                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "template";

                                MensajePlantillaWhatsAppEnvioTemplate _mensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                                _mensajePlantilla.to = DTO.WaTo;
                                _mensajePlantilla.type = "template";
                                _mensajePlantilla.template = new template();

                                _mensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                _mensajePlantilla.template.name = DTO.WaBody;

                                _mensajePlantilla.template.language = new language();
                                _mensajePlantilla.template.language.policy = "deterministic";
                                _mensajePlantilla.template.language.code = "es";

                                _mensajePlantilla.template.components = new List<components>();
                                components Componente = new components();
                                Componente.type = "body";


                                if (DTO.datosPlantillaWhatsApp != null)
                                {
                                    Componente.parameters = new List<parameters>();
                                    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                    {
                                        parameters Dato = new parameters();
                                        Dato.type = "text";
                                        Dato.text = listaDatos.Texto;

                                        Componente.parameters.Add(Dato);
                                    }
                                }

                                _mensajePlantilla.template.components.Add(Componente);

                                using (WebClient Client = new WebClient())
                                {
                                    Client.Encoding = Encoding.UTF8;
                                    var MensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                    var Serializer = new JavaScriptSerializer();

                                    var SerializedResult = Serializer.Serialize(_mensajePlantilla);
                                    string MyParameters = SerializedResult;
                                    Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                    Client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = Client.UploadString(urlToPost, MyParameters);
                                }


                                //anterior

                                //urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                //_waType = "hsm";

                                //MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

                                //_mensajePlantilla.to = DTO.WaTo;
                                //_mensajePlantilla.type = DTO.WaType;
                                //_mensajePlantilla.hsm = new hsm();

                                //_mensajePlantilla.hsm.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                //_mensajePlantilla.hsm.element_name = DTO.WaBody;

                                //_mensajePlantilla.hsm.language = new language();
                                //_mensajePlantilla.hsm.language.policy = "deterministic";
                                //_mensajePlantilla.hsm.language.code = "es";

                                //if (DTO.datosPlantillaWhatsApp != null)
                                //{
                                //    _mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
                                //    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                //    {
                                //        localizable_params _dato = new localizable_params();
                                //        _dato.@default = listaDatos.texto;

                                //        _mensajePlantilla.hsm.localizable_params.Add(_dato);
                                //    }
                                //}

                                //using (WebClient client = new WebClient())
                                //{
                                //    client.Encoding = Encoding.UTF8;
                                //    var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                //    var serializer = new JavaScriptSerializer();

                                //    var serializedResult = serializer.Serialize(_mensajePlantilla);
                                //    string myParameters = serializedResult;
                                //    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                //    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                //    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                //    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                //    resultado = client.UploadString(urlToPost, myParameters);
                                //}

                                break;
                            case "image":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "image";

                                MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                _mensajeImagen.to = DTO.WaTo;
                                _mensajeImagen.type = DTO.WaType;
                                _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                _mensajeImagen.image = new image();

                                _mensajeImagen.image.caption = DTO.WaCaption;
                                _mensajeImagen.image.link = DTO.WaLink;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeImagen);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "document":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "document";

                                MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                _mensajeDocumento.to = DTO.WaTo;
                                _mensajeDocumento.type = DTO.WaType;
                                _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                _mensajeDocumento.document = new document();

                                _mensajeDocumento.document.caption = DTO.WaCaption;
                                _mensajeDocumento.document.link = DTO.WaLink;
                                _mensajeDocumento.document.filename = DTO.WaFileName;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeDocumento);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                        WhatsAppConfiguracionEnvioDetalle mensajeEnviado = new WhatsAppConfiguracionEnvioDetalle();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = true;
                        mensajeEnviado.MensajeError = "";
                        mensajeEnviado.IdConjuntoListaResultado = conjuntoListaResultado.IdConjuntoListaResultado;
                        mensajeEnviado.ConjuntoListaNroEjecucion = 0;
                        mensajeEnviado.Mensaje = DTO.WaCaption;
                        mensajeEnviado.WhatsAppId = datoRespuesta.messages[0].id;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        mensajeEnviado.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        _repWhatsAppConfiguracionEnvioDetalle.InsertarWhatsAppConfiguracionEnvioDetalle(mensajeEnviado);

                        // Mantener por respaldo
                        //_repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);

                        //return Ok(mensajeEnviado.WaId);
                    }
                    else
                    {
                        WhatsAppConfiguracionEnvioDetalle mensajeEnviado = new WhatsAppConfiguracionEnvioDetalle();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = false;
                        mensajeEnviado.MensajeError = "Error en credenciales de login o revise su conexion de red para el servidor de whatsapp.";
                        mensajeEnviado.IdConjuntoListaResultado = conjuntoListaResultado.IdConjuntoListaResultado;
                        mensajeEnviado.ConjuntoListaNroEjecucion = conjuntoListaResultado.NroEjecucion;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        mensajeEnviado.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        _repWhatsAppConfiguracionEnvioDetalle.InsertarWhatsAppConfiguracionEnvioDetalle(mensajeEnviado);

                        // Mantener por respaldo
                        //_repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }
                }
                catch (Exception ex)
                {
                    WhatsAppConfiguracionEnvioDetalle mensajeEnviado = new WhatsAppConfiguracionEnvioDetalle();

                    mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                    mensajeEnviado.EnviadoCorrectamente = false;
                    mensajeEnviado.MensajeError = ex.ToString();
                    mensajeEnviado.IdConjuntoListaResultado = conjuntoListaResultado.IdConjuntoListaResultado;
                    mensajeEnviado.ConjuntoListaNroEjecucion = conjuntoListaResultado.NroEjecucion;
                    mensajeEnviado.Estado = true;
                    mensajeEnviado.FechaCreacion = DateTime.Now;
                    mensajeEnviado.FechaModificacion = DateTime.Now;
                    mensajeEnviado.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                    mensajeEnviado.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                    _repWhatsAppConfiguracionEnvioDetalle.InsertarWhatsAppConfiguracionEnvioDetalle(mensajeEnviado);

                    // Mantener por respaldo
                    //_repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                }

                System.Threading.Thread.Sleep(5000);
            }

        }
        public void ValidarNumeroConjuntoLista(List<WhatsAppResultadoConjuntoListaDTO> listaConjuntoListaResultado, int IdWhatsAppConfiguracionEnvio)
        {
            string urlToPost;
            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            foreach (var ConjuntoListaResultado in listaConjuntoListaResultado)
            {
                WhatsAppMensajePublicidadDTO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadDTO();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
                DTO.contacts = new List<string>();
                DTO.blocking = "wait";
                DTO.contacts.Add("+" + ConjuntoListaResultado.Celular);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    var _credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(ConjuntoListaResultado.IdCodigoPais);
                    var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(ConjuntoListaResultado.IdPersonal.Value, ConjuntoListaResultado.IdCodigoPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.CredencialUsuarioLogin(ConjuntoListaResultado.IdPersonal.Value);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                modelCredencial.UserAuthToken = item.token;
                                modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                modelCredencial.EsMigracion = true;
                                modelCredencial.Estado = true;
                                modelCredencial.FechaCreacion = DateTime.Now;
                                modelCredencial.FechaModificacion = DateTime.Now;
                                modelCredencial.UsuarioCreacion = "whatsapp";
                                modelCredencial.UsuarioModificacion = "whatsapp";

                                modelCredencial.Id = _unitOfWork.WhatsAppUsuarioCredencialRepository.InsertarWhatsAppUsuarioCredencial(modelCredencial);
                                // Mantener por respaldo
                                //var rpta = _repTokenUsuario.Insert(modelCredencial);

                                _tokenComunicacion = item.token;
                            }

                            banderaLogin = true;

                        }
                        else
                        {
                            banderaLogin = false;
                        }

                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                        foreach (var item in datoRespuesta.contacts)
                        {
                            if (item.status == "invalid")
                            {
                                ConjuntoListaResultado.Validado = false;
                            }
                            else
                            {
                                ConjuntoListaResultado.Validado = true;
                            }
                        }
                        //Alumno.Validado = true;
                        whatsAppMensajePublicidad.IdAlumno = ConjuntoListaResultado.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = ConjuntoListaResultado.IdPersonal.Value;
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = ConjuntoListaResultado.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = ConjuntoListaResultado.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = ConjuntoListaResultado.Celular;
                        whatsAppMensajePublicidad.EsValido = ConjuntoListaResultado.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        whatsAppMensajePublicidad.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        // Mantener para reversion
                        //_repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                        whatsAppMensajePublicidad.Id = _unitOfWork.WhatsAppMensajePublicidadRepository.InsertarWhatsAppMensajePublicidad(whatsAppMensajePublicidad);

                    }
                    else
                    {
                        ConjuntoListaResultado.Validado = false;

                        whatsAppMensajePublicidad.IdAlumno = ConjuntoListaResultado.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = ConjuntoListaResultado.IdPersonal.Value;
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = ConjuntoListaResultado.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = ConjuntoListaResultado.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = ConjuntoListaResultado.Celular;
                        whatsAppMensajePublicidad.EsValido = ConjuntoListaResultado.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        whatsAppMensajePublicidad.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        // Mantener para reversion
                        //_repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                        whatsAppMensajePublicidad.Id = _unitOfWork.WhatsAppMensajePublicidadRepository.InsertarWhatsAppMensajePublicidad(whatsAppMensajePublicidad);

                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }

                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>
                    {
                        "jvillena@bsginstitute.com",
                        "fvaldez@bsginstitute.com",
                        "gmiranda@bsginstitute.com"
                    };

                    TMK_MailService Mailservice = new TMK_MailService();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Validacion Numero WhatsApp",
                        Message = "Alumno: " + ConjuntoListaResultado.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + ConjuntoListaResultado.IdConjuntoListaResultado.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }


        }
        public bool ReanudarEnvioAutomatico(int IdConjuntoLista)
        {
            try
            {
                WhatsAppConfiguracionEnvioService _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioService(_unitOfWork);
                WhatsAppConfiguracionEnvioPorProgramaService _repWhatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaService(_unitOfWork);

                var eliminadoCorrectamente = _repWhatsAppConfiguracionEnvio.EliminarEnviosProcesados(IdConjuntoLista);
                //var eliminadoCorrectamente = 1;

                if (eliminadoCorrectamente == 1)
                {
                    var ListasWhatsApp = _repWhatsAppConfiguracionEnvio.ObtenerConfiguracionPorIdConjuntoLista(IdConjuntoLista);
                    foreach (var item in ListasWhatsApp)
                    {
                        if (item.Id != null)
                        {
                            item.ProgramaPrincipal = _repWhatsAppConfiguracionEnvioPorPrograma.GetBy(Convert.ToInt32(item.Id)).ToList();
                            item.ProgramaSecundario = _repWhatsAppConfiguracionEnvioPorPrograma.GetBy(Convert.ToInt32(item.Id)).ToList();
                        }
                    }
                    foreach (var WhatsAppConfiguracionEnvio in ListasWhatsApp)
                    {
                        WhatsAppConfiguracionLogEjecucion logEjecion = new WhatsAppConfiguracionLogEjecucion();
                        try
                        {
                            logEjecion.FechaInicio = DateTime.Now;
                            logEjecion.IdWhatsAppConfiguracionEnvio = WhatsAppConfiguracionEnvio.Id ?? default(int);
                            logEjecion.Estado = true;
                            logEjecion.FechaCreacion = DateTime.Now;
                            logEjecion.FechaModificacion = DateTime.Now;
                            logEjecion.UsuarioCreacion = "ProcesoAutomatico";
                            logEjecion.UsuarioModificacion = "ProcesoAutomatico";
                            _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(logEjecion);



                            var Respuesta = _unitOfWork.ConjuntoListaResultadoRepository.ObtenerConjuntoListaResultado(WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);
                            ValidarNumeroConjuntoLista(Respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.Id ?? default(int));
                            Respuesta = Respuesta.Where(w => w.Validado == true).ToList();

                            RemplazarEtiquetas(Respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), WhatsAppConfiguracionEnvio.ProgramaPrincipal, WhatsAppConfiguracionEnvio.ProgramaSecundario);
                            Respuesta = Respuesta.Where(w => w.Plantilla != null && w.Plantilla != "" && w.objetoplantilla.Count != 0).ToList();

                            EnvioAutomaticoPlantilla(Respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), logEjecion.Id);

                            var logEjecucionFinal = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.FirstById(logEjecion.Id);
                            logEjecucionFinal.FechaFin = DateTime.Now;
                            _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Update(logEjecucionFinal);


                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                if (logEjecion.Id == 0 || logEjecion.Id == null)
                                {
                                    logEjecion.FechaFin = DateTime.Now;
                                    _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(logEjecion);
                                }
                            }
                            catch (Exception e)
                            {

                            }


                        }

                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void ValidarNumeroConjuntoLista(List<WhatsAppResultadoConjuntoListaDTO> NumerosValidados, int IdPersonal, int IdWhatsAppConfiguracionEnvio)
        {
            string urlToPost;
            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            foreach (var Alumno in NumerosValidados)
            {
                TWhatsAppMensajePublicidad whatsAppMensajePublicidad = new TWhatsAppMensajePublicidad();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
                DTO.contacts = new List<string>();
                DTO.blocking = "wait";
                DTO.contacts.Add("+" + Alumno.Celular);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    var _credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(Alumno.IdCodigoPais);
                    var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(Alumno.IdPersonal.Value, Alumno.IdCodigoPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.CredencialUsuarioLogin(IdPersonal);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                modelCredencial.UserAuthToken = item.token;
                                modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                modelCredencial.EsMigracion = true;
                                modelCredencial.Estado = true;
                                modelCredencial.FechaCreacion = DateTime.Now;
                                modelCredencial.FechaModificacion = DateTime.Now;
                                modelCredencial.UsuarioCreacion = "whatsapp";
                                modelCredencial.UsuarioModificacion = "whatsapp";

                                var rpta = _unitOfWork.WhatsAppUsuarioCredencialRepository.Insert(modelCredencial);

                                _tokenComunicacion = item.token;
                            }

                            banderaLogin = true;

                        }
                        else
                        {
                            banderaLogin = false;
                        }

                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);


                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                        foreach (var item in datoRespuesta.contacts)
                        {
                            if (item.status == "invalid")
                            {
                                Alumno.Validado = false;
                            }
                            else
                            {
                                Alumno.Validado = true;
                            }
                        }
                        //Alumno.Validado = true;
                        whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = IdPersonal;
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "ValidacionAutomatica";
                        whatsAppMensajePublicidad.UsuarioModificacion = "ValidacionAutomatica";
                        _unitOfWork.WhatsAppMensajePublicidadRepository.Insert(whatsAppMensajePublicidad);
                    }
                    else
                    {
                        Alumno.Validado = false;
                        whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = IdPersonal;
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "ValidacionAutomatica";
                        whatsAppMensajePublicidad.UsuarioModificacion = "ValidacionAutomatica";
                        _unitOfWork.WhatsAppMensajePublicidadRepository.Insert(whatsAppMensajePublicidad);
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }

                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailService Mailservice = new TMK_MailService();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Validacion Numero WhatsApp";
                    mailData.Message = "Alumno: " + Alumno.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + Alumno.IdConjuntoListaResultado.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }
        }


        private void EnvioAutomaticoPlantilla(List<WhatsAppResultadoConjuntoListaDTO> MensajeAlumno, int IdPersonal, int IdPlantilla, int IdWhatsAppConfiguracionLogEjecucion)
        {

            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            var Plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaPorId(IdPlantilla);
            foreach (var AlumnoMensaje in MensajeAlumno)
            {


                WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = AlumnoMensaje.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = AlumnoMensaje.Plantilla,
                    datosPlantillaWhatsApp = AlumnoMensaje.objetoplantilla
                };

                try
                {
                    var EnvioDuplicado = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.VerificadEnvioDuplicado(AlumnoMensaje.Celular);

                    if (EnvioDuplicado == 2)
                    {
                        ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };

                        WhatsAppConfiguracionService _repCredenciales = new WhatsAppConfiguracionService(_unitOfWork);
                        WhatsAppUsuarioCredencialService _repTokenUsuario = new WhatsAppUsuarioCredencialService(_unitOfWork);

                        var _credencialesHost = _repCredenciales.ObtenerCredencialHost(AlumnoMensaje.IdCodigoPais);
                        var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdPersonal, AlumnoMensaje.IdCodigoPais);

                        string urlToPost = _credencialesHost.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        //TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                        if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                        {
                            string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                            var userLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

                            var client = new RestClient(urlToPostUsuario);
                            var request = new RestSharp.RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("Content-Length", "");
                            request.AddHeader("Accept-Encoding", "gzip, deflate");
                            request.AddHeader("Host", _credencialesHost.IpHost);
                            request.AddHeader("Cache-Control", "no-cache");
                            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                            request.AddHeader("Content-Type", "application/json");

                        }
                        else
                        {
                            _tokenComunicacion = tokenValida.UserAuthToken;
                            banderaLogin = true;
                        }

                        if (banderaLogin)
                        {
                            switch (DTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                    _waType = "text";

                                    MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                    _mensajeTexto.to = DTO.WaTo;
                                    _mensajeTexto.type = DTO.WaType;
                                    _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                    _mensajeTexto.text = new text();

                                    _mensajeTexto.text.body = DTO.WaBody;

                                    using (WebClient client = new WebClient())
                                    {
                                        //client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeTexto);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "hsm":

                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "template";

                                    MensajePlantillaWhatsAppEnvioTemplate _mensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                                    _mensajePlantilla.to = DTO.WaTo;
                                    _mensajePlantilla.type = "template";
                                    _mensajePlantilla.template = new template();

                                    _mensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                    _mensajePlantilla.template.name = DTO.WaBody;

                                    _mensajePlantilla.template.language = new language();
                                    _mensajePlantilla.template.language.policy = "deterministic";
                                    _mensajePlantilla.template.language.code = "es";

                                    _mensajePlantilla.template.components = new List<components>();
                                    components Componente = new components();
                                    Componente.type = "body";


                                    if (DTO.datosPlantillaWhatsApp != null)
                                    {
                                        Componente.parameters = new List<parameters>();
                                        foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                        {
                                            parameters Dato = new parameters();
                                            Dato.type = "text";
                                            Dato.text = listaDatos.Texto;

                                            Componente.parameters.Add(Dato);
                                        }
                                    }

                                    _mensajePlantilla.template.components.Add(Componente);

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(_mensajePlantilla);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = Client.UploadString(urlToPost, MyParameters);
                                    }

                                    break;
                                case "image":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "image";

                                    MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                    _mensajeImagen.to = DTO.WaTo;
                                    _mensajeImagen.type = DTO.WaType;
                                    _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                    _mensajeImagen.image = new image();

                                    _mensajeImagen.image.caption = DTO.WaCaption;
                                    _mensajeImagen.image.link = DTO.WaLink;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeImagen);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "document":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "document";

                                    MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                    _mensajeDocumento.to = DTO.WaTo;
                                    _mensajeDocumento.type = DTO.WaType;
                                    _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                    _mensajeDocumento.document = new document();

                                    _mensajeDocumento.document.caption = DTO.WaCaption;
                                    _mensajeDocumento.document.link = DTO.WaLink;
                                    _mensajeDocumento.document.filename = DTO.WaFileName;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeDocumento);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                            }

                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                            TWhatsAppConfiguracionEnvioDetalle mensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle();

                            mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                            mensajeEnviado.EnviadoCorrectamente = true;
                            mensajeEnviado.MensajeError = "";
                            mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                            mensajeEnviado.Mensaje = DTO.WaCaption;
                            mensajeEnviado.WhatsAppId = datoRespuesta.messages[0].id;
                            mensajeEnviado.Estado = true;
                            mensajeEnviado.FechaCreacion = DateTime.Now;
                            mensajeEnviado.FechaModificacion = DateTime.Now;
                            mensajeEnviado.UsuarioCreacion = "Envio";
                            mensajeEnviado.UsuarioModificacion = "Envio";

                            _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(mensajeEnviado);

                            //return Ok(mensajeEnviado.WaId);
                        }
                        else
                        {
                            TWhatsAppConfiguracionEnvioDetalle mensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle();

                            mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                            mensajeEnviado.EnviadoCorrectamente = false;
                            mensajeEnviado.MensajeError = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.";
                            mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                            mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                            mensajeEnviado.Estado = true;
                            mensajeEnviado.FechaCreacion = DateTime.Now;
                            mensajeEnviado.FechaModificacion = DateTime.Now;
                            mensajeEnviado.UsuarioCreacion = "Envio";
                            mensajeEnviado.UsuarioModificacion = "Envio";
                            _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(mensajeEnviado);
                            //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                        }
                    }
                    else
                    {
                        TWhatsAppConfiguracionEnvioDetalle mensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = false;
                        mensajeEnviado.MensajeError = "Envio Duplicado";
                        mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                        mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "Envio";
                        mensajeEnviado.UsuarioModificacion = "Envio";
                        _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(mensajeEnviado);
                    }
                }
                catch (Exception ex)
                {
                    TWhatsAppConfiguracionEnvioDetalle mensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle();

                    mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                    mensajeEnviado.EnviadoCorrectamente = false;
                    mensajeEnviado.MensajeError = ex.ToString();
                    mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                    mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                    mensajeEnviado.Estado = true;
                    mensajeEnviado.FechaCreacion = DateTime.Now;
                    mensajeEnviado.FechaModificacion = DateTime.Now;
                    mensajeEnviado.UsuarioCreacion = "Envio";
                    mensajeEnviado.UsuarioModificacion = "Envio";
                    _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(mensajeEnviado);
                }

                System.Threading.Thread.Sleep(5000);


            }

        }
        private void RemplazarEtiquetas(ref List<WhatsAppResultadoConjuntoListaDTO> listaConjuntoListaResultado, int IdPlantilla)
        {

            foreach (var itemConjuntoListaResultado in listaConjuntoListaResultado)
            {
                try
                {
                    itemConjuntoListaResultado.objetoplantilla = new List<DatoPlantillaWhatsAppDTO>();


                    // Mantener por respaldo
                    //if (!_repConjuntoListaResultado.Exist(itemConjuntoListaResultado.IdConjuntoListaResultado))
                    if (!_unitOfWork.ConjuntoListaResultadoRepository.ExisteConjuntoListaResultado(itemConjuntoListaResultado.IdConjuntoListaResultado))
                    {
                        continue;
                    }

                    // Mantener por respaldo
                    //var conjuntoListaResultado = _repConjuntoListaResultado.FirstById(itemConjuntoListaResultado.IdConjuntoListaResultado);
                    var conjuntoListaResultado = _unitOfWork.ConjuntoListaResultadoRepository.BuscaConjuntoListaResultado(itemConjuntoListaResultado.IdConjuntoListaResultado);

                    if (!_unitOfWork.OportunidadRepository.Exist(conjuntoListaResultado.IdOportunidad.Value))
                    {
                        continue;
                    }
                    var oportunidad = _unitOfWork.OportunidadRepository.FirstById(conjuntoListaResultado.IdOportunidad.Value);

                    var resultadoReemplazo = new ReemplazoEtiquetaPlantillaService(_unitOfWork).ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO()
                    {
                        IdOportunidad = conjuntoListaResultado.IdOportunidad.Value,
                        IdPlantilla = IdPlantilla
                    });
                    itemConjuntoListaResultado.objetoplantilla = resultadoReemplazo.WhatsAppReemplazado.ListaEtiquetas;
                    itemConjuntoListaResultado.IdPersonal = oportunidad.IdPersonalAsignado;
                    itemConjuntoListaResultado.Plantilla = resultadoReemplazo.WhatsAppReemplazado.Plantilla;
                }
                catch (Exception e)
                {
                    List<string> correos = new List<string>
                    {
                        "wchoque@bsginstitute.com",
                        "jvillena@bsginstitute.com",
                        "gmiranda@bsginstitute.com"
                    };

                    TMK_MailService mailService = new TMK_MailService();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Plantillas",
                        Message = "Alumno: " + itemConjuntoListaResultado.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + itemConjuntoListaResultado.IdConjuntoListaResultado.ToString() + " < br/>" + e.Message + " <br/> Mensaje toString <br/> " + e.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    mailService.SetData(mailData);
                    mailService.SendMessageTask();
                }
            }
        }

        private List<PreWhatsAppResultadoConjuntoListaDTO> ValidarNumeroConjuntoListaMasivo(List<PreWhatsAppResultadoConjuntoListaDTO> NumerosValidados, int IdPersonal, int IdWhatsAppConfiguracionEnvio, int IdPlantilla, int IdConjuntoLista)
        {
            string UrlToPost;
            bool BanderaLogin = false;
            string TokenComunicacion = string.Empty;
            string Resultado = string.Empty;
            int EstadoValiacion = 0;

            var WhatsAppEstadoValidacionRepositorio = _unitOfWork.WhatsAppEstadoValidacionRepository;

            ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
            DTO.contacts = new List<string>();
            DTO.blocking = "wait";

            List<PreWhatsAppResultadoConjuntoListaDTO> ListaPreWhatsAppMensajePublicidad = new List<PreWhatsAppResultadoConjuntoListaDTO>();

            var ListaPaises = NumerosValidados.Where(x => x.IdCodigoPais != -1).GroupBy(x => x.IdCodigoPais).Select(x => x.Key).ToList();

            if (ListaPaises != null && ListaPaises.Count > 0)
            {
                var ListaEstados = WhatsAppEstadoValidacionRepositorio.ObtenerListaEstadosValidacionNumeroWhatsApp();
                foreach (var item in ListaPaises)
                {
                    var ListaNumeros = NumerosValidados.Where(x => x.IdCodigoPais == item).ToList();
                    if (ListaNumeros != null && ListaNumeros.Count > 0)
                    {
                        foreach (var Alumno in ListaNumeros)
                        {
                            DTO.contacts.Add("+" + Alumno.Celular);
                        }

                        var subListasBloque = DTO.contacts.Select((x, i) => new { Index = i, Value = x })
                        .GroupBy(x => x.Index / 2500)
                        .Select(x => x.Select(v => v.Value).ToList())
                        .ToList();
                        try
                        {
                            ServicePointManager.ServerCertificateValidationCallback =
                            delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                            {
                                return true;
                            };

                            var CredencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(item);
                            var TokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(IdPersonal, item);

                            var MensajeJson = JsonConvert.SerializeObject(DTO);
                            Resultado = string.Empty;

                            if (TokenValida == null || DateTime.Now >= TokenValida.ExpiresAfter)
                            {
                                string UrlToPostUsuario = CredencialesHost.UrlWhatsApp + "/v1/users/login";

                                var UserLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.CredencialUsuarioLogin(IdPersonal);

                                var Client = new RestClient(UrlToPostUsuario);
                                var Request = new RestSharp.RestRequest(Method.POST);
                                Request.AddHeader("cache-control", "no-cache");
                                Request.AddHeader("Content-Length", "");
                                Request.AddHeader("Accept-Encoding", "gzip, deflate");
                                Request.AddHeader("Host", CredencialesHost.IpHost);
                                Request.AddHeader("Cache-Control", "no-cache");
                                Request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(UserLogin.UserUsername + ":" + UserLogin.UserPassword)));
                                Request.AddHeader("Content-Type", "application/json");
                                IRestResponse Response = Client.Execute(Request);

                                if (Response.StatusCode == HttpStatusCode.OK)
                                {
                                    var Datos = JsonConvert.DeserializeObject<userLogeo>(Response.Content);

                                    foreach (var itemCredencial in Datos.users)
                                    {
                                        TWhatsAppUsuarioCredencial ModelCredencial = new TWhatsAppUsuarioCredencial();

                                        ModelCredencial.IdWhatsAppUsuario = UserLogin.IdWhatsAppUsuario;
                                        ModelCredencial.IdWhatsAppConfiguracion = CredencialesHost.Id;
                                        ModelCredencial.UserAuthToken = itemCredencial.token;
                                        ModelCredencial.ExpiresAfter = Convert.ToDateTime(itemCredencial.expires_after);
                                        ModelCredencial.EsMigracion = true;
                                        ModelCredencial.Estado = true;
                                        ModelCredencial.FechaCreacion = DateTime.Now;
                                        ModelCredencial.FechaModificacion = DateTime.Now;
                                        ModelCredencial.UsuarioCreacion = "whatsapp";
                                        ModelCredencial.UsuarioModificacion = "whatsapp";

                                        var Rpta = _unitOfWork.WhatsAppUsuarioCredencialRepository.Insert(ModelCredencial);

                                        TokenComunicacion = itemCredencial.token;
                                    }

                                    BanderaLogin = true;

                                }
                                else
                                {
                                    BanderaLogin = false;
                                }

                            }
                            else
                            {
                                TokenComunicacion = TokenValida.UserAuthToken;
                                BanderaLogin = true;
                            }

                            UrlToPost = CredencialesHost.UrlWhatsApp + "/v1/contacts";

                            foreach (var contactos in subListasBloque)
                            {
                                var nuevoBloque = new ValidarNumerosWhatsAppDTO() { blocking = DTO.blocking, contacts = contactos };
                                var mensajeJson = JsonConvert.SerializeObject(nuevoBloque);
                                if (BanderaLogin)
                                {
                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;

                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(nuevoBloque);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = mensajeJson.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        Resultado = Client.UploadString(UrlToPost, MyParameters);


                                    }

                                    var DatoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(Resultado);
                                    string NumeroValidadoRespuesta = string.Empty;

                                    foreach (var itemRespuesta in DatoRespuesta.contacts)
                                    {
                                        if (itemRespuesta.status == "valid")
                                        {
                                            NumeroValidadoRespuesta = itemRespuesta.wa_id;
                                        }
                                        else
                                        {
                                            NumeroValidadoRespuesta = itemRespuesta.input.Replace("+", "");
                                        }

                                        var UsuarioEstado = ListaNumeros.Where(x => x.Celular == NumeroValidadoRespuesta).FirstOrDefault();
                                        if (UsuarioEstado != null)
                                        {
                                            if (itemRespuesta.status == "valid")
                                            {
                                                UsuarioEstado.Validado = true;
                                            }
                                            else
                                            {
                                                UsuarioEstado.Validado = false;
                                            }

                                            var RespuestaEstado = ListaEstados.Where(x => x.Nombre == itemRespuesta.status).FirstOrDefault();

                                            if (RespuestaEstado != null)
                                            {
                                                EstadoValiacion = RespuestaEstado.Id;
                                            }
                                            else
                                            {
                                                EstadoValiacion = 4;
                                            }
                                            UsuarioEstado.IdWhatsAppEstadoValidacion = EstadoValiacion;


                                            TWhatsAppMensajePublicidad WhatsAppMensajePublicidad = new TWhatsAppMensajePublicidad();

                                            WhatsAppMensajePublicidad.IdAlumno = UsuarioEstado.IdAlumno;
                                            WhatsAppMensajePublicidad.IdPersonal = IdPersonal;
                                            WhatsAppMensajePublicidad.IdConjuntoListaResultado = UsuarioEstado.IdConjuntoListaResultado;
                                            WhatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                                            WhatsAppMensajePublicidad.IdPais = UsuarioEstado.IdCodigoPais;
                                            WhatsAppMensajePublicidad.Celular = UsuarioEstado.Celular;
                                            WhatsAppMensajePublicidad.EsValido = UsuarioEstado.Validado;
                                            WhatsAppMensajePublicidad.Estado = true;
                                            WhatsAppMensajePublicidad.IdWhatsAppEstadoValidacion = EstadoValiacion;
                                            WhatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                                            WhatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                                            WhatsAppMensajePublicidad.UsuarioCreacion = "Pre-ValidacionAutomatica";
                                            WhatsAppMensajePublicidad.UsuarioModificacion = "Pre-ValidacionAutomatica";
                                            _unitOfWork.WhatsAppMensajePublicidadRepository.Insert(WhatsAppMensajePublicidad);
                                            UsuarioEstado.IdWhatsappMensajePublicidad = WhatsAppMensajePublicidad.Id;

                                            UsuarioEstado.IdPersonal = IdPersonal;
                                            UsuarioEstado.IdPlantilla = IdPlantilla;

                                            ListaPreWhatsAppMensajePublicidad.Add(UsuarioEstado);

                                        }

                                    }
                                }


                            }


                        }
                        catch (Exception e)
                        {
                            //Enviar correo a sistemas
                            try
                            {
                                List<string> correos = new List<string>
                                {
                                    "ccrispin@bsginstitute.com",
                                    "jvillena@bsginstitute.com"
                                };

                                //------------------------------ Enviar Correo ----------------------//

                                //TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                                //TMKMailDataDTO mailData = new TMKMailDataDTO
                                //{
                                //    Sender = "jvillena@bsginstitute.com",
                                //    Recipient = string.Join(",", correos),
                                //    Subject = "Error Pre-Procesamiento Listas - Subrupo",
                                //    Message = "IdConjuntoLista: " + IdConjuntoLista.ToString() + " <br/>" + e.Message + (e.InnerException != null ? (" - " + e.InnerException.Message) : "") + " <br/> Mensaje toString <br/> " + e.ToString(),
                                //    Cc = "",
                                //    Bcc = "",
                                //    AttachedFiles = null
                                //};

                                //Mailservice.SetData(mailData);
                                //Mailservice.SendMessageTask();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    DTO.contacts.Clear();

                    //break;
                }

                return ListaPreWhatsAppMensajePublicidad;
            }
            else
            {
                List<PreWhatsAppResultadoConjuntoListaDTO> ListaPreWhatsAppMensajePublicidadError = new List<PreWhatsAppResultadoConjuntoListaDTO>();
                return ListaPreWhatsAppMensajePublicidadError;
            }
        }

        private void RemplazarEtiquetaMasivo(ref List<PreWhatsAppResultadoConjuntoListaDTO> NumeroAlumno, int IdPersonal, int IdPlantilla, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaPrincipal, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaSecundario)
        {
            string Plantilla = string.Empty;
            string Valor = string.Empty;
            string Numero = "";

            foreach (var AlumnoEtiqueta in NumeroAlumno)
            {
                if (AlumnoEtiqueta.Validado)
                {
                    try
                    {
                        AlumnoEtiqueta.objetoplantilla = new List<DatoPlantillaWhatsAppDTO>();

                        Numero = AlumnoEtiqueta.Celular;
                        if (Numero.StartsWith("51"))
                        {
                            Numero = Numero.Substring(2, 9);
                        }
                        else if (Numero.StartsWith("57"))
                        {
                            Numero = "00" + Numero;
                        }
                        else if (Numero.StartsWith("591"))
                        {
                            Numero = "00" + Numero;
                        }
                        else
                        {

                        }
                        var Alumno = _repAlumno.FirstBy(w => w.Id == AlumnoEtiqueta.IdAlumno, y => new { y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno });
                        //var Asesor = _repPersonal.FirstBy(w => w.Id == IdPersonal, y => new { y.Nombres, y.Apellidos, y.Anexo3Cx, y.Central, y.MovilReferencia });
                        var Asesor = _repPersonal.ObtenerDatoPersonal(IdPersonal);



                        Plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

                        PlantillaCentroCostoDTO Rpta = new PlantillaCentroCostoDTO();
                        ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
                        List<ModalidadProgramaDTO> Fecha = new List<ModalidadProgramaDTO>();
                        foreach (var item in ProgramaPrincipal)
                        {
                            Rpta = _repCentroCosto.ObtenerRemplazoPlantilla(item.IdPgeneral);
                            if (Plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || Plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || Plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                            {
                                Fecha = _repPgeneral.ObtenerFechaInicioProgramaGeneral(item.IdPgeneral, AlumnoEtiqueta.IdCodigoPais);

                                if (Fecha.Count > 0)
                                {
                                    FechaInicioPrograma = Fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                    if (FechaInicioPrograma == null)
                                    {
                                        FechaInicioPrograma = Fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                    }
                                }
                            }
                            //plantillaPw.ObtenerFechaInicioPrograma(item.IdPgeneral, rpta.IdCentroCosto);
                        }


                        foreach (string word in Plantilla.Split('{'))
                        {
                            var PlantillaEtiqueValor = new DatoPlantillaWhatsAppDTO();
                            if (word.Contains('}'))
                            {
                                string Etiqueta = word.Split('}')[0];
                                //Separamos solo los Id´s

                                if (Etiqueta.Contains("tPartner.nombre"))
                                {

                                    Valor = Rpta.NombrePartner;
                                }
                                else if (Etiqueta.Contains("tPEspecifico.nombre"))
                                {
                                    Valor = Rpta.NombrePEspecifico;
                                }
                                else if (Etiqueta.Contains("tPLA_PGeneral.Nombre"))
                                {
                                    Valor = Rpta.NombrePGeneral;
                                }

                                if (Etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                                {
                                    if (Fecha.Count != 0)
                                    {
                                        CultureInfo ci = new CultureInfo("es-ES");
                                        DateTime FechaInicioetiqueta = new DateTime();
                                        FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                        Valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                        Valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Valor);
                                    }
                                    else
                                    {
                                        Valor = "";
                                    }
                                }
                                else if (Etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                                {
                                    if (Fecha.Count != 0)
                                    {
                                        DateTime FechaInicioetiqueta = new DateTime();
                                        FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                        Valor = FechaInicioetiqueta.Day.ToString();
                                    }
                                    else
                                    {
                                        Valor = "";
                                    }
                                }
                                else if (Etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                                {
                                    if (Fecha.Count != 0)
                                    {
                                        //CultureInfo ci = new CultureInfo("es-Es");
                                        DateTime FechaInicioetiqueta = new DateTime();
                                        FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                        Valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                        Valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Valor);
                                    }
                                    else
                                    {
                                        Valor = "";
                                    }
                                }
                                if (Etiqueta.Contains("Template"))
                                {
                                    Valor = "";
                                }
                                else
                                {

                                    if ((Etiqueta == "tPersonal.email" || Etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || Etiqueta == "tPersonal.nombres" || Etiqueta == "tPersonal.apellidos" || Etiqueta == "tPersonal.UrlFirmaCorreos" || Etiqueta == "tPersonal.Telefono" || Etiqueta == "tAlumnos.apepaterno" || Etiqueta == "tAlumnos.apematerno" || Etiqueta == "tAlumnos.nombre1" || Etiqueta == "tAlumnos.nombre2") && IdPersonal > 0)
                                    {
                                        switch (Etiqueta)
                                        {
                                            case "tPersonal.PrimerNombreApellidoPaterno":
                                                Valor = Asesor.PrimerNombreApellidoPaterno; break;
                                            case "tPersonal.email":
                                                Valor = Asesor.Email; break;
                                            case "tPersonal.nombres":
                                                Valor = Asesor.Nombres; break;
                                            case "tPersonal.apellidos":
                                                Valor = Asesor.Apellidos; break;
                                            case "tPersonal.Telefono":
                                                {
                                                    if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
                                                    {
                                                        Valor = Asesor.MovilReferencia;
                                                    }
                                                    else
                                                    {
                                                        if (Asesor.Central == "192.168.0.20" || Asesor.Central == "192.168.2.20")
                                                        {
                                                            //Arequipa //lima
                                                            Valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
                                                        }
                                                        else if (Asesor.Central == "192.168.3.20")
                                                        {
                                                            //bogota
                                                            Valor = "57 (601) 381 9462 - Anexo " + Asesor.Anexo3Cx;
                                                        }
                                                        else if (Asesor.Central == "192.168.4.20")
                                                        {
                                                            //cd mexico
                                                            Valor = "52 (55) 4000 3255 - Anexo " + Asesor.Anexo3Cx;
                                                        }
                                                        else if (Asesor.Central == "192.168.5.20")
                                                        {
                                                            //santiago
                                                            Valor = "56 (2) 2760 9120 - Anexo " + Asesor.Anexo3Cx;
                                                        }
                                                        else
                                                        {
                                                            Valor = "No registra central asignada";
                                                        }
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.apepaterno":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        Valor = Alumno.ApellidoPaterno;
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.apematerno":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        Valor = Alumno.ApellidoMaterno;
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.nombre1":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        Valor = Alumno.Nombre1;
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.nombre2":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        Valor = Alumno.Nombre2;
                                                    }
                                                }
                                                break;
                                            default:
                                                Valor = ""; break;
                                        }

                                    }
                                }
                                if (Valor != null)
                                {
                                    Valor = Valor.Replace("#$%", "<br>");
                                    Plantilla = Plantilla.Replace("{" + Etiqueta + "}", Valor);

                                    PlantillaEtiqueValor.Codigo = "{ " + Etiqueta + "}";
                                    PlantillaEtiqueValor.Texto = Valor;

                                }
                                else
                                {
                                    Plantilla = Plantilla.Replace("{" + Etiqueta + "}", "");

                                    PlantillaEtiqueValor.Codigo = "{ " + Etiqueta + "}";
                                    PlantillaEtiqueValor.Texto = "";
                                }
                                AlumnoEtiqueta.objetoplantilla.Add(PlantillaEtiqueValor);
                            }
                        }
                        AlumnoEtiqueta.Plantilla = Plantilla;
                        //return Ok(new { plantilla, objetoplantilla });
                    }
                    catch (Exception ex)
                    {
                        List<string> correos = new List<string>();
                        correos.Add("fvaldez@bsginstitute.com");

                        //------------- Aca poner funcion edson enviar correos --------------------------//

                        //TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        //TMKMailDataDTO mailData = new TMKMailDataDTO();
                        //mailData.Sender = "fvaldez@bsginstitute.com";
                        //mailData.Recipient = string.Join(",", correos);
                        //mailData.Subject = "Error Proceso Plantillas";
                        //mailData.Message = "Alumno: " + AlumnoEtiqueta.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + AlumnoEtiqueta.IdConjuntoListaResultado.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                        //mailData.Cc = "";
                        //mailData.Bcc = "";
                        //mailData.AttachedFiles = null;

                        //Mailservice.SetData(mailData);
                        //Mailservice.SendMessageTask();
                    }
                }
                else
                {
                    AlumnoEtiqueta.Plantilla = Plantilla;
                }
            }
        }


        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Funcion con los resultados donde ya se registra si los numeros osn validos, plantilla, varibles en la plantilla y su estado del numero, permitiendo guardar la lista pre procesada para el envio por el servicio
        /// </summary>
        /// <param name="RegistrosProcesados">Lista de registros procesado</param>
        /// <param name="IdPersonal">Identificador del personal</param>
        /// <param name="IdWhatsAppConfiguracionEnvio">Identificador de la configuracion</param>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista detalle</param>
        /// <returns>Retorna true si el proceso cuncluye de forma exitosa, caso contrario false</returns>
        public bool RegistraPreValidacion(List<PreWhatsAppResultadoConjuntoListaDTO> RegistrosProcesados, int IdPersonal, int IdWhatsAppConfiguracionEnvio, int IdConjuntoListaDetalle)
        {
            try
            {
                List<TWhatsAppConfiguracionPreEnvio> listaWhatsAppConfiguracionPreEnvio = new List<TWhatsAppConfiguracionPreEnvio>();

                try
                {
                    foreach (var item in RegistrosProcesados)
                    {
                        try
                        {
                            TWhatsAppConfiguracionPreEnvio WhatsAppConfiguracionPreEnvio = new TWhatsAppConfiguracionPreEnvio();

                            WhatsAppConfiguracionPreEnvio.IdWhatsappMensajePublicidad = item.IdWhatsappMensajePublicidad;
                            WhatsAppConfiguracionPreEnvio.IdConjuntoListaResultado = item.IdConjuntoListaResultado;
                            WhatsAppConfiguracionPreEnvio.IdAlumno = item.IdAlumno;
                            WhatsAppConfiguracionPreEnvio.Celular = item.Celular;
                            WhatsAppConfiguracionPreEnvio.IdPais = item.IdCodigoPais;
                            WhatsAppConfiguracionPreEnvio.NroEjecucion = item.NroEjecucion;
                            WhatsAppConfiguracionPreEnvio.Validado = item.Validado;
                            WhatsAppConfiguracionPreEnvio.Plantilla = item.Plantilla;
                            WhatsAppConfiguracionPreEnvio.IdPersonal = item.IdPersonal;
                            WhatsAppConfiguracionPreEnvio.IdPgeneral = item.IdPgeneral;
                            WhatsAppConfiguracionPreEnvio.IdPlantilla = item.IdPlantilla;
                            WhatsAppConfiguracionPreEnvio.IdWhatsAppEstadoValidacion = (int)item.IdWhatsAppEstadoValidacion;

                            if (item.objetoplantilla != null && item.objetoplantilla.Count > 0)
                            {
                                WhatsAppConfiguracionPreEnvio.ObjetoPlantilla = JsonConvert.SerializeObject(item.objetoplantilla);
                            }
                            else
                            {
                                WhatsAppConfiguracionPreEnvio.ObjetoPlantilla = "";
                            }

                            WhatsAppConfiguracionPreEnvio.IdConjuntoListaDetalle = IdConjuntoListaDetalle;
                            WhatsAppConfiguracionPreEnvio.Procesado = false;
                            WhatsAppConfiguracionPreEnvio.MensajeProceso = "No Porcesado";

                            WhatsAppConfiguracionPreEnvio.Estado = true;
                            WhatsAppConfiguracionPreEnvio.FechaCreacion = DateTime.Now;
                            WhatsAppConfiguracionPreEnvio.FechaModificacion = DateTime.Now;
                            WhatsAppConfiguracionPreEnvio.UsuarioCreacion = "PreDatosAutomatica";
                            WhatsAppConfiguracionPreEnvio.UsuarioModificacion = "PreDatosAutomatica";

                            listaWhatsAppConfiguracionPreEnvio.Add(WhatsAppConfiguracionPreEnvio);
                        }
                        catch (Exception ex)
                        {
                        }

                    }

                    bool resultado = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.Insert(listaWhatsAppConfiguracionPreEnvio);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool ProcesarListasWhatsappEnvioAutomaticoMasivo(List<ConjuntoListaDetalleWhatsAppDTO> ListasWhatsApp)
        {
            var seguimientoPreProcesoListaWhatsApp = new SeguimientoPreProcesoListaWhatsApp();
            int seguimientoLista = 0, contadorRegistros = 0;

            try
            {
                if (ListasWhatsApp != null && ListasWhatsApp.Any())
                {
                    var datoSeguimientoLista = ListasWhatsApp.Select(x => x.IdConjuntoLista).FirstOrDefault();

                    seguimientoLista = datoSeguimientoLista != null ? datoSeguimientoLista.Value : 0;

                    seguimientoPreProcesoListaWhatsApp.IdEstadoSeguimientoPreProcesoListaWhatsApp = 2;
                    seguimientoPreProcesoListaWhatsApp.IdConjuntoLista = seguimientoLista;
                    seguimientoPreProcesoListaWhatsApp.Estado = true;
                    seguimientoPreProcesoListaWhatsApp.FechaCreacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.FechaModificacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.UsuarioCreacion = "Pre-ProcesoAutomatico";
                    seguimientoPreProcesoListaWhatsApp.UsuarioModificacion = "Pre-ProcesoAutomatico";

                    _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.Insert(seguimientoPreProcesoListaWhatsApp);

                    foreach (var WhatsAppConfiguracionEnvio in ListasWhatsApp)
                    {
                        try
                        {
                            var respuesta = _unitOfWork.ConjuntoListaResultadoRepository.ObtenerListaPreparadaProcesamiento(WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);

                            if (respuesta != null && respuesta.Any())
                            {
                                contadorRegistros++;

                                var resultadoValidacion = this.ValidarNumeroConjuntoListaMasivo(respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.Id ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), WhatsAppConfiguracionEnvio.IdConjuntoLista ?? default(int));

                                this.RemplazarEtiquetaMasivo(ref resultadoValidacion, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), WhatsAppConfiguracionEnvio.ProgramaPrincipal, WhatsAppConfiguracionEnvio.ProgramaSecundario);

                                var resultadoInsercion = RegistraPreValidacion(resultadoValidacion, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.Id ?? default(int), WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);
                            }
                        }
                        catch (Exception ex)
                        {
                            var LogSeguimientoBO = _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.FirstById(seguimientoPreProcesoListaWhatsApp.Id);
                            LogSeguimientoBO.IdEstadoSeguimientoPreProcesoListaWhatsApp = ValorEstatico.IdEstadoSeguimientoPreProcesoListaWhatsAppSinDatos;
                            LogSeguimientoBO.FechaModificacion = DateTime.Now;
                            _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.Update(LogSeguimientoBO);
                        }
                    }

                    if (contadorRegistros > 0)
                    {
                        var LogSeguimientoBO = _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.FirstById(seguimientoPreProcesoListaWhatsApp.Id);
                        LogSeguimientoBO.IdEstadoSeguimientoPreProcesoListaWhatsApp = 3;
                        LogSeguimientoBO.FechaModificacion = DateTime.Now;
                        _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.Update(LogSeguimientoBO);

                    }
                    else
                    {
                        var LogSeguimientoBO = _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.FirstById(seguimientoPreProcesoListaWhatsApp.Id);
                        LogSeguimientoBO.IdEstadoSeguimientoPreProcesoListaWhatsApp = ValorEstatico.IdEstadoSeguimientoPreProcesoListaWhatsAppSinDatos;
                        LogSeguimientoBO.FechaModificacion = DateTime.Now;
                        _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.Update(LogSeguimientoBO);
                    }
                }
                else
                {
                    seguimientoPreProcesoListaWhatsApp.IdEstadoSeguimientoPreProcesoListaWhatsApp = ValorEstatico.IdEstadoSeguimientoPreProcesoListaWhatsAppSinDatos;
                    seguimientoPreProcesoListaWhatsApp.IdConjuntoLista = seguimientoLista;
                    seguimientoPreProcesoListaWhatsApp.Estado = true;
                    seguimientoPreProcesoListaWhatsApp.FechaCreacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.FechaModificacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.UsuarioCreacion = "Pre-ProcesoAutomatico";
                    seguimientoPreProcesoListaWhatsApp.UsuarioModificacion = "Pre-ProcesoAutomatico";

                    _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.Insert(seguimientoPreProcesoListaWhatsApp);
                }

                return true;
            }
            catch (Exception Ex)
            {
                var DatoSeguimientoLista = ListasWhatsApp.Select(x => x.IdConjuntoLista).FirstOrDefault();
                if (DatoSeguimientoLista != null)
                {
                    seguimientoLista = DatoSeguimientoLista.Value;
                }
                else
                {
                    seguimientoLista = 0;
                }

                if (seguimientoPreProcesoListaWhatsApp != null && seguimientoPreProcesoListaWhatsApp.Id != 0)
                {
                    var LogSeguimientoBO = _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.FirstById(seguimientoPreProcesoListaWhatsApp.Id);
                    LogSeguimientoBO.IdEstadoSeguimientoPreProcesoListaWhatsApp = 5;
                    LogSeguimientoBO.FechaModificacion = DateTime.Now;
                    _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.Update(LogSeguimientoBO);
                }
                else
                {
                    seguimientoPreProcesoListaWhatsApp.IdEstadoSeguimientoPreProcesoListaWhatsApp = 5;
                    seguimientoPreProcesoListaWhatsApp.IdConjuntoLista = seguimientoLista;
                    seguimientoPreProcesoListaWhatsApp.Estado = true;
                    seguimientoPreProcesoListaWhatsApp.FechaCreacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.FechaModificacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.UsuarioCreacion = "Pre-ProcesoAutomatico";
                    seguimientoPreProcesoListaWhatsApp.UsuarioModificacion = "Pre-ProcesoAutomatico";

                    _unitOfWork.SeguimientoPreProcesoListaWhatsAppRepository.Insert(seguimientoPreProcesoListaWhatsApp);
                }


                return false;
            }
        }
    }
}

