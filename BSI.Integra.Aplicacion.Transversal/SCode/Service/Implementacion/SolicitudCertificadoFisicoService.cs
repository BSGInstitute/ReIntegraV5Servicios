using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ReportesService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de Solicitud de certificado físico
    /// </summary>
    public class SolicitudCertificadoFisicoService : ISolicitudCertificadoFisicoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SolicitudCertificadoFisicoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSolicitudCertificadoFisico, SolicitudCertificadoFisico>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SolicitudCertificadoFisico Add(SolicitudCertificadoFisico entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudCertificadoFisicoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudCertificadoFisico>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudCertificadoFisico Update(SolicitudCertificadoFisico entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudCertificadoFisicoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudCertificadoFisico>(modelo);
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
                _unitOfWork.SolicitudCertificadoFisicoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudCertificadoFisico> Add(List<SolicitudCertificadoFisico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudCertificadoFisicoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudCertificadoFisico>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudCertificadoFisico> Update(List<SolicitudCertificadoFisico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudCertificadoFisicoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudCertificadoFisico>>(modelo);
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
                _unitOfWork.SolicitudCertificadoFisicoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el nombre del courier de una solicitud
        /// </summary>
        /// <returns>L></returns>
        public string ObtenerCourierPorNombre(int idSolicitudCertificado)
        {
            try
            {
                return _unitOfWork.SolicitudCertificadoFisicoRepository.ObtenerCourierPorNombre(idSolicitudCertificado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tupla por idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public SolicitudCertificadoFisico ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.SolicitudCertificadoFisicoRepository.ObtenerPorIdMatriculaCabecera(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del reporte de Certificado Físico según el CodigoMatricula
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<DatosReporteEnvioCertificadoFisicoDTO> DatosReporteCertificadoEnvioFisicoPorId(string codigoMatricula)
        {
            try
            {
                return _unitOfWork.SolicitudCertificadoFisicoRepository.DatosReporteCertificadoEnvioFisicoPorId(codigoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 09/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del reporte de Certificado Físico según el CodigoMatricula
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<DatosEnvioAlumnoDTO> obtenerDatosEnvio(int IdMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.SolicitudCertificadoFisicoRepository.obtenerDatosEnvio(IdMatriculaCabecera);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DatosEnvioAlumnoDTO InsertarDatosEnviosOperaciones(DatosEnvioAlumnoDTO filtro)
        {
            try
            {
                return _unitOfWork.SolicitudCertificadoFisicoRepository.InsertarDatosEnviosOperaciones(filtro);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DatosRegistroEnvioFisicoDTO DatosRegistroEnvioFisico(int IdSolicitudCertificadoFisico)
        {
            try
            {
                return _unitOfWork.SolicitudCertificadoFisicoRepository.DatosRegistroEnvioFisico(IdSolicitudCertificadoFisico);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public bool InsertarSolicitudCertificado(SolicitudCertificadoFisicoDTO json)
        {
            try
            {



                var solicitudCertificadoFisicoBO = _unitOfWork.SolicitudCertificadoFisicoRepository.obtenerSolicitudCertificado(json.IdMatriculaCabecera, json.IdCertificadoGeneradoAutomatico);
                //    _repSolicitudCertificadoFisico.FirstBy(x => x.IdMatriculaCabecera == json.IdMatriculaCabecera && x.IdCertificadoGeneradoAutomatico == json.IdCertificadoGeneradoAutomatico);
                if (solicitudCertificadoFisicoBO.Id != 0) //Tienes que validarlo El ID, porque la funcion de arriba NUNCA te retornará NULL
                {
                    return false;
                }

                solicitudCertificadoFisicoBO = new SolicitudCertificadoFisico();

                solicitudCertificadoFisicoBO.IdMatriculaCabecera = json.IdMatriculaCabecera;
                solicitudCertificadoFisicoBO.IdPersonal = json.IdPersonal;
                solicitudCertificadoFisicoBO.FechaSolicitud = DateTime.Now;
                solicitudCertificadoFisicoBO.IdEstadoCertificadoFisico = 1; /*1= Pendiente de Envio*/
                solicitudCertificadoFisicoBO.IdCertificadoGeneradoAutomatico = json.IdCertificadoGeneradoAutomatico;
                solicitudCertificadoFisicoBO.Estado = true;
                solicitudCertificadoFisicoBO.FechaCreacion = DateTime.Now;
                solicitudCertificadoFisicoBO.FechaModificacion = DateTime.Now;
                solicitudCertificadoFisicoBO.UsuarioCreacion = json.Usuario;
                solicitudCertificadoFisicoBO.UsuarioModificacion = json.Usuario;

                var rpta = Add(solicitudCertificadoFisicoBO);

                return true;  //Ya esta


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DataSolicitudCertificadoFisicoDTO> ObtenerSolicitudesCertificadoFisico(filtroSolicitudCertificadoFisicoDTO json)
        {
            try
            {
                return _unitOfWork.SolicitudCertificadoFisicoRepository.ObtenerSolicitudesCertificadoFisico(json);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SolicitudCertificadoFisico ObtenerPorId(int id)
        {
            try
            {
                var registro = _unitOfWork.SolicitudCertificadoFisicoRepository.FirstById(id);
                return _mapper.Map<SolicitudCertificadoFisico>(registro);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
