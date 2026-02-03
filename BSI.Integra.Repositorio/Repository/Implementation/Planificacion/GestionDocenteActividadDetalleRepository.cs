using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteActividadDetalleRepository : GenericRepository<TGestionDocenteActividadDetalle>, IGestionDocenteActividadDetalleRepository
    {
        private Mapper _mapper;

        public GestionDocenteActividadDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteActividadDetalle, TGestionDocenteActividadDetalle>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteActividadDetalle Add(GestionDocenteActividadDetalle entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadDetalle>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteActividadDetalle Update(GestionDocenteActividadDetalle entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadDetalle>(entidad);
                var existing = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                if (existing != null) model.RowVersion = existing.RowVersion;
                base.Update(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
