using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProductoPresentacionRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_ProductoPresentacion
    /// </summary>
    public class ProductoPresentacionRepository : GenericRepository<TProductoPresentacion>, IProductoPresentacionRepository
    {
        private Mapper _mapper;

        public ProductoPresentacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProductoPresentacion, ProductoPresentacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProductoPresentacion MapeoEntidad(ProductoPresentacion entidad)
        {
            try
            {
                //crea la entidad padre
                TProductoPresentacion modelo = _mapper.Map<TProductoPresentacion>(entidad);

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

        public TProductoPresentacion Add(ProductoPresentacion entidad)
        {
            try
            {
                var ProductoPresentacion = MapeoEntidad(entidad);
                base.Insert(ProductoPresentacion);
                return ProductoPresentacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProductoPresentacion Update(ProductoPresentacion entidad)
        {
            try
            {
                var ProductoPresentacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProductoPresentacion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProductoPresentacion);
                return ProductoPresentacion;
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


        public IEnumerable<TProductoPresentacion> Add(IEnumerable<ProductoPresentacion> listadoEntidad)
        {
            try
            {
                List<TProductoPresentacion> listado = new List<TProductoPresentacion>();
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

        public IEnumerable<TProductoPresentacion> Update(IEnumerable<ProductoPresentacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProductoPresentacion> listado = new List<TProductoPresentacion>();
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
        /// Obtiene registros de T_ProductoPresentacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProductoPresentacionComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM fin.T_ProductoPresentacion WHERE Estado = 1;";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabia
        /// Fecha: 19/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProductoPresentacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProductoPresentacionComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM fin.T_ProductoPresentacion WHERE Estado = 1;";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerComboAsync(): {ex.Message}", ex);
            }
        }
    }
}
