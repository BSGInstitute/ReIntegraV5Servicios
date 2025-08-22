using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FurFaseAprobacionRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_FurFaseAprobacion
    /// </summary>
    public class FurFaseAprobacionRepository : GenericRepository<TFurFaseAprobacion>, IFurFaseAprobacionRepository
    {
        private Mapper _mapper;

        public FurFaseAprobacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFurFaseAprobacion, FurFaseAprobacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TFurFaseAprobacion MapeoEntidad(FurFaseAprobacion entidad)
        {
            try
            {
                //crea la entidad padre
                TFurFaseAprobacion modelo = _mapper.Map<TFurFaseAprobacion>(entidad);

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

        public TFurFaseAprobacion Add(FurFaseAprobacion entidad)
        {
            try
            {
                var FurFaseAprobacion = MapeoEntidad(entidad);
                base.Insert(FurFaseAprobacion);
                return FurFaseAprobacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurFaseAprobacion Update(FurFaseAprobacion entidad)
        {
            try
            {
                var FurFaseAprobacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FurFaseAprobacion.RowVersion = entidadExistente.RowVersion;

                base.Update(FurFaseAprobacion);
                return FurFaseAprobacion;
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


        public IEnumerable<TFurFaseAprobacion> Add(IEnumerable<FurFaseAprobacion> listadoEntidad)
        {
            try
            {
                List<TFurFaseAprobacion> listado = new List<TFurFaseAprobacion>();
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

        public IEnumerable<TFurFaseAprobacion> Update(IEnumerable<FurFaseAprobacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFurFaseAprobacion> listado = new List<TFurFaseAprobacion>();
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

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FurFaseAprobacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<FurFaseAprobacionComboDTO> </returns>
        public IEnumerable<Object> ObtenerCombo()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new { Id = x.Id, x.Nombre }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
