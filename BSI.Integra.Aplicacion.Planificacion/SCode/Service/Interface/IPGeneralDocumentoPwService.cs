using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPGeneralDocumentoPwService
    {
        #region Metodos Base
        PGeneralDocumentoPw Add(PGeneralDocumentoPw entidad);
        PGeneralDocumentoPw Update(PGeneralDocumentoPw entidad);
        bool Delete(int id, string usuario);

        List<PGeneralDocumentoPw> Add(List<PGeneralDocumentoPw> listadoEntidad);
        List<PGeneralDocumentoPw> Update(List<PGeneralDocumentoPw> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PGeneralDocumentoPwDTO> ObtenerPGeneralDocumentoPw();
        PGeneralDocumentoSeccionDTO ObtenerSeccionDocumentoPGeneral(int idPGeneral, string tituloSeccion);
    }
}
