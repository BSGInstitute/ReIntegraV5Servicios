using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class AnalisisProcesoSeleccionService : IAnalisisProcesoSeleccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public AnalisisProcesoSeleccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                
            });
            _mapper = new Mapper(config);
        }
        public ReportePrincipalAnalisisProcesoSeleccionDTO GenerarReporte(FiltroAnalisisProcesoSeleccionDTO Filtro)
        {
            try
            {
                if (Filtro.FechaInicio == null || Filtro.FechaFin == null)
                {
                    Filtro.FechaFin = DateTime.Now;
                    Filtro.FechaInicio = new DateTime(1900, 12, 31);
                }

                var fechaInicioChange = Filtro.FechaInicio?.ToString("yyyy-MM-dd");
                var fechaInicioChange2 = Convert.ToDateTime(fechaInicioChange);
                TimeSpan fechaInicioChange3 = new TimeSpan(00, 00, 00);
                fechaInicioChange2 = fechaInicioChange2 + fechaInicioChange3;
                Filtro.FechaInicio = fechaInicioChange2;

                var fechaFinChange = Filtro.FechaFin?.ToString("yyyy-MM-dd");
                var fechaFinChange2 = Convert.ToDateTime(fechaFinChange);
                TimeSpan fechaFinChange3 = new TimeSpan(23, 59, 59);
                fechaFinChange2 = fechaFinChange2 + fechaFinChange3;
                Filtro.FechaFin = fechaFinChange2;


                var listaEtapas = _unitOfWork.ProcesoSeleccionRepository.ObtenerReporteAnalisisProcesoSeleccion(Filtro).OrderBy(x => x.OrdenEtapa).ToList();
                List<ReporteAnalisisProcesoSeleccionPorcentajeDTO> listaPorcentaje = new List<ReporteAnalisisProcesoSeleccionPorcentajeDTO>();

                for (var i = 0; i < listaEtapas.Count; i++)
                {
                    if (i != 0)
                    {
                        listaEtapas[i].NumeroPostulante = listaEtapas[i - 1].Aprobados;
                    }

                    ReporteAnalisisProcesoSeleccionPorcentajeDTO PorcentajeEtapaReporte = new ReporteAnalisisProcesoSeleccionPorcentajeDTO();
                    PorcentajeEtapaReporte.IdEtapa = listaEtapas[i].IdEtapa;
                    PorcentajeEtapaReporte.IdProcesoSeleccion = listaEtapas[i].IdProcesoSeleccion;
                    PorcentajeEtapaReporte.IdProveedor = listaEtapas[i].IdProveedor;
                    PorcentajeEtapaReporte.NombreEtapa = listaEtapas[i].NombreEtapa;
                    PorcentajeEtapaReporte.Proveedor = listaEtapas[i].Proveedor;
                    PorcentajeEtapaReporte.OrdenEtapa = listaEtapas[i].OrdenEtapa;
                    PorcentajeEtapaReporte.Contactados = Math.Round((listaEtapas[i].Contactados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Aprobados = Math.Round((listaEtapas[i].Aprobados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Desaprobados = Math.Round((listaEtapas[i].Desaprobados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.EnProceso = Math.Round((listaEtapas[i].EnProceso * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Abandonados = Math.Round((listaEtapas[i].Abandonados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.SinRendir = Math.Round((listaEtapas[i].SinRendir * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    if (listaEtapas[i].OrdenEtapa == 1)
                    {
                        PorcentajeEtapaReporte.NumeroPostulante = Math.Round((listaEtapas[i].NumeroPostulante * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    }
                    else
                    {
                        PorcentajeEtapaReporte.NumeroPostulante = Math.Round((listaEtapas[i].NumeroPostulante * 100.0M) / (listaEtapas[i - 1].NumeroPostulante == 0 ? 1.0M : listaEtapas[i - 1].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    }
                    listaPorcentaje.Add(PorcentajeEtapaReporte);
                }
                ReportePrincipalAnalisisProcesoSeleccionDTO resultado = new ReportePrincipalAnalisisProcesoSeleccionDTO
                {
                    listaEtapas = listaEtapas,
                    listaEtapasPorcentaje = listaPorcentaje
                };
               return resultado;
            }
            catch (Exception e)
            {
                throw new BadRequestException("Error en Generar Reporte");
            }
        }

        public ReportePrincipalAnalisisProcesoSeleccionDTO GenerarReporte_V2(FiltroAnalisisProcesoSeleccionDTO Filtro)
        {
            try
            {
                if (Filtro.FechaInicio == null || Filtro.FechaFin == null)
                {
                    Filtro.FechaFin = DateTime.Now;
                    Filtro.FechaInicio = new DateTime(1900, 12, 31);
                }
                var listaEtapas = _unitOfWork.ProcesoSeleccionRepository.ObtenerReporteAnalisisProcesoSeleccion_V2(Filtro).ToList();

                List<ReporteAnalisisProcesoSeleccionPorcentajeDTO> listaPorcentaje = new List<ReporteAnalisisProcesoSeleccionPorcentajeDTO>();
                for (var i = 0; i < listaEtapas.Count; i++)
                {
                    if (listaEtapas[i].OrdenEtapa != 1)
                    {
                        listaEtapas[i].NumeroPostulante = listaEtapas[i - 1].Aprobados;
                    }
                    ReporteAnalisisProcesoSeleccionPorcentajeDTO PorcentajeEtapaReporte = new ReporteAnalisisProcesoSeleccionPorcentajeDTO();
                    PorcentajeEtapaReporte.IdEtapa = listaEtapas[i].IdEtapa;
                    PorcentajeEtapaReporte.IdProcesoSeleccion = listaEtapas[i].IdProcesoSeleccion;
                    PorcentajeEtapaReporte.IdProveedor = listaEtapas[i].IdProveedor;
                    PorcentajeEtapaReporte.NombreEtapa = listaEtapas[i].NombreEtapa;
                    PorcentajeEtapaReporte.Proveedor = listaEtapas[i].Proveedor;
                    PorcentajeEtapaReporte.OrdenEtapa = listaEtapas[i].OrdenEtapa;
                    PorcentajeEtapaReporte.Contactados = Math.Round((listaEtapas[i].Contactados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Aprobados = Math.Round((listaEtapas[i].Aprobados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Desaprobados = Math.Round((listaEtapas[i].Desaprobados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.EnProceso = Math.Round((listaEtapas[i].EnProceso * 100) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.Abandonados = Math.Round((listaEtapas[i].Abandonados * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    PorcentajeEtapaReporte.SinRendir = Math.Round((listaEtapas[i].SinRendir * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    if (listaEtapas[i].OrdenEtapa == 1)
                    {
                        PorcentajeEtapaReporte.NumeroPostulante = Math.Round((listaEtapas[i].NumeroPostulante * 100.0M) / (listaEtapas[i].NumeroPostulante == 0 ? 1.0M : listaEtapas[i].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    }
                    else
                    {
                        PorcentajeEtapaReporte.NumeroPostulante = Math.Round((listaEtapas[i].NumeroPostulante * 100.0M) / (listaEtapas[i - 1].NumeroPostulante == 0 ? 1.0M : listaEtapas[i - 1].NumeroPostulante * 1.0M), 0, MidpointRounding.AwayFromZero) + "%";
                    }
                    listaPorcentaje.Add(PorcentajeEtapaReporte);

                }
                ReportePrincipalAnalisisProcesoSeleccionDTO resultado = new ReportePrincipalAnalisisProcesoSeleccionDTO
                {
                    listaEtapas = listaEtapas,
                    listaEtapasPorcentaje = listaPorcentaje
                };
                return resultado;
                //return Ok(new { listaEtapasAgrupadas = listaEtapas, listaEtapasAgrupadasPorcentaje = listaPorcentaje });
            }
            catch (Exception e)
            {
                throw new BadRequestException("Error en Generar Reporte V2");
            }
        }

        public ReporteFiltroAnalisisDTO ObtenerComboFiltro()
        {
            try
            {
                ReporteFiltroAnalisisDTO resultado = new ReporteFiltroAnalisisDTO
                {
                    listaPuestoTrabajo = _unitOfWork.PuestoTrabajoRepository.ObtenerCombo(),
                    listaProcesoSeleccion = _unitOfWork.ProcesoSeleccionRepository.ObtenerCombo(),
                };
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
