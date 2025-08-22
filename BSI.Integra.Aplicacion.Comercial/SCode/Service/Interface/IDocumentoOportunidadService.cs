using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IDocumentoOportunidadService
    {
        #region Metodos Base
        DocumentoOportunidad Add(DocumentoOportunidad entidad);
        DocumentoOportunidad Update(DocumentoOportunidad entidad);
        bool Delete(int id, string usuario);

        List<DocumentoOportunidad> Add(List<DocumentoOportunidad> listadoEntidad);
        List<DocumentoOportunidad> Update(List<DocumentoOportunidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoOportunidadDTO> ObtenerDocumentoOportunidad();
        IEnumerable<DocumentoOportunidadComboDTO> ObtenerCombo();
        List<DocumentoOportunidadInsertadoDTO> ObtenerDocumentosPorOportunidad(int idOportunidad);
        ValorIntDTO ObtenerDocOportunidadPorIdYTipo(int idOportunidad, int idTipo);
    }
}
