using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReporteIngresoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteIngreso
    /// </summary>
    public class ReporteIngresoService : IReporteIngresoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteIngresoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReporteIngresoCongelamiento, ReporteIngresoCongelamientoRecibidoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ReporteIngresoCongelamiento, TReporteIngresoCongelamiento>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReporteIngresoRecibidoDTO, ReporteIngreso>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// <summary>
        /// Obtiene los registros,
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public IEnumerable<ReporteIngresoCongelamientoDTO> ObtenerReporteIngresoCongelamiento()
        {
            try
            {
                return _unitOfWork.ReporteIngresoCongelamientoRepository.ObtenerReporteIngresoCongelamiento();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Inserta un Registro
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public ReporteIngresoCongelamiento InsertarReporteIngresoCongelamiento(ReporteIngresoCongelamientoRecibidoDTO data,string Usuario)
        {
            try
            {
                var rep = _unitOfWork.ReporteIngresoCongelamientoRepository;
                TReporteIngresoCongelamiento nuevaData = _mapper.Map<TReporteIngresoCongelamiento>(data);

                nuevaData.Estado = true;
                nuevaData.FechaCreacion = DateTime.Now;
                nuevaData.FechaModificacion = DateTime.Now;
                nuevaData.FechaCongelamiento = DateTime.Now;
                nuevaData.UsuarioModificacion = Usuario;
                nuevaData.UsuarioCreacion = Usuario;
                var modelo = rep.Insert(nuevaData);
                _unitOfWork.Commit();
                return _mapper.Map<ReporteIngresoCongelamiento>(nuevaData);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }        
        }

        /// <summary>
        /// Elimina el registrso
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public bool EliminarReporteIngresoCongelamiento(int Id,string Usuario)
        {
            try
            {
                _unitOfWork.ReporteIngresoCongelamientoRepository.Delete(Id, Usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosVentas(FiltroFechaDTO Filtro)
        {
            try
            {
                return _unitOfWork.ReporteIngresoRepository.ObtenerReporteIngresosVentas(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosOperaciones(FiltroFechaDTO Filtro)
        {
            try
            {
                return _unitOfWork.ReporteIngresoRepository.ObtenerReporteIngresosOperaciones(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerReporteIngresosOperacionesTipoCambio(FiltroFechaDTO Filtro)
        {
            try
            {
                return _unitOfWork.ReporteIngresoRepository.ObtenerReporteIngresosOperacionesTipoCambio(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosOtrosIngresos(FiltroFechaDTO Filtro)
        {
            try
            {
                return _unitOfWork.ReporteIngresoRepository.ObtenerReporteIngresosOtrosIngresos(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresos(FiltroFechaDTO Filtro)
        {
            try
            {
                return _unitOfWork.ReporteIngresoRepository.ObtenerPagosIngresos(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosPosterior(FiltroFechaDTO Filtro)
        {
            try
            {
                return _unitOfWork.ReporteIngresoRepository.ObtenerPagosIngresosPosterior(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosAnterior(FiltroFechaDTO Filtro)
        {
            try
            {
                var anterior = Filtro.FechaInicio.Date.AddMonths(-2);
                Filtro.FechaFin = Filtro.FechaInicio.Date.AddHours(00).AddMinutes(00).AddSeconds(00);
                Filtro.FechaInicio = anterior;
                return _unitOfWork.ReporteIngresoRepository.ObtenerPagosIngresosAnterior(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosGestionCobranza(FiltroFechaDTO Filtro)
        {
            try
            {
                return _unitOfWork.ReporteIngresoRepository.ObtenerPagosIngresosGestionCobranza(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosTasasAcademicas(FiltroFechaDTO Filtro)
        {
            try
            {
                return _unitOfWork.ReporteIngresoRepository.ObtenerPagosTasasAcademicas(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosAnteriorConDeposito(FiltroFechaDTO Filtro)
        {
            try
            {
                var anterior = Filtro.FechaInicio.Date.AddMonths(-2);
                Filtro.FechaFin = Filtro.FechaInicio;
                Filtro.FechaInicio = anterior;

                return _unitOfWork.ReporteIngresoRepository.ObtenerPagosIngresosAnteriorConDeposito(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosPosteriorConDeposito(FiltroFechaDTO Filtro)
        {
            try
            {
                return _unitOfWork.ReporteIngresoRepository.ObtenerPagosIngresosPosteriorConDeposito(Filtro);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<ReporteIngresosDetalleDTO> ObtenerReporteIngresosFinal(ReporteCompletoDTO data)
        {
            try
            {
                var repFeriadoRep = _unitOfWork.FeriadoRepository;

                var ItemVentas = data.resultVentas.Where(x => x.FechaPago.Value >= data.Filtro.FechaInicio && x.FechaPago.Value <= data.Filtro.FechaFin).ToList(); // Extrae los pagos que se dieron en el periodo de tiempo seleccionado que NO sean INHOUSE
                var ItemOperacionesAdelanto = data.resultOperaciones.Where(x => x.FechaCuota.Value >= data.Filtro.FechaFin).ToList();
                var ItemOperacionesReal = data.resultOperaciones.Where(x => x.FechaCuota.Value >= data.Filtro.FechaInicio && x.FechaCuota.Value <= data.Filtro.FechaFin).ToList();
                var ItemOperacionesRecuperaciones = data.resultOperaciones.Where(x => x.FechaCuota.Value < data.Filtro.FechaInicio).ToList();
                var ItemOperacionesInHouse = data.resultOperaciones.Where(x => x.IdCategoriaOrigen == 1).ToList();

                var resultOtrosIngresos = data.resultOtrosIngresosEgresos.Where(x => x.IdTipoMovimientoCaja == 1).ToList(); //Extrae Solo Los INGRESOS
                var resultOtrosEgresos = data.resultOtrosIngresosEgresos.Where(x => x.IdTipoMovimientoCaja == 2).ToList();//Extrae Solo Los EGRESOS
                var resultTipoCambio = data.resultOperacionesTipoCambio.Where(x => x.FechaPagoOriginal.Value >= data.Filtro.FechaInicio && x.FechaPagoOriginal.Value <= data.Filtro.FechaFin).ToList(); //pagos con nuevos

                var ItemReportePagosPagos = data.resultReportePagos.Where(x => x.FechaPagoOriginal.Value >= data.Filtro.FechaInicio && x.FechaPagoOriginal.Value <= data.Filtro.FechaFin).ToList();

                var resultOtrosIngresosPorFechaPago = resultOtrosIngresos.Where(x => x.FechaPago.Value >= data.Filtro.FechaInicio && x.FechaPagoOriginal.Value <= data.Filtro.FechaFin).ToList();
                var resultOtrosEgresosPorFechaPago = resultOtrosEgresos.Where(x => x.FechaPago.Value >= data.Filtro.FechaInicio && x.FechaPagoOriginal.Value <= data.Filtro.FechaFin).ToList();

                var ListFeriados = repFeriadoRep.GetBy(x => x.Estado == true).ToList();

                //---------------------------------------------------------------
                //---------------------------------------------------------------

                DateTime? FechaDisponibleOriginal = null;
                DateTime? FechaIngresoEnCuentaOriginal = null;
                foreach (var item in data.resultReportePagos)
                {

                    //Aqui les calculo a todos el porcentaje de cobro 
                    item.TotalPagadoDisponible = item.TotalPagado * (item.PorcentajeCobro / 100);
                    //item.Cuota = item.Cuota * (item.PorcentajeCobro / 100);
                    //Fin Aqui les calculo a todos el porcentaje de cobro

                    FechaDisponibleOriginal = item.FechaDisponible;
                    FechaIngresoEnCuentaOriginal = item.FechaDepositaron;

                    if (item.CuentaFeriados == false && item.ConsideraDiasHabilesLV == false && item.ConsideraDiasHabilesLS == false)
                    {
                        item.FechaDepositaron = item.FechaPagoReal;
                        item.FechaDisponible = item.FechaPagoReal;
                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(item.DiasDeposito.Value);
                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(item.DiasDisponible.Value);
                    }
                    else
                    {
                        //deposito
                        if (item.DiasDeposito == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDepositaron = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && item.ConsideraDiasHabilesLS == true) || ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && item.ConsideraDiasHabilesLV == true))
                                    {
                                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDepositaron = item.FechaPagoReal;
                            while (item.DiasDeposito > 0)
                            {
                                item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }
                                else
                                {
                                    if (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsideraDiasHabilesLV == true && (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsideraDiasHabilesLS == true && item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                        else
                                            item.DiasDeposito = item.DiasDeposito - 1;
                                    }
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }

                            }
                        }

                        //disponible
                        if (item.DiasDisponible == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDisponible = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && item.ConsideraDiasHabilesLS == true) || ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && item.ConsideraDiasHabilesLV == true))
                                    {
                                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDisponible = item.FechaPagoReal;
                            while (item.DiasDisponible > 0)
                            {
                                item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }
                                else
                                {
                                    if (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsideraDiasHabilesLV == true && (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsideraDiasHabilesLS == true && item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                        else
                                            item.DiasDisponible = item.DiasDisponible - 1;
                                    }
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }

                            }
                        }

                    }

                }
                foreach (var item in data.resultReportePagosPosterior)
                {

                    //Aqui les calculo a todos el porcentaje de cobro 
                    item.TotalPagadoDisponible = item.TotalPagado * (item.PorcentajeCobro / 100);
                    //item.Cuota = item.Cuota * (item.PorcentajeCobro / 100);
                    //Fin Aqui les calculo a todos el porcentaje de cobro

                    FechaDisponibleOriginal = item.FechaDisponible;
                    FechaIngresoEnCuentaOriginal = item.FechaDepositaron;

                    if (item.CuentaFeriados == false && item.ConsideraDiasHabilesLV == false && item.ConsideraDiasHabilesLS == false)
                    {
                        item.FechaDepositaron = item.FechaPagoReal;
                        item.FechaDisponible = item.FechaPagoReal;
                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(item.DiasDeposito.Value);
                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(item.DiasDisponible.Value);
                    }
                    else
                    {
                        //deposito
                        if (item.DiasDeposito == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDepositaron = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && item.ConsideraDiasHabilesLS == true) || ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && item.ConsideraDiasHabilesLV == true))
                                    {
                                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDepositaron = item.FechaPagoReal;
                            while (item.DiasDeposito > 0)
                            {
                                item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }
                                else
                                {
                                    if (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsideraDiasHabilesLV == true && (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsideraDiasHabilesLS == true && item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                        else
                                            item.DiasDeposito = item.DiasDeposito - 1;
                                    }
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }

                            }
                        }

                        //disponible
                        if (item.DiasDisponible == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDisponible = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && item.ConsideraDiasHabilesLS == true) || ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && item.ConsideraDiasHabilesLV == true))
                                    {
                                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDisponible = item.FechaPagoReal;
                            while (item.DiasDisponible > 0)
                            {
                                item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }
                                else
                                {
                                    if (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsideraDiasHabilesLV == true && (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsideraDiasHabilesLS == true && item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                        else
                                            item.DiasDisponible = item.DiasDisponible - 1;
                                    }
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }

                            }
                        }

                    }
                }

                foreach (var itemanterior in data.resultReportePagosAnterior)
                {

                    //Aqui les calculo a todos el porcentaje de cobro 
                    itemanterior.TotalPagadoDisponible = itemanterior.TotalPagado * (itemanterior.PorcentajeCobro / 100);
                    //item.Cuota = item.Cuota * (item.PorcentajeCobro / 100);
                    //Fin Aqui les calculo a todos el porcentaje de cobro

                    FechaDisponibleOriginal = itemanterior.FechaDisponible;
                    FechaIngresoEnCuentaOriginal = itemanterior.FechaDepositaron;

                    if (itemanterior.CuentaFeriados == false && itemanterior.ConsideraDiasHabilesLV == false && itemanterior.ConsideraDiasHabilesLS == false)
                    {
                        itemanterior.FechaDepositaron = itemanterior.FechaPagoReal;
                        itemanterior.FechaDisponible = itemanterior.FechaPagoReal;
                        itemanterior.FechaDepositaron = itemanterior.FechaDepositaron.Value.AddDays(itemanterior.DiasDeposito.Value);
                        itemanterior.FechaDisponible = itemanterior.FechaDisponible.Value.AddDays(itemanterior.DiasDisponible.Value);
                    }
                    else
                    {
                        //deposito
                        if (itemanterior.DiasDeposito == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            itemanterior.FechaDepositaron = itemanterior.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == itemanterior.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itemanterior.IdCiudad).FirstOrDefault() != null) && itemanterior.CuentaFeriados == true)
                                {
                                    itemanterior.FechaDepositaron = itemanterior.FechaDepositaron.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && itemanterior.ConsideraDiasHabilesLS == true) || ((itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && itemanterior.ConsideraDiasHabilesLV == true))
                                    {
                                        itemanterior.FechaDepositaron = itemanterior.FechaDepositaron.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            itemanterior.FechaDepositaron = itemanterior.FechaPagoReal;
                            while (itemanterior.DiasDeposito > 0)
                            {
                                itemanterior.FechaDepositaron = itemanterior.FechaDepositaron.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == itemanterior.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itemanterior.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (itemanterior.CuentaFeriados == true)
                                        itemanterior.DiasDeposito = itemanterior.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        itemanterior.DiasDeposito = itemanterior.DiasDeposito - 1;
                                }
                                else
                                {
                                    if (itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((itemanterior.ConsideraDiasHabilesLV == true && (itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (itemanterior.ConsideraDiasHabilesLS == true && itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                            itemanterior.DiasDeposito = itemanterior.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                        else
                                            itemanterior.DiasDeposito = itemanterior.DiasDeposito - 1;
                                    }
                                    else
                                        itemanterior.DiasDeposito = itemanterior.DiasDeposito - 1;
                                }

                            }
                        }

                        //disponible
                        if (itemanterior.DiasDisponible == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            itemanterior.FechaDisponible = itemanterior.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == itemanterior.FechaDisponible.Value.Date && w.IdTroncalCiudad == itemanterior.IdCiudad).FirstOrDefault() != null) && itemanterior.CuentaFeriados == true)
                                {
                                    itemanterior.FechaDisponible = itemanterior.FechaDisponible.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && itemanterior.ConsideraDiasHabilesLS == true) || ((itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && itemanterior.ConsideraDiasHabilesLV == true))
                                    {
                                        itemanterior.FechaDisponible = itemanterior.FechaDisponible.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            itemanterior.FechaDisponible = itemanterior.FechaPagoReal;
                            while (itemanterior.DiasDisponible > 0)
                            {
                                itemanterior.FechaDisponible = itemanterior.FechaDisponible.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == itemanterior.FechaDisponible.Value.Date && w.IdTroncalCiudad == itemanterior.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (itemanterior.CuentaFeriados == true)
                                        itemanterior.DiasDisponible = itemanterior.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        itemanterior.DiasDisponible = itemanterior.DiasDisponible - 1;
                                }
                                else
                                {
                                    if (itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((itemanterior.ConsideraDiasHabilesLV == true && (itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (itemanterior.ConsideraDiasHabilesLS == true && itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                            itemanterior.DiasDisponible = itemanterior.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                        else
                                            itemanterior.DiasDisponible = itemanterior.DiasDisponible - 1;
                                    }
                                    else
                                        itemanterior.DiasDisponible = itemanterior.DiasDisponible - 1;
                                }

                            }
                        }

                    }

                }

                DateTime fdss = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


                var resultOtrosIngresosPorFechaPagoAnterior = data.resultReportePagosAnterior.Where(x => x.FechaDepositaron.Value >= data.Filtro.FechaInicio && x.FechaDepositaron.Value <= data.Filtro.FechaFin).ToList();
                var resultOtrosEgresosPorFechaPagoPosterior = data.resultReportePagosPosterior.Where(x => x.FechaDepositaron.Value > data.Filtro.FechaFin).ToList();
                var resultOtrosIngresosPorFechaPagoAnteriorDeposito = data.resultReportePagosAnteriorDeposito.Where(x => x.FechaDepositaron.Value >= data.Filtro.FechaInicio && x.FechaDepositaron.Value <= data.Filtro.FechaFin).ToList();
                var resultOtrosEgresosPorFechaPagoPosteriorDeposito = data.resultReportePagosPosteriorDeposito.Where(x => x.FechaDepositaron.Value > data.Filtro.FechaFin).ToList();
                var resultComisiones = data.resultReportePagos.Where(x => x.FechaPagoReal.Value >= data.Filtro.FechaInicio && x.FechaPagoReal.Value <= data.Filtro.FechaFin).ToList();
                var resultGestionCobranza = data.resultReporteGestionCobranza.Where(x => x.FechaPagoReal.Value >= data.Filtro.FechaInicio && x.FechaPagoReal.Value <= data.Filtro.FechaFin).ToList();
                var resultTasasAcademicas = data.resultReporteTasasAcademicas.Where(x => x.FechaPagoReal.Value >= data.Filtro.FechaInicio && x.FechaPagoReal.Value <= data.Filtro.FechaFin).ToList();

                //Sumamos las listas finales
                var SumaIngresoRealVentas = ItemVentas.Sum(x => x.MontoPagado);//Suma de cuotas
                var SumaIngresoAdelanto = ItemOperacionesAdelanto.Sum(x => x.MontoPagado);//Suma de cuotas
                var SumaIngresoRealMes = ItemOperacionesReal.Sum(x => x.MontoPagado);//Suma de cuotas
                var SumaIngresoRecuperado = ItemOperacionesRecuperaciones.Sum(x => x.MontoPagado);// suma de cuotas
                var SumaIngresoInHouse = ItemOperacionesInHouse.Sum(x => x.MontoPagado);// suma cuotas de inhouse
                var SumaTotalOtrosIngresos = resultOtrosIngresosPorFechaPago.Sum(x => x.MontoPagado);
                var SumaTotalOtrosEgresos = resultOtrosEgresosPorFechaPago.Sum(x => x.MontoPagado);
                var SumaTotalPagos = data.resultReportePagos.Sum(x => x.TotalPagado);
                var SumaTotalTipoCambio = resultTipoCambio.Sum(x => x.TotalPagado); // suma monto total pagado cuota + mora
                var SumaTotalPagosAnteriores = resultOtrosIngresosPorFechaPagoAnterior.Sum(x => x.TotalPagado);
                var SumaTotalPagosPosteriores = resultOtrosEgresosPorFechaPagoPosterior.Sum(x => x.TotalPagado);
                var SumaTotalPagosAnterioresDeposito = resultOtrosIngresosPorFechaPagoAnteriorDeposito.Sum(x => x.TotalPagado);
                var SumaTotalPagosPosterioresDeposito = resultOtrosEgresosPorFechaPagoPosteriorDeposito.Sum(x => x.TotalPagado);
                var SumaTotalPagado = resultComisiones.Sum(x => x.TotalPagado);
                var SumaTotalPagadoDisponible = resultComisiones.Sum(x => x.TotalPagadoDisponible);
                var SumaTotalGestionCobranza = resultGestionCobranza.Sum(x => x.TotalPagado);
                var SumaTotalTasasAcademicas = resultTasasAcademicas.Sum(x => x.TotalPagado);

                //--------------------------------------------------------------------------------

                /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

                List<ReporteIngresosDetalleDTO> detalles = new List<ReporteIngresosDetalleDTO>();

                ReporteIngresosDetalleDTO detalle1 = new ReporteIngresosDetalleDTO();
                detalle1.Tipo = "Ingreso Real de Operaciones por Adelantos($)";
                detalle1.Valor = Decimal.Round(SumaIngresoAdelanto.Value);
                detalles.Add(detalle1);

                ReporteIngresosDetalleDTO detalle2 = new ReporteIngresosDetalleDTO();
                detalle2.Tipo = "Ingreso Real de Operaciones($)";
                detalle2.Valor = Decimal.Round(SumaIngresoRealMes.Value - SumaIngresoInHouse.Value);
                detalles.Add(detalle2);

                ReporteIngresosDetalleDTO detalle3 = new ReporteIngresosDetalleDTO();
                detalle3.Tipo = "Ingreso Real de Operaciones por Recuperaciones($)";
                detalle3.Valor = Decimal.Round(SumaIngresoRecuperado.Value);
                detalles.Add(detalle3);

                ReporteIngresosDetalleDTO detalle4 = new ReporteIngresosDetalleDTO();
                detalle4.Tipo = "Ingreso Real de Ventas($)";
                detalle4.Valor = Decimal.Round(SumaIngresoRealVentas.Value);
                detalles.Add(detalle4);

                ReporteIngresosDetalleDTO detalle5 = new ReporteIngresosDetalleDTO();
                detalle5.Tipo = "Inhouse($)";
                detalle5.Valor = Decimal.Round(SumaIngresoInHouse.Value);
                detalles.Add(detalle5);
                //////////////////////////////////inicio
                ReporteIngresosDetalleDTO detalle6 = new ReporteIngresosDetalleDTO();
                detalle6.Tipo = "Otros Ingresos($)";
                detalle6.Valor = Decimal.Round(SumaTotalOtrosIngresos.Value + SumaTotalGestionCobranza.Value + SumaTotalTasasAcademicas.Value);
                detalles.Add(detalle6);

                ReporteIngresosDetalleDTO detalle12 = new ReporteIngresosDetalleDTO();
                detalle12.Tipo = "Otros Egresos($)";
                detalle12.Valor = (-1) * Decimal.Round(SumaTotalOtrosEgresos.Value);
                detalles.Add(detalle12);

                ReporteIngresosDetalleDTO detalle7 = new ReporteIngresosDetalleDTO();
                detalle7.Tipo = "Diferencia por Tipo Cambio($)";
                detalle7.Valor = Decimal.Round(SumaTotalTipoCambio.Value - (SumaIngresoAdelanto.Value + SumaIngresoRealMes.Value + SumaIngresoRecuperado.Value + SumaIngresoRealVentas.Value));
                detalles.Add(detalle7);

                ReporteIngresosDetalleDTO detalle8 = new ReporteIngresosDetalleDTO();
                detalle8.Tipo = "Monto en Flujo($)";
                detalle8.Valor = Decimal.Round(SumaIngresoAdelanto.Value + SumaIngresoRealMes.Value + SumaIngresoRecuperado.Value + SumaIngresoRealVentas.Value + SumaIngresoInHouse.Value + detalle6.Valor + detalle7.Valor + detalle12.Valor);
                detalles.Add(detalle8);

                ReporteIngresosDetalleDTO detalle9 = new ReporteIngresosDetalleDTO();
                detalle9.Tipo = "Ingresos Registrados Anteriores pero Ingresados en el Periodo Seleccionado($)";
                detalle9.Valor = Decimal.Round(SumaTotalPagosAnteriores.Value + SumaTotalPagosAnterioresDeposito.Value);
                detalles.Add(detalle9);

                ReporteIngresosDetalleDTO detalle10 = new ReporteIngresosDetalleDTO();
                detalle10.Tipo = "Ingresos Registrados en el Periodo Seleccionado pero Registrados despues($)";
                detalle10.Valor = (-1) * Decimal.Round(SumaTotalPagosPosteriores.Value + SumaTotalPagosPosterioresDeposito.Value);
                detalles.Add(detalle10);

                ReporteIngresosDetalleDTO detalle11 = new ReporteIngresosDetalleDTO();
                detalle11.Tipo = "Comisiones por Comercio Electronico($)";
                detalle11.Valor = (-1) * Decimal.Round(SumaTotalPagado.Value - SumaTotalPagadoDisponible.Value);
                detalles.Add(detalle11);

                return detalles;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }
}
