using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EstadoActividadDetalleService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_EstadoActividadDetalle
    /// </summary>
    public class EstadoActividadDetalleService : IEstadoActividadDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EstadoActividadDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoInteracccion, EstadoActividadDetalle>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public EstadoActividadDetalle Add(EstadoActividadDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoActividadDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoActividadDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoActividadDetalle Update(EstadoActividadDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoActividadDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoActividadDetalle>(modelo);
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
                _unitOfWork.EstadoActividadDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoActividadDetalle> Add(List<EstadoActividadDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoActividadDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoActividadDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoActividadDetalle> Update(List<EstadoActividadDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoActividadDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoActividadDetalle>>(modelo);
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
                _unitOfWork.EstadoActividadDetalleRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


       public List<DTO.ComboDTO> ObtenerDetalleActividadFiltroCodigo()
        {
            try
            {
                return _unitOfWork.EstadoActividadDetalleRepository.ObtenerDetalleActividadFiltroCodigo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
