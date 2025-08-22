using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICampoContactoService
    {
        #region Metodos Base
        CampoContacto Add(CampoContactoDTO entidad, string Usuario);
        CampoContacto Update(CampoContactoDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<CampoContacto> Add(List<CampoContacto> listadoEntidad);
        List<CampoContacto> Update(List<CampoContacto> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<CampoContactoDTO> ObtenerCampoContacto();
        IEnumerable<CampoContactoFiltroDTO> ObtenerFiltroCampoContacto();
        IEnumerable<CampoContactoTodoDTO> ObtenerFiltroCampoContactoTodo();


    }
}
