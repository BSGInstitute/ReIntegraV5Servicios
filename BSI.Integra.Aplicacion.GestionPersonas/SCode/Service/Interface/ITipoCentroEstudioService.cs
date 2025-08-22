using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface ITipoCentroEstudioService
    {
        IEnumerable<TipoCentroEstudioDTO> Obtener();
        TipoCentroEstudioDTO Insertar(TipoCentroEstudioDTO dto, string usuario);
        TipoCentroEstudioDTO Actualizar(TipoCentroEstudioDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
