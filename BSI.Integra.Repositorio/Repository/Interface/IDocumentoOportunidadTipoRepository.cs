using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoOportunidadTipoRepository : IGenericRepository<TDocumentoOportunidadTipo>
    {
        #region Metodos Base
        TDocumentoOportunidadTipo Add(DocumentoOportunidadTipo entidad);
        TDocumentoOportunidadTipo Update(DocumentoOportunidadTipo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoOportunidadTipo> Add(IEnumerable<DocumentoOportunidadTipo> listadoEntidad);
        IEnumerable<TDocumentoOportunidadTipo> Update(IEnumerable<DocumentoOportunidadTipo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoOportunidadTipoDTO> ObtenerDocumentoOportunidadTipo();
        IEnumerable<DocumentoOportunidadTipoComboDTO> ObtenerCombo();
    }
}