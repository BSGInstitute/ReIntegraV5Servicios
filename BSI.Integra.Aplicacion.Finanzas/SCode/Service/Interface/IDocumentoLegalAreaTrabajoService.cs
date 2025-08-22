using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IDocumentoLegalAreaTrabajoService
    {
        #region Metodos Base
        DocumentoLegalAreaTrabajo Add(DocumentoLegalAreaTrabajo entidad);
        DocumentoLegalAreaTrabajo Update(DocumentoLegalAreaTrabajo entidad);
        bool Delete(int id, string usuario);

        List<DocumentoLegalAreaTrabajo> Add(List<DocumentoLegalAreaTrabajo> listadoEntidad);
        List<DocumentoLegalAreaTrabajo> Update(List<DocumentoLegalAreaTrabajo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DocumentoLegalAreaTrabajoDTO> ObtenerDocumentoLegalAreaTrabajo();
        IEnumerable<DocumentoLegalAreaTrabajoComboDTO> ObtenerCombo();
    }
}
