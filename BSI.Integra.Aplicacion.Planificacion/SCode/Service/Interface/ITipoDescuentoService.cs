using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ITipoDescuentoService
    {

        CompuestoTipoDescuentoDTO Insertar(CompuestoTipoDescuentoDTO dto, string usuario);
        CompuestoTipoDescuentoDTO Actualizar(CompuestoTipoDescuentoDTO dto, string usuario);

        IEnumerable<TipoDescuentoDTO> Obtener();
        ComboTipoDescuentoDTO ObtenerCombosModulo();
        IEnumerable<TipoDescuentoOportunidadDTO> ObtenerTipoDescuentoOportunidad(int idOportunidad, string tipoPersonal);
        IEnumerable<string> ObtenerTiposPorIdTipoDescuento(int idDescuentoAsesor);
        bool Eliminar(int id, string usuario);
        IEnumerable<TipoDescuentoConNivelAprobacionDTO> ObtenerTipoDescuentoConNivelAprobacion();
        IEnumerable<TipoDescuentoNivelAprobacionDTO> ObtenerNivelesAprobacion();
        InfoProgramaCentroCostoDTO ObtenerInfoProgramaPorCentroCosto(int idCentroCosto);

    }
}
