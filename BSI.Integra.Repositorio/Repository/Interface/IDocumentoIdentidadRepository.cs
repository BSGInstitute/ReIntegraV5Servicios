using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoIdentidadRepository : IGenericRepository<TDocumentoIdentidad>
    {
        #region Metodos Base
        TDocumentoIdentidad Add(DocumentoIdentidad entidad);
        TDocumentoIdentidad Update(DocumentoIdentidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoIdentidad> Add(IEnumerable<DocumentoIdentidad> listadoEntidad);
        IEnumerable<TDocumentoIdentidad> Update(IEnumerable<DocumentoIdentidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoIdentidadComboDTO> ObtenerCombo();
    }
}
