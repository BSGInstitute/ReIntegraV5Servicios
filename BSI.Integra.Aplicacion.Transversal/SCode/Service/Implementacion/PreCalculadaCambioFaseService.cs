using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PreCalculadaCambioFaseService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_PreCalculadaCambioFase
    /// </summary>
    public class PreCalculadaCambioFaseService : IPreCalculadaCambioFaseService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreCalculadaCambioFaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreCalculadaCambioFase, PreCalculadaCambioFase>(MemberList.None).ReverseMap();
                cfg.CreateMap<PreCalculadaCambioFaseDTO, PreCalculadaCambioFase>(MemberList.None);
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PreCalculadaCambioFase Add(PreCalculadaCambioFase entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreCalculadaCambioFaseRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreCalculadaCambioFase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PreCalculadaCambioFase Update(PreCalculadaCambioFase entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreCalculadaCambioFaseRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PreCalculadaCambioFase>(modelo);
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
                _unitOfWork.PreCalculadaCambioFaseRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreCalculadaCambioFase> Add(List<PreCalculadaCambioFase> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreCalculadaCambioFaseRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreCalculadaCambioFase>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreCalculadaCambioFase> Update(List<PreCalculadaCambioFase> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreCalculadaCambioFaseRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PreCalculadaCambioFase>>(modelo);
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
                _unitOfWork.PreCalculadaCambioFaseRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreCalculadaCambioFase
        /// </summary>
        /// <returns> List<PreCalculadaCambioFaseDTO> </returns>
        public IEnumerable<PreCalculadaCambioFaseDTO> ObtenerPreCalculadaCambioFase()
        {
            try
            {
                return _unitOfWork.PreCalculadaCambioFaseRepository.ObtenerPreCalculadaCambioFase();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea de un PreCalculadaCambioFaseDTO a PreCalculadaCambioFase
        /// </summary>
        /// <returns> PreCalculadaCambioFase </returns>
        public PreCalculadaCambioFase MapeoEntidadDesdeDTO(PreCalculadaCambioFaseDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<PreCalculadaCambioFase>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el numero de Contador basado en el parametro recibido.
        /// </summary>
        /// <returns> int </returns>
        public int ExistePreCalculadaCambioFase(PreCalculadaCambioFase tPre)
        {
            try
            {
                return _unitOfWork.PreCalculadaCambioFaseRepository.ExistePreCalculadaCambioFase(tPre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
