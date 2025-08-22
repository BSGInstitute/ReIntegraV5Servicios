using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: PaisAsignacionRegularService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_AlumnoCuponRegistro
    /// </summary>
    public class PaisAsignacionRegularService : IPaisAsignacionRegularService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PaisAsignacionRegularService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPaisAsignacionRegular, PaisAsignacionRegular>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PaisAsignacionRegular Add(PaisAsignacionRegular entidad)
        {
            try
            {
                var modelo = _unitOfWork.PaisAsignacionRegularRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PaisAsignacionRegular>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PaisAsignacionRegular Update(PaisAsignacionRegular entidad)
        {
            try
            {
                var modelo = _unitOfWork.PaisAsignacionRegularRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PaisAsignacionRegular>(modelo);
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
                _unitOfWork.PaisAsignacionRegularRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaisAsignacionRegular> Add(List<PaisAsignacionRegular> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PaisAsignacionRegularRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PaisAsignacionRegular>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaisAsignacionRegular> Update(List<PaisAsignacionRegular> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PaisAsignacionRegularRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PaisAsignacionRegular>>(modelo);
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
                _unitOfWork.PaisAsignacionRegularRepository.Delete(listadoIds, usuario);
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
