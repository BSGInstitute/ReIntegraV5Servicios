using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IArticuloPGeneralService
    {
        #region Metodos Base
        ArticuloPGeneral Add(ArticuloPGeneral entidad);
        ArticuloPGeneral Update(ArticuloPGeneral entidad);
        bool Delete(int id, string usuario);
        List<ArticuloPGeneral> Add(List<ArticuloPGeneral> listadoEntidad);
        List<ArticuloPGeneral> Update(List<ArticuloPGeneral> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public List<ArticuloPGeneral> ObtenerArticuloPGeneralAsociados(int IdArticulo);
    }
}
