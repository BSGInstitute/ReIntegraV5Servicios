using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFormularioSolicitudTextoBotonService
    {
        #region Metodos Base
        FormularioSolicitudTextoBoton Add(FormularioSolicitudTextoBotonDTO entidad, string Usuario);
        FormularioSolicitudTextoBoton Update(FormularioSolicitudTextoBotonDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<FormularioSolicitudTextoBoton> Add(List<FormularioSolicitudTextoBoton> listadoEntidad);
        List<FormularioSolicitudTextoBoton> Update(List<FormularioSolicitudTextoBoton> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<FormularioSolicitudTextoBotonDTO> ObtenerFormularioSolicitudTextoBoton();
        IEnumerable<FormularioSolicitudTextoBotonFiltroDTO> ObtenerFiltroFormularioSolicitudTextoBoton();

    }
}
