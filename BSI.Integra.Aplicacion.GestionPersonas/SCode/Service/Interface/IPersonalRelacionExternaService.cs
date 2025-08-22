using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IPersonalRelacionExternaService
    {
        IEnumerable<PersonalRelacionExternaDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerAreaTrabajo();
        PersonalRelacionExternaDTO Insertar(PersonalRelacionExternaDTO dto, string usuario);
        PersonalRelacionExternaDTO Actualizar(PersonalRelacionExternaDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
