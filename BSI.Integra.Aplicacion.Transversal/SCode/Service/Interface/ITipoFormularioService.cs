using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITipoFormularioService
    {
        #region Metodos Base
        TipoFormulario Add(TipoFormulario entidad);
        TipoFormulario Update(TipoFormulario entidad);
        bool Delete(int id, string usuario);

        List<TipoFormulario> Add(List<TipoFormulario> listadoEntidad);
        List<TipoFormulario> Update(List<TipoFormulario> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        
        }
}
