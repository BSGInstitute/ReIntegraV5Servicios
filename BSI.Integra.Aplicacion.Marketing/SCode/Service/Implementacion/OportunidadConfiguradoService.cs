using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: OportunidadConfiguradoService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OportunidadConfigurado
    /// </summary>
    public class OportunidadConfiguradoService : IOportunidadConfiguradoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OportunidadConfiguradoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOportunidadConfigurado, OportunidadConfigurado>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OportunidadConfigurado Add(OportunidadConfigurado entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadConfiguradoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadConfigurado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadConfigurado Update(OportunidadConfigurado entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadConfiguradoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadConfigurado>(modelo);
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
                _unitOfWork.OportunidadConfiguradoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadConfigurado> Add(List<OportunidadConfigurado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadConfiguradoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadConfigurado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadConfigurado> Update(List<OportunidadConfigurado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadConfiguradoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadConfigurado>>(modelo);
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
                _unitOfWork.OportunidadConfiguradoRepository.Delete(listadoIds, usuario);
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
