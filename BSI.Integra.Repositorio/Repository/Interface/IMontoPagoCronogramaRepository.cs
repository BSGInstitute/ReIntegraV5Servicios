using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMontoPagoCronogramaRepository : IGenericRepository<TMontoPagoCronograma>
    {
        #region Metodos Base
        TMontoPagoCronograma Add(MontoPagoCronograma entidad);
        TMontoPagoCronograma Update(MontoPagoCronograma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMontoPagoCronograma> Add(IEnumerable<MontoPagoCronograma> listadoEntidad);
        IEnumerable<TMontoPagoCronograma> Update(IEnumerable<MontoPagoCronograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MontoPagoCronogramaDTO> ObtenerMontoPagoCronograma();
        IEnumerable<MontoPagoCronogramaComboDTO> ObtenerCombo();
        MontoPagoCronograma ObtenerPorIdOportunidad(int idOportunidad);
        Task<MontoPagoCronograma> ObtenerPorIdOportunidadAsync(int idOportunidad);
        MontoPagoCronogramaDocumentoDTO ObtenerConogramaPagoDocumentoPorIdOportunidad(int idOportunidad);
        MontoPagoCronogramaDocumentoDTO ObtenerConogramaPagoDocumentoPorIdOportunidadOperaciones(int idOportunidad);
        List<SesionesDTO> ObtenerSesionesOnline(int idPespecifico);
        List<MontoPagadoDTO> ObtenerMontoPagado(int idMatriculaCabecera, int idOportunidad);
        MontoPagoCronogramaDTO ObtenerPorId(int idCronograma);
        ResultadoDTO GenerarCronogramaPorCoordinador(int idCronograma);
        ResultadoDTO EliminarCronogramaVentasPorCoordinador(int idCronograma);
        DatosUsuarioPortalDTO ObtenerUsuarioClavePortalWeb(int idAlumno, string email);
        DatosUsuarioPortalDTO CrearUsuarioClavePortalWeb(int idAlumno, string email, string clave, string claveEncriptada, string nombres, string apellidos, string telefono, string celular, int? idCodigoCiudad, int? idCodigoPais, DateTime fecha);
        List<DetalleMontoPagoDTO> ObtenerDetalleMontoPago(int idMontoPago);
        ResultadoDTO? CuotaPagada(string codigoMatricula);
        List<TipoDescuentoOportunidadDTO> ObtenerTipoDescuento(int idOportunidad, string tipoPersonal);
        List<DatosMontosComplementariosDTO> ObtenerMontosComplementarios(int idPGeneral, int idPais, int idMontoPago, int idMatriculaCabecera);
        List<MontoPagoOportunidadDTO> ObtenerMontosPagos(int idOportunidad);
        MontoPagoCronogramaCompletoDTO ObtenerPorIdOportunidadYTipoPersonal(int idOportunidad, string tipoPersonal);
        List<ValorIntDTO> listadoIdsPorOportunidad(int idOportunidad);
        ResultadoFiltroReporteCompromisoDTO ObtenerReporteCompromisoPagoFiltrado(PaginadorDTO Paginador, ReporteCompromisoPagoDTO Filtro, GridFiltersDTO FilterGrid);
    }
}