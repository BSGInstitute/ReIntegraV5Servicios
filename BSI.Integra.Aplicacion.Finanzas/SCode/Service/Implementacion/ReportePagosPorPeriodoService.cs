using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReportePagosPorPeriodoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePagosPorPeriodo
    /// </summary>
    public class ReportePagosPorPeriodoService : IReportePagosPorPeriodoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReportePagosPorPeriodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReportePagosPorPeriodo, ReportePagosPorPeriodo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReportePagosPorPeriodoRecibidoDTO, ReportePagosPorPeriodo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// <summary>
        /// Obtiene el Reporte de Indicadores de Productividad de Ventas
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerReportePagosIngresos(ReportePagosPorPeriodoFiltroDTO filtro)
        {

            try
            {
                var reportesRepositorio =  _unitOfWork.ReportesRepository;
                var repFeriadoRep = _unitOfWork.FeriadoRepository;
                //reportesRepositorio.ActualizarCronogramaVersionFinal();
                var result = reportesRepositorio.ObtenerReportePagosIngresos(filtro);
                var ListFeriados = repFeriadoRep.GetBy(x => x.Estado == true).ToList();

                foreach (var item in result)
                {
                    var FDeposito = item.FechaDepositaron;
                    var FDisponible = item.FechaDisponible;
                    //Aqui les calculo a todos el porcentaje de cobro 
                    item.TotalPagadoDisponible = item.TotalPagado * (item.PorcentajeCobro / 100);
                    //item.Cuota = item.Cuota * (item.PorcentajeCobro / 100);
                    //Fin Aqui les calculo a todos el porcentaje de cobro
                    item.Numero = item.Numero.Replace("-", "");
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

                    item.FechaDisponible = FDisponible == null ? item.FechaDisponible : FDisponible;
                    item.FechaDepositaron = FDeposito == null ? item.FechaDisponible : FDeposito;

                    if (DateTime.Now.Date >= item.FechaDisponible.Value.Date)
                        item.EstadoEfectivo = "Disponible";
                    else
                        if (DateTime.Now.Date >= item.FechaDepositaron.Value.Date)
                        item.EstadoEfectivo = "Depositado";
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        ///Repositorio: ReporteRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 07/05/2021
        /// <summary>
        /// Congela los datos del reporte de pagos en base a una fecha  
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDePagosPorDia(CongelarFlujoDTO filtro, string Usuario)
        {
            try
            {
                var servicio = _unitOfWork.ReportesRepository;
                return servicio.CongelarReporteDePagosPorDia(filtro.FechaCongelamiento.Value, Usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: ReporteRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 07/05/2021
        /// <summary>
        /// Congela los datos del reporte de pagos en base al dia fiunal de un periodo 
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="IdPeriod"> periodo</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDePagosPorPeriodo(CongelarFlujoDTO filtro, string Usuario)
        {
            try
            {
                var servicio = _unitOfWork.ReportesRepository;
                return servicio.CongelarReporteDePagosPorPeriodo(filtro.FechaCongelamiento.Value, filtro.IdPeriodo.Value, Usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
