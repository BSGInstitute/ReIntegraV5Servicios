using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPersonalRelacionExternaRepository : IGenericRepository<TPersonalRelacionExterna>
    {
        #region Metodos Base
        TPersonalRelacionExterna Add(PersonalRelacionExterna entidad);
        TPersonalRelacionExterna Update(PersonalRelacionExterna entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalRelacionExterna> Add(IEnumerable<PersonalRelacionExterna> listadoEntidad);
        IEnumerable<TPersonalRelacionExterna> Update(IEnumerable<PersonalRelacionExterna> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PersonalRelacionExternaDTO> Obtener();
        PersonalRelacionExterna? ObtenerPorId(int id);
    }
}
