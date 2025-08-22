using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: OrigenDatoCalidadService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenDatoCalidad
    /// </summary>
    public class OrigenDatoCalidadService : IOrigenDatoCalidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OrigenDatoCalidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOrigenDatoCalidad, OrigenDatoCalidad>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OrigenDatoCalidad Add(OrigenDatoCalidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenDatoCalidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OrigenDatoCalidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrigenDatoCalidad Update(OrigenDatoCalidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenDatoCalidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OrigenDatoCalidad>(modelo);
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
                _unitOfWork.OrigenDatoCalidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrigenDatoCalidad> Add(List<OrigenDatoCalidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenDatoCalidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OrigenDatoCalidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrigenDatoCalidad> Update(List<OrigenDatoCalidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenDatoCalidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OrigenDatoCalidad>>(modelo);
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
                _unitOfWork.OrigenDatoCalidadRepository.Delete(listadoIds, usuario);
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
