using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICongelamientoReporteFlujoRepository
    {
        public bool GenerarCongelamientoReporte(List<FlujoCongelamientoDTO> FlujoCongelamiento);
        public List<RecibirDatosReporteFlujoMaestroDTO> ReporteFlujoMaestro(ReporteFlujoMaestroFiltroDTO Parametros);
        public CodigoMatriculaV2DTO ObtenerIdMatriculaPorCodigo(string Codigo);
        public List<RecibirDatosCoordinadores> ObternerTodosCoordinadores();
        public List<MatriculaInHouseDTO> ObtenerListaInHouse();
        public bool InsertarCambiosPeriodo(string data);
        public bool ActualizarEstadoInHouseMatricula(int IdMatriculaCabecera, int EsInHouse);
        public bool ActualizarEstadoInHouseCodigoMatricula(string CodigoMatricula, int EsInHouse, string usuario);
        public List<CongeladosDTO> ExportarCongelados(FechaInicioFinDTO fechas);

    }
}
