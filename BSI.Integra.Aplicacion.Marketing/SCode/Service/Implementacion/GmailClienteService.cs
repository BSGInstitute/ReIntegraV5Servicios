using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using MailBee.Mime;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: GmailClienteService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_GmailCliente
    /// </summary>
    public class GmailClienteService : IGmailClienteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public GmailClienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TGmailCliente, GmailCliente>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public GmailCliente Add(GmailCliente entidad)
        {
            try
            {
                var modelo = _unitOfWork.GmailClienteRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GmailCliente>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GmailCliente Update(GmailCliente entidad)
        {
            try
            {
                var modelo = _unitOfWork.GmailClienteRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GmailCliente>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.GmailClienteRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GmailCliente> Add(List<GmailCliente> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GmailClienteRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GmailCliente>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GmailCliente> Update(List<GmailCliente> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GmailClienteRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GmailCliente>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.GmailClienteRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_GmailCliente
        /// </summary>
        /// <returns> List<GmailClienteDTO> </returns>
        public IEnumerable<GmailClienteDTO> ObtenerGmailCliente()
        {
            try
            {
                return _unitOfWork.GmailClienteRepository.ObtenerGmailCliente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_GmailCliente para mostrarse en combo.
        /// </summary>
        /// <returns> List<GmailClienteComboDTO> </returns>
        public IEnumerable<GmailClienteComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.GmailClienteRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las credenciales del Asesor, para conectarse al Servicio Imap.
        /// </summary>
        /// <param name="idAsesor">Id del Asesor</param>
        /// <returns> List<CorreoClienteCredencialDTO> </returns>
        public CorreoClienteCredencialDTO ObtenerClienteCredencial(int idAsesor)
        {
            try
            {
                return _unitOfWork.GmailClienteRepository.ObtenerClienteCredencial(idAsesor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 23/08/2022
        /// Version: 1.0
        /// <summary>
        /// Se arma el cuerpo del correo
        /// </summary>
        /// <returns> List<CorreoBodyDTO> </returns>
        public CorreoBodyDTO ObtenerCorreoBody(int IdAsesor, int IdCorreo, string Folder)
        {
            var _gmailClienteServicio = new GmailClienteService(_unitOfWork);
            _gmailClienteServicio.ObtenerClienteCredencial(IdAsesor);

            var _imapService = new TMK_ImapService();
            MailMessage mensaje = _imapService.ObtenerBodyCorreo(IdCorreo, _gmailClienteServicio.ObtenerClienteCredencial(IdAsesor).EmailAsesor, _gmailClienteServicio.ObtenerClienteCredencial(IdAsesor).PasswordCorreo, Folder);
            mensaje.Parser.PlainToHtmlMode = PlainToHtmlAutoConvert.IfNoHtml;
            var correoBodyDTO = new CorreoBodyDTO();

            string correo = _gmailClienteServicio.ObtenerClienteCredencial(IdAsesor).EmailAsesor.Substring(0, _gmailClienteServicio.ObtenerClienteCredencial(IdAsesor).EmailAsesor.IndexOf('@'));
            correoBodyDTO.EmailBody = "<br>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>";
            //correoBodyDTO.EmailBody = "<hr>" + mensaje.GetHtmlWithBase64EncodedRelatedFiles() + "<br/>--<br/>" + "<img src='" + _linkRepositorioFirmas + correo + ".png' />";

            if (mensaje.Attachments.Count > 0)
            {
                correoBodyDTO.ArchivosAdjuntos = new List<CorreoArchivoAdjuntoDTO>();
                foreach (Attachment attach in mensaje.Attachments)
                {
                    CorreoArchivoAdjuntoDTO archivoAdjunto = new CorreoArchivoAdjuntoDTO();
                    archivoAdjunto.IdCorreo = IdCorreo;
                    archivoAdjunto.NombreArchivo = attach.Filename.ToString();

                    correoBodyDTO.ArchivosAdjuntos.Add(archivoAdjunto);
                }
            }
            return (correoBodyDTO);
        }

    }
}
