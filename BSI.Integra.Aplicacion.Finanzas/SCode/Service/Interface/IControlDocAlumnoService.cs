using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IControlDocAlumnoService
    {
        #region Metodos Base
        ControlDocAlumno Add(ControlDocAlumno entidad);
        ControlDocAlumno Update(ControlDocAlumno entidad);
        bool Delete(int id, string usuario);

        List<ControlDocAlumno> Add(List<ControlDocAlumno> listadoEntidad);
        List<ControlDocAlumno> Update(List<ControlDocAlumno> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        ControlDocumentoAlumnoDTO ActualizarControlDocAlumno(ControlDocumentoAlumnoDTO ControlDocumentoAlumnoDTO, string usuario);
        bool ActualizarCriterioCalificacion(CriterioObservacionDTO dto, string usuario);
        bool ActualizarMatriculaObservacion(CriterioObservacionDTO dto, string usuario);
    }
}
