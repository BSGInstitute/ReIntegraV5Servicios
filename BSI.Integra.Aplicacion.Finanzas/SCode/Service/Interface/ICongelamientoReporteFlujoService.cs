using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICongelamientoReporteFlujoService
    {
        public bool GenerarCongelamientoReporte(List<FlujoCongelamientoDTO> FlujoCongelamiento);
        public List<RecibirDatosReporteFlujoMaestroDTO> ReporteFlujoMaestro(ReporteFlujoMaestroFiltroDTO Parametros);
        public CodigoMatriculaV2DTO ObtenerIdMatriculaPorCodigo(string Codigo);
        public List<RecibirDatosCoordinadores> ObternerTodosCoordinadores();
        public List<MatriculaInHouseDTO> ObtenerListaInHouse();
        public bool InsertarCambiosPeriodo(List<SubidaExcelDTO> Json, string usuario);
        public bool ActualizarEstadoInHouseMatricula(int IdMatriculaCabecera, int EsInHouse);

        public bool ActualizarEstadoInHouseCodigoMatricula(string CodigoMatricula, int EsInHouse, string usuario);
        public List<CongeladosDTO> ExportarCongelados(FechaInicioFinDTO fechas);


    }
}
