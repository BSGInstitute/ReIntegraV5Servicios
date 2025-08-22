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
    /// Repositorio: ReporteDocumentosRepository
    /// Autor: Adriana Chipana
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteDocumentos
    /// </summary>
    public class ReporteDocumentosRepository : IReporteDocumentosRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteDocumentosRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }



        public List<ReporteDocumentosDTO> ObtenerReporteDocumentos(ReporteDocumentosFiltroDTO FiltroControlDocumentos)
        {
            try
            {
                DateTime _FechaIni = FiltroControlDocumentos.FechaInicio;
                DateTime _FechaFin = FiltroControlDocumentos.FechaFin;
                string _Asesor = FiltroControlDocumentos.Asesor;
                string _Coordinador = FiltroControlDocumentos.Coordinador;
                _FechaIni = new DateTime(FiltroControlDocumentos.FechaInicio.Year, FiltroControlDocumentos.FechaInicio.Month, FiltroControlDocumentos.FechaInicio.Day, 0, 0, 0);
                _FechaFin = new DateTime(FiltroControlDocumentos.FechaFin.Year, FiltroControlDocumentos.FechaFin.Month, FiltroControlDocumentos.FechaFin.Day, 23, 59, 59);


                List<ReporteDocumentosDTO> items = new List<ReporteDocumentosDTO>();
                var query = "fin.SP_ReporteDocumentosV5";
                var respuestaBD = _dapperRepository.QuerySPDapper(query, new { FechaInicio = _FechaIni, FechaFin = _FechaFin, IdAsesor = _Asesor, IdCoordinador = _Coordinador});

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteDocumentosDTO>>(respuestaBD);
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
