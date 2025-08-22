using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IFiltroSegmentoValorTipoService
    {
        #region Metodos Base
        FiltroSegmentoValorTipo Add(FiltroSegmentoValorTipo entidad);
        FiltroSegmentoValorTipo Update(FiltroSegmentoValorTipo entidad);
        bool Delete(int id, string usuario);

        List<FiltroSegmentoValorTipo> Add(List<FiltroSegmentoValorTipo> listadoEntidad);
        List<FiltroSegmentoValorTipo> Update(List<FiltroSegmentoValorTipo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
     
    }
}
