using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteActividadCabeceraRepository : GenericRepository<TGestionDocenteActividadCabecera>, IGestionDocenteActividadCabeceraRepository
    {
        private Mapper _mapper;

        public GestionDocenteActividadCabeceraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteActividadCabecera, TGestionDocenteActividadCabecera>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteActividadCabecera Add(GestionDocenteActividadCabecera entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadCabecera>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteActividadCabecera Update(GestionDocenteActividadCabecera entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadCabecera>(entidad);
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
