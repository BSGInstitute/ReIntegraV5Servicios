using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: InteraccionRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 24/08/2022
    /// <summary>
    /// Gestión general de T_Interaccion
    /// </summary>
    internal class InteraccionRepository : GenericRepository<TInteraccion>, IInteraccionRepository
    {
        private Mapper _mapper;
        public InteraccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TInteraccion, Interaccion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        private TInteraccion MapeoEntidad(Interaccion entidad)
        {
            try
            {
                //crea la entidad padre
                TInteraccion modelo = _mapper.Map<TInteraccion>(entidad);

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
        public TInteraccion Add(Interaccion entidad)
        {
            try
            {
                var Interaccion = MapeoEntidad(entidad);
                base.Insert(Interaccion);
                return Interaccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TInteraccion> Add(IEnumerable<Interaccion> listadoEntidad)
        {
            try
            {
                List<TInteraccion> listado = new List<TInteraccion>();
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

        public TInteraccion Update(Interaccion entidad)
        {
            try
            {
                var TInteraccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TInteraccion.RowVersion = entidadExistente.RowVersion;

                base.Update(TInteraccion);
                return TInteraccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TInteraccion> Update(IEnumerable<Interaccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TInteraccion> listado = new List<TInteraccion>();
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
    }
}
