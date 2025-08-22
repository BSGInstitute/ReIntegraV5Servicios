using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IMontoPagoCronogramaService
    {
        #region Metodos Base
        MontoPagoCronograma Add(MontoPagoCronograma entidad);
        MontoPagoCronograma Update(MontoPagoCronograma entidad);
        bool Delete(int id, string usuario);

        List<MontoPagoCronograma> Add(List<MontoPagoCronograma> listadoEntidad);
        List<MontoPagoCronograma> Update(List<MontoPagoCronograma> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<MontoPagoCronogramaDTO> ObtenerMontoPagoCronograma();
        IEnumerable<MontoPagoCronogramaComboDTO> ObtenerCombo();
        MontoPagoCronograma ObtenerPorIdOportunidad(int idOportunidad);
        MontoPagoCronogramaDocumentoDTO ObtenerConogramaPagoDocumentoPorIdOportunidad(int idOportunidad);
        MontoPagoCronogramaDocumentoDTO ObtenerConogramaPagoDocumentoPorIdOportunidadOperaciones(int idOportunidad);
        string ObtenerCostoTotalConDescuento(int idOportunidad);
        List<MontoPagadoDTO> ObtenerMontoPagado(int idMatriculaCabecera, int idOportunidad);
        MontoPagoCronogramaDTO ObtenerPorId(int idCronograma);
        ResultadoDTO GenerarCronogramaPorCoordinador(int idCronograma);
        ResultadoDTO EliminarCronogramaVentasPorCoordinador(int idCronograma);
        DatosUsuarioPortalDTO ObtenerUsuarioClavePortalWeb(int idAlumno, string email);
        DatosUsuarioPortalDTO CrearUsuarioClavePortalWeb(int idAlumno, string email, string clave, string claveEncriptada, string nombres, string apellidos, string telefono, string celular, int? idCodigoCiudad, int? idCodigoPais, DateTime fecha);
        List<DetalleMontoPagoDTO> ObtenerDetalleMontoPago(int idMontoPago);
        ResultadoDTO CuotaPagada(string codigoMatricula);
        List<TipoDescuentoOportunidadDTO> ObtenerTipoDescuento(int idOportunidad, string tipoPersonal);
        List<DatosMontosComplementariosDTO> ObtenerMontosComplementarios(int idPGeneral, int idPais, int idMontoPago, int idMatriculaCabecera);
        List<MontoPagoOportunidadDTO> ObtenerMontosPagos(int idOportunidad);
        MontoPagoCronogramaCompletoDTO ObtenerPorIdOportunidadYTipoPersonal(int idOportunidad, string tipoPersonal);
        ResumenCronogramaMontoPagoDTO ObtenerOportunidadCronogramaPago(int idOportunidad, string tipoPersonal);
    }
}
