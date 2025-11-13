using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class CampoFormularioOpcionRepository : GenericRepository<TCampoFormularioOpcion>, ICampoFormularioOpcionRepository
    {
        private Mapper _mapper;

        public CampoFormularioOpcionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
        : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CampoFormularioOpcion, TCampoFormularioOpcion>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TCampoFormularioOpcion MapeoEntidad(CampoFormularioOpcion entidad)
        {
            try
            {
                return _mapper.Map<TCampoFormularioOpcion>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampoFormularioOpcion Add(CampoFormularioOpcion entidad)
        {
            try
            {
                var entidadMapeada = MapeoEntidad(entidad);
                base.Insert(entidadMapeada);
                return entidadMapeada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TCampoFormularioOpcion> Add(IEnumerable<CampoFormularioOpcion> listadoEntidad)
        {
            try
            {
                var listadoMapeado = _mapper.Map<IEnumerable<TCampoFormularioOpcion>>(listadoEntidad);
                base.Insert(listadoMapeado);
                return listadoMapeado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}