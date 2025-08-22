using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using MailBee.ImapMail;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CorreoGmailService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/11/2022
    /// <summary>
    /// Gestión general de T_CorreoGmail
    /// </summary>
    public class CorreoGmailService : ICorreoGmailService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CorreoGmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCorreoGmail, CorreoGmail>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// <summary>
        /// Filtro de correos por persona.
        /// </summary>
        /// <param name="idFolder">Id de folder de correo</param>
        /// <param name="queryFiltro">Filtro para query</param>
        /// <returns> Lista de objetos (CorreoDTO) </returns>
        public List<CorreoDTO> FiltroCorreosPorPersona(int idFolder, string queryFiltro)
        {
            try
            {
                return _unitOfWork.CorreoGmailRepository.FiltroCorreosPorPersona(idFolder, queryFiltro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// <summary>
        /// Obtiene data modelada de mkt.T_GmailCorreo para casos webinar y aonline.
        /// </summary>
        /// <param name="queryFiltro"> Filtro para query </param>
        /// <returns> Lista de objetos (CorreoDTO) </returns>
        public List<CorreoDTO> FiltroCorreosPorPersonaGmailCorreo(string queryFiltro)
        {
            try
            {
                return _unitOfWork.CorreoGmailRepository.FiltroCorreosPorPersonaGmailCorreo(queryFiltro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// <summary>
        /// Cuenta correos por persona.
        /// </summary>
        /// <param name="idPersonal">Id de personal</param>
        /// <param name="idFolder">Id de folder de correo</param>
        /// <returns>int</returns>
        public int ContadorCorreosPorPersona(int idPersonal, int idFolder)
        {
            try
            {
                return _unitOfWork.CorreoGmailRepository.ContadorCorreosPorPersona(idPersonal, idFolder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene correos por grupos sin version.
        /// </summary>
        /// <param name="idCentroCosto">Id centro de costo</param>
        /// <param name="IdPaquete">Id de paquete</param>
        /// <param name="estado">Estado</param>
        /// <param name="subEstado">Sub estado</param>
        /// <returns> ListaCorreosGrupoDTO </returns>
        public ListaCorreosGrupoDTO ObtenerCorreosGruposSinVersion(int idCentroCosto, List<int> estado, List<int> subEstado)
        {
            try
            {
                return _unitOfWork.CorreoGmailRepository.ObtenerCorreosGruposSinVersion(idCentroCosto, estado, subEstado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// Autor: --, Jashin Salazar
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene correos por grupos con version.
        /// </summary>
        /// <param name="IdCentroCosto">Id centro de costo</param>
        /// <param name="IdPaquete">Id de paquete</param>
        /// <param name="Estados">Estado</param>
        /// <param name="SubEstados">Sub estado</param>
        /// <returns> ListaCorreosGrupoDTO </returns>
        public ListaCorreosGrupoDTO ObtenerCorreosGruposConVersion(int idCentroCosto, int idPaquete, List<int> estado, List<int> subEstado)
        {
            try
            {
                return _unitOfWork.CorreoGmailRepository.ObtenerCorreosGruposConVersion(idCentroCosto, idPaquete, estado, subEstado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Plantilla obtenerPlantilla()
        {
            try
            {
                return _unitOfWork.CorreoGmailRepository.obtenerPlantilla();

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO enviarAccesoPortalWebAlumnoWhatsApp(DatosOportunidadAccesosPortalDTO datosOportunidad)
        {
            try
            {
                var plantilla = _unitOfWork.CorreoGmailRepository.obtenerPlantilla();
                var alumno = _unitOfWork.AlumnoRepository.ObtenerDatosAlumnoPorId(datosOportunidad.idAlumno);
                var alumnoNumero = _unitOfWork.AlumnoRepository.ObtenerNumeroWhatsApp(alumno.IdCodigoPais, alumno.Celular);
                AlumnoService alumnoBO = new AlumnoService(_unitOfWork);
                ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();
                contact.contacts = new List<string>();
                contact.contacts.Add("+" + alumnoNumero);
                var validacion = alumnoBO.ValidarNumeroEnvioWhatsApp(datosOportunidad.idPersonalAsignado, alumno.IdCodigoPais, contact);
                RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO respuesta = new RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO()
                {
                    idPlantilla = plantilla.Id,
                    numero = alumnoNumero.Trim()
                };
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO enviarAccesoMoodleAlumnoWhatsApp(DatosOportunidadAccesosPortalDTO datosOportunidad)
        {
            try
            {
                AlumnoService alumnoBO = new AlumnoService(_unitOfWork);
                //AlumnoBO alumnoBO = new AlumnoBO();
                ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();

                contact.contacts = new List<string>();
                var alumno = _unitOfWork.AlumnoRepository.ObtenerDatosAlumnoPorId(datosOportunidad.idAlumno);
                var alumnoNumero = _unitOfWork.AlumnoRepository.ObtenerNumeroWhatsApp(alumno.IdCodigoPais, alumno.Celular);
                contact.contacts.Add("+" + alumnoNumero);
                var validacion = alumnoBO.ValidarNumeroEnvioWhatsApp(datosOportunidad.idPersonalAsignado, alumno.IdCodigoPais, contact);
                var accesos = _unitOfWork.CorreoGmailRepository.obtenerAccesosInicialesMoodle(datosOportunidad.idAlumno);
                var moodleAlumno = _unitOfWork.AlumnoRepository.ObtenerAccesosMoodle(accesos.usuarioMoodle);
                var plantilla = _unitOfWork.CorreoGmailRepository.obtenerPlantillaAccesoMoodleAlumnoWhatsApp();
                RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO respuesta = new RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO()
                {
                    idPlantilla = plantilla.Id,
                    numero = alumnoNumero.Trim()
                };
                return respuesta;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO enviarCondicionesCaracteristicas(DatosOportunidadAccesosPortalDTO datosOportunidad)
        {
            try
            {

                //var _repPlantilla = new PlantillaRepositorio(_integraDBContext);
                //var _repAlumno = new AlumnoRepositorio(_integraDBContext);
                //var alumno = _repAlumno.FirstById(DatosOportunidad.IdAlumno);
                var alumno = _unitOfWork.AlumnoRepository.ObtenerDatosAlumnoPorId(datosOportunidad.idAlumno);

                //1227	Condiciones y Características - PERÚ OPERACIONES
                //1245	Condiciones y Características - COLOMBIA OPERACIONES
                var _idPlantilla = 0;
                if (alumno.IdCodigoPais == 57)
                {
                    _idPlantilla = 1245;
                }
                else if (alumno.IdCodigoPais == 51)
                {
                    _idPlantilla = 1227;
                }


                RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO respuesta = new RetornoEnviarAccesoPortalWebAlumnoWhatsAppDTO()
                {
                    idPlantilla = _idPlantilla,
                };
                return respuesta;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DocumentoEnviadoWebPw InsertarEnvio(int IdOportunidad, string NombreUsuario)
        {

            try { 
                var _oportunidad = _unitOfWork.OportunidadRepository.ObtenerOportunidadPorId(IdOportunidad);
                //_repOportunidad.FirstById(IdOportunidad);
                var _pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto((int)_oportunidad.IdCentroCosto);

                DocumentoEnviadoWebPwService documentoEnviadoWebPwService = new DocumentoEnviadoWebPwService(_unitOfWork);
                // _repPEspecifico.FirstBy(x => x.IdCentroCosto == _oportunidad.IdCentroCosto);
                DocumentoEnviadoWebPw documentoEnviadoWebPw = new DocumentoEnviadoWebPw()
                {
                    IdAlumno =(int)_oportunidad.IdAlumno,
                    Nombre = "BSG Institute - Condiciones y Características",
                    IdPespecifico = _pEspecifico.Id,
                    FechaEnvio = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = NombreUsuario,
                    UsuarioModificacion = NombreUsuario,
                    Estado = true

                };
                documentoEnviadoWebPwService.Add(documentoEnviadoWebPw);

              

                return documentoEnviadoWebPw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public Plantilla enviarAccesoMoodleAlumno(DatosOportunidadAccesosPortalDTO datosOportunidad)
        {

            try
            {



                //integraDBContext contexto = new integraDBContext();
                //AulaVirtualContext moodleContext = new AulaVirtualContext();
                //MdlUser usuarioMoodle = new MdlUser();

                //MoodleWebService moodleWebService = new MoodleWebService();

                //PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                //AlumnoRepositorio _repAlumno = new AlumnoRepositorio();

                var accesos = _unitOfWork.CorreoGmailRepository.obtenerAccesosInicialesMoodle(datosOportunidad.idAlumno);
               
                // var accesos = _repAlumno.ObtenerAccesosInicialesMoodle(datosOportunidad.IdAlumno);

                //var moodleAlumno = moodleContext.MdlUser.Where(x => x.Username == accesos.UsuarioMoodle).FirstOrDefault();
                //if (moodleAlumno == null)
                //{
                //    return BadRequest("El alumno aún no tiene los accesos de aula virtual creados.");
                //}

                //MoodleWebServiceActualizarClaveDTO moodleWebServiceActualizarClave = new MoodleWebServiceActualizarClaveDTO();
                //moodleWebServiceActualizarClave.IdMoodle = moodleAlumno.Id;
                //moodleWebServiceActualizarClave.Clave = accesos.PasswordMoodle;

                //var actualizarClave = moodleWebService.ActualizarClaveMoodle(moodleWebServiceActualizarClave);
                //if (!actualizarClave.Estado)
                //{
                //    return BadRequest("No se pudo actualizar la clave del alumno en moodle");
                //}

               // var plantilla = _repPlantilla.FirstBy(x => x.Descripcion.Contains("Accesos para el aula virtual") && x.IdPlantillaBase == 2);
                //var plantilla = _unitOfWork.PlantillaRepository.FirstBy(x => x.Descripcion.Contains("Accesos para el aula virtual") && x.IdPlantillaBase == 2);
                //if (plantilla == null)
                //{
                //    return BadRequest(ModelState);
                //}
                //return (plantilla);


                var plantilla = _unitOfWork.CorreoGmailRepository.obtenerPlantillaAccesoMoodleAlumno();
                return plantilla;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
