using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoLegalAreaTrabajoRepository : IGenericRepository<TDocumentoLegalAreaTrabajo>
    {
        #region Metodos Base
        TDocumentoLegalAreaTrabajo Add(DocumentoLegalAreaTrabajo entidad);
        TDocumentoLegalAreaTrabajo Update(DocumentoLegalAreaTrabajo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoLegalAreaTrabajo> Add(IEnumerable<DocumentoLegalAreaTrabajo> listadoEntidad);
        IEnumerable<TDocumentoLegalAreaTrabajo> Update(IEnumerable<DocumentoLegalAreaTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoLegalAreaTrabajoDTO> ObtenerDocumentoLegalAreaTrabajo();
        IEnumerable<DocumentoLegalAreaTrabajoComboDTO> ObtenerCombo();
    }
}