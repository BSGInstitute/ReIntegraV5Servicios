using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AsignacionAutomaticaConfiguracion
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AsignacionAutomaticaConfiguracion
    /// </summary>
    public class AsignacionAutomaticaConfiguracionService : IAsignacionAutomaticaConfiguracionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AsignacionAutomaticaConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAsignacionAutomaticaConfiguracion, AsignacionAutomaticaConfiguracion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AsignacionAutomaticaConfiguracion Add(AsignacionAutomaticaConfiguracion entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaConfiguracionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionAutomaticaConfiguracion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionAutomaticaConfiguracion Update(AsignacionAutomaticaConfiguracion entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaConfiguracionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionAutomaticaConfiguracion>(modelo);
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
                _unitOfWork.AsignacionAutomaticaConfiguracionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionAutomaticaConfiguracion> Add(List<AsignacionAutomaticaConfiguracion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaConfiguracionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionAutomaticaConfiguracion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionAutomaticaConfiguracion> Update(List<AsignacionAutomaticaConfiguracion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaConfiguracionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionAutomaticaConfiguracion>>(modelo);
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
                _unitOfWork.AsignacionAutomaticaConfiguracionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Lista de Tipo de Datos para filtro en formularios
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<FiltroDTO> ObtenerFiltro()
        //{
        //    try
        //    {
        //        return _unitOfWork.AsignacionAutomaticaConfiguracionRepository.ObtenerFiltro();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
