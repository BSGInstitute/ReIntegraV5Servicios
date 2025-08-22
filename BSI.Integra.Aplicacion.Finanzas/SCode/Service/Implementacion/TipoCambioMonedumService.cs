using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: TipoCambioMonedumService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_TipoCambioMonedum
    /// </summary>
    public class TipoCambioMonedumService : ITipoCambioMonedumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper mapperTipoCambio;
        private readonly int _idMonedaSoles = 20;
        private readonly int _idMonedaCol = 10;

        public TipoCambioMonedumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoCambioMonedum, TipoCambioMonedum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            mapperTipoCambio = new Mapper(config);
        }

        #region Metodos Base
        public TipoCambioMonedum Add(TipoCambioMonedum entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioMonedumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCambioMonedum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoCambioMonedum Update(TipoCambioMonedum entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioMonedumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCambioMonedum>(modelo);
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
                _unitOfWork.TipoCambioMonedumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCambioMonedum> Add(List<TipoCambioMonedum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioMonedumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCambioMonedum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCambioMonedum> Update(List<TipoCambioMonedum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioMonedumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCambioMonedum>>(modelo);
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
                _unitOfWork.TipoCambioMonedumRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoCambioMonedum.
        /// </summary>
        /// <returns> List<TipoCambioMonedumDTO> </returns>
        public IEnumerable<TipoCambioMonedumDTO> ObtenerTipoCambioMonedum()
        {
            try
            {
                return _unitOfWork.TipoCambioMonedumRepository.ObtenerTipoCambioMonedum();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza en T_TipoCambioMoneda,T_TipoCambio,T_TipoCAmbioCol.
        /// </summary>
        /// <returns> TTipoCambioMonedum </returns>
        /// <param name="TipoCambioMonedaDTO">Grupo de Datos, para guardar.</param>
        public object ActualizarGeneral(FiltroTipoCambioMonedaDTO TipoCambioMonedaDTO)
        {

            try
            {
                var _repTipoCambioMoneda = _unitOfWork.TipoCambioMonedumRepository;
                var _repTipoCambio = _unitOfWork.TipoCambioRepository;
                var servicioTipoCambio = new TipoCambioService(_unitOfWork);
                var _repTipoCambioCol = _unitOfWork.TipoCambioColRepository;
                var servicioTipoCambioCol = new TipoCambioColService(_unitOfWork);


                if (_repTipoCambioMoneda.Exist(TipoCambioMonedaDTO.Id))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var TtipoCambioMoneda = _repTipoCambioMoneda.FirstById(TipoCambioMonedaDTO.Id);
                        var tipoCambioMoneda = _mapper.Map<TipoCambioMonedum>(TtipoCambioMoneda);

                        tipoCambioMoneda.MonedaAdolar = TipoCambioMonedaDTO.MonedaAdolar;
                        tipoCambioMoneda.DolarAmoneda = TipoCambioMonedaDTO.DolarAmoneda;
                        tipoCambioMoneda.FechaModificacion = DateTime.Now;
                        tipoCambioMoneda.UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario;

                        var RPTTCM = this.Update(tipoCambioMoneda);
                        tipoCambioMoneda = RPTTCM;
                        if (!(RPTTCM == null))
                        {

                            if (tipoCambioMoneda.IdTipoCambio == null && tipoCambioMoneda.IdTipoCambioCol == null)
                            {
                                //Es un registro de tabla misma no viene ni de T_TipoCambio, ni T_TipoCambioCol
                                //no hacemos nada
                            }
                            else if (tipoCambioMoneda.IdTipoCambio != null && tipoCambioMoneda.IdTipoCambioCol == null)
                            {
                                //viene de tipo cambio
                                //actualizacion
                                var TtipoCambio = _repTipoCambio.GetBy(x => x.Id == tipoCambioMoneda.IdTipoCambio).FirstOrDefault();
                                var tipoCambio = servicioTipoCambio.mapperTipoCambio.Map<TipoCambio>(TtipoCambio);
                                tipoCambio.SolesDolares = TipoCambioMonedaDTO.MonedaAdolar;
                                tipoCambio.DolaresSoles = TipoCambioMonedaDTO.DolarAmoneda;
                                tipoCambio.UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario;
                                tipoCambio.FechaModificacion = DateTime.Now;
                                tipoCambio = servicioTipoCambio.Update(tipoCambio);
                            }
                            else if (tipoCambioMoneda.IdTipoCambio == null && tipoCambioMoneda.IdTipoCambioCol != null)
                            {
                                //viene de tipo cambio col
                                //actualizacion
                                var TtipoCambioCol = _repTipoCambioCol.GetBy(x => x.Id == tipoCambioMoneda.IdTipoCambioCol).FirstOrDefault();
                                var tipoCambioCol = servicioTipoCambioCol.mapperTipoCambioCol.Map<TipoCambioCol>(TtipoCambioCol);

                                tipoCambioCol.PesosDolares = TipoCambioMonedaDTO.MonedaAdolar;
                                tipoCambioCol.DolaresPesos = TipoCambioMonedaDTO.DolarAmoneda;
                                tipoCambioCol.UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario;
                                tipoCambioCol.FechaModificacion = DateTime.Now;

                                tipoCambioCol = servicioTipoCambioCol.Update(tipoCambioCol);
                            }
                        }
                        else
                        {
                            throw new Exception("error al actualizar TipoCambioMoneda");
                        }
                        scope.Complete();
                        return tipoCambioMoneda;
                    }
                }
                else
                {
                    throw new Exception("Registro no existente");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta valores en T_TipoCambio,T_TipoCambioCol,T_TipoCambioMoneda.
        /// </summary>
        /// <returns> Retorna un Objeto como respuesta correcta, o retorna mensaje segun el error </returns>
        /// <paramref name="tipoCambioMonedaDTO"/> Grupo de Parametros.
        public object InsertarGeneral(FiltroTipoCambioMonedaDTO tipoCambioMonedaDTO)
        {
            try
            {
                var _repTipoCambioMoneda = _unitOfWork.TipoCambioMonedumRepository;
                var _repPeriodo = _unitOfWork.PeriodoRepository;
                var _repTipoCambioCol = _unitOfWork.TipoCambioColRepository;
                var serviceTipoCambioCol = new TipoCambioColService(_unitOfWork);
                var _repTipoCambio = _unitOfWork.TipoCambioRepository;
                var serviceTipoCambio = new TipoCambioService(_unitOfWork);

                if (tipoCambioMonedaDTO.IdPeriodo != 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var periodo = _repPeriodo.GetBy(x => x.Id == tipoCambioMonedaDTO.IdPeriodo, x => new { x.FechaInicialFinanzas, x.FechaFinFinanzas }).ToList().FirstOrDefault();
                        var dias = Enumerable.Range(0, 1 + periodo.FechaFinFinanzas.Subtract(periodo.FechaInicialFinanzas).Days).Select(offset => periodo.FechaInicialFinanzas.AddDays(offset)).ToList();

                        List<TipoCambioMonedum> listaTipoCambio = new List<TipoCambioMonedum>();

                        var listaFechasEliminar = _repTipoCambio.GetBy(x => x.Fecha >= dias.First() && x.Fecha <= dias.Last()).ToList();
                        var listaFechasEliminarCol = _repTipoCambioCol.GetBy(x => x.Fecha >= dias.First() && x.Fecha <= dias.Last()).ToList();

                        var listadoEliminarFechaTipoCambioMoneda = listaFechasEliminar.Select(x => x.Id).ToList();
                        var any = listadoEliminarFechaTipoCambioMoneda.Any(w => 1 == w);
                        var listadoEliminarFechaMoneda = _repTipoCambioMoneda.GetBy(x => listadoEliminarFechaTipoCambioMoneda.Any(w => (int)x.IdTipoCambio == w)).ToList();

                        foreach (var item in dias)
                        {
                            var tipoCambioMonedaFecha = _repTipoCambioMoneda.GetBy(x => x.IdMoneda == tipoCambioMonedaDTO.IdMoneda && x.Fecha == item).ToList();
                            if (listaFechasEliminar.Where(x => x.Fecha == item).Count() == 0 || tipoCambioMonedaFecha.Count() == 0 || tipoCambioMonedaFecha == null)//si no existe para esa fecha ese tipo de moneda
                            {
                                TipoCambioMonedum tipoCambioMoneda = new TipoCambioMonedum()
                                {
                                    MonedaAdolar = tipoCambioMonedaDTO.MonedaAdolar,
                                    DolarAmoneda = tipoCambioMonedaDTO.DolarAmoneda,
                                    Fecha = item,
                                    IdMoneda = tipoCambioMonedaDTO.IdMoneda,
                                    IdTipoCambio = null,
                                    IdTipoCambioCol = null,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                    UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                };

                                if (tipoCambioMonedaDTO.IdMoneda != _idMonedaSoles && tipoCambioMonedaDTO.IdMoneda != _idMonedaCol)
                                {
                                    tipoCambioMoneda = this.Add(tipoCambioMoneda);
                                }
                                else if (tipoCambioMonedaDTO.IdMoneda == _idMonedaSoles)
                                {
                                    TipoCambio tipoCambio = new TipoCambio()
                                    {
                                        SolesDolares = tipoCambioMonedaDTO.MonedaAdolar,
                                        DolaresSoles = tipoCambioMonedaDTO.DolarAmoneda,
                                        Fecha = item,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                        UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                    };

                                    tipoCambio = serviceTipoCambio.Add(tipoCambio);
                                    tipoCambioMoneda.IdTipoCambio = tipoCambio.Id;
                                    tipoCambioMoneda = this.Add(tipoCambioMoneda);

                                }
                                else if (tipoCambioMonedaDTO.IdMoneda == _idMonedaCol)
                                {
                                    TipoCambioCol tipoCambioCol = new TipoCambioCol()
                                    {
                                        PesosDolares = tipoCambioMonedaDTO.MonedaAdolar,
                                        DolaresPesos = tipoCambioMonedaDTO.DolarAmoneda,
                                        Fecha = item,
                                        IdMoneda = tipoCambioMonedaDTO.IdMoneda,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                        UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                    };

                                    tipoCambioCol = serviceTipoCambioCol.Add(tipoCambioCol);
                                    tipoCambioMoneda.IdTipoCambioCol = tipoCambioCol.Id;
                                    tipoCambioMoneda = this.Add(tipoCambioMoneda);
                                }

                                if (!(tipoCambioMoneda == null))
                                {

                                    listaTipoCambio.Add(tipoCambioMoneda);

                                }
                                else
                                {
                                    throw new Exception("Error al insertar el Tipo de Cambio Moneda");
                                }

                            }
                            else
                            {//ya existe entonces actualizamos

                                if (tipoCambioMonedaDTO.IdMoneda != _idMonedaCol && tipoCambioMonedaDTO.IdMoneda == _idMonedaSoles)
                                {
                                    var tipoCambioMonedaOtro = _mapper.Map<TipoCambioMonedum>(tipoCambioMonedaFecha);
                                    tipoCambioMonedaOtro.MonedaAdolar = tipoCambioMonedaDTO.MonedaAdolar;
                                    tipoCambioMonedaOtro.DolarAmoneda = tipoCambioMonedaDTO.DolarAmoneda;
                                    tipoCambioMonedaOtro.FechaModificacion = DateTime.Now;
                                    tipoCambioMonedaOtro.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;
                                    var rpt1 = this.Update(tipoCambioMonedaOtro);
                                    if (rpt1 == null) throw new Exception("Error al insertar Actualizacion Tipo Cambio Moneda");
                                }
                                else if (tipoCambioMonedaDTO.IdMoneda == _idMonedaSoles)
                                {
                                    var TtipoCambioSoles = listaFechasEliminar.Where(x => x.Fecha == item).FirstOrDefault();
                                    var tipoCambioSoles = _mapper.Map<TipoCambio>(TtipoCambioSoles);
                                    tipoCambioSoles.SolesDolares = tipoCambioMonedaDTO.MonedaAdolar;
                                    tipoCambioSoles.DolaresSoles = tipoCambioMonedaDTO.DolarAmoneda;
                                    tipoCambioSoles.FechaModificacion = DateTime.Now;
                                    tipoCambioSoles.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;

                                    var RptTC = serviceTipoCambio.Update(tipoCambioSoles);
                                    tipoCambioSoles = RptTC;
                                    if (!(RptTC == null))
                                    {
                                        var TtipoCambioMoneda = _repTipoCambioMoneda.GetBy(x => x.IdTipoCambio == tipoCambioSoles.Id && x.IdTipoCambioCol == null).FirstOrDefault();
                                        var tipoCambioMoneda = _mapper.Map<TipoCambioMonedum>(TtipoCambioMoneda);
                                        tipoCambioMoneda.MonedaAdolar = tipoCambioMonedaDTO.MonedaAdolar;
                                        tipoCambioMoneda.DolarAmoneda = tipoCambioMonedaDTO.DolarAmoneda;
                                        tipoCambioMoneda.FechaModificacion = DateTime.Now;
                                        tipoCambioMoneda.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;
                                        var rpt = this.Update(tipoCambioMoneda);
                                        tipoCambioMoneda = rpt;

                                        if (rpt == null) throw new Exception("Error al insertar Actualizacion TipoCambioMoneda");
                                    }
                                    else
                                    {
                                        throw new Exception("Error al insertar Actualizacion TipoCambioSoles"); ;
                                    }
                                }
                                else if (tipoCambioMonedaDTO.IdMoneda == _idMonedaCol)
                                {
                                    var TtipoCambioCol = listaFechasEliminarCol.Where(x => x.Fecha == item).FirstOrDefault();
                                    var tipoCambioCol = serviceTipoCambioCol.mapperTipoCambioCol.Map<TipoCambioCol>(TtipoCambioCol);
                                    tipoCambioCol.PesosDolares = tipoCambioMonedaDTO.MonedaAdolar;
                                    tipoCambioCol.DolaresPesos = tipoCambioMonedaDTO.DolarAmoneda;
                                    tipoCambioCol.FechaModificacion = DateTime.Now;
                                    tipoCambioCol.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;

                                    var RptTC = serviceTipoCambioCol.Update(tipoCambioCol);
                                    tipoCambioCol = RptTC;
                                    if (!(RptTC == null))
                                    {
                                        var TtipoCambioMoneda = _repTipoCambioMoneda.GetBy(x => x.IdTipoCambioCol == tipoCambioCol.Id && x.IdTipoCambio == null).FirstOrDefault();
                                        var tipoCambioMoneda = _mapper.Map<TipoCambioMonedum>(TtipoCambioMoneda);
                                        tipoCambioMoneda.MonedaAdolar = tipoCambioMonedaDTO.MonedaAdolar;
                                        tipoCambioMoneda.DolarAmoneda = tipoCambioMonedaDTO.DolarAmoneda;
                                        tipoCambioMoneda.FechaModificacion = DateTime.Now;
                                        tipoCambioMoneda.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;
                                        var rpt = this.Update(tipoCambioMoneda);
                                        tipoCambioMoneda = rpt;

                                        if (rpt == null) throw new Exception("Error al insertar Actualizacion TipoCambioMoneda");
                                    }
                                    else
                                    {
                                        throw new Exception("Error al insertar Actualizacion TipoCambioDolares"); ;
                                    }
                                }
                            }
                        }
                        scope.Complete();
                        return listaTipoCambio.Select(x => new { x.Id, x.IdMoneda, x.DolarAmoneda, x.MonedaAdolar, x.Fecha });
                    }
                }
                else if (tipoCambioMonedaDTO.Fecha != null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var tempDiasExistente = _repTipoCambioMoneda.GetBy(x => x.IdMoneda == tipoCambioMonedaDTO.IdMoneda && x.Fecha == tipoCambioMonedaDTO.Fecha).ToList();
                        ///Calcula que el registro a insertar no exista
                        if (tempDiasExistente.Count() == 0 || tempDiasExistente == null)
                        {
                            var idTipoCambio = 0;
                            var idTipoCambioCol = 0;
                            if (tipoCambioMonedaDTO.IdMoneda == _idMonedaSoles)
                            {
                                TipoCambio tipoCambio = new TipoCambio()
                                {
                                    SolesDolares = tipoCambioMonedaDTO.MonedaAdolar,
                                    DolaresSoles = tipoCambioMonedaDTO.DolarAmoneda,
                                    Fecha = tipoCambioMonedaDTO.Fecha.Value,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                    UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                };

                                tipoCambio = serviceTipoCambio.Add(tipoCambio);
                                idTipoCambio = tipoCambio.Id;
                            }
                            else if (tipoCambioMonedaDTO.IdMoneda == _idMonedaCol)
                            {
                                TipoCambioCol tipoCambioCol = new TipoCambioCol()
                                {
                                    PesosDolares = tipoCambioMonedaDTO.MonedaAdolar,
                                    DolaresPesos = tipoCambioMonedaDTO.DolarAmoneda,
                                    Fecha = tipoCambioMonedaDTO.Fecha.Value,
                                    IdMoneda = tipoCambioMonedaDTO.IdMoneda,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                    UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                };
                                tipoCambioCol = serviceTipoCambioCol.Add(tipoCambioCol);
                                idTipoCambioCol = tipoCambioCol.Id;
                            }
                            TipoCambioMonedum tipoCambioMoneda = new TipoCambioMonedum()
                            {
                                MonedaAdolar = tipoCambioMonedaDTO.MonedaAdolar,
                                DolarAmoneda = tipoCambioMonedaDTO.DolarAmoneda,
                                Fecha = tipoCambioMonedaDTO.Fecha.Value,
                                IdMoneda = tipoCambioMonedaDTO.IdMoneda,
                                IdTipoCambio = idTipoCambio == 0 ? null : idTipoCambio,
                                IdTipoCambioCol = idTipoCambioCol == 0 ? null : idTipoCambioCol,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                            };
                            tipoCambioMoneda = this.Add(tipoCambioMoneda);

                            scope.Complete();
                            return tipoCambioMoneda;
                        }
                        else
                        {
                            throw new Exception("Existe un tipo de cambio para esa fecha!");
                        }
                    }
                }
                else
                {
                    throw new Exception("Error valores invalidos");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// obtiene el tipo de cambio de la fecha actual, por moneda.
        /// </summary>
        /// <returns> Ture, ó error. </returns>
        /// <param name="Usuario">Usuario de eliminacion</param>
        /// <param name="Id">Id de registro a eleiminar</param>
        public TipoCambioFechaDTO ObtenerTasaCambioMoneda(int idMoneda)
        {
            try
            {
                return _unitOfWork.TipoCambioMonedumRepository.ObtenerTasaCambioMoneda(idMoneda);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza en T_TipoCambioMoneda,T_TipoCambio,T_TipoCAmbioCol.
        /// </summary>
        /// <returns> Ture, ó error. </returns>
        /// <param name="Usuario">Usuario de eliminacion</param>
        /// <param name="Id">Id de registro a eleiminar</param>
        public bool EliminarGeneral(int Id, string Usuario)
        {

            try
            {
                var _repTipoCambioMoneda = _unitOfWork.TipoCambioMonedumRepository;
                var _repTipoCambio = _unitOfWork.TipoCambioRepository;
                var servicioTipoCambio = new TipoCambioService(_unitOfWork);
                var _repTipoCambioCol = _unitOfWork.TipoCambioColRepository;
                var servicioTipoCambioCol = new TipoCambioColService(_unitOfWork);


                if (_repTipoCambioMoneda.Exist(Id))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var TtipoCambioMoneda = _repTipoCambioMoneda.GetBy(x => x.Id == Id).FirstOrDefault();
                        var tipoCambioMoneda = _mapper.Map<TipoCambioMonedum>(TtipoCambioMoneda);
                        var RPT = this.Delete(Id, Usuario);
                        if (RPT == true)
                        {
                            if (tipoCambioMoneda.IdTipoCambio == null && tipoCambioMoneda.IdTipoCambioCol == null)
                            {
                                //Es un registro de tabla misma no viene ni de T_TipoCambio, ni T_TipoCambioCol
                                //no hacemos nada
                            }
                            else if (tipoCambioMoneda.IdTipoCambio != null && tipoCambioMoneda.IdTipoCambioCol == null)
                            {
                                //viene de tipo cambio
                                //eliminamos de tipo cambio
                                var listadoTipoCambio = _repTipoCambio.GetBy(x => x.Id == tipoCambioMoneda.IdTipoCambio, x => new { x.Id }).ToList();
                                foreach (var item in listadoTipoCambio)
                                {
                                    var RPTCambio = servicioTipoCambio.Delete(item.Id, Usuario);
                                    if (RPTCambio) RPTCambio = false;
                                    else throw new Exception("Fallo al eliminar Tipo de Cambio");
                                }

                            }
                            else if (tipoCambioMoneda.IdTipoCambio == null && tipoCambioMoneda.IdTipoCambioCol != null)
                            {
                                //viene de tipo cambio col
                                //eliminamos de tipo cambio moneda
                                var listadoTipoCambioCol = _repTipoCambioCol.GetBy(x => x.Id == tipoCambioMoneda.IdTipoCambioCol, x => new { x.Id }).ToList();
                                foreach (var item in listadoTipoCambioCol)
                                {
                                    var RPTCambioCol = servicioTipoCambioCol.Delete(item.Id, Usuario);
                                    if (RPTCambioCol) RPTCambioCol = false;
                                    else throw new Exception("Fallo al eliminar Tipo de CambioCol");
                                }
                            }
                            scope.Complete();
                            return true;
                        }
                        else
                        {
                            throw new Exception("Fallo al eliminar Tipo de Cambio Moneda");
                        }

                    }
                }
                else
                {
                    throw new Exception("Registro no existente");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}


