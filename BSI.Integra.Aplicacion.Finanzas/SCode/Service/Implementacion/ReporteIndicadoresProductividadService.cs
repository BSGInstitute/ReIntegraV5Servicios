using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReporteIndicadoresProductividadService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteIndicadoresProductividad
    /// </summary>
    public class ReporteIndicadoresProductividadService : IReporteIndicadoresProductividadService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteIndicadoresProductividadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReporteIndicadoresProductividad, ReporteIndicadoresProductividad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReporteIndicadoresProductividadRecibidoDTO, ReporteIndicadoresProductividad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// <summary>
        /// Obtiene el Reporte de Indicadores de Productividad de Ventas
        /// </summary>
        /// <returns></returns>
        public List<ReporteProductividadVentasHorasTrabajadasDTO> ObtenerReporteProductividadVentasHorasTrabajadas(FiltroFechaDTO filtro)
        {

            return _unitOfWork.ReporteIndicadoresProductividadRepository.ObtenerReporteProductividadVentasHorasTrabajadas(filtro).OrderBy(x => x.FechaPeriodo).ToList();
            
        }
        /// <summary>
        /// Obtiene el Reporte de Indicadores de Productividad de Ventas
        /// </summary>
        /// <returns></returns>
        public List<ReporteProductividadVentasIndicadoresDTO> ObtenerReporteProductividadVentasIndicadores(FiltroFechaDTO filtro)
        {

            return _unitOfWork.ReporteIndicadoresProductividadRepository.ObtenerReporteProductividadVentasIndicadores(filtro).ToList();

        }


        public IEnumerable<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> GenerarReporteIndicadoresHorasTrabajadasVentas(ReporteIndicadoresProductividadVentasGeneralDTO respuestaGeneral)
        {
            IEnumerable<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> agrupado = null;

            var diasTrabajadas = respuestaGeneral.HorasTrabajadas.Where(x => x.DiasTrabajados > 0).ToList();
            agrupado = diasTrabajadas.GroupBy(x => x.PeriodoMarcacion)
            .Select(g => new ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO
            {
                Periodo = g.Key,
                DetalleFecha = g.Select(y => new ReporteProductividadVentasHorasTrabajadasDTO
                {
                    IdPersonal = y.IdPersonal,
                    NombrePersonal = y.NombrePersonal,
                    CoordinadorAsesor = y.CoordinadorAsesor,
                    NombreJefe = y.NombreJefe,
                    HorasTrabajadas = y.HorasTrabajadas,
                    DiasTrabajados = y.DiasTrabajados,
                    TotalVendido = y.TotalVendido,
                    Sede = y.Sede

                }).ToList()
            });

            return agrupado;
        }

        public IEnumerable<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> GenerarReporteHorasTrabajadasProductividadEquipoVentas(ReporteIndicadoresProductividadVentasGeneralDTO respuestaGeneral)
        {
            IEnumerable<ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO> agrupado = null;

            agrupado = respuestaGeneral.HorasTrabajadas.GroupBy(x => x.PeriodoMarcacion)
            .Select(g => new ReporteIndicadoresProductividadHorasTrabajadasAgrupadoDTO
            {
                Periodo = g.Key,
                DetalleFecha = g.Select(y => new ReporteProductividadVentasHorasTrabajadasDTO
                {
                    IdPersonal = y.IdPersonal,
                    NombrePersonal = y.NombrePersonal,
                    CoordinadorAsesor = y.CoordinadorAsesor,
                    NombreJefe = y.NombreJefe,
                    HorasTrabajadas = y.HorasTrabajadas,
                    DiasTrabajados = y.DiasTrabajados,
                    TotalVendido = y.TotalVendido,
                    Sede = y.Sede

                }).ToList()
            });

            return agrupado;
        }

        public IList<ReporteProductividadVentasDetalleFinalDTO> GenerarReporteIndicadoresProductividadIndicadoresVentas(ReporteIndicadoresProductividadVentasGeneralDTO respuestaGeneral)
        {
            var agrupado = respuestaGeneral.Indicadores.OrderBy(x => x.PeriodoMarcacion);

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReporteProductividadVentasDetalleDTO> detalles = new List<ReporteProductividadVentasDetalleDTO>();

            ReporteProductividadVentasDetalleDTO detalle1 = new ReporteProductividadVentasDetalleDTO();
            detalle1.Tipo = "Dias Laborables por Mes";
            detalle1.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReporteProductividadVentasDetalleDTO detalle2 = new ReporteProductividadVentasDetalleDTO();
            detalle2.Tipo = "Numero de Asesores";
            detalle2.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReporteProductividadVentasDetalleDTO detalle3 = new ReporteProductividadVentasDetalleDTO();
            detalle3.Tipo = "Asesores de Capacitacion(dias)";
            detalle3.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReporteProductividadVentasDetalleDTO detalle4 = new ReporteProductividadVentasDetalleDTO();
            detalle4.Tipo = "Coordinadores (dias)";
            detalle4.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReporteProductividadVentasDetalleDTO detalle5 = new ReporteProductividadVentasDetalleDTO();
            detalle5.Tipo = "Inscritos";
            detalle5.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle5);

            ReporteProductividadVentasDetalleDTO detalle6 = new ReporteProductividadVentasDetalleDTO();
            detalle6.Tipo = "Inscritos Instituto";
            detalle6.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReporteProductividadVentasDetalleDTO detalle7 = new ReporteProductividadVentasDetalleDTO();
            detalle7.Tipo = "Inscritos por dia laborable";
            detalle7.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle7);

            ReporteProductividadVentasDetalleDTO detalle8 = new ReporteProductividadVentasDetalleDTO();
            detalle8.Tipo = "Inscritos por asesor";
            detalle8.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReporteProductividadVentasDetalleDTO detalle9 = new ReporteProductividadVentasDetalleDTO();
            detalle9.Tipo = "Inscritos / asesor dia";
            detalle9.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReporteProductividadVentasDetalleDTO detalle10 = new ReporteProductividadVentasDetalleDTO();
            detalle10.Tipo = "Inscritos / coordinador dia";
            detalle10.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle10);

            ReporteProductividadVentasDetalleDTO detalle11 = new ReporteProductividadVentasDetalleDTO();
            detalle11.Tipo = "Ventas($)";
            detalle11.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle11);

            ReporteProductividadVentasDetalleDTO detalle12 = new ReporteProductividadVentasDetalleDTO();
            detalle12.Tipo = "Ventas Instituto($)";
            detalle12.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReporteProductividadVentasDetalleDTO detalle13 = new ReporteProductividadVentasDetalleDTO();
            detalle13.Tipo = "Venta promedio / alumno inscrito($)";
            detalle13.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle13);

            ReporteProductividadVentasDetalleDTO detalle14 = new ReporteProductividadVentasDetalleDTO();
            detalle14.Tipo = "Venta promedio / alumno inscrito (Instituto)($)";
            detalle14.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle14);

            ReporteProductividadVentasDetalleDTO detalle15 = new ReporteProductividadVentasDetalleDTO();
            detalle15.Tipo = "Venta / asesor dia($)";
            detalle15.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle15);

            ReporteProductividadVentasDetalleDTO detalle16 = new ReporteProductividadVentasDetalleDTO();
            detalle16.Tipo = "Venta / coordinador dia($)";
            detalle16.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle16);

            ReporteProductividadVentasDetalleDTO detalle17 = new ReporteProductividadVentasDetalleDTO();
            detalle17.Tipo = "Gastos($)";
            detalle17.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle17);

            ReporteProductividadVentasDetalleDTO detalle18 = new ReporteProductividadVentasDetalleDTO();
            detalle18.Tipo = "Comisiones de Ventas";
            detalle18.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle18);

            ReporteProductividadVentasDetalleDTO detalle19 = new ReporteProductividadVentasDetalleDTO();
            detalle19.Tipo = "Gastos relacionados al Instituto";
            detalle19.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle19);

            ReporteProductividadVentasDetalleDTO detalle20 = new ReporteProductividadVentasDetalleDTO();
            detalle20.Tipo = "Gastos relacionados directamente con Ventas";
            detalle20.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle20);

            ReporteProductividadVentasDetalleDTO detalle21 = new ReporteProductividadVentasDetalleDTO();
            detalle21.Tipo = "* Telefonos Fijos";
            detalle21.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle21);

            ReporteProductividadVentasDetalleDTO detalle22 = new ReporteProductividadVentasDetalleDTO();
            detalle22.Tipo = "* Recargas telefónicas";
            detalle22.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle22);

            ReporteProductividadVentasDetalleDTO detalle23 = new ReporteProductividadVentasDetalleDTO();
            detalle23.Tipo = "* Telefonos Moviles";
            detalle23.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle23);

            ReporteProductividadVentasDetalleDTO detalle26 = new ReporteProductividadVentasDetalleDTO();
            detalle26.Tipo = "* Alimentacion/Alojamiento/Pasajes/Movilidad";
            detalle26.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle26);

            ReporteProductividadVentasDetalleDTO detalle24 = new ReporteProductividadVentasDetalleDTO();
            detalle24.Tipo = "* Capacitación de personal";
            detalle24.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle24);

            ReporteProductividadVentasDetalleDTO detalle27 = new ReporteProductividadVentasDetalleDTO();
            detalle27.Tipo = "* Premios por Cumplimiento de Metas";
            detalle27.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle27);

            ReporteProductividadVentasDetalleDTO detalle25 = new ReporteProductividadVentasDetalleDTO();
            detalle25.Tipo = "* Publicidad";
            detalle25.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle25);

            ReporteProductividadVentasDetalleDTO detalle28 = new ReporteProductividadVentasDetalleDTO();
            detalle28.Tipo = "* Cargas de Personal";
            detalle28.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle28);

            ReporteProductividadVentasDetalleDTO detalle29 = new ReporteProductividadVentasDetalleDTO();
            detalle29.Tipo = "* Liquidaciones";
            detalle29.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle29);

            ReporteProductividadVentasDetalleDTO detalle30 = new ReporteProductividadVentasDetalleDTO();
            detalle30.Tipo = "* Es Salud / AFP / Renta 5ta Catg";
            detalle30.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle30);

            ReporteProductividadVentasDetalleDTO detalle31 = new ReporteProductividadVentasDetalleDTO();
            detalle31.Tipo = "* Participaciones / CTS / Gratificaciones";
            detalle31.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle31);

            ReporteProductividadVentasDetalleDTO detalle32 = new ReporteProductividadVentasDetalleDTO();
            detalle32.Tipo = "* Sueldos Asesores";
            detalle32.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle32);

            ReporteProductividadVentasDetalleDTO detalle46 = new ReporteProductividadVentasDetalleDTO();
            detalle46.Tipo = "GASTOS RELACIONADOS CON VENTAS (%)";
            detalle46.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle46);

            ReporteProductividadVentasDetalleDTO detalle33 = new ReporteProductividadVentasDetalleDTO();
            detalle33.Tipo = "Porcentaje Gastos Totales";
            detalle33.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle33);

            ReporteProductividadVentasDetalleDTO detalle34 = new ReporteProductividadVentasDetalleDTO();
            detalle34.Tipo = "Porcentaje Gastos Instituto";
            detalle34.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle34);

            ReporteProductividadVentasDetalleDTO detalle35 = new ReporteProductividadVentasDetalleDTO();
            detalle35.Tipo = "Porcentaje Compensacion de Ventas";
            detalle35.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle35);

            ReporteProductividadVentasDetalleDTO detalle36 = new ReporteProductividadVentasDetalleDTO();
            detalle36.Tipo = "Porcentaje Gastos Relacionados Directamente con Ventas";
            detalle36.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle36);

            ReporteProductividadVentasDetalleDTO detalle37 = new ReporteProductividadVentasDetalleDTO();
            detalle37.Tipo = "* Porcentaje  Telefonia";
            detalle37.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle37);

            ReporteProductividadVentasDetalleDTO detalle38 = new ReporteProductividadVentasDetalleDTO();
            detalle38.Tipo = "* Porcentaje Alimentacion/ Alojamiento  / Pasajes / Movilidades";
            detalle38.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle38);

            ReporteProductividadVentasDetalleDTO detalle39 = new ReporteProductividadVentasDetalleDTO();
            detalle39.Tipo = "* Porcentaje Capacitación de personal";
            detalle39.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle39);

            ReporteProductividadVentasDetalleDTO detalle40 = new ReporteProductividadVentasDetalleDTO();
            detalle40.Tipo = "* Porcentaje Premios por Cumplimiento de Metas";
            detalle40.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle40);

            ReporteProductividadVentasDetalleDTO detalle41 = new ReporteProductividadVentasDetalleDTO();
            detalle41.Tipo = "* Porcentaje Publicidad";
            detalle41.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle41);

            ReporteProductividadVentasDetalleDTO detalle42 = new ReporteProductividadVentasDetalleDTO();
            detalle42.Tipo = "* Porcentaje Cargas de Personal";
            detalle42.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle42);

            ReporteProductividadVentasDetalleDTO detalle47 = new ReporteProductividadVentasDetalleDTO();
            detalle47.Tipo = "COSTO POR ASESOR";
            detalle47.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle47);

            ReporteProductividadVentasDetalleDTO detalle43 = new ReporteProductividadVentasDetalleDTO();
            detalle43.Tipo = "Gastos por Asesor($)";
            detalle43.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle43);

            ReporteProductividadVentasDetalleDTO detalle44 = new ReporteProductividadVentasDetalleDTO();
            detalle44.Tipo = "Gastos por Asesor (Incluyendo Publicidad)($)";
            detalle44.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle44);

            ReporteProductividadVentasDetalleDTO detalle45 = new ReporteProductividadVentasDetalleDTO();
            detalle45.Tipo = "Gastos por Asesor (Incluyendo Publicidad y Costo de Supervision)($)";
            detalle45.ListaMontosMeses = new List<ReporteProductividadVentasDetallesMesesDTO>();
            detalles.Add(detalle45);

            foreach (var item in agrupado)
            {
                var cargasPersonal = (item.PagoDolaresCTSLiquidacion + item.PagoDolaresGratificacionLiquidacion + item.PagoDolaresSueldoLiquidacion + item.PagoDolaresEsSaludAsesores +
                    item.PagoDolaresSisPensionarioAsesores + item.PagoDolaresRenta5Asesores + item.PagoDolaresParticipacionesAsesores + item.PagoDolaresCTSAsesores +
                    item.PagoDolaresGratificacionAsesores + item.PagoDolaresSueldoAsesores);

                var gastosVentas = (item.PagoDolaresRecargasTelefonicas + item.PagoDolaresTelefonosFijos + item.PagoDolaresTelefonosMoviles +
                    item.PagoDolaresCapacitacionPersonal + item.BeaticosVentas + item.PagoDolaresPublicidad + item.PagoDolaresPremios + cargasPersonal);

                //Dias Laborables por Mes
                ReporteProductividadVentasDetallesMesesDTO detallemes1 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoMarcacion;
                detallemes1.Monto = item.DiasLaborables.ToString();
                detalles.Where(w => w.Tipo == "Dias Laborables por Mes").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //Numero de Asesores
                ReporteProductividadVentasDetallesMesesDTO detallemes2 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoMarcacion;
                if (item.DiasLaborables == 0)
                {
                    detallemes2.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes2.Monto = (item.DiasAsesor / item.DiasLaborables).ToString();
                }
                detalles.Where(w => w.Tipo == "Numero de Asesores").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //Asesores de Capacitacion(dias)
                ReporteProductividadVentasDetallesMesesDTO detallemes3 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoMarcacion;
                detallemes3.Monto = item.DiasAsesor.ToString();
                detalles.Where(w => w.Tipo == "Asesores de Capacitacion(dias)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //Coordinadores (dias)
                ReporteProductividadVentasDetallesMesesDTO detallemes4 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoMarcacion;
                detallemes4.Monto = item.DiasCoordinador.ToString();
                detalles.Where(w => w.Tipo == "Coordinadores (dias)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //Inscritos
                ReporteProductividadVentasDetallesMesesDTO detallemes5 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoMarcacion;
                detallemes5.Monto = item.NumeroIS.ToString();
                detalles.Where(w => w.Tipo == "Inscritos").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                //Inscritos Instituto
                ReporteProductividadVentasDetallesMesesDTO detallemes6 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoMarcacion;
                var Instituto = 0.0m;
                detallemes6.Monto = Instituto.ToString();
                detalles.Where(w => w.Tipo == "Inscritos Instituto").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Inscritos por dia laborable
                ReporteProductividadVentasDetallesMesesDTO detallemes7 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoMarcacion;
                if (item.DiasLaborables == 0)
                {
                    detallemes7.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes7.Monto = ((Instituto + item.NumeroIS) / item.DiasLaborables).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Inscritos por dia laborable").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Inscritos por asesor
                ReporteProductividadVentasDetallesMesesDTO detallemes8 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoMarcacion;
                decimal NumAsesores2;
                if (item.DiasLaborables == 0)
                {
                    NumAsesores2 = 0;
                }
                else
                {
                    NumAsesores2 = Convert.ToDecimal(item.DiasAsesor) / Convert.ToDecimal(item.DiasLaborables);
                }
                if (item.DiasLaborables == 0 || (item.DiasAsesor / item.DiasLaborables) == 0)
                {
                    detallemes8.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes8.Monto = ((Instituto + item.NumeroIS) / NumAsesores2).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Inscritos por asesor").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Inscritos / asesor dia
                ReporteProductividadVentasDetallesMesesDTO detallemes9 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoMarcacion;
                if (item.DiasAsesor == 0)
                {
                    detallemes9.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes9.Monto = (Convert.ToDecimal(item.NumeroIS) / Convert.ToDecimal(item.DiasAsesor)).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Inscritos / asesor dia").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Inscritos / coordinador dia
                ReporteProductividadVentasDetallesMesesDTO detallemes10 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoMarcacion;
                if (item.DiasCoordinador == 0)
                {
                    detallemes10.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes10.Monto = (Convert.ToDecimal(item.NumeroIS) / Convert.ToDecimal(item.DiasCoordinador)).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Inscritos / coordinador dia").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //Ventas
                ReporteProductividadVentasDetallesMesesDTO detallemes11 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes11.Mes = item.PeriodoMarcacion;
                detallemes11.Monto = item.TotalVendido.ToString();
                detalles.Where(w => w.Tipo == "Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes11);

                //Ventas Instituto
                ReporteProductividadVentasDetallesMesesDTO detallemes12 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes12.Mes = item.PeriodoMarcacion;
                var VentasInstituto = 0.0m;
                detallemes12.Monto = VentasInstituto.ToString();
                detalles.Where(w => w.Tipo == "Ventas Instituto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Venta promedio / alumno inscrito
                ReporteProductividadVentasDetallesMesesDTO detallemes13 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes13.Mes = item.PeriodoMarcacion;
                if (item.NumeroIS == 0)
                {
                    detallemes13.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes13.Monto = (item.TotalVendido / item.NumeroIS).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Venta promedio / alumno inscrito($)").FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                //Venta promedio / alumno inscrito (Instituto)
                ReporteProductividadVentasDetallesMesesDTO detallemes14 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes14.Mes = item.PeriodoMarcacion;
                if (VentasInstituto == 0)
                {
                    detallemes14.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes14.Monto = (item.TotalVendido / VentasInstituto).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Venta promedio / alumno inscrito (Instituto)($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);

                //Venta / asesor dia
                ReporteProductividadVentasDetallesMesesDTO detallemes15 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes15.Mes = item.PeriodoMarcacion;
                if (item.DiasAsesor == 0)
                {
                    detallemes15.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes15.Monto = (item.TotalVendido / item.DiasAsesor).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Venta / asesor dia($)").FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //Venta / asesor dia
                ReporteProductividadVentasDetallesMesesDTO detallemes16 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes16.Mes = item.PeriodoMarcacion;
                if (item.DiasCoordinador == 0)
                {
                    detallemes16.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes16.Monto = (item.TotalVendido / item.DiasCoordinador).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Venta / coordinador dia($)").FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Gastos
                ReporteProductividadVentasDetallesMesesDTO detallemes17 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes17.Mes = item.PeriodoMarcacion;
                detallemes17.Monto = (item.PagoDolaresComisiones + gastosVentas).ToString("0.00");
                detalles.Where(w => w.Tipo == "Gastos($)").FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //Comisiones de Ventas
                ReporteProductividadVentasDetallesMesesDTO detallemes18 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes18.Mes = item.PeriodoMarcacion;
                detallemes18.Monto = item.PagoDolaresComisiones.ToString();
                detalles.Where(w => w.Tipo == "Comisiones de Ventas").FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //Gastos relacionados al Instituto
                ReporteProductividadVentasDetallesMesesDTO detallemes19 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes19.Mes = item.PeriodoMarcacion;
                var GastosInstituto = 0.0m;
                detallemes19.Monto = GastosInstituto.ToString();
                detalles.Where(w => w.Tipo == "Gastos relacionados al Instituto").FirstOrDefault().ListaMontosMeses.Add(detallemes19);

                //Gastos relacionados directamente con Ventas
                ReporteProductividadVentasDetallesMesesDTO detallemes20 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes20.Mes = item.PeriodoMarcacion;
                detallemes20.Monto = (item.PagoDolaresRecargasTelefonicas + item.PagoDolaresTelefonosFijos + item.PagoDolaresTelefonosMoviles +
                    item.PagoDolaresCapacitacionPersonal + item.BeaticosVentas + item.PagoDolaresPublicidad + item.PagoDolaresPremios + cargasPersonal).ToString("0.00");
                detalles.Where(w => w.Tipo == "Gastos relacionados directamente con Ventas").FirstOrDefault().ListaMontosMeses.Add(detallemes20);

                //Telefonos Fijos
                ReporteProductividadVentasDetallesMesesDTO detallemes21 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes21.Mes = item.PeriodoMarcacion;
                detallemes21.Monto = item.PagoDolaresTelefonosFijos.ToString();
                detalles.Where(w => w.Tipo == "* Telefonos Fijos").FirstOrDefault().ListaMontosMeses.Add(detallemes21);

                //Recargas telefónicas
                ReporteProductividadVentasDetallesMesesDTO detallemes22 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes22.Mes = item.PeriodoMarcacion;
                detallemes22.Monto = item.PagoDolaresRecargasTelefonicas.ToString();
                detalles.Where(w => w.Tipo == "* Recargas telefónicas").FirstOrDefault().ListaMontosMeses.Add(detallemes22);

                //Telefonos Moviles
                ReporteProductividadVentasDetallesMesesDTO detallemes23 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes23.Mes = item.PeriodoMarcacion;
                detallemes23.Monto = item.PagoDolaresTelefonosMoviles.ToString();
                detalles.Where(w => w.Tipo == "* Telefonos Moviles").FirstOrDefault().ListaMontosMeses.Add(detallemes23);

                //Alimentacion/ Alojamiento  / Pasajes / Movilidades
                ReporteProductividadVentasDetallesMesesDTO detallemes26 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes26.Mes = item.PeriodoMarcacion;
                detallemes26.Monto = item.BeaticosVentas.ToString();
                detalles.Where(w => w.Tipo == "* Alimentacion/Alojamiento/Pasajes/Movilidad").FirstOrDefault().ListaMontosMeses.Add(detallemes26);

                //Capacitación de personal
                ReporteProductividadVentasDetallesMesesDTO detallemes24 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes24.Mes = item.PeriodoMarcacion;
                detallemes24.Monto = item.PagoDolaresCapacitacionPersonal.ToString();
                detalles.Where(w => w.Tipo == "* Capacitación de personal").FirstOrDefault().ListaMontosMeses.Add(detallemes24);

                //Premios por Cumplimiento de Metas
                ReporteProductividadVentasDetallesMesesDTO detallemes27 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes27.Mes = item.PeriodoMarcacion;
                detallemes27.Monto = item.PagoDolaresPremios.ToString();
                detalles.Where(w => w.Tipo == "* Premios por Cumplimiento de Metas").FirstOrDefault().ListaMontosMeses.Add(detallemes27);

                //Publicidad
                ReporteProductividadVentasDetallesMesesDTO detallemes25 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes25.Mes = item.PeriodoMarcacion;
                detallemes25.Monto = item.PagoDolaresPublicidad.ToString();
                detalles.Where(w => w.Tipo == "* Publicidad").FirstOrDefault().ListaMontosMeses.Add(detallemes25);

                //Cargas de Personal
                ReporteProductividadVentasDetallesMesesDTO detallemes28 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes28.Mes = item.PeriodoMarcacion;
                detallemes28.Monto = (item.PagoDolaresCTSLiquidacion + item.PagoDolaresGratificacionLiquidacion + item.PagoDolaresSueldoLiquidacion + item.PagoDolaresEsSaludAsesores +
                    item.PagoDolaresSisPensionarioAsesores + item.PagoDolaresParticipacionesAsesores + item.PagoDolaresRenta5Asesores + item.PagoDolaresCTSAsesores +
                    item.PagoDolaresGratificacionAsesores + item.PagoDolaresSueldoAsesores).ToString("0.00");
                detalles.Where(w => w.Tipo == "* Cargas de Personal").FirstOrDefault().ListaMontosMeses.Add(detallemes28);

                //Liquidaciones
                ReporteProductividadVentasDetallesMesesDTO detallemes29 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes29.Mes = item.PeriodoMarcacion;
                detallemes29.Monto = (item.PagoDolaresCTSLiquidacion + item.PagoDolaresGratificacionLiquidacion + item.PagoDolaresSueldoLiquidacion).ToString();
                detalles.Where(w => w.Tipo == "* Liquidaciones").FirstOrDefault().ListaMontosMeses.Add(detallemes29);

                //Es Salud / AFP / Renta 5ta Catg
                ReporteProductividadVentasDetallesMesesDTO detallemes30 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes30.Mes = item.PeriodoMarcacion;
                detallemes30.Monto = (item.PagoDolaresEsSaludAsesores + item.PagoDolaresSisPensionarioAsesores + item.PagoDolaresRenta5Asesores).ToString("0.00");
                detalles.Where(w => w.Tipo == "* Es Salud / AFP / Renta 5ta Catg").FirstOrDefault().ListaMontosMeses.Add(detallemes30);

                //Participaciones / CTS / Gratificaciones
                ReporteProductividadVentasDetallesMesesDTO detallemes31 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes31.Mes = item.PeriodoMarcacion;
                detallemes31.Monto = (item.PagoDolaresCTSAsesores + item.PagoDolaresGratificacionAsesores + item.PagoDolaresParticipacionesAsesores).ToString();
                detalles.Where(w => w.Tipo == "* Participaciones / CTS / Gratificaciones").FirstOrDefault().ListaMontosMeses.Add(detallemes31);

                //Sueldos Asesores
                ReporteProductividadVentasDetallesMesesDTO detallemes32 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes32.Mes = item.PeriodoMarcacion;
                detallemes32.Monto = item.PagoDolaresSueldoAsesores.ToString();
                detalles.Where(w => w.Tipo == "* Sueldos Asesores").FirstOrDefault().ListaMontosMeses.Add(detallemes32);

                //GASTOS RELACIONADOS CON VENTAS (%)
                ReporteProductividadVentasDetallesMesesDTO detallemes46 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes46.Mes = item.PeriodoMarcacion;
                detallemes46.Monto = " ".ToString();
                detalles.Where(w => w.Tipo == "GASTOS RELACIONADOS CON VENTAS (%)").FirstOrDefault().ListaMontosMeses.Add(detallemes46);

                //Porcentaje Gastos Totales
                ReporteProductividadVentasDetallesMesesDTO detallemes33 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes33.Mes = item.PeriodoMarcacion;
                var gastos = item.PagoDolaresComisiones + (item.PagoDolaresRecargasTelefonicas + item.PagoDolaresTelefonosFijos + item.PagoDolaresTelefonosMoviles +
                    item.PagoDolaresCapacitacionPersonal + item.BeaticosVentas + item.PagoDolaresPublicidad + item.PagoDolaresPremios + cargasPersonal);
                if (item.TotalVendido == 0)
                {
                    detallemes33.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes33.Monto = "% " + ((gastos / item.TotalVendido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Porcentaje Gastos Totales").FirstOrDefault().ListaMontosMeses.Add(detallemes33);

                //Porcentaje Gastos Instituto
                ReporteProductividadVentasDetallesMesesDTO detallemes34 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes34.Mes = item.PeriodoMarcacion;
                detallemes34.Monto = "% " + (0.00m * 100).ToString("0.00");
                detalles.Where(w => w.Tipo == "Porcentaje Gastos Instituto").FirstOrDefault().ListaMontosMeses.Add(detallemes34);

                //Porcentaje Compensacion de Ventas
                ReporteProductividadVentasDetallesMesesDTO detallemes35 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes35.Mes = item.PeriodoMarcacion;
                if (item.TotalVendido == 0)
                {
                    detallemes35.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes35.Monto = "% " + ((item.PagoDolaresComisiones / item.TotalVendido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Porcentaje Compensacion de Ventas").FirstOrDefault().ListaMontosMeses.Add(detallemes35);

                //Porcentaje Gastos Relacionados Directamente con Ventas
                ReporteProductividadVentasDetallesMesesDTO detallemes36 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes36.Mes = item.PeriodoMarcacion;
                if (item.TotalVendido == 0)
                {
                    detallemes36.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes36.Monto = "% " + ((gastosVentas / item.TotalVendido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Porcentaje Gastos Relacionados Directamente con Ventas").FirstOrDefault().ListaMontosMeses.Add(detallemes36);

                //* Porcentaje  Telefonia
                ReporteProductividadVentasDetallesMesesDTO detallemes37 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes37.Mes = item.PeriodoMarcacion;
                if (item.TotalVendido == 0)
                {
                    detallemes37.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes37.Monto = "% " + (((item.PagoDolaresRecargasTelefonicas + item.PagoDolaresTelefonosFijos + item.PagoDolaresTelefonosMoviles) / item.TotalVendido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "* Porcentaje  Telefonia").FirstOrDefault().ListaMontosMeses.Add(detallemes37);

                //* Porcentaje Alimentacion/ Alojamiento  / Pasajes / Movilidades
                ReporteProductividadVentasDetallesMesesDTO detallemes38 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes38.Mes = item.PeriodoMarcacion;
                if (item.TotalVendido == 0)
                {
                    detallemes38.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes38.Monto = "% " + ((item.BeaticosVentas / item.TotalVendido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "* Porcentaje Alimentacion/ Alojamiento  / Pasajes / Movilidades").FirstOrDefault().ListaMontosMeses.Add(detallemes38);

                //* Porcentaje Capacitación de personal
                ReporteProductividadVentasDetallesMesesDTO detallemes39 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes39.Mes = item.PeriodoMarcacion;
                if (item.TotalVendido == 0)
                {
                    detallemes39.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes39.Monto = "% " + ((item.PagoDolaresCapacitacionPersonal / item.TotalVendido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "* Porcentaje Capacitación de personal").FirstOrDefault().ListaMontosMeses.Add(detallemes39);

                //* Porcentaje Premios por Cumplimiento de Metas
                ReporteProductividadVentasDetallesMesesDTO detallemes40 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes40.Mes = item.PeriodoMarcacion;
                if (item.TotalVendido == 0)
                {
                    detallemes40.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes40.Monto = "% " + ((item.PagoDolaresPremios / item.TotalVendido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "* Porcentaje Premios por Cumplimiento de Metas").FirstOrDefault().ListaMontosMeses.Add(detallemes40);

                //* Porcentaje Publicidad
                ReporteProductividadVentasDetallesMesesDTO detallemes41 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes41.Mes = item.PeriodoMarcacion;
                if (item.TotalVendido == 0)
                {
                    detallemes41.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes41.Monto = "% " + ((item.PagoDolaresPublicidad / item.TotalVendido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "* Porcentaje Publicidad").FirstOrDefault().ListaMontosMeses.Add(detallemes41);

                //* Porcentaje Cargas de Personal
                ReporteProductividadVentasDetallesMesesDTO detallemes42 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes42.Mes = item.PeriodoMarcacion;
                if (item.TotalVendido == 0)
                {
                    detallemes42.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes42.Monto = "% " + ((cargasPersonal / item.TotalVendido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "* Porcentaje Cargas de Personal").FirstOrDefault().ListaMontosMeses.Add(detallemes42);

                //COSTO POR ASESOR
                ReporteProductividadVentasDetallesMesesDTO detallemes47 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes47.Mes = item.PeriodoMarcacion;
                detallemes47.Monto = " ".ToString();
                detalles.Where(w => w.Tipo == "COSTO POR ASESOR").FirstOrDefault().ListaMontosMeses.Add(detallemes47);

                //Gastos por Asesor
                ReporteProductividadVentasDetallesMesesDTO detallemes43 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes43.Mes = item.PeriodoMarcacion;
                decimal NumAsesores;
                if (item.DiasLaborables == 0)
                {
                    NumAsesores = 0;
                }
                else
                {
                    NumAsesores = Convert.ToDecimal(item.DiasAsesor) / Convert.ToDecimal(item.DiasLaborables);
                }
                if (NumAsesores == 0)
                {
                    detallemes43.Monto = (0.00m).ToString();

                }
                else
                {
                    detallemes43.Monto = ((cargasPersonal + item.PagoDolaresComisiones + item.PagoDolaresTelefonosFijos + item.PagoDolaresRecargasTelefonicas + item.PagoDolaresTelefonosMoviles +
                    item.PagoDolaresCapacitacionPersonal + item.PagoDolaresPremios + item.BeaticosVentas) / NumAsesores).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "Gastos por Asesor($)").FirstOrDefault().ListaMontosMeses.Add(detallemes43);

                //Gastos por Asesor (Incluyendo Publicidad)
                ReporteProductividadVentasDetallesMesesDTO detallemes44 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes44.Mes = item.PeriodoMarcacion;
                var gastoAsesorPubli = 0.00m;
                if (NumAsesores == 0)
                {
                    detallemes44.Monto = (0.00m).ToString();
                    gastoAsesorPubli = 0.00m;
                }
                else
                {
                    detallemes44.Monto = ((cargasPersonal + item.PagoDolaresComisiones + item.PagoDolaresTelefonosFijos + item.PagoDolaresRecargasTelefonicas + item.PagoDolaresTelefonosMoviles +
                    item.PagoDolaresCapacitacionPersonal + item.PagoDolaresPremios + item.PagoDolaresPublicidad + item.BeaticosVentas) / NumAsesores).ToString("0.00");

                    gastoAsesorPubli = ((cargasPersonal + item.PagoDolaresComisiones + item.PagoDolaresTelefonosFijos + item.PagoDolaresRecargasTelefonicas + item.PagoDolaresTelefonosMoviles +
                    item.PagoDolaresCapacitacionPersonal + item.PagoDolaresPremios + item.PagoDolaresPublicidad + item.BeaticosVentas) / NumAsesores);
                }
                detalles.Where(w => w.Tipo == "Gastos por Asesor (Incluyendo Publicidad)($)").FirstOrDefault().ListaMontosMeses.Add(detallemes44);

                //Gastos por Asesor (Incluyendo Publicidad y Costo de Supervision)
                ReporteProductividadVentasDetallesMesesDTO detallemes45 = new ReporteProductividadVentasDetallesMesesDTO();
                detallemes45.Mes = item.PeriodoMarcacion;
                //var numeroAsesores = (item.DiasAsesor / item.DiasLaborables);

                if (NumAsesores == 0)
                {
                    detallemes45.Monto = (gastoAsesorPubli).ToString();

                }
                else
                {
                    detallemes45.Monto = (((item.PagoDolaresSueldoCoordinadores + item.PagoDolaresEsSaludCoordinadores + item.PagoDolaresSisPensionarioCoordinadores + item.PagoDolaresParticipacionesCoordinadores +
                        item.PagoDolaresRenta5Coordinadores + item.PagoDolaresCTSCoordinadores + item.PagoDolaresGratificacionCoordinadores) / NumAsesores) + gastoAsesorPubli).ToString("0.00");

                }
                detalles.Where(w => w.Tipo == "Gastos por Asesor (Incluyendo Publicidad y Costo de Supervision)($)").FirstOrDefault().ListaMontosMeses.Add(detallemes45);

            }

            List<ReporteProductividadVentasDetalleFinalDTO> finales = new List<ReporteProductividadVentasDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReporteProductividadVentasDetalleFinalDTO item = new ReporteProductividadVentasDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }

            }
            return finales;
        }

        public ReporteIndicadoresProductividadVentasCompuestoDTO GenerarReporte(ReporteIndicadoresProductividadVentasGeneralDTO respuestaGeneral)
        {
            try
            {
                var agrupado = this.GenerarReporteIndicadoresHorasTrabajadasVentas(respuestaGeneral);

                var agrupado3 = this.GenerarReporteHorasTrabajadasProductividadEquipoVentas(respuestaGeneral);

                var listRpta = this.GenerarReporteIndicadoresProductividadIndicadoresVentas(respuestaGeneral);
                var agrupado4 = (from p in listRpta
                                 group p by p.Periodo into grupo
                                 select new ReporteProductividadVentas { g = grupo.Key, l = grupo.ToList() });


                ReporteIndicadoresProductividadVentasCompuestoDTO reporte = new ReporteIndicadoresProductividadVentasCompuestoDTO();
                reporte.ReporteProductividadHorasTrabajadas = agrupado.ToList();
                reporte.ReporteHorasTrabajadasProductividadEquipo = agrupado3.ToList();
                reporte.ReporteIndicadoresProductividad = agrupado4.ToList();

                return reporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }

}
