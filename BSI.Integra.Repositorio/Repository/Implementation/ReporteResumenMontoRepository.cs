using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReporteResumenMontoRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteResumenMonto
    /// </summary>
    public class ReporteResumenMontoRepository :  IReporteResumenMontoRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteResumenMontoRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ResumenMontos_Cierre.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosCierreDTO> ObtenerReporteResumenMontosCierre(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                List<ReporteResumenMontosCierreDTO> items = new List<ReporteResumenMontosCierreDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteResumenMontos_CierreV5]", new {
                    IdPeriodo = FiltroPendiente.PeriodoCierre,
                    FechaInicio = FiltroPendiente.FechaInicio,
                    FechaFin = FiltroPendiente.FechaFin,
                    IdPais = FiltroPendiente.IdPais,
                    IsOtrosPais = FiltroPendiente.IsOtrosPais,
                    IsTodo = FiltroPendiente.IsTodo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosCierreDTO>>(query);
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
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ResumenMontos_Cierre.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosCambiosDTO> ObtenerReporteResumenMontosCambios(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                List<ReporteResumenMontosCambiosDTO> items = new List<ReporteResumenMontosCambiosDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteResumenMontosCambiosv5]", new
                {
                    IdPeriodo = FiltroPendiente.PeriodoActual,
                    IdPais = FiltroPendiente.IdPais,
                    IsOtrosPais = FiltroPendiente.IsOtrosPais
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosCambiosDTO>>(query);
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
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ResumenMontos_Cierre.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosDiferenciasDTO> ObtenerReporteResumenMontosDiferencias(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                List<ReporteResumenMontosDiferenciasDTO> items = new List<ReporteResumenMontosDiferenciasDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteResumenMontosDiferenciasV5]", new
                {
                    FechaFin = FiltroPendiente.FechaFin,
                    IdPeriodo = FiltroPendiente.PeriodoActual,
                    IdPais = FiltroPendiente.IdPais,
                    IsOtrosPais = FiltroPendiente.IsOtrosPais
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosDiferenciasDTO>>(query);
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
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ResumenMontos_NuevosMatriculados.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosNuevosMatriculadosDTO> ObtenerReporteResumenMontosNuevosMatriculados(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                List<ReporteResumenMontosNuevosMatriculadosDTO> items = new List<ReporteResumenMontosNuevosMatriculadosDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteResumenMontos_NuevosMatriculadosV5]", new
                {
                    FechaFin = FiltroPendiente.FechaFin,
                    FechaInicio = FiltroPendiente.FechaInicio,
                    IdPeriodo = FiltroPendiente.PeriodoActual,
                    IdPais = FiltroPendiente.IdPais,
                    IsOtrosPais = FiltroPendiente.IsOtrosPais,
                    IsTodo = FiltroPendiente.IsTodo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosNuevosMatriculadosDTO>>(query);
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
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte ResumenMontos_Cierre.
        /// </summary>
        /// <returns> List<ReporteResumenMontoDTO> </returns>
        public List<ReporteResumenMontosDTO> ObtenerReporteResumenMontos(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                List<ReporteResumenMontosDTO> items = new List<ReporteResumenMontosDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteResumenMontosV5]", new
                {
                    FechaInicio = FiltroPendiente.FechaInicio,
                    FechaFin = FiltroPendiente.FechaFin,
                    IdPeriodo = FiltroPendiente.PeriodoActual,
                    IdPais = FiltroPendiente.IdPais,
                    IsOtrosPais = FiltroPendiente.IsOtrosPais,
                    IsTodo = FiltroPendiente.IsTodo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }
}
