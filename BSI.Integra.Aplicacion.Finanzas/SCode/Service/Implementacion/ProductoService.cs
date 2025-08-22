using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ProductoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 27/06/2022
    /// <summary>
    /// Gestión general de T_Producto
    /// </summary>
    public class ProductoService : IProductoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProductoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProducto, Producto>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProductoDatosDTO, Producto>(MemberList.None).ReverseMap();
            }
          );

            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Producto Add(ProductoDatosDTO entidad, string Usuario)
        {
            try
            {
                Producto data = _mapper.Map<Producto>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.ProductoRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<Producto>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Producto InsertarProducto(Producto entidad)
        {
            try
            {

                var modelo = _unitOfWork.ProductoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Producto>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Producto Update(ProductoDatosDTO entidad, string Usuario)
        {
            try
            {
                var rep = _unitOfWork.ProductoRepository;
                var entidadActual = _mapper.Map<Producto>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.Descripcion = entidad.Descripcion;
                entidadActual.CuentaGeneral = entidad.CuentaGeneral;
                entidadActual.CuentaGeneralCodigo = entidad.CuentaGeneralCodigo;
                entidadActual.CuentaEspecifica = entidad.CuentaEspecifica;
                entidadActual.CuentaEspecificaCodigo = entidad.CuentaEspecificaCodigo;
                entidadActual.IdProductoPresentacion = entidad.IdProductoPresentacion;

                var modelo = _unitOfWork.ProductoRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<Producto>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Producto ActualizarProducto(Producto entidad)
        {
            try
            {


                var modelo = _unitOfWork.ProductoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Producto>(modelo);
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
                _unitOfWork.ProductoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Producto> Add(List<Producto> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProductoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Producto>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Producto> Update(List<Producto> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProductoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Producto>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.ProductoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Producto
        /// </summary>
        /// <returns> List<ProductoDTO> </returns>
        public IEnumerable<ProductoDTO> ObtenerProducto()
        {
            try
            {
                return _unitOfWork.ProductoRepository.ObtenerProducto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Producto para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProductoRepository.ObtenerCombo();
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
                return _unitOfWork.ProductoRepository.ObtenerProductoCuentaContable(idProducto);
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
                return _unitOfWork.ProductoRepository.ObtenerProductoAutocomplete(nombre);
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
                return _unitOfWork.ProductoRepository.ObtenerDetalleHistorio(idProducto, idProveedor);
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
                return _unitOfWork.ProductoRepository.ObtenerListaProductoMaterialesParaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
