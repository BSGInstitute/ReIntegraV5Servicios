using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OperadorComparacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 02/08/2022
    /// <summary>
    /// Gestión general de T_OperadorComparacion
    /// </summary>
    public class OperadorComparacionService : IOperadorComparacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OperadorComparacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOperadorComparacion, OperadorComparacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<OperadoresComparacionDTO, OperadorComparacion>(MemberList.None).ReverseMap();
            }
          );

           
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OperadorComparacion Add(OperadorComparacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.OperadorComparacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OperadorComparacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OperadorComparacion Update(OperadorComparacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.OperadorComparacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OperadorComparacion>(modelo);
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
                _unitOfWork.OperadorComparacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OperadorComparacion> Add(List<OperadorComparacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OperadorComparacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OperadorComparacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OperadorComparacion> Update(List<OperadorComparacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OperadorComparacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OperadorComparacion>>(modelo);
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
                _unitOfWork.OperadorComparacionRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OperadorComparacion
        /// </summary>
        /// <returns> List<OperadorComparacionDTO> </returns>
        public IEnumerable<OperadorComparacionDTO> ObtenerOperadorComparacion()
        {
            try
            {
                return _unitOfWork.OperadorComparacionRepository.ObtenerOperadorComparacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de T_OperadorComparacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<OperadorComparacionComboNombreDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.OperadorComparacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de OperadorComparacionDTO para mostrarse Listado
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<OperadoresComparacionDTO> ObtenerListado()
        {
            try
            {
                return _unitOfWork.OperadorComparacionRepository.ObtenerListado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DTO.ComboDTO> ObtenerComboParaFilroSegmento()
        {
            try
            {
                return _unitOfWork.OperadorComparacionRepository.ObtenerComboParaFilroSegmento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
