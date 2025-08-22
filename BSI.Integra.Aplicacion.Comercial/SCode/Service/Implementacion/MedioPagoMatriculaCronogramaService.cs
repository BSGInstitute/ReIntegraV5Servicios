using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: MedioPagoMatriculaCronogramaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_MedioPagoMatriculaCronograma
    /// </summary>
    public class MedioPagoMatriculaCronogramaService : IMedioPagoMatriculaCronogramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MedioPagoMatriculaCronogramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TMedioPagoMatriculaCronograma, MedioPagoMatriculaCronograma>(MemberList.None).ReverseMap();
                    cfg.CreateMap<MedioPagoMatriculaCronogramaDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public MedioPagoMatriculaCronograma Add(MedioPagoMatriculaCronograma entidad)
        {
            try
            {
                var modelo = _unitOfWork.MedioPagoMatriculaCronogramaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MedioPagoMatriculaCronograma>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedioPagoMatriculaCronograma Update(MedioPagoMatriculaCronograma entidad)
        {
            try
            {
                var modelo = _unitOfWork.MedioPagoMatriculaCronogramaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MedioPagoMatriculaCronograma>(modelo);
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
                _unitOfWork.MedioPagoMatriculaCronogramaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MedioPagoMatriculaCronograma> Add(List<MedioPagoMatriculaCronograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MedioPagoMatriculaCronogramaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MedioPagoMatriculaCronograma>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MedioPagoMatriculaCronograma> Update(List<MedioPagoMatriculaCronograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MedioPagoMatriculaCronogramaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MedioPagoMatriculaCronograma>>(modelo);
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
                _unitOfWork.MedioPagoMatriculaCronogramaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MedioPagoMatriculaCronograma
        /// </summary>
        /// <returns> List<MedioPagoMatriculaCronogramaDTO> </returns>
        public IEnumerable<MedioPagoMatriculaCronogramaDTO> ObtenerMedioPagoMatriculaCronograma()
        {
            try
            {
                return _unitOfWork.MedioPagoMatriculaCronogramaRepository.ObtenerMedioPagoMatriculaCronograma();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene atributos principales de MedioPagoMatriculaCronograma relacionados a un IdMatriculaCabecera.
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> List< MedioPagoMatriculaCronogramaDTO> </returns>
        public  MedioPagoMatriculaCronogramaDTO ObtenerMedioPagoMatriculaCronogramaPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MedioPagoMatriculaCronogramaRepository.ObtenerMedioPagoMatriculaCronogramaPorIdMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el método de pago registrado según el IdMatricula
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> List< MedioPagoMatriculaCronogramaDTO> </returns>
        public  MedioPagoMatriculaCronogramaDTO MedioPagoMatriculaCronogramaPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MedioPagoMatriculaCronogramaRepository.MedioPagoMatriculaCronogramaPorIdMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Cambia el método de pago registrado para la matrícula
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> bool </returns>
        public bool DesactivarMedioPagoMatriculaCronogramaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MedioPagoMatriculaCronogramaRepository.DesactivarMedioPagoMatriculaCronogramaPorMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Cambia el método de pago registrado para la matrícula
        /// </summary>
        /// <param name="medioPagoMatricula">Datos de MedioPago a Insertar</param>
        /// <returns>RegistroMedioPagoMatriculaCronogramaDTO</returns>
        public RegistroMedioPagoMatriculaCronogramaDTO RegistroMedioPagoMatriculaCronograma(RegistroMedioPagoMatriculaCronogramaDTO medioPagoMatricula)
        {
            try
            {
                return _unitOfWork.MedioPagoMatriculaCronogramaRepository.RegistroMedioPagoMatriculaCronograma(medioPagoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
