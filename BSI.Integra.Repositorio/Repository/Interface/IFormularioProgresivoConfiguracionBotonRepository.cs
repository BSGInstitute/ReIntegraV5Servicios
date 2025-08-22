using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormularioProgresivoConfiguracionBotonRepository : IGenericRepository<TFormularioProgresivoConfiguracionBoton>
    {
        #region Metodos Base
        List<TFormularioProgresivoConfiguracionBoton> Add(FormularioProgresivoConfiguracionBoton entidad);
        List<TFormularioProgresivoConfiguracionBoton> Update(FormularioProgresivoConfiguracionBoton entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivoConfiguracionBoton ObtenerPorId(int id);
        IEnumerable<FormularioProgresivoConfiguracionBoton> ObtenerPorIdFormularioProgresivo(int idFormularioProgresivo);
    }
}
