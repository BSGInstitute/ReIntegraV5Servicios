using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionDocenteDisparadorReglaTiempoFijoRepository : GenericRepository<TGestionDocenteDisparadorReglaTiempoFijo>, IGestionDocenteDisparadorReglaTiempoFijoRepository
    {
        private Mapper _mapper;
        private readonly IntegraDBContext _dbContext;

        public GestionDocenteDisparadorReglaTiempoFijoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            _dbContext = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionDocenteDisparadorReglaTiempoFijo, TGestionDocenteDisparadorReglaTiempoFijo>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionDocenteDisparadorReglaTiempoFijo Add(GestionDocenteDisparadorReglaTiempoFijo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteDisparadorReglaTiempoFijo>(entidad);
                base.Insert(model);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionDocenteDisparadorReglaTiempoFijo Update(GestionDocenteDisparadorReglaTiempoFijo entidad)
        {
            try
            {
                var model = _mapper.Map<TGestionDocenteDisparadorReglaTiempoFijo>(entidad);
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

        public int ObtenerIdReglaTiempoPorTipo(string tipoRegla)
        {
            var regla = _dbContext.TGestionDocenteDisparadorReglaTiempos.First(x => x.TipoRegla == tipoRegla);
            return regla.Id;
        }
    }
}
