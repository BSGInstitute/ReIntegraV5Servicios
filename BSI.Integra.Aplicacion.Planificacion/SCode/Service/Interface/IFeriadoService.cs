using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IFeriadoService
    {
        IEnumerable<FeriadoDTO> Listar();
        FeriadoDTO ObtenerPorId(int id);
        IEnumerable<FeriadoConPaisDTO> ListarPorPaises(IEnumerable<int> idsTroncalPais);
        FeriadoDTO Insertar(FeriadoDTO dto, string usuario);
        FeriadoDTO Actualizar(FeriadoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        IEnumerable<ComboTroncalCiudadDTO> ComboTroncalCiudad();
        IEnumerable<ComboTroncalPaisDTO> ComboTroncalPais();
    }
}
