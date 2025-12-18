using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionContactoRepository : GenericRepository<TGestionContacto>, IGestionContactoRepository
    {
        private Mapper _mapper;

        public GestionContactoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionContacto, TGestionContacto>().ReverseMap();
                cfg.CreateMap<GestionContactoLog, TGestionContactoLog>().ReverseMap();
                cfg.CreateMap<ActividadDetalleGestionContacto, TActividadDetalleGestionContacto>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionContacto AddAsync(GestionContacto entidad)
        {
            try
            {
                var tGestionContacto = MapeoEntidad(entidad);
                base.InsertAsync(tGestionContacto);
                return tGestionContacto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TGestionContacto MapeoEntidad(GestionContacto entidad)
        {
            try
            {
                TGestionContacto modelo = _mapper.Map<TGestionContacto>(entidad);
                if (entidad.ListaGestionContactoLog != null && entidad.ListaGestionContactoLog.Count > 0)
                {
                    foreach (var logBO in entidad.ListaGestionContactoLog)
                    {
                        var logDB = _mapper.Map<TGestionContactoLog>(logBO);
                        modelo.TGestionContactoLogs.Add(logDB);
                    }
                }
                if (entidad.ListaActividadDetalle != null && entidad.ListaActividadDetalle.Count > 0)
                {
                    foreach (var actividadBO in entidad.ListaActividadDetalle)
                    {
                        var actividadDB = _mapper.Map<TActividadDetalleGestionContacto>(actividadBO);
                        modelo.TActividadDetalleGestionContactos.Add(actividadDB);
                    }
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
