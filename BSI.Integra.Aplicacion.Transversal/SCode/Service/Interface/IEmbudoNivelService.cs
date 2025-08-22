using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEmbudoNivelService
    {
        #region Metodos Base
        EmbudoNivel Add(EmbudoNivel entidad);
        EmbudoNivel Update(EmbudoNivel entidad);
        bool Delete(int id, string usuario);

        List<EmbudoNivel> Add(List<EmbudoNivel> listadoEntidad);
        List<EmbudoNivel> Update(List<EmbudoNivel> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion



        List<DTO.ComboDTO> ObtenerEmbudoNivel();
        }
}
