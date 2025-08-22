using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICampoContactoRepository : IGenericRepository<TCampoContacto>
    {
        #region Metodos Base
        TCampoContacto Add(CampoContacto entidad);
        TCampoContacto Update(CampoContacto entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCampoContacto> Add(IEnumerable<CampoContacto> listadoEntidad);
        IEnumerable<TCampoContacto> Update(IEnumerable<CampoContacto> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<CampoContactoDTO> ObtenerCampoContacto();
        IEnumerable<CampoContactoFiltroDTO> ObtenerFiltroCampoContacto();
        IEnumerable<CampoContactoTodoDTO> ObtenerFiltroCampoContactoTodo();


    }
}
