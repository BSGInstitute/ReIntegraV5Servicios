using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IOportunidadMaximaPorCategoriaService
    {
        #region Metodos Base
        OportunidadMaximaPorCategoria Add(OportunidadMaximaPorCategoria entidad);
        OportunidadMaximaPorCategoria Update(OportunidadMaximaPorCategoria entidad);
        bool Delete(int id, string usuario);

        List<OportunidadMaximaPorCategoria> Add(List<OportunidadMaximaPorCategoria> listadoEntidad);
        List<OportunidadMaximaPorCategoria> Update(List<OportunidadMaximaPorCategoria> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OportunidadMaximaPorCategoriaDTO> ObtenerOportunidadMaximaPorCategoria();
        IEnumerable<OportunidadMaximaPorCategoriaComboDTO> ObtenerCombo();
        SeguimientoAsesorDTO ObtenerSeguimientoAsesor(int idPersonal, int idCategoriaOrigen, int estadoPantalla);
        void ActualizarDatosEstaticosPantalla2(int idAsesor, int idCategoriaOrigen, int estadoISOM);
    }
}
