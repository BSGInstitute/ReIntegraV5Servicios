using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionContactoActividadDetalleSesionRepository : GenericRepository<TGestionContactoActividadDetalleSesion>, IGestionContactoActividadDetalleSesionRepository
    {
        private Mapper _mapper;

        public GestionContactoActividadDetalleSesionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionContactoActividadDetalleSesion, TGestionContactoActividadDetalleSesion>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionContactoActividadDetalleSesion Add(GestionContactoActividadDetalleSesion entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionContactoActividadDetalleSesion>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
