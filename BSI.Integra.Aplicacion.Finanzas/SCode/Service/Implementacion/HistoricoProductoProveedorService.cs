using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Org.BouncyCastle.Ocsp;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: HistoricoProductoProveedorService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_HistoricoProductoProveedor
    /// </summary>
    public class HistoricoProductoProveedorService : IHistoricoProductoProveedorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public HistoricoProductoProveedorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<THistoricoProductoProveedor, HistoricoProductoProveedor>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public HistoricoProductoProveedor Add(HistoricoProductoProveedor entidad)
        {
            try
            {
                var modelo = _unitOfWork.HistoricoProductoProveedorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<HistoricoProductoProveedor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HistoricoProductoProveedor Update(ActualizarHistoricoDTO entidad)
        {
            try
            {
                HistoricoProductoProveedor historico = new HistoricoProductoProveedor();
                var _repHistoricoRep = _unitOfWork.HistoricoProductoProveedorRepository;
                historico = _mapper.Map<HistoricoProductoProveedor>(_repHistoricoRep.FirstById(entidad.Id));
               
                historico.IdCondicionPago = entidad.IdCondicionPago;
                historico.IdCondicionTipoPago = entidad.IdTipoPago;
                historico.Observaciones = entidad.Observaciones;
                historico.UsuarioModificacion = entidad.UsuarioModificacion;
                historico.FechaModificacion = DateTime.Now;
             
                var modelo = _unitOfWork.HistoricoProductoProveedorRepository.Update(historico);
                _unitOfWork.Commit();
                return _mapper.Map<HistoricoProductoProveedor>(modelo);
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
                _unitOfWork.HistoricoProductoProveedorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoProductoProveedor> Add(List<HistoricoProductoProveedor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.HistoricoProductoProveedorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<HistoricoProductoProveedor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoProductoProveedor> Update(List<HistoricoProductoProveedor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.HistoricoProductoProveedorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<HistoricoProductoProveedor>>(modelo);
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
                _unitOfWork.HistoricoProductoProveedorRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
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
                return _unitOfWork.HistoricoProductoProveedorRepository.ObtenerHistoricoUltimaVersion(IdHistoricoPP);
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
        public IEnumerable<DTO.ComboDTO> ObtenerNombreHistoricoAutocomplete()
        {
            try
            {
                return _unitOfWork.HistoricoProductoProveedorRepository.ObtenerNombreHistoricoAutocomplete();
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
        public DetalleFurDTO ObtenerDetalleFUR(int idProducto, int idProveedor)
        {
            try
            {
                return _unitOfWork.HistoricoProductoProveedorRepository.ObtenerDetalleFUR(idProducto, idProveedor);
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
        /// Obtiene lista de productos con su precio asociados a un proveedor para (Utilizado para llenado de ComboBox en modulo CostosFijos)
        /// </summary>
        /// <returns> List<ProductoPorProveedorDTO> </returns>.
        /// <paramref name="IdProveedor"/> Identidificador de Proveedor.
        public ICollection<ProductoPorProveedorDTO> ObtenerListaProductoPorProveedor(int IdProveedor)
        {
            try
            {
                return _unitOfWork.HistoricoProductoProveedorRepository.ObtenerListaProductoPorProveedor(IdProveedor);
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
        /// Obtiene lista de productos con su precio asociados a un proveedor .
        /// </summary>
        /// <returns> List<ProductoPorProveedorUltimaVersionDTO> </returns>.
        public IEnumerable<ProductoPorProveedorUltimaVersionDTO> ObtenerListaProductoPorProveedorUltimaVersion()
        {
            try
            {
                return _unitOfWork.HistoricoProductoProveedorRepository.ObtenerListaProductoPorProveedorUltimaVersion();
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
        /// Inserta Producto Historico.
        /// para el llenado de un combo.
        /// </summary>
        /// <returns> Mensaje result </returns>
        /// <param name="objetoHistorico"></param>
        public bool insertarHistoricoProducto(HistoricoProductoProveedorVersionDTO objetoHistorico)
        {

            try
            {
                var _repHistoricoRep = _unitOfWork.HistoricoProductoProveedorRepository;
                var _repTipoCambioMonedaRep = _unitOfWork.TipoCambioMonedumRepository;
                decimal monedaAdolar = 0;
                var tipoCambioMonedaDia = _repTipoCambioMonedaRep.ObtenerPorFecha(objetoHistorico.IdMoneda);
                //var tipoCambioMonedaDia = new { Id=10, MonedaAdolar=3, DolarAmoneda=4 };
                if (tipoCambioMonedaDia != null)
                {
                    monedaAdolar = (decimal)tipoCambioMonedaDia.MonedaAdolar;

                    var objetoVersion = _repHistoricoRep.GetBy(x => x.IdProducto == objetoHistorico.IdProducto && x.IdProveedor == objetoHistorico.IdProveedor, x => new { version = x.Version }).OrderByDescending(y => y.version).FirstOrDefault();
                    int version = 0;

                    if (objetoVersion == null)
                    {
                        version = 0;
                    }
                    else
                    {
                        version = objetoVersion.version + 1;
                    }
                    var existe = 0;//_repHistoricoRep.GetBy(x => x.IdProducto == objetoHistorico.IdProducto && x.IdProveedor == objetoHistorico.IdProveedor && x.Precio == objetoHistorico.Precio && x.IdMoneda == objetoHistorico.IdMoneda && x.Estado == true).ToList();

                    HistoricoProductoProveedor historico = new HistoricoProductoProveedor();
                    if (existe == 0)
                    {
                        historico.IdProducto = objetoHistorico.IdProducto;
                        historico.IdProveedor = objetoHistorico.IdProveedor;
                        //Si la moneda es igual a dolares se tomara el tipo de cambio a soles, para poder pagar en soles y dolares,
                        //Si la moneda es diferente a dolares se podra pagar en la moneda origen y en dolare
                        historico.CostoMonedaOrigen = objetoHistorico.Precio;
                        historico.CostoDolares = objetoHistorico.IdMoneda == 19 ? objetoHistorico.Precio : (objetoHistorico.Precio / monedaAdolar);
                        historico.IdMoneda = objetoHistorico.IdMoneda;
                        historico.Precio = objetoHistorico.Precio;
                        historico.TipoCambio = objetoHistorico.IdMoneda == 19 ? 1 : monedaAdolar;
                        historico.IdCondicionPago = objetoHistorico.IdCondicionPago;
                        historico.IdCondicionTipoPago = objetoHistorico.IdTipoPago;
                        historico.Version = version;
                        historico.Observaciones = objetoHistorico.Observaciones;
                        historico.Estado = true;
                        historico.UsuarioCreacion = objetoHistorico.UsuarioModificacion;
                        historico.UsuarioModificacion = objetoHistorico.UsuarioModificacion;
                        historico.FechaModificacion = DateTime.Now;
                        historico.FechaCreacion = DateTime.Now;
                        _repHistoricoRep.Add(historico);
                        _unitOfWork.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }

}
