using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IDocumentoIdentidadService
    {
        #region Metodos Base
        DocumentoIdentidad Add(DocumentoIdentidad entidad);
        DocumentoIdentidad Update(DocumentoIdentidad entidad);
        bool Delete(int id, string usuario);

        List<DocumentoIdentidad> Add(List<DocumentoIdentidad> listadoEntidad);
        List<DocumentoIdentidad> Update(List<DocumentoIdentidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DocumentoIdentidadComboDTO> ObtenerCombo();
    }
}
