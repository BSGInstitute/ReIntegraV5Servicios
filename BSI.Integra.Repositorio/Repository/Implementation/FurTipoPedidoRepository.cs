using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FurTipoPedidoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_FurTipoPedido
    /// </summary>
    public class FurTipoPedidoRepository : GenericRepository<TFurTipoPedido>, IFurTipoPedidoRepository
    {
        private Mapper _mapper;

        public FurTipoPedidoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFurTipoPedido, FurTipoPedido>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TFurTipoPedido MapeoEntidad(FurTipoPedido entidad)
        {
            try
            {
                //crea la entidad padre
                TFurTipoPedido modelo = _mapper.Map<TFurTipoPedido>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurTipoPedido Add(FurTipoPedido entidad)
        {
            try
            {
                var FurTipoPedido = MapeoEntidad(entidad);
                base.Insert(FurTipoPedido);
                return FurTipoPedido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurTipoPedido Update(FurTipoPedido entidad)
        {
            try
            {
                var FurTipoPedido = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FurTipoPedido.RowVersion = entidadExistente.RowVersion;

                base.Update(FurTipoPedido);
                return FurTipoPedido;
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


        public IEnumerable<TFurTipoPedido> Add(IEnumerable<FurTipoPedido> listadoEntidad)
        {
            try
            {
                List<TFurTipoPedido> listado = new List<TFurTipoPedido>();
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

        public IEnumerable<TFurTipoPedido> Update(IEnumerable<FurTipoPedido> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFurTipoPedido> listado = new List<TFurTipoPedido>();
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
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene El combo de TFurTipoPedido.
        /// </summary>
        /// <returns> IEnumerable<object> </returns>
        public IEnumerable<object> ObtenerTipoPedidoFur()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new { Id = x.Id, x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
