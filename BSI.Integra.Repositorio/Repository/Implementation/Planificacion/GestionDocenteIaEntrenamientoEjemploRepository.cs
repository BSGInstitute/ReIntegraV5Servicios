using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteIaEntrenamientoEjemploRepository : GenericRepository<TGestionDocenteIaEntrenamientoEjemplo>, IGestionDocenteIaEntrenamientoEjemploRepository
    {
        private Mapper _mapper;

        public GestionDocenteIaEntrenamientoEjemploRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteIaEntrenamientoEjemplo, TGestionDocenteIaEntrenamientoEjemplo>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteIaEntrenamientoEjemplo Add(GestionDocenteIaEntrenamientoEjemplo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteIaEntrenamientoEjemplo>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteIaEntrenamientoEjemplo Update(GestionDocenteIaEntrenamientoEjemplo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteIaEntrenamientoEjemplo>(entidad);
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
