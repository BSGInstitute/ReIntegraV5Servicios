using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface INivelCompetenciaTecnicaService
    {
        IEnumerable<NivelCompetenciaTecnicaDTO> Obtener();
        NivelCompetenciaTecnicaDTO Insertar(NivelCompetenciaTecnicaDTO dto, string usuario);
        NivelCompetenciaTecnicaDTO Actualizar(NivelCompetenciaTecnicaDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
