using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IControlDocService
    {
        #region Metodos Base
        ControlDoc Add(ControlDoc entidad);
        ControlDoc Update(ControlDoc entidad);
        bool Delete(int id, string usuario);

        List<ControlDoc> Add(List<ControlDoc> listadoEntidad);
        List<ControlDoc> Update(List<ControlDoc> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        List<ControlDocumentoMatriculaDTO> ObtenerDocumentosPorMatriculaCabeceraControl(int idMatriculaCabecera);

        ControlDocumentoDTO ActualizarControlDocumento(ControlDocumentoDTO entidad, string usuario);
    }
}
