using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteDisparadorReglaTiempoRelativoRepository : GenericRepository<TGestionDocenteDisparadorReglaTiempoRelativo>, IGestionDocenteDisparadorReglaTiempoRelativoRepository
    {
        private Mapper _mapper;

        public GestionDocenteDisparadorReglaTiempoRelativoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteDisparadorReglaTiempoRelativo, TGestionDocenteDisparadorReglaTiempoRelativo>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteDisparadorReglaTiempoRelativo Add(GestionDocenteDisparadorReglaTiempoRelativo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteDisparadorReglaTiempoRelativo>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteDisparadorReglaTiempoRelativo Update(GestionDocenteDisparadorReglaTiempoRelativo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteDisparadorReglaTiempoRelativo>(entidad);
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
