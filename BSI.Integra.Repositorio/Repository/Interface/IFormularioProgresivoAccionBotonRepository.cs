using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormularioProgresivoAccionBotonRepository : IGenericRepository<TFormularioProgresivoAccionBoton>
    {
        #region Metodos Base
        List<TFormularioProgresivoAccionBoton> Add(FormularioProgresivoAccionBoton entidad);
        List<TFormularioProgresivoAccionBoton> Update(FormularioProgresivoAccionBoton entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivoAccionBoton ObtenerPorId(int id);
        IEnumerable<FormularioProgresivoAccionBoton> ObtenerRegistros();
    }
}
