using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: OportunidadPreAsignadumService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OportunidadPreAsignadum
    /// </summary>
    public class OportunidadPreAsignadumService : IOportunidadPreAsignadumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OportunidadPreAsignadumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOportunidadPreAsignadum, OportunidadPreAsignadum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OportunidadPreAsignadum Add(OportunidadPreAsignadum entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPreAsignadumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadPreAsignadum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadPreAsignadum Update(OportunidadPreAsignadum entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPreAsignadumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadPreAsignadum>(modelo);
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
                _unitOfWork.OportunidadPreAsignadumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadPreAsignadum> Add(List<OportunidadPreAsignadum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPreAsignadumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadPreAsignadum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadPreAsignadum> Update(List<OportunidadPreAsignadum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPreAsignadumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadPreAsignadum>>(modelo);
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
                _unitOfWork.OportunidadPreAsignadumRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public bool ProcesoAsignacionAutomatizada()
        {
            try
            {


                try
                {

                }
                catch
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
