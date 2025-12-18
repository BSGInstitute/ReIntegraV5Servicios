using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ActividadDetalleGestionContactoRepository : GenericRepository<TActividadDetalleGestionContacto>, IActividadDetalleGestionContactoRepository
    {
        private Mapper _mapper;

        public ActividadDetalleGestionContactoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ActividadDetalleGestionContacto, TActividadDetalleGestionContacto>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TActividadDetalleGestionContacto AddAsync(ActividadDetalleGestionContacto entidad)
        {
            try
            {
                var tActividad = MapeoEntidad(entidad);
                base.InsertAsync(tActividad);
                return tActividad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TActividadDetalleGestionContacto MapeoEntidad(ActividadDetalleGestionContacto entidad)
        {
            try
            {
                TActividadDetalleGestionContacto modelo = _mapper.Map<TActividadDetalleGestionContacto>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
