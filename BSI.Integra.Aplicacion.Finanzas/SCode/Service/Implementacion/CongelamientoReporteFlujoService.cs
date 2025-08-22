using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CongelamientoReporteFlujoService
    /// Autor Modificacion: Adriana Chipana.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CongelamientoReporteFlujo
    /// </summary>
    public class CongelamientoReporteFlujoService : ICongelamientoReporteFlujoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CongelamientoReporteFlujoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TCongelamientoReporteFlujo, CongelamientoReporteFlujo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<CongelamientoReporteFlujoRecibidoDTO, CongelamientoReporteFlujo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public bool GenerarCongelamientoReporte(List<FlujoCongelamientoDTO> FlujoCongelamiento)
        {
            try
            {
                var valor = _unitOfWork.CongelamientoReporteFlujoRepository.GenerarCongelamientoReporte(FlujoCongelamiento);

                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecibirDatosReporteFlujoMaestroDTO> ReporteFlujoMaestro(ReporteFlujoMaestroFiltroDTO Parametros)
        {
            try
            {
                var valor = _unitOfWork.CongelamientoReporteFlujoRepository.ReporteFlujoMaestro(Parametros);

                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public CodigoMatriculaV2DTO ObtenerIdMatriculaPorCodigo(string Codigo)
        {
            try
            {
                var valor = _unitOfWork.CongelamientoReporteFlujoRepository.ObtenerIdMatriculaPorCodigo(Codigo);

                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecibirDatosCoordinadores> ObternerTodosCoordinadores()
        {
            try
            {
                var valor = _unitOfWork.CongelamientoReporteFlujoRepository.ObternerTodosCoordinadores();

                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MatriculaInHouseDTO> ObtenerListaInHouse()
        {
            try
            {
                var valor = _unitOfWork.CongelamientoReporteFlujoRepository.ObtenerListaInHouse();

                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertarCambiosPeriodo(List<SubidaExcelDTO> Json, string usuario)
        {
            try
            {
                var dataCadena = "";
                //using (TransactionScope scope = new TransactionScope())
                //{
                var flujoRepositorio = _unitOfWork.CongelamientoReporteFlujoRepository;
                DateTime? Periodo = DateTime.Now;
                DateTime? FechaVencimiento = DateTime.Now;
                DateTime? fechanueva = DateTime.Now;
                var fechaActual = (
                        string.Format("{0000}", fechanueva.Value.Year) + '-' +
                        string.Format("{00}", fechanueva.Value.Day) + '-' +
                        string.Format("{00}", fechanueva.Value.Month)
                        + " 00:00:00.000");
                var FechaPeriodoFlujo = string.Empty;
                var FechaVencimientoFlujo = string.Empty;
                foreach (var flujo in Json)
                {
                    Periodo = flujo.PeriodoCambio;
                    FechaVencimiento = flujo.FechaVencimientoCambio;
                    FechaPeriodoFlujo = (
                        string.Format("{0000}", Periodo.Value.Year) + '-' +
                        string.Format("{00}", (Periodo.Value.Day-1)) + '-' +
                        string.Format("{00}", Periodo.Value.Month)
                        + " 00:00:00.000");
                    FechaVencimientoFlujo = (
                        string.Format("{0000}", FechaVencimiento.Value.Year) + '-' +
                        string.Format("{00}", (FechaVencimiento.Value.Day - 1)) + '-' +
                        string.Format("{00}", FechaVencimiento.Value.Month)
                        + " 00:00:00.000");
                    dataCadena += flujo.CodigoMatricula + "|" +
                     FechaVencimientoFlujo + "|" +
                     flujo.MontoCambio + "|" +
                     flujo.TipoModificacion + "|" +
                     FechaPeriodoFlujo + "|" +
                     usuario + "|" +
                     usuario + "|" +
                     fechaActual + "|" +
                     fechaActual + "!";
                }
                //
                var respuesta = flujoRepositorio.InsertarCambiosPeriodo(dataCadena.Substring(0, dataCadena.Length - 1));
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ActualizarEstadoInHouseMatricula(int IdMatriculaCabecera, int EsInHouse)
        {
            try
            {
                var valor = _unitOfWork.CongelamientoReporteFlujoRepository.ActualizarEstadoInHouseMatricula(IdMatriculaCabecera, EsInHouse);
                _unitOfWork.Commit();
                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarEstadoInHouseCodigoMatricula(string CodigoMatricula, int EsInHouse, string usuario)
          {
            try
            {
                var valor = _unitOfWork.CongelamientoReporteFlujoRepository.ActualizarEstadoInHouseCodigoMatricula(CodigoMatricula, EsInHouse, usuario);
                _unitOfWork.Commit();
                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CongeladosDTO> ExportarCongelados(FechaInicioFinDTO fechas)
        {
            try
            {
                var valor = _unitOfWork.CongelamientoReporteFlujoRepository.ExportarCongelados(fechas);
                _unitOfWork.Commit();
                return valor;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public bool EditarReporteFlujoMaestro(EditarReporteFlujoMaestroFiltroDTO Parametros, string usuario)
        {
            try
            {

                var datosReporte = _unitOfWork.ReporteFlujoCongeladoPorDiumRepository.GetBy(x => x.Id == Parametros.Id).FirstOrDefault();

                var datos = new ReporteFlujoCongeladoPorDium();

                datos.Id = Parametros.Id;
                datos.CodigoMatricula = Parametros.CodigoMatricula;
                datos.EstadoMatricula = Parametros.EstadoMatricula;
                datos.IdMatriculaCabecera = datosReporte.IdMatriculaCabecera;
                datos.IdPespecifico = datosReporte.IdPespecifico;
                datos.NombrePrograma = datosReporte.NombrePrograma;
                datos.NombreAlumno = datosReporte.NombreAlumno;
                datos.FechaVencimiento = (DateTime)Parametros.FechaVencimiento;
                datos.FechaCongelamiento = (DateTime)Parametros.FechaCongelamiento;
                datos.MontoCuota = Parametros.MontoCuota;
                datos.TotalPagado = datosReporte.TotalPagado;
                datos.FechaPago = datosReporte.FechaPago;
                datos.FechaProcesoPago = datosReporte.FechaProcesoPago;
                datos.SaldoPendiente = datosReporte.SaldoPendiente;
                datos.SaldoPendienteDolar = datosReporte.SaldoPendienteDolar;
                datos.TotalCuotaDolar = datosReporte.TotalCuotaDolar;
                datos.RealPagoDolar = datosReporte.RealPagoDolar;
                datos.NroDocumento = datosReporte.NroDocumento;
                datos.MonedaPago = datosReporte.MonedaPago;
                datos.TipoCambio = datosReporte.TipoCambio;
                datos.Mora = datosReporte.Mora;
                datos.Version = datosReporte.Version;
                datos.Cancelado = datosReporte.Cancelado;
                datos.Dni = datosReporte.Dni;
                datos.EstadoMatricula = Parametros.EstadoMatricula;
                datos.CoordinadorAcademico = Parametros.CoordinadorAcademico;
                datos.CoordinadorCobranza = Parametros.CoordinadorCobranza;
                datos.NroCuota = Parametros.NroCuota;
                datos.NroSubCuota = Parametros.NroSubCuota;
                datos.Estado = true;
                datos.UsuarioCreacion = datosReporte.UsuarioCreacion;
                datos.UsuarioModificacion = usuario;
                datos.FechaCreacion = DateTime.Now;
                datos.FechaModificacion = DateTime.Now;
                datos.Email = datosReporte.Email;

                var valor = _unitOfWork.ReporteFlujoCongeladoPorDiumRepository.Update(datos);

                _unitOfWork.Commit();


                var datosCronograma = new ActualizarCronogramaCongeladoOriginalesDTO();

                datosCronograma.CoordinadoraAcademica = datosReporte.CoordinadorAcademico;
                datosCronograma.FechaModificacion = datosReporte.FechaModificacion;
                datosCronograma.CodigoMatricula = datosReporte.CodigoMatricula;
                datosCronograma.FechaCongelamiento = datosReporte.FechaCongelamiento;
                datosCronograma.NroCuota = datosReporte.NroCuota;
                datosCronograma.NroSubCuota = datosReporte.NroSubCuota;

                var valorCronograma = _unitOfWork.CronogramaOriginalesCongeladoPorDiumRepository.ActualizarCronogramaCongeladosOriginales(datosCronograma, usuario);

                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var respuesta = _unitOfWork.CronogramaOriginalesCongeladoPorDiumRepository.Delete(id, usuario);

                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
