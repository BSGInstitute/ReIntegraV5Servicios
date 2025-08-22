using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadBeneficioService
    /// Autor: Gilmer  quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de OportunidadBeneficio
    /// </summary>
    public class OportunidadBeneficioService : IOportunidadBeneficioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OportunidadBeneficioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadBeneficio, OportunidadBeneficio>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public OportunidadBeneficio Add(OportunidadBeneficio entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadBeneficioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadBeneficio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadBeneficio Update(OportunidadBeneficio entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadBeneficioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadBeneficio>(modelo);
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
                _unitOfWork.OportunidadBeneficioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadBeneficio> Add(List<OportunidadBeneficio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadBeneficioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadBeneficio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadBeneficio> Update(List<OportunidadBeneficio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadBeneficioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadBeneficio>>(modelo);
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
                _unitOfWork.OportunidadBeneficioRepository.Delete(listadoIds, usuario);
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
