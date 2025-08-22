using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface ITipoExperienciaService
    {
        IEnumerable<TipoExperienciaDTO> Obtener();
        TipoExperienciaDTO Insertar(TipoExperienciaDTO dto, string usuario);
        TipoExperienciaDTO Actualizar(TipoExperienciaDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
