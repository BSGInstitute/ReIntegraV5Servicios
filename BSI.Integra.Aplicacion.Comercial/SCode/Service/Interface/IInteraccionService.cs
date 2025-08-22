using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IInteraccionService
    {
        #region Metodos Base
        Interaccion Add(Interaccion entidad);
        Interaccion Update(Interaccion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<Interaccion> Add(IEnumerable<Interaccion> listadoEntidad);
        IEnumerable<Interaccion> Update(IEnumerable<Interaccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
