using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITroncalPgeneralService
    {
        #region Metodos Base
        TroncalPgeneral Add(TroncalPgeneral entidad);
        TroncalPgeneral Update(TroncalPgeneral entidad);
        bool Delete(int id, string usuario);

        List<TroncalPgeneral> Add(List<TroncalPgeneral> listadoEntidad);
        List<TroncalPgeneral> Update(List<TroncalPgeneral> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        object ObtenerTroncalPgeneral();

    }
}
