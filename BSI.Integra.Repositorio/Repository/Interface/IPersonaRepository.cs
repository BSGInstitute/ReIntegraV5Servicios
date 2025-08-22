using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPersonaRepository : IGenericRepository<TPersona>
    {
        #region Metodos Base
        TPersona Add(Persona entidad);
        TPersona Update(Persona entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersona> Add(IEnumerable<Persona> listadoEntidad);
        IEnumerable<TPersona> Update(IEnumerable<Persona> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Persona ObtenerPorId(int? idPersona);
        bool ExistePorEmail(string email);
        Persona? ObtenerPorEmail(string email);
    }
}
