using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: MandrilService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 04/07/2022
    /// <summary>
    /// Gestión general de T_Mandril
    /// </summary>
    public class MandrilService : IMandrilService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MandrilService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMandril, Mandril>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Mandril Add(Mandril entidad)
        {
            try
            {
                var modelo = _unitOfWork.MandrilRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Mandril>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Mandril Update(Mandril entidad)
        {
            try
            {
                var modelo = _unitOfWork.MandrilRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Mandril>(modelo);
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
                _unitOfWork.MandrilRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Mandril> Add(List<Mandril> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MandrilRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Mandril>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Mandril> Update(List<Mandril> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MandrilRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Mandril>>(modelo);
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
                _unitOfWork.MandrilRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 04/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Mandril
        /// </summary>
        /// <returns> List<MandrilDTO> </returns>
        public IEnumerable<MandrilDTO> ObtenerMandril()
        {
            try
            {
                return _unitOfWork.MandrilRepository.ObtenerMandril();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el ultimo Tipo de Interaccion de Correos asociados a un Alumno y un Personal.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> List<CorreoInteraccionV2AgendaDTO> </returns>
        public IEnumerable<CorreoInteraccionV2AgendaDTO> ObtenerCorreoInteraccionV2EnviadosPorPersonalParaAgenda(int idAlumno, int idPersonal)
        {
            try
            {
                return _unitOfWork.MandrilRepository.ObtenerCorreoInteraccionV2EnviadosPorPersonalParaAgenda(idAlumno, idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <param name="correoReceptor"></param>
        /// <param name="messageId"></param>
        /// <returns>CorreoAlumnoSpeechDTO</returns>
        public CorreoAlumnoSpeechDTO VerCorreoAlumnoSpeech(string correoReceptor, string messageId)
        {
            try
            {
                return _unitOfWork.MandrilRepository.VerCorreoAlumnoSpeech(correoReceptor, messageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Fecha: 05/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico para mezclar con correos recibidos
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idAsesor"></param>
        /// <returns>CorreoDTO</returns>
        public List<CorreoDTO> ListaInteraccionCorreoAlumnoCorreo(int idAlumno, int idAsesor)
        {
            try
            {
                return _unitOfWork.MandrilRepository.ListaInteraccionCorreoAlumnoCorreo(idAlumno, idAsesor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los correos enviados por un Asesor a un Alumno en especifico
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idAsesor"></param>
        ///  /// <param name="messageId"></param>
        /// <returns>CorreoInteraccionesAlumnoDTO</returns>
        public List<CorreoInteraccionesAlumnoDTO> ListaInteraccionCorreoAlumno(int idAlumno, int idAsesor, string messageId)
        {
            try
            {
                return _unitOfWork.MandrilRepository.ListaInteraccionCorreoAlumno(idAlumno, idAsesor, messageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
