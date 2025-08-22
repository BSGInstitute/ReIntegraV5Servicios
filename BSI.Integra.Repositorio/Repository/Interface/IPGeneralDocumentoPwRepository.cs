using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPGeneralDocumentoPwRepository : IGenericRepository<TPgeneralDocumentoPw>
    {
        #region Metodos Base
        TPgeneralDocumentoPw Add(PGeneralDocumentoPw entidad);
        TPgeneralDocumentoPw Update(PGeneralDocumentoPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralDocumentoPw> Add(IEnumerable<PGeneralDocumentoPw> listadoEntidad);
        IEnumerable<TPgeneralDocumentoPw> Update(IEnumerable<PGeneralDocumentoPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PGeneralDocumentoPwDTO> ObtenerPGeneralDocumentoPw();
        PGeneralDocumentoSeccionDTO ObtenerSeccionDocumentoPGeneral(int idPGeneral, string tituloSeccion);
        PGeneralDocumentoPw ObtenerPorId(int id);
        List<PGeneralDocumentoPw> ObtenerPorIdPGeneral(int idPGeneral);
        List<ComboDTO> ObtenerDocumentoProgramaGeneralTrabajosEvaluacion(int idPGeneral);
        IEnumerable<PreEstructuraProgramaDTO> ObtenerPreConfigurarVideoPrograma(int idPGeneral);
    }
}