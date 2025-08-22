using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ITokenPostulanteProcesoSeleccionRepository : IGenericRepository<TTokenPostulanteProcesoSeleccion>
    {
        #region Metodos Base
        TTokenPostulanteProcesoSeleccion Add(TokenPostulanteProcesoSeleccion entidad);
        TTokenPostulanteProcesoSeleccion Update(TokenPostulanteProcesoSeleccion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTokenPostulanteProcesoSeleccion> Add(IEnumerable<TokenPostulanteProcesoSeleccion> listadoEntidad);
        IEnumerable<TTokenPostulanteProcesoSeleccion> Update(IEnumerable<TokenPostulanteProcesoSeleccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TokenPostulanteProcesoSeleccion? ObtenerPorId(int id);
        TokenPostulanteProcesoSeleccionDTO ObtenerUltimoTokenPorPostulanteProcesoSeleccion(int idPostulanteProcesoSeleccion);
    }
}
