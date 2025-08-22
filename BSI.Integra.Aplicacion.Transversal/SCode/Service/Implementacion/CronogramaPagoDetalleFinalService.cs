using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Api.Ads.AdWords.v201809;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PdfSharp.Pdf.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CronogramaPagoDetalleFinalService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_CronogramaPagoDetalleFinal
    /// </summary>
    public class CronogramaPagoDetalleFinalService : ICronogramaPagoDetalleFinalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CronogramaPagoDetalleFinalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCronogramaPagoDetalleFinal, CronogramaPagoDetalleFinal>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CronogramaPagoDetalleFinal Add(CronogramaPagoDetalleFinal entidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaPagoDetalleFinalRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CronogramaPagoDetalleFinal>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CronogramaPagoDetalleFinal Update(CronogramaPagoDetalleFinal entidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaPagoDetalleFinalRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CronogramaPagoDetalleFinal>(modelo);
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
                _unitOfWork.CronogramaPagoDetalleFinalRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CronogramaPagoDetalleFinal> Add(List<CronogramaPagoDetalleFinal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaPagoDetalleFinalRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CronogramaPagoDetalleFinal>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CronogramaPagoDetalleFinal> Update(List<CronogramaPagoDetalleFinal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaPagoDetalleFinalRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CronogramaPagoDetalleFinal>>(modelo);
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
                _unitOfWork.CronogramaPagoDetalleFinalRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CronogramaPagoDetalleFinal
        /// </summary>
        /// <returns> List<CronogramaPagoDetalleFinalDTO> </returns>
        public IEnumerable<CronogramaPagoDetalleFinalDTO> ObtenerCronogramaPagoDetalleFinal()
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerCronogramaPagoDetalleFinal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CronogramaPagoDetalleFinal para mostrarse en combo.
        /// </summary>
        /// <returns> List<CronogramaPagoDetalleFinalComboDTO> </returns>
        public IEnumerable<CronogramaPagoDetalleFinalComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerCombo();
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
        /// Obtiene las cuotas ordenadas asociadas a una Matricula Cabecera.
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matriula Cabecera</param>
        /// <returns> List<CronogramaPagoDetalleFinalCuotaDTO> </returns>
        public IEnumerable<CronogramaPagoDetalleFinalCuotaDTO> ObtenerListaCuotaPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerListaCuotaPorIdMatriculaCabecera(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// utor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// <summary>
        /// Obtiene el Cronograma Finanzas por la version y el IdMatriculaCabecera
        /// </summary>
        /// <param name="version"> Version </param>
        /// <param name="idMatriculaCabecera"> Id de la Matricula </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public List<CronogramaPagoDetalleFinalFinanzasDTO> ObtenerCronogramaFinanzasPorVersionYMCabecera(int version, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerCronogramaFinanzasPorVersionYMCabecera(version, idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 16/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CronogramaPagoDetalleFinal por IdMatriculaCabecera
        /// </summary>
        /// <returns> List<CronogramaPagoDetalleFinalDTO> </returns>
        public IEnumerable<CronogramaPagoDetalleFinalDTO> ObtenerCronograma(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerCronograma(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Lista del Programa de cuotas por medio del idMatricula
        /// </summary>
        /// <param name="idMatricula"></param>
        /// <returns> List<ProgramaListaCuotaDTO> </returns>
        public List<ProgramaListaCuotaDTO> ObtenerListaCuotaPrograma(int idMatricula)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerListaCuotaPrograma(idMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///Autor: Gkilmer Quispe
        ///Fecha: 02/12/2023
        /// <summary>
        /// Obtiene versiones de Fecha de Compromiso
        /// </summary>
        /// <param name="IdCuota"> Id de la cuota </param>
        /// <returns> Lista de Personal por nombre Registrados : List<ResultadoFechaCompromiso> </returns>
        public List<ResultadoFechaCompromiso> ObtenerVersionesFechaCompromiso(int idCuota)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerVersionesFechaCompromiso(idCuota);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///Autor: Juan D. Huanaco Quispe
        ///Fecha: 03/05/2024
        /// <summary>
        /// Obtiene los compromisos de una cuota
        /// </summary>
        /// <param name="IdCuota"> Id de la cuota </param>
        /// <returns> List<AgendaAtcCompromiso> </returns>
        public List<AgendaAtcCompromiso> ObtenerAgendaAtcCompromiso(int idCuota)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerAgendaAtcCompromiso(idCuota);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el comprobante de Pago por alumno
        /// </summary>
        /// <returns> List<ComprobantePagoOportunidadDTO> </returns>
        public bool ActualizarComprobantePago(ActualizarComprobantePagoAlumnoDTO data)
        {
            try
            {
                var repDetalle = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var entidad = _mapper.Map<CronogramaPagoDetalleFinal>(repDetalle.FirstById(data.Id));
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.IdTipoComprobante = data.TipoComprobante;
                entidad.NombreRazonSocial = data.NombreRazonSocial;
                entidad.Observacion = data.Observacion;
                entidad.NroDocumentoComprobante = data.NroDocumento;
                entidad = this.Update(entidad);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte de Pendientes.
        /// </summary>
        /// <returns> DTO: ReportePendienteGeneralDTO </returns>
        public ReportePendienteGeneralDTO GenerarReportePendienteOperacionesGeneral(ReportePendienteFiltroDTO filtroPendiente)
        {
            ReporteService reporteRepositorio = new ReporteService(_unitOfWork);

            var entities = reporteRepositorio.ObtenerReportePendientePeriodoYCoordinador(filtroPendiente).ToList();
            var cambios = reporteRepositorio.ObtenerReportePendienteCambiosPorCoordinador(filtroPendiente).ToList();
            var modificaciones = reporteRepositorio.ObtenerReportePendienteDiferencias(filtroPendiente).ToList();

            ReportePendienteGeneralDTO general = new ReportePendienteGeneralDTO();

            general.Periodo = entities;
            general.Cambios = cambios;
            general.Diferencias = modificaciones;

            return general;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reporte pendiente por periodo operaciones.
        /// </summary>
        /// <param name="respuestaGeneral"></param>
        /// <returns> Lista DTO: IList<ReportePendienteDetalleFinalDTO></returns>
        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendientePorPeriodoOperaciones(ReportePendienteGeneralDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.Periodo
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReportePendientePeriodoDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       Diferencia = grupo.Select(x => x.Diferencia).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                       DiferenciaRetirosCD = grupo.Select(x => x.DiferenciaRetirosCD).Sum(),
                                       DiferenciaRetirosSD = grupo.Select(x => x.DiferenciaRetirosSD).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       IngresoVentas = grupo.Select(x => x.IngresoVentas).Sum(),
                                       MontoRecuperadoMes = grupo.Select(x => x.MontoRecuperadoMes).Sum(),
                                       PagosAdelantadoAcumulado = grupo.Select(x => x.PagosAdelantadoAcumulado).Sum(),
                                       PendientePorFactura = grupo.Select(x => x.PendientePorFactura).Sum(),
                                       PendienteSinFactura = grupo.Select(x => x.PendienteSinFactura).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = respuestaGeneral.Cambios;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReportePendientesDiferenciasDTO temp = new ReportePendientesDiferenciasDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {

                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReportePendienteDetalleDTO detalle2 = new ReportePendienteDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReportePendienteDetalleDTO detalle3 = new ReportePendienteDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReportePendienteDetalleDTO detalle4 = new ReportePendienteDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReportePendienteDetalleDTO detalle5 = new ReportePendienteDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle5);
            //////////////////////////////////inicio
            ReportePendienteDetalleDTO detalle6 = new ReportePendienteDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReportePendienteDetalleDTO detalle7 = new ReportePendienteDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle7);
            ///////////////////////////////////fin
            ReportePendienteDetalleDTO detalle8 = new ReportePendienteDetalleDTO();
            detalle8.Tipo = "Proyectado Actual($)";
            detalle8.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReportePendienteDetalleDTO detalle12 = new ReportePendienteDetalleDTO();
            detalle12.Tipo = "Ingreso Ventas($)";
            detalle12.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReportePendienteDetalleDTO detalle13 = new ReportePendienteDetalleDTO();
            detalle13.Tipo = "Proyectado Inicial menos Ventas($)";
            detalle13.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle13);

            ReportePendienteDetalleDTO detalle18 = new ReportePendienteDetalleDTO();
            detalle18.Tipo = "Proyectado Actual menos Ventas($)";
            detalle18.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle18);

            ReportePendienteDetalleDTO detalle9 = new ReportePendienteDetalleDTO();
            detalle9.Tipo = "Monto Pagado menos Ventas($)";
            detalle9.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReportePendienteDetalleDTO detalle14 = new ReportePendienteDetalleDTO();
            detalle14.Tipo = "Recuperacion en el Mes($)";
            detalle14.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle14);

            ReportePendienteDetalleDTO detalle19 = new ReportePendienteDetalleDTO();
            detalle19.Tipo = "Pagos Adelantados($)";
            detalle19.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle19);

            ReportePendienteDetalleDTO detalle15 = new ReportePendienteDetalleDTO();
            detalle15.Tipo = "Pendiente($)";
            detalle15.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle15);

            ReportePendienteDetalleDTO detalle16 = new ReportePendienteDetalleDTO();
            detalle16.Tipo = "Pendiente por Factura($)";
            detalle16.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle16);

            ReportePendienteDetalleDTO detalle17 = new ReportePendienteDetalleDTO();
            detalle17.Tipo = "Pendiente sin Factura($)";
            detalle17.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle17);

            ReportePendienteDetalleDTO detalle10 = new ReportePendienteDetalleDTO();
            detalle10.Tipo = "% Cobrado/Inicial";
            detalle10.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle10);

            ReportePendienteDetalleDTO detalle11 = new ReportePendienteDetalleDTO();
            detalle11.Tipo = "% Cobrado/Actual";
            detalle11.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle11);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReportePendienteDetallesMesesDTO detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReportePendienteDetallesMesesDTO detallemes3 = new ReportePendienteDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReportePendienteDetallesMesesDTO detallemes4 = new ReportePendienteDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReportePendienteDetallesMesesDTO detallemes5 = new ReportePendienteDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                /////////////////////inicio

                //Retiros Con Devolucion
                ReportePendienteDetallesMesesDTO detallemes6 = new ReportePendienteDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.DiferenciaRetirosCD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReportePendienteDetallesMesesDTO detallemes7 = new ReportePendienteDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.DiferenciaRetirosSD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                /////////////////////fin

                //Actual
                ReportePendienteDetallesMesesDTO detallemes8 = new ReportePendienteDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Ingreso Ventas
                ReportePendienteDetallesMesesDTO detallemes12 = new ReportePendienteDetallesMesesDTO();
                detallemes12.Mes = item.PeriodoPorFechaVencimiento;
                detallemes12.Monto = item.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Proyectado Inicial menos Ventas
                ReportePendienteDetallesMesesDTO detallemes13 = new ReportePendienteDetallesMesesDTO();
                detallemes13.Mes = item.PeriodoPorFechaVencimiento;
                detallemes13.Monto = (item.Proyectado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                //Proyectado Actual menos Ventas
                ReportePendienteDetallesMesesDTO detallemes18 = new ReportePendienteDetallesMesesDTO();
                detallemes18.Mes = item.PeriodoPorFechaVencimiento;
                detallemes18.Monto = (item.Actual - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //MontoPagado
                ReportePendienteDetallesMesesDTO detallemes9 = new ReportePendienteDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = (item.MontoPagado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Monto Pagado menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Recuperacion en el Mes
                ReportePendienteDetallesMesesDTO detallemes14 = new ReportePendienteDetallesMesesDTO();
                detallemes14.Mes = item.PeriodoPorFechaVencimiento;
                detallemes14.Monto = item.MontoRecuperadoMes.ToString();
                detalles.Where(w => w.Tipo == "Recuperacion en el Mes($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);

                //Pagos Adelantados
                ReportePendienteDetallesMesesDTO detallemes19 = new ReportePendienteDetallesMesesDTO();
                detallemes19.Mes = item.PeriodoPorFechaVencimiento;
                detallemes19.Monto = item.PagosAdelantadoAcumulado.ToString();
                detalles.Where(w => w.Tipo == "Pagos Adelantados($)").FirstOrDefault().ListaMontosMeses.Add(detallemes19);

                //Pendiente
                ReportePendienteDetallesMesesDTO detallemes15 = new ReportePendienteDetallesMesesDTO();
                detallemes15.Mes = item.PeriodoPorFechaVencimiento;
                detallemes15.Monto = ((item.Actual - item.IngresoVentas) - (item.MontoPagado - item.IngresoVentas)).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //Pendiente por factura
                ReportePendienteDetallesMesesDTO detallemes16 = new ReportePendienteDetallesMesesDTO();
                detallemes16.Mes = item.PeriodoPorFechaVencimiento;
                detallemes16.Monto = item.PendientePorFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente por Factura($)").FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Pendiente sin factura
                ReportePendienteDetallesMesesDTO detallemes17 = new ReportePendienteDetallesMesesDTO();
                detallemes17.Mes = item.PeriodoPorFechaVencimiento;
                detallemes17.Monto = item.PendienteSinFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente sin Factura($)").FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //%CobradoInicial
                ReportePendienteDetallesMesesDTO detallemes10 = new ReportePendienteDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Proyectado - item.IngresoVentas == 0)
                {
                    detallemes10.Monto = "% " + (0.00m * 100).ToString("0.00");
                }
                else
                {
                    detallemes10.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Proyectado - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Inicial").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //%CobradoActual
                ReportePendienteDetallesMesesDTO detallemes11 = new ReportePendienteDetallesMesesDTO();
                detallemes11.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Actual - item.IngresoVentas == 0)
                {
                    detallemes11.Monto = "% " + (0.00m * 100).ToString("0.00");
                }
                else
                {
                    detallemes11.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Actual - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Actual").FirstOrDefault().ListaMontosMeses.Add(detallemes11);
            }
            List<ReportePendienteDetalleFinalDTO> finales = new List<ReportePendienteDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalDTO item = new ReportePendienteDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }
            }
            return finales;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera el reporte pendiente del ingreso de ventas por periodo operaciones anterior.
        /// </summary>
        /// <param name="respuestaGeneral"></param>
        /// <returns> Lista DTO: IList<ReportePendienteDetalleFinalDTO> </returns>
        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendienteIngresoVentasPorPeriodoOperacionesAnterior(ReportePendienteGeneralDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.Periodo
                                   group r by new { r.PeriodoPorFechaVencimiento } into grupo
                                   select new ReportePendientePeriodoTotalizadoDTO
                                   {
                                       PeriodoPorFechaVencimiento = grupo.Key.PeriodoPorFechaVencimiento,

                                       IngresoVentas = grupo.Select(x => x.MatriculadosFechaPago).Sum(),
                                       PagoEnFechaVenc = grupo.Select(x => x.MatriculadosFechaVencimiento).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.PeriodoPorFechaVencimiento);

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Ingreso Matriculas según Fecha de Cronograma($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReportePendienteDetalleDTO detalle2 = new ReportePendienteDetalleDTO();
            detalle2.Tipo = "Ingreso según Fecha de Pago($)";
            detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle2);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.PagoEnFechaVenc.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Matriculas según Fecha de Cronograma($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReportePendienteDetallesMesesDTO detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso según Fecha de Pago($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

            }
            List<ReportePendienteDetalleFinalDTO> finales = new List<ReportePendienteDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalDTO item = new ReportePendienteDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }
            }
            return finales;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reporte pendiente por coordinadora operaciones.
        /// </summary>
        /// <param name="respuestaGeneral"></param>
        /// <returns> IList<ReportePendienteDetalleFinalDTO> </returns>
        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendientePorCoordinadoraOperaciones(ReportePendienteGeneralDTO respuestaGeneral)
        {
            var agrupadoGeneral = (from r in respuestaGeneral.Periodo
                                   group r by new { r.Coordinador } into grupo
                                   select new ReportePendientePeriodoPorCoordinadorDTO
                                   {
                                       Coordinador = grupo.Key.Coordinador,

                                       Proyectado = grupo.Select(x => x.Proyectado).Sum(),
                                       Actual = grupo.Select(x => x.Actual).Sum(),
                                       Diferencia = grupo.Select(x => x.Diferencia).Sum(),
                                       DiferenciaCambioFecha = grupo.Select(x => x.DiferenciaCambioFecha).Sum(),
                                       DiferenciaCambioMonto = grupo.Select(x => x.DiferenciaCambioMonto).Sum(),
                                       DiferenciaConsiderarMoraAdelantoSgteCuota = grupo.Select(x => x.DiferenciaConsiderarMoraAdelantoSgteCuota).Sum(),
                                       DiferenciaModificacionNroCuotas = grupo.Select(x => x.DiferenciaModificacionNroCuotas).Sum(),
                                       DiferenciaRetirosCD = grupo.Select(x => x.DiferenciaRetirosCD).Sum(),
                                       DiferenciaRetirosSD = grupo.Select(x => x.DiferenciaRetirosSD).Sum(),
                                       MontoPagado = grupo.Select(x => x.MontoPagado).Sum(),
                                       IngresoVentas = grupo.Select(x => x.IngresoVentas).Sum(),
                                       PendientePorFactura = grupo.Select(x => x.PendientePorFactura).Sum(),
                                       PendienteSinFactura = grupo.Select(x => x.PendienteSinFactura).Sum(),
                                       MontoVencido = grupo.Select(x => x.MontoVencido).Sum(),
                                       MontoPorVencer = grupo.Select(x => x.MontoPorVencer).Sum(),
                                       PagoPrevio = grupo.Select(x => x.PagoPrevio).Sum(),
                                       PagoDentroMes = grupo.Select(x => x.PagoDentroMes).Sum(),
                                   });

            var entities = agrupadoGeneral.ToList().OrderBy(x => x.Coordinador);
            var cambios = respuestaGeneral.Cambios;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReportePendientesDiferenciasDTO temp = new ReportePendientesDiferenciasDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {
                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetalleDTO> detalles = new List<ReportePendienteDetalleDTO>();

            ReportePendienteDetalleDTO detalle1 = new ReportePendienteDetalleDTO();
            detalle1.Tipo = "Proyectado Inicial($)";
            detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle1);

            ReportePendienteDetalleDTO detalle2 = new ReportePendienteDetalleDTO();
            detalle2.Tipo = "Ajuste Cambio Fecha($)";
            detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle2);

            ReportePendienteDetalleDTO detalle3 = new ReportePendienteDetalleDTO();
            detalle3.Tipo = "Ajuste Cambio Monto($)";
            detalle3.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle3);

            ReportePendienteDetalleDTO detalle4 = new ReportePendienteDetalleDTO();
            detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
            detalle4.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle4);

            ReportePendienteDetalleDTO detalle5 = new ReportePendienteDetalleDTO();
            detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
            detalle5.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle5);
            //////////////////////////////////inicio
            ReportePendienteDetalleDTO detalle6 = new ReportePendienteDetalleDTO();
            detalle6.Tipo = "Retiros Con Devolucion($)";
            detalle6.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle6);

            ReportePendienteDetalleDTO detalle7 = new ReportePendienteDetalleDTO();
            detalle7.Tipo = "Retiros Sin Devolucion($)";
            detalle7.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle7);
            ///////////////////////////////////fin

            ReportePendienteDetalleDTO detalle22 = new ReportePendienteDetalleDTO();
            detalle22.Tipo = "Proyectado Actual($)";
            detalle22.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle22);

            ReportePendienteDetalleDTO detalle21 = new ReportePendienteDetalleDTO();
            detalle21.Tipo = "Proyectado Inicial menos Ventas($)";
            detalle21.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle21);

            ReportePendienteDetalleDTO detalle19 = new ReportePendienteDetalleDTO();
            detalle19.Tipo = "Proyectado Actual menos Ventas($)";
            detalle19.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle19);

            ReportePendienteDetalleDTO detalle8 = new ReportePendienteDetalleDTO();
            detalle8.Tipo = "Vencido($)";
            detalle8.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle8);

            ReportePendienteDetalleDTO detalle12 = new ReportePendienteDetalleDTO();
            detalle12.Tipo = "Por Vencer($)";
            detalle12.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle12);

            ReportePendienteDetalleDTO detalle9 = new ReportePendienteDetalleDTO();
            detalle9.Tipo = "Monto Pagado($)";
            detalle9.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle9);

            ReportePendienteDetalleDTO detalle14 = new ReportePendienteDetalleDTO();
            detalle14.Tipo = "Real Ingreso Previo($)";
            detalle14.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle14);

            ReportePendienteDetalleDTO detalle18 = new ReportePendienteDetalleDTO();
            detalle18.Tipo = "Real Ingreso($)";
            detalle18.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle18);

            ReportePendienteDetalleDTO detalle15 = new ReportePendienteDetalleDTO();
            detalle15.Tipo = "Pendiente($)";
            detalle15.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle15);

            ReportePendienteDetalleDTO detalle16 = new ReportePendienteDetalleDTO();
            detalle16.Tipo = "Pendiente por Factura($)";
            detalle16.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle16);

            ReportePendienteDetalleDTO detalle17 = new ReportePendienteDetalleDTO();
            detalle17.Tipo = "Pendiente sin Factura($)";
            detalle17.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle17);

            ReportePendienteDetalleDTO detalle10 = new ReportePendienteDetalleDTO();
            detalle10.Tipo = "% Cobrado/Inicial";
            detalle10.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle10);

            ReportePendienteDetalleDTO detalle20 = new ReportePendienteDetalleDTO();
            detalle20.Tipo = "% Cobrado/Actual";
            detalle20.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle20);

            ReportePendienteDetalleDTO detalle11 = new ReportePendienteDetalleDTO();
            detalle11.Tipo = "% Cobrado/Vencido";
            detalle11.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
            detalles.Add(detalle11);

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.Coordinador;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)").FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReportePendienteDetallesMesesDTO detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = item.Coordinador;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)").FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReportePendienteDetallesMesesDTO detallemes3 = new ReportePendienteDetallesMesesDTO();
                detallemes3.Mes = item.Coordinador;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)").FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReportePendienteDetallesMesesDTO detallemes4 = new ReportePendienteDetallesMesesDTO();
                detallemes4.Mes = item.Coordinador;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)").FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReportePendienteDetallesMesesDTO detallemes5 = new ReportePendienteDetallesMesesDTO();
                detallemes5.Mes = item.Coordinador;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                /////////////////////inicio

                //Retiros Con Devolucion
                ReportePendienteDetallesMesesDTO detallemes6 = new ReportePendienteDetallesMesesDTO();
                detallemes6.Mes = item.Coordinador;
                detallemes6.Monto = (item.DiferenciaRetirosCD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReportePendienteDetallesMesesDTO detallemes7 = new ReportePendienteDetallesMesesDTO();
                detallemes7.Mes = item.Coordinador;
                detallemes7.Monto = (item.DiferenciaRetirosSD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)").FirstOrDefault().ListaMontosMeses.Add(detallemes7);

                //Proyectado Actual
                ReportePendienteDetallesMesesDTO detallemes22 = new ReportePendienteDetallesMesesDTO();
                detallemes22.Mes = item.Coordinador;
                detallemes22.Monto = (item.Actual).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)").FirstOrDefault().ListaMontosMeses.Add(detallemes22);

                /////////////////////fin
                //Proyectado con Cambios Inicial
                ReportePendienteDetallesMesesDTO detallemes21 = new ReportePendienteDetallesMesesDTO();
                detallemes21.Mes = item.Coordinador;
                detallemes21.Monto = (item.Proyectado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes21);

                //Proyectado con Cambios Actual
                ReportePendienteDetallesMesesDTO detallemes19 = new ReportePendienteDetallesMesesDTO();
                detallemes19.Mes = item.Coordinador;
                detallemes19.Monto = (item.Actual - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas($)").FirstOrDefault().ListaMontosMeses.Add(detallemes19);

                //Vencido
                ReportePendienteDetallesMesesDTO detallemes8 = new ReportePendienteDetallesMesesDTO();
                detallemes8.Mes = item.Coordinador;
                detallemes8.Monto = item.MontoVencido.ToString();
                detalles.Where(w => w.Tipo == "Vencido($)").FirstOrDefault().ListaMontosMeses.Add(detallemes8);

                //Por Vencer
                ReportePendienteDetallesMesesDTO detallemes12 = new ReportePendienteDetallesMesesDTO();
                detallemes12.Mes = item.Coordinador;
                detallemes12.Monto = item.MontoPorVencer.ToString();
                detalles.Where(w => w.Tipo == "Por Vencer($)").FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //MontoPagado
                ReportePendienteDetallesMesesDTO detallemes9 = new ReportePendienteDetallesMesesDTO();
                detallemes9.Mes = item.Coordinador;
                detallemes9.Monto = (item.MontoPagado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Monto Pagado($)").FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Real Ingreso Previo
                ReportePendienteDetallesMesesDTO detallemes14 = new ReportePendienteDetallesMesesDTO();
                detallemes14.Mes = item.Coordinador;
                detallemes14.Monto = item.PagoPrevio.ToString();
                detalles.Where(w => w.Tipo == "Real Ingreso Previo($)").FirstOrDefault().ListaMontosMeses.Add(detallemes14);


                //Real Ingreso
                ReportePendienteDetallesMesesDTO detallemes18 = new ReportePendienteDetallesMesesDTO();
                detallemes18.Mes = item.Coordinador;
                detallemes18.Monto = item.PagoDentroMes.ToString();
                detalles.Where(w => w.Tipo == "Real Ingreso($)").FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //Pendiente
                ReportePendienteDetallesMesesDTO detallemes15 = new ReportePendienteDetallesMesesDTO();
                detallemes15.Mes = item.Coordinador;
                detallemes15.Monto = ((item.Actual - item.IngresoVentas) - (item.MontoPagado - item.IngresoVentas)).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)").FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //Pendiente por factura
                ReportePendienteDetallesMesesDTO detallemes16 = new ReportePendienteDetallesMesesDTO();
                detallemes16.Mes = item.Coordinador;
                detallemes16.Monto = item.PendientePorFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente por Factura($)").FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Pendiente sin factura
                ReportePendienteDetallesMesesDTO detallemes17 = new ReportePendienteDetallesMesesDTO();
                detallemes17.Mes = item.Coordinador;
                detallemes17.Monto = item.PendienteSinFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente sin Factura($)").FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //%CobradoInicial
                ReportePendienteDetallesMesesDTO detallemes10 = new ReportePendienteDetallesMesesDTO();
                detallemes10.Mes = item.Coordinador;
                if (item.Proyectado - item.IngresoVentas == 0)
                {
                    detallemes10.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes10.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Proyectado - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Inicial").FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //%CobradoActual
                ReportePendienteDetallesMesesDTO detallemes20 = new ReportePendienteDetallesMesesDTO();
                detallemes20.Mes = item.Coordinador;
                if (item.Actual - item.IngresoVentas == 0)
                {
                    detallemes20.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes20.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Actual - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Actual").FirstOrDefault().ListaMontosMeses.Add(detallemes20);

                //%CobradoVencido
                ReportePendienteDetallesMesesDTO detallemes11 = new ReportePendienteDetallesMesesDTO();
                detallemes11.Mes = item.Coordinador;
                if (item.MontoVencido == 0.00m)
                {
                    detallemes11.Monto = "% " + (0.00m * 100).ToString("0.00");
                }
                else
                {
                    detallemes11.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / item.MontoVencido) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Vencido").FirstOrDefault().ListaMontosMeses.Add(detallemes11);

            }
            List<ReportePendienteDetalleFinalDTO> finales = new List<ReportePendienteDetalleFinalDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalDTO item = new ReportePendienteDetalleFinalDTO();
                    item.TipoMonto = det.Tipo;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }
            }
            return finales;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reporte pendiente por periodo y coordinador operaciones.
        /// </summary>
        /// <param name="respuestaGeneral"></param>
        /// <returns> Lista DTO: IList<ReportePendienteDetalleFinalPorCoordinadorDTO> </returns>
        public IList<ReportePendienteDetalleFinalPorCoordinadorDTO> GenerarReportePendientePeriodoYCoordinadorOperaciones(ReportePendienteGeneralDTO respuestaGeneral)
        {

            var entities = respuestaGeneral.Periodo.OrderBy(x => x.PeriodoPorFechaVencimiento);
            var cambios = respuestaGeneral.Cambios;
            var modificaciones = respuestaGeneral.Diferencias;

            var agrupadomatricula = (from p in modificaciones
                                     group p by new { p.IdMatricula, p.NroCuota, p.NroSubCuota } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

            foreach (var cambio in cambios)
            {

                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == cambio.IdMatricula && w.g.NroCuota == cambio.NroCuota && w.g.NroSubCuota == cambio.NroSubCuota).FirstOrDefault();
                if (DiferenciaPorCambio == null)
                {
                    continue;
                }
                ReportePendientesDiferenciasDTO temp = new ReportePendientesDiferenciasDTO();
                //TCRM_ReporteDiferenciasV2JCDTO temp = new TCRM_ReporteDiferenciasV2JCDTO();
                temp = DiferenciaPorCambio.l.OrderBy(w => w.Version).FirstOrDefault();
                foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                {
                    if (modi.DescripcionCambio == "Cambio de monto" && modi.DetalleCambio == "Una cuota" || modi.DetalleCambio == "Cambio de moneda")
                    {
                        cambio.Cambio = "Cambio de monto";
                    }
                    if (modi.DescripcionCambio == "Cambio de fecha" && modi.DetalleCambio == "Una cuota")
                    {
                        if ((temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual && modi.Diferencia > 0) || (temp.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Cambio de fecha";
                            break;
                        }
                        else
                        {
                            temp.PeriodoaProyectado = modi.PruebaFechaVencimiento;
                        }

                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Fraccionar cuotas")
                    {
                        if (modi.PruebaFechaVencimiento == modi.PeriodoActual || (modi.PruebaFechaVencimiento != modi.PeriodoActual && modi.Diferencia < 0))
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                            break;
                        }
                    }
                    if (modi.DescripcionCambio == "Cambio de montos" && modi.DetalleCambio == "Considerar mora como adelanto de la sgte cuota")
                    {
                        cambio.Cambio = "Considerar mora como adelanto de la sgte cuota";
                    }
                    if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                    {
                        if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento == modi.PeriodoActual)
                        {
                            cambio.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }

            //Para arregalr losque faltan si es que faltan
            foreach (var item in cambios.Where(w => w.Cambio == ""))
            {
                var DiferenciaPorCambio = agrupadomatricula.Where(w => w.g.IdMatricula == item.IdMatricula && w.g.NroCuota == item.NroCuota && w.g.NroSubCuota == item.NroSubCuota).FirstOrDefault();

                if (DiferenciaPorCambio == null)
                {
                }
                if (DiferenciaPorCambio != null)
                {
                    foreach (var modi in DiferenciaPorCambio.l.OrderBy(w => w.Version))
                    {
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas Pago")
                        {
                            if (modi.PeriodoaProyectado != modi.PruebaFechaVencimiento && modi.PruebaFechaVencimiento != modi.PeriodoActual)
                            {
                                item.Cambio = "Modificacion de numeros de cuotas";
                            }
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Eliminar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                        if (modi.DescripcionCambio == "Modificacion de numeros de cuotas" && modi.DetalleCambio == "Agregar cuotas")
                        {
                            item.Cambio = "Modificacion de numeros de cuotas";
                        }
                    }
                }
            }
            //fin Para arregalr losque faltan si es que faltan

            foreach (var item in cambios)
            {
                if (item.PeriodoActual != null)
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoActual && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else
                {
                    switch (item.Cambio)
                    {
                        case "Cambio de fecha":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioFecha + item.Diferencia;
                            break;
                        case "Modificacion de numeros de cuotas":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaModificacionNroCuotas + item.Diferencia;
                            break;
                        case "Cambio de monto":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaCambioMonto + item.Diferencia;
                            break;
                        case "Considerar mora como adelanto de la sgte cuota":
                            entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota = entities.Where(w => w.PeriodoPorFechaVencimiento == item.PeriodoProyectado && w.Coordinador == item.Coordinador).FirstOrDefault().DiferenciaConsiderarMoraAdelantoSgteCuota + item.Diferencia;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
            }
            //var carlos = cambios.Where(w => w.cambio == "").ToList();

            /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

            List<ReportePendienteDetallePorCoordinadorDTO> detalles = new List<ReportePendienteDetallePorCoordinadorDTO>();
            var listaCoordinadoras = entities.Select(x => x.Coordinador).Distinct();
            foreach (var item in listaCoordinadoras)
            {

                ReportePendienteDetallePorCoordinadorDTO detalle1 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle1.Tipo = "Proyectado Inicial($)";
                detalle1.Coordinador = item;
                detalle1.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle1);

                ReportePendienteDetallePorCoordinadorDTO detalle2 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle2.Tipo = "Ajuste Cambio Fecha($)";
                detalle2.Coordinador = item;
                detalle2.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle2);

                ReportePendienteDetallePorCoordinadorDTO detalle3 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle3.Tipo = "Ajuste Cambio Monto($)";
                detalle3.Coordinador = item;
                detalle3.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle3);

                ReportePendienteDetallePorCoordinadorDTO detalle4 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle4.Tipo = "Ajuste Considerar Mora Adelanto Sgte Cuota($)";
                detalle4.Coordinador = item;
                detalle4.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle4);

                ReportePendienteDetallePorCoordinadorDTO detalle5 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle5.Tipo = "Ajuste Modificacion Nro Cuotas($)";
                detalle5.Coordinador = item;
                detalle5.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle5);
                //////////////////////////////////inicio
                ReportePendienteDetallePorCoordinadorDTO detalle6 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle6.Tipo = "Retiros Con Devolucion($)";
                detalle6.Coordinador = item;
                detalle6.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle6);

                ReportePendienteDetallePorCoordinadorDTO detalle7 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle7.Tipo = "Retiros Sin Devolucion($)";
                detalle7.Coordinador = item;
                detalle7.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle7);
                ///////////////////////////////////fin
                ReportePendienteDetallePorCoordinadorDTO detalle8 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle8.Tipo = "Proyectado Actual($)";
                detalle8.Coordinador = item;
                detalle8.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle8);

                ReportePendienteDetallePorCoordinadorDTO detalle12 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle12.Tipo = "Ingreso Ventas($)";
                detalle12.Coordinador = item;
                detalle12.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle12);

                ReportePendienteDetallePorCoordinadorDTO detalle13 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle13.Tipo = "Proyectado Inicial menos Ventas($)";
                detalle13.Coordinador = item;
                detalle13.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle13);

                ReportePendienteDetallePorCoordinadorDTO detalle18 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle18.Tipo = "Proyectado Actual menos Ventas($)";
                detalle18.Coordinador = item;
                detalle18.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle18);

                ReportePendienteDetallePorCoordinadorDTO detalle9 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle9.Tipo = "Monto Pagado menos Ventas($)";
                detalle9.Coordinador = item;
                detalle9.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle9);

                ReportePendienteDetallePorCoordinadorDTO detalle14 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle14.Tipo = "Recuperacion en el Mes($)";
                detalle14.Coordinador = item;
                detalle14.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle14);

                ReportePendienteDetallePorCoordinadorDTO detalle19 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle19.Tipo = "Pagos Adelantados($)";
                detalle19.Coordinador = item;
                detalle19.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle19);

                ReportePendienteDetallePorCoordinadorDTO detalle15 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle15.Tipo = "Pendiente($)";
                detalle15.Coordinador = item;
                detalle15.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle15);

                ReportePendienteDetallePorCoordinadorDTO detalle16 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle16.Tipo = "Pendiente por Factura($)";
                detalle16.Coordinador = item;
                detalle16.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle16);

                ReportePendienteDetallePorCoordinadorDTO detalle17 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle17.Tipo = "Pendiente sin Factura($)";
                detalle17.Coordinador = item;
                detalle17.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle17);

                ReportePendienteDetallePorCoordinadorDTO detalle10 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle10.Tipo = "% Cobrado/Inicial";
                detalle10.Coordinador = item;
                detalle10.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle10);

                ReportePendienteDetallePorCoordinadorDTO detalle11 = new ReportePendienteDetallePorCoordinadorDTO();
                detalle11.Tipo = "% Cobrado/Actual";
                detalle11.Coordinador = item;
                detalle11.ListaMontosMeses = new List<ReportePendienteDetallesMesesDTO>();
                detalles.Add(detalle11);
            }

            foreach (var item in entities)
            {
                //Proyectado Inicial
                ReportePendienteDetallesMesesDTO detallemes1 = new ReportePendienteDetallesMesesDTO();
                detallemes1.Mes = item.PeriodoPorFechaVencimiento;
                detallemes1.Monto = item.Proyectado.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes1);

                //DiferenciaCambioFecha
                ReportePendienteDetallesMesesDTO detallemes2 = new ReportePendienteDetallesMesesDTO();
                detallemes2.Mes = item.PeriodoPorFechaVencimiento;
                detallemes2.Monto = item.DiferenciaCambioFecha.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Fecha($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes2);

                //DiferenciaCambioMonto
                ReportePendienteDetallesMesesDTO detallemes3 = new ReportePendienteDetallesMesesDTO();
                detallemes3.Mes = item.PeriodoPorFechaVencimiento;
                detallemes3.Monto = item.DiferenciaCambioMonto.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Cambio Monto($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes3);

                //DiferenciaConsiderarMoraAdelantoSgteCuota
                ReportePendienteDetallesMesesDTO detallemes4 = new ReportePendienteDetallesMesesDTO();
                detallemes4.Mes = item.PeriodoPorFechaVencimiento;
                detallemes4.Monto = item.DiferenciaConsiderarMoraAdelantoSgteCuota.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Considerar Mora Adelanto Sgte Cuota($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes4);

                //DiferenciaModificacionNroCuotas
                ReportePendienteDetallesMesesDTO detallemes5 = new ReportePendienteDetallesMesesDTO();
                detallemes5.Mes = item.PeriodoPorFechaVencimiento;
                detallemes5.Monto = item.DiferenciaModificacionNroCuotas.ToString();
                detalles.Where(w => w.Tipo == "Ajuste Modificacion Nro Cuotas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes5);

                /////////////////////inicio

                //Retiros Con Devolucion
                ReportePendienteDetallesMesesDTO detallemes6 = new ReportePendienteDetallesMesesDTO();
                detallemes6.Mes = item.PeriodoPorFechaVencimiento;
                detallemes6.Monto = (item.DiferenciaRetirosCD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Con Devolucion($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes6);

                //Retiros Sin Devolucion
                ReportePendienteDetallesMesesDTO detallemes7 = new ReportePendienteDetallesMesesDTO();
                detallemes7.Mes = item.PeriodoPorFechaVencimiento;
                detallemes7.Monto = (item.DiferenciaRetirosSD * -1).ToString();
                detalles.Where(w => w.Tipo == "Retiros Sin Devolucion($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes7);


                /////////////////////fin

                //Actual
                ReportePendienteDetallesMesesDTO detallemes8 = new ReportePendienteDetallesMesesDTO();
                detallemes8.Mes = item.PeriodoPorFechaVencimiento;
                detallemes8.Monto = item.Actual.ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes8);


                //Ingreso Ventas
                ReportePendienteDetallesMesesDTO detallemes12 = new ReportePendienteDetallesMesesDTO();
                detallemes12.Mes = item.PeriodoPorFechaVencimiento;
                detallemes12.Monto = item.IngresoVentas.ToString();
                detalles.Where(w => w.Tipo == "Ingreso Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes12);

                //Proyectado Inicial menos Ventas
                ReportePendienteDetallesMesesDTO detallemes13 = new ReportePendienteDetallesMesesDTO();
                detallemes13.Mes = item.PeriodoPorFechaVencimiento;
                detallemes13.Monto = (item.Proyectado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Inicial menos Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes13);

                //Proyectado Actual menos Ventas
                ReportePendienteDetallesMesesDTO detallemes18 = new ReportePendienteDetallesMesesDTO();
                detallemes18.Mes = item.PeriodoPorFechaVencimiento;
                detallemes18.Monto = (item.Actual - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Proyectado Actual menos Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes18);

                //MontoPagado
                ReportePendienteDetallesMesesDTO detallemes9 = new ReportePendienteDetallesMesesDTO();
                detallemes9.Mes = item.PeriodoPorFechaVencimiento;
                detallemes9.Monto = (item.MontoPagado - item.IngresoVentas).ToString();
                detalles.Where(w => w.Tipo == "Monto Pagado menos Ventas($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes9);

                //Recuperacion en el Mes
                ReportePendienteDetallesMesesDTO detallemes14 = new ReportePendienteDetallesMesesDTO();
                detallemes14.Mes = item.PeriodoPorFechaVencimiento;
                detallemes14.Monto = item.MontoRecuperadoMes.ToString();
                detalles.Where(w => w.Tipo == "Recuperacion en el Mes($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes14);

                //Pagos Adelantados
                ReportePendienteDetallesMesesDTO detallemes19 = new ReportePendienteDetallesMesesDTO();
                detallemes19.Mes = item.PeriodoPorFechaVencimiento;
                detallemes19.Monto = item.PagosAdelantadoAcumulado.ToString();
                detalles.Where(w => w.Tipo == "Pagos Adelantados($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes19);

                //Pendiente
                ReportePendienteDetallesMesesDTO detallemes15 = new ReportePendienteDetallesMesesDTO();
                detallemes15.Mes = item.PeriodoPorFechaVencimiento;
                detallemes15.Monto = ((item.Actual - item.IngresoVentas) - (item.MontoPagado - item.IngresoVentas)).ToString();
                detalles.Where(w => w.Tipo == "Pendiente($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes15);

                //Pendiente por factura
                ReportePendienteDetallesMesesDTO detallemes16 = new ReportePendienteDetallesMesesDTO();
                detallemes16.Mes = item.PeriodoPorFechaVencimiento;
                detallemes16.Monto = item.PendientePorFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente por Factura($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes16);

                //Pendiente sin factura
                ReportePendienteDetallesMesesDTO detallemes17 = new ReportePendienteDetallesMesesDTO();
                detallemes17.Mes = item.PeriodoPorFechaVencimiento;
                detallemes17.Monto = item.PendienteSinFactura.ToString();
                detalles.Where(w => w.Tipo == "Pendiente sin Factura($)" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes17);

                //%CobradoInicial
                ReportePendienteDetallesMesesDTO detallemes10 = new ReportePendienteDetallesMesesDTO();
                detallemes10.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Proyectado - item.IngresoVentas == 0)
                {
                    detallemes10.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes10.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Proyectado - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Inicial" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes10);

                //%CobradoActual
                ReportePendienteDetallesMesesDTO detallemes11 = new ReportePendienteDetallesMesesDTO();
                detallemes11.Mes = item.PeriodoPorFechaVencimiento;
                if (item.Actual - item.IngresoVentas == 0)
                {
                    detallemes11.Monto = "% " + (0.00m * 100).ToString("0.00");

                }
                else
                {
                    detallemes11.Monto = "% " + (((item.MontoPagado - item.IngresoVentas) / (item.Actual - item.IngresoVentas)) * 100).ToString("0.00");
                }
                detalles.Where(w => w.Tipo == "% Cobrado/Actual" && w.Coordinador == item.Coordinador).FirstOrDefault().ListaMontosMeses.Add(detallemes11);
            }
            List<ReportePendienteDetalleFinalPorCoordinadorDTO> finales = new List<ReportePendienteDetalleFinalPorCoordinadorDTO>();
            foreach (var det in detalles)
            {
                foreach (var mes in det.ListaMontosMeses)
                {
                    ReportePendienteDetalleFinalPorCoordinadorDTO item = new ReportePendienteDetalleFinalPorCoordinadorDTO();
                    item.TipoMonto = det.Tipo;
                    item.Coordinador = det.Coordinador;
                    item.Periodo = mes.Mes;
                    item.Monto = mes.Monto;
                    finales.Add(item);
                }
            }
            return finales;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte de Pendientes
        /// </summary>
        /// <returns> DTO: general - PagosDiaPeriodoGeneralDTO </returns>
        public PagosDiaPeriodoGeneralDTO GenerarReportePagosDiaPeriodoGeneral(ReportePagosDiaPeriodoFiltroDTO FiltroReportePagosDiaPeriodo)
        {
            ReporteService reporteRepositorio = new ReporteService(_unitOfWork);
            var entities = reporteRepositorio.ObtenerReportePagosDia(FiltroReportePagosDiaPeriodo).OrderBy(x => x.FechaPagoDia).ToList();
            var entitiesperiodo = reporteRepositorio.ObtenerReportePagosPeriodo(FiltroReportePagosDiaPeriodo).OrderBy(x => x.FechaPagoDia).ToList();
            //var modificaciones = reporteRepositorio.ObtenerReportePendienteDiferencias(FiltroPendiente).ToList(); 

            PagosDiaPeriodoGeneralDTO general = new PagosDiaPeriodoGeneralDTO();
            general.Periodo = entities;
            general.PeriodoMeses = entitiesperiodo;
            //general.Diferencias = modificaciones;
            return general;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte de Pagos por Dia
        /// </summary>
        /// <returns> Lista: ReportePagosDiaPeriodoAgrupadoDTO </returns>
        public IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> GenerarReportePagosPorDia(PagosDiaPeriodoGeneralDTO respuestaGeneral)
        {

            IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> agrupado = null;

            DateTime date2 = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime date3 = new DateTime(2400, 1, 1, 0, 0, 0);

            DateTime date4 = new DateTime(1900, 12, 12, 0, 0, 0);
            DateTime date5 = new DateTime(2400, 12, 12, 0, 0, 0);

            agrupado = respuestaGeneral.Periodo.GroupBy(x => x.FechaPagoDia)
            .Select(g => new ReportePagosDiaPeriodoAgrupadoDTO
            {
                Periodo = g.Key == date2 ? "Cuotas_Adelantadas" : g.Key == date3 ? "Cuotas_Atrasadas" : g.Key.Value.ToString("yyyyMMdd"),
                DetalleFecha = g.Select(y => new ReportePagosDiaPeriodoAgrupadoDetalleFechaDTO
                {
                    FechaVencimiento = y.FechaVencimiento.ToString("yyyy-MM-dd"),
                    PeriodoFechaVencimiento = y.PeriodoFechaVencimiento,
                    Actual = y.Actual,
                    MontoPagado = y.MontoPagado,
                    MontoPendiente = y.MontoPendiente,
                    ActualConAtrasos = y.ActualConAtrasos,
                    ActualSinAtrasos = y.ActualSinAtrasos,
                    TotalPagadoDentroDelMes = y.TotalPagadoDentroDelMes

                }).ToList()
            });
            return agrupado;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte de Pagos por Periodo
        /// </summary>
        /// <returns> Lista: IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> </returns>
        public IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> GenerarReportePagosPorPeriodo(PagosDiaPeriodoGeneralDTO respuestaGeneral)
        {

            IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> agrupado = null;

            DateTime date2 = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime date3 = new DateTime(2400, 1, 1, 0, 0, 0);

            agrupado = respuestaGeneral.PeriodoMeses.GroupBy(x => x.PeriodoFechaPagoDia)
            .Select(g => new ReportePagosDiaPeriodoAgrupadoDTO
            {
                Periodo = g.Key,
                DetalleFecha = g.Select(y => new ReportePagosDiaPeriodoAgrupadoDetalleFechaDTO
                {
                    PeriodoFechaVencimiento = y.PeriodoFechaVencimiento,
                    FechaVencimiento = y.FechaVencimiento.ToString("yyyy-MM-dd"),
                    Actual = y.Actual,
                    MontoPagado = y.MontoPagado,
                    MontoPendiente = y.MontoPendiente,
                    ActualConAtrasos = y.ActualConAtrasos,
                    ActualSinAtrasos = y.ActualSinAtrasos,
                    TotalPagadoDentroDelMes = y.TotalPagadoDentroDelMes

                }).ToList()
            });
            return agrupado;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener Cronograma Finanzas del Alumno
        /// </summary>
        /// <param name="version"> Version </param>
        /// <param name="idMatriculaCabecera"> Id de la Matricula </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public List<CronogramaPagoDetalleFinalDTO> ObtenerCronogramaFinanzas(int version, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerCronogramaFinanzas(version, idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Juan Diego Huanaco Quispe
        /// Fecha: 22/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtener Cronograma Finanzas del Alumno
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de la Matricula </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public List<CuotaDataAdicionalDTO> ObtenerMorasCalculadas(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerCuotaDataAdicional(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public byte[] GenerarCrep(CrepCabeceraDTO objeto, List<CrepListaCuotasSeleccionadasDTO> lista, List<CrepListaAlumnosDTO> listalumnos)
        {
            /*
           * Cabecera
           1 - 2 2 A Tipo de registro (CC= Cabecera, DD= Detalle)
           3 - 5 3 N Código de la sucursal1 (de la cuenta de la empresa afiliada)
           6 - 6 1 N Código de la moneda1 (de la cuenta de la empresa afiliada), (soles = “0”, dólares = “1”)
           7 - 13 7 N Número de cuenta de la empresa afiliada1
           14 - 14 1 A Tipo de validación (C= Completa)
           15 - 54 40 A Nombre de la empresa afiliada
           55 - 62 8 N Fecha de transmisión (AAAAMMDD)
           63 - 71 9 N Cantidad total de registros enviados en el detalle
           72 - 86 15 N Monto total enviado (2 decimales)(sólo validación completa)*
           87 - 87 1 A Tipo de archivo (R=Archivo de Reemplazo, A=Archivo de Actualización)
           88 - 200 113 N Filler (libre)
            */

            /*Detalle
            1 - 2 2 A Tipo de registro (CC= Cabecera, DD= Detalle)
            3 - 5 3 N Código de la sucursal (de la cuenta de la empresa afiliada)
            6 - 6 1 N Código de la moneda (de la cuenta de la empresa afiliada)
            7 - 13 7 N Número de cuenta de la empresa afiliada
            14 - 27 14 A-N-A/N Código de Identificación del Depositante o Usuario (ver página 5 y 6)
            28 - 67 40 A Nombre del Depositante (para registros tipo “A” o “M”)
            68 - 97 30 A-N-A/N Campo con información de retorno (para registros tipo “A” o “M”)
            98 - 105 8 N Fecha de emisión del cupón (sólo validación completa) (para registros tipo “A” o “M”)
            106 - 113 8 N Fecha de vencimiento del cupón (sólo validación completa) (para registros tipo “A” o “M”)
            114 - 128 15 N Monto del cupón (2 decimales) (sólo validación completa)(para registros tipo “A” o “M”)
            129 - 143 15 N Monto de mora* (2 decimales) (sólo validación completa) (para registros tipo “A” o “M”)
            144 - 152 9 N Monto mínimo1 (2 decimales) (sólo validación completa con rangos)(para registros tipo “A” o “M”)
            153 - 153 1 A Tipo de Registro2 (A=Registro a Agregar, M=Registro a Modificar, E=Registro a Eliminar)
            154 - 200 47 N Filler (libre)
                */

            //generamos la cabecera
            var _repCronogramaPagoDetalleModLogFinalRep = _unitOfWork.CronogramaPagoDetalleModLogFinalRepository;
            StringBuilder linea = new StringBuilder();
            string _nombrearchivo = String.Empty;
            if (objeto.NombreArchivo == "")
            {
                _nombrearchivo = "CREP_X";
            }
            else
            {
                _nombrearchivo = objeto.NombreArchivo;
            }
            string _tiporegistro = "CC";
            string _codigosucursal = "215";
            string _codigomoneda = (objeto.Moneda == "SOLES" ? "0" : "1");
            string _nrocuenta = objeto.hidCuenta; //---------------//
            string _tipovalidacion = "C";
            //string _nombreempresa = "BSGRUPOLIMASOLES".PadRight(40);
            string _nombreempresa = ("BSGRUPO" + objeto.hidCiudad + objeto.Moneda).PadRight(40);
            string _fecha = String.Format("{0:yyyyMMdd}", DateTime.Now);
            string _temp = ObtenerTotales(listalumnos, lista, objeto);
            //colocamos el nombre para guiarnos
            var la = listalumnos;
            var lc = lista;

            string _totalregistros = _temp.Split('&').GetValue(1).ToString().PadLeft(9, '0');
            string _montototal = _temp.Split('&').GetValue(0).ToString().PadLeft(15, '0');

            string _tipoarchivo = "A";
            string _libre = "000".PadLeft(113);
            byte[] registroCrepByte;
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter myStreamWriter = new StreamWriter(ms))
                {
                    linea.Append(_tiporegistro + _codigosucursal + _codigomoneda + _nrocuenta + _tipovalidacion + _nombreempresa + _fecha + _totalregistros + _montototal + _tipoarchivo + _libre);
                    myStreamWriter.WriteLine(linea.ToString());
                    linea.Remove(0, linea.Length);

                    string _codigousuario, _nombreusuario, _fechaemision, _montomora, _montominimo, _tiporegistro2;
                    string _codigoespecial, _montocuota, _fechavencimiento;
                    /////////////////////////////////////////////////////////////

                    //generamos en forma automática o manual
                    if (objeto.ManualAutomatico == "Automatica")
                    {

                        string _cuotao = "";
                        string _fechav = "";
                        string _morao = "0";

                        //generamos el detalle
                        string _tiporegistrod = "DD";
                        string _libred = "".PadLeft(47);
                        for (int x = 0; x < la.Count; x++)
                        {

                            la[x].NombreCompletoAlumno= Regex.Replace(la[x].NombreCompletoAlumno.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                            _codigousuario = la[x].CodigoMatricula.PadLeft(14);
                            _nombreusuario = la[x].NombreCompletoAlumno.PadRight(40);
                            _fechaemision = String.Format("{0:yyyyMMdd}", la[x].FechaMatricula);

                            _montomora = "0".PadLeft(15, '0');
                            _montominimo = "0".PadLeft(9, '0');

                            //_tiporegistro2 = Modalidad.Text; //el valor del combo Modalidad

                            //Obteniendo cuotas originales, o las que se han personalizado
                            //verificar si se ha personalizado (primera fuente), caso contrario, van las originales
                            //ir guardando los datos personalizados en una lista de clase

                            //***** buscar en lc en base al codigo de usuario
                            //filtramos con los datos del usuario actual
                            //List<AlumnoC> results = lc.Exists(
                            bool existeTmp = lc.Exists(
                                delegate (CrepListaCuotasSeleccionadasDTO c)//Alumnoc=CrepListaCuotasSeleccionadasDTO
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );

                            if (existeTmp)
                            {
                                int indexi = lc.FindIndex(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );
                                int indexf = lc.FindLastIndex(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );
                                for (int a = indexi; a <= indexf; a++)
                                {
                                    _codigoespecial = (lc[a].CodigoEspecial.ToString() + lc[a].Adicional).PadRight(30);
                                    if (lc[a].enviado) //se genera primero ELIMINAR
                                    {
                                        //extraemos la cantidad original y la fecha anterior enviada
                                        var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                                        var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == lc[a].CodUsuario, y => new { y.Id }).FirstOrDefault();
                                        var objdatoslog = _repCronogramaPagoDetalleModLogFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.NroCuota == lc[a].nroCuota && w.NroSubCuota == lc[a].nroSubcuota && w.Aprobado == true && w.Ultimo == true).Select(y => new { y.Cuota, y.Mora, y.FechaVencimiento }).FirstOrDefault();//ExtraerDatosLog(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                        _cuotao = objdatoslog.Cuota.ToString();
                                        _fechav = objdatoslog.FechaVencimiento.ToString();
                                        //_morao = objdatoslog.mora.ToString();

                                        _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(_cuotao) + Convert.ToDouble(_morao))).Replace(".", "").PadLeft(15, '0');
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(_fechav));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(_fechav, "d/MM/yyyy HH:mm:ss", null));
                                        _tiporegistro2 = "E";
                                        linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                        myStreamWriter.WriteLine(linea.ToString());
                                        linea.Remove(0, linea.Length);
                                    }
                                    //luego ya se genera la ACTUALIZACION (verificando la fecha de vencimiento, si es antes se envió a Eliminar... 
                                    //--debe ser un día más en caso la fecha sea igual)
                                    _tiporegistro2 = "A";
                                    _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(lc[a].Cuota) + Convert.ToDouble(lc[a].Mora))).Replace(".", "").PadLeft(15, '0');
                                    _fechavencimiento = "";
                                    if (lc[a].fechaVencimiento == lc[a].fechaAnterior)
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaVencimiento).AddDays(1));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaVencimiento, "dd/MM/yyyy", null).AddDays(1));
                                    }
                                    else
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaVencimiento));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaVencimiento, "dd/MM/yyyy", null));
                                    }
                                    linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                    myStreamWriter.WriteLine(linea.ToString());
                                    linea.Remove(0, linea.Length);
                                    //actualizamos ENVIADO y ULTIMO

                                    var _repCronogramaPagoDetalleFinalRep =_unitOfWork.CronogramaPagoDetalleFinalRepository;

                                    var insertado = _repCronogramaPagoDetalleFinalRep.ActualizarEnviado(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                    var actualizado = _repCronogramaPagoDetalleFinalRep.ActualizarUltimo(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                }
                            }
                            else //se extrae de la base
                            {
                                var _repCronogramaPagoDetalleFinalRep =_unitOfWork.CronogramaPagoDetalleFinalRepository;
                                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(w => w.CodigoMatricula == la[x].CodigoMatricula, w => new { w.Id }).FirstOrDefault();
                                var versionAprobada = _repCronogramaPagoDetalleFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Aprobado == true, w => new { w.Version }).OrderByDescending(w => w.Version).FirstOrDefault();


                                var dt = _repCronogramaPagoDetalleFinalRep.ObtenerCuotas(matriculaCabeceraTemp.Id, versionAprobada.Version);//ObtenerCuotas(la[x].CodigoMatricula);
                                for (int i = 0; i < dt.Count; i++)
                                {
                                    _codigoespecial = dt[i].CodigoEspecial.ToString().PadRight(30);
                                    if (Convert.ToBoolean(dt[i].Enviado)) //se genera primero ELIMINAR
                                    {
                                        //extraemos la cantidad original y la fecha anterior enviada
                                        var matriculaCabeceraTemp2 = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == la[x].CodigoMatricula, y => new { y.Id }).FirstOrDefault();

                                        var objdatoslog = _repCronogramaPagoDetalleModLogFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp2.Id && w.NroCuota == dt[i].NroCuota && w.NroSubCuota == dt[i].NroSubCuota && w.Aprobado == true && w.Ultimo == true).Select(y => new { y.Cuota, y.Mora, y.FechaVencimiento }).FirstOrDefault();//ExtraerDatosLog(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString());
                                        _cuotao = objdatoslog.Cuota.ToString();
                                        _fechav = objdatoslog.FechaVencimiento.ToString();
                                        //_morao = objdatoslog.mora.ToString();
                                        _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(_cuotao) + Convert.ToDouble(_morao))).Replace(".", "").PadLeft(15, '0');
                                        // _fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(_fechav));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(_fechav, "d/MM/yyyy HH:mm:ss", null));
                                        _tiporegistro2 = "E";
                                        linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                        myStreamWriter.WriteLine(linea.ToString());
                                        linea.Remove(0, linea.Length);
                                    }
                                    //luego ya se genera la ACTUALIZACION (verificando la fecha de vencimiento, si es antes se envió a Eliminar... 
                                    //--debe ser un día más en caso la fecha sea igual)
                                    _tiporegistro2 = "A";
                                    _montocuota = dt[i].Cuota.ToString().Replace(".", "").PadLeft(15, '0');
                                    _fechavencimiento = "";

                                    if ((dt[i].FechaVencimiento.ToString()) == (dt[i].FechaAnterior.ToString()))
                                    {
                                        // _fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaVencimiento).AddDays(1));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaVencimiento, "dd/MM/yyyy", null).AddDays(1));
                                    }
                                    else
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaVencimiento));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaVencimiento, "dd/MM/yyyy", null));
                                    }

                                    linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                    myStreamWriter.WriteLine(linea.ToString());
                                    linea.Remove(0, linea.Length);
                                    //actualizamos ENVIADO y ULTIMO
                                    //var insertado = _tcronogramapagosdetalle_finalRepository.ActualizarEnviado(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString()).FirstOrDefault();
                                    //var actualizado = _tcronogramapagosdetalle_finalRepository.ActualizarUltimo(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString()).FirstOrDefault();

                                    var insertado = _repCronogramaPagoDetalleFinalRep.ActualizarEnviado(la[x].CodigoMatricula, dt[i].NroCuota, dt[i].NroSubCuota);
                                    var actualizado = _repCronogramaPagoDetalleFinalRep.ActualizarUltimo(la[x].CodigoMatricula, dt[i].NroCuota, dt[i].NroSubCuota);

                                }
                            }
                            //
                        }
                    }
                    else //MANUAL
                    {
                        //generamos el detalle
                        string _tiporegistrod = "DD";
                        string _libred = "".PadLeft(47);
                        for (int x = 0; x < la.Count; x++)
                        {
                            la[x].NombreCompletoAlumno = Regex.Replace(la[x].NombreCompletoAlumno.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                            _codigousuario = la[x].CodigoMatricula.PadLeft(14);
                            _nombreusuario = la[x].NombreCompletoAlumno.PadRight(40);
                            _fechaemision = String.Format("{0:yyyyMMdd}", la[x].FechaMatricula);

                            _montomora = "0".PadLeft(15, '0');
                            _montominimo = "0".PadLeft(9, '0');

                            _tiporegistro2 = objeto.ActualizarEliminar;//A-E//Modalidad.Text; //el valor del combo Modalidad

                            //Obteniendo cuotas originales, o las que se han personalizado
                            //verificar si se ha personalizado (primera fuente), caso contrario, van las originales
                            //ir guardando los datos personalizados en una lista de clase

                            //***** buscar en lc en base al codigo de usuario
                            //filtramos con los datos del usuario actual
                            //List<AlumnoC> results = lc.Exists(
                            bool existeTmp = lc.Exists(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );

                            if (existeTmp)
                            {
                                int indexi = lc.FindIndex(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );
                                int indexf = lc.FindLastIndex(
                                delegate (CrepListaCuotasSeleccionadasDTO c)
                                {
                                    return c.CodUsuario == la[x].CodigoMatricula;
                                }
                                );
                                for (int a = indexi; a <= indexf; a++)
                                {
                                    _codigoespecial = (lc[a].CodigoEspecial.ToString() + lc[a].Adicional).PadRight(30);
                                    _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(lc[a].Cuota) + Convert.ToDouble(lc[a].Mora))).Replace(".", "").PadLeft(15, '0');
                                    _fechavencimiento = "";
                                    if (objeto.ActualizarEliminar == "A")
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaVencimiento));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaVencimiento, "dd/MM/yyyy", null));
                                    }
                                    if (objeto.ActualizarEliminar == "E")
                                    {
                                        lc[a].fechaAnterior = lc[a].fechaAnterior == null ? "" : lc[a].fechaAnterior;
                                        if (lc[a].fechaAnterior.ToString() == "") //de repente es 1er envio
                                        {
                                            //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaVencimiento));
                                            _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaVencimiento, "dd/MM/yyyy", null));
                                        }
                                        else
                                        {
                                            //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(lc[a].fechaAnterior));
                                            _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(lc[a].fechaAnterior, "dd/MM/yyyy", null));
                                        }
                                    }
                                    linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                    myStreamWriter.WriteLine(linea.ToString());
                                    linea.Remove(0, linea.Length);
                                    //actualizamos ENVIADO y ULTIMO
                                    //var insertado = _tcronogramapagosdetalle_finalRepository.ActualizarEnviado(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota).FirstOrDefault();
                                    //var actualizado = _tcronogramapagosdetalle_finalRepository.ActualizarUltimo(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota).FirstOrDefault();
                                    var _repCronogramaPagoDetalleFinalRep = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                                    var insertado = _repCronogramaPagoDetalleFinalRep.ActualizarEnviado(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                    var actualizado = _repCronogramaPagoDetalleFinalRep.ActualizarUltimo(lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                                }
                            }
                            else
                            {
                                var _repCronogramaPagoDetalleFinalRep = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(y => y.CodigoMatricula == la[x].CodigoMatricula, y => new { y.Id }).FirstOrDefault();
                                var versionAprobada = _repCronogramaPagoDetalleFinalRep.GetBy(y => y.IdMatriculaCabecera == matriculaCabeceraTemp.Id && y.Aprobado == true, y => new { y.Version }).OrderByDescending(y => y.Version).FirstOrDefault();


                                var dt = _repCronogramaPagoDetalleFinalRep.ObtenerCuotas(matriculaCabeceraTemp.Id, versionAprobada.Version);//ObtenerCuotas(la[x].CodigoMatricula);
                                for (int i = 0; i < dt.Count; i++)
                                {
                                    _codigoespecial = dt[i].CodigoEspecial.ToString().PadRight(30);
                                    _montocuota = dt[i].Cuota.ToString().Replace(".", "").PadLeft(15, '0');
                                    _fechavencimiento = "";

                                    if (objeto.ActualizarEliminar == "A")
                                    {
                                        //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaVencimiento));
                                        _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaVencimiento, "dd/MM/yyyy", null));
                                    }
                                    if (objeto.ActualizarEliminar == "E")
                                    {
                                        if (dt[i].FechaAnterior.ToString() == "") //de repente es 1er envio
                                        {
                                            //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaVencimiento));
                                            _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaVencimiento, "dd/MM/yyyy", null));
                                        }
                                        else
                                        {
                                            //_fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(dt[i].FechaAnterior));
                                            _fechavencimiento = String.Format("{0:yyyyMMdd}", DateTime.ParseExact(dt[i].FechaAnterior, "dd/MM/yyyy", null));
                                        }
                                    }
                                    linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuenta + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                    myStreamWriter.WriteLine(linea.ToString());
                                    linea.Remove(0, linea.Length);
                                    //actualizamos ENVIADO y ULTIMO
                                    //var insertado = _tcronogramapagosdetalle_finalRepository.ActualizarEnviado(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString()).FirstOrDefault();
                                    //var actualizado = _tcronogramapagosdetalle_finalRepository.ActualizarUltimo(la[x].CodigoMatricula, dt[i].NroCuota.ToString(), dt[i].NroSubCuota.ToString()).FirstOrDefault();

                                    var insertado = _repCronogramaPagoDetalleFinalRep.ActualizarEnviado(la[x].CodigoMatricula, dt[i].NroCuota, dt[i].NroSubCuota);
                                    var actualizado = _repCronogramaPagoDetalleFinalRep.ActualizarUltimo(la[x].CodigoMatricula, dt[i].NroCuota, dt[i].NroSubCuota);
                                }
                            }
                            //
                        }
                    }

                    //escribimos el archivo
                    myStreamWriter.Close();
                }
                registroCrepByte = ms.ToArray();
            }

            return registroCrepByte;
        }


        private string ObtenerTotales(List<CrepListaAlumnosDTO> la, List<CrepListaCuotasSeleccionadasDTO> lc, CrepCabeceraDTO objeto)
        {
            var _repCronogramaPagoDetalleModLogFinalRep = _unitOfWork.CronogramaPagoDetalleModLogFinalRepository;
            var _repCronogramaPagoDetalleFinalRep = _unitOfWork.CronogramaPagoDetalleFinalRepository;

            string _cuotao = "";
            string _fechav = "";
            string _morao = "0";
            //total de registros y de monto
            int _totalregistros = 0;
            double _totalmonto = 0.00;
            for (int i = 0; i < la.Count; i++)
            {
                //interceptamos
                bool existeTmp = lc.Exists(
                    delegate (CrepListaCuotasSeleccionadasDTO c)
                    {
                        return c.CodUsuario == la[i].CodigoMatricula;
                    }
                    );

                if (existeTmp)
                {
                    int indexi = lc.FindIndex(
                    delegate (CrepListaCuotasSeleccionadasDTO c)
                    {
                        return c.CodUsuario == la[i].CodigoMatricula;
                    }
                    );
                    int indexf = lc.FindLastIndex(
                    delegate (CrepListaCuotasSeleccionadasDTO c)
                    {
                        return c.CodUsuario == la[i].CodigoMatricula;
                    }
                    );
                    for (int a = indexi; a <= indexf; a++)
                    {
                        if ((lc[a].enviado) && (objeto.ManualAutomatico == "Automatica")) //cuando es automático
                        {

                            //Obtenemos el IdMatricula
                            var _repMatriculaCabecera =_unitOfWork.MatriculaCabeceraRepository ;
                            var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == lc[a].CodUsuario, x => new { x.Id }).FirstOrDefault();
                            //extraemos la cantidad original y la fecha anterior enviada
                            var objdatoslog = _repCronogramaPagoDetalleModLogFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.NroCuota == lc[a].nroCuota && w.NroSubCuota == lc[a].nroSubcuota && w.Aprobado == true && w.Ultimo == true).Select(x => new { x.Cuota, x.Mora, x.FechaVencimiento }).FirstOrDefault();//lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);
                            _cuotao = objdatoslog.Cuota.ToString();
                            _fechav = objdatoslog.FechaVencimiento.ToString();
                            //_morao = objdatoslog.mora.ToString();
                            _totalmonto += (Convert.ToDouble(_cuotao) + Convert.ToDouble(_morao));
                            _totalregistros += 1;
                        }
                        _totalmonto += (Convert.ToDouble(lc[a].Cuota) + Convert.ToDouble(lc[a].Mora));
                    }
                    _totalregistros += (indexf - indexi + 1);
                }
                else
                {
                    //verificamos las montos para registros que ya han sido enviados
                    var _repMatriculaCabecera =_unitOfWork.MatriculaCabeceraRepository;
                    var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == la[i].CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                    var versionAprobada = _repCronogramaPagoDetalleFinalRep.GetBy(x => x.IdMatriculaCabecera == matriculaCabeceraTemp.Id && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();

                    var dt = _repCronogramaPagoDetalleFinalRep.ObtenerCuotas(matriculaCabeceraTemp.Id, versionAprobada.Version);//ObtenerCuotas(la[i].Matricula);
                    for (int x = 0; x < dt.Count; x++)
                    {
                        if (Convert.ToBoolean(dt[x].Enviado)) //se genera primero ELIMINAR
                        {
                            //extraemos la cantidad original y la fecha anterior enviada
                            var objdatoslog = _repCronogramaPagoDetalleModLogFinalRep.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.NroCuota == dt[x].NroCuota && w.NroSubCuota == dt[x].NroSubCuota && w.Aprobado == true && w.Ultimo == true).Select(w => new { w.Cuota, w.Mora, w.FechaVencimiento }).FirstOrDefault();//ExtraerDatosLog(la[i].Matricula, dt[x].nroCuota.ToString(), dt[x].nroSubCuota.ToString());
                            _cuotao = objdatoslog.Cuota.ToString();
                            _fechav = objdatoslog.FechaVencimiento.ToString();
                            //_morao = objdatoslog.mora.ToString();
                            _totalmonto += (Convert.ToDouble(_cuotao) + Convert.ToDouble(_morao));
                            _totalregistros += 1;
                        }
                        _totalmonto += (Convert.ToDouble(dt[x].Cuota.ToString()));
                    }
                    _totalregistros += dt.Count;
                    /*
                    ip.ObtenerTotales(la[i].Matricula); //se extraen los datos de la base
                    _totalmonto += ip.TotalMonto(); //monto global solo de los regulares
                    _totalregistros += ip.TotalRegistros(); //cantidad total de los regulares
                     */
                }
            }

            return String.Format("{0:0.00}", _totalmonto).Replace(".", "") + "&" + _totalregistros.ToString();
        }


        //Adriana


        public IEnumerable<PagoBancoDTO> ProcesarCDPGFinanzas(IFormFile files)
        {
            List<PagoBancoDTO> listaCuotas = new List<PagoBancoDTO>();
            StreamReader objReader = new StreamReader(files.OpenReadStream());
            string sLine = "";
            ArrayList detalle = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    detalle.Add(sLine);
            }
            objReader.Close();
            int total = detalle.Count;
            for (int x = 1; x < total; x++)
            {
                PagoBancoDTO pb = new PagoBancoDTO();
                pb.Codigousuario = detalle[x].ToString().Substring(13, 14);
                pb.Codigoespecial = detalle[x].ToString().Substring(27, 30);
                pb.Fechapago = detalle[x].ToString().Substring(57, 8);
                pb.Fechavencimiento = detalle[x].ToString().Substring(65, 8);
                pb.Montopago = (detalle[x].ToString().Substring(73, 15).Insert(13, ".")).TrimStart('0');
                var mora = ((detalle[x].ToString().Substring(88, 15).Insert(13, ".")).TrimStart('0'));
                pb.Montomora = mora == ".00" ? "0.00" : mora;
                pb.Montototal = (detalle[x].ToString().Substring(103, 15).Insert(13, ".")).TrimStart('0');

                pb.Moneda = (detalle[x].ToString().Substring(5, 1) == "0" ? "soles" : "dolares");
                pb.Cuenta = detalle[x].ToString().Substring(124, 6);

                //VALIDACIONES///////////////////////////////

                var matriculaCabeceraTemp = _unitOfWork.MatriculaCabeceraRepository.GetBy(y => y.CodigoMatricula == pb.Codigousuario.Trim(), y => new { y.Id }).FirstOrDefault();
                var versionAprobada = _unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(y => y.IdMatriculaCabecera == matriculaCabeceraTemp.Id && y.Aprobado == true, y => new { y.Version }).OrderByDescending(y => y.Version).FirstOrDefault();
                var validarAprobacion = _unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version).Select(q => new { q.Aprobado }).FirstOrDefault();//lc[a].CodUsuario, lc[a].nroCuota, lc[a].nroSubcuota);

                if (validarAprobacion != null)
                {
                    if (validarAprobacion.Aprobado.Value)
                    {
                        CronogramaPagoDetalleFinalService CronogramaPagDetalleFinal = new CronogramaPagoDetalleFinalService(_unitOfWork);
                        if (CronogramaPagDetalleFinal.ValidarCuota(pb.Codigousuario.Trim(), pb.Codigoespecial) == true)
                        {
                            pb.Observaciones = "Nro Cuota Correcta";
                            //validamos el monto de la cuota                                
                            if (!CronogramaPagDetalleFinal.ValidarMonto(pb.Codigousuario.Trim(), pb.Codigoespecial.Trim().Substring(1, 2), pb.Codigoespecial.Trim().Substring(3, 2), Convert.ToDouble(pb.Montopago), pb.Codigoespecial.Trim().Substring(0, 4)))
                            {
                                pb.Observaciones += ", Monto no coincide";
                            }
                        }
                        else
                        {
                            pb.Observaciones = "Nro Cuota no coincide";
                        }
                    }
                    else
                    {
                        pb.Observaciones = "El cronograma tiene cambios pendientes o matricula no existe";
                    }
                }

                listaCuotas.Add(pb);
            }
            var Nregistros = total - 1;
            return listaCuotas; 
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 20/01/2023
        /// Version: 1.0
        /// <summary>
        /// Procesa el pago de cuotas
        /// </summary>
        /// <param name="ListaPagosBanco"> Version </param>
        /// <param name="usuario"> Id de la Matricula </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public IEnumerable<PagoBancoDTO> ProcesarPago(List<PagoBancoDTO> ListaPagosBanco, string usuario)
        {
            string idmat;
            int nrosubcuota = 0, nrocuota = 0;
            DateTime fechapago;
            double montopagado, morabanco;
            string moneda;
            string nroDoc;

            var cp = _unitOfWork.CronogramaPagoRepository;

            List<ImoprtarCrepLogDTO> importar = new List<ImoprtarCrepLogDTO>();
            var error = 1;
            for (int i = 0; i < ListaPagosBanco.Count; i++)
            {
                idmat = ListaPagosBanco[i].Codigousuario.Trim();
                var idmatricula = cp.ObtenerIdporCodigo(idmat);
                var comprobante = cp.ObtenerComprobanteReciente(idmatricula);

                if (ListaPagosBanco[i].Codigoespecial.Trim().Substring(0, 2) == "1C") //código anterior
                {
                    nrocuota = int.Parse(ListaPagosBanco[i].Codigoespecial.Trim().Substring(2, 2));
                    nrosubcuota = 1;
                }
                else
                {
                    nrocuota = int.Parse(ListaPagosBanco[i].Codigoespecial.Trim().Substring(1, 2));
                    nrosubcuota = int.Parse(ListaPagosBanco[i].Codigoespecial.Trim().Substring(3, 2));
                }
                //fechapago = Convert.ToDateTime(Json.listaPagosBanco[i]._fechapago.Substring(6, 2) + "/" + Json.listaPagosBanco[i]._fechapago.Substring(4, 2) + "/" + Json.listaPagosBanco[i]._fechapago.Substring(0, 4));
                fechapago = new DateTime(Convert.ToInt32(ListaPagosBanco[i].Fechapago.Substring(0, 4)), Convert.ToInt32(ListaPagosBanco[i].Fechapago.Substring(4, 2)), Convert.ToInt32(ListaPagosBanco[i].Fechapago.Substring(6, 2)));

                //Buscar el periodo
                int IdPeriodo = 0;

                try
                {
                    IdPeriodo = _unitOfWork.PeriodoRepository.GetBy(y => y.FechaInicialFinanzas.Date <= fechapago.Date && y.FechaFinFinanzas.Date >= fechapago.Date).OrderByDescending(y => y.FechaCreacion).Select(y => y.Id).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    IdPeriodo = 0;
                }
                //End Buscar periodo
                montopagado = Convert.ToDouble(ListaPagosBanco[i].Montototal);
                morabanco = Convert.ToDouble(ListaPagosBanco[i].Montomora);
                moneda = ListaPagosBanco[i].Moneda;
                nroDoc = ListaPagosBanco[i].Cuenta;

                ImoprtarCrepLogDTO ICP = new ImoprtarCrepLogDTO();
                ICP.IdMatricula = idmat;
                ICP.NumeroCuota = nrocuota;
                ICP.NumeroSubCuota = nrosubcuota;
                ICP.IdPeriodo = IdPeriodo;
                ICP.NumeroDocumento = nroDoc;
                ICP.Codigousuario = ListaPagosBanco[i].Codigousuario;
                ICP.Codigoespecial = ListaPagosBanco[i].Codigoespecial;
                ICP.Fechavencimiento = ListaPagosBanco[i].Fechavencimiento;
                ICP.Fechapago = ListaPagosBanco[i].Fechapago;
                ICP.Montopago = ListaPagosBanco[i].Montopago;
                ICP.Montomora = ListaPagosBanco[i].Montomora;
                ICP.Montototal = ListaPagosBanco[i].Montototal;
                ICP.Observaciones = ListaPagosBanco[i].Observaciones;
                ICP.Moneda = ListaPagosBanco[i].Moneda;
                ICP.Cuenta = ListaPagosBanco[i].Cuenta;
                ICP.Excepcion = "";
                var ex = ""; 

                var resultado = _unitOfWork.CronogramaPagoDetalleFinalRepository.
                    PagarCuotaCDPG_CtoFinal(
                    idmat, nrocuota, nrosubcuota, fechapago, 
                    montopagado, morabanco, moneda, nroDoc, 
                    IdPeriodo, usuario, ref ex, comprobante.IdTipoComprobante == -1 ?null: comprobante.IdTipoComprobante,
                    comprobante.NroDocumentoComprobante,comprobante.NombreRazonSocial);
                if (resultado < 0)
                {
                    error = 2;
                }
                ICP.Excepcion = ex;

                importar.Add(ICP);
            }

     

            ActividadCrepLog acl = new ActividadCrepLog();
                acl.TipoOperacion = "Importacion";
                acl.TipoActividad = "Importacion";
                acl.EstadoOperacion = error;//1:ERROR , 2:Processado
                acl.ExcepcionProceso = "" ;
                acl.Crep = JsonConvert.SerializeObject(importar) ;
                acl.FechaCreacion = DateTime.Now;
                acl.FechaModificacion = DateTime.Now;
                acl.UsuarioCreacion = usuario;
                acl.UsuarioModificacion = usuario;
                acl.Estado = true;

            _unitOfWork.ActividadCrepLogRepository.Add(acl);
            _unitOfWork.Commit();

            return ListaPagosBanco;
        }

        public bool ValidarCuota(string codigousuario, string codigoespecial)
        {
            
            var matriculaCabeceraTemp = _unitOfWork.MatriculaCabeceraRepository.GetBy(y => y.CodigoMatricula == codigousuario, y => new { y.Id }).FirstOrDefault();
            var versionAprobada = _unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(y => y.IdMatriculaCabecera == matriculaCabeceraTemp.Id && y.Aprobado == true).OrderByDescending(y => y.Version).FirstOrDefault();
            //validamos que la cuota sea la correcta, es decir, la que continúa en la lista de pendientes de pago
            var lista = new List<int>() { 1, 2, 6, 7 };
            var Cuota = _unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version && (w.Cancelado == false || lista.Contains(w.IdFormaPago.Value))).Select(w => w.NroCuota).Min();
            var subCuota = _unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version && w.NroCuota == Cuota && (w.Cancelado == false || lista.Contains(w.IdFormaPago.Value))).Select(w => w.NroSubCuota).Min();
            string CuotaPad = Cuota.ToString().Length > 1 ? Cuota.ToString().Substring(Cuota.ToString().Length - 2, 2) : Cuota.ToString();
            string SubCuotaPad = subCuota.ToString().Length > 1 ? subCuota.ToString().Substring(subCuota.ToString().Length - 2, 2) : subCuota.ToString();
            string CuotaValidada = CuotaPad.PadLeft(2, '0') + SubCuotaPad.PadLeft(2, '0');
            Int16 cuotasgte = Convert.ToInt16(CuotaValidada);
            string CuotaPagada = String.Empty;
            if (codigoespecial.Substring(0, 2) == "1C") //archivo antiguo
            {
                CuotaPagada = codigoespecial.Substring(2, 2) + "01";
            }
            else
            {
                CuotaPagada = codigoespecial.Substring(1, 4);
            }

            if (Convert.ToInt16(CuotaPagada) == cuotasgte)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidarMonto(string codigousuario, string NroCuota, string NroSubCuota, double CuotaPagada, string ValAnt)
        {
            //validamos que la cuota sea la correcta, es decir, la que continúa en la lista de pendientes de pago
            var matriculaCabeceraTemp = _unitOfWork.MatriculaCabeceraRepository.GetBy(y => y.CodigoMatricula == codigousuario, y => new { y.Id }).FirstOrDefault();
            var versionAprobada = _unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(y => y.IdMatriculaCabecera == matriculaCabeceraTemp.Id && y.Aprobado == true).OrderByDescending(y => y.Version).FirstOrDefault();
            //validamos que la cuota sea la correcta, es decir, la que continúa en la lista de pendientes de pago                     

            double CuotaOriginal = 0.00;
            if (ValAnt.Substring(0, 2) == "1C") //archivo antiguo
            {
                CuotaOriginal = Convert.ToDouble(_unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version && w.NroCuota == int.Parse(ValAnt.Substring(2, 2)) && w.NroSubCuota == 1).Select(x => new { Cuota = (Math.Round(x.Cuota.Value, 2) + Math.Round(x.Mora == null ? 0 : x.Mora.Value, 2)) }).Select(w => w.Cuota).FirstOrDefault());
            }
            else
            {
                CuotaOriginal = Convert.ToDouble(_unitOfWork.CronogramaPagoDetalleFinalRepository.GetBy(w => w.IdMatriculaCabecera == matriculaCabeceraTemp.Id && w.Version == versionAprobada.Version && w.NroCuota == int.Parse(NroCuota) && w.NroSubCuota == int.Parse(NroSubCuota)).Select(x => new { Cuota = (Math.Round(x.Cuota.Value, 2) + Math.Round(x.Mora == null ? 0 : x.Mora.Value, 2)) }).Select(w => w.Cuota).FirstOrDefault());
            }

            if (CuotaPagada == CuotaOriginal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 20/01/2023
        /// Version: 1.0
        /// <summary>
        /// Cambiar Fecha Proceso Pago varios
        /// </summary>
        /// <param name="listaEnteros"> Version </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public bool CambiarFechaProcesos( ListaEnterosDTO listaEnteros)
        {
            try
            {
                var repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var IdCronograma in listaEnteros.ListaEnteros)
                    {
                        var CronogramaPagoDetalleFinalItem = repCronogramaPagoDetalleFinal.FirstById(IdCronograma);

                        if(listaEnteros.Tipo==1) CronogramaPagoDetalleFinalItem.FechaProcesoPagoReal = listaEnteros.FechaDiferida;// fecha pago real
                        else if (listaEnteros.Tipo == 2) CronogramaPagoDetalleFinalItem.FechaIngresoEnCuenta = listaEnteros.FechaDiferida;// fecha ingreso en cuenta
                        else if (listaEnteros.Tipo == 3) CronogramaPagoDetalleFinalItem.FechaEfectivoDisponible = listaEnteros.FechaDiferida;// fecha efectivo disponible

                        CronogramaPagoDetalleFinalItem.UsuarioModificacion = listaEnteros.UsuarioModificacion;
                        CronogramaPagoDetalleFinalItem.FechaModificacion = DateTime.Now;

                        repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinalItem);
                        _unitOfWork.Commit();

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

        /// Autor: Griselberto Huaman
        /// Fecha: 20/01/2023
        /// Version: 1.0
        /// <summary>
        /// Cambiar Fecha Proceso Pago varios
        /// </summary>
        /// <param name="data"> Version </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public bool CambiarFechaProcesoCronograma(FechaCronogramaDTO data)
        {
            try
            {
                var repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;

                using (TransactionScope scope = new TransactionScope())
                {
                    var CronogramaPagoDetalleFinalItem = repCronogramaPagoDetalleFinal.FirstById(data.idCronogramaPagoDetalleFinal);

                    if (data.Tipo == 1) CronogramaPagoDetalleFinalItem.FechaProcesoPagoReal = data.FechaDiferida;// fecha pago real
                    else if (data.Tipo == 2) CronogramaPagoDetalleFinalItem.FechaIngresoEnCuenta = data.FechaDiferida;// fecha ingreso en cuenta
                    else if (data.Tipo == 3) CronogramaPagoDetalleFinalItem.FechaEfectivoDisponible = data.FechaDiferida;// fecha efectivo disponible

                    CronogramaPagoDetalleFinalItem.UsuarioModificacion = data.UsuarioModificacion;
                    CronogramaPagoDetalleFinalItem.FechaModificacion = DateTime.Now;

                    repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinalItem);
                    _unitOfWork.Commit();
                    scope.Complete();
                }

                return true;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


      
        public object ObtenerCronogramaFinal(int idMatriculaCabecera)
        {
           
            try
            {
                var _repCronogramaPagoDetalleFinal = _unitOfWork.CronogramaPagoDetalleFinalRepository;
                var versionAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Aprobado == true, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                var listaCronogramaPagoDetalleFinal = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Version == versionAprobada.Version, x => new { x.Id, x.Cancelado, FlagCancelado = x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version, x.FechaDeposito }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota);
                var versionNoAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Aprobado == false, x => new { x.Version }).OrderByDescending(x => x.Version).FirstOrDefault();
                //var versionNoAprobada = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Aprobado == false, x => new { x.Version });
                if (versionNoAprobada != null)
                {
                    //versionNoAprobada.GroupBy(x => x.Version).Max().FirstOrDefault();
                    var listaCronogramaNoAprobado = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera && x.Version == versionNoAprobada.Version, x => new { x.Id, x.Cancelado, x.NroCuota, x.NroSubCuota, x.TipoCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Mora, x.Saldo, x.Moneda, x.MontoPagado, x.FechaPago, x.IdFormaPago, x.IdCuenta, x.FechaPagoBanco, x.Enviado, x.Observaciones, x.IdDocumentoPago, x.NroDocumento, x.MonedaPago, x.TipoCambio, x.CuotaDolares, x.FechaProcesoPago, x.Version }).OrderBy(x => x.NroCuota).ThenBy(x => x.NroSubCuota);
                    return (new { listaCronogramaPagoDetalleFinal, listaCronogramaNoAprobado });
                }
                return (new { listaCronogramaPagoDetalleFinal });
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        /// Autor:Jorge Gamero
        /// Fecha: 12/08/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene detalle de pagos de cuotas desde ATC por filtro
        /// </summary> 
        /// <returns> IEnumerable<DetalleCuotasTransaccionAuditoriaDTO> </returns>
        public IEnumerable<DetalleCuotasTransaccionAuditoriaDTO> ObtenerDetalleCuotasTransaccionAuditoria(FiltroDetalleCuotasTransaccionAuditoriaDTO FiltroDetalle)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerDetalleCuotasTransaccionAuditoria(FiltroDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Jorge Gamero
        /// Fecha: 12/08/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene detalle de pagos de matrícula desde Comercial por filtro
        /// </summary> 
        /// <returns> IEnumerable<DetalleMatriculaTransaccionAuditoriaDTO> </returns>
        public IEnumerable<DetalleMatriculaTransaccionAuditoriaDTO> ObtenerDetalleMatriculaTransaccionAuditoria(FiltroDetalleMatriculaTransaccionAuditoriaDTO FiltroDetalle)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ObtenerDetalleMatriculaTransaccionAuditoria(FiltroDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Jorge Gamero
        /// Fecha: 25/09/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza tabla fin.T_CronogramaPagoDetalleFinal columna EnviadoSiigo por SP
        /// </summary> 
        /// <returns>  </returns>
        public bool ActualizaEnviadoSiigo(int id)
        {
            try
            {
                return _unitOfWork.CronogramaPagoDetalleFinalRepository.ActualizaEnviadoSiigo(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
