using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: HistoricoProductoProveedorRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 13/07/2022
    /// <summary>
    /// Gestión general de T_HistoricoProductoProveedor
    /// </summary>
    public class HistoricoProductoProveedorRepository : GenericRepository<THistoricoProductoProveedor>, IHistoricoProductoProveedorRepository
    {
        private Mapper _mapper;

        public HistoricoProductoProveedorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<THistoricoProductoProveedor, HistoricoProductoProveedor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private THistoricoProductoProveedor MapeoEntidad(HistoricoProductoProveedor entidad)
        {
            try
            {
                //crea la entidad padre
                THistoricoProductoProveedor modelo = _mapper.Map<THistoricoProductoProveedor>(entidad);

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

        public THistoricoProductoProveedor Add(HistoricoProductoProveedor entidad)
        {
            try
            {
                var HistoricoProductoProveedor = MapeoEntidad(entidad);
                base.Insert(HistoricoProductoProveedor);
                return HistoricoProductoProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public THistoricoProductoProveedor Update(HistoricoProductoProveedor entidad)
        {
            try
            {

                var HistoricoProductoProveedor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                HistoricoProductoProveedor.RowVersion = entidadExistente.RowVersion;

                base.Update(HistoricoProductoProveedor);
                return HistoricoProductoProveedor;
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


        public IEnumerable<THistoricoProductoProveedor> Add(IEnumerable<HistoricoProductoProveedor> listadoEntidad)
        {
            try
            {
                List<THistoricoProductoProveedor> listado = new List<THistoricoProductoProveedor>();
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

        public IEnumerable<THistoricoProductoProveedor> Update(IEnumerable<HistoricoProductoProveedor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<THistoricoProductoProveedor> listado = new List<THistoricoProductoProveedor>();
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
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de FIN.V_ObtenerProductosPrecioHistorico o solo el que conincida con IdHistorico.
        /// </summary>
        /// <returns> List<HistoricoProductoProveedorDTO> </returns>
        /// <paramref name="IdHistoricoPP"/> Identidificador de FIN.V_ObtenerProductosPrecioHistorico.
        public IEnumerable<HistoricoProductoProveedorVersionDTO> ObtenerHistoricoUltimaVersion(int? IdHistoricoPP)
        {
            try
            {
                int? idHistorico = IdHistoricoPP;
                List<HistoricoProductoProveedorVersionDTO> rpta = new List<HistoricoProductoProveedorVersionDTO>();
                var _query = "";
                if (idHistorico != null && idHistorico != 0)
                {
                    _query = "SELECT Id,Producto,IdProducto,Proveedor,IdProveedor,IdCondicionPago,CondicionPago,Moneda,IdMoneda,Precio,IdTipoPago,TipoPago,Observaciones,UsuarioModificacion,FechaModificacion,Estado FROM FIN.V_ObtenerProductosPrecioHistorico where Estado = 1 and Id=@idHistorico ORDER BY Id desc";
                }
                else
                {
                    _query = "SELECT Id,Producto,IdProducto,Proveedor,IdProveedor,IdCondicionPago,CondicionPago,Moneda,IdMoneda,Precio,IdTipoPago,TipoPago,Observaciones,UsuarioModificacion,FechaModificacion,Estado FROM FIN.V_ObtenerProductosPrecioHistorico where Estado = 1 ORDER BY Id desc";
                }
                var resultado = _dapperRepository.QueryDapper(_query, new { idHistorico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<HistoricoProductoProveedorVersionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de FIN.V_ObtenerNombreHistoricoPP que contengan el valor de la variable "valor".
        /// para el llenado de un combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        /// <paramref name="valor"/> Identidificador de FIN.V_ObtenerProductosPrecioHistorico.
        public IEnumerable<ComboDTO> ObtenerNombreHistoricoAutocomplete()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var _query = string.Empty;
                _query = "SELECT Id ,Nombre FROM FIN.V_ObtenerNombreHistoricoPP ORDER By Nombre ASC";

                var resultado = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de fin.SP_ObtenerDetalleSesionesFUR, segun los parametros de IdProducto,idProveedor .
        /// </summary>
        /// <returns> List<DetalleFurDTO> </returns>
        /// <paramref name="idProducto"/> Identidificador de Producto.
        /// <paramref name="idProveedor"/> Identidificador de Proveedor.
        public DetalleFurDTO? ObtenerDetalleFUR(int idProducto, int idProveedor)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPFirstOrDefault("fin.SP_ObtenerDetalleSesionesFUR", new { IdProducto = idProducto, IdProveedor = idProveedor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<DetalleFurDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de productos con su precio asociados a un proveedor para (Utilizado para llenado de ComboBox en modulo CostosFijos)
        /// </summary>
        /// <returns> List<ProductoPorProveedorDTO> </returns>.
        /// <paramref name="IdProveedor"/> Identidificador de Proveedor.
        public ICollection<ProductoPorProveedorDTO> ObtenerListaProductoPorProveedor(int IdProveedor)
        {
            try
            {
                List<ProductoPorProveedorDTO> productos = new List<ProductoPorProveedorDTO>();
                var _query = string.Empty;
                _query = "SELECT Id as IdHistoricoProveedorProducto, IdProducto As Id, Nombre, Precio, IdMoneda FROM fin.V_ObtenerProductoPorProveedor WHERE IdProveedor=" + IdProveedor;
                var productosDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(productosDB) && !productosDB.Contains("[]"))
                {
                    productos = JsonConvert.DeserializeObject<List<ProductoPorProveedorDTO>>(productosDB);
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de productos conn ultima version asociados a un proveedor
        /// </summary>
        /// <returns> List<ProductoPorProveedorDTO> </returns>.
        /// <paramref name="IdProveedor"/> Identidificador de Proveedor.
        public IEnumerable<ProductoPorProveedorUltimaVersionDTO> ObtenerListaProductoPorProveedorUltimaVersion()
        {
            try
            {
                List<ProductoPorProveedorUltimaVersionDTO> productos = new List<ProductoPorProveedorUltimaVersionDTO>();
                var _query = string.Empty;
                _query = @"SELECT [IdHistoricoProductoProveedor]
                                  ,[IdProveedor]
                                  ,[NroDocumento]
                                  ,[RazonSocial]
                                  ,[IdProducto]
                                  ,[NombreProducto]
                                  ,[Precio]
                                  ,[IdMoneda]
                           FROM [fin].[V_ObtenerProductoPorProveedorV5]";
                var productosDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(productosDB) && !productosDB.Contains("[]"))
                {
                    productos = JsonConvert.DeserializeObject<List<ProductoPorProveedorUltimaVersionDTO>>(productosDB);
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 13/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de productos conn ultima version asociados a un proveedor
        /// </summary>
        /// <returns> List<ProductoPorProveedorDTO> </returns>.
        /// <paramref name="IdProveedor"/> Identidificador de Proveedor.
        public DetalleHistoricoFurDTO ObtenerDetalleHistoricoProyeccionById(int id)
        {
            try
            {
                DetalleHistoricoFurDTO productos = new DetalleHistoricoFurDTO();
                var _query = string.Empty;
                _query = @"SELECT [Id]
                                  ,[NumeroCuenta]
                                  ,[CuentaDescripcion]
                                  ,[IdProductoPresentacion]
                                  ,[PrecioOrigen]
                                  ,[PrecioDolares]
                                  ,[Precio]
                                  ,[IdMoneda]
                                  ,[IdProducto]
                                  ,[IdProveedor]
                            FROM [fin].[V_ObtenerDetalleProyeccionFur] 
                            WHERE Id=@Id";
                var productosDB = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(productosDB))
                {
                    productos = JsonConvert.DeserializeObject<DetalleHistoricoFurDTO>(productosDB);
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ///// Autor: Griselberto Huaman.
        ///// Fecha: 13/07/2022
        ///// Version: 1.0
        ///// <summary>
        ///// Obtiene lista de productos conn ultima version asociados a un proveedor
        ///// </summary>
        ///// <returns> List<ProductoPorProveedorDTO> </returns>.
        ///// <paramref name="IdProveedor"/> Identidificador de Proveedor.
        //public bool ValidarHistoricoExiste(int idHistorico)
        //{
        //    try
        //    {
        //        List<ValorIntDTO> historicos = new List<ValorIntDTO>();
        //        var _query = string.Empty;
        //        _query = @" SELECT Id 
        //                    FROM fin.T_HistoricoProductoProveedor 
        //                    WHERE Estado=0 AND Id  in (select item from conf.F_Splitstring(@IdHistoricos,','))";
        //        var productosDB = _dapperRepository.QueryDapper(_query, new { IdHistoricos = idHistoricos });
        //        if (!string.IsNullOrEmpty(productosDB))
        //        {
        //            historicos = JsonConvert.DeserializeObject<List<ValorIntDTO>>(productosDB);
        //        }
        //        return historicos;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

    }
}
