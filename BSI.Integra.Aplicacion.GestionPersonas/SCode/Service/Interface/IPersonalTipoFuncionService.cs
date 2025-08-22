using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IPersonalTipoFuncionService
    {
        IEnumerable<PersonalTipoFuncionDTO> Obtener();
        PersonalTipoFuncionDTO Insertar(PersonalTipoFuncionDTO dto, string usuario);
        PersonalTipoFuncionDTO Actualizar(PersonalTipoFuncionDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
