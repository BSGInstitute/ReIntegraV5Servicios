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
    /// Repositorio: ReportePendienteV2Repository
    /// Autor: Adriana Chipana
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReportePendienteV2
    /// </summary>
    public class ReportePendienteV2Repository : IReportePendienteV2Repository
    {
        private IDapperRepository _dapperRepository;
        public ReportePendienteV2Repository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }



        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 0, 0, 0);

                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePendientesPeriodo_Cierre_V5]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 0, 0, 0);

                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinador_Periodo_Cierre_Matriculados]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public async Task<bool>ObtenerReportePendienteCierrePorPeriodo(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente)
        {
            try
            {

                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierrePrevio = new DateTime(filtroPendiente.FechaCortePrevio.Year, filtroPendiente.FechaCortePrevio.Month, filtroPendiente.FechaCortePrevio.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 23, 59, 59);
                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = await _dapperRepository.QuerySPDapperAsync("[fin].[SP_ReportePendientesPeriodo_Cierre_Comparar_V5]", new { fechainicio, fechafin, fechaCierre, fechaCierrePrevio, Identificador =  filtroPendiente.Identificador.ToString()});
                //var query2 = _dapperRepository.QuerySPDapper("[fin].[SP_ReportePendientesPeriodo_Cierre_Comparar]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre, fechaCierrePrevio });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return true;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendienteCierrePorPeriodoPrueba(StringDTO valor)
        {
            try
            {

                var dato = valor.Valor;
                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var _query = "select * from fin.V_ObtenerReportePendientePeriodoAuxiliar where IdentificadorGuid = @dato ";

                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { dato = dato });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(respuestaDapper);
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
