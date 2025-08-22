using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReporteIndicadoresProductividadRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteIndicadoresProductividad
    /// </summary>
    public class ReporteIndicadoresProductividadRepository : IReporteIndicadoresProductividadRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteIndicadoresProductividadRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }


        public List<ReporteProductividadVentasHorasTrabajadasDTO> ObtenerReporteProductividadVentasHorasTrabajadas(FiltroFechaDTO filtro)
        {
            try
            {
                List<ReporteProductividadVentasHorasTrabajadasDTO> items = new List<ReporteProductividadVentasHorasTrabajadasDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteProductividadVentas_HorasTrabajadas]", new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteProductividadVentasHorasTrabajadasDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReporteProductividadVentasIndicadoresDTO> ObtenerReporteProductividadVentasIndicadores(FiltroFechaDTO filtro)
        {
            try
            {
                List<ReporteProductividadVentasIndicadoresDTO> items = new List<ReporteProductividadVentasIndicadoresDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteProductividadVentas_Indicadores]", new {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteProductividadVentasIndicadoresDTO>>(query);
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
