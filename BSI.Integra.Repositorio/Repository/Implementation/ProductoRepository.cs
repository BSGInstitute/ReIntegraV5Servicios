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
    /// Repositorio: ProductoRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 05/07/2022
    /// <summary>
    /// Gestión general de T_Producto
    /// </summary>
    public class ProductoRepository : GenericRepository<TProducto>, IProductoRepository
    {
        private Mapper _mapper;

        public ProductoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProducto, Producto>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TProducto MapeoEntidad(Producto entidad)
        {
            try
            {
                //crea la entidad padre
                TProducto modelo = _mapper.Map<TProducto>(entidad);

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

        public TProducto Add(Producto entidad)
        {
            try
            {
                var Producto = MapeoEntidad(entidad);
                base.Insert(Producto);
                return Producto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProducto Update(Producto entidad)
        {
            try
            {
                var Producto = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Producto.RowVersion = entidadExistente.RowVersion;

                base.Update(Producto);
                return Producto;
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


        public IEnumerable<TProducto> Add(IEnumerable<Producto> listadoEntidad)
        {
            try
            {
                List<TProducto> listado = new List<TProducto>();
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

        public IEnumerable<TProducto> Update(IEnumerable<Producto> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProducto> listado = new List<TProducto>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 09/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de la tabla T_Producto por medio del Id
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Entidad - Producto </returns>
        public Producto? ObtenerPorId(int id)
        {
            try
            {
                PGeneral rpta = new();
                var query = @"
                    SELECT 
                        Id,
                        Nombre,
                        Descripcion,
                        CuentaGeneral,
                        CuentaGeneralCodigo,
                        CuentaEspecifica,
                        CuentaEspecificaCodigo,
                        IdProductoPresentacion,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        fin.T_Producto 
                    WHERE 
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Producto>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 05/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Producto para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM fin.T_Producto WHERE Estado=1";
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Producto para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM fin.T_Producto WHERE Estado=1";
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
        /// Autor: Griselberto Huaman
        /// Fecha: 05/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Producto
        /// </summary>
        /// <returns> List<ProductoDTO> </returns>
        public IEnumerable<ProductoDTO> ObtenerProducto()
        {
            try
            {
                List<ProductoDTO> rpta = new List<ProductoDTO>();
                var query = @"
                SELECT [Id]
                      ,[Nombre]
                      ,[Descripcion]
                      ,[CuentaGeneral]
                      ,[CuentaGeneralCodigo]
                      ,[CuentaEspecifica]
                      ,[CuentaEspecificaCodigo]
                      ,[IdProductoPresentacion]
                      ,[UsuarioModificacion]
                FROM [fin].[T_Producto]
                WHERE [Estado] = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProductoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 05/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos un solo registro de FIN.V_TProductoCuentaContable, que coincida con el IdProducto.
        /// </summary>
        /// <returns> List<ProductoDTO> </returns>
        /// <param name="idProducto">Identificador del prodcuto</param>
        public IEnumerable<ProductoCuentaContableDTO> ObtenerProductoCuentaContable(int idProducto)
        {
            try
            {
                List<ProductoCuentaContableDTO> rpta = new List<ProductoCuentaContableDTO>();
                var query = @"
                    SELECT 
                        IdProducto,
                        NombreProducto,
                        DescripcionProducto,
                        CuentaEspecifica,
                        IdProductoPresentacion
                    FROM FIN.V_TProductoCuentaContable 
                    where IdProducto=@idProducto order by IdProducto desc";
                var resultado = _dapperRepository.QueryDapper(query, new { idProducto });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProductoCuentaContableDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 05/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de fin.V_AutoCompleteProducto que 
        /// contegan el valor de la variable "nombre"
        /// </summary>
        /// <returns> List<ProductoDTO> </returns>
        /// <param name="nombre">Nombre del producto</param>
        public IEnumerable<ComboDTO> ObtenerProductoAutocomplete(string nombre)
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                    SELECT  
                        Id, 
                        Nombre  
                    FROM fin.V_AutoCompleteProducto 
                    WHERE Nombre LIKE CONCAT('%',@nombre,'%') AND Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, new { nombre });
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

        /// Autor: Griselberto Huaman
        /// Fecha: 05/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de pla.SP_ObtenerDetalleHistorico que coincidan con
        /// el valord e la variable "idProducto" / "idProveedor"
        /// </summary>
        /// <returns> List<ProductoDTO> </returns>
        /// <param name="idProducto">identificador del producto</param>
        /// <param name="idProveedor">identificador del proveedor</param>
        public IEnumerable<ComboDTO> ObtenerDetalleHistorio(int idProducto, int idProveedor)
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var resultado = _dapperRepository.QuerySPDapper("pla.SP_ObtenerDetalleHistorico", new { idProducto, idProveedor });
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

        /// Autor: Griselberto Huaman
        /// Fecha: 05/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de productos de materiales para combo de programa especifico FUR
        /// </summary>
        /// <returns> List<ProductoDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerListaProductoMaterialesParaCombo()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        Nombre
                    FROM 
                        fin.T_Producto
                    WHERE 
                        Estado = 1 AND Descripcion = 'MATERIALES DE ENSEÑANZA ALUMNOS' ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PR-OLPMPC-001@Error en ObtenerListaProductoMaterialesParaCombo() {ex.Message}", ex);
            }
        }
    }
}
