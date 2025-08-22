using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ConfiguracionPeriodoMatriculaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionPeriodoMatricula
    /// </summary>
    public class ConfiguracionPeriodoMatriculaService : IConfiguracionPeriodoMatriculaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionPeriodoMatriculaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionPeriodoMatricula, ConfiguracionPeriodoMatricula>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionPeriodoMatricula, ConfiguracionPeriodoMatriculaRecibidoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public ConfiguracionPeriodoMatricula Add(ConfiguracionPeriodoMatriculaRecibidoDTO data,string Usuario)
        {
            try
            {
                ConfiguracionPeriodoMatricula entidad = _mapper.Map<ConfiguracionPeriodoMatricula>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.ConfiguracionPeriodoMatriculaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionPeriodoMatricula>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionPeriodoMatricula Update(ConfiguracionPeriodoMatriculaRecibidoDTO data, string Usuario)
        {
            try
            {
                var repositorioConfiguracionPeriodoMatricula = _unitOfWork.ConfiguracionPeriodoMatriculaRepository;
                var entidad = _mapper.Map<ConfiguracionPeriodoMatricula>(repositorioConfiguracionPeriodoMatricula.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.FechaInicio = data.FechaInicio;
                entidad.FechaFin = data.FechaFin;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.ConfiguracionPeriodoMatriculaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionPeriodoMatricula>(modelo);
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
                _unitOfWork.ConfiguracionPeriodoMatriculaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionPeriodoMatricula> Add(List<ConfiguracionPeriodoMatricula> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionPeriodoMatriculaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionPeriodoMatricula>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionPeriodoMatricula> Update(List<ConfiguracionPeriodoMatricula> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionPeriodoMatriculaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionPeriodoMatricula>>(modelo);
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
                _unitOfWork.ConfiguracionPeriodoMatriculaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConfiguracionPeriodoMatricula.
        /// </summary>
        /// <returns> List<ConfiguracionPeriodoMatriculaDTO> </returns>
        public IEnumerable<ConfiguracionPeriodoMatriculaRecibidoDTO> ObtenerConfiguracionPeriodoMatricula()
        {
            try
            {
                return _unitOfWork.ConfiguracionPeriodoMatriculaRepository.ObtenerConfiguracionPeriodoMatricula();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
