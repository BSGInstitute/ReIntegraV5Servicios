using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadPrerequisitoEspecificoService
    /// Autor: Gilmer  quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de OportunidadPrerequisitoEspecifico
    /// </summary>
    public class OportunidadPrerequisitoEspecificoService : IOportunidadPrerequisitoEspecificoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OportunidadPrerequisitoEspecificoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadPrerequisitoEspecifico, OportunidadPrerequisitoEspecifico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public OportunidadPrerequisitoEspecifico Add(OportunidadPrerequisitoEspecifico entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPrerequisitoEspecificoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadPrerequisitoEspecifico>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadPrerequisitoEspecifico Update(OportunidadPrerequisitoEspecifico entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPrerequisitoEspecificoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadPrerequisitoEspecifico>(modelo);
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
                _unitOfWork.OportunidadPrerequisitoEspecificoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadPrerequisitoEspecifico> Add(List<OportunidadPrerequisitoEspecifico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPrerequisitoEspecificoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadPrerequisitoEspecifico>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadPrerequisitoEspecifico> Update(List<OportunidadPrerequisitoEspecifico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadPrerequisitoEspecificoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadPrerequisitoEspecifico>>(modelo);
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
                _unitOfWork.OportunidadPrerequisitoEspecificoRepository.Delete(listadoIds, usuario);
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
