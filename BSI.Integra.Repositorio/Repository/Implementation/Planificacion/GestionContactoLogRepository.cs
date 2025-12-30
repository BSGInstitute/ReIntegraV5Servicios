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
    public class GestionContactoLogRepository : GenericRepository<TGestionContactoLog>, IGestionContactoLogRepository
    {
        private Mapper _mapper;

        public GestionContactoLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionContactoLog, TGestionContactoLog>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionContactoLog AddAsync(GestionContactoLog entidad)
        {
            try
            {
                var tLog = MapeoEntidad(entidad);
                base.Insert(tLog);
                return tLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TGestionContactoLog MapeoEntidad(GestionContactoLog entidad)
        {
            try
            {
                TGestionContactoLog modelo = _mapper.Map<TGestionContactoLog>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
