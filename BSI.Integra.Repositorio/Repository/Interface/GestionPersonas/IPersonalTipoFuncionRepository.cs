using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPersonalTipoFuncionRepository : IGenericRepository<TPersonalTipoFuncion>
    {
        #region Metodos Base
        TPersonalTipoFuncion Add(PersonalTipoFuncion entidad);
        TPersonalTipoFuncion Update(PersonalTipoFuncion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalTipoFuncion> Add(IEnumerable<PersonalTipoFuncion> listadoEntidad);
        IEnumerable<TPersonalTipoFuncion> Update(IEnumerable<PersonalTipoFuncion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PersonalTipoFuncionDTO> Obtener();
        PersonalTipoFuncion? ObtenerPorId(int id);
    }
}
