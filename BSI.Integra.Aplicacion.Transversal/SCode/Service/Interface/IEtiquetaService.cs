using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEtiquetaService
    {
        #region Metodos Base
        Etiqueta Add(Etiqueta entidad);
        Etiqueta Update(Etiqueta entidad);
        bool Delete(int id, string usuario);

        List<Etiqueta> Add(List<Etiqueta> listadoEntidad);
        List<Etiqueta> Update(List<Etiqueta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<EtiquetaDTO> ObtenerEtiqueta();
        IEnumerable<EtiquetaComboDTO> ObtenerCombo();
        IEnumerable<Etiqueta> ObtenerPorIdNodoPadre(int idNodoPadre);
    }
}
