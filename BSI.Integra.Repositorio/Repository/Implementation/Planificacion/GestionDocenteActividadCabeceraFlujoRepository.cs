using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteActividadCabeceraFlujoRepository : GenericRepository<TGestionDocenteActividadCabeceraFlujo>, IGestionDocenteActividadCabeceraFlujoRepository
    {
        private Mapper _mapper;

        public GestionDocenteActividadCabeceraFlujoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteActividadCabeceraFlujo, TGestionDocenteActividadCabeceraFlujo>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteActividadCabeceraFlujo Add(GestionDocenteActividadCabeceraFlujo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadCabeceraFlujo>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteActividadCabeceraFlujo Update(GestionDocenteActividadCabeceraFlujo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteActividadCabeceraFlujo>(entidad);
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

        public new bool Delete(int id, string usuario)
        {
            try
            {
                return base.Delete(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
