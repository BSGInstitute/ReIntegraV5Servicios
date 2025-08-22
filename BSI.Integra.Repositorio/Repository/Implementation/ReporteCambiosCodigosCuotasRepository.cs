using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReporteCambiosCodigosCuotasRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteCambiosCodigosCuotas
    /// </summary>
    public class ReporteCambiosCodigosCuotasRepository : IReporteCambiosCodigosCuotasRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteCambiosCodigosCuotasRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// <summary>
        /// Obtiene el reporte de cambios
        /// </summary>
        /// <returns></returns>
        public List<ReporteCambiosDTO> ObtenerReporteCambios(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                List<ReporteCambiosDTO> items = new List<ReporteCambiosDTO>();
                var _query = string.Empty;
                _query = @"
                Select 
                    IdCronogramaMod, IdAlumno,Alumno, 
                    Modalidad, FechaCambio, Ciudad, 
                    Programa, IdCentroCosto, CodigoAlumno, 
                    IdMatricula, Observaciones, RealizadoPor, 
                    MensajeSistema, SolicitadoPor, AprobadoPor, 
                    Observaciones2 
                from fin.V_ReporteCambios 
                where 
                    CAST(FechaCambio AS date)>= CAST(@FechaIni AS DATE) and CAST(FechaCambio AS date)<= CAST(@FechaFin AS DATE) 
                    and (@IdCentroCosto is null or IdCentroCosto=@IdCentroCosto)
                    and (@IdAlumno is null or IdAlumno=@IdAlumno) 
                    and (@IdMatricula is null or IdMatricula=@IdMatricula) 
                order by FechaCambio desc";

                var respuestaDapper = _dapperRepository.QueryDapper(_query, new
                { 
                    FechaIni = FiltroCambios.FechaInicio, 
                    FechaFin = FiltroCambios.FechaFin, 
                    FiltroCambios.IdCentroCosto, 
                    FiltroCambios.IdAlumno, 
                    FiltroCambios.IdMatricula 
                });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// <summary>
        /// Obtiene el reporte de codigos
        /// </summary>
        /// <returns></returns>
        public List<ReporteCodigosDTO> ObtenerReporteCodigos(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {

                List<ReporteCodigosDTO> items = new List<ReporteCodigosDTO>();
                var _query = string.Empty;
                _query = @"
                Select 
                    IdAlumno, Modalidad, Ciudad, 
                    Programa, IdCentroCosto, Codigo, 
                    IdMatricula, Alumno, FechaCreacion 
                from fin.V_ReporteCodigoAlumnos
                where 
                    CAST(FechaCreacion as date)>= CAST(@FechaIni as date) and CAST(FechaCreacion as date)<= CAST(@FechaFin as DATE)
                    and(@IdCentroCosto is null or IdCentroCosto = @IdCentroCosto) 
                    and(@IdAlumno is null or IdAlumno = @IdAlumno) 
                    and(@IdMatricula is null or IdMatricula = @IdMatricula) 
                order by FechaCreacion desc";

                var respuestaDapper = _dapperRepository.QueryDapper(_query, new 
                { 
                    FechaIni = FiltroCambios.FechaInicio, 
                    FechaFin = FiltroCambios.FechaFin, 
                    FiltroCambios.IdCentroCosto, 
                    FiltroCambios.IdAlumno, 
                    FiltroCambios.IdMatricula 
                });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCodigosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// <summary>
        /// Obtiene el reporte de cuotas
        /// </summary>
        /// <returns></returns>
        public List<ReporteCuotasDTO> ObtenerReporteCuotas(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {

                List<ReporteCuotasDTO> items = new List<ReporteCuotasDTO>();
                var _query = string.Empty;
                _query = @"
                Select 
                    IdAlumno, IdMatricula, CodigoMatricula, 
                    Modalidad, Ciudad, CentroCosto, IdCentroCosto, 
                    Alumno, FechaCuota, Cuota, SaldoPendiente, 
                    Cuota_SubCuota,MonedaPago, EstadoCuota 
                from fin.V_ReporteCuotaAlumno 
                where 
                    CAST(FechaCuota as date)>=CAST(@FechaIni as date) and CAST(FechaCuota as date)<=CAST(@FechaFin as DATE) 
                    and(@IdCentroCosto is null or IdCentroCosto = @IdCentroCosto) 
                    and(@IdAlumno is null or IdAlumno = @IdAlumno) 
                    and(@IdMatricula is null or IdMatricula = @IdMatricula)";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new
                {
                    FechaIni = FiltroCambios.FechaInicio,
                    FechaFin = FiltroCambios.FechaFin,
                    FiltroCambios.IdCentroCosto,
                    FiltroCambios.IdAlumno,
                    FiltroCambios.IdMatricula
                });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCuotasDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// <summary>
        /// Obtiene el reporte de traslaciones
        /// </summary>
        /// <returns></returns>
        public List<ReporteCambioProgramaDTO> ObtenerReporteTraslados(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {

                List<ReporteCambioProgramaDTO> items = new List<ReporteCambioProgramaDTO>();
                var _query = string.Empty;
                _query = @"
                Select 
                    Fecha, IdAlumno, Alumno, IdMatriculaCabecera, 
                    CodigoMatricula, CentroCostoAnterior, CentroCostoNuevo 
                from [fin].[V_ReporteCambioProgramaV5]
                where 
                    cast(fecha as DATE) >= cast(@FechaIni as DATE) and cast(fecha as DATE) <= cast(@FechaFin  as DATE)  
                    and IdCentroAnterior <> IdCentroNuevo 
                    and(@IdCentroCosto is null or IdCentroNuevo = @IdCentroCosto or  IdCentroAnterior = @IdCentroCosto) 
                    and(@IdAlumno is null or IdAlumno = @IdAlumno) 
                    and(@IdMatricula is null or IdMatriculaCabecera = @IdMatricula)
                order by Fecha desc";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new
                {
                    FechaIni = FiltroCambios.FechaInicio,
                    FechaFin = FiltroCambios.FechaFin,
                    FiltroCambios.IdCentroCosto,
                    FiltroCambios.IdAlumno,
                    FiltroCambios.IdMatricula
                });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambioProgramaDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        ///Autor: Griselberto Huamanc
        /// Fecha: 28/06/2022
        /// <summary>
        /// Congela los datos de la tabla T_CronogramaPagoDetalleModLogFinal en base a una fecha fecha 
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDeCambios(DateTime FechaCongelamiento, string Usuario)
        {
            try
            {
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("fin.SP_DividirMensajeSistema", new { Usuario, FechaCongelamiento });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
