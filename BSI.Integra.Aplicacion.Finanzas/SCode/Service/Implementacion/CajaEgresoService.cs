using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CajaEgresoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_CajaEgreso
    /// </summary>
    public class CajaEgresoService : ICajaEgresoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CajaEgresoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCajaEgreso, CajaEgreso>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CajaEgreso Add(CajaEgreso entidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaEgresoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CajaEgreso>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CajaEgreso Update(CajaEgreso entidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaEgresoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CajaEgreso>(modelo);
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
                _unitOfWork.CajaEgresoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CajaEgreso> Add(List<CajaEgreso> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaEgresoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CajaEgreso>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CajaEgreso> Update(List<CajaEgreso> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaEgresoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CajaEgreso>>(modelo);
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
                _unitOfWork.CajaEgresoRepository.Delete(listadoIds, usuario);
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
        /// Devuelve los registros  de Caja Egreso
        /// </summary>
        /// <returns> List<CajaEgresoDTO> </returns>
        public object ObtenerCajaEgresoEnviado(FiltroCajaEgresoDTO filtro)
        {
            try
            {
                var respuesta = _unitOfWork.CajaEgresoRepository.ObtenerCajaEgresoEnviado(filtro);

                if (respuesta!=null && respuesta.Count()>0)
                {
                    var listadoSolicitante = respuesta.Select(x => new { Id = x.IdPersonalSolicitante, Nombre = x.PersonalSolicitante }).Distinct().ToList();
                    return new { respuesta, listadoSolicitante };
                }
                return new { respuesta, listadoSolicitante = 0 };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los nombres de los solicitantes correspondientes al Encargado de caja
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public IEnumerable<CajaPorRendirCombosDTO> ObtenerCajaPorRendirSolicitanteREC(int idPersonalResponsable)
        {
            try
            {
                return _unitOfWork.CajaEgresoRepository.ObtenerCajaPorRendirSolicitanteREC(idPersonalResponsable);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta en CajaEgreso 
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public List<CajaEgresoDTO> InsertarCajaEgreso(InsertCajaEgresoDTO RequestDTO)
        {
            try
            {
                var _repoCajaEgreso = _unitOfWork.CajaEgresoRepository;
                var _repoComprobantePagoPorFurRepositorio = _unitOfWork.ComprobantePagoPorFurRepository;
                var _serComprobantePagoPorFurRepositorio = new ComprobantePagoPorFurService(_unitOfWork);


                ComprobantePagoPorFur ComprobantePagoPorFur = new ComprobantePagoPorFur();
                ComprobantePagoPorFur.IdComprobantePago = RequestDTO.IdComprobantePago;
                ComprobantePagoPorFur.IdFur = (RequestDTO.IdFur == null ? 0 : RequestDTO.IdFur);
                ComprobantePagoPorFur.Monto = RequestDTO.TotalEfectivo;
                ComprobantePagoPorFur.Estado = true;
                ComprobantePagoPorFur.UsuarioCreacion = RequestDTO.UsuarioModificacion;
                ComprobantePagoPorFur.UsuarioModificacion = RequestDTO.UsuarioModificacion;
                ComprobantePagoPorFur.FechaCreacion = DateTime.Now;
                ComprobantePagoPorFur.FechaModificacion = DateTime.Now;
                ComprobantePagoPorFur = _serComprobantePagoPorFurRepositorio.Add(ComprobantePagoPorFur);

                CajaEgreso CajaEgreso = new CajaEgreso();
                CajaEgreso.IdCajaPorRendirCabecera = RequestDTO.IdCajaPorRendirCabecera;
                CajaEgreso.IdCaja = RequestDTO.IdCaja;
                CajaEgreso.IdComprobantePago = RequestDTO.IdComprobantePago;

                CajaEgreso.IdComprobantePagoPorFur = ComprobantePagoPorFur.Id;

                CajaEgreso.IdFur = RequestDTO.IdFur;
                CajaEgreso.Descripcion = RequestDTO.Descripcion;
                CajaEgreso.IdMoneda = RequestDTO.IdMoneda;
                CajaEgreso.TotalEfectivo = RequestDTO.TotalEfectivo;
                CajaEgreso.IdCajaEgresoAprobado = null;
                CajaEgreso.EsEnviado = false;
                CajaEgreso.IdPersonalSolicitante = RequestDTO.IdPersonalSolicitante.Value;
                CajaEgreso.Estado = true;
                CajaEgreso.UsuarioCreacion = RequestDTO.UsuarioModificacion;
                CajaEgreso.UsuarioModificacion = RequestDTO.UsuarioModificacion;
                CajaEgreso.FechaCreacion = DateTime.Now;
                CajaEgreso.FechaModificacion = DateTime.Now;
                CajaEgreso = this.Add(CajaEgreso);

                var CajasEgreso = _repoCajaEgreso.ObtenerRegistroCajaEgreso(CajaEgreso.Id);

                if (CajasEgreso.Count > 1) throw new Exception("Error: Multiples registros encontrados");
                if (CajasEgreso.Count == 0) throw new Exception("Error: Ningun registro encontrado");

                return CajasEgreso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor : Griselberto Huaman. 
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve la solicitud de Caja Egreso
        /// </summary>
        /// <returns> List<CuentaCorrientesDTO> </returns>
        public bool DevolverSolicitudCajaEgreso(int id, string usuario)
        {
            try
            {
                var repCajaEgreso = _unitOfWork.CajaEgresoRepository;
                CajaEgreso entidad = new CajaEgreso();
                entidad = _mapper.Map<CajaEgreso>(repCajaEgreso.FirstById(id));
                entidad.EsEnviado = false;
                entidad.FechaEnvio = null;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioModificacion = usuario;
                this.Update(entidad);
                return true;

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
        /// Devuelve la solicitud de Caja Egreso
        /// </summary>
        /// <returns> List<CuentaCorrientesDTO> </returns>
        public List<CajaEgresoDTO> ObtenerRegistrosCajaEgreso(int IdCajaPorRendirCabecera)
        {
            try
            {
                return _unitOfWork.CajaEgresoRepository.ObtenerRegistrosCajaEgreso(IdCajaPorRendirCabecera);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Inserta Registro de CajaEgreso con su idEgresoAprobado ya validado se actualiza el estado de Furs en caso sean cancelados, 
        /// de lo contrario se cambia a un estado para que puedan seguir siendo usados para pagos.
        /// </summary>
        /// <param name="objetoEgresoAprobado"></param>
        /// <param name="idRecCancelado"></param>
        /// <returns></returns>
        public bool InsertarAprobacionCajaEgreso(CajaEgresoAprobadoDTO objetoEgresoAprobado, CajaEgresoDTO objEgresoCaja)
        {
            try
            {
                var serComprobantePagoFur = new ComprobantePagoPorFurService(_unitOfWork);
                var serCuentaCorriente = new CuentaCorrienteService(_unitOfWork);
                var serCaja = new CajaService(_unitOfWork);
                var serFurPago = new FurPagoService(_unitOfWork);
                var serFur = new FurService(_unitOfWork);
                var repFur = _unitOfWork.FurRepository;

                CajaEgreso registroEgreso = new CajaEgreso();
                registroEgreso.IdCaja = objEgresoCaja.IdCaja;
                registroEgreso.IdCajaPorRendirCabecera = objEgresoCaja.IdCajaPorRendirCabecera;
                registroEgreso.IdComprobantePago = objEgresoCaja.IdComprobantePago;
                registroEgreso.IdFur = objEgresoCaja.IdFur == 0 ? null : objEgresoCaja.IdFur;
                registroEgreso.Descripcion = objEgresoCaja.Descripcion;
                registroEgreso.IdMoneda = objEgresoCaja.IdMoneda;
                registroEgreso.TotalEfectivo = objEgresoCaja.TotalEfectivo;
                registroEgreso.IdPersonalSolicitante = objEgresoCaja.IdPersonalSolicitante.Value;
                registroEgreso.IdPersonalResponsable = objEgresoCaja.IdPersonalResponsable;
                registroEgreso.IdCajaEgresoAprobado = objetoEgresoAprobado.Id;
                registroEgreso.EsEnviado = objEgresoCaja.EsEnviado;
                registroEgreso.FechaEnvio = DateTime.Now;
                registroEgreso.Estado = true;
                registroEgreso.FechaCreacion = DateTime.Now;
                registroEgreso.FechaModificacion = DateTime.Now;
                registroEgreso.UsuarioCreacion = objEgresoCaja.UsuarioModificacion;
                registroEgreso.UsuarioModificacion = objEgresoCaja.UsuarioModificacion;

                using (TransactionScope scope = new TransactionScope())
                {
                    if (registroEgreso.IdFur != null)
                    {

                        //Se inserta la asociacion del fur con el comprobante 
                        ComprobantePagoPorFur comprobanteFur = new ComprobantePagoPorFur();

                        comprobanteFur.IdComprobantePago = registroEgreso.IdComprobantePago.Value;
                        comprobanteFur.IdFur = registroEgreso.IdFur.Value;
                        comprobanteFur.Monto = registroEgreso.TotalEfectivo;
                        comprobanteFur.Estado = true;
                        comprobanteFur.FechaCreacion = DateTime.Now;
                        comprobanteFur.FechaModificacion = DateTime.Now;
                        comprobanteFur.UsuarioCreacion = objetoEgresoAprobado.UsuarioModificacion;
                        comprobanteFur.UsuarioModificacion = objetoEgresoAprobado.UsuarioModificacion;

                        comprobanteFur = serComprobantePagoFur.Add(comprobanteFur);

                        registroEgreso.IdComprobantePagoPorFur = comprobanteFur.Id;

                        var numPago = serFurPago.obtenerNumeroPagoByFur(objEgresoCaja.IdFur.Value);
                        var idCuentaCorriente = serCaja.obtenerIdCuentaCorriente(objEgresoCaja.IdCaja);

                        FurPago furPago = new FurPago();
                        furPago.IdComprobantePago = registroEgreso.IdComprobantePago.Value;
                        furPago.IdComprobantePagoPorFur = registroEgreso.IdComprobantePagoPorFur;
                        furPago.IdFur = objEgresoCaja.IdFur;
                        furPago.NumeroPago = numPago == 0 ? 1 : numPago + 1;
                        furPago.IdMoneda = objEgresoCaja.IdMoneda;
                        furPago.IdCuentaCorriente = idCuentaCorriente;
                        furPago.NumeroRecibo = objetoEgresoAprobado.CodigoRec;
                        furPago.IdFormaPago = 2; //Añadir Valor Estatico
                        furPago.FechaCobroBanco = objetoEgresoAprobado.FechaCreacionRegistro;
                        furPago.PrecioTotalMonedaDolares = objEgresoCaja.IdMoneda == 19 ? objEgresoCaja.TotalEfectivo : 0;
                        furPago.PrecioTotalMonedaOrigen = objEgresoCaja.IdMoneda != 19 ? objEgresoCaja.TotalEfectivo : 0;

                        furPago.Estado = true;
                        furPago.FechaCreacion = DateTime.Now;
                        furPago.FechaModificacion = DateTime.Now;
                        furPago.UsuarioCreacion = objetoEgresoAprobado.UsuarioModificacion;
                        furPago.UsuarioModificacion = objetoEgresoAprobado.UsuarioModificacion;
                        furPago = serFurPago.Add(furPago);

                        Fur fur = new Fur();
                        fur = serFur.mapperFur.Map<Fur>(repFur.FirstById(objEgresoCaja.IdFur.Value));

                        if (objEgresoCaja.EsCancelado)
                        {
                            fur.Cancelado = true;
                            fur.OcupadoSolicitud = true;
                            fur.OcupadoRendicion = true;
                        }
                        else
                        {
                            fur.OcupadoSolicitud = false;
                            fur.OcupadoRendicion = false;
                        }
                        _unitOfWork.DetachAll();
                        fur = serFur.Update(fur);
                    }
                    this.Add(registroEgreso);
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Se actualiza la tabla CajaEgreso Aprobado y se coloca el IdCajaEgresoAprobado, Se crea un nuevo pago del REC
        /// generado y se actualiza el estado del fur si ha sido candelado en su totalidad o no. Este procedimiento se realiza para el metodo de Generacion de REC.
        /// </summary>
        /// <param name="objetoEgresoAprobado"></param>
        /// <param name="idRecCancelado"></param>
        /// <returns></returns>
        public bool ActualizarAprobacionCajaEgreso(CajaEgresoAprobadoDTO objetoEgresoAprobado, IdCajaEgresoCanceladoDTO idRecCancelado)
        {
            try
            {
                var _repCajaEgresoRep = _unitOfWork.CajaEgresoRepository;
                var serCuentaCorriente = new CuentaCorrienteService(_unitOfWork);
                var serFurPago = new FurPagoService(_unitOfWork);
                var serFur = new FurService(_unitOfWork);
                var serCaja = new CajaService(_unitOfWork);
                var repFur = _unitOfWork.FurRepository;

                using (TransactionScope scope = new TransactionScope())
                {
                    CajaEgreso entidad = new CajaEgreso();
                    entidad = _mapper.Map<CajaEgreso>(_repCajaEgresoRep.FirstById(idRecCancelado.IdRec));
                    entidad.IdPersonalResponsable = objetoEgresoAprobado.IdPersonalResponsable;
                    entidad.IdCajaEgresoAprobado = objetoEgresoAprobado.Id;
                    entidad.UsuarioModificacion = objetoEgresoAprobado.UsuarioModificacion;
                    entidad.FechaModificacion = DateTime.Now;
                    entidad = this.Update(entidad);


                    if (entidad.IdFur != null)
                    {
                        var numPago = serFurPago.obtenerNumeroPagoByFur(entidad.IdFur.Value);
                        var idCuentaCorriente = serCaja.obtenerIdCuentaCorriente(entidad.IdCaja);
                        FurPago furPago = new FurPago();

                        furPago.IdFur = entidad.IdFur;
                        furPago.NumeroPago = numPago == 0 ? 1 : numPago + 1;
                        furPago.IdComprobantePago = entidad.IdComprobantePago.Value;
                        furPago.IdComprobantePagoPorFur = entidad.IdComprobantePagoPorFur;
                        furPago.IdMoneda = entidad.IdMoneda;
                        furPago.IdCuentaCorriente = idCuentaCorriente;
                        furPago.NumeroRecibo = objetoEgresoAprobado.CodigoRec;
                        furPago.IdFormaPago = 2; //Añadir Valor Estatico
                        furPago.FechaCobroBanco = objetoEgresoAprobado.FechaCreacionRegistro;
                        furPago.PrecioTotalMonedaDolares = entidad.IdMoneda == 19 ? entidad.TotalEfectivo : 0;
                        furPago.PrecioTotalMonedaOrigen = entidad.IdMoneda != 19 ? entidad.TotalEfectivo : 0;
                        furPago.Estado = true;
                        furPago.FechaCreacion = DateTime.Now;
                        furPago.FechaModificacion = DateTime.Now;
                        furPago.UsuarioCreacion = objetoEgresoAprobado.UsuarioModificacion;
                        furPago.UsuarioModificacion = objetoEgresoAprobado.UsuarioModificacion;

                        furPago = serFurPago.Add(furPago);
                        Fur fur = new Fur();
                        fur = serFur.mapperFur.Map<Fur>(repFur.FirstById(entidad.IdFur.Value));
                        if (idRecCancelado.FurEsCancelado)
                        {
                            fur.Cancelado = true;
                            fur.OcupadoSolicitud = true;
                            fur.OcupadoRendicion = true;
                        }
                        else
                        {
                            fur.OcupadoSolicitud = false;
                            fur.OcupadoRendicion = false;
                        }
                        _unitOfWork.DetachAll();
                        fur = serFur.Update(fur);
                        scope.Complete();
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera el RegistroEgresado
        /// </summary>
        /// <returns> true </returns>
        public bool GenerarRegistroEgresoCaja(GenerarRegistroEgresoDTO Data)
        {
            try
            {
                var repCajaEgresoAprobado = _unitOfWork.CajaEgresoAprobadoRepository;
                var serCajaEgresoAprobado = new CajaEgresoAprobadoService(_unitOfWork);
                int correlativo = 0;
                var ListaCondigoREC = repCajaEgresoAprobado.GetBy(x => x.Estado == true && x.CodigoRec.Contains(Data.CajaRECAprobado.CodigoRec)).ToList();

                if (ListaCondigoREC != null && ListaCondigoREC.Count() != 0)
                {
                    var CodigoRecMayor = ListaCondigoREC.OrderByDescending(x => x.Id).FirstOrDefault().CodigoRec;
                    if (Int32.Parse(CodigoRecMayor.Substring(CodigoRecMayor.LastIndexOf(".") + 1).Trim()) > correlativo)
                    {
                        correlativo = Int32.Parse(CodigoRecMayor.Substring(CodigoRecMayor.LastIndexOf(".") + 1).Trim());
                    }
                    correlativo++;
                }
                else
                {
                    correlativo = 1;
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    Data.CajaRECAprobado.CodigoRec += correlativo;

                    CajaEgresoAprobado egresoAprobado = new CajaEgresoAprobado();
                    egresoAprobado.CodigoRec = Data.CajaRECAprobado.CodigoRec;
                    egresoAprobado.IdCaja = Data.CajaRECAprobado.IdCaja;
                    egresoAprobado.Anho = Data.CajaRECAprobado.Anho;
                    egresoAprobado.FechaCreacionRegistro = Data.CajaRECAprobado.FechaCreacionRegistro;
                    egresoAprobado.Detalle = Data.CajaRECAprobado.Detalle;
                    egresoAprobado.Observacion = Data.CajaRECAprobado.Observacion;
                    egresoAprobado.Origen = Data.CajaRECAprobado.Origen;
                    egresoAprobado.Estado = true;
                    egresoAprobado.UsuarioCreacion = Data.CajaRECAprobado.UsuarioModificacion;
                    egresoAprobado.UsuarioModificacion = Data.CajaRECAprobado.UsuarioModificacion;
                    egresoAprobado.FechaModificacion = DateTime.Now;
                    egresoAprobado.FechaCreacion = DateTime.Now;

                    egresoAprobado = serCajaEgresoAprobado.Add(egresoAprobado);

                    Data.CajaRECAprobado.Id = egresoAprobado.Id; //_repEgresoAprobadoRep.InsertarRegistroEgresoAprobado(generacionRegEgresoDTO.CajaRECAprobado, integraDBContext);

                    foreach (var listaId in Data.ListaEgresoCancelado)
                    {
                        var respuesta = this.ActualizarAprobacionCajaEgreso(Data.CajaRECAprobado, listaId);
                        if (respuesta != true) throw new Exception("Fallo al actualizar la Aprobación de Caja Egreso");

                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera el RegistroEgresado Inmediato
        /// </summary>
        /// <returns> true </returns>
        public bool GenerarRegistroEgresoCajaInmediato(GenerarRegistroEgresoInmediatoDTO Data)
        {
            try
            {
                var repCajaEgresoAprobado = _unitOfWork.CajaEgresoAprobadoRepository;
                var serCajaEgresoAprobado = new CajaEgresoAprobadoService(_unitOfWork);
                int correlativo = 0;
                var ListaCondigoREC = repCajaEgresoAprobado.GetBy(x => x.Estado == true && x.CodigoRec.Contains(Data.CajaEgresoAprobado.CodigoRec) && x.IdCaja == Data.CajaEgresoAprobado.IdCaja).ToList();

                if (ListaCondigoREC != null && ListaCondigoREC.Count() != 0)
                {
                    var CodigoRecMayor = ListaCondigoREC.OrderByDescending(x => x.Id).FirstOrDefault().CodigoRec;

                    if (Int32.Parse(CodigoRecMayor.Substring(CodigoRecMayor.LastIndexOf(".") + 1).Trim()) > correlativo)
                    {
                        correlativo = Int32.Parse(CodigoRecMayor.Substring(CodigoRecMayor.LastIndexOf(".") + 1).Trim());
                    }
                    correlativo++;
                }
                else
                {
                    correlativo = 1;
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    Data.CajaEgresoAprobado.CodigoRec += correlativo;

                    CajaEgresoAprobado egresoAprobado = new CajaEgresoAprobado();

                    egresoAprobado.CodigoRec = Data.CajaEgresoAprobado.CodigoRec;
                    egresoAprobado.IdCaja = Data.CajaEgresoAprobado.IdCaja;
                    egresoAprobado.Anho = Data.CajaEgresoAprobado.Anho;
                    egresoAprobado.FechaCreacionRegistro = Data.CajaEgresoAprobado.FechaCreacionRegistro;
                    egresoAprobado.Detalle = Data.CajaEgresoAprobado.Detalle;
                    egresoAprobado.Observacion = Data.CajaEgresoAprobado.Observacion;
                    egresoAprobado.Origen = Data.CajaEgresoAprobado.Origen;
                    egresoAprobado.Estado = true;
                    egresoAprobado.UsuarioCreacion = Data.CajaEgresoAprobado.UsuarioModificacion;
                    egresoAprobado.UsuarioModificacion = Data.CajaEgresoAprobado.UsuarioModificacion;
                    egresoAprobado.FechaModificacion = DateTime.Now;
                    egresoAprobado.FechaCreacion = DateTime.Now;
                    egresoAprobado = serCajaEgresoAprobado.Add(egresoAprobado);
                    if (egresoAprobado != null)
                    {
                        Data.CajaEgresoAprobado.Id = egresoAprobado.Id;
                        foreach (var listaCajaEgreso in Data.ListaRegistroEgreso)
                        {
                            var respuesta = this.InsertarAprobacionCajaEgreso(Data.CajaEgresoAprobado, listaCajaEgreso);
                            if (respuesta != true) throw new Exception("Fallo al insertar la Aprobación de Caja Egreso");
                        }
                        scope.Complete();
                    }
                    else throw new Exception("Error al insertar EgresoAprobado");

                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Elimina y actualiza dependencias a la caja Egreso
        /// </summary>
        /// <returns> true </returns>
        public bool EliminarCajaEgreso(int id, string usuario)
        {
            try
            {
                var repCajaEgreso = _unitOfWork.CajaEgresoRepository;
                var repComprobantePagoFur = _unitOfWork.ComprobantePagoPorFurRepository;
                var serComprobantePagoFur = new ComprobantePagoPorFurService(_unitOfWork);
                var repFur = _unitOfWork.FurRepository;
                var serFur = new FurService(_unitOfWork);

                using (TransactionScope scope = new TransactionScope())
                {
                    CajaEgreso cajaEgreso = new CajaEgreso();
                    cajaEgreso = _mapper.Map<CajaEgreso>(repCajaEgreso.FirstById(id));

                    if (cajaEgreso == null) throw new Exception("El registro que se desea eliminar no existe ¿Id correcto?");

                    ComprobantePagoPorFur comprobantePagoPor = new ComprobantePagoPorFur();

                    comprobantePagoPor = serComprobantePagoFur.mapperComprobantePagoFur
                                            .Map<ComprobantePagoPorFur>(
                                                repComprobantePagoFur
                                                .GetBy(x =>
                                                x.IdFur == cajaEgreso.IdFur &&
                                                x.IdComprobantePago == cajaEgreso.IdComprobantePago &&
                                                x.Monto == cajaEgreso.TotalEfectivo && x.Estado == true).FirstOrDefault());

                    if (comprobantePagoPor == null)
                        throw new Exception("No se encontro registro de 'ComprobantePagoPorFur' para el IdComprobante=" + cajaEgreso.IdComprobantePago + " e IdFur=" + cajaEgreso.IdFur + " indicados");

                    var respuesta = serComprobantePagoFur.Delete(comprobantePagoPor.Id, usuario);
                    if (respuesta != true) throw new Exception("Fallos al eliminar 'ComprobantePagoPorFur' para el IdComprobante=" + cajaEgreso.IdComprobantePago + " e IdFur=" + cajaEgreso.IdFur + " indicados");

                    Fur fur = new Fur();
                    fur = serFur.mapperFur.Map<Fur>(repFur.FirstById(cajaEgreso.IdFur.Value));
                    fur.OcupadoRendicion = false;
                    fur.UsuarioModificacion = usuario;
                    fur.FechaModificacion = DateTime.Now;
                    serFur.Update(fur);

                    this.Delete(id, usuario);
                    scope.Complete();
                }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// actualiza a la caja Egreso y dependencias
        /// </summary>
        /// <returns> true </returns>
        public bool ActualizarCajaEgresoEstablecerRendido(int IdPersonal,int IdCajaPorRendirCabecera)
        {
           
            try
            {
                var _repoCajaEgreso = _unitOfWork.CajaEgresoRepository;
                var _repoCajaPorRendirCabecera = _unitOfWork.CajaPorRendirCabeceraRepository;
                List<CajaEgreso> CajasEgreso = _mapper.Map<List<CajaEgreso>>(_repoCajaEgreso.GetBy(x => x.IdCajaPorRendirCabecera == IdCajaPorRendirCabecera && x.Estado == true && x.EsEnviado == false).ToList());
                if (CajasEgreso == null) throw new Exception("El registro que se desea actualizar no existe ¿Id correcto?");
                else if (CajasEgreso.Count == 0) throw new Exception("No se detecto ningun Registro para Rendirlo");


                for (int i = 0; i < CajasEgreso.Count; ++i)
                {
                    CajasEgreso[i].EsEnviado = true;
                    CajasEgreso[i].FechaEnvio = DateTime.Now;
                    this.Update(CajasEgreso[i]);
                }

                var CajaPorRendirCabecera = _repoCajaPorRendirCabecera.GetBy(x => x.Id == IdCajaPorRendirCabecera).FirstOrDefault();
                if (CajaPorRendirCabecera == null) throw new Exception("El registro que se desea actualizar no existe ¿Id correcto?");

                CajaPorRendirCabecera.EsRendido = true;

                _repoCajaPorRendirCabecera.Update(CajaPorRendirCabecera);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// actualiza a la caja Egreso y dependencias
        /// </summary>
        /// <returns> true </returns>
        public CajaEgresoActualizar ActualizarRegistroEgresoCajaEnviado(CajaEgresoActualizar data)
        {
            try
            {
                var repCajaEgreso = _unitOfWork.CajaEgresoRepository;
                var repFur = _unitOfWork.FurRepository;
                var repComprobantePagoPorFur = _unitOfWork.ComprobantePagoPorFurRepository;
                var serComprobantePagoPorFur = new ComprobantePagoPorFurService(_unitOfWork);
                ComprobantePagoPorFur comprobanteAsociado = new ComprobantePagoPorFur();
                CajaEgreso cajaRec = new CajaEgreso();
                cajaRec = _mapper.Map<CajaEgreso>(repCajaEgreso.FirstById(data.Id));

                using (TransactionScope scope = new TransactionScope())
                {
                    if ((cajaRec.IdFur != data.IdFur || cajaRec.TotalEfectivo != data.TotalEfectivo || cajaRec.IdComprobantePago != data.IdComprobantePago) &&
                        (data.IdComprobantePago != null && data.IdFur != null && data.IdComprobantePago != 0 && data.IdFur != 0))
                    {

                        if (cajaRec.IdComprobantePagoPorFur != null && cajaRec.IdComprobantePagoPorFur != 0)
                        {
                            serComprobantePagoPorFur.Delete(cajaRec.IdComprobantePagoPorFur.Value, data.UsuarioModificacion);
                        }
                        comprobanteAsociado.IdComprobantePago = data.IdComprobantePago.Value;
                        comprobanteAsociado.IdFur = data.IdFur.Value;
                        comprobanteAsociado.Monto = data.TotalEfectivo;
                        comprobanteAsociado.Estado = true;
                        comprobanteAsociado.UsuarioCreacion = data.UsuarioModificacion;
                        comprobanteAsociado.UsuarioModificacion = data.UsuarioModificacion;
                        comprobanteAsociado.FechaModificacion = DateTime.Now;
                        comprobanteAsociado.FechaCreacion = DateTime.Now;
                        serComprobantePagoPorFur.Add(comprobanteAsociado);
                    }
                    else { comprobanteAsociado = null; }

                    cajaRec.IdComprobantePago = data.IdComprobantePago;
                    cajaRec.IdComprobantePagoPorFur = comprobanteAsociado == null ? cajaRec.IdComprobantePagoPorFur : comprobanteAsociado.Id;
                    cajaRec.IdFur = data.IdFur;
                    cajaRec.Descripcion = data.Descripcion;
                    cajaRec.IdMoneda = data.IdMoneda;
                    cajaRec.TotalEfectivo = data.TotalEfectivo;
                    cajaRec.UsuarioModificacion = data.UsuarioModificacion;
                    cajaRec.FechaModificacion = DateTime.Now;

                    this.Update(cajaRec);


                    if (data.IdFur != data.IdFurAnterior)
                    {
                        if (data.IdFur != null && data.IdFur != 0)
                        {
                            var fur = repFur.FirstById(data.IdFur.Value);
                            fur.OcupadoRendicion = true;
                            fur.OcupadoSolicitud = true;
                            var respuesta = repFur.Update(fur);
                            if (respuesta != true) throw new Exception("Fallo al Ocupar el FUR id=" + data.IdFur.Value);
                        }
                        if (data.IdFurAnterior != null && data.IdFurAnterior != 0)
                        {
                            var fur = repFur.FirstById(data.IdFurAnterior.Value);
                            fur.OcupadoRendicion = false;
                            fur.OcupadoSolicitud = false;
                            var respuesta = repFur.Update(fur);
                            if (respuesta != true) throw new Exception("Fallo al Liberar el FUR-Anterior id=" + data.IdFurAnterior.Value);
                        }
                    }
                    scope.Complete();
                }

                return data;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// obtiene el monto limite del fur
        /// </summary>
        /// <returns> true </returns>
        public decimal ObtenerMontoLimite(int IdFur)
        {
            try
            {
                var repFur = _unitOfWork.FurRepository;
                var repComprobantePagoPorFur = _unitOfWork.ComprobantePagoPorFurRepository;
                var serFur = new FurService(_unitOfWork);

                Fur Fur = serFur.mapperFur.Map<Fur>(repFur.FirstById(IdFur));

                if (Fur == null) throw new Exception("Error No se encontro el FUR");

                List<TComprobantePagoPorFur> RegistrosDelFur = repComprobantePagoPorFur.GetBy(x => x.IdFur == IdFur).ToList();

                decimal PagosAcumulado = 0;
                for (int i = 0; i < RegistrosDelFur.Count; ++i)
                    PagosAcumulado += RegistrosDelFur[i].Monto;
                var MontoLimite = (Fur.PrecioTotalMonedaOrigen - PagosAcumulado);
                return MontoLimite;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos para el llenado de GRilla en ResumenCaja - Tab(REC)
        /// </summary>
        /// <returns> List<CajaEgresoGenerarPdfDTO> </returns>
        public IEnumerable<CajaEgresoGenerarPdfDTO> ObtenerCajaEgresoAprobadoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja)
        {
            try
            {
                return _unitOfWork.CajaEgresoRepository.ObtenerCajaEgresoAprobadoByFecha(FechaInicial, FechaFinal, IdCaja);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos para la generacion de Bytes
        /// </summary>
        /// <returns> List<CajaEgresoGenerarPdfDTO> </returns>
        public IEnumerable<CajaEgresoGenerarPdfDTO> ObtenerDatosCajaEgreso(int[] IdEgresoCaja)
        {
            try
            {
                return _unitOfWork.CajaEgresoRepository.ObtenerDatosCajaEgreso(IdEgresoCaja);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Byte para la generacion de PDF, solicitados segun el reporte.
        /// </summary>
        /// <param name="listaEnteros">Lista de Id de EgresoCaja</param>
        /// <returns> byte[]</returns>
        public IEnumerable<byte[]> ObtenerDocumentosEgresoCaja(List<int> listaEnteros)
        {
            try
            {
                List<byte[]> listaPDFbytes = new List<byte[]>();

                var listaCajaEgresoDatosPdf = this.ObtenerDatosCajaEgreso(listaEnteros.ToArray());
                foreach (var datosEgresoCaja in listaCajaEgresoDatosPdf)
                {
                    var pdf = this.GenerarPDFReciboEgresoCaja(datosEgresoCaja);
                    listaPDFbytes.Add(pdf);
                }

                return listaPDFbytes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        



        /// Autor: Griselberto Huaman
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Byte para la generacion de PDF, solicitados segun el reporte.
        /// </summary>
        /// <param name="datosGenerarPdf">Datos requeridos para llenar los datos de los Pdf</param>
        /// <returns> byte[]</returns>
        public byte[] GenerarPDFReciboEgresoCaja(CajaEgresoGenerarPdfDTO datosGenerarPdf)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 72f, 65f, 70f, 65f);
                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, ms);
                doc.AddTitle("Recibo" + datosGenerarPdf.CodigoEgresoCaja);
                doc.AddCreator("BS grupo");
                doc.Open();
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font ForLine = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont_normal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfPTable TablaTop = new PdfPTable(3);
                TablaTop.WidthPercentage = 100;
                TablaTop.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths = new float[] { 25f, 46.5f, 28.5f };
                TablaTop.SetWidths(widths);
                PdfPCell SeccionEmpresa = new PdfPCell(new Phrase(datosGenerarPdf.RazonSocial, _standardFont));
                SeccionEmpresa.BorderWidth = 1;
                SeccionEmpresa.BackgroundColor = BaseColor.LIGHT_GRAY;
                PdfPCell SeccionTitulo = new PdfPCell(new Phrase("              RECIBO DE EGRESOS EFECTIVO", _standardFont));
                SeccionTitulo.BorderWidth = 0;
                PdfPCell SeccioNumeroRecibo = new PdfPCell(new Phrase(datosGenerarPdf.CodigoEgresoCaja, _standardFont));
                SeccioNumeroRecibo.BorderWidth = 1;
                SeccioNumeroRecibo.BackgroundColor = BaseColor.LIGHT_GRAY;
                TablaTop.AddCell(SeccionEmpresa);
                TablaTop.AddCell(SeccionTitulo);
                TablaTop.AddCell(SeccioNumeroRecibo);
                doc.Add(TablaTop);
                //Segunda 
                Paragraph paDir = new Paragraph("Dirección: " + datosGenerarPdf.Direccion, _standardFont_normal); paDir.SpacingBefore = 10f; doc.Add(paDir);
                doc.Add(new Paragraph("Ruc: " + datosGenerarPdf.Ruc, _standardFont_normal));
                Paragraph paCentral = new Paragraph("Central: " + datosGenerarPdf.Central, _standardFont_normal); paCentral.SpacingAfter = 8f; doc.Add(paCentral);
                //Tercera seccion
                PdfPTable TablaBottom = new PdfPTable(2);
                TablaBottom.DefaultCell.Padding = 10f;
                //TablaBottom.DefaultCell.CellEvent = new CellSpacingEvent(20);
                TablaBottom.WidthPercentage = 80;
                TablaBottom.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths2 = new float[] { 26f, 78f };
                TablaBottom.SetWidths(widths2);
                PdfPCell SeccionCuentaTitulo = new PdfPCell(new Phrase("Cuenta", _standardFont2));
                SeccionCuentaTitulo.PaddingTop = 6f;
                SeccionCuentaTitulo.BorderWidthTop = 1;
                SeccionCuentaTitulo.BorderWidth = 0;
                PdfPCell SeccionCuentaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.NumeroCuenta, _standardFont_normal));
                SeccionCuentaValor.BorderWidth = 0;
                SeccionCuentaValor.PaddingTop = 6f;
                SeccionCuentaValor.BorderWidthTop = 1;
                PdfPCell SeccionFechaTitulo = new PdfPCell(new Phrase("Fecha Pago", _standardFont2));
                SeccionFechaTitulo.BorderWidth = 0;
                PdfPCell SeccionFechaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.FechaGeneracionREC, _standardFont_normal));
                SeccionFechaValor.BorderWidth = 0;
                PdfPCell SeccionNroFurTitulo = new PdfPCell(new Phrase("Nro Furs", _standardFont2));
                SeccionNroFurTitulo.BorderWidth = 0;
                PdfPCell SeccionNroFurValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.CodigoFur, _standardFont_normal));
                SeccionNroFurValor.BorderWidth = 0;
                PdfPCell SeccionSeentregoaTitulo = new PdfPCell(new Phrase("Se entregó a", _standardFont2));
                SeccionSeentregoaTitulo.BorderWidth = 0;
                PdfPCell SeccionSeentregoaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.EntregadoA, _standardFont_normal));
                SeccionSeentregoaValor.BorderWidth = 0;
                //////////////
                PdfPCell SeccionProveedorTitulo = new PdfPCell(new Phrase("Proveedor", _standardFont2));
                SeccionProveedorTitulo.BorderWidth = 0;
                PdfPCell SeccionProveedorValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.NombreProveedor, _standardFont_normal));
                SeccionProveedorValor.BorderWidth = 0;
                PdfPCell SeccionRucProveedorTitulo = new PdfPCell(new Phrase("RUC", _standardFont2));
                SeccionRucProveedorTitulo.BorderWidth = 0;
                PdfPCell SeccionRucProveedorValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.RucProveedor, _standardFont_normal));
                SeccionRucProveedorValor.BorderWidth = 0;
                ///////////////////
                PdfPCell SeccionTipoDocumentosTitulo = new PdfPCell(new Phrase("T. Documento", _standardFont2));
                SeccionTipoDocumentosTitulo.BorderWidth = 0;
                PdfPCell SeccionTipoDocumentosValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.TipoDocumentosSunat, _standardFont_normal));
                SeccionTipoDocumentosValor.BorderWidth = 0;
                PdfPCell SeccionComprobantesrTitulo = new PdfPCell(new Phrase("N° Comprobante", _standardFont2));
                SeccionComprobantesrTitulo.BorderWidth = 0;
                PdfPCell SeccionComprobantesrValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.Comprobantes, _standardFont_normal));
                SeccionComprobantesrValor.BorderWidth = 0;



                PdfPCell SeccionImporteTitulo = new PdfPCell(new Phrase("Importe", _standardFont2));
                SeccionImporteTitulo.BorderWidth = 0;
                PdfPCell SeccionImporteValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.MontoTotal + "   " + datosGenerarPdf.Moneda, _standardFont_normal));
                SeccionImporteValor.BorderWidth = 0;
                PdfPCell SeccionDetalleTitulo = new PdfPCell(new Phrase("Concepto", _standardFont2));
                SeccionDetalleTitulo.PaddingBottom = 43f;
                SeccionDetalleTitulo.BorderWidth = 0;
                PdfPCell SeccionDetalleValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.Detalle, _standardFont_normal));
                SeccionDetalleTitulo.PaddingBottom = 43f;
                SeccionDetalleValor.BorderWidth = 0;
                TablaBottom.AddCell(SeccionCuentaTitulo);
                TablaBottom.AddCell(SeccionCuentaValor);
                TablaBottom.AddCell(SeccionFechaTitulo);
                TablaBottom.AddCell(SeccionFechaValor);
                TablaBottom.AddCell(SeccionNroFurTitulo);
                TablaBottom.AddCell(SeccionNroFurValor);
                TablaBottom.AddCell(SeccionSeentregoaTitulo);
                TablaBottom.AddCell(SeccionSeentregoaValor);

                TablaBottom.AddCell(SeccionProveedorTitulo);
                TablaBottom.AddCell(SeccionProveedorValor);
                TablaBottom.AddCell(SeccionRucProveedorTitulo);
                TablaBottom.AddCell(SeccionRucProveedorValor);

                TablaBottom.AddCell(SeccionTipoDocumentosTitulo);
                TablaBottom.AddCell(SeccionTipoDocumentosValor);
                TablaBottom.AddCell(SeccionComprobantesrTitulo);
                TablaBottom.AddCell(SeccionComprobantesrValor);



                TablaBottom.AddCell(SeccionImporteTitulo);
                TablaBottom.AddCell(SeccionImporteValor);
                TablaBottom.AddCell(SeccionDetalleTitulo);
                TablaBottom.AddCell(SeccionDetalleValor);
                doc.Add(TablaBottom);
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new iTextSharp.text.Paragraph("  ________________________                             ________________________", _standardFont2));
                doc.Add(new iTextSharp.text.Paragraph("     ENTREGUE CONFORME                                       RECIBI CONFORME", _standardFont2));
                PdfPTable TablaBottom2 = new PdfPTable(2);
                TablaBottom2.WidthPercentage = 100;
                TablaBottom2.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths3 = new float[] { 48, 60 };
                TablaBottom2.SetWidths(widths3);
                PdfPCell SeccionCuentaTitulo2 = new PdfPCell(new Phrase(datosGenerarPdf.Responsable, _standardFont_normal));
                SeccionCuentaTitulo2.PaddingTop = 10f;
                SeccionCuentaTitulo2.BorderWidth = 0;
                PdfPCell SeccionCuentaValor2 = new PdfPCell(new Phrase("              " + "________________________", _standardFont_normal));
                SeccionCuentaValor2.BorderWidth = 0;
                SeccionCuentaValor2.PaddingTop = 10f;
                PdfPCell SeccionNroFurTitulo2 = new PdfPCell(new Phrase("DNI: ", _standardFont_normal));
                SeccionNroFurTitulo2.BorderWidth = 0;
                PdfPCell SeccionNroFurValor2 = new PdfPCell(new Phrase("              DNI: ", _standardFont_normal));
                SeccionNroFurValor2.BorderWidth = 0;
                TablaBottom2.AddCell(SeccionCuentaTitulo2);
                TablaBottom2.AddCell(SeccionCuentaValor2);
                TablaBottom2.AddCell(SeccionNroFurTitulo2);
                TablaBottom2.AddCell(SeccionNroFurValor2);
                doc.Add(TablaBottom2);
                doc.Close();
                writer.Close();
                doc.Dispose();

                return ms.ToArray();
            }
        }
    }
}
