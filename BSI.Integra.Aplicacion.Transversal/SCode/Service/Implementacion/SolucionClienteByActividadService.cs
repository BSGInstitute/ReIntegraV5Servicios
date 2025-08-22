using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{

    /// Service: SolucionClienteByActividadService
    /// Autor: Gilmer  quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de SolucionClienteByActividad
    /// </summary>
    public class SolucionClienteByActividadService : ISolucionClienteByActividadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SolucionClienteByActividadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolucionClienteByActividad, SolucionClienteByActividad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SolucionClienteByActividad Add(SolucionClienteByActividad entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolucionClienteByActividadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolucionClienteByActividad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolucionClienteByActividad Update(SolucionClienteByActividad entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolucionClienteByActividadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolucionClienteByActividad>(modelo);
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
                _unitOfWork.SolucionClienteByActividadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolucionClienteByActividad> Add(List<SolucionClienteByActividad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolucionClienteByActividadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolucionClienteByActividad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolucionClienteByActividad> Update(List<SolucionClienteByActividad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolucionClienteByActividadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolucionClienteByActividad>>(modelo);
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
                _unitOfWork.SolucionClienteByActividadRepository.Delete(listadoIds, usuario);
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
