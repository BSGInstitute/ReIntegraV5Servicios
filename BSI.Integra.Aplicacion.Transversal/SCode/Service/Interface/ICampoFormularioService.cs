using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICampoFormularioService
    {
        #region Metodos Base
        CampoFormulario Add(CampoFormulario entidad);
        CampoFormulario Update(CampoFormulario entidad);
        bool Delete(int id, string usuario);

        List<CampoFormulario> Add(List<CampoFormulario> listadoEntidad);
        List<CampoFormulario> Update(List<CampoFormulario> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<CampoFormularioDTO> ObtenerCampoFormulario();

        IEnumerable<CampoFormularioSeleccionadoDTO> ObtenerCampoFormularioPorIdFormularioSolicitud(int idFormularioSolicitud);
    }
}
