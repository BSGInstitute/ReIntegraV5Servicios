using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadPrerequisitoGeneralService
    /// Autor: Gilmer  quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de OportunidadPrerequisitoGeneral
    /// </summary>
    public class OportunidadPrerequisitoGeneralService : IOportunidadPrerequisitoGeneralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OportunidadPrerequisitoGeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadPrerequisitoGeneral, OportunidadPrerequisitoGeneral>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public OportunidadPrerequisitoGeneral Add(OportunidadPrerequisitoGeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPrerequisitoGeneralRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadPrerequisitoGeneral>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadPrerequisitoGeneral Update(OportunidadPrerequisitoGeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPrerequisitoGeneralRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadPrerequisitoGeneral>(modelo);
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
                _unitOfWork.OportunidadPrerequisitoGeneralRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadPrerequisitoGeneral> Add(List<OportunidadPrerequisitoGeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPrerequisitoGeneralRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadPrerequisitoGeneral>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadPrerequisitoGeneral> Update(List<OportunidadPrerequisitoGeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPrerequisitoGeneralRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadPrerequisitoGeneral>>(modelo);
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
                _unitOfWork.OportunidadPrerequisitoGeneralRepository.Delete(listadoIds, usuario);
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
