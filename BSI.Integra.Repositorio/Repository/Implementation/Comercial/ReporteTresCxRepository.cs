using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Comercial
{
    public class ReporteTresCxRepository : IReporteTresCxRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteTresCxRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        ///Repositorio: ReportesRepositorio
        ///Autor: Flavio R.
        ///Fecha: 08/11/2023
        /// <summary>
        /// Obtiene las actividades realizadas por un asesor en un determinado dia
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda por Id</param>
        /// <param name="fechaInicio"> Filtro de Fecha de Inicio</param>
        /// <param name="fechaFin"> Filtro de Fecha de Fin </param>
        /// <returns> Lista de ReporteRealizadaDataTresCxDTO </returns>
        public List<ReporteRealizadaDataTresCxAlternoDTO> ObtenerReporteActividadesRealizadasTresCx(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin, bool esActual)
        {
            try
            {
                List<ReporteRealizadaDataTresCxAlternoDTO> items = new List<ReporteRealizadaDataTresCxAlternoDTO>();
                string? fasesOrigen = null;
                string? fasesDestino = null;

                if (filtro.IdFasesOportunidadOrigen != null && filtro.IdFasesOportunidadOrigen.Count() > 0)
                {
                    fasesOrigen = string.Join(",", filtro.IdFasesOportunidadOrigen);
                }
                if (filtro.IdFasesOportunidadDestino != null && filtro.IdFasesOportunidadDestino.Count() > 0)
                {
                    fasesDestino = string.Join(",", filtro.IdFasesOportunidadDestino);
                }
                //var sp = esActual ? "com.SP_ReporteActividadesRealizadasNWNuevoModeloTresCxAlterno" : "com.SP_ReporteActividadesRealizadasNWNuevoModeloCongeladoTresCxAlterno";
                //var sp = esActual ? "com.SP_ReporteActividadesRealizadasNWNuevoModeloTresCx" : "com.SP_ReporteActividadesRealizadasNWNuevoModeloCongeladoTresCx";
                //var sp = esActual ? "com.SP_ReporteActividadesRealizadasNWNuevoModeloTresCxV2" : "com.SP_ReporteActividadesRealizadasNWNuevoModeloCongeladoTresCxV2";
                var sp = esActual ? "com.SP_ReporteActividadesRealizadasNWNuevoModeloTresCxV3" : "com.SP_ReporteActividadesRealizadasNWNuevoModeloCongeladoTresCxV3";
                var resultado = _dapperRepository.QuerySPDapper(sp, new
                {
                    filtro.IdAsesor,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    filtro.IdCentroCosto,
                    filtro.IdAlumno,
                    filtro.IdTipoDato,
                    IdCategoriaOrigen = filtro.IdTipoCategoriaOrigen,
                    filtro.IdProbabilidadActual,
                    filtro.IdEstadoOcurrencia,
                    FasesOrigen = fasesOrigen,
                    FasesDestino = fasesDestino
                });

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    items = JsonConvert.DeserializeObject<List<ReporteRealizadaDataTresCxAlternoDTO>>(resultado)!;
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        ///Autor: Flavio R.
        ///Fecha: 27/05/2024
        /// <summary>
        /// Obtiene las actividades realizadas por un asesor en un determinado dia
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda por Id</param>
        /// <param name="fechaInicio"> Filtro de Fecha de Inicio</param>
        /// <param name="fechaFin"> Filtro de Fecha de Fin </param>
        /// <param name="esActual"> Flag validacion fecha actual </param>
        /// <returns> Lista de ReporteActividadRealizadaDTO</returns>
        public List<ReporteActividadRealizadaDTO> ObtenerReporteActividadesRealizadas(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin, bool esActual)
        {
            try
            {
                List<ReporteActividadRealizadaDTO> items = new List<ReporteActividadRealizadaDTO>();
                string? fasesOrigen = null;
                string? fasesDestino = null;

                if (filtro.IdFasesOportunidadOrigen != null && filtro.IdFasesOportunidadOrigen.Count() > 0)
                    fasesOrigen = string.Join(",", filtro.IdFasesOportunidadOrigen);
                if (filtro.IdFasesOportunidadDestino != null && filtro.IdFasesOportunidadDestino.Count() > 0)
                    fasesDestino = string.Join(",", filtro.IdFasesOportunidadDestino);
                var query = esActual ? "com.SP_ReporteActividadesRealizadasV5" : "com.SP_ReporteActividadesRealizadasCongeladoV5";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    filtro.IdAsesor,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    filtro.IdCentroCosto,
                    filtro.IdAlumno,
                    filtro.IdTipoDato,
                    IdCategoriaOrigen = filtro.IdTipoCategoriaOrigen,
                    filtro.IdProbabilidadActual,
                    filtro.IdEstadoOcurrencia,
                    FasesOrigen = fasesOrigen,
                    FasesDestino = fasesDestino
                });

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    items = JsonConvert.DeserializeObject<List<ReporteActividadRealizadaDTO>>(resultado)!;
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
