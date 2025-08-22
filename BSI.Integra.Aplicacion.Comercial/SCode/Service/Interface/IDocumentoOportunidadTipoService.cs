using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IDocumentoOportunidadTipoService
    {
        #region Metodos Base
        DocumentoOportunidadTipo Add(DocumentoOportunidadTipo entidad);
        DocumentoOportunidadTipo Update(DocumentoOportunidadTipo entidad);
        bool Delete(int id, string usuario);

        List<DocumentoOportunidadTipo> Add(List<DocumentoOportunidadTipo> listadoEntidad);
        List<DocumentoOportunidadTipo> Update(List<DocumentoOportunidadTipo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoOportunidadTipoDTO> ObtenerDocumentoOportunidadTipo();
        IEnumerable<DocumentoOportunidadTipoComboDTO> ObtenerCombo();
    }
}
