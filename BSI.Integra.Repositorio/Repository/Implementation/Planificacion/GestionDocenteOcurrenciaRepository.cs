using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteOcurrenciaRepository : GenericRepository<TGestionDocenteOcurrencium>, IGestionDocenteOcurrenciaRepository
    {
        private Mapper _mapper;

        public GestionDocenteOcurrenciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteOcurrencia, TGestionDocenteOcurrencium>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteOcurrencium Add(GestionDocenteOcurrencia entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteOcurrencium>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteOcurrencium Update(GestionDocenteOcurrencia entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteOcurrencium>(entidad);
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
