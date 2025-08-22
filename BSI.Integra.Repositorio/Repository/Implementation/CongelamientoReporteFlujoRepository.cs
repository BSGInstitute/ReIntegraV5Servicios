using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CongelamientoReporteFlujoRepository
    /// Autor: Adriana Chipana
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CongelamientoReporteFlujo
    /// </summary>
    public class CongelamientoReporteFlujoRepository : ICongelamientoReporteFlujoRepository
    {
        private IDapperRepository _dapperRepository;
        public CongelamientoReporteFlujoRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public bool GenerarCongelamientoReporte(List<FlujoCongelamientoDTO> FlujoCongelamiento)
        {
            try
            {
                bool items = false;
                foreach (var element in FlujoCongelamiento)
                {
                    var query = _dapperRepository.QuerySPDapper("[fin].[SP_GenerarCongelamientoReporteFlujo]", new
                    {
                        fechaCongelamiento = element.fechaCongelamiento,
                        idMatriculaCabecera = element.idMatriculaCabecera,
                        idCoordAcademico = element.idCoordAcademico,
                        coordinadorAcademico = element.coordinadorAcademico,
                        idPespecifico = element.idPespecifico,
                        programa = element.programa,
                        codigoMatricula = element.codigoMatricula,
                        alumno = element.alumno,
                        fechaCuota = element.fechaCuota,
                        montoCuota = element.montoCuota,
                        fechaPago = element.fechaPago,
                        pago = element.pago,
                        saldoPendiente = element.saldoPendiente,
                        mora = element.mora,
                        nroCuota = element.nroCuota,
                        nroSubCuota = element.nroSubCuota,
                        moneda = element.moneda,
                        totalUSD = element.totalUSD,
                        realUSD = element.realUSD,
                        penUSD = element.penUSD,
                        Estado = element.Estado,
                        fechaCreacion = DateTime.Now,
                        fechaModificacion = DateTime.Now,
                        UsuarioCreacion = element.UsuarioCreacion,
                        UsuarioModificacion = element.UsuarioModificacion,
                    });
                    if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                    {
                        items = true;
                    }
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<RecibirDatosReporteFlujoMaestroDTO> ReporteFlujoMaestro(ReporteFlujoMaestroFiltroDTO Parametros)
        {
            try
            {
                List<RecibirDatosReporteFlujoMaestroDTO> items = new List<RecibirDatosReporteFlujoMaestroDTO>();

                if (Parametros.FechaCongelamiento != null)
                {
                    var _query = string.Empty;

                    DateTime? FechaIni = Parametros.FechaInicio;
                    DateTime? FechaFin = Parametros.FechaFin;
                    DateTime? FechaCon = Parametros.FechaCongelamiento;

                    FechaCon = new DateTime(Parametros.FechaCongelamiento.Value.Year, Parametros.FechaCongelamiento.Value.Month, Parametros.FechaCongelamiento.Value.Day, 0, 0, 0);

                    _query = @" select Id,CodigoMatricula,NroCuota,NroSubCuota,FechaVencimiento,MontoCuota,EstadoMatricula,CoordinadorAcademico,CoordinadorCobranza, FechaCongelamiento
                                from fin.V_TReporteFlujoCongeladoPorDia_ListaFlujo
                                where Estado = 1 and FechaCongelamiento = @FC  ";

                    if (Parametros.FechaInicio != null && Parametros.FechaFin != null)
                    {
                        FechaIni = new DateTime(Parametros.FechaInicio.Value.Year, Parametros.FechaInicio.Value.Month, Parametros.FechaInicio.Value.Day, 0, 0, 0);
                        FechaFin = new DateTime(Parametros.FechaFin.Value.Year, Parametros.FechaFin.Value.Month, Parametros.FechaFin.Value.Day, 23, 59, 59);

                        _query = _query + " and FechaVencimiento >=@FI  AND  FechaVencimiento <= @FF and FechaCongelamiento = @FC ";
                    }

                    if (!string.IsNullOrEmpty(Parametros.EstadoMatricula))
                    {
                        _query = _query + " and EstadoMatricula = @EM ";
                    }

                    if (!string.IsNullOrEmpty(Parametros.CodigoMatricula))
                    {
                        _query = _query + " and CodigoMatricula = @CM ";
                    }


                    var respuestaDapper = _dapperRepository.QueryDapper(_query,
                        new
                        {
                            FI = FechaIni,
                            FF = FechaFin,
                            FC = FechaCon,
                            EM = Parametros.EstadoMatricula,
                            CM = Parametros.CodigoMatricula
                        });

                    if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    {
                        items = JsonConvert.DeserializeObject<List<RecibirDatosReporteFlujoMaestroDTO>>(respuestaDapper);
                    }

                }

                return items;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CodigoMatriculaV2DTO ObtenerIdMatriculaPorCodigo(string Codigo)
        {
            try
            {
                List<CodigoMatriculaV2DTO> codigosMatriculaPEspecifico = new List<CodigoMatriculaV2DTO>();
                var _query = "SELECT top 1 Id,CodigoMatricula, EstadoMatricula FROM fin.T_MatriculaCabecera WHERE  CodigoMatricula=@Codigo ";
                var codigosMatriculaDB = _dapperRepository.QueryDapper(_query, new { Codigo });
                if (!string.IsNullOrEmpty(codigosMatriculaDB) && !codigosMatriculaDB.Contains("[]"))
                {
                    codigosMatriculaPEspecifico = JsonConvert.DeserializeObject<List<CodigoMatriculaV2DTO>>(codigosMatriculaDB);
                }
                else
                {
                    return null;
                }
                return codigosMatriculaPEspecifico[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RecibirDatosCoordinadores> ObternerTodosCoordinadores()
        {
            try
            {
                List<RecibirDatosCoordinadores> items = new List<RecibirDatosCoordinadores>();
                var _query = "select * from fin.V_ObtenerUsuarioAsesor order by Coordinadores Asc ";

                var respuestaDapper = _dapperRepository.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<RecibirDatosCoordinadores>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MatriculaInHouseDTO> ObtenerListaInHouse()
        {
            try
            {
                List<MatriculaInHouseDTO> InHouseMatriculados = new List<MatriculaInHouseDTO>();
                var _query = "select CodigoMatricula, Cuota, Monto, FechaMatricula,FechaVencimiento from fin.V_ListaAlumnosInHouse ";
                var inHouseDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(inHouseDB) && !inHouseDB.Contains("[]"))
                {
                    InHouseMatriculados = JsonConvert.DeserializeObject<List<MatriculaInHouseDTO>>(inHouseDB);
                }
                return InHouseMatriculados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertarCambiosPeriodo(string data)
        {
            try
            {
                bool items = false;

                var prueba = data.Replace(",", ".");
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_InsertarCambiosReporte]", new
                {
                    Texto = prueba
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = true;
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ActualizarEstadoInHouseMatricula(int IdMatriculaCabecera, int EsInHouse)
        {
            try
            {

                var _query = "fin.SP_ActualizarEstadoInHouseMatricula";
                var inHouseDB = _dapperRepository.QuerySPDapper(_query, new { IdMatriculaCabecera, EsInHouse });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ActualizarEstadoInHouseCodigoMatricula(string CodigoMatricula, int EsInHouse, string usuario)
        {
            try
            {

                var FechaModificacion = DateTime.Now;
                var UsuarioModificacion = usuario;
                var _query = "[fin].[SP_ActualizarEstadoInHouseCodigoMatricula]";
                var inHouseDB = _dapperRepository.QuerySPDapper(_query, new { CodigoMatricula, EsInHouse, FechaModificacion, UsuarioModificacion });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<CongeladosDTO> ExportarCongelados(FechaInicioFinDTO fechas)
        {
            try
            {
                DateTime fechainicio = new DateTime(fechas.FechaInicio.Year, fechas.FechaInicio.Month, fechas.FechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(fechas.FechaFin.Year, fechas.FechaFin.Month, fechas.FechaFin.Day, 23, 59, 59);
                var tipos = "0,1,2"; 
                var _query = "[fin].[SP_CronogramasOriginales_Bryan]";
                string coordinadoras = null;
                var respuesta = _dapperRepository.QuerySPDapper(_query, new { fechainicio, fechafin, tipos, coordinadoras });

                List<CongeladosDTO> items = new List<CongeladosDTO>();
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<CongeladosDTO>>(respuesta);
                }

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
