using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IRetencionService
    {
        #region Metodos Base
        Retencion Add(RetencionRecibidoDTO data, string Usuario);
        Retencion Update(RetencionRecibidoDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<Retencion> Add(List<Retencion> listadoEntidad);
        List<Retencion> Update(List<Retencion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<RetencionDTO> ObtenerRetencion();
        IEnumerable<RetencionComboDTO> ObtenerCombo();
    }
}
