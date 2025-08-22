using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: PaisConfiguracionAsignacionRegularService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_AlumnoCuponRegistro
    /// </summary>
    public class PaisConfiguracionAsignacionRegularService : IPaisConfiguracionAsignacionRegularService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PaisConfiguracionAsignacionRegularService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPaisConfiguracionAsignacionRegular, PaisConfiguracionAsignacionRegular>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PaisConfiguracionAsignacionRegular Add(PaisConfiguracionAsignacionRegular entidad)
        {
            try
            {
                var modelo = _unitOfWork.PaisConfiguracionAsignacionRegularRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PaisConfiguracionAsignacionRegular>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PaisConfiguracionAsignacionRegular Update(PaisConfiguracionAsignacionRegular entidad)
        {
            try
            {
                var modelo = _unitOfWork.PaisConfiguracionAsignacionRegularRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PaisConfiguracionAsignacionRegular>(modelo);
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
                _unitOfWork.PaisConfiguracionAsignacionRegularRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaisConfiguracionAsignacionRegular> Add(List<PaisConfiguracionAsignacionRegular> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PaisConfiguracionAsignacionRegularRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PaisConfiguracionAsignacionRegular>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaisConfiguracionAsignacionRegular> Update(List<PaisConfiguracionAsignacionRegular> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PaisConfiguracionAsignacionRegularRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PaisConfiguracionAsignacionRegular>>(modelo);
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
                _unitOfWork.PaisConfiguracionAsignacionRegularRepository.Delete(listadoIds, usuario);
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
