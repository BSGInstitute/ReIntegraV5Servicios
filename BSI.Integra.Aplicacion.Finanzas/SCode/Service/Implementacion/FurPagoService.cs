using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: FurPagoService
    /// Autor Modificacion: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_FurPago
    /// </summary>
    public class FurPagoService : IFurPagoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FurPagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFurPago, FurPago>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFur, Fur>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFurPago, FurPago>(MemberList.None).ReverseMap();
                
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FurPago Add(FurPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.FurPagoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FurPago>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FurPago Update(FurPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.FurPagoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FurPago>(modelo);
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
                _unitOfWork.FurPagoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FurPago> Add(List<FurPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurPagoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FurPago>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FurPago> Update(List<FurPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurPagoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FurPago>>(modelo);
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
                _unitOfWork.FurPagoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor : Griselberto Huaman. 
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve  el mayor numero de pago
        /// </summary>
        /// <param name="idFur"> Identificador del fur</param>
        /// <returns> int </returns>
        public int obtenerNumeroPagoByFur(int idFur)
        {
            try
            {
                return _unitOfWork.FurPagoRepository.obtenerNumeroPagoByFur(idFur);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor : Griselberto Huaman. 
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene todos la lista de furpagos relacionado a un fur.
        /// </summary>
        /// <param name="data">Grupo de Parametros </param>
        /// <returns> IEnumerable<FurPagoDTO></returns>
        public IEnumerable<FurPagoDTO> BuscarListaFurPagos(FiltroFurPagoDTO data)
        {
            try
            {
                var listaObtenerFurPagos =  _unitOfWork.FurPagoRepository.BuscarListaFurPagos(data.area, data.ciudad, data.anio, data.semana, data.moneda, data.estado);

                var listaObtenerFurPagosAgrupado = (
                      from p in listaObtenerFurPagos
                      group p by new
                      {
                          p.IdFur,
                          p.Codigo,
                          p.IdProveedor,
                          p.NombreProveedor,
                          p.IdProducto,
                          p.NombreProducto,
                          p.IdCC,
                          p.IdPais,
                          p.Cantidad,
                          p.NombreCentroCosto,
                          p.NumeroCuenta,
                          p.DescripcionCuenta,
                          p.MonedaFur,
                          p.NombreMonedaFur,
                          p.PrecioUnitarioDolares,
                          p.PrecioUnitarioSoles,
                          p.PrecioTotalDolares,
                          p.PrecioTotalSoles,
                          //p.IdDocumentoPago,
                          //p.NombreDocumento,
                          p.Descripcion,
                          //p.FechaEfectivo,
                          p.FaseAprobacion,
                          p.Antiguo,
                          p.MonedaPagoRealizado,
                          p.NombreMonedaPagoRealizado,
                          p.Usuario,
                          p.EstadoCancelado,
                          p.FechaModificacion

                      } into g
                      select new FurPagoDTO
                      {
                          IdFur = g.Key.IdFur,
                          Codigo = g.Key.Codigo,
                          IdProveedor = g.Key.IdProveedor,
                          NombreProveedor = g.Key.NombreProveedor,
                          IdProducto = g.Key.IdProducto,
                          NombreProducto = g.Key.NombreProducto,
                          IdCC = g.Key.IdCC,
                          IdPais = g.Key.IdPais,
                          Cantidad = g.Key.Cantidad,
                          NombreCentroCosto = g.Key.NombreCentroCosto,
                          NumeroCuenta = g.Key.NumeroCuenta,
                          DescripcionCuenta = g.Key.DescripcionCuenta,
                          MonedaFur = g.Key.MonedaFur,
                          NombreMonedaFur = g.Key.NombreMonedaFur,
                          //IdDocumentoPago = g.Key.IdDocumentoPago,
                          Descripcion = g.Key.Descripcion,
                          FaseAprobacion = g.Key.FaseAprobacion,
                          Antiguo = g.Key.Antiguo,
                          MonedaPagoRealizado = g.Key.MonedaPagoRealizado,
                          NombreMonedaPagoRealizado = g.Key.NombreMonedaPagoRealizado,
                          Usuario = g.Key.Usuario,
                          EstadoCancelado = g.Key.EstadoCancelado,
                          FechaModificacion = g.Key.FechaModificacion,
                          PrecioUnitarioDolares = g.Key.PrecioUnitarioDolares,
                          PrecioUnitarioSoles = g.Key.PrecioUnitarioSoles,
                          PrecioTotalDolares = g.Key.PrecioTotalDolares,
                          PrecioTotalSoles = g.Key.PrecioTotalSoles,

                          NombreProveedorComprobante = string.Join("/", g.Select(x => x.NombreProveedorComprobante).ToList()),
                          NombreDocumento = string.Join("/", g.Select(x => x.NombreDocumento).ToList()),
                          //FechaEfectivo = string.Join("/", g.Select(x => x.FechaEfectivo).ToList()),
                          //PrecioUnitarioDolares = g.Select(x => x.PrecioUnitarioDolares).Sum(),
                          //PrecioUnitarioSoles = g.Select(x => x.PrecioUnitarioSoles).Sum(),
                          //PrecioTotalDolares = g.Select(x => x.PrecioTotalDolares).Sum(),
                          //PrecioTotalSoles = g.Select(x => x.PrecioTotalSoles).Sum(),
                          PagoMonedaOrigen = g.Select(x => x.PagoMonedaOrigen).Sum(),
                          PagoDolares = g.Select(x => x.PagoDolares).Sum(),
                          NumeroRecibo = string.Join("/", g.Select(x => x.NumeroRecibo).ToList()),
                          NumeroComprobante = string.Join("/", g.Select(x => x.NumeroComprobante).ToList()),
                      }
               );
                return listaObtenerFurPagosAgrupado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor : Griselberto Huaman. 
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene todos la lista de furpagos relacionado a un fur.
        /// </summary>
        /// <param name="data">Grupo de Parametros </param>
        /// <returns> IEnumerable<FurPagoRealizadoDTO></returns>
        public IEnumerable<FurPagoRealizadoDTO> ObtenerPagosRealizadosPorFur(int IdFur)
        {
            try
            {
                return _unitOfWork.FurPagoRepository.ObtenerPagosRealizadosPorFur(IdFur);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertarFurPago(RegistrarFurPagoDTO Json)
        {
            try
            {
                var repFurPagoRep = _unitOfWork.FurPagoRepository;
                var repFurRep = _unitOfWork.FurRepository;
                var serFur = new FurService(_unitOfWork);
                var serComprobantePago = new ComprobantePagoPorFurService(_unitOfWork);
                FurPago furPago = new FurPago();
                Fur furBO;
                var correlativo = repFurPagoRep.ObtenerFurPago(Json.IdFur);
                using (TransactionScope scope = new TransactionScope())
                {
                    furBO = serFur.mapperFur.Map<Fur>(repFurRep.GetBy(x => x.Id == Json.IdFur).FirstOrDefault());

                    furPago.IdFur = Json.IdFur;
                    if (Json.IdComprobantePagoPorFur == null)
                    {
                        furPago.IdComprobantePago = null;
                    }
                    else
                    {
                        furPago.IdComprobantePago = serComprobantePago.ObtenerIdComprobante(Json.IdComprobantePagoPorFur);
                    }
                    furPago.NumeroPago = correlativo;
                    furPago.IdMoneda = Json.IdMoneda;
                    furPago.IdCuentaCorriente = Json.NumeroCuenta;
                    // furPago.NumeroCuenta = Json.NumeroCuenta;
                    furPago.NumeroRecibo = Json.NumeroRecibo;
                    furPago.IdFormaPago = Json.IdFormaPago;
                    furPago.FechaCobroBanco = Json.FechaCobroBanco;
                    if (Json.IdMoneda == 19)
                    {
                        furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaOrigen;
                        furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaDolares;
                    }
                    else
                    {
                        furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                        furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                    }
                    furPago.IdComprobantePagoPorFur = Json.IdComprobantePagoPorFur;
                    furPago.UsuarioCreacion = Json.Usuario;
                    furPago.FechaCreacion = DateTime.Now;
                    furPago.UsuarioModificacion = Json.Usuario;
                    furPago.FechaModificacion = DateTime.Now;
                    furPago.Estado = true;

                    if (furBO != null)
                    {
                        furBO.IdFurSubFaseAprobacion = 2;
                        furBO.Cancelado = Json.IdCancelado;
                        furBO.UsuarioModificacion = Json.Usuario;
                        furBO.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        throw new Exception("No existe FUR ");
                    }

                    serFur.Update(furBO);
                    this.Add(furPago);
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool ActualizarFurPago(RegistrarFurPagoDTO Json)
        {
            
            try
            {
                //integraDBContext contexto = new integraDBContext();
                var repFurPagoRep = _unitOfWork.FurPagoRepository;
                var repComPagoPorFur = _unitOfWork.ComprobantePagoPorFurRepository;
                var repFurRep = _unitOfWork.FurRepository;
                var serComprobantePago = new ComprobantePagoPorFurService(_unitOfWork);
                var serFur = new FurService(_unitOfWork);


                Fur furBO = new Fur();
                FurPago furPago = new FurPago();

                var correlativo = repFurPagoRep.ObtenerFurPago(Json.IdFur);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (Json.NumeroPago != 0)
                    {
                        furBO = _mapper.Map<Fur>(repFurRep.GetBy(x => x.Id == Json.IdFur).FirstOrDefault());

                        furPago = _mapper.Map <FurPago>( repFurPagoRep.FirstById(Json.Id));
                        furPago.IdFur = Json.IdFur;
                        if (Json.IdComprobantePagoPorFur == null)
                        {
                            furPago.IdComprobantePago = null;
                        }
                        else
                        {
                            furPago.IdComprobantePago = serComprobantePago.ObtenerIdComprobante(Json.IdComprobantePagoPorFur);
                        }
                        furPago.NumeroPago = Json.NumeroPago;
                        furPago.IdMoneda = Json.IdMoneda;
                        furPago.IdCuentaCorriente = Json.NumeroCuenta;
                        //furPago.NumeroCuenta = Json.NumeroCuenta;
                        furPago.NumeroRecibo = Json.NumeroRecibo;
                        furPago.IdFormaPago = Json.IdFormaPago;
                        furPago.FechaCobroBanco = Json.FechaCobroBanco;
                        if (Json.IdMoneda == 19)
                        {
                            furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                            furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                        }
                        else
                        {
                            furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                            furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                        }
                        furPago.IdComprobantePagoPorFur = Json.IdComprobantePagoPorFur;
                        //furPago.UsuarioCreacion = Json.Usuario;
                        furPago.FechaCreacion = DateTime.Now;
                        furPago.UsuarioModificacion = Json.Usuario;
                        furPago.FechaModificacion = DateTime.Now;
                        furPago.Estado = true;
                        if (furBO != null)
                        {
                            furBO.IdFurSubFaseAprobacion = 2;
                            furBO.Cancelado = Json.IdCancelado;
                            furBO.UsuarioModificacion = Json.Usuario;
                            furBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            throw new Exception("No existe FUR ");
                        }

                        serFur.Update(furBO);
                        this.Update(furPago);
                    }
                    else
                    {
                        furBO = _mapper.Map<Fur>(repFurRep.GetBy(x => x.Id == Json.IdFur).FirstOrDefault()) ;
                        furPago.IdFur = Json.IdFur;
                        if (Json.IdComprobantePagoPorFur == null)
                        {
                            furPago.IdComprobantePago = Json.IdComprobantePago;
                        }
                        else
                        {
                            furPago.IdComprobantePago = serComprobantePago.ObtenerIdComprobante(Json.IdComprobantePagoPorFur);
                        }
                        furPago.NumeroPago = correlativo;
                        furPago.IdMoneda = Json.IdMoneda;
                        furPago.IdCuentaCorriente = Json.NumeroCuenta;
                        //furPago.NumeroCuenta = Json.NumeroCuenta;
                        furPago.NumeroRecibo = Json.NumeroRecibo;
                        furPago.IdFormaPago = Json.IdFormaPago;
                        furPago.FechaCobroBanco = Json.FechaCobroBanco;
                        if (Json.IdMoneda == 19)
                        {
                            furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                            furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                        }
                        else
                        {
                            furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                            furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                        }
                        furPago.IdComprobantePagoPorFur = Json.IdComprobantePagoPorFur;
                        furPago.UsuarioCreacion = Json.Usuario;
                        furPago.FechaCreacion = DateTime.Now;
                        furPago.UsuarioModificacion = Json.Usuario;
                        furPago.FechaModificacion = DateTime.Now;
                        furPago.Estado = true;
                        if (furBO != null)
                        {
                            furBO.IdFurSubFaseAprobacion = 2;
                            furBO.Cancelado = Json.IdCancelado;
                            furBO.UsuarioModificacion = Json.Usuario;
                            furBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            throw new Exception("No existe FUR ");
                        }

                        serFur.Update(furBO);
                        this.Add(furPago);
                    }

                    scope.Complete();
                }

                return true;

            }
            catch (Exception Ex)
            {
               throw new Exception(Ex.Message);
            }
        }

        public IEnumerable<DTO.ComboDTO> ObtenerListaFormaPago()
        {
            try
            {
                return _unitOfWork.FurPagoRepository.ObtenerListaFormaPago();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
