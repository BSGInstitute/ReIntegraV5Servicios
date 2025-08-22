using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IPersonalAreaTrabajoService
    {
        IEnumerable<PersonalAreaTrabajoDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        PersonalAreaTrabajoDTO Insertar(PersonalAreaTrabajoDTO dto, string usuario);
        PersonalAreaTrabajoDTO Actualizar(PersonalAreaTrabajoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
