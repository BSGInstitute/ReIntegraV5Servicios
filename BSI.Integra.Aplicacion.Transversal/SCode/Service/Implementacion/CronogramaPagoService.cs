using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CronogramaPagoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 05/08/2022
    /// <summary>
    /// Gestión general de T_CronogramaPago
    /// </summary>
    public class CronogramaPagoService : ICronogramaPagoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CronogramaPagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCronogramaPago, CronogramaPago>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CronogramaPago Add(CronogramaPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaPagoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CronogramaPago>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CronogramaPago Update(CronogramaPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaPagoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CronogramaPago>(modelo);
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
                _unitOfWork.CronogramaPagoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CronogramaPago> Add(List<CronogramaPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaPagoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CronogramaPago>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CronogramaPago> Update(List<CronogramaPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaPagoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CronogramaPago>>(modelo);
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
                _unitOfWork.CronogramaPagoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CronogramaPago
        /// </summary>
        /// <returns> List<CronogramaPagoDTO> </returns>
        public IEnumerable<CronogramaPagoDTO> ObtenerCronogramaPago()
        {
            try
            {
                return _unitOfWork.CronogramaPagoRepository.ObtenerCronogramaPago();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CronogramaPago para mostrarse en combo.
        /// </summary>
        /// <returns> List<CronogramaPagoComboDTO> </returns>
        public IEnumerable<CronogramaPagoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CronogramaPagoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cuotas e informacion de programas asociados a una Matricula Cabecera.
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de Matricula Cabecera</param>
        /// <returns> List<ProgramaCuotasDetalleDTO> </returns>
        public ProgramaCuotasDTO ObtenerProgramaCuotasPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.CronogramaPagoRepository.ObtenerProgramaCuotasPorIdMatriculaCabecera(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: griselberto huaman.
        /// Fecha: 11/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cuotas e informacion de programas asociados a una Matricula Cabecera.
        /// </summary>
        /// <param name="CodigoMatricula">codigo de Matricula Cabecera</param>
        /// <returns> List<ProgramaCuotasDetalleDTO> </returns>
        public object ObtenerCronogramaPagoPorCodigoMatricula(string CodigoMatricula)
        {
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var idMatriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();

                var _repCronogramaPagoDetalle = _unitOfWork.CronogramaPagoDetalleRepository;
                var respuesta = _repCronogramaPagoDetalle.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabeceraTemp.Id, x => new { x.Id, x.IdMatriculaCabecera, x.NroCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Saldo, x.TipoCuota, x.Moneda });
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: griselberto huaman.
        /// Fecha: 11/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cuotas e informacion de programas asociados a una Matricula Cabecera.
        /// </summary>
        /// <param name="CronogramaPago">codigo de Matricula Cabecera</param>
        /// <returns> List<ProgramaCuotasDetalleDTO> </returns>
        public object ActualizarCronogramaPago(CronogramaPagoAlumnoDTO CronogramaPago)
        {
            try
            {
                var _repCronogramaPago =_unitOfWork.CronogramaPagoRepository;
                var _repCronogramaPagoDetalle =_unitOfWork.CronogramaPagoDetalleRepository;
                var _repCronogramaPagoDetalleModLogFinal =_unitOfWork.CronogramaPagoDetalleModLogFinalRepository;
                var _repCronogramaPagoDetalleFinalRepositorio =_unitOfWork.CronogramaPagoDetalleFinalRepository;
                var _repCronogramaPagoDetalleOriginal =_unitOfWork.CronogramaPagoDetalleOriginalRepository;

                var idsCronogramaPagoDetalle = _repCronogramaPagoDetalle.GetBy(x => x.IdMatriculaCabecera == CronogramaPago.ListaCronogramaDetallePago.FirstOrDefault().IdMatriculaCabecera).Select(x => x.Id).ToList();
                var idMatriculaCabecera = CronogramaPago.ListaCronogramaDetallePago.FirstOrDefault().IdMatriculaCabecera;
                var listaRpta = _repCronogramaPagoDetalle.Delete(idsCronogramaPagoDetalle, CronogramaPago.NombreUsuario);

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var item in CronogramaPago.ListaCronogramaDetallePago)
                    {
                        TCronogramaPagoDetalleModLogFinal cronogramaPagoDetalleModLogFinalBO = new TCronogramaPagoDetalleModLogFinal()
                        {
                            IdMatriculaCabecera = item.IdMatriculaCabecera,
                            Fecha = DateTime.Now,
                            NroCuota = item.NroCuota,
                            NroSubCuota = 1,
                            FechaVencimiento = item.FechaVencimiento,
                            TotalPagar = item.TotalPagar,
                            Cuota = item.Cuota,
                            Mora = 0,
                            MontoPagado = 0,
                            Saldo = item.Saldo,
                            Cancelado = false,
                            TipoCuota = item.TipoCuota,
                            Moneda = item.Moneda,
                            FechaPago = null,
                            IdFormaPago = null,
                            FechaPagoBanco = null,
                            Ultimo = false,
                            Observaciones = null,
                            IdDocumentoPago = null,
                            NroDocumento = null,
                            MonedaPago = null,
                            TipoCambio = CronogramaPago.TipoCambio,
                            MensajeSistema = null,
                            FechaProcesoPago = null,
                            EstadoPrimerLog = "1",
                            Version = 0,
                            Aprobado = true,
                            Estado2 = true,
                            UsuarioCreacion = CronogramaPago.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = CronogramaPago.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        TCronogramaPagoDetalleFinal cronogramaPagoDetalleFinalBO = new TCronogramaPagoDetalleFinal()
                        {
                            IdMatriculaCabecera = item.IdMatriculaCabecera,
                            NroCuota = item.NroCuota,
                            NroSubCuota = 1,
                            FechaVencimiento = item.FechaVencimiento,
                            TotalPagar = item.TotalPagar,
                            Cuota = item.Cuota,
                            Saldo = item.Saldo,
                            MontoPagado = 0,
                            Cancelado = Convert.ToBoolean(0),
                            TipoCuota = item.TipoCuota,
                            Moneda = item.Moneda,
                            Mora = 0,
                            Version = 0,
                            Enviado = false,
                            Aprobado = true,
                            UsuarioCreacion = CronogramaPago.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = CronogramaPago.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };


                        TCronogramaPagoDetalle cronogramaPagoDetalleBO = new TCronogramaPagoDetalle()
                        {
                            IdMatriculaCabecera = item.IdMatriculaCabecera,
                            NroCuota = item.NroCuota,
                            FechaVencimiento = item.FechaVencimiento,
                            TotalPagar = item.TotalPagar,
                            Cuota = item.Cuota,
                            Saldo = item.Saldo,
                            TipoCuota = item.TipoCuota,
                            Moneda = item.Moneda,
                            Cancelado = false,
                            UsuarioCreacion = CronogramaPago.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = CronogramaPago.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        _repCronogramaPagoDetalle.Insert(cronogramaPagoDetalleBO);
                        _unitOfWork.Commit();

                        var cantidad = _repCronogramaPagoDetalleOriginal.GetBy(x => x.IdMatriculaCabecera == item.IdMatriculaCabecera && x.NroCuota == item.NroCuota).Count();
                        if (cantidad == 0)
                        {
                            TCronogramaPagoDetalleOriginal cronogramaPagoDetalleOriginalBO = new TCronogramaPagoDetalleOriginal()
                            {
                                IdMatriculaCabecera = item.IdMatriculaCabecera,
                                NroCuota = item.NroCuota,
                                NroSubCuota = 1,
                                FechaVencimiento = item.FechaVencimiento,
                                TotalPagar = item.TotalPagar,
                                Cuota = item.Cuota,
                                Saldo = item.Saldo,
                                Cancelado = false,
                                TipoCuota = item.TipoCuota,
                                Moneda = CronogramaPago.Moneda,
                                TipocCambio = CronogramaPago.TipoCambio,
                                UsuarioCreacion = CronogramaPago.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = CronogramaPago.NombreUsuario,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };
                            _repCronogramaPagoDetalleOriginal.Insert(cronogramaPagoDetalleOriginalBO);
                            _unitOfWork.Commit();
                        }

                        _repCronogramaPagoDetalleFinalRepositorio.Insert(cronogramaPagoDetalleFinalBO);
                        _unitOfWork.Commit();
                        _repCronogramaPagoDetalleModLogFinal.Insert(cronogramaPagoDetalleModLogFinalBO);
                        _unitOfWork.Commit();
                    }
                    bool afirmacion = true;
                    var cronogramaPago = _repCronogramaPago.FirstBy(x => x.IdMatriculaCabecera == CronogramaPago.ListaCronogramaDetallePago[0].IdMatriculaCabecera);
                    var version = _repCronogramaPagoDetalleFinalRepositorio.ObtenerMaximaVersionCronograma(CronogramaPago.ListaCronogramaDetallePago[0].IdMatriculaCabecera);
                    var cronogramaPagoDetFin = _repCronogramaPagoDetalleFinalRepositorio.GetBy(x => x.IdMatriculaCabecera == CronogramaPago.ListaCronogramaDetallePago[0].IdMatriculaCabecera && x.Version == version).OrderBy(x => x.NroCuota).FirstOrDefault();
                    cronogramaPago.ConCuotaInicial = true;
                    cronogramaPago.CuotaInicial = cronogramaPagoDetFin.Cuota;
                    cronogramaPago.FechaModificacion = DateTime.Now;
                    cronogramaPago.UsuarioModificacion = CronogramaPago.NombreUsuario;
                    _repCronogramaPago.Update(cronogramaPago);
                    _unitOfWork.Commit();
                    scope.Complete();

                    return new { afirmacion, idMatriculaCabecera };                
                }

            }
            catch (Exception e)
            {

                bool afirmacion = false;
                return new { afirmacion};
            }
        }       
    }
}
