using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class ActividadCrepLogRepository : GenericRepository<TActividadCrepLog>, IActividadCrepLogRepository
    {
        private Mapper _mapper;

        public ActividadCrepLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TActividadCrepLog, ActividadCrepLog>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TActividadCrepLog MapeoEntidad(ActividadCrepLog entidad)
        {
            try
            {
                //crea la entidad padre
                TActividadCrepLog modelo = _mapper.Map<TActividadCrepLog>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TActividadCrepLog Add(ActividadCrepLog entidad)
        {
            try
            {
                var ActividadCrepLog = MapeoEntidad(entidad);
                base.Insert(ActividadCrepLog);
                return ActividadCrepLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TActividadCrepLog Update(ActividadCrepLog entidad)
        {
            try
            {
                var ActividadCrepLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ActividadCrepLog.RowVersion = entidadExistente.RowVersion;

                base.Update(ActividadCrepLog);
                return ActividadCrepLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TActividadCrepLog> Add(IEnumerable<ActividadCrepLog> listadoEntidad)
        {
            try
            {
                List<TActividadCrepLog> listado = new List<TActividadCrepLog>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TActividadCrepLog> Update(IEnumerable<ActividadCrepLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TActividadCrepLog> listado = new List<TActividadCrepLog>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
