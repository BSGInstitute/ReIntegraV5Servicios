using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TiempoFrecuenciaService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_TiempoFrecuencia
    /// </summary>
    public class TiempoFrecuenciaService : ITiempoFrecuenciaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TiempoFrecuenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoInteracccion, TiempoFrecuencia>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TiempoFrecuencia Add(TiempoFrecuencia entidad)
        {
            try
            {
                var modelo = _unitOfWork.TiempoFrecuenciaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TiempoFrecuencia>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TiempoFrecuencia Update(TiempoFrecuencia entidad)
        {
            try
            {
                var modelo = _unitOfWork.TiempoFrecuenciaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TiempoFrecuencia>(modelo);
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
                _unitOfWork.TiempoFrecuenciaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TiempoFrecuencia> Add(List<TiempoFrecuencia> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TiempoFrecuenciaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TiempoFrecuencia>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TiempoFrecuencia> Update(List<TiempoFrecuencia> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TiempoFrecuenciaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TiempoFrecuencia>>(modelo);
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
                _unitOfWork.TiempoFrecuenciaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


       public List<DTO.ComboDTO> ObtenerListaTiempoFrecuencia()
        {
            try
            {
                return _unitOfWork.TiempoFrecuenciaRepository.ObtenerListaTiempoFrecuencia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DTO.ComboDTO> ObtenerListaParaFiltroSegmento()
        {
            try
            {
                return _unitOfWork.TiempoFrecuenciaRepository.ObtenerListaParaFiltroSegmento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
