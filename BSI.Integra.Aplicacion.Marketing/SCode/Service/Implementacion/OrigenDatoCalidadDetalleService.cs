using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: OrigenDatoCalidadDetalleService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenDatoCalidadDetalle
    /// </summary>
    public class OrigenDatoCalidadDetalleService : IOrigenDatoCalidadDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OrigenDatoCalidadDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOrigenDatoCalidadDetalle, OrigenDatoCalidadDetalle>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OrigenDatoCalidadDetalle Add(OrigenDatoCalidadDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenDatoCalidadDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OrigenDatoCalidadDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrigenDatoCalidadDetalle Update(OrigenDatoCalidadDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenDatoCalidadDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OrigenDatoCalidadDetalle>(modelo);
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
                _unitOfWork.OrigenDatoCalidadDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrigenDatoCalidadDetalle> Add(List<OrigenDatoCalidadDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenDatoCalidadDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OrigenDatoCalidadDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrigenDatoCalidadDetalle> Update(List<OrigenDatoCalidadDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenDatoCalidadDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OrigenDatoCalidadDetalle>>(modelo);
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
                _unitOfWork.OrigenDatoCalidadDetalleRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta los sectores junto con las configuraciones
        /// </summary>
        /// <returns>List<ActividadAgendaDTO></returns>
        public OrigenDatoCalidadDetalleConfiguracionDTO ObtenerOrigenSectorConfigurado(int IdOrigenSector)
        {
            try
            {
                OrigenDatoCalidadDetalleConfiguracionDTO ConfiguracionSector = new OrigenDatoCalidadDetalleConfiguracionDTO();

                ConfiguracionSector.origenDatoCalidadDetalleIndividual = _unitOfWork.OrigenDatoCalidadDetalleRepository.ObtenerOrigenSectorConfigurado(IdOrigenSector);
                ConfiguracionSector.origenDatoCalidadDetalleAgrupado.listaOrigenesAgrupado = _unitOfWork.OrigenDatoCalidadDetalleRepository.ObtenerOrigenSectorConfiguradoCategoriaAgrupado(IdOrigenSector);
                ConfiguracionSector.origenDatoCalidadDetalleAgrupado.NombreCantidadAgrupadoVarDTO = _unitOfWork.OrigenDatoCalidadDetalleRepository.ObtenerNombreOrigenDatoCalidadDetalleAgrupado(IdOrigenSector);
                return ConfiguracionSector;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
