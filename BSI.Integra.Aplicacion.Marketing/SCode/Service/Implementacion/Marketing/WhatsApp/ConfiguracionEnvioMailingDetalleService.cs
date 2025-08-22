using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using PdfSharp.Pdf.Filters;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{

    public class ConfiguracionEnvioMailingDetalleService : IConfiguracionEnvioMailingDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper _mapperActividad;

        public ConfiguracionEnvioMailingDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<ActividadDetalleDTO, ActividadDetalle>(MemberList.None);
            });
            _mapper = new Mapper(config);
            _mapperActividad = new Mapper(config);
        }

        public IEnumerable<ConfiguracionEnvioMailingDetalle> GetBy(Expression<Func<TConfiguracionEnvioMailingDetalle, bool>> filter)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.GetBy(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionEnvioMailingDetalle FirstById(int id)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.FirstById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionEnvioMailingDetalle FirstBy(Expression<Func<TConfiguracionEnvioMailingDetalle, bool>> filter)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.FirstBy(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(ConfiguracionEnvioMailingDetalle objetoBO)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.Insert(objetoBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(IEnumerable<ConfiguracionEnvioMailingDetalle> listadoBO)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.Insert(listadoBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(ConfiguracionEnvioMailingDetalle objetoBO)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.Update(objetoBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(IEnumerable<ConfiguracionEnvioMailingDetalle> listadoBO)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.Update(listadoBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignacionId(TConfiguracionEnvioMailingDetalle entidad, ConfiguracionEnvioMailingDetalle objetoBO)
        {
            try
            {
                _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.AsignacionId(entidad, objetoBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionEnvioMailingDetalle MapeoEntidad(ConfiguracionEnvioMailingDetalle objetoBO)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.MapeoEntidad(objetoBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ConfiguracionEnvioMailingDetalle> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionEnvioMailingDetalle, bool>>> filters, Expression<Func<TConfiguracionEnvioMailingDetalle, KProperty>> orderBy, bool ascending)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.GetFiltered(filters,orderBy, ascending);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ListaAlumnoMailingDTO> ObtenerRegistrosParaEnvioPersonalizado(int IdMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.ObtenerRegistrosParaEnvioPersonalizado(IdMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerContenidoPlantilla(int IdPlantilla)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.ObtenerContenidoPlantilla(IdPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertarConfiguracionEnvioMailingDetalle(List<ConfiguracionEnvioMailingDetalle> listaConfiguracionEnvioMailingDetalle)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.InsertarConfiguracionEnvioMailingDetalle(listaConfiguracionEnvioMailingDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExisteConfiguracionEnvioMailingDetalle(int idConfiguracionEnvioMailingDetalle)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.ExisteConfiguracionEnvioMailingDetalle(idConfiguracionEnvioMailingDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionEnvioMailingDetalle BuscaConfiguracionEnvioMailingDetallePorId(int idConfiguracionEnvioMailingDetalle)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.BuscaConfiguracionEnvioMailingDetallePorId(idConfiguracionEnvioMailingDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionEnvioMailingDetalle> BuscaConfiguracionEnvioMailingDetallePorIdConfiguracionEnvioMailing(int idConfiguracionEnvioMailing, bool enviadoCorrectamente)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.BuscaConfiguracionEnvioMailingDetallePorIdConfiguracionEnvioMailing(idConfiguracionEnvioMailing, enviadoCorrectamente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarConfiguracionEnvioMailingDetalle(ConfiguracionEnvioMailingDetalle filtro)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingDetalleRepository.ActualizarConfiguracionEnvioMailingDetalle(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
