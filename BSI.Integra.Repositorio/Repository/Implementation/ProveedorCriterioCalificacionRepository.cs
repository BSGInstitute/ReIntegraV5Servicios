using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProveedorCriterioCalificacionRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_ProveedorCriterioCalificacion
    /// </summary>
    public class ProveedorCriterioCalificacionRepository : GenericRepository<TProveedorCriterioCalificacion>, IProveedorCriterioCalificacionRepository
    {
        private Mapper _mapper;

        public ProveedorCriterioCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProveedorCriterioCalificacion, ProveedorCriterioCalificacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProveedorCriterioCalificacion MapeoEntidad(ProveedorCriterioCalificacion entidad)
        {
            try
            {
                //crea la entidad padre
                TProveedorCriterioCalificacion modelo = _mapper.Map<TProveedorCriterioCalificacion>(entidad);

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

        public TProveedorCriterioCalificacion Add(ProveedorCriterioCalificacion entidad)
        {
            try
            {
                var ProveedorCriterioCalificacion = MapeoEntidad(entidad);
                base.Insert(ProveedorCriterioCalificacion);
                return ProveedorCriterioCalificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProveedorCriterioCalificacion Update(ProveedorCriterioCalificacion entidad)
        {
            try
            {
                var ProveedorCriterioCalificacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProveedorCriterioCalificacion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProveedorCriterioCalificacion);
                return ProveedorCriterioCalificacion;
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


        public IEnumerable<TProveedorCriterioCalificacion> Add(IEnumerable<ProveedorCriterioCalificacion> listadoEntidad)
        {
            try
            {
                List<TProveedorCriterioCalificacion> listado = new List<TProveedorCriterioCalificacion>();
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

        public IEnumerable<TProveedorCriterioCalificacion> Update(IEnumerable<ProveedorCriterioCalificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProveedorCriterioCalificacion> listado = new List<TProveedorCriterioCalificacion>();
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
