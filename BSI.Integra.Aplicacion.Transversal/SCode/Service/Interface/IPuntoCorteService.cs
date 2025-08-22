using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPuntoCorteService
    {
        #region Metodos Base
        PuntoCorte Add(PuntoCorte entidad);
        PuntoCorte Update(PuntoCorte entidad);
        bool Delete(int id, string usuario);

        List<PuntoCorte> Add(List<PuntoCorte> listadoEntidad);
        List<PuntoCorte> Update(List<PuntoCorte> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
