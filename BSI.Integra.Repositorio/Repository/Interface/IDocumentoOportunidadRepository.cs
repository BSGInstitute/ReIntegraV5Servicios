using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoOportunidadRepository : IGenericRepository<TDocumentoOportunidad>
    {
        #region Metodos Base
        TDocumentoOportunidad Add(DocumentoOportunidad entidad);
        TDocumentoOportunidad Update(DocumentoOportunidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoOportunidad> Add(IEnumerable<DocumentoOportunidad> listadoEntidad);
        IEnumerable<TDocumentoOportunidad> Update(IEnumerable<DocumentoOportunidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoOportunidadDTO> ObtenerDocumentoOportunidad();
        IEnumerable<DocumentoOportunidadComboDTO> ObtenerCombo();
        List<DocumentoOportunidadInsertadoDTO> ObtenerDocumentosPorOportunidad(int idOportunidad);
        ValorIntDTO ObtenerDocOportunidadPorIdYTipo(int idOportunidad, int idTipo);
    }
}