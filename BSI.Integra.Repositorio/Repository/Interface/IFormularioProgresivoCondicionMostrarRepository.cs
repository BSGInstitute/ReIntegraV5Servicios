using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormularioProgresivoCondicionMostrarRepository : IGenericRepository<TFormularioProgresivoCondicionMostrar>
    {
        #region Metodos Base
        List<TFormularioProgresivoCondicionMostrar> Add(FormularioProgresivoCondicionMostrar entidad);
        List<TFormularioProgresivoCondicionMostrar> Update(FormularioProgresivoCondicionMostrar entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivoCondicionMostrar ObtenerPorId(int id);
        IEnumerable<FormularioProgresivoCondicionMostrar> ObtenerRegistros();
    }
}
