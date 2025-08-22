using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IBloqueHorarioProcesaOportunidadService
    {
        #region Metodos Base
        BloqueHorarioProcesaOportunidad Add(BloqueHorarioProcesaOportunidad entidad);
        BloqueHorarioProcesaOportunidad Update(BloqueHorarioProcesaOportunidad entidad);
        bool Delete(int id, string usuario);

        List<BloqueHorarioProcesaOportunidad> Add(List<BloqueHorarioProcesaOportunidad> listadoEntidad);
        List<BloqueHorarioProcesaOportunidad> Update(List<BloqueHorarioProcesaOportunidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
