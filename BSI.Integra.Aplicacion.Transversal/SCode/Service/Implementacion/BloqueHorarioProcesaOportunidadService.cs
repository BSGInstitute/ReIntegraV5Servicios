using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: BloqueHorarioProcesaOportunidadService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_BloqueHorarioProcesaOportunidad
    /// </summary>
    public class BloqueHorarioProcesaOportunidadService : IBloqueHorarioProcesaOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public BloqueHorarioProcesaOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TBloqueHorarioProcesaOportunidad, BloqueHorarioProcesaOportunidad>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public BloqueHorarioProcesaOportunidad Add(BloqueHorarioProcesaOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.BloqueHorarioProcesaOportunidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<BloqueHorarioProcesaOportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BloqueHorarioProcesaOportunidad Update(BloqueHorarioProcesaOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.BloqueHorarioProcesaOportunidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<BloqueHorarioProcesaOportunidad>(modelo);
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
                _unitOfWork.BloqueHorarioProcesaOportunidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BloqueHorarioProcesaOportunidad> Add(List<BloqueHorarioProcesaOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.BloqueHorarioProcesaOportunidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<BloqueHorarioProcesaOportunidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BloqueHorarioProcesaOportunidad> Update(List<BloqueHorarioProcesaOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.BloqueHorarioProcesaOportunidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<BloqueHorarioProcesaOportunidad>>(modelo);
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
                _unitOfWork.BloqueHorarioProcesaOportunidadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
