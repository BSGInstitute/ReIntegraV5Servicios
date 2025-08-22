using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface IGradoEstudioService
    {
        IEnumerable<GradoEstudioDTO> Obtener();
        GradoEstudioDTO Insertar(GradoEstudioDTO dto, string usuario);
        GradoEstudioDTO Actualizar(GradoEstudioDTO dto, string usuario);
        bool Eliminar(int id, string usuario);

    }
}
