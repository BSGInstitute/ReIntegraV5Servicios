using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProveedorSubCriterioCalificacionRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 08/07/2022
    /// <summary>
    /// Gestión general de T_ProveedorSubCriterioCalificacion
    /// </summary>
    public class ProveedorSubCriterioCalificacionRepository : GenericRepository<TProveedorSubCriterioCalificacion>, IProveedorSubCriterioCalificacionRepository
    {
        private Mapper _mapper;

        public ProveedorSubCriterioCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProveedorSubCriterioCalificacion, ProveedorSubCriterioCalificacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TProveedorSubCriterioCalificacion MapeoEntidad(ProveedorSubCriterioCalificacion entidad)
        {
            try
            {
                //crea la entidad padre
                TProveedorSubCriterioCalificacion modelo = _mapper.Map<TProveedorSubCriterioCalificacion>(entidad);

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

        public TProveedorSubCriterioCalificacion Add(ProveedorSubCriterioCalificacion entidad)
        {
            try
            {
                var ProveedorSubCriterioCalificacion = MapeoEntidad(entidad);
                base.Insert(ProveedorSubCriterioCalificacion);
                return ProveedorSubCriterioCalificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProveedorSubCriterioCalificacion Update(ProveedorSubCriterioCalificacion entidad)
        {
            try
            {
                var ProveedorSubCriterioCalificacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProveedorSubCriterioCalificacion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProveedorSubCriterioCalificacion);
                return ProveedorSubCriterioCalificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool Delete(int id, string usuario)
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


        public IEnumerable<TProveedorSubCriterioCalificacion> Add(IEnumerable<ProveedorSubCriterioCalificacion> listadoEntidad)
        {
            try
            {
                List<TProveedorSubCriterioCalificacion> listado = new List<TProveedorSubCriterioCalificacion>();
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

        public IEnumerable<TProveedorSubCriterioCalificacion> Update(IEnumerable<ProveedorSubCriterioCalificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProveedorSubCriterioCalificacion> listado = new List<TProveedorSubCriterioCalificacion>();
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
        /// Autor: Griselberto Huaman
        /// Fecha: 08/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id y idcriterioCalificacion de la tabla ProveedorSubCriterioCalificacion.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public object ObtenerSubCriterioCalificacion()
        {
            try
            {
                var listaSubCriterioProveedor = this.GetBy(x => x.Estado == true, x => new { Id = x.Id, IdCriterioCalificacion = x.IdProveedorCriterioCalificacion, Nombre = x.Nombre + " [" + x.Puntaje + " ptos]", Puntaje = x.Puntaje }).ToList();
                return listaSubCriterioProveedor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
