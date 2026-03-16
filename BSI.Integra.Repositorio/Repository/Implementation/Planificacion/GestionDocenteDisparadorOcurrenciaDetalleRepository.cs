using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteDisparadorOcurrenciaDetalleRepository : GenericRepository<TGestionDocenteDisparadorOcurrenciaDetalle>, IGestionDocenteDisparadorOcurrenciaDetalleRepository
    {
        private Mapper _mapper;

        public GestionDocenteDisparadorOcurrenciaDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteDisparadorOcurrenciaDetalle, TGestionDocenteDisparadorOcurrenciaDetalle>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteDisparadorOcurrenciaDetalle Add(GestionDocenteDisparadorOcurrenciaDetalle entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteDisparadorOcurrenciaDetalle>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteDisparadorOcurrenciaDetalle Update(GestionDocenteDisparadorOcurrenciaDetalle entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteDisparadorOcurrenciaDetalle>(entidad);
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
